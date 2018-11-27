using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sephiroth.Infrastructure.Common.Attributes
{
    /// <summary>
    /// 分页参数 基础类，拥有 分页属性参数
    /// </summary>
    public class PaginationParam
    {
        /// <summary>
        /// 页面大小 默认10
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 页码 默认-1 显示所有 不分页
        /// </summary>
        public int PageNumber { get; set; } = -1; 

        /// <summary>
        /// order by 字段 
        /// </summary>
        [Required]
        public string SortName { get; set; }

        /// <summary>
        /// asc desc
        /// </summary>
        [Required]
        public string SortOrder { get; set; }

        /// <summary>
        /// 页面开始位置
        /// </summary>
        public int PageStart
        {
            get
            {
                return (PageNumber - 1) * PageSize + 1;
            }
        }

        /// <summary>
        /// 页面结束位置
        /// </summary>
        public int PageEnd
        {
            get { return PageNumber * PageSize; }
        }
    }
}
