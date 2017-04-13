/***********
 * 版权声明：
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
    /// 实体模板
    /// </summary>
    public class TEntityService : FileGenerator
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
        public TemplateEntityInfo Template
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
        public TEntityService(SourceType sourceType, TableInfo source, TemplateEntityInfo template)
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

            // 属性
            var columns = this.Source.Columns;
            Region propertyRegion = new Region();
            propertyRegion.Name = "==== 属性 ====";

            foreach (var item in columns)
            {
                Property property = CreateParameterProperty(item);
                propertyRegion.AddContent(property);
                propertyRegion.AddContent(new BlankLine());
            }



            result.AddRegion(propertyRegion);

            // 构造函数
            Region constructorRegion = new Region();
            constructorRegion.Name = "==== 构造函数 ====";
            var constructors = this.Template.SConstructors;

            if (constructors != null && constructors.Count > 0)
            {
                foreach (var setting in constructors)
                {
                    Constructor constructor = CreateConstructor(setting);
                    constructorRegion.AddContent(constructor);
                }
            }
                       
            result.AddRegion(constructorRegion);

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

        /// <summary>
        /// 创建参数属性
        /// </summary>
        /// <param name="item">参数信息</param>
        /// <returns>属性生成器</returns>
        private Property CreateParameterProperty(ColumnInfo column)
        {
            Property result = new Property();

            //注释
            DocumentComment comment = new DocumentComment();

            if (this.Template.SProperty.IsComment)
            {
                comment.SummaryLines.Add(string.Format("获得或设置{0}",column.Name),false);
                result.Comment = comment;
            }

            //基本信息
            result.Name = column.Name.Value;
            result.Visibility = this.Template.SProperty.Visibility;

          



            if (this.Template.SProperty.DataType == DataType.DataField)
            {
                result.TypeName = new TypeName(TypeFormatter.Format(this.SourceType, column.Type.Value, false));

                //访问器 get
                GetterSetter getter = new GetterSetter();
                getter.Type = GetterSetterType.Getter;
                getter.AddCode(new Code(
                    "return this.GetField<{0}>(\"{1}\");",
                    result.TypeName,
                    result.Name));
                result.AddGetterSetter(getter);


                GetterSetter setter = new GetterSetter();
                setter.Type = GetterSetterType.Setter;
                setter.AddCode(new Code(
                    "this.GetField<{0}>(\"{1}\").Value = value.Value;",
                    result.TypeName,
                    result.Name));
                result.AddGetterSetter(setter);
            }
            else
            {
                result.TypeName = new TypeName(TypeFormatter.Format(this.SourceType, column.Type.Value, true));

                //访问器 get
                GetterSetter getter = new GetterSetter();
                getter.Type = GetterSetterType.Getter;
                result.AddGetterSetter(getter);


                GetterSetter setter = new GetterSetter();
                setter.Type = GetterSetterType.Setter;
                result.AddGetterSetter(setter);
            }
             
              
         

            return result;
        }

        /// <summary>
        /// 创建构造函数
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>构造函数生成器</returns>
        private Constructor CreateConstructor(ConstructorInfo setting)
        {
            Constructor result = new Constructor();

            if (setting.ParaType == ParaType.Default)
            {
                // 注释
                DocumentComment comment = new DocumentComment();

                comment.SummaryLines.Add(string.Format("创建一个 {0} 的新的实例",this.Source.Name.Value),false);
                result.Comment = comment;
                result.Visibility = setting.Visibility;
                result.Name = this.Source.Name.Value;
            }
            else
            {
                // 注释
                DocumentComment comment = new DocumentComment();
                comment.SummaryLines.Add(string.Format("创建一个 {0} 的新的实例", this.Source.Name.Value), false);

                if (this.Source.Columns != null && this.Source.Columns.Count > 0)
                {
                    foreach (var column in this.Source.Columns)
                    {
                        bool isStandard = true;
                        comment.SummaryLines.Add(string.Format("<param name=\"{0}\">{0}</param>",column.Name.Value.ToFirstCharLower()),true);

                        if (setting.ParaDataType == DataType.DataField)
                        {
                            isStandard = false;
                        }

                        result.Paras.Add(column.Name.Value, TypeFormatter.Format(this.SourceType, column.Type.Value, isStandard));
                    }
                }

                result.Comment = comment;
                result.Visibility = setting.Visibility;
                result.Name = this.Source.Name.Value;
            }

            return result;
        }


        #endregion
    }
}
