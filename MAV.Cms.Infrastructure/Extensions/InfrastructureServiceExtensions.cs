using MAV.Cms.Common.Interfaces;
using MAV.Cms.Common.Services;
using MAV.Cms.Domain.Interfaces;
using MAV.Cms.Infrastructure.Interfaces;
using MAV.Cms.Infrastructure.Repositories;
using MAV.Cms.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MAV.Cms.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
