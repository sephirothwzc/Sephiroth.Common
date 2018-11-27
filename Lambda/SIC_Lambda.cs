using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
   * CLR 版本：       4.0.30319.42000
   * 类 名 称：       SIC_Lambda
   * 机器名称：       BE27
   * 命名空间：       Sephiroth.Infrastructure.Common.Lambda
   * 文 件 名：       SIC_Lambda
   * 创建时间：       2017/6/8 上午11:15:23
   * 作    者：       吴占超
   * 说    明：        
   * 修改时间：
   * 修 改 人：
  *************************************************************************************/

namespace Sephiroth.Infrastructure.Common.Lambda
{
    /// <summary>
    /// 
    /// </summary>
    public class SIC_Lambda
    {

        /// <summary>
        /// 递归方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<T> Fix<T>(Func<Func<T>, Func<T>> f)
        {
            return f(Fix(f));
        }

        /// <summary>
        /// 递归方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<T, TResult> Fix<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> f)
        {
            return x => f(Fix(f))(x);
        }

        /// <summary>
        /// 递归运算
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<T1, T2, TResult> Fix<T1, T2, TResult>(Func<Func<T1, T2, TResult>, Func<T1, T2, TResult>> f)
        {
            return (x, y) => f(Fix(f))(x, y);
        }
    }
}
