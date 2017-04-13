/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alive.Foundation.Utilities.Format;
using Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Common;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators;
using Alive.Tools.CodeGenerator.Foundatation.Metadata;
using Alive.Foundation.Data;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Templates
{
    public class TTableAccessService : FileGenerator
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// using 节的信息
        /// </summary>
        private Using usingStage = new Using();


        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 模板
        /// </summary>
        public TemplateTableAccessInfo Template
        {
            get;
            set;
        }

        #endregion

        #region === 构造函数 ====

        /// <summary>
        /// 根据制定的方法信息，创建一个客户端服务代码文件生成器
        /// </summary>
        /// <param name="method">要访问的方法信息</param>
        public TTableAccessService(SourceType sourceType, TableInfo source, TemplateTableAccessInfo template)
        {
            this.SourceType = sourceType;
            this.Source = source;
            this.Template = template;
            this.FileName = this.Source.Name + ".cs";

            if (template.SUsings != null && template.SUsings.Count > 0)
            {
                foreach (var item in template.SUsings)
                {
                    this.usingStage.Add(item);
                }
            }
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 创建生成器
        /// </summary>
        /// <returns></returns>
        protected override ICodeGenerator CreateGenerator()
        {
            Document doc = new Document();

            doc.Content.Add(this.CreateFileTitle());
            doc.Content.Add(this.usingStage);
            doc.Content.Add(this.CreateNameSpace());

            return doc;
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 创建文件头注释
        /// </summary>
        /// <returns>头注释生成器</returns>
        private ICodeGenerator CreateFileTitle()
        {
            FileTitleComment result = new FileTitleComment();

            if (this.Template.STitleComments != null && this.Template.STitleComments.Count > 0)
            {
                foreach (var comment in this.Template.STitleComments)
                {
                    result.Lines.Add(comment);
                }
            }

            return result;
        }

        /// <summary>
        /// 创建命名空间
        /// </summary>
        /// <returns></returns>
        private ICodeGenerator CreateNameSpace()
        {
            NameSpace result = new NameSpace();

            // 命名空间名称
            result.Name = "ClassLibrary1";//默认

            if (!string.IsNullOrEmpty(this.Template.SNameSpace))
            {
                result.Name = this.Template.SNameSpace;
            }

            // 内容
            result.AddContent(this.CreateActionClass());

            return result;
        }

        /// <summary>
        /// 创建动作接口
        /// </summary>
        /// <returns>动作接口生成器</returns>
        private Class CreateActionClass()
        {
            Class result = new Class();


            // 注释
            result.Comment = CreateHeader();
            result.Attributes.Add(string.Format("[TableAttribute(TableName.{0})]",this.Source.Name.Value));

            // 基本信息
            result.BaseName = "ITableBase";
            result.IsPublic = this.Template.SClassVisibility == QualifierValue.Public;
            result.Name = this.Source.Name.Value;

            //方法生成逻辑
            Region region = new Region();
            region.Name = "==== 方法 ====";

            var methords = this.CreateMethords();
            if (methords != null && methords.Count > 0)
            {
                foreach (var methrod in methords)
                {
                    region.AddContent(methrod);
                    region.AddContent(new BlankLine());
                }
            }

            result.AddRegion(region);

            return result;
        }

        /// <summary>
        /// 创建参数属性
        /// </summary>
        /// <param name="item">参数信息</param>
        /// <returns>属性生成器</returns>
        private List<Methord> CreateMethords()
        {
            #region 接口方法1

            Methord methord1 = new Methord();
            //注释
            DocumentComment comment1 = new DocumentComment();
            comment1.SummaryLines.Add("向数据库表中添加记录", false);
            comment1.SummaryLines.Add("<param name=\"action\">数据请求</param>", true);
            comment1.SummaryLines.Add("<param name=\"data\">业务数据</param>", true);
            comment1.SummaryLines.Add("<param name=\"whereCondition\">Where子句执行条件</param>", true);
            comment1.SummaryLines.Add("<returns>数据库执行结果</returns>", true);
            methord1.Comment = comment1;

            //基本信息
            methord1.Visibility = QualifierValue.Internal;
            methord1.Name = "Add";
            methord1.Return = "DbResult ";
            methord1.Paras.Add("action", "NoneQueryRequest");
            methord1.Paras.Add("data", "BusinessObject");
            methord1.Paras.Add("whereCondition", "string");

            methord1.AddCode(new Code("action.SQL = SqlBuilder.GetInsertor<{0}>({1}));", this.Source.Name.Value, "data"));
            methord1.AddCode(new Code("action.SetParameters({0});", "data"));
            methord1.AddCode(new Code("return action.Execute();"));

            #endregion

            #region 接口方法2

            Methord methord2 = new Methord();
            //注释
            DocumentComment comment2 = new DocumentComment();
            comment2.SummaryLines.Add("向数据库表中添加记录", false);
            comment2.SummaryLines.Add("<param name=\"action\">数据请求</param>", true);
            comment2.SummaryLines.Add("<param name=\"data\">业务数据</param>", true);
            comment2.SummaryLines.Add("<returns>数据库执行结果</returns>", true);
            methord2.Comment = comment2;

            //基本信息
            methord2.Visibility = QualifierValue.Internal;
            methord2.Name = "Add";
            methord2.Return = "DbResult ";
            methord2.Paras.Add("action", "NoneQueryRequest");
            methord2.Paras.Add("datas", "IBoList");

            methord2.AddCode(new Code("DbResult result = null;"));
            methord2.AddCode(new Code("var list = datas as {0}List;",this.Source.Name.Value));
            methord2.AddCode(new Code(""));
            methord2.AddCode(new Code("if(list != null && list.Count > 0"));
            methord2.AddCode(new Code("{"));
            methord2.AddCode(new Code("    foreach(var info in list)"));
            methord2.AddCode(new Code("    {"));
            methord2.AddCode(new Code("        action.SQL = SqlBuilder.GetInsertor<{0}>({1}));", this.Source.Name.Value, "info"));
            methord2.AddCode(new Code("        action.SetParameters({0});", "info"));
            methord2.AddCode(new Code("        result = action.Execute();"));
            methord2.AddCode(new Code(""));
            methord2.AddCode(new Code("        if(!result.IsSuccessful)"));
            methord2.AddCode(new Code("        {"));
            methord2.AddCode(new Code("            break;"));
            methord2.AddCode(new Code("        }"));
            methord2.AddCode(new Code("    }"));
            methord2.AddCode(new Code("}"));
            methord2.AddCode(new Code(""));
            methord2.AddCode(new Code("return result;"));

            #endregion

            #region 接口方法2

            Methord methord3 = new Methord();
            //注释
            DocumentComment comment3 = new DocumentComment();
            comment3.SummaryLines.Add("向数据库表中更新记录", false);
            comment3.SummaryLines.Add("<param name=\"action\">数据请求</param>", true);
            comment3.SummaryLines.Add("<param name=\"data\">业务数据</param>", true);
            comment3.SummaryLines.Add("<param name=\"whereCondition\">Where子句执行条件</param>", true);
            comment3.SummaryLines.Add("<returns>数据库执行结果</returns>", true);
            methord3.Comment = comment3;

            //基本信息
            methord3.Visibility = QualifierValue.Internal;
            methord3.Name = "Update";
            methord3.Return = "DbResult ";
            methord3.Paras.Add("action", "NoneQueryRequest");
            methord3.Paras.Add("data", "BusinessObject");
            methord3.Paras.Add("whereCondition", "string");

            methord3.AddCode(new Code("action.SQL = SqlBuilder.GetUpdator<{0}>({1},{2}));", this.Source.Name.Value, "data", "whereCondition"));
            methord3.AddCode(new Code("action.SetParameters({0});", "data"));
            methord3.AddCode(new Code("return action.Execute();"));

            #endregion

            #region 接口方法4

            Methord methord4 = new Methord();
            //注释
            DocumentComment comment4 = new DocumentComment();
            comment4.SummaryLines.Add("向数据库表中添加记录", false);
            comment4.SummaryLines.Add("<param name=\"action\">数据请求</param>", true);
            comment4.SummaryLines.Add("<param name=\"whereCondition\">Where子句执行条件</param>", true);
            comment4.SummaryLines.Add("<returns>数据库执行结果</returns>", true);
            methord4.Comment = comment4;

            //基本信息
            methord4.Visibility = QualifierValue.Internal;
            methord4.Name = "Remove";
            methord4.Return = "DbResult ";
            methord4.Paras.Add("action", "NoneQueryRequest");
            methord4.Paras.Add("whereCondition", "string");

            methord4.AddCode(new Code("action.SQL = SqlBuilder.GetDeleter<{0}>(new {0}(),{1}));", this.Source.Name.Value, "whereCondition"));
            methord4.AddCode(new Code("return action.Execute();"));

            #endregion

            #region 接口方法5

            Methord methord5 = new Methord();
            //注释
            DocumentComment comment5 = new DocumentComment();
            comment5.SummaryLines.Add("查询数据库表记录", false);
            comment5.SummaryLines.Add("<param name=\"action\">数据请求</param>", true);
            comment5.SummaryLines.Add("<param name=\"whereCondition\">Where子句执行条件</param>", true);
            comment5.SummaryLines.Add("<returns>数据库执行结果</returns>", true);
            methord5.Comment = comment5;

            //基本信息
            methord5.Visibility = QualifierValue.Internal;
            methord5.Name = "Select";
            methord5.Return = "DataResult<IBoList> ";
            methord5.Paras.Add("action", "QueryRequest");
            methord5.Paras.Add("whereCondition", "string");

            methord5.AddCode(new Code("DataResult<{0}List> result = new DataResult<{0}List>();", this.Source.Name.Value));
            methord5.AddCode(new Code("action.SQL = SqlBuilder.GetSelector<{0}>(new {0}(),{1}));", this.Source.Name.Value, "whereCondition"));
            methord5.AddCode(new Code("DbResult dbResult = action.Execute();"));
            methord5.AddCode(new Code(""));
            methord5.AddCode(new Code("if (dbResult.IsSuccessful)"));
            methord5.AddCode(new Code("{"));

            if (this.Source.Columns != null && this.Source.Columns.Count > 0)
            {
                foreach (var column in this.Source.Columns)
                {
                    methord5.AddCode(new Code("    dbResult.Injector.SetFieldMapping(\"{0}\", \"{0}\");", column.Name.Value));
                }
            }

            methord5.AddCode(new Code("    {0}List list = new {0}List();", this.Source.Name.Value));
            methord5.AddCode(new Code("    dbResult.Injector.Inject(list);"));
            methord5.AddCode(new Code("    dbResult.Injector.Close();"));
            methord5.AddCode(new Code("    result.ResultData = list;"));
            methord5.AddCode(new Code("}"));
            methord5.AddCode(new Code(""));
            methord5.AddCode(new Code("return result;"));

            #endregion


            return new List<Methord>() { methord1, methord2, methord3, methord4, methord5 };
        }


        /// <summary>
        /// 生成类的头注释生成器
        /// </summary>
        /// <returns>类的头注释生成器</returns>
        private DocumentComment CreateHeader()
        {
            DocumentComment comments = new DocumentComment();

            if (this.Template.SDocumentComment != null && this.Template.SDocumentComment.Count > 0)
            {
                foreach (var comment in this.Template.SDocumentComment)
                {
                    comments.SummaryLines.Add(comment, false);
                }
            }

            return comments;
        }

        #endregion
    }
}
