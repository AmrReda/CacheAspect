using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace CacheAspect.Repository.Cache
{
    public static class RedisHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("amrreda-redis.redis.cache.windows.net,abortConnect=false,ssl=true,password=wQBlPAXncFppn8AuYXHA3OwHWs5CeijkEwVzatJeO9U="));

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }


        public static bool AddObject<T>(this IDatabase database, string key, T value, TimeSpan expiresAt) where T : class
        {
            var serializedObject = JsonConvert.SerializeObject(value);
           
            return database.StringSet(key, serializedObject, expiresAt);
        }

        public static T GetObject<T>(this IDatabase database, string key) where T : class
        {
            var serializedObject = database.StringGet(key);

            return JsonConvert.DeserializeObject<T>(serializedObject);
        }
    }
}
