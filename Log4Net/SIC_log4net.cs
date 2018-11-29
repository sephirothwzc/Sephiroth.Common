using System;
using Common.Logging;

namespace Sephiroth.Infrastructure.Common.Log4Net
{
    /// <summary>
    /// 扩展log
    /// </summary>
    public class SIC_log4net
	{
		/// <summary>
		/// 输出日志到Log4Net
		/// </summary>
		/// <param name="t"></param>
		/// <param name="ex"></param>
		#region static void WriteLog(Type t, Exception ex)

		public static void WriteLog(Type t, Exception ex)
		{
			ILog log = LogManager.GetLogger(t);
			log.Error("Error", ex);
		}

		#endregion

		/// <summary>
		/// 输出日志到Log4Net
		/// </summary>
		/// <param name="t"></param>
		/// <param name="msg"></param>
        /// <param name="messageType"></param>
		#region static void WriteLog(Type t, string msg)

		public static void WriteLog(Type t, string msg, MessageType messageType = MessageType.Info)
		{
			ILog log = LogManager.GetLogger(t);
            switch (messageType)
            {
                case MessageType.Error:
                    log.Error(msg);
                    break;
                case MessageType.Fatal:
                    log.Fatal(msg);
                    break;
                case MessageType.Debug:
                    log.Debug(msg);
                    break;
                case MessageType.Info:
                    log.Info(msg);
                    break;
                case MessageType.Warn:
                    log.Warn(msg);
                    break;
            }
        }

		#endregion


	}

    /// <summary>
    /// 
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 调试
        /// </summary>
        Debug,
        /// <summary>
        /// 重大
        /// </summary>
        Fatal,
        /// <summary>
        /// 信息
        /// </summary>
        Info,
        /// <summary>
        /// 警告
        /// </summary>
        Warn
    }
}
