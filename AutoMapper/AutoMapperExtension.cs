using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sephiroth.Infrastructure.Common.AutoMapper
{ 

    /// <summary>
    /// object 扩展
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        /// 对象到对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) return default(T);
            var config = new MapperConfiguration(cfg => cfg.CreateMap(obj.GetType(), typeof(T)));
            var mapper = config.CreateMapper();
            return mapper.Map<T>(obj);
        }

        /// <summary>
        /// 集合到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="ListT"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> MapTo<T,ListT>(this IEnumerable obj)
        {
            if (obj == null) throw new ArgumentNullException();
            Mapper.Initialize(ctx => ctx.CreateMap(typeof(ListT), typeof(T)));
            return Mapper.Map<List<T>>(obj);
        }
    }


}
