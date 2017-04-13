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

namespace Alive.Tools.CodeGenerator
{
    public partial class Form_TemplateManager : Form
    {

        public Form_TemplateManager()
        {
            InitializeComponent();

            var rootNode = new TreeNode(new FileInfo("模板管理").Name);
            treeView.ImageIndex = 0;
            rootNode.SelectedImageIndex = 0;
            rootNode.Tag = "Folder";
            //rootNode.Expand();

            //foreach (var item in e.Tables)
            //{
            //    var node = new TreeNode(item.Name.Value);
            //    node.Tag = item;
            //    node.NodeFont = new Font(treeView.Font, FontStyle.Bold);
            //    node.Expand();
            //    rootNode.Nodes.Add(node);

            //    var columns = new TreeNode("Columns");
            //    columns.Expand();
            //    node.Nodes.Add(columns);

            //    foreach (var column in item.Columns)
            //    {
            //        var columnNode = new TreeNode(column.Name.Value);
            //        //columnNode.Tag = new KeyValuePair<string, string>(item.Name.Value, column.Name.Value);
            //        columns.Nodes.Add(columnNode);
            //    }
            //}

            treeView.Nodes.Clear();
            treeView.Nodes.Add(rootNode);
            treeView.SelectedNode = rootNode;

           

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void DataSource_Created(object sender, DataSourceEventArgs e)
        {
            
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point clickPoint = new Point(e.X, e.Y);
                TreeNode currentNode = treeView.GetNodeAt(clickPoint);

                if (currentNode != null)
                {
                    switch (currentNode.Tag.ToString())
                    { 
                        case "Folder":
                            currentNode.ContextMenuStrip = this.contextMenuStrip_OnFolder;
                            break;
                        default:
                            currentNode.ContextMenuStrip = this.contextMenuStrip_OnTemplate;
                            break;
                    }
                    
                }

                treeView.SelectedNode = currentNode;
            }
        }

        private void 模板_Click(object sender, EventArgs e)
        {
            var parentNode = this.treeView.SelectedNode;
            var node = new TreeNode(new FileInfo("新建模板").Name);
            node.ImageIndex = 2;
            node.SelectedImageIndex = 2;
            node.Tag = "Template";           
            parentNode.Nodes.Add(node);
            parentNode.Expand();
            this.treeView.SelectedNode = node;
            this.treeView.SelectedNode.BeginEdit();
        }

        private void 文件夹_Click(object sender, EventArgs e)
        {
            var parentNode = this.treeView.SelectedNode;
            var node = new TreeNode(new FileInfo("新建文件夹").Name);
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;
            node.Tag = "Folder";
            parentNode.Nodes.Add(node);
            parentNode.Expand();
            this.treeView.SelectedNode = node;
            this.treeView.SelectedNode.BeginEdit();
        }
        
       
    }
}
