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
        private int pagesize = 10;
        /// <summary>
        /// 页面大小 默认10
        /// </summary>
        public int pageSize { get { return pagesize; } set { pagesize = value; } }

        private int pagenumber = -1;
        /// <summary>
        /// 页码 默认-1 显示所有 不分页
        /// </summary>
        public int pageNumber { get { return pagenumber; } set { pagenumber = value; } } 

        /// <summary>
        /// order by 字段 
        /// </summary>
        [Required]
        public string sortName { get; set; }

        /// <summary>
        /// asc desc
        /// </summary>
        [Required]
        public string sortOrder { get; set; }

        /// <summary>
        /// 页面开始位置
        /// </summary>
        public int pageStart
        {
            get
            {
                return (pageNumber - 1) * pageSize + 1;
            }
        }

        /// <summary>
        /// 页面结束位置
        /// </summary>
        public int pageEnd
        {
            get { return pageNumber * pageSize; }
        }
    }
}
