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
    /// 实体模板信息
    /// </summary>
    public class TemplateEntityInfo:TemplateInfoBase
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        public TemplateEntityInfo(string templatePath)
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

            //SBaseClass
            this.SBaseClass = root.Element("Class").Attribute("base").Value;

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
            var attributes = root.Element("Class").Element("Attributes").Elements("Attribute");
            if (attributes != null && attributes.Count() > 0)
            {
                this.SAttributes = new List<string>();

                foreach (var element in attributes)
                {
                    this.SDocumentComment.Add(element.Value);
                }
            }

            //SConstructors
            var Constructors = root.Element("Class").Element("Constructors").Elements("Constructor");
            if (Constructors != null && Constructors.Count() > 0)
            {
                this.SConstructors = new List<ConstructorInfo>();

                foreach (var element in Constructors)
                {
                    var consturctor = new ConstructorInfo(
                        (QualifierValue)Enum.Parse(typeof(QualifierValue), element.Attribute("visibility").Value, true),
                        (ParaType)Enum.Parse(typeof(ParaType), element.Attribute("paraType").Value, true));

                    if (consturctor.ParaType == ParaType.Full)
                    {
                        consturctor.ParaDataType = (DataType)Enum.Parse(typeof(DataType), element.Attribute("dataType").Value, true);
                    }

                    this.SConstructors.Add(consturctor);
                }
            }

            //属性设置信息
            this.SProperty = new PropertyInfo(
                (QualifierValue)Enum.Parse(typeof(QualifierValue), root.Element("Class").Element("Propertys").Attribute("visibility").Value, true),
                (DataType)Enum.Parse(typeof(DataType), root.Element("Class").Element("Propertys").Attribute("dataType").Value, true),
                root.Element("Class").Element("Propertys").Attribute("comment").Value.ToUpper() == "YES" ? true : false);
        }

        #endregion

        #region ==== 公共属性 ====

        /// <summary>
        /// 模板构造器信息设置
        /// </summary>
        internal List<ConstructorInfo> SConstructors
        {
            get;
            private set;
        }

        /// <summary>
        /// 属性设置信息
        /// </summary>
        internal PropertyInfo SProperty
        {
            get;
            private set;
        }

        #endregion
    }
}
