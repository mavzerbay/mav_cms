using MAV.Cms.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MAV.Cms.Common.Helpers
{
    public static class PathResolver
    {
        private static IHttpContextAccessor _httpContextAccessor => new HttpContextAccessor();
        public static string ResolveUrl(this string source, IConfiguration config = null)
        {
            if (source.HasValue())
            {
                if (config == null)
                    config = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                var baseUrl = config["ApiUrl"];
                source = source.StartsWith(baseUrl) ? source : $"{baseUrl}{source}";
            }
            return source;
        }
    }
}
