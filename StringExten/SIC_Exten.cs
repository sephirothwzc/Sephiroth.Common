namespace Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Security;

    /// <summary>
    /// 通用字符串扩展
    /// </summary>
    public static class SIC_Exten
    {
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        public static string EnCode(this string str, CodeType toType)
        {
            string str2 = str;
            switch (toType)
            {
                case CodeType.MD5:
                    return GetSwcMD5(str);
                    //return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");

                case CodeType.SHA1:
                    return GetSwcSH1(str);
                    //return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
            }
            return str2;
        }

        private static string GetSwcSH1(string value)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }

        private static string GetSwcMD5(string value)
        {
            MD5 algorithm = MD5.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }

        public static string EnCode(this string str, string key, CodeType toType)
        {
            string str2 = str;
            switch (toType)
            {
                case CodeType.MD5:
                    return GetSwcMD5(key + str);
                    //return FormsAuthentication.HashPasswordForStoringInConfigFile(key + str, "md5");

                case CodeType.SHA1:
                    return GetSwcSH1(key + str);
                    //return FormsAuthentication.HashPasswordForStoringInConfigFile(key + str, "SHA1");
            }
            return str2;
        }

        //public static string GetParam(string str)
        //{
        //    if (HttpContext.Current.Request.QueryString[str] != null)
        //    {
        //        return HttpContext.Current.Request.QueryString[str].Replace("script", "<i>script<i>");
        //    }
        //    if (HttpContext.Current.Request.Form[str] != null)
        //    {
        //        return HttpContext.Current.Request.Form[str].Replace("script", "<i>script<i>");
        //    }
        //    return "";
        //}

        public static bool NotEmpty(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static double ToDouble(this string str, double def)
        {
            double num;
            if (double.TryParse(str, out num))
            {
                return num;
            }
            return def;
        }

        /// <summary>
        /// TryParse
        /// </summary>
        /// <param name="str"></param>
        /// <param name="def">转换失败默认int参数</param>
        /// <returns></returns>
        public static int ToInt(this string str, int def = 0)
        {
            int num;
            if (int.TryParse(str, out num))
            {
                return num;
            }
            return def;
        }

        public static long ToTimeMs(this DateTime dt, DateTime beginTime)
        {
            long ticks = dt.Ticks - beginTime.Ticks;
            TimeSpan span = new TimeSpan(ticks);
            return (long) span.TotalMilliseconds;
        }

        public static string Week(string date)
        {
            string str = DateTime.Parse(date).DayOfWeek.ToString();
            string str3 = str;
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "Monday"))
            {
                if (str3 != "Tuesday")
                {
                    if (str3 == "Wednesday")
                    {
                        return "星期三";
                    }
                    if (str3 == "Friday")
                    {
                        return "星期五";
                    }
                    if (str3 == "Saturday")
                    {
                        return "星期六";
                    }
                    if (str3 != "Sunday")
                    {
                        return str;
                    }
                    return "星期日";
                }
            }
            else
            {
                return "星期一";
            }
            return "星期二";
        }

        public enum CodeType
        {
            DES = 3,
            MD5 = 1,
            SHA1 = 2
        }

        /// <summary>
        /// 获取中文字符串的首字母
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetChineseSpell(this string str)
        {
            int len = str.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += str.Substring(i, 1).getSpell();
            }
            return myStr;
        }

        /// <summary>
        /// 获取单个中文的首字母
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        private static string getSpell(this string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };

                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25)
                    {
                        max = areacode[i + 1];
                    }
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(97 + i) });
                    }
                }
                return "*";
            }
            else return cnChar;
        }

        ///   <summary>
        ///   给一个字符串进行MD5加密
        ///   </summary>
        ///   <param   name="strText">待加密字符串</param>
        ///   <returns>加密后的字符串</returns>
        public static string MD5Encrypt(this string strText)
        {
            byte[] data = Encoding.GetEncoding("GB2312").GetBytes(strText);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);

            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }
            // return OutString.ToUpper();
            return OutString.ToLower();

            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
            //return System.Text.Encoding.Default.GetString(result);
        }
    }
}

