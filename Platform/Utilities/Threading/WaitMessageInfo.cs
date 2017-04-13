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
using System.Threading;
using System.Text;

namespace Alive.Foundation.Utilities.Threading.Pool
{
    /// <summary>
    /// 等待消息信息
    /// </summary>
    public struct WaitMessageInfo
    {
        /// <summary>
        /// 线程索引号
        /// </summary>
        public int Index;

        /// <summary>
        /// 等待的线程事件信号量
        /// </summary>
        public AutoResetEvent WaitingEvent;

        /// <summary>
        /// 等待的一个作业组的最后一个完成的信号量
        /// </summary>
        public AutoResetEvent WaitingtGrouptLastEvent;

        /// <summary>
        /// 自己的线程事件信号量
        /// </summary>
        public AutoResetEvent MyEvent;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index">线程索引号</param>
        /// <param name="waitingEvent">等待的线程事件信号量</param>
        /// <param name="waitingtGrouptLastEvent">等待的一个作业组的最后一个完成的信号量</param>
        /// <param name="myEvent">自己的线程事件信号量</param>
        /// <param name="Info">待扩展参数</param>
        public WaitMessageInfo(int index, AutoResetEvent waitingEvent, AutoResetEvent waitingtGrouptLastEvent, AutoResetEvent myEvent)
        {
            this.Index = index;
            this.WaitingEvent = waitingEvent;
            this.WaitingtGrouptLastEvent = waitingtGrouptLastEvent;
            this.MyEvent = myEvent;
        }
    }
}
