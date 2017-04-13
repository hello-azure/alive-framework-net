/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Reflection;
using System.Windows.Forms;
using Alive.Foundation.Utilities;

namespace Alive.Tools.CodeGenerator
{
    partial class Form_AboutBox : Form
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        public Form_AboutBox()
        {
            InitializeComponent();

            this.labelProductName.Text = AssemblyTool.AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyTool.AssemblyVersion);
            this.labelCopyright.Text = AssemblyTool.AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyTool.AssemblyCompany;
            this.textBoxDescription.Text = AssemblyTool.AssemblyDescription;
        }

        #endregion

       
    }
}