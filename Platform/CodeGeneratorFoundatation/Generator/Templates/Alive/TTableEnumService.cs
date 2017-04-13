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
    /// <summary>
    /// 表名枚举类型生成服务
    /// </summary>
    public class TTableEnumService : FileGenerator
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
        public TemplateTableEnumInfo Template
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
        public TTableEnumService(SourceType sourceType, TableInfoList sources, TemplateTableEnumInfo template)
        {
            this.SourceType = sourceType;
            this.Sources = sources;
            this.Template = template;
            this.FileName = "TableName.cs";

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
        private DEnum CreateActionClass()
        {
            DEnum result = new DEnum();

            // 注释
            result.Comment = CreateClassHeader();

            // 基本信息
            result.IsPublic = this.Template.SClassVisibility == QualifierValue.Public;
            result.Name = "TableName";


            if (this.Sources != null && this.Sources.Count > 0)
            {
                for (int i = 0; i < this.Sources.Count; i++)
                {
                    if (i > 0)
                    {
                        result.AddEnumeration(new Code(","));
                    }

                    result.AddEnumeration(new Code(this.Sources[i].Name.Value));
                }
            }

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
                    comments.SummaryLines.Add(comment, false);
                }
            }

            return comments;
        }

        #endregion
    }
}
