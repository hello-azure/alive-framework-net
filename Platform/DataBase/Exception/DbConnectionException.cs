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
    public class DbConnectionException:Exception
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        public DbConnectionException()
            : base("数据库连接打开错误!请检查连接字符串是否设置正确!")
        { }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public DbConnectionException(string message)
            : base(message)
        {

        }

        #endregion
    }
}
