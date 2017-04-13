/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alive.Tools.CodeGenerator
{
    /// <summary>
    /// 输出信息事件
    /// </summary>
    public class OutputEventArgs:EventArgs
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// 输出消息
        /// </summary>
        public string Message
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">输出消息</param>
        public OutputEventArgs(string message)
        {
            this.Message = message;
        }

        #endregion
    }
}
