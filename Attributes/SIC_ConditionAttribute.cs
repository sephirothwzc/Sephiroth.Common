using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sephiroth.Infrastructure.Common.Attributes
{
    /// <summary>
    /// 自定义where 条件拼凑
    /// </summary>
    public sealed class SIC_ConditionAttribute: Attribute
    {
        /// <summary>
        /// 条件键值对
        /// </summary>
        public static Dictionary<condition, string> conditionStr = new Dictionary<condition, string>()
        {
            { condition.eq," = " },
            { condition.gt," > " },
            { condition.gte," >= " },
            { condition.like," like " },
            { condition.lt," < " },
            { condition.lte," <= " },
            { condition.ne," != " },
        };

        /// <summary>
        /// 条件
        /// </summary>
        public enum condition
        {
            /// <summary>
            /// 等于
            /// </summary>
            eq = 1,
            /// <summary>
            /// 不等于
            /// </summary>
            ne,
            /// <summary>
            /// 大于
            /// </summary>
            gt,
            /// <summary>
            /// 小于
            /// </summary>
            lt,
            /// <summary>
            /// 大于等于
            /// </summary>
            gte,
            /// <summary>
            /// 小于等于
            /// </summary>
            lte,
            /// <summary>
            ///  like %{0}% 
            /// </summary>
            like,
        }

        /// <summary>
        /// 列名 条件列
        /// </summary>
        public string column { get; set; }

        private condition _type = condition.like;

        /// <summary>
        /// 类型
        /// </summary>
        public condition type { get { return _type; } set { _type = value; } } 
    }
}
