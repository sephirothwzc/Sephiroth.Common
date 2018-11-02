using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
   * CLR 版本：       4.0.30319.42000
   * 类 名 称：       SIC_Result
   * 机器名称：       BE27
   * 命名空间：       Sephiroth.Infrastructure.Common.Result
   * 文 件 名：       SIC_Result
   * 创建时间：       2017/5/18 上午10:05:16
   * 作    者：       吴占超
   * 说    明：        
   * 修改时间：
   * 修 改 人：
  *************************************************************************************/

namespace Sephiroth.Infrastructure.Common.Result
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SIC_Result<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public enum e_state
        {
            /// <summary>
            /// 
            /// </summary>
            异常=-1,
            /// <summary>
            /// 
            /// </summary>
            失败=0,
            /// <summary>
            /// 
            /// </summary>
            成功=1,
        }

        private e_state state = e_state.成功;

        /// <summary>
        /// def 成功
        /// </summary>
        public e_state State
        {
            get { return state; }
            set { state = value; }
        } 

        /// <summary>
        /// 
        /// </summary>
        public string Msg { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class SIC_Result
    {
        /// <summary>
        /// 
        /// </summary>
        public enum e_state
        {
            /// <summary>
            /// 
            /// </summary>
            异常 = -1,
            /// <summary>
            /// 
            /// </summary>
            失败 = 0,
            /// <summary>
            /// 
            /// </summary>
            成功 = 1,
        }

        private e_state state = e_state.成功;
        /// <summary>
        /// def 成功
        /// </summary>
        public e_state State { get { return state; } set { state = value; } }

        private string msg = string.Empty;
        /// <summary>
        /// 错误异常消息
        /// </summary>
        public string Msg { get { return msg; } set { msg = value; } }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

    }
}
