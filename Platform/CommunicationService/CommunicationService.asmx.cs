/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web.Services;
using Alive.Foundation.Data;
using Alive.Foundation.Data.Communication;

namespace LionTH.Myconos
{
    /// <summary>
    /// CommunicationService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.lionth.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class CommunicationService : System.Web.Services.WebService
    {
        private static readonly object fileLocker = new object();

        #region ==== 服务方法 ====

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="pipeName">管道名称</param>
        /// <param name="message">消息内容</param>
        /// <returns>返回一个值，用来判断是否发送成功</returns>
        [WebMethod(Description="发送消息,将消息添加至队列")]
        public bool SendMessage(string pipeName, string message)
        {
            var pipe = PipeManager.Pipe.Get(pipeName);

            if (pipe != null)
            {
                var result = MessageBase.FromXml(message);

                if (result != null)
                {
                    pipe.Enqueue(result);

                    this.Log(string.Format("发送消息到 [{1}]：{0} \r\n {2}", pipeName, true, message));

                    return true;
                }
            }

            this.Log(string.Format("发送消息到 [{1}]：{0} \r\n {2}", pipeName, false, message));

            return false;
        }

        /// <summary>
        /// 发送广播消息到指定的分组
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <param name="message">消息内容</param>
        /// <returns>返回一个值，表示发送成功的数量</returns>
        [WebMethod(Description = "发送广播消息到指定的分组")]
        public int SendMessageToGroup(string groupName, string message)
        {
            var pipes = PipeManager.Pipe.GetPipeGroup(groupName);
            int result = 0;

            foreach (var pipe in pipes)
            {
                if (this.SendMessage(pipe, message))
                {
                    result++;
                }
            }

            this.Log(string.Format("发送消息到：{0} [{1}条] \r\n {2}", groupName, result, message));

            return result;
        }

        /// <summary>
        /// 捕获消息
        /// </summary>
        /// <param name="pipeName">管道名称</param>
        /// <returns>返回消息列表</returns>
        [WebMethod(Description = "接收消息,将消息从队列取出")]
        public List<string> GetMessage(string pipeName)
        {

            List<string> result = new List<string>();
            var queue = PipeManager.Pipe.Get(pipeName);
            if (queue != null)
            {
                while (queue.Count > 0)
                {
                    var message = queue.Dequeue();
                    result.Add(MessageBase.ToXml(message));
                }
            }
            return result;
        }

        /// <summary>
        /// 注册管道
        /// </summary>
        /// <param name="pipeName">管道名称</param>
        [WebMethod(Description = "注册消息通道")]
        public void Register(string pipeName) 
        {
            PipeManager.Pipe.Create(pipeName);

            this.Log(string.Format("注册：{0}", pipeName));
        }

        /// <summary>
        /// 注册管道
        /// </summary>
        /// <param name="pipeName">管道名称</param>
        /// <param name="groupName">分组名称</param>
        [WebMethod(Description = "注册消息通道")]
        public void RegisterWithGroup(string pipeName, string groupName)
        {
            PipeManager.Pipe.Create(pipeName, groupName);

            this.Log(string.Format("注册：【{1}】{0}", pipeName, groupName));
        }

        /// <summary>
        /// 注销管道
        /// </summary>
        /// <param name="pipeName">管道名称</param>
        [WebMethod(Description = "注销消息通道")]
        public void SignOff(string pipeName)
        {
            PipeManager.Pipe.Remove(pipeName);

            this.Log(string.Format("注销：{0}", pipeName));
        }

        #endregion

        #region ==== 私有方法 ====

        private void Log(string text)
        {
            lock (fileLocker)
            {
                string fileName = Path.Combine(Server.MapPath("~/"), "log.txt");
                using (FileStream log = new FileStream(fileName, FileMode.Append))
                {
                    StreamWriter writer = new StreamWriter(log);

                    writer.WriteLine(string.Format(
                        "[{0} {1}] {2}",
                        DateTime.Now.ToShortDateString(),
                        DateTime.Now.ToShortTimeString(),
                        text));

                    writer.Flush();

                }

                FileInfo info = new FileInfo(fileName);

                if (info.Length > 1024 * 1024)
                {
                    string newFileName = Path.GetDirectoryName(fileName);
                    newFileName = Path.Combine(newFileName, "Log_backup");
                    newFileName = Path.Combine(newFileName, Guid.NewGuid().ToString("N"));

                    File.Copy(fileName, newFileName);
                }
            }
        }

        #endregion
    }
}
