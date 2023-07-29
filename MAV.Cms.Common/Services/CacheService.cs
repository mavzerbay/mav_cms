using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MAV.Cms.Common.Interfaces;
using StackExchange.Redis;

namespace MAV.Cms.Common.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        private IConnectionMultiplexer _connection;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
            _connection = redis;
        }

        public async Task SetCacheAsync(string cacheKey, object response, TimeSpan timetoLive)
        {
            if (response == null) return;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var serialisedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cacheKey, serialisedResponse, timetoLive);
        }

        public async Task<string> GetCacheAsync(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);

            if (cachedResponse.IsNullOrEmpty)
            {
                return null;
            }

            return cachedResponse;
        }
        public string GetCache(string cacheKey)
        {
            var cachedResponse = _database.StringGet(cacheKey);

            if (cachedResponse.IsNullOrEmpty)
            {
                return null;
            }

            return cachedResponse;
        }
        public IReadOnlyList<RedisKey> GetKeysByPattern(string Pattern)
        {
            var endPoint = _connection.GetEndPoints().FirstOrDefault();
            return _connection.GetServer(endPoint).Keys(pattern: Pattern+"*").ToList();
        }
        public async Task<bool> RemoveFromCacheAsync(string cacheKey)
        {
            return await _database.KeyDeleteAsync(cacheKey);
        }
    }
}