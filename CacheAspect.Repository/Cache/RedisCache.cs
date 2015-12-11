using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace CacheAspect.Repository.Cache
{
    public class RedisCache : ICache
    {
        private const string RedisKeys = "urn:cacheKeys";

        private readonly TimeSpan _defaultTimeout;
        private readonly IDatabase _dataCache;

        public RedisCache()
        {
            _defaultTimeout = new TimeSpan(1, 0, 0);
            _dataCache = RedisHelper.Connection.GetDatabase();
        }

        public object this[string key]
        {
            get { return _dataCache.GetObject<object>(key); }
            set
            {
                UpdateCacheKeys(key);
                _dataCache.AddObject(key, value, _defaultTimeout);
            }
        }

        /// <summary>
        /// Update the cache keys list.
        /// </summary>
        /// <param name="key">New key to add to the cache key list, if it doesn't already exist.</param>
        private void UpdateCacheKeys(string key)
        {
            if (key.Equals(RedisKeys))
            {
                throw new ArgumentException("The current cache key cannot be used as it's already employed by the system.", "key");
            }

            IList<string> cacheKeys;
            var cacheKeysObject = _dataCache.GetObject<object>(RedisKeys);

            if (cacheKeysObject == null)
            {
                cacheKeys = new List<string>();
            }
            else
            {
                cacheKeys = (List<string>)cacheKeysObject;

                if (cacheKeys.Contains(key))
                {
                    return;
                }
            }

            cacheKeys.Add(key);
            _dataCache.AddObject(RedisKeys, cacheKeys, _defaultTimeout);
        }

        public void Remove(string key)
        {
            var cacheKeysObject = _dataCache.GetObject<object>(RedisKeys);

            if (cacheKeysObject == null)
            {
                return;
            }

            var cacheKeys = (List<string>)cacheKeysObject;

            foreach (var cacheKey in cacheKeys.Where(c => c.StartsWith(key)).ToList())
            {
                _dataCache.KeyDelete(cacheKey);
                cacheKeys.Remove(cacheKey);
            }

            _dataCache.AddObject(RedisKeys, cacheKeys, _defaultTimeout);
        }
    }
}
