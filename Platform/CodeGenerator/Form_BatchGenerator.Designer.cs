﻿namespace Alive.Tools.CodeGenerator
{
    partial class Form_BatchGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("代码模板");
            this.groupBox_数据源信息 = new System.Windows.Forms.GroupBox();
            this.textBox_数据源 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_数据源类型 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox_tables = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_设置实体模板 = new System.Windows.Forms.Button();
            this.textBox_实体模板 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_保存设置 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.groupBox_数据源信息.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_数据源信息
            // 
            this.groupBox_数据源信息.Controls.Add(this.textBox_数据源);
            this.groupBox_数据源信息.Controls.Add(this.label2);
            this.groupBox_数据源信息.Controls.Add(this.textBox_数据源类型);
            this.groupBox_数据源信息.Controls.Add(this.label1);
            this.groupBox_数据源信息.Location = new System.Drawing.Point(12, 12);
            this.groupBox_数据源信息.Name = "groupBox_数据源信息";
            this.groupBox_数据源信息.Size = new System.Drawing.Size(760, 70);
            this.groupBox_数据源信息.TabIndex = 2;
            this.groupBox_数据源信息.TabStop = false;
            this.groupBox_数据源信息.Text = "数据源信息";
            // 
            // textBox_数据源
            // 
            this.textBox_数据源.Location = new System.Drawing.Point(323, 30);
            this.textBox_数据源.Name = "textBox_数据源";
            this.textBox_数据源.ReadOnly = true;
            this.textBox_数据源.Size = new System.Drawing.Size(423, 21);
            this.textBox_数据源.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据源：";
            // 
            // textBox_数据源类型
            // 
            this.textBox_数据源类型.Location = new System.Drawing.Point(104, 30);
            this.textBox_数据源类型.Name = "textBox_数据源类型";
            this.textBox_数据源类型.ReadOnly = true;
            this.textBox_数据源类型.Size = new System.Drawing.Size(120, 21);
            this.textBox_数据源类型.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据源类型：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.listBox_tables);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 242);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择表和视图";
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(346, 175);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "<<";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(346, 132);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "<";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(346, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(346, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = ">>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(462, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(284, 208);
            this.listBox1.TabIndex = 1;
            // 
            // listBox_tables
            // 
            this.listBox_tables.FormattingEnabled = true;
            this.listBox_tables.ItemHeight = 12;
            this.listBox_tables.Location = new System.Drawing.Point(23, 20);
            this.listBox_tables.Name = "listBox_tables";
            this.listBox_tables.Size = new System.Drawing.Size(284, 208);
            this.listBox_tables.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_设置实体模板);
            this.groupBox2.Controls.Add(this.textBox_实体模板);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 515);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 73);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "保存位置";
            // 
            // button_设置实体模板
            // 
            this.button_设置实体模板.Location = new System.Drawing.Point(668, 27);
            this.button_设置实体模板.Name = "button_设置实体模板";
            this.button_设置实体模板.Size = new System.Drawing.Size(75, 23);
            this.button_设置实体模板.TabIndex = 8;
            this.button_设置实体模板.Text = "设置";
            this.button_设置实体模板.UseVisualStyleBackColor = true;
            // 
            // textBox_实体模板
            // 
            this.textBox_实体模板.Location = new System.Drawing.Point(101, 29);
            this.textBox_实体模板.Name = "textBox_实体模板";
            this.textBox_实体模板.ReadOnly = true;
            this.textBox_实体模板.Size = new System.Drawing.Size(561, 21);
            this.textBox_实体模板.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "输出目录：";
            // 
            // button_保存设置
            // 
            this.button_保存设置.Location = new System.Drawing.Point(652, 617);
            this.button_保存设置.Name = "button_保存设置";
            this.button_保存设置.Size = new System.Drawing.Size(103, 35);
            this.button_保存设置.TabIndex = 7;
            this.button_保存设置.Text = "批量生成";
            this.button_保存设置.UseVisualStyleBackColor = true;
            this.button_保存设置.Click += new System.EventHandler(this.button_保存设置_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 617);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(634, 35);
            this.progressBar1.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.listBox2);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Location = new System.Drawing.Point(12, 336);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(760, 173);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选择模板";
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(346, 75);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = ">";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(459, 19);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(284, 136);
            this.listBox2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView);
            this.panel1.Location = new System.Drawing.Point(23, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 137);
            this.panel1.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode1.Name = "nodeTables";
            treeNode1.Text = "代码模板";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView.Size = new System.Drawing.Size(284, 137);
            this.treeView.TabIndex = 1;
            // 
            // Form_BatchGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(784, 665);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button_保存设置);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_数据源信息);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_BatchGenerator";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "代码批量生成";
            this.groupBox_数据源信息.ResumeLayout(false);
            this.groupBox_数据源信息.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_数据源信息;
        private System.Windows.Forms.TextBox textBox_数据源;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_数据源类型;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox_tables;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_设置实体模板;
        private System.Windows.Forms.TextBox textBox_实体模板;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_保存设置;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TreeView treeView;
    }
}