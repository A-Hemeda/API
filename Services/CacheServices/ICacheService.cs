using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CacheServices
{
    public interface ICacheService
    {
        Task SetCacheResponseAsyc(string cacheKey, Object response, TimeSpan timeToLive);
        Task<string> GetCacheResponseAsyc(string cacheKey);
    }
}
