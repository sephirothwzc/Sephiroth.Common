using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
   * CLR 版本：       4.0.30319.42000
   * 类 名 称：       SIC_Enum
   * 机器名称：       BE27
   * 命名空间：       Sephiroth.Infrastructure.Common.Enum
   * 文 件 名：       SIC_Enum
   * 创建时间：       2017/5/18 下午3:34:09
   * 作    者：       吴占超
   * 说    明：        
   * 修改时间：
   * 修 改 人：
  *************************************************************************************/

namespace Sephiroth.Infrastructure.Common.Enums
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class SIC_Enum
    {
        /// <summary>
        /// name to type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Parse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
