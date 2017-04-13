using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alive.Foundation.Data.Communication;

namespace Alive.Test.CommunicationClient
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 端对端通讯通道
        /// </summary>
        private readonly CommunicationLib message = new CommunicationLib("Client");

        public Form1()
        {
            InitializeComponent();
            message.MessagesRecieved += new EventHandler<MessageListEventArgs>(message_MessagesRecieved);
        }

        /// <summary>
        /// 收到端对端消息的逻辑
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件发生的参数</param>
        void message_MessagesRecieved(object sender, MessageListEventArgs e)
        {
            //收到端对端消息的逻辑
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

        //#region 通信处理

        ///// <summary>
        ///// 处理字典过滤同步消息
        ///// </summary>
        ///// <param name="filterRequests"></param>
        //private static void ExecuteDictFilterMessage(IEnumerable<DictFilterMessage> filterRequests)
        //{
        //    BackgroundWorker worker = new BackgroundWorker();

        //    worker.DoWork += new DoWorkEventHandler((sender, e) =>
        //    {
        //        if (filterRequests.Count() > 0)
        //        {
        //            // 获得所有服务器词汇
        //            List<string> totalList = new List<string>();

        //            foreach (var message in filterRequests)
        //            {
        //                var m = message;

        //                if (m.Words != null)
        //                {
        //                    foreach (var word in m.Words)
        //                    {
        //                        if (!totalList.Contains(word))
        //                        {
        //                            totalList.Add(word);
        //                        }
        //                    }
        //                }
        //            }

        //            var originalList = Logic.GetAllDictFilterWords();
        //            var removeList = new List<string>();

        //            // 过滤已有词汇
        //            foreach (var word in originalList)
        //            {
        //                if (totalList.Contains(word))
        //                {
        //                    totalList.Remove(word);
        //                }
        //                else
        //                {
        //                    removeList.Add(word);
        //                }
        //            }

        //            // 删除过期词汇
        //            foreach (var word in removeList)
        //            {
        //                Logic.RemoveDictFilterWord(word);
        //            }

        //            // 添加新增词汇
        //            foreach (var word in totalList)
        //            {
        //                Logic.AddDictFilterWord(word);
        //            }
        //        }
        //    });

        //    worker.RunWorkerAsync();
        //}

        //#endregion

        ///// <summary>
        ///// 画面关闭时的逻辑
        ///// </summary>
        ///// <param name="sender">触发事件的对象</param>
        ///// <param name="e">事件发生的参数</param>
        //private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    this.message.Dispose();
        //}

        ///// <summary>
        ///// 工具栏【限制客户端】按钮按下的逻辑
        ///// </summary>
        ///// <param name="sender">触发事件的对象</param>
        ///// <param name="e">事件参数</param>
        //private void 限制客户端toolStripBtn_Click(object sender, EventArgs e)
        //{
        //    BWListForm form = new BWListForm();

        //    if (form.ShowDialog(this) == DialogResult.OK)
        //    {
        //        var condition = form.GetCondition();
        //        ClientRules.BlackWhiteList.Add(condition);

        //        BlackWhiteListMessage msg = new BlackWhiteListMessage();
        //        msg.SetCondition(condition);
        //        msg.AddOrRemove = true;

        //        this.message.SendToGroup("Client", msg);
        //    }
        //}
    }
}
