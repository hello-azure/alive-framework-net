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

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Templates
{
    /// <summary>
    /// 表名枚举类型生成服务
    /// </summary>
    public class TTableAttributeService : FileGenerator
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
        public TemplateTableAttributeInfo Template
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
        public TTableAttributeService(TemplateTableAttributeInfo template)
        {
            this.Template = template;
            this.FileName = "TableAttribute.cs";

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

            // 属性
            result.Attributes = this.Template.SAttributes;
            

            // 基本信息
            result.IsPublic = false;
            result.Name = "TableAttribute";

            // 属性
            Region propertyRegion = new Region();
            propertyRegion.Name = "==== 公共属性 ====";
            Property property = this.CreateProperty();
            propertyRegion.AddContent(property);
            result.AddRegion(propertyRegion);

            // 构造函数
            Region constructorRegion = new Region();
            constructorRegion.Name = "==== 构造函数 ====";
            Constructor constructor = this.CreateConstructor();
            constructorRegion.AddContent(constructor);
            result.AddRegion(constructorRegion);

            return result;
        }

        /// <summary>
        /// 创建参数属性
        /// </summary>
        /// <param name="item">参数信息</param>
        /// <returns>属性生成器</returns>
        private Property CreateProperty()
        {
            Property result = new Property();

            //注释
            DocumentComment comment = new DocumentComment();

            comment.SummaryLines.Add("获得表名", false);
            result.Comment = comment;

            //基本信息
            result.Name = "Name";
            result.Visibility = QualifierValue.Internal;

            result.TypeName = new TypeName("TableName");

            //访问器 get
            GetterSetter getter = new GetterSetter();
            getter.StandardWording = true;
            getter.Type = GetterSetterType.Getter;
            result.AddGetterSetter(getter);


            GetterSetter setter = new GetterSetter();
            setter.StandardWording = true;
            setter.Visibility = QualifierValue.Private;
            setter.Type = GetterSetterType.Setter;
            result.AddGetterSetter(setter);

            return result;
        }

        /// <summary>
        /// 创建构造函数
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>构造函数生成器</returns>
        private Constructor CreateConstructor()
        {
            Constructor result = new Constructor();

            // 注释
            DocumentComment comment = new DocumentComment();

            comment.SummaryLines.Add("构造函数", false);
            comment.SummaryLines.Add("<param name=\"name\">表名</param>",true);
            result.Comment = comment;

            result.StandardWording = true;
            result.Paras.Add("name", "TableName");
            result.Visibility = QualifierValue.Internal;
            result.Name = "TableAttribute";
            

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
