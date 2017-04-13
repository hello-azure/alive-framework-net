using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alive.Foundation.Data.Communication;

namespace Alive.Test.CommunicationServer
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 端对端通讯通道
        /// </summary>
        private readonly CommunicationLib message = new CommunicationLib("Server"); 

        public Form1()
        {
            InitializeComponent();

            this.message.MessagesRecieved += new EventHandler<MessageListEventArgs>(message_MessagesRecieved);
        }

        /// <summary>
        /// 收到端对端消息的逻辑
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件发生的参数</param>
        void message_MessagesRecieved(object sender, MessageListEventArgs e)
        {
            //// 字典同步请求
            //var filterRequests = from message in e.Messages
            //                     where message is DicFilterReuqestMessage
            //                     select message as DicFilterReuqestMessage;

            //ResponseDicFilterRequest(filterRequests);

            //// 黑白名单同步请求
            //var bwListReuqests = from message in e.Messages
            //                     where message is BlackWhiteListRequestMessage
            //                     select message as BlackWhiteListRequestMessage;

            //ResponseBlackWhiteListRequest(bwListReuqests);
        }

        //#region 通信处理

        ///// <summary>
        ///// 相应字典过滤信息同步的请求
        ///// </summary>
        ///// <param name="filterRequests">所有请求</param>
        //private void ResponseDicFilterRequest(IEnumerable<DicFilterReuqestMessage> filterRequests)
        //{
        //    BackgroundWorker worker = new BackgroundWorker();

        //    worker.DoWork += new DoWorkEventHandler((sender, e) =>
        //    {
        //        if (filterRequests.Count() > 0)
        //        {
        //            var allWords = Logic.GetAllDictFilterWords();
        //            DictFilterMessage response = new DictFilterMessage();
        //            response.Words = allWords;

        //            foreach (var request in filterRequests)
        //            {
        //                this.message.SendToPipe(request.Sender, response);
        //            }
        //        }
        //    });

        //    worker.RunWorkerAsync();
        //}

        ///// <summary>
        ///// 相应黑白名单过滤信息同步请求
        ///// </summary>
        ///// <param name="filterRequests">所有请求</param>
        //private void ResponseBlackWhiteListRequest(IEnumerable<BlackWhiteListRequestMessage> requests)
        //{
        //    lock (ClientRules.BlackWhiteList)
        //    {
        //        foreach (var condition in ClientRules.BlackWhiteList)
        //        {
        //            BlackWhiteListMessage response = new BlackWhiteListMessage();
        //            response.AddOrRemove = true;

        //            response.SetCondition(condition);

        //            BackgroundWorker worker = new BackgroundWorker();

        //            worker.DoWork += new DoWorkEventHandler((workerSender, workerE) =>
        //            {
        //                foreach (var request in requests)
        //                {
        //                    this.message.SendToPipe(request.Sender, response);
        //                }
        //            });

        //            worker.RunWorkerAsync();
        //        }
        //    }
        //}

        //#endregion

        ///// <summary>
        ///// 画面载入时的逻辑
        ///// </summary>
        ///// <param name="sender">触发事件的对象</param>
        ///// <param name="e">事件发生的参数</param>
        //private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    this.message.Dispose();
        //}
    }
}
