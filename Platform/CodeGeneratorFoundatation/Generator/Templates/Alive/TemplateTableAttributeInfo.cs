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
using System.Xml.Linq;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Templates
{
    /// <summary>
    /// 表名枚举模板信息
    /// </summary>
    public class TemplateTableAttributeInfo:TemplateInfoBase
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        public TemplateTableAttributeInfo(string templatePath)
        {
            XElement root = XElement.Load(templatePath);

            //STitleComments
            var elements = root.Element("TitleComments").Elements("Comment");

            if (elements != null && elements.Count() > 0)
            {
                this.STitleComments = new List<string>();
                foreach (var element in elements)
                {
                    this.STitleComments.Add(element.Value);
                }
            }

            //SUsings
            var usings = root.Element("Usings").Elements("using");
            if (usings != null && usings.Count() > 0)
            {
                this.SUsings = new List<string>();

                foreach (var element in usings)
                {
                    this.SUsings.Add(element.Value);
                }
            }

            //SNameSpace
            this.SNameSpace = root.Element("NameSpace").Attribute("name").Value;

            //SClassVisibility
            this.SClassVisibility = (QualifierValue)Enum.Parse(typeof(QualifierValue), root.Element("Class").Attribute("visibility").Value, true);

            //SDocumentComment
            var documentComment = root.Element("Class").Element("DocumentComment").Elements("Comment");
            if (documentComment != null && documentComment.Count() > 0)
            {
                this.SDocumentComment = new List<string>();

                foreach (var element in documentComment)
                {
                    this.SDocumentComment.Add(element.Value);
                }
            }

            //SAttributes
            var attributes = root.Element("Enum").Element("Attributes").Elements("Attribute");
            if (attributes != null && attributes.Count() > 0)
            {
                this.SAttributes = new List<string>();

                foreach (var element in attributes)
                {
                    this.SDocumentComment.Add(element.Value);
                }
            }
        }

        #endregion
    }
}
