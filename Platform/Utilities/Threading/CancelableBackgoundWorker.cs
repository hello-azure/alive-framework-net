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
using System.ComponentModel;

namespace Alive.Foundation.Utilities.Threading
{
    /// <summary>
    /// 后台线程
    /// </summary>
    public class CancelableBackgoundWorker
    {

        #region ==== 私有字段 ====

        /// <summary>
        /// 在单独的线程上执行操作的类
        /// </summary>
        private BackgroundWorker activeWorker;

        #endregion

        #region ==== 事件定义 ====

        /// <summary>
        /// 调用RunWorkerAsync时发生的事件
        /// </summary>
        public event DoWorkEventHandler DoWork;

        /// <summary>
        /// 调用ReportProgress时发生的事件
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// 当后台操作已完成,被取消或引发异常时发生的事件
        /// </summary>
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;

        #endregion

        #region ==== 公有方法 ====

        public void RunWorkerAsync()
        {
            if (activeWorker != null)
            {
                activeWorker.RunWorkerCompleted -= activeWorker_RunWorkerCompleted;
                activeWorker.CancelAsync();
                activeWorker.Dispose();
            }

            activeWorker = new BackgroundWorker();

            activeWorker.WorkerReportsProgress = true;
            activeWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(activeWorker_DoWork);
            activeWorker.ProgressChanged +=new System.ComponentModel.ProgressChangedEventHandler(activeWorker_ProgressChanged);
            activeWorker.RunWorkerCompleted+=new System.ComponentModel.RunWorkerCompletedEventHandler(activeWorker_RunWorkerCompleted);

            activeWorker.RunWorkerAsync();
        }

        public void ReportProgress(int percentProcess, object userState)
        {
            if (activeWorker != null)
            {
                activeWorker.ReportProgress(percentProcess, userState);
            }
        }

        #endregion

        #region ==== 事件句柄 ====

        /// <summary>
        /// 调用RunWorkerAsync时触发的逻辑
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件发生的参数</param>
        public void activeWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var handle = this.DoWork;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        /// <summary>
        /// 调用ReportProgress时触发的逻辑
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件发生的参数</param>
        public void activeWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var handle = this.ProgressChanged;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        /// <summary>
        /// 当后台操作已完成,被取消或引发异常时触发的逻辑
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件发生的参数</param>
        public void activeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (sender != this.activeWorker)
            {
                return;
            }

            var handle = this.RunWorkerCompleted;

            if (handle != null)
            {
                handle(this, e);
            }
        }

        #endregion
    }
}
