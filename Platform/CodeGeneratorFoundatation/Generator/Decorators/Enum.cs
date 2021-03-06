﻿/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.IO;
using System.Reflection;
using Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Common;
using System.Collections.Generic;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators
{
    /// <summary>
    /// 枚举定义
    /// </summary>
    internal class DEnum : CodeDecorator
    {
        #region ==== 私有字段 ====


        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 获得或设置枚举名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 获得或设置一个值，表示当前类是否为一个公有的类型
        /// </summary>
        public bool IsPublic
        {
            get;
            set;
        }

        /// <summary>
        /// 获得或设置类注释生成器
        /// </summary>
        public DocumentComment Comment
        {
            get;
            set;
        }

        /// <summary>
        /// 获得或设置枚举值
        /// </summary>
        public IList<string> Enumerations
        {
            get;
            set;
        }

        /// <summary>
        /// 设置的特性
        /// </summary>
        public List<string> Attributes
        {
            get;
            set;
        }


        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 标准构造函数
        /// </summary>
        public DEnum()
        {
            this.IsPublic = false;
            this.Enumerations = new List<string>();
            this.Attributes = new List<string>();
        }

        #endregion

        #region ==== 内部方法 ====

        /// <summary>
        /// 添加枚举值
        /// </summary>
        /// <param name="item">属性生成器</param>
        internal void AddEnumeration(ICodeGenerator item)
        {
            this.Content.Add(item);
            this.Content.Add(new BlankLine());
        }

        /// <summary>
        /// 添加一个Region
        /// </summary>
        /// <param name="item">Region生成器</param>
        internal void AddRegion(Region item)
        {
            this.Content.Add(item);
            this.Content.Add(new BlankLine());
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 在写入被装饰代码之前，写入装饰内容
        /// </summary>
        /// <param name="writer">写入媒介的接口</param>
        /// <param name="indent">缩进管理器</param>
        protected override void OnWritingContent(TextWriter writer, IndentManager indent)
        {
            // 类注释
            if (this.Comment != null)
            {
                Comment.Write(writer, indent);
            }

            indent.WriteSpace(writer);

            if (this.Attributes.Count > 0) 
            {
                foreach (var item in this.Attributes)
                {
                    writer.WriteLine(item);
                }
            }

            // 类代码
            indent.WriteSpace(writer);

            // 限定符
            Qualifier qulifier = new Qualifier();
            qulifier.Items.Add(this.IsPublic ? QualifierValue.Public : QualifierValue.Internal);
            qulifier.Items.Add(QualifierValue.Enum);
            qulifier.Write(writer, indent);

            // 类名
            writer.Write(this.Name);


            // 大括号
            writer.WriteLine();
            indent.WriteSpace(writer);
            writer.WriteLine("{");

            // 处理多余的空行
            if (this.Content.Count > 0)
            {
                this.Content.RemoveAt(this.Content.Count - 1);
            }

            //// 类属性
            //if (Enumerations.Count > 0)
            //{
            //    foreach (var enumeration in Enumerations)
            //    {
            //        if (!string.IsNullOrEmpty(enumeration))
            //        {
            //            indent.WriteSpace(writer);
            //            writer.WriteLine(enumeration);
            //        }
            //    }
            //}

            indent.IncreaseIndent();
        }

        /// <summary>
        /// 在写入被装入代码之后，写入装饰内容
        /// </summary>
        /// <param name="writer">写入媒介的接口</param>
        /// <param name="indent">缩进管理器</param>
        protected override void OnWrittenContent(TextWriter writer, IndentManager indent)
        {
            indent.DecreaseIndent();

            indent.WriteSpace(writer);
            writer.WriteLine("}");
        }

        #endregion
    }
}
