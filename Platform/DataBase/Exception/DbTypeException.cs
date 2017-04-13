/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alive.Foundation.Storage
{
    /// <summary>
    /// 数据库类型设置错误
    /// </summary>
    public class DbTypeException:Exception
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        public DbTypeException()
            : base("数据库类型设置错误!目前系统支持SQLSERVER、ORACLE、ACCESS,请检查确认<appSettings>配置节是否存在DbType配置项!")
        { }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public DbTypeException(string message)
            : base(message)
        {

        }

        #endregion
    }
}
