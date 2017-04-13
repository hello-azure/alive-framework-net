

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Alive.Foundation.Utilities.Communication;
using System.IO;

namespace Alive.Test.TcpClient
{
    public partial class Form1 : Form
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 组合消息
        /// </summary>
        List<byte> composite = new List<byte>();

        /// <summary>
        /// 是否组合消息
        /// </summary>
        bool isComposite = false;

        /// <summary>
        /// filePathOrder
        /// </summary>
        private int filePathOrder = 1;

        /// <summary>
        /// 定时器
        /// </summary>
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// TcpClient
        /// </summary>
        System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient();

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
        /// 定时接收数据
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件参数</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {

                #region 
                TcpBase cb = new TcpBase();
                string data = cb.Receive(tcpClient, int.Parse(textBox5.Text));

                if (data == null)
                {
                    this.timer.Enabled = false;
                    return;
                }
                //byte[] bytes = cb.RecieveStreame(tcpClient, int.Parse(textBox5.Text));
                //this.listBox1.Items.Add(bytes);

             
                #endregion
            }
            catch
            { 
            
            }
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="data"></param>
        void WriteFile(byte[] data)
        {
            string folderPath = string.Format("{0}", this.textBox4.Text);
            string filePath = string.Format("{0}{1}.txt", this.textBox4.Text, filePathOrder);

            if (!Directory.Exists(@folderPath))
            {
                Directory.CreateDirectory(@folderPath);
            }

            FileStream fs;

            if (!File.Exists(@filePath))
            {
                fs = File.Create(@filePath);
            }
            else
            {
                fs = File.Open(@filePath, FileMode.Open);
            }

            fs.Write(data, 0, data.Length);
            fs.Close();

            //StreamWriter sw = new StreamWriter(fs);
            //sw.WriteLine(data);
            //sw.Close();
            filePathOrder++;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        void ConnectServer()
        {
            string hostIP = this.textBox1.Text;
            IPAddress ipa = IPAddress.Parse(hostIP);
            IPEndPoint ipe = new IPEndPoint(ipa, int.Parse(this.textBox2.Text));


            try
            {
                this.listBox1.Items.Add("主机IP" + ipa.ToString());
                this.listBox1.Items.Add("连接主机中...");
                tcpClient.Connect(ipe);

                if (tcpClient.Connected)
                {
                    this.listBox1.Items.Add("连接主机成功！");

                    this.timer.Enabled = true;



                }
                else
                {
                    this.listBox1.Items.Add("连接失败！");
                }
            }
            catch(Exception ex)
            {
                this.listBox1.Items.Add(ex.ToString());
            }
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件参数</param>
        private void button1_Click(object sender, EventArgs e)
        {
            ConnectServer();
        }

        /// <summary>
        /// 设置定时接收的间隔时间
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

        /// <summary>
        /// 开始接收数据
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">事件参数</param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.timer.Enabled = true;
        }

        #endregion
    }
}
