/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.Net.Sockets;
using System.Text;

namespace Alive.Foundation.Utilities.Communication
{
    /// <summary>
    /// 通信基础类
    /// </summary>
    public class TcpBase
    {
        #region ==== 公共方法 ====

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="tcpClient">TcpClient</param>
        /// <param name="message">消息</param>
        public void Send(TcpClient tcpClient, string message)
        {
            NetworkStream stream = tcpClient.GetStream();

            if (stream.CanWrite)
            {
                byte[] messageByte = Encoding.ASCII.GetBytes(message);
                stream.Write(messageByte, 0, messageByte.Length);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="tcpClient">TcpClient</param>
        /// <param name="bufferSize">缓存大小</param>
        /// <returns>消息</returns>
        public string Receive(TcpClient tcpClient, int bufferSize)
        {
            string result = string.Empty;
            byte[] receiveBytes = new byte[bufferSize];
            int numberofBytesRead = 0;
            NetworkStream stream = tcpClient.GetStream();

            if (stream.CanRead && stream.DataAvailable)
            {
                numberofBytesRead = stream.Read(receiveBytes, 0, bufferSize);
                result = Encoding.ASCII.GetString(receiveBytes, 0, numberofBytesRead);
                //do
                //{
                //numberofBytesRead = stream.Read(receiveBytes, 0, bufferSize);
                //result = Encoding.ASCII.GetString(receiveBytes, 0, numberofBytesRead);
                //}
                //while (stream.DataAvailable);
                return result;
            }

            return null;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="tcpClient">TcpClient</param>
        /// <param name="bufferSize">缓存大小</param>
        /// <returns>消息流</returns>
        public byte[] RecieveStreame(TcpClient tcpClient, int bufferSize)
        {
            byte[] receiveBytes = new byte[bufferSize];
            int numberofBytesRead = 0;
            NetworkStream stream = tcpClient.GetStream();

            if (stream.CanRead && stream.DataAvailable)
            {
                numberofBytesRead = stream.Read(receiveBytes, 0, bufferSize);
                return receiveBytes;
                //do
                //{

                //}
                //while (stream.DataAvailable);
            }

            return null;
        }

        #endregion
    }
}
