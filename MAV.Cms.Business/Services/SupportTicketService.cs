using AutoMapper;
using FluentValidation.Results;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.EMail;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Exceptions;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Business.Specifications;
using MAV.Cms.Business.Validators;
using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Common.Interfaces;
using MAV.Cms.Domain.Entities;
using MAV.Cms.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Services
{
    public class SupportTicketService : ISupportTicketService
    {
        private readonly IBaseRepository<SupportTicket> _supportTicketRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SupportTicketService> _logger;
        private readonly IMailService _mailService;
        private readonly ICacheService _cacheService;
        private readonly ICustomVarService _customVarService;
        public SupportTicketService(IUnitOfWork uow, IBaseRepository<SupportTicket> supportTicketRepository, IMapper mapper, ILogger<SupportTicketService> logger, IMailService mailService, ICacheService cacheService, ICustomVarService customVarService)
        {
            _supportTicketRepository = uow.Repository<SupportTicket>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _customVarService = customVarService ?? throw new ArgumentNullException(nameof(customVarService));
        }

        public async Task<ApiResponse<SupportTicketResponse>> Create(CreateSupportTicketDTO request)
        {
            try
            {

                var customVarJson = await _cacheService.GetCacheAsync("CustomVar");
                string subject = null;
                if (customVarJson.HasValue())
                {
                    var customVarResponse = JsonSerializer.Deserialize<IReadOnlyList<CustomVarResponse>>(customVarJson);
                    if (customVarResponse != null && customVarResponse.Any(x => x.Id == request.SupportTypeId))
                    {
                        subject = customVarResponse.FirstOrDefault(x => x.Id == request.SupportTypeId).CustomVarTrans.FirstOrDefault(x => x.LanguageId == LangManager.CurrentLanguageId).Name;
                    }
                }

                if (!subject.HasValue())
                {
                    var customVarResponse = await _customVarService.GetAllCustomVar("SupportType");
                    if (customVarResponse != null && customVarResponse.IsSuccess && customVarResponse.DataMulti.Any(x => x.Id == request.SupportTypeId))
                    {
                        subject = customVarResponse.DataMulti.FirstOrDefault(x => x.Id == request.SupportTypeId).CustomVarTrans.FirstOrDefault(x => x.LanguageId == LangManager.CurrentLanguageId).Name;
                    }
                }

                var generalSettingsJson = await _cacheService.GetCacheAsync("GeneralSettings");
                string supportMail = "mavzerbay@gmail.com";
                if (generalSettingsJson.HasValue())
                {
                    var generalSettings = JsonSerializer.Deserialize<GeneralSettingsResponse>(generalSettingsJson);
                    if (generalSettings != null && generalSettings.SupportMail.HasValue())
                    {
                        supportMail = generalSettings.SupportMail;
                    }
                }

                var body = $"<p><b>{LangManager.Translate("Common.NameSurname")}</b> : {string.Format("{0} {1}", request.Name, request.Surname)}<p>" +
                    $"<p><b>{LangManager.Translate("Common.Email")}</b> : {request.Email}<p>" +
                    string.Format("{0}", request.PhoneNumber.HasValue() ? $"<p><b>{LangManager.Translate("Common.PhoneNumber")}</b> : {request.PhoneNumber}<p>" : "") +
                    $"<p><b>{LangManager.Translate("Contact.Message")}</b></p>" +
                    $"{request.Content}";

                var mail = new EmailDTO()
                {
                    MailPriority = System.Net.Mail.MailPriority.High,
                    Body = body,
                    Subject = subject,
                    ToEmail = supportMail,
                };

                var mailResponse = await _mailService.SendMailAsync(mail);

                SupportTicketResponse supportTicketResponse = null;
                if (mailResponse)
                {

                    var supportTicket = _mapper.Map<SupportTicket>(request);

                    CreateSupportTicketValidator validator = new CreateSupportTicketValidator();
                    ValidationResult validationResult = validator.Validate(request);

                    if (!validationResult.IsValid)
                    {
                        throw new ValidationException(validationResult.Errors);
                    }

                    await _supportTicketRepository.AddAsync(supportTicket);

                    await _supportTicketRepository.SaveChangesAsync();

                    var spec = new SupportTicketSpec(supportTicket.Id);
                    supportTicketResponse = await _supportTicketRepository.GetEntityWithSpec<SupportTicketResponse>(spec, _mapper);
                    supportTicketResponse.MailSended = mailResponse;

                }
                return new ApiResponse<SupportTicketResponse>(supportTicketResponse, supportTicketResponse != null, StatusCodes.Status201Created, supportTicketResponse != null ? string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("SupportTicket.ControllerTitle")) :"");
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<bool>> Delete(BaseDeleteDTO request)
        {
            try
            {
                var supportTicket = await _supportTicketRepository.GetByIdAsync(request.Id);
                if (supportTicket == null)
                    throw new NotFoundException("SupportTicket", request.Id);

                if (request.IsSoftDelete)
                {
                    supportTicket.isSoftDelete = true;
                    _supportTicketRepository.Update(supportTicket);
                }
                else
                    _supportTicketRepository.Delete(supportTicket);

                var result = await _supportTicketRepository.SaveChangesAsync();

                return new ApiResponse<bool>(result > 0, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("SupportTicket.ControllerTitle")));
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("404", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<SupportTicketResponse>> GetById(Guid Id)
        {
            try
            {

                var spec = new SupportTicketSpec(Id);
                var supportTicketResponse = await _supportTicketRepository.GetEntityWithSpec<SupportTicketResponse>(spec, _mapper);
                if (supportTicketResponse == null)
                    throw new NotFoundException("SupportTicket", Id);

                return new ApiResponse<SupportTicketResponse>(supportTicketResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("404", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<SupportTicketResponse>> GetAll(GetAllSupportTicketSpecDTO request)
        {
            try
            {

                var spec = new SupportTicketSpec(request);
                var specCount = new SupportTicketSpecCount(request);
                var supportTicketCount = await _supportTicketRepository.CountAsync(specCount);
                var supportTicketResponse = await _supportTicketRepository.ListWithSpecAsync<SupportTicketResponse>(spec, _mapper);

                return new ApiResponse<SupportTicketResponse>(request.PageIndex, request.PageSize, supportTicketCount, supportTicketResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<SupportTicketResponse>> Update(UpdateSupportTicketDTO request)
        {
            try
            {
                var spec = new SupportTicketSpec(request.Id);
                var supportTicket = await _supportTicketRepository.GetEntityWithSpec(spec);
                if (supportTicket == null)
                    throw new NotFoundException("SupportTicket", request.Id);

                supportTicket.IsClosed = request.IsClosed;

                _supportTicketRepository.Update(supportTicket);

                await _supportTicketRepository.SaveChangesAsync();

                var supportTicketResponse = await _supportTicketRepository.GetEntityWithSpec<SupportTicketResponse>(spec, _mapper);

                return new ApiResponse<SupportTicketResponse>(supportTicketResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("SupportTicket.ControllerTitle")));
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ValidationException(ex.Errors);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("404", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
