using StackExchange.Redis;
using System.Text.Json;

namespace Services.CacheServices
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task SetCacheResponseAsyc(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serlizedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cacheKey , serlizedResponse, timeToLive);
        }
        public async Task<string> GetCacheResponseAsyc(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);

            if (cachedResponse.IsNullOrEmpty)
                return null;

            return cachedResponse;
            

        }

    }
}
