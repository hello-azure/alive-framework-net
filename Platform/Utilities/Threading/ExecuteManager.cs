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
using System.Threading;

namespace Alive.Foundation.Utilities.Threading.Pool
{
    /// <summary>
    /// 示例线程池操作类,该类描述了一个使用ThreadPool构建线程同步的方式
    /// 实际使用过程中需对该实例代码进行扩展或定制
    /// </summary>
    public class FileBuilderManager
    {
        #region ==== 私有字段 ==== 
 
        /// <summary>
        /// 打印作业线程池操作类的实例
        /// </summary>
        private static readonly FileBuilderManager instance = new FileBuilderManager();
 
        /// <summary>
        /// 当前任务是否繁忙
        /// </summary>
        private bool isBusy = false;

        /// <summary>
        /// 状态锁对象
        /// </summary>
        private object stateLock = new object();

        /// <summary>
        /// 通知等待线程的事件信号量
        /// </summary>
        private AutoResetEvent wait = new AutoResetEvent(false);

        /// <summary>
        /// 最后一组数据的等待信号
        /// </summary>
        AutoResetEvent waitingtGrouptLastEvent = null;

        #endregion

        #region ==== 公有字段 ===

        /// <summary>
        /// 文件读写事件
        /// </summary>
        public static event EventHandler FileReady;

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        private FileBuilderManager()
        {
            ThreadPool.SetMaxThreads(50, 50);


            int workMaxThreadCount;
            int workMaxIOCount;
            ThreadPool.GetMaxThreads(out workMaxThreadCount, out workMaxIOCount); 

            int aviableThreadCount;
            int aviableIOCount;
            ThreadPool.GetAvailableThreads(out aviableThreadCount, out aviableIOCount);

            ThreadPool.RegisterWaitForSingleObject(
                this.wait,
                (state, isTimeOut) =>
                {
                    if (!isTimeOut)
                    {
                        this.DoWork(10);
                    }
                },
                null,
                Timeout.Infinite,
                false);
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 作业开始
        /// </summary>
        /// <param name="data">打印作业数据</param>
        public static void Start()
        {
            if (!instance.isBusy)
            {
                lock (instance.stateLock)
                {
                    if (!instance.isBusy)
                    {
                        instance.wait.Set();
                    }
                }
            }

        }

        /// <summary>
        /// 设置线程池最大线程数
        /// </summary>
        /// <param name="threadCount">最大线程数</param>
        public static void SetThreadMaxCount(int threadCount)
        {
            bool result = ThreadPool.SetMaxThreads(threadCount, threadCount);
            int workMaxThreadCount;
            int workMaxIOCount;
            ThreadPool.GetMaxThreads(out workMaxThreadCount, out workMaxIOCount); 

            int aviableThreadCount;
            int aviableIOCount;
            ThreadPool.GetAvailableThreads(out aviableThreadCount, out aviableIOCount);
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 线程池工作
        /// </summary>
        private void DoWork(int taskCount)
        {
            AutoResetEvent waitingtEvent = null;

            for (int i = 0; i < taskCount; i++)
            {
                WaitMessageInfo message = new WaitMessageInfo();
                message.Index = i;
                message.WaitingEvent = waitingtEvent;
                message.WaitingtGrouptLastEvent = waitingtGrouptLastEvent;
                message.MyEvent = new AutoResetEvent(false);

                int workMaxThreadCount;
                int workMaxIOCount;
                ThreadPool.GetMaxThreads(out workMaxThreadCount, out workMaxIOCount);

                int aviableThreadCount;
                int aviableIOCount;
                ThreadPool.GetAvailableThreads(out aviableThreadCount, out aviableIOCount);

                ThreadPool.QueueUserWorkItem(userState =>
                {

                    WaitMessageInfo myInfo = (WaitMessageInfo)userState;

                    //此处:设置线程执行工作

                    if (myInfo.Index == 0 && myInfo.WaitingtGrouptLastEvent != null)
                    {
                        myInfo.WaitingtGrouptLastEvent.WaitOne();
                    }

                    if (myInfo.WaitingEvent != null)
                    {
                        myInfo.WaitingEvent.WaitOne();  //等待前一线程工作结束
                    }

                    //此处:设置线程同步后，即前置线程完成任务后，当前线程的动作
                    //this.OnExecuteReady();

                    myInfo.MyEvent.Set();
                },
                message);

                waitingtEvent = message.MyEvent;

                //一个作业组的最后一个信号量
                if (i == taskCount - 1)
                {
                    //等待的一个作业组的最后一个完成的信号量
                    waitingtGrouptLastEvent = message.MyEvent;
                }


            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        private void OnExecuteReady()
        { 
            var handler = FileBuilderManager.FileReady; 

            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }


        #endregion
    }
}
