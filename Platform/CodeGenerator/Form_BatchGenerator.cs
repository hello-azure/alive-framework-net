using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Templates;
using Alive.Tools.CodeGenerator.Foundatation.Metadata;
using Alive.Tools.CodeGenerator.Foundatation;
using Alive.Foundation.Data;

namespace Alive.Tools.CodeGenerator
{
    public partial class Form_BatchGenerator : Form
    {
        string outputBasePath = "..\\ClientFiles";

        public Form_BatchGenerator()
        {
            InitializeComponent();
        }

        private void button_保存设置_Click(object sender, EventArgs e)
        {
            

             var datas=GlobalData.DataSource as TableInfoList;

             if (datas != null && datas.Count > 0)
             {
                 foreach (var item in datas)
                 {
                     //TEntityService file = new TEntityService(SourceType.SQLSERVER, item, new TemplateEntityInfo(@"E:\基础库\Platform\CodeGenerator\Templates\EntityTemplate.xml"));
                     //file.OutputPath = outputBasePath + "\\TheItems";
                     //file.Generate();

                     //TDataAccessService file = new TDataAccessService(SourceType.SQLSERVER, item, new TemplateDataAccessInfo(@"E:\基础库\Platform\CodeGenerator\Templates\DataAccessTemplate.xml"));
                     //file.OutputPath = outputBasePath + "\\TheDataAccess";
                     //file.Generate();

                     TTableAccessService file = new TTableAccessService(SourceType.SQLSERVER, item, new TemplateTableAccessInfo(@"E:\基础库\v2\Platform\CodeGenerator\Templates\AliveTableAccessTemplate.xml"));
                     file.OutputPath = outputBasePath + "\\TheDataAccess1";
                     file.Generate();
                 }
             }

             //TTableEnumService file = new TTableEnumService(SourceType.SQLSERVER, datas, new TemplateTableEnumInfo(@"E:\基础库\v2\Platform\CodeGenerator\Templates\AliveTableEnumTemplate.xml"));
             //file.OutputPath = outputBasePath + "\\TheDataAccess";
             //file.Generate();

             //TTableBaseService file1 = new TTableBaseService(new TemplateTableBaseInfo(@"E:\基础库\v2\Platform\CodeGenerator\Templates\AliveTableBaseTemplate.xml"));
             //file1.OutputPath = outputBasePath + "\\TheDataAccess";
             //file1.Generate();

             //TTableAttributeService file2 = new TTableAttributeService(new TemplateTableAttributeInfo(@"E:\基础库\v2\Platform\CodeGenerator\Templates\AliveTableAttributeTemplate.xml"));
             //file2.OutputPath = outputBasePath + "\\TheDataAccess";
             //file2.Generate();
           
            //var result = Alive.Foundation.Data.DataSerializer.Encode(new TestClass());

             TDbTableService file3 = new TDbTableService(SourceType.SQLSERVER, new TemplateDbTableInfo(@"E:\基础库\v2\Platform\CodeGenerator\Templates\AliveDbTableTemplate.xml"));
             file3.OutputPath = outputBasePath + "\\TheDataAccess1";
             file3.Generate();


        }
    }
}
