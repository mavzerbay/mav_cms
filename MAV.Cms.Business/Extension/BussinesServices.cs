using FluentValidation;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MAV.Cms.Business.Extension
{
    public static class BussinesServices
    {
        public static IServiceCollection AddBussinesServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            #region CRUD Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomVarService,CustomVarService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPageCommentService, PageCommentService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<ISlideService, SlideService>();
            services.AddScoped<ISupportTicketService, SupportTicketService>();
            services.AddScoped<ITranslateService, TranslateService>();
            services.AddScoped<IGeneralSettingsService, GeneralSettingsService>();
            #endregion

            services.AddScoped<IUploadService, UploadService>();
            services.AddTransient<IMailService, MailService>();

            return services;
        }
    }
}
