using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Es.Udc.DotNet.PracticaMaD.Model.Util
{
    public static class CacheUtil
    {
        public static T GetFromCache<T>(string key)
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (T)cache[key];

            return cachedObject;
        }

        public static void AddToCache<T>(string key, T value)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1);

            if (cache.Count() >= 5)
            {
                KeyValuePair<string, object> keyValue = cache.First();
                cache.Remove(keyValue.Key);
            }

            cache.Add(key, value, policy);

        }
    }
}
