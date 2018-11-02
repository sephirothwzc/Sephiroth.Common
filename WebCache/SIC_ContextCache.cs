using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sephiroth.Infrastructure.Common.WebCache
{
    public class SIC_ContextCache: SIC_ICache
    {
        /// <summary>
        /// 读取缓存项
        /// </summary>
        /// <returns></returns>
        public object CacheReader(string cacheKey)
        {
            return HttpContext.Current.Cache[cacheKey];
        }

        /// <summary>
        /// 写入缓存项
        /// </summary>
        public void CacheWriter(string cacheKey, object cacheValue, int cache_time = 0)
        {
            //HttpRuntime.Cache.Insert(cacheKey, cacheValue, null,
            //    DateTime.Now.AddMinutes(cache_time <= 0 ? cacheTime : cache_time),
            //    Cache.NoSlidingExpiration);
            HttpContext.Current.Cache.Insert(cacheKey, cacheValue);
        }

        /// <summary>
        /// 移除指定缓存项
        /// </summary>
        public void CacheRemove(string cacheName)
        {
            HttpContext.Current.Cache.Remove(cacheName);
        }

		/// <summary>
		/// 移除指定缓存项
		/// </summary>
		public void CacheRemove<T>(string cacheKey = null)
		{
			string cachekey = typeof(T).GetHashCode() + cacheKey ?? "";
			HttpContext.Current.Cache.Remove(cachekey);
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
    }
}
