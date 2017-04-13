/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Alive.Tools.CodeGenerator.Properties;
using ICSharpCode.TextEditor.Document;
using Microsoft.SqlServer.MessageBox;
using Alive.Tools.CodeGenerator.Foundatation.Metadata;

namespace Alive.Tools.CodeGenerator
{
    public partial class Form_Main : Form
    {

        public Form_Main(string[] args)
        {
            InitializeComponent();
            GlobalMessage.Notifier.DataSourceCreated += DataSource_Created;

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SelectDatabase form = new Form_SelectDatabase();
            form.ShowDialog();
        }

        private void DataSource_Created(object sender, EventArgs e)
        {
            var datas=GlobalData.DataSource as TableInfoList;

            if (datas != null && datas.Count>0)
            {
                var rootNode = new TreeNode(new FileInfo("lianjie").Name);
                rootNode.Expand();

                foreach (var item in datas)
                {
                    var node = new TreeNode(item.Name.Value);
                    node.Tag = item;
                    node.NodeFont = new Font(treeView.Font, FontStyle.Bold);
                    node.Expand();
                    rootNode.Nodes.Add(node);

                    var columns = new TreeNode("Columns");
                    columns.Expand();
                    node.Nodes.Add(columns);

                    foreach (var column in item.Columns)
                    {
                        var columnNode = new TreeNode(column.Name.Value);
                        //columnNode.Tag = new KeyValuePair<string, string>(item.Name.Value, column.Name.Value);
                        columns.Nodes.Add(columnNode);
                    }
                }

                treeView.Nodes.Clear();
                treeView.Nodes.Add(rootNode);
                treeView.SelectedNode = rootNode;
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_TemplateManager form = new Form_TemplateManager();
            form.ShowDialog();
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_TemplateSettings form = new Form_TemplateSettings();
            form.ShowDialog();
        }

        private void 批量生成buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_BatchGenerator form = new Form_BatchGenerator();
            form.ShowDialog();
        }

        private void alive标准数据层框架ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_AliveStandardDal form = new Form_AliveStandardDal();
            form.ShowDialog();
        }

        private void 设置toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_GlobalSettings form = new Form_GlobalSettings();
            form.ShowDialog();
        }

        private void 关于我们ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_AboutBox form = new Form_AboutBox();
            form.ShowDialog();
        }

        

        

        
        
       
    }
}
