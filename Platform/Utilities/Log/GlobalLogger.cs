/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace Alive.Foundation.Utilities.Log
{
    public class GlobalLogger<T> : GlobalLogger
        where T : class
    {
        #region ==== 私有字段 ====

        #endregion

        #region ==== 构造函数 =====

        private GlobalLogger()
        {
        }

        #endregion

        public static MethodTrack MethodTrack(string methodName)
        {
            return new MethodTrack(typeof(T), methodName);
        }

        #region ==== 公有函数 =====

        /// <summary>
        /// Debug日志信息
        /// </summary>
        /// <param name="txt">字符串</param>
        public static void Debug(string txt)
        {
            GetLogger<T>().Debug(txt);
        }

        /// <summary>
        /// Debug日志信息异常重载
        /// </summary>
        /// <param name="txt">字符串</param>
        /// <param name="e">Exception</param>
        public static void Debug(string txt, Exception e)
        {
            GetLogger<T>().Debug(txt, e);
        }

        /// <summary>
        /// Debug日志信息重载
        /// </summary>
        /// <param name="stringformat">字符串格式</param>
        /// <param name="args">参数列表</param>
        public static void Debug(string stringformat, params object[] args)
        {
            GetLogger<T>().DebugFormat(stringformat, args);
        }

        /// <summary>
        /// 常用日志信息
        /// </summary>
        /// <param name="stringformat">字符串格式</param>
        /// <param name="args">参数列表</param>
        public static void Info(string stringformat, params object[] args)
        {
            GetLogger<T>().InfoFormat(stringformat, args);
        }

        /// <summary>
        /// 常用日志信息
        /// </summary>
        /// <param name="txt">字符串</param>
        public static void Info(string txt)
        {
            GetLogger<T>().Info(txt);
        }

        /// <summary>
        /// 警告日志信息
        /// </summary>
        /// <param name="stringformat">字符串</param>
        public static void Warn(string txt)
        {
            GetLogger<T>().Warn(txt);
        }

        /// <summary>
        /// 警告日志信息
        /// </summary>
        /// <param name="stringformat">字符串格式</param>
        /// <param name="args">参数列表</param>
        public static void Warn(string stringformat, params object[] args)
        {
            GetLogger<T>().WarnFormat(stringformat, args);
        }

        /// <summary>
        /// 错误日志信息
        /// </summary>
        /// <param name="txt">字符串</param>
        public static void Error(string txt)
        {
            GetLogger<T>().Error(txt);
        }

        /// <summary>
        /// 错误日志异常信息重载
        /// </summary>
        /// <param name="txt">字符串</param>
        /// <param name="e">Exception</param>
        public static void Error(string txt, Exception e)
        {
            GetLogger<T>().Error(txt, e);
        }

        /// <summary>
        /// 错误日志信息
        /// </summary>
        /// <param name="stringformat">字符串格式</param>
        /// <param name="args">参数列表</param>
        public static void Error(string stringformat, params object[] args)
        {
            GetLogger<T>().ErrorFormat(stringformat, args);
        }

        #endregion
    }

    public class GlobalLogger
    {
        private static readonly Dictionary<Type, ILog> loggerDIc = new Dictionary<Type, ILog>();

        protected static ILog GetLogger(Type type)
        {
            ILog log = null;

            if (loggerDIc.ContainsKey(type))
            {
                log = loggerDIc[type];
            }
            else
            {
                string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
                string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
                string configFilePath = Path.Combine(assemblyDirPath, "Alive.Foundation.Utilities.dll.config");
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));
                log = LogManager.GetLogger(type);
                loggerDIc.Add(type, log);
            }

            return log;
        }

        protected static ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }
    }
}
