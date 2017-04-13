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
    /// 通知器
    /// </summary>
    public class GlobalMessage
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 通知器实例
        /// </summary>
        private static readonly GlobalMessage notifier = new GlobalMessage();

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        private GlobalMessage()
        { }

        #endregion

        #region ==== 公共属性 ====

        /// <summary>
        /// 通知器实例
        /// </summary>
        public static GlobalMessage Notifier
        {
            get
            {
                return notifier;
            }
        }

        #endregion

        #region ==== 公有事件 ====

        /// <summary>
        /// 数据源创建事件
        /// </summary>
        public EventHandler DataSourceCreated;

        /// <summary>
        /// 消息输出事件
        /// </summary>
        public EventHandler<OutputEventArgs> MessageOutputed;


        #endregion
    }
}
