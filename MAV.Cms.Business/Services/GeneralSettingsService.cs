using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Business.Specifications;
using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Common.Interfaces;
using MAV.Cms.Domain.Entities;
using MAV.Cms.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Services
{
    public class GeneralSettingsService : IGeneralSettingsService
    {
        private readonly IBaseRepository<GeneralSettings> _generalSettingsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GeneralSettingsService> _logger;
        private readonly IUploadService _uploadService;
        private readonly IConfiguration _config;
        private readonly ICacheService _cacheService;
        public GeneralSettingsService(IUnitOfWork uow, IMapper mapper, ILogger<GeneralSettingsService> logger, IUploadService uploadService, IConfiguration config, ICacheService cacheService)
        {
            _generalSettingsRepository = uow.Repository<GeneralSettings>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task<ApiResponse<GeneralSettingsResponse>> CreateOrUpdate(CreateOrUpdateGeneralSettingsDTO request)
        {
            try
            {
                bool isCreate = true;
                var spec = new GeneralSettingsSpec(new GetAllGeneralSettingsSpecDTO());
                var data = await _generalSettingsRepository.ListWithSpecAsync(spec);
                GeneralSettings generalSettings = null;
                if (data != null && data.Any())
                {
                    isCreate = false;
                    generalSettings = data.FirstOrDefault();
                    _mapper.Map(request, generalSettings, typeof(CreateOrUpdateGeneralSettingsDTO), typeof(GeneralSettings));
                }
                else
                {
                    generalSettings = _mapper.Map<GeneralSettings>(request);
                }

                generalSettings.FillChildMasterId();
                if (request.GeneralSettingsTrans != null && request.GeneralSettingsTrans.Any())
                {
                    for (int i = 0; i < request.GeneralSettingsTrans.Count; i++)
                    {
                        if (request.GeneralSettingsTrans.ElementAt(i).ContactOgImageFile != null && request.GeneralSettingsTrans.ElementAt(i).ContactOgImageFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = generalSettings.Id,
                                ModelName = nameof(GeneralSettings),
                                File = request.GeneralSettingsTrans.ElementAt(i).ContactOgImageFile,
                            });
                            if (uploadResponse.IsSuccess)
                                generalSettings.GeneralSettingsTrans.FirstOrDefault(x => x.LanguageId == request.GeneralSettingsTrans.ElementAt(i).LanguageId).ContactOgImage = uploadResponse.Url;
                        }
                        else if (request.GeneralSettingsTrans.ElementAt(i).ContactOgImage.HasValue())
                            generalSettings.GeneralSettingsTrans.FirstOrDefault(x => x.LanguageId == request.GeneralSettingsTrans.ElementAt(i).LanguageId).ContactOgImage = request.GeneralSettingsTrans.ElementAt(i).ContactOgImage;

                        if (request.GeneralSettingsTrans.ElementAt(i).HomeOgImageFile != null && request.GeneralSettingsTrans.ElementAt(i).HomeOgImageFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = generalSettings.Id,
                                ModelName = nameof(GeneralSettings),
                                File = request.GeneralSettingsTrans.ElementAt(i).HomeOgImageFile,
                            });
                            if (uploadResponse.IsSuccess)
                                generalSettings.GeneralSettingsTrans.FirstOrDefault(x => x.LanguageId == request.GeneralSettingsTrans.ElementAt(i).LanguageId).HomeOgImage = uploadResponse.Url;
                        }
                        else if (request.GeneralSettingsTrans.ElementAt(i).HomeOgImage.HasValue())
                            generalSettings.GeneralSettingsTrans.FirstOrDefault(x => x.LanguageId == request.GeneralSettingsTrans.ElementAt(i).LanguageId).HomeOgImage = request.GeneralSettingsTrans.ElementAt(i).HomeOgImage;

                        if (request.GeneralSettingsTrans.ElementAt(i).LogoFile != null && request.GeneralSettingsTrans.ElementAt(i).LogoFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = generalSettings.Id,
                                ModelName = nameof(Page),
                                File = request.GeneralSettingsTrans.ElementAt(i).LogoFile,
                            });
                            if (uploadResponse.IsSuccess)
                                generalSettings.GeneralSettingsTrans.FirstOrDefault(x => x.LanguageId == request.GeneralSettingsTrans.ElementAt(i).LanguageId).LogoPath = uploadResponse.Url;
                        }
                        else if (request.GeneralSettingsTrans.ElementAt(i).LogoPath.HasValue())
                            generalSettings.GeneralSettingsTrans.FirstOrDefault(x => x.LanguageId == request.GeneralSettingsTrans.ElementAt(i).LanguageId).LogoPath = request.GeneralSettingsTrans.ElementAt(i).LogoPath;

                        if (request.GeneralSettingsTrans.ElementAt(i).IcoFile != null && request.GeneralSettingsTrans.ElementAt(i).IcoFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = generalSettings.Id,
                                ModelName = nameof(Page),
                                File = request.GeneralSettingsTrans.ElementAt(i).IcoFile,
                            });
                            if (uploadResponse.IsSuccess)
                                generalSettings.GeneralSettingsTrans.FirstOrDefault(x => x.LanguageId == request.GeneralSettingsTrans.ElementAt(i).LanguageId).IcoPath = uploadResponse.Url;
                        }
                        else if (request.GeneralSettingsTrans.ElementAt(i).IcoPath.HasValue())
                            generalSettings.GeneralSettingsTrans.FirstOrDefault(x => x.LanguageId == request.GeneralSettingsTrans.ElementAt(i).LanguageId).IcoPath = request.GeneralSettingsTrans.ElementAt(i).IcoPath;
                    }
                }

                if (isCreate)
                    await _generalSettingsRepository.AddAsync(generalSettings);
                else
                    _generalSettingsRepository.Update(generalSettings);

                await _generalSettingsRepository.SaveChangesAsync();

                await _cacheService.RemoveFromCacheAsync("GeneralSettings");

                var response = await Get();

                return new ApiResponse<GeneralSettingsResponse>(response.DataSingle, true, isCreate ? StatusCodes.Status201Created : StatusCodes.Status204NoContent, string.Format(isCreate ? LangManager.Translate("Common.Created") : LangManager.Translate("Common.Updated"), LangManager.Translate("Page.ControllerTitle")));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<GeneralSettingsResponse>> Get()
        {

            try
            {
                var generalSettingsJson = await _cacheService.GetCacheAsync("GeneralSettings");
                GeneralSettingsResponse generalSettingsResponse = null;
                if (generalSettingsJson.HasValue())
                {
                    generalSettingsResponse = JsonSerializer.Deserialize<GeneralSettingsResponse>(generalSettingsJson);
                }
                else
                {
                    var spec = new GeneralSettingsSpec(new GetAllGeneralSettingsSpecDTO());
                    var generalSettingsResponseList = await _generalSettingsRepository.ListWithSpecAsync<GeneralSettingsResponse>(spec, _mapper);
                    if (generalSettingsResponseList != null && generalSettingsResponseList.Any())
                    {
                        generalSettingsResponse = generalSettingsResponseList.FirstOrDefault();
                        for (int i = 0; i < generalSettingsResponse.GeneralSettingsTrans.Count; i++)
                        {
                            generalSettingsResponse.GeneralSettingsTrans.ElementAt(i).IcoPath = generalSettingsResponse.GeneralSettingsTrans.ElementAt(i).IcoPath.ResolveUrl(_config);
                            generalSettingsResponse.GeneralSettingsTrans.ElementAt(i).LogoPath = generalSettingsResponse.GeneralSettingsTrans.ElementAt(i).LogoPath.ResolveUrl(_config);
                            generalSettingsResponse.GeneralSettingsTrans.ElementAt(i).ContactOgImage = generalSettingsResponse.GeneralSettingsTrans.ElementAt(i).ContactOgImage.ResolveUrl(_config);
                            generalSettingsResponse.GeneralSettingsTrans.ElementAt(i).HomeOgImage = generalSettingsResponse.GeneralSettingsTrans.ElementAt(i).HomeOgImage.ResolveUrl(_config);
                        }
                        await _cacheService.SetCacheAsync("GeneralSettings", generalSettingsResponse, TimeSpan.FromDays(30));
                    }
                }

                return new ApiResponse<GeneralSettingsResponse>(generalSettingsResponse, generalSettingsResponse != null, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
