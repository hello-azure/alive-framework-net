using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Alive.Tools.CodeGenerator.Foundatation;
using Alive.Tools.CodeGenerator.Foundatation.Metadata;
using Alive.Foundation.Data;

namespace Alive.Tools.CodeGenerator
{
    public partial class Form_SelectDatabase : Form
    {
        public Form_SelectDatabase()
        {
            InitializeComponent();
            this.textBox1.Text="Data Source=localhost\\SQLEXPRESS;Initial Catalog=Myconos;Integrated Security=True";
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            GlobalData.DataSource = LogicFacade.Instance.GetData(SourceType.SQLSERVER, this.textBox1.Text);
            GlobalMessage.Notifier.DataSourceCreated(this, new EventArgs());
        }
     
    }
}
