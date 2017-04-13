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

namespace Alive.Tools.CodeGenerator.Foundatation
{
    /// <summary>
    /// 数据库类型设置错误
    /// </summary>
    public class DataTypeFormatException:Exception
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataTypeFormatException()
            : base("数据库类型转换错误!")
        { }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public DataTypeFormatException(string message)
            : base(message)
        {

        }

        #endregion
    }
}
