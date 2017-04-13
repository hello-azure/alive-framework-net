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

namespace Alive.Foundation.Utilities.Events
{
    /// <summary>
    /// 全局事件项定义
    /// </summary>
    public class EventItem<TArgs> where TArgs : EventArgs
    {
        /// <summary>
        /// 定义的全局事件
        /// </summary>
        public event EventHandler<TArgs> Event;

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="e"></param>
        public void Run(TArgs e)
        {
            var handler = this.Event;

            if (handler != null)
            {
                handler(this, e);

            }
        }
    }
}
