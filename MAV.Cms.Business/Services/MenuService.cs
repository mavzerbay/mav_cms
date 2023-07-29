using AutoMapper;
using FluentValidation.Results;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Exceptions;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Business.Specifications;
using MAV.Cms.Business.Validators;
using MAV.Cms.Common.BaseModels;
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
    public class MenuService : IMenuService
    {
        private readonly IBaseRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuService> _logger;
        private readonly IUploadService _uploadService;
        private readonly ICustomVarService _customVarService;
        private readonly ICacheService _cacheService;
        public MenuService(IUnitOfWork uow, IMapper mapper, ILogger<MenuService> logger, IUploadService uploadService, ICustomVarService customVarService, ICacheService cacheService)
        {
            _menuRepository = uow.Repository<Menu>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
            _customVarService = customVarService ?? throw new ArgumentNullException(nameof(customVarService));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task<ApiResponse<MenuResponse>> Create(CreateMenuDTO request)
        {
            try
            {
                var menu = _mapper.Map<Menu>(request);

                CreateMenuValidator validator = new CreateMenuValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                menu.FillChildMasterId();

                if (request.BackgroundImageFile != null && request.BackgroundImageFile.Length > 0)
                {
                    var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                    {
                        DataId = menu.Id,
                        ModelName = nameof(Menu),
                        File = request.BackgroundImageFile,
                        OverriteName = "bg",
                    });
                    if (uploadResponse.IsSuccess)
                        menu.BackgroundImagePath = uploadResponse.Url;

                }

                await _menuRepository.AddAsync(menu);

                await _menuRepository.SaveChangesAsync();

                if (!menu.IsBackend)
                {

                    var keys = _cacheService.GetKeysByPattern("TopMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }

                    keys = _cacheService.GetKeysByPattern("BottomLeftMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }

                    keys = _cacheService.GetKeysByPattern("BottomMiddleMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }

                    keys = _cacheService.GetKeysByPattern("BottomRightMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }
                }

                var spec = new MenuSpec(menu.Id);
                var menuResponse = await _menuRepository.GetEntityWithSpec<MenuResponse>(spec, _mapper);
                return new ApiResponse<MenuResponse>(menuResponse, true, StatusCodes.Status201Created, string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("Menu.ControllerTitle")));
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
                var menu = await _menuRepository.GetByIdAsync(request.Id);
                if (menu == null)
                    throw new NotFoundException("Menu", request.Id);

                if (request.IsSoftDelete)
                {
                    menu.SetModelAndChildSoftDelete();
                    _menuRepository.Update(menu);
                }
                else
                    _menuRepository.Delete(menu);

                var result = await _menuRepository.SaveChangesAsync();

                if (!menu.IsBackend)
                {
                    var keys = _cacheService.GetKeysByPattern("TopMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }

                    keys = _cacheService.GetKeysByPattern("BottomLeftMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }

                    keys = _cacheService.GetKeysByPattern("BottomMiddleMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }

                    keys = _cacheService.GetKeysByPattern("BottomRightMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }
                }

                return new ApiResponse<bool>(result > 0, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("Menu.ControllerTitle")));
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

        public async Task<ApiResponse<MenuResponse>> GetById(Guid Id)
        {
            try
            {

                var spec = new MenuSpec(Id);
                var menuResponse = await _menuRepository.GetEntityWithSpec<MenuResponse>(spec, _mapper);
                if (menuResponse == null)
                    throw new NotFoundException("Menu", Id);

                menuResponse.BackgroundImagePath = menuResponse.BackgroundImagePath.ResolveUrl();
                return new ApiResponse<MenuResponse>(menuResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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

        public async Task<ApiResponse<MenuResponse>> GetAll(GetAllMenuSpecDTO request)
        {
            try
            {
                if (!request.LanguageId.HasValue)
                    request.LanguageId = LangManager.CurrentLanguageId;

                var spec = new MenuSpec(request);
                var specCount = new MenuSpecCount(request);
                var menuCount = await _menuRepository.CountAsync(specCount);
                var menuResponse = await _menuRepository.ListWithSpecAsync<MenuResponse>(spec, _mapper);

                return new ApiResponse<MenuResponse>(request.PageIndex, request.PageSize, menuCount, menuResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<MenuResponse>> Update(UpdateMenuDTO request)
        {
            try
            {
                var spec = new MenuSpec(request.Id);
                var menu = await _menuRepository.GetEntityWithSpec(spec);
                if (menu == null)
                    throw new NotFoundException("Menu", request.Id);

                _mapper.Map(request, menu, typeof(UpdateMenuDTO), typeof(Menu));

                UpdateMenuValidator validator = new UpdateMenuValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                menu.FillChildMasterId();

                if (request.BackgroundImageFile != null && request.BackgroundImageFile.Length > 0)
                {
                    var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                    {
                        DataId = menu.Id,
                        ModelName = nameof(Menu),
                        File = request.BackgroundImageFile,
                        OverriteName = "bg",
                        ReplacePath = request.BackgroundImagePath,
                    });
                    if (uploadResponse.IsSuccess)
                        menu.BackgroundImagePath = uploadResponse.Url;
                }
                else if (request.BackgroundImagePath.HasValue())
                    menu.BackgroundImagePath = request.BackgroundImagePath;

                _menuRepository.Update(menu);

                await _menuRepository.SaveChangesAsync();

                if (!menu.IsBackend)
                {
                    var keys = _cacheService.GetKeysByPattern("TopMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }

                    keys = _cacheService.GetKeysByPattern("BottomLeftMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }

                    keys = _cacheService.GetKeysByPattern("BottomMiddleMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }

                    keys = _cacheService.GetKeysByPattern("BottomRightMenu");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));
                    }
                }

                var menuResponse = await _menuRepository.GetEntityWithSpec<MenuResponse>(spec, _mapper);

                return new ApiResponse<MenuResponse>(menuResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("Menu.ControllerTitle")));
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

        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllMenuSpecDTO request)
        {
            try
            {
                if (!request.LanguageId.HasValue)
                    request.LanguageId = LangManager.CurrentLanguageId;

                if (!request.isAll.HasValue)
                    request.isAll = true;

                if (!request.Activity.HasValue)
                    request.Activity = true;

                var spec = new MenuSpec(request);
                var specCount = new MenuSpecCount(request);
                var menuCount = await _menuRepository.CountAsync(specCount);
                var menuResponse = await _menuRepository.ListWithSpecAsync<BaseDropdownResponse>(spec, _mapper);

                return new ApiResponse<BaseDropdownResponse>(request.PageIndex, request.PageSize, menuCount, menuResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<ClientMenuResponse>> GetClientMenuList(GetAllMenuSpecDTO request)
        {
            try
            {
                if (!request.LanguageId.HasValue)
                    request.LanguageId = LangManager.CurrentLanguageId;

                if (!request.isAll.HasValue)
                    request.isAll = true;

                if (!request.Activity.HasValue)
                    request.Activity = true;

                if (!request.IncludePage.HasValue)
                    request.IncludePage = true;

                Guid? menuPositionId = null;

                IReadOnlyList<ClientMenuResponse> menuResponse = null;
                int menuCount = 0;

                string cacheKey = string.Empty;

                if (request.Position.HasValue && request.IsBackend.HasValue && !request.IsBackend.Value)
                {

                    switch (request.Position.Value)
                    {
                        case 0: cacheKey = $"TopMenu:{request.LanguageId}"; break;
                        case 1: cacheKey = $"BottomLeftMenu:{request.LanguageId}"; break;
                        case 2: cacheKey = $"BottomMiddleMenu:{request.LanguageId}"; break;
                        case 3: cacheKey = $"BottomRightMenu:{request.LanguageId}"; break;
                    }
                    var menuJson = await _cacheService.GetCacheAsync(cacheKey);
                    if (menuJson.HasValue())
                    {
                        menuResponse = JsonSerializer.Deserialize<IReadOnlyList<ClientMenuResponse>>(menuJson);
                    }
                    else
                    {
                        var customVarResponse = await _customVarService.GetAllCustomVar();
                        if (request.Position.Value == 0)
                        {
                            if (customVarResponse.IsSuccess && customVarResponse.DataMulti.Any(x => x.GroupName == "MenuPosition" && x.KeyName == "Top"))
                                menuPositionId = customVarResponse.DataMulti.FirstOrDefault(x => x.GroupName == "MenuPosition" && x.KeyName == "Top").Id;

                        }
                        else if (request.Position.Value == 1)
                        {

                            if (customVarResponse.IsSuccess && customVarResponse.DataMulti.Any(x => x.GroupName == "MenuPosition" && x.KeyName == "BottomLeft"))
                                menuPositionId = customVarResponse.DataMulti.FirstOrDefault(x => x.GroupName == "MenuPosition" && x.KeyName == "BottomLeft").Id;
                        }
                        else if (request.Position.Value == 2)
                        {

                            if (customVarResponse.IsSuccess && customVarResponse.DataMulti.Any(x => x.GroupName == "MenuPosition" && x.KeyName == "BottomMiddle"))
                                menuPositionId = customVarResponse.DataMulti.FirstOrDefault(x => x.GroupName == "MenuPosition" && x.KeyName == "BottomMiddle").Id;
                        }
                        else if (request.Position.Value == 3)
                        {

                            if (customVarResponse.IsSuccess && customVarResponse.DataMulti.Any(x => x.GroupName == "MenuPosition" && x.KeyName == "BottomRight"))
                                menuPositionId = customVarResponse.DataMulti.FirstOrDefault(x => x.GroupName == "MenuPosition" && x.KeyName == "BottomRight").Id;
                        }

                        request.MenuPositionId = menuPositionId;
                    }
                }

                if (request.IsBackend.HasValue && (request.IsBackend.Value || (!request.IsBackend.Value && request.Position.HasValue && request.MenuPositionId.HasValue) || menuResponse == null))
                {
                    var spec = new MenuSpec(request);
                    var specCount = new MenuSpecCount(request);
                    menuCount = await _menuRepository.CountAsync(specCount);
                    menuResponse = await _menuRepository.ListWithSpecAsync<ClientMenuResponse>(spec, _mapper);

                    menuResponse = GetMenusWithChild(menuResponse.Where(x => !x.ParentMenuId.HasValue).ToList(), menuResponse.Where(x => x.ParentMenuId.HasValue).ToList());

                    if (cacheKey.HasValue())
                    {
                        await _cacheService.SetCacheAsync(cacheKey, menuResponse, TimeSpan.FromDays(30));
                    }
                }

                return new ApiResponse<ClientMenuResponse>(request.PageIndex, request.PageSize, menuCount, menuResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        private List<ClientMenuResponse> GetMenusWithChild(List<ClientMenuResponse> parentMenuList, List<ClientMenuResponse> childMenuList = null)
        {
            List<ClientMenuResponse> clientMenuList = new List<ClientMenuResponse>();
            if (childMenuList != null && childMenuList.Any())
            {
                for (int i = 0; i < parentMenuList.Count; i++)
                {
                    if (childMenuList.Any(x => x.ParentMenuId.HasValue && x.ParentMenuId.Equals(parentMenuList[i].Id)))
                    {
                        var responseList = GetMenusWithChild(childMenuList, childMenuList.Where(x => x.ParentMenuId.HasValue && x.ParentMenuId.Equals(parentMenuList[i].Id)).ToList());
                        if (responseList != null && responseList.Any())
                            clientMenuList.AddRange(responseList);
                    }
                }
                return clientMenuList.OrderBy(x => x.DisplayOrder).ToList();
            }
            else
                return parentMenuList.OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
