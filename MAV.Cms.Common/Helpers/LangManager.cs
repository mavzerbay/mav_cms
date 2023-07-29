using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Interfaces;
using MAV.Cms.Common.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MAV.Cms.Common.Helpers
{
    public class LangManager
    {
        private static IHttpContextAccessor _httpContext => new HttpContextAccessor();

        public static Guid CurrentLanguageId
        {
            get
            {
                return GetUserLanguageId();
            }
        }
        public static string Translate(string key)
        {
            var cacheService = _httpContext.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            string cachedResponse = cacheService.GetCache("Translation");

            if (cachedResponse.HasValue())
            {
                var translationList = JsonSerializer.Deserialize<IReadOnlyList<TranslationResponse>>(cachedResponse);
                if (translationList != null && translationList.Any(x => x.KeyName == key))
                    return translationList.FirstOrDefault(x => x.KeyName == key).Translation;
            }

            return key;
        }

        public static bool IsPrimaryLanguage(Guid languageId)
        {
            //languageId uyuşuyorsa true
            return languageId == CurrentLanguageId;
        }

        private static Guid GetUserLanguageId()
        {
            if (_httpContext.HttpContext.Request.Headers.Any(x => x.Key == "langid"))
                return Guid.Parse(_httpContext.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "langid").Value);
            return Guid.Parse("08d9a936-de58-4ee7-804c-55b1456e2374");
        }
    }
}
