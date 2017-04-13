namespace Alive.Tools.CodeGenerator
{
    partial class Form_GlobalSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_设置实体模板 = new System.Windows.Forms.Button();
            this.textBox_实体模板 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbGeneratedCodeEntities = new ICSharpCode.TextEditor.TextEditorControl();
            this.button_保存设置 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_设置实体模板);
            this.groupBox1.Controls.Add(this.textBox_实体模板);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "存储设置";
            // 
            // button_设置实体模板
            // 
            this.button_设置实体模板.Location = new System.Drawing.Point(668, 18);
            this.button_设置实体模板.Name = "button_设置实体模板";
            this.button_设置实体模板.Size = new System.Drawing.Size(75, 23);
            this.button_设置实体模板.TabIndex = 14;
            this.button_设置实体模板.Text = "设置";
            this.button_设置实体模板.UseVisualStyleBackColor = true;
            // 
            // textBox_实体模板
            // 
            this.textBox_实体模板.Location = new System.Drawing.Point(131, 20);
            this.textBox_实体模板.Name = "textBox_实体模板";
            this.textBox_实体模板.ReadOnly = true;
            this.textBox_实体模板.Size = new System.Drawing.Size(531, 21);
            this.textBox_实体模板.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "默认代码存储位置：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbGeneratedCodeEntities);
            this.groupBox2.Location = new System.Drawing.Point(12, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 172);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "头注释";
            // 
            // rtbGeneratedCodeEntities
            // 
            this.rtbGeneratedCodeEntities.AllowDrop = true;
            this.rtbGeneratedCodeEntities.IsReadOnly = false;
            this.rtbGeneratedCodeEntities.Location = new System.Drawing.Point(20, 20);
            this.rtbGeneratedCodeEntities.Name = "rtbGeneratedCodeEntities";
            this.rtbGeneratedCodeEntities.ShowVRuler = false;
            this.rtbGeneratedCodeEntities.Size = new System.Drawing.Size(723, 142);
            this.rtbGeneratedCodeEntities.TabIndex = 3;
            // 
            // button_保存设置
            // 
            this.button_保存设置.Location = new System.Drawing.Point(652, 279);
            this.button_保存设置.Name = "button_保存设置";
            this.button_保存设置.Size = new System.Drawing.Size(103, 35);
            this.button_保存设置.TabIndex = 7;
            this.button_保存设置.Text = "保存设置";
            this.button_保存设置.UseVisualStyleBackColor = true;
            // 
            // Form_GlobalSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(784, 341);
            this.Controls.Add(this.button_保存设置);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_GlobalSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "系统设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_设置实体模板;
        private System.Windows.Forms.TextBox textBox_实体模板;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private ICSharpCode.TextEditor.TextEditorControl rtbGeneratedCodeEntities;
        private System.Windows.Forms.Button button_保存设置;
    }
}