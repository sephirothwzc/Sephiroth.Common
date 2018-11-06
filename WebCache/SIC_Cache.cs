using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Sephiroth.Infrastructure.Common.WebCache
{
    /// <summary>
    /// 缓存帮助类 
    /// wzc
    /// 2016年12月22日16:09:10
    /// </summary>
    public class SIC_Cache : SIC_ICache
    {

        /// <summary>
        /// 读取缓存项
        /// </summary>
        /// <returns></returns>
        public object CacheReader(string cacheKey)
        {
            return HttpRuntime.Cache[cacheKey];
        }

        /// <summary>
        /// 写入缓存项
        /// </summary>
        public void CacheWriter(string cacheKey, object cacheValue, int cache_time = 0)
        {
            //HttpRuntime.Cache.Insert(cacheKey, cacheValue, null,
            //    DateTime.Now.AddMinutes(cache_time <= 0 ? cacheTime : cache_time),
            //    Cache.NoSlidingExpiration);
            HttpRuntime.Cache.Insert(cacheKey, cacheValue);
        }

        /// <summary>
        /// 移除指定缓存项
        /// </summary>
        public void CacheRemove(string cacheName)
        {
            HttpRuntime.Cache.Remove(cacheName);
        }

        /// <summary>
        /// 缓存对象泛型实现
        /// </summary>
        public T ObjectReader<T>(string cacheKey = null)
            where T : class
        {
            string cachekey = typeof(T).GetHashCode() + cacheKey ?? "";
            var obj = CacheReader(cachekey) as T;
            return obj;
        }

        /// <summary>
        /// 缓存对象泛型实现
        /// </summary>
        public void ObjectWriter<T>(T cacheValue, string cacheKey = null, int cache_time = 0)
            where T : class
        {
            string cachekey = typeof(T).GetHashCode() + cacheKey ?? "";
            CacheWriter(cachekey, cacheValue, cache_time);
        }

        /// <summary>
        /// 缓存删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        public void CacheRemove<T>(string cacheKey)
        {
            string cachekey = typeof(T).GetHashCode() + cacheKey ?? "";
            HttpRuntime.Cache.Remove(cachekey);
        }
    }
}