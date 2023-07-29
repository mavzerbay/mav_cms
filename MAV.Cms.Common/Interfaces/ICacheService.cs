using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAV.Cms.Common.Interfaces
{
    public interface ICacheService
    {
        Task SetCacheAsync(string cacheKey, object response, TimeSpan timetoLive);
        Task<string> GetCacheAsync(string cacheKey);
        string GetCache(string cacheKey);
        Task<bool> RemoveFromCacheAsync(string cacheKey);
        IReadOnlyList<RedisKey> GetKeysByPattern(string Pattern);
    }
}
