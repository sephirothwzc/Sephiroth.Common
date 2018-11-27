using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Converters;

namespace Sephiroth.Infrastructure.Common.WebClient
{
    /// <summary>
    /// 
    /// </summary>
    public class SIC_WebClient
    {
        /// <summary>
        /// 
        /// </summary>
        public SIC_WebClient()
        {
        }

        /// <summary> 
        /// 根据参数集合拼凑json提交字符串 
        /// </summary> 
        /// <param name="datas"></param> 
        /// <returns></returns> 
        private string CreateDataJson(IDictionary<string, object> datas = null)
        {
            if (datas == null)
                return string.Empty;
            var namevalues = datas.Select(d =>
            string.Format(@"""{0}"":{1}"
            , d.Key
            , d.Value.GetType().IsValueType
            ? string.Format(@"""{0}""", (d.Value ?? "").ToString())
            : this.ObjectToJson(d.Value))
            ).ToArray();

            return string.Format("{{{0}}}", string.Join(",", namevalues));
        }

		#region ObjectToJson--对象序列化json

		/// <summary> 
		/// ObjectToJson 对象序列化 返回json 
		/// </summary> 
		/// <param name="obj"></param> 
		/// <returns></returns> 
		public string ObjectToJson(object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
		#endregion

        /// <summary>
        /// webclient 请求
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="uri">URI.</param>
        /// <param name="datas">Datas.</param>
        /// <param name="method">Method.</param>
        /// <param name="charset">Charset.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
		public T WebRequest<T>(Uri uri, IDictionary<string, object> datas = null, string method = "POST", string charset = "UTF8")
		{
			string data = this.CreateDataJson(datas);

			using (System.Net.WebClient webClient = new System.Net.WebClient())
			{
				//webClient.Encoding = (Encoding)Enum.Parse(typeof(Encoding), charset); 
				webClient.Headers["Method"] = method.ToString();
				webClient.Headers["Content-Type"] = string.Format(@"application/json;charset={0}", charset);
                webClient.Encoding = Encoding.UTF8;
				string dwstring;
				if ("POST".Equals(method))
					dwstring = webClient.UploadString(uri, method, data);
				else
					dwstring = webClient.DownloadString(uri);
				return JsonConvert.DeserializeObject<T>(dwstring);
			}
		}


        /// <summary>
        /// webclient 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="charset"></param>
        /// <param name="DateTimeFormats"></param>
        /// <returns></returns>
        public T WebRequest<T>(string url, object data = null, string method = "POST", string charset = "UTF8", string DateTimeFormats = "yyyy-MM-dd HH:mm:ss")
        {
            string outjson = string.Empty;
            return this.WebRequest<T>(url, out outjson, data, method, charset, DateTimeFormats);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="charset"></param>
        /// <param name="DateTimeFormats"></param>
        /// <param name="resultJson"></param>
        /// <returns></returns>
        public T WebRequest<T>(string url, out string resultJson, object data = null, string method = "POST", string charset = "UTF8", string DateTimeFormats = "yyyy-MM-dd HH:mm:ss")
        {
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                //webClient.Encoding = (Encoding)Enum.Parse(typeof(Encoding), charset); 
                webClient.Headers["Method"] = method.ToString();
                webClient.Headers["Content-Type"] = string.Format(@"application/json;charset={0}", charset);
                webClient.Encoding = Encoding.UTF8;
                if ("POST".Equals(method))
                {
                    var timeConverter = new IsoDateTimeConverter { DateTimeFormat = DateTimeFormats };
                    string dataString = JsonConvert.SerializeObject(data, Formatting.Indented, timeConverter);
                    resultJson = webClient.UploadString(new Uri(url), method, dataString);
                }
                else
                    resultJson = webClient.DownloadString(new Uri(url));
                return JsonConvert.DeserializeObject<T>(resultJson);
            }
        }


        /// <summary> 
        /// 异步调用返回结果 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="uri"></param> 
        /// <param name="datas"></param> 
        /// <param name="method"></param> 
        /// <param name="charset"></param> 
        /// <returns></returns> 
        public async System.Threading.Tasks.Task<T> WebRequestAsync<T>(Uri uri, IDictionary<string, object> datas = null, string method = "POST", string charset = "UTF8")
		{
			string data = this.CreateDataJson(datas);
			using (System.Net.WebClient webClient = new System.Net.WebClient())
			{
				//webClient.Encoding = (Encoding)Enum.Parse(typeof(Encoding), charset); 
				webClient.Headers["Method"] = method.ToString();
				webClient.Headers["Content-Type"] = string.Format(@"application/json;charset={0}", charset);
				string dwstring;
				if ("POST".Equals(method))
					dwstring = await webClient.UploadStringTaskAsync(uri, method, data);
				else
					dwstring = await webClient.DownloadStringTaskAsync(uri);
				return JsonConvert.DeserializeObject<T>(dwstring);
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="datas"></param>
        /// <param name="method"></param>
        /// <param name="charset"></param>
        /// <param name="RunT"></param>
		public void WebRequestCompleted<T>(Uri uri, IDictionary<string, object> datas = null, string method = "POST", string charset = "UTF8", Action<T> RunT = null)
		{
			string data = this.CreateDataJson(datas);
			using (System.Net.WebClient webClient = new System.Net.WebClient())
			{
				//webClient.Encoding = (Encoding)Enum.Parse(typeof(Encoding), charset); 
				webClient.Headers["Method"] = method.ToString();
				webClient.Headers["Content-Type"] = string.Format(@"application/json;charset={0}", charset);
				if ("POST".Equals(method))
				{
					webClient.UploadStringCompleted += (sender, e) =>
					{
						var dwstring = e.Result;
						if (RunT != null)
							RunT(JsonConvert.DeserializeObject<T>(dwstring));
					};
					webClient.UploadStringAsync(uri, method, data);
				}
				else
				{
					webClient.DownloadStringCompleted += (sender, e) =>
					{
						var dwstring = e.Result;
						if (RunT != null)
							RunT(JsonConvert.DeserializeObject<T>(dwstring));
					};
					webClient.DownloadStringAsync(uri);
				}
			}
		}
	}
}
