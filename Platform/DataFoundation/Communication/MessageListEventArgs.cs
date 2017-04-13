/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alive.Foundation.Data.Communication
{
    /// <summary>
    /// 带有消息列表的事件的参数
    /// </summary>
    public class MessageListEventArgs : EventArgs
    {
        #region ==== 属性 ====

        /// <summary>
        /// 事件关联的消息
        /// </summary>
        public List<MessageBase> Messages
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的实例
        /// </summary>
        /// <param name="messages">关联的消息集合</param>
        public MessageListEventArgs(IEnumerable<MessageBase> messages)
        {
            this.Messages = new List<MessageBase>(messages);
        }

        #endregion
    }
}
