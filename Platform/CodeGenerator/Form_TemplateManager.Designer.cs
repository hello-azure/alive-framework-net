namespace Alive.Tools.CodeGenerator
{
    partial class Form_TemplateManager
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
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);
            //this.Dispose(true);
            //this.Close();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("代码模板");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_TemplateManager));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.rtbGeneratedCodeEntities = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabOutput = new System.Windows.Forms.TabControl();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.rtbOutput = new System.Windows.Forms.TextBox();
            this.contextMenuStrip_OnFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建 = new System.Windows.Forms.ToolStripMenuItem();
            this.模板 = new System.Windows.Forms.ToolStripMenuItem();
            this.文件夹 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除 = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名 = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_OnTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.实体模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.业务模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据访问模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.contextMenuStrip_OnFolder.SuspendLayout();
            this.contextMenuStrip_OnTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 497);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabOutput);
            this.splitContainer1.Size = new System.Drawing.Size(784, 497);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.rtbGeneratedCodeEntities);
            this.splitContainer.Size = new System.Drawing.Size(784, 384);
            this.splitContainer.SplitterDistance = 202;
            this.splitContainer.TabIndex = 3;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList1;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode2.Name = "nodeTables";
            treeNode2.Text = "代码模板";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(202, 384);
            this.treeView.TabIndex = 0;
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tm.ico");
            this.imageList1.Images.SetKeyName(1, "folder.ico");
            this.imageList1.Images.SetKeyName(2, "template.ico");
            // 
            // rtbGeneratedCodeEntities
            // 
            this.rtbGeneratedCodeEntities.AllowDrop = true;
            this.rtbGeneratedCodeEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbGeneratedCodeEntities.IsReadOnly = false;
            this.rtbGeneratedCodeEntities.Location = new System.Drawing.Point(0, 0);
            this.rtbGeneratedCodeEntities.Name = "rtbGeneratedCodeEntities";
            this.rtbGeneratedCodeEntities.ShowVRuler = false;
            this.rtbGeneratedCodeEntities.Size = new System.Drawing.Size(578, 384);
            this.rtbGeneratedCodeEntities.TabIndex = 3;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.tabPageOutput);
            this.tabOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOutput.HotTrack = true;
            this.tabOutput.Location = new System.Drawing.Point(0, 0);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.SelectedIndex = 0;
            this.tabOutput.Size = new System.Drawing.Size(784, 109);
            this.tabOutput.TabIndex = 0;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.rtbOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(776, 83);
            this.tabPageOutput.TabIndex = 0;
            this.tabPageOutput.Text = "输出信息";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // rtbOutput
            // 
            this.rtbOutput.AcceptsReturn = true;
            this.rtbOutput.AcceptsTab = true;
            this.rtbOutput.AllowDrop = true;
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(3, 3);
            this.rtbOutput.Multiline = true;
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rtbOutput.Size = new System.Drawing.Size(770, 77);
            this.rtbOutput.TabIndex = 0;
            // 
            // contextMenuStrip_OnFolder
            // 
            this.contextMenuStrip_OnFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建,
            this.打开,
            this.删除,
            this.重命名,
            this.保存ToolStripMenuItem1,
            this.另存为ToolStripMenuItem1});
            this.contextMenuStrip_OnFolder.Name = "contextMenuStrip1";
            this.contextMenuStrip_OnFolder.Size = new System.Drawing.Size(153, 158);
            // 
            // 新建
            // 
            this.新建.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.模板,
            this.文件夹});
            this.新建.Name = "新建";
            this.新建.Size = new System.Drawing.Size(152, 22);
            this.新建.Text = "新建";
            // 
            // 模板
            // 
            this.模板.Name = "模板";
            this.模板.Size = new System.Drawing.Size(112, 22);
            this.模板.Text = "模板";
            this.模板.Click += new System.EventHandler(this.模板_Click);
            // 
            // 文件夹
            // 
            this.文件夹.Name = "文件夹";
            this.文件夹.Size = new System.Drawing.Size(112, 22);
            this.文件夹.Text = "文件夹";
            this.文件夹.Click += new System.EventHandler(this.文件夹_Click);
            // 
            // 打开
            // 
            this.打开.Name = "打开";
            this.打开.Size = new System.Drawing.Size(152, 22);
            this.打开.Text = "打开";
            // 
            // 删除
            // 
            this.删除.Name = "删除";
            this.删除.Size = new System.Drawing.Size(152, 22);
            this.删除.Text = "删除";
            // 
            // 重命名
            // 
            this.重命名.Name = "重命名";
            this.重命名.Size = new System.Drawing.Size(152, 22);
            this.重命名.Text = "重命名";
            // 
            // 保存ToolStripMenuItem1
            // 
            this.保存ToolStripMenuItem1.Name = "保存ToolStripMenuItem1";
            this.保存ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.保存ToolStripMenuItem1.Text = "保存";
            // 
            // 另存为ToolStripMenuItem1
            // 
            this.另存为ToolStripMenuItem1.Name = "另存为ToolStripMenuItem1";
            this.另存为ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.另存为ToolStripMenuItem1.Text = "另存为";
            // 
            // contextMenuStrip_OnTemplate
            // 
            this.contextMenuStrip_OnTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.重命名toolStripMenuItem,
            this.设置为ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem});
            this.contextMenuStrip_OnTemplate.Name = "contextMenuStrip_OnTemplate";
            this.contextMenuStrip_OnTemplate.Size = new System.Drawing.Size(113, 136);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开ToolStripMenuItem.Text = "编辑";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // 设置为ToolStripMenuItem
            // 
            this.设置为ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.实体模板ToolStripMenuItem,
            this.业务模板ToolStripMenuItem,
            this.数据访问模板ToolStripMenuItem});
            this.设置为ToolStripMenuItem.Name = "设置为ToolStripMenuItem";
            this.设置为ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设置为ToolStripMenuItem.Text = "设置为";
            // 
            // 实体模板ToolStripMenuItem
            // 
            this.实体模板ToolStripMenuItem.Name = "实体模板ToolStripMenuItem";
            this.实体模板ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.实体模板ToolStripMenuItem.Text = "实体模板";
            // 
            // 业务模板ToolStripMenuItem
            // 
            this.业务模板ToolStripMenuItem.Name = "业务模板ToolStripMenuItem";
            this.业务模板ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.业务模板ToolStripMenuItem.Text = "业务模板";
            // 
            // 数据访问模板ToolStripMenuItem
            // 
            this.数据访问模板ToolStripMenuItem.Name = "数据访问模板ToolStripMenuItem";
            this.数据访问模板ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.数据访问模板ToolStripMenuItem.Text = "数据访问模板";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.另存为ToolStripMenuItem.Text = "另存为";
            // 
            // 重命名toolStripMenuItem
            // 
            this.重命名toolStripMenuItem.Name = "重命名toolStripMenuItem";
            this.重命名toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.重命名toolStripMenuItem.Text = "重命名";
            // 
            // Form_TemplateManager
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(784, 519);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_TemplateManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "模板管理     ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.contextMenuStrip_OnFolder.ResumeLayout(false);
            this.contextMenuStrip_OnTemplate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TabControl tabOutput;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TextBox rtbOutput;
        private ICSharpCode.TextEditor.TextEditorControl rtbGeneratedCodeEntities;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_OnFolder;
        private System.Windows.Forms.ToolStripMenuItem 新建;
        private System.Windows.Forms.ToolStripMenuItem 模板;
        private System.Windows.Forms.ToolStripMenuItem 文件夹;
        private System.Windows.Forms.ToolStripMenuItem 打开;
        private System.Windows.Forms.ToolStripMenuItem 删除;
        private System.Windows.Forms.ToolStripMenuItem 重命名;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_OnTemplate;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 实体模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 业务模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据访问模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名toolStripMenuItem;

    }
}