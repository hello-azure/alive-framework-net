/***********
 * 版权说明：
 *   本文件是 Capetown 服务程序的一部分。
 *   版本：V 1.0
 *   Copyright 中国电子科技集团第十五研究所 2012 保留一切权利
 *   
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Alive.Foundation.Utilities.Communication;

namespace Alive.Test.TcpServer
{
    public partial class Form1 : Form
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 计数器
        /// </summary>
        private int i = 1;

        /// <summary>
        /// 定时器
        /// </summary>
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// TcpClient
        /// </summary>
        private TcpClient tcpClient;

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.timer.Interval = int.Parse(this.textBox3.Text);
            this.timer.Tick += new EventHandler(timer_Tick);
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 定时发送
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件参数</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                TcpBase cb = new TcpBase();
                cb.Send(tcpClient, string.Format("data{0}0x0a", i));
                this.listBox1.Items.Add(i);
                i++;
               
            }
            catch
            {
                this.listBox1.Items.Add("客户端关闭连接");
                tcpClient.Close();
            }
        }

        /// <summary>
        /// 创建测试数据
        /// </summary>
        /// <returns>数据消息</returns>
        string CreateData()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 1024 * 1024; i++)
            {
                sb.Append("R,00214EF5,16.43,1.35,1.59,11,1313826463.353,1,S01\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 监听连接
        /// </summary>
        void ListenConnection()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipa = Dns.GetHostAddresses(hostName);

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(this.textBox1.Text), int.Parse(this.textBox2.Text));
            TcpListener listener = new TcpListener(endpoint);
            listener.Start();
            this.listBox1.Items.Add("等待客户端连接中...");

            bool Connected = true;

            while (Connected)
            {
                try
                {
                    tcpClient = listener.AcceptTcpClient();

                    if (tcpClient.Connected)
                    {
                        Connected = false;
                        this.listBox1.Items.Add("已成功连接");
                        //timer.Enabled = true;
                        TcpBase cb = new TcpBase();
                        cb.Send(tcpClient, CreateData());
                    }
                }
                catch (Exception ex)
                {
                    this.listBox1.Items.Add(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        void Communicate()
        {
            try
            {
                TcpBase cb = new TcpBase();
                string message = cb.Receive(tcpClient,1024);
                cb.Send(tcpClient, "data1");
            }
            catch
            {
                this.listBox1.Items.Add("客户端关闭连接");
                tcpClient.Close();
            }
        }

        /// <summary>
        /// 监听客户端连接
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件参数</param>
        private void button1_Click(object sender, EventArgs e)
        {
            ListenConnection();
        }

        /// <summary>
        /// 设置发送数据的时间间隔
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件参数</param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.timer.Interval = int.Parse(this.textBox3.Text);
            MessageBox.Show("设置成功！");
        }

        /// <summary>
        /// 停止接收数据
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件参数</param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
