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
            Mapper.Initialize(ctx => ctx.CreateMap(obj.GetType(), typeof(T)));
            return Mapper.Map<T>(obj);
        }

        /// <summary>
        /// 集合到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>

        public static List<T> MapTo<T>(this IEnumerable obj)
        {
            if (obj == null) throw new ArgumentNullException();
            Mapper.Initialize(ctx => ctx.CreateMap(obj.GetType(), typeof(T)));
            return Mapper.Map<List<T>>(obj);
        }
    }


}
