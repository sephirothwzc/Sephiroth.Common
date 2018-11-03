using System;
using System.Diagnostics;
using DevExpress.Xpo;
using Newtonsoft.Json;

namespace Sephiroth.Infrastructure.Common.JsonUtil
{
    /// <summary>
    /// json 序列化
    /// </summary>
    public class SIC_Json
    {
        #region 字符串对象序列化--ObjectConvertJson
        /// <summary> 
        /// 字符串对象序列化 
        /// </summary> 
        /// <param name="jsonstr"></param> 
        /// <returns></returns> 
        public static T ObjectConvertJson<T>(string jsonstr)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonstr) || jsonstr == "{}" || jsonstr == "Token String in state Start would result in an invalid JavaScript object.")
                    return default(T);
                if (!typeof(T).Name.Equals("Object"))//不等于动态类型 
                    return JsonConvert.DeserializeObject<T>(jsonstr);
                return default(T);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.Print(ex.Message);
#endif
                throw new NotImplementedException(jsonstr + "\r\n+" + ex.Message);
            }
        }

        #endregion
    }
}