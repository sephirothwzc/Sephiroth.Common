using Sephiroth.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SIC_Sign
    {
        #region 属性

        //签名秘钥
        public static string appsecret = "KY0j9HmtJ571Ksma"; //
        //明文加密秘钥
        public static string appkey = "qyAwvU46fc";

        #endregion

        #region 基础方法




        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static bool CheckSign(string signature, ref string cipherText)
        {
            var sec = appsecret.EnCode(SIC_Exten.CodeType.MD5).ToLower();

            var sign = (DESEncryption.Encrypt(cipherText,appkey) + sec).EnCode(SIC_Exten.CodeType.MD5).ToLower();
            if (sign==signature)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 生成签名和密文
        /// </summary>
        /// <param name="signature">签名</param>
        /// <param name="cipherText">明文 签名后 密文</param>
        public static void ToSign(out string signature, ref string cipherText)
        {
            var sec = appsecret.EnCode(SIC_Exten.CodeType.MD5).ToLower();
            cipherText = DESEncryption.Encrypt(cipherText,appkey);
            signature= (cipherText + sec).EnCode(SIC_Exten.CodeType.MD5).ToLower();
           
        }

        #endregion
    }

    /// <summary>
    /// 加密字符串
    /// </summary>
    public class DESEncryption
    {
        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <param name="sKey"></param>
        /// <returns>加密后的BASE64编码的字符串</returns>
        public static string Encrypt(string sourceString, string sKey)
        {
            if (sourceString == "") return sourceString;

            byte[] btKey = Encoding.UTF8.GetBytes(sKey);
            byte[] btIV = Encoding.UTF8.GetBytes(sKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    return sourceString;
                }
            }
        }


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位</param>
        /// <returns>已解密的字符串</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {

            if (pToDecrypt == "") return pToDecrypt;

            //转义特殊字符
            pToDecrypt = pToDecrypt.Replace(" ", "+");
            pToDecrypt = pToDecrypt.Replace("_", "/");
            pToDecrypt = pToDecrypt.Replace("~", "=");
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

    }
}
