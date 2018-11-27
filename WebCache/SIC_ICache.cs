using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sephiroth.Infrastructure.Common.WebCache
{
    /// <summary>
    /// 
    /// </summary>
    public interface SIC_ICache
    {

        /// <summary>
        /// 读取缓存项
        /// </summary>
        /// <returns></returns>
        object CacheReader(string cacheKey);

        /// <summary>
        /// 写入缓存项
        /// </summary>
        void CacheWriter(string cacheKey, object cacheValue, int cache_time = 0);


        /// <summary>
        /// 移除指定缓存项
        /// </summary>
        void CacheRemove(string cacheName);

		/// <summary>
		/// 移除指定缓存项
		/// </summary>
		void CacheRemove<T>(string cacheKey);

        /// <summary>
        /// 缓存对象泛型实现
        /// </summary>
        T ObjectReader<T>(string cacheKey = null) where T : class;

        /// <summary>
        /// 缓存对象泛型实现
        /// </summary>
        void ObjectWriter<T>(T cacheValue, string cacheKey = null, int cache_time = 0)
            where T : class;
    }
}
