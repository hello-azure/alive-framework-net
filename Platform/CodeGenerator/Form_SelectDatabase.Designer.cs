namespace Alive.Tools.CodeGenerator
{
    partial class Form_SelectDatabase
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
            this.groupBox_数据源 = new System.Windows.Forms.GroupBox();
            this.radioButton_Access = new System.Windows.Forms.RadioButton();
            this.radioButton_Oracle = new System.Windows.Forms.RadioButton();
            this.radioButton_SQLServer = new System.Windows.Forms.RadioButton();
            this.button_Next = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox_数据源.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_数据源
            // 
            this.groupBox_数据源.Controls.Add(this.radioButton_Access);
            this.groupBox_数据源.Controls.Add(this.radioButton_Oracle);
            this.groupBox_数据源.Controls.Add(this.radioButton_SQLServer);
            this.groupBox_数据源.Font = new System.Drawing.Font("SimSun", 9F);
            this.groupBox_数据源.Location = new System.Drawing.Point(12, 12);
            this.groupBox_数据源.Name = "groupBox_数据源";
            this.groupBox_数据源.Size = new System.Drawing.Size(478, 206);
            this.groupBox_数据源.TabIndex = 0;
            this.groupBox_数据源.TabStop = false;
            this.groupBox_数据源.Text = "数据源";
            // 
            // radioButton_Access
            // 
            this.radioButton_Access.AutoSize = true;
            this.radioButton_Access.Location = new System.Drawing.Point(134, 147);
            this.radioButton_Access.Name = "radioButton_Access";
            this.radioButton_Access.Size = new System.Drawing.Size(191, 16);
            this.radioButton_Access.TabIndex = 2;
            this.radioButton_Access.Text = "Oledb(Access 2003及以上版本)";
            this.radioButton_Access.UseVisualStyleBackColor = true;
            // 
            // radioButton_Oracle
            // 
            this.radioButton_Oracle.AutoSize = true;
            this.radioButton_Oracle.Font = new System.Drawing.Font("SimSun", 9F);
            this.radioButton_Oracle.Location = new System.Drawing.Point(134, 97);
            this.radioButton_Oracle.Name = "radioButton_Oracle";
            this.radioButton_Oracle.Size = new System.Drawing.Size(155, 16);
            this.radioButton_Oracle.TabIndex = 1;
            this.radioButton_Oracle.Text = "Oracle 10g(及以上版本)";
            this.radioButton_Oracle.UseVisualStyleBackColor = true;
            // 
            // radioButton_SQLServer
            // 
            this.radioButton_SQLServer.AutoSize = true;
            this.radioButton_SQLServer.Checked = true;
            this.radioButton_SQLServer.Font = new System.Drawing.Font("SimSun", 9F);
            this.radioButton_SQLServer.Location = new System.Drawing.Point(134, 47);
            this.radioButton_SQLServer.Name = "radioButton_SQLServer";
            this.radioButton_SQLServer.Size = new System.Drawing.Size(197, 16);
            this.radioButton_SQLServer.TabIndex = 0;
            this.radioButton_SQLServer.TabStop = true;
            this.radioButton_SQLServer.Text = "SQL Server 2005（及以上版本）";
            this.radioButton_SQLServer.UseVisualStyleBackColor = true;
            // 
            // button_Next
            // 
            this.button_Next.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Next.Location = new System.Drawing.Point(274, 343);
            this.button_Next.Name = "button_Next";
            this.button_Next.Size = new System.Drawing.Size(93, 33);
            this.button_Next.TabIndex = 1;
            this.button_Next.Text = "连接";
            this.button_Next.UseVisualStyleBackColor = true;
            this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(397, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 33);
            this.button1.TabIndex = 2;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 235);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 65);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接字符串";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(463, 34);
            this.textBox1.TabIndex = 0;
            // 
            // Form_SelectDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 388);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_Next);
            this.Controls.Add(this.groupBox_数据源);
            this.MaximizeBox = false;
            this.Name = "Form_SelectDatabase";
            this.ShowIcon = false;
            this.Text = "选择数据库类型";
            this.groupBox_数据源.ResumeLayout(false);
            this.groupBox_数据源.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_数据源;
        private System.Windows.Forms.RadioButton radioButton_Access;
        private System.Windows.Forms.RadioButton radioButton_Oracle;
        private System.Windows.Forms.RadioButton radioButton_SQLServer;
        private System.Windows.Forms.Button button_Next;
        private System.Windows.Forms.Button button1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}