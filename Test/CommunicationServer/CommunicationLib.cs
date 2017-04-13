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
using System.Windows.Forms;
using Alive.Foundation.Data.Communication;
using System.ComponentModel;
using Alive.Test.CommunicationServer.Communication;

namespace Alive.Test.CommunicationServer
{
    /// <summary>
    /// 端到端通讯
    /// </summary>
    internal class CommunicationLib : IDisposable
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 消息通讯服务
        /// </summary>
        private CommunicationServiceSoapClient service = new CommunicationServiceSoapClient();

        /// <summary>
        /// 管道名称
        /// </summary>
        private readonly string pipeName = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 管道分组
        /// </summary>
        private string groupName = string.Empty;

        /// <summary>
        /// 计时器
        /// </summary>
        private Timer timer = null;

        /// <summary>
        /// 当前通讯是否可用
        /// </summary>
        private bool isActive = false;

        #endregion

        #region ==== 事件声明 ====

        /// <summary>
        /// 收到消息触发的参数
        /// </summary>
        public event EventHandler<MessageListEventArgs> MessagesRecieved;

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个独立的通讯通道
        /// </summary>
        public CommunicationLib()
        {
            this.OpenConnection();
        }

        /// <summary>
        /// 创建指定分组的通讯通道
        /// </summary>
        /// <param name="groupName">分组名称</param>
        public CommunicationLib(string groupName)
        {
            this.groupName = groupName;

            this.OpenConnection();
        }

        #endregion

        #region ==== 接口实现 ====

        #region IDisposable 成员

        public void Dispose()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
            }

#if !SERVERONLY
            this.service.SignOff(this.pipeName);
#endif
        }

        #endregion

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 发送消息到指定的通道
        /// </summary>
        /// <param name="message">要发送的消息</param>
        public void SendToPipe(string pipeName, MessageBase message)
        {
            message.Sender = this.pipeName;

            if (OpenConnection())
            {
                try
                {
                    this.service.SendMessage(pipeName, MessageBase.ToXml(message));
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 发送消息到指定的通道分组
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <param name="message">要发送的消息</param>
        public void SendToGroup(string groupName, MessageBase message)
        {
            message.Sender = this.pipeName;

            if (OpenConnection())
            {
                try
                {
                    this.service.SendMessageToGroup(groupName, MessageBase.ToXml(message));
                }
                catch
                { }
            }
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 触发MessagesRecieved事件
        /// </summary>
        /// <param name="args">触发事件的参数</param>
        protected virtual void OnRecieved(IEnumerable<MessageBase> args)
        {
            var result = from item in args
                         where item != null
                         select item;

            if (result.Count() == 0)
            {
                return;
            }

            var handler = this.MessagesRecieved;

            if (handler != null)
            {
                handler(this, new MessageListEventArgs(args));
            }
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 创建计时器
        /// </summary>
        private void CreateTimer()
        {
            this.timer = new Timer();
            this.timer.Stop();

            this.timer.Interval = 1000;
            this.timer.Tick += new EventHandler(timer_Tick);

            this.timer.Start();
        }

        /// <summary>
        /// 尝试打开连接
        /// </summary>
        /// <returns>返回一个值表示连接是否建立</returns>
        private bool OpenConnection()
        {

#if SERVERONLY
            return true;
#endif
            if (this.isActive)
            {
                return true;
            }

            lock (this)
            {
                if (this.isActive)
                {
                    return true;
                }

                try
                {
                    if (string.IsNullOrEmpty(this.groupName))
                    {
                        service.Register(this.pipeName);
                    }
                    else
                    {
                        service.RegisterWithGroup(this.pipeName, groupName);
                    }


                    this.isActive = true;


                    this.CreateTimer();
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region ==== 事件句柄 ====

        /// <summary>
        /// 定时触发的事件
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件参数</param>
        void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();

            BackgroundWorker worker = new BackgroundWorker();
            IEnumerable<MessageBase> messageList = null;

            worker.DoWork += new DoWorkEventHandler((workerSender, workerE) =>
                {
                    var messages = this.service.GetMessage(this.pipeName);

                    messageList = from message in messages
                                      select MessageBase.FromXml(message);
                });

            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((workerSender, workerE) =>
                {
                    this.OnRecieved(messageList);
                });

            worker.RunWorkerAsync();

            this.timer.Start();
        }

        #endregion
    }
}
