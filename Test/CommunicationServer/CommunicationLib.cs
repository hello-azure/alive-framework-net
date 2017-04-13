/***********
 * ��Ȩ˵����
 *   ���ļ��� ����������ƽ̨ �����һ���֡�
 *   �汾��V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 ����һ��Ȩ��
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
    /// �˵���ͨѶ
    /// </summary>
    internal class CommunicationLib : IDisposable
    {
        #region ==== ˽���ֶ� ====

        /// <summary>
        /// ��ϢͨѶ����
        /// </summary>
        private CommunicationServiceSoapClient service = new CommunicationServiceSoapClient();

        /// <summary>
        /// �ܵ�����
        /// </summary>
        private readonly string pipeName = Guid.NewGuid().ToString("N");

        /// <summary>
        /// �ܵ�����
        /// </summary>
        private string groupName = string.Empty;

        /// <summary>
        /// ��ʱ��
        /// </summary>
        private Timer timer = null;

        /// <summary>
        /// ��ǰͨѶ�Ƿ����
        /// </summary>
        private bool isActive = false;

        #endregion

        #region ==== �¼����� ====

        /// <summary>
        /// �յ���Ϣ�����Ĳ���
        /// </summary>
        public event EventHandler<MessageListEventArgs> MessagesRecieved;

        #endregion

        #region ==== ���캯�� ====

        /// <summary>
        /// ����һ��������ͨѶͨ��
        /// </summary>
        public CommunicationLib()
        {
            this.OpenConnection();
        }

        /// <summary>
        /// ����ָ�������ͨѶͨ��
        /// </summary>
        /// <param name="groupName">��������</param>
        public CommunicationLib(string groupName)
        {
            this.groupName = groupName;

            this.OpenConnection();
        }

        #endregion

        #region ==== �ӿ�ʵ�� ====

        #region IDisposable ��Ա

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

        #region ==== ���з��� ====

        /// <summary>
        /// ������Ϣ��ָ����ͨ��
        /// </summary>
        /// <param name="message">Ҫ���͵���Ϣ</param>
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
        /// ������Ϣ��ָ����ͨ������
        /// </summary>
        /// <param name="groupName">��������</param>
        /// <param name="message">Ҫ���͵���Ϣ</param>
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

        #region ==== �ܱ������� ====

        /// <summary>
        /// ����MessagesRecieved�¼�
        /// </summary>
        /// <param name="args">�����¼��Ĳ���</param>
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

        #region ==== ˽�з��� ====

        /// <summary>
        /// ������ʱ��
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
        /// ���Դ�����
        /// </summary>
        /// <returns>����һ��ֵ��ʾ�����Ƿ���</returns>
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

        #region ==== �¼���� ====

        /// <summary>
        /// ��ʱ�������¼�
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param>
        /// <param name="e">�¼�����</param>
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
