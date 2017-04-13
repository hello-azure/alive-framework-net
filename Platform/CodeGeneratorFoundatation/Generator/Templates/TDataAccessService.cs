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
    public class TDataAccessService: FileGenerator
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
        public TemplateDataAccessInfo Template
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
        public TDataAccessService(SourceType sourceType, TableInfo source, TemplateDataAccessInfo template)
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
        /// 创建动作类
        /// </summary>
        /// <returns>动作类生成器</returns>
        private Class CreateActionClass()
        {
            Class result = new Class();
           

            // 注释
            result.Comment = CreateClassHeader();
            result.Attributes = this.Template.SAttributes;
            
            // 基本信息
            result.BaseName = this.Template.SBaseClass;
            result.IsPublic = this.Template.SClassVisibility == QualifierValue.Public;
            result.Name = this.Source.Name.Value;

            //方法生成逻辑
            Region region = new Region();
            region.Name = "==== 方法 ====";

            Methord insertMethord = CreateInsertMethord(this.Source.Columns);
            region.AddContent(insertMethord);
            region.AddContent(new BlankLine());
            result.AddRegion(region);

            return result;
        }

        /// <summary>
        /// 创建参数属性
        /// </summary>
        /// <param name="item">参数信息</param>
        /// <returns>属性生成器</returns>
        private Methord CreateInsertMethord(ColumnInfoList columns)
        {
            Methord  result = new Methord();

            //注释
            DocumentComment comment = new DocumentComment();
            comment.SummaryLines.Add(string.Format("向{0}表中插入数据", this.Source.Name.Value), false);
            result.Comment = comment;

            //基本信息
            result.Name = "Insert";
            result.Visibility = QualifierValue.Internal;
            result.SecondModifier = QualifierValue.Override;
            result.Return = "bool";
            result.Paras.Add("data", string.Format("{0}List", this.Source.Name));

            result.AddCode(new Code("using (DbOperator mainDb = new DbOperator(DataBaseName.Main))"));
            result.AddCode(new Code("{"));
            result.AddCode(new Code("NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();"));
            result.AddCode(new Code(string.Format("sql.Append(\"insert into {0}(\");", this.Source.Name)));

            var intotable = string.Empty;
            var intotablepara = string.Empty;

            for (int i = 0; i < columns.Count; i++)
            {
                if (i != columns.Count - 1)
                {
                    intotable += string.Format("{0},", columns[i].Name.Value);
                    intotablepara += string.Format("@{0},", columns[i].Name.Value);
                }
                else
                {
                    intotable += string.Format("{0}", columns[i].Name.Value);
                    intotablepara += string.Format("@{0}", columns[i].Name.Value);
                }
            }

            result.AddCode(new Code(string.Format("sql.Append(\"{0}\");", intotable)));
            result.AddCode(new Code("sql.Append(\") values (\");"));
            result.AddCode(new Code(string.Format("sql.Append(\"{0}\");", intotablepara)));
            result.AddCode(new Code("sql.Append(\") \");"));
            result.AddCode(new Code(" action.SQL = sql.ToString();"));

            foreach (var column in columns)
            {
                result.AddCode(new Code(string.Format("action.SetParameter(\"@{0}\", data.{0}.value);", column.Name.Value)));
            }

            result.AddCode(new Code("DbResult result = action.Execute();"));
            result.AddCode(new Code("return result.IsSuccessful;"));

            return result;
        }


        /// <summary>
        /// 生成类的头注释生成器
        /// </summary>
        /// <returns>类的头注释生成器</returns>
        private DocumentComment CreateClassHeader()
        {
            DocumentComment comments = new DocumentComment();

            if (this.Template.SDocumentComment != null && this.Template.SDocumentComment.Count > 0)
            {
                foreach (var comment in this.Template.SDocumentComment)
                {
                    comments.SummaryLines.Add(comment,false);
                }
            }

            return comments;
        }

        #endregion
    }
}
