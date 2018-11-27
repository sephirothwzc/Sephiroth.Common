using System;
namespace Sephiroth.Infrastructure.Common.Log4Net
{
    /// <summary>
    /// 
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
			log4net.ILog log = log4net.LogManager.GetLogger(t);
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
			log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (messageType == MessageType.Error)
                log.Error(msg);
            else if (messageType == MessageType.Fatal)
                log.Fatal(msg);
            else if (messageType == MessageType.Debug)
                log.Debug(msg);
            else if (messageType == MessageType.Info)
                log.Info(msg);
            else if (messageType == MessageType.Warn)
                log.Warn(msg);
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
