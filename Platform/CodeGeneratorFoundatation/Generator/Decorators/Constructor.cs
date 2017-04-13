/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.IO;
using Alive.Foundation.Utilities.Format;
using Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Common;
using System.Collections.Generic;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators
{
    class Constructor : CodeDecorator
    {
        #region ==== 属性 ====

        /// <summary>
        /// 属性名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 属性的可见性
        /// </summary>
        public QualifierValue Visibility
        {
            get;
            set;
        }

        /// <summary>
        /// 获得或设置头注释生成器
        /// </summary>
        public DocumentComment Comment
        {
            get;
            set;
        }

        /// <summary>
        /// 获得或设置参数列表<参数名称,参数类型>
        /// </summary>
        public IDictionary<string,string> Paras
        {
            get;
            set;
        }

        /// <summary>
        /// 获得或设置构造函数赋值是否为标准写法
        /// </summary>
        public bool StandardWording
        {
            get;
            set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Constructor()
        {
            this.Visibility = QualifierValue.Public;
            this.Paras = new Dictionary<string, string>();
            this.StandardWording = false;
        }

        #endregion

        #region ==== 内部方法 ====

        /// <summary>
        /// 添加代码
        /// </summary>
        /// <param name="item">代码生成器</param>
        internal void AddCode(Code item)
        {
            this.Content.Add(item);
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
            // 头注释
            if (this.Comment != null)
            {
                Comment.Write(writer, indent);
            }

            // 代码
            indent.WriteSpace(writer);

            // 可见性
            Qualifier qulifier = new Qualifier();
            qulifier.Items.Add(this.Visibility);
            qulifier.Write(writer, indent);

            // 属性名
            writer.Write(this.Name);
            writer.Write("(");

            // TODO: 这里将来添加参数的处理
            if (Paras.Count > 0)
            {
                int loop = 0;

                foreach (var item in Paras)
                {
                    if (loop != Paras.Count - 1)
                    {
                        writer.Write(string.Format("{0} {1},", item.Value, item.Key.ToFirstCharLower()));
                    }
                    else
                    {
                        writer.Write(string.Format("{0} {1}", item.Value, item.Key.ToFirstCharLower()));
                    }

                    loop++;    
                }
            }

            writer.WriteLine(")");

            // 大括号
            indent.WriteSpace(writer);
            writer.WriteLine("{");
            indent.IncreaseIndent();

            // TODO: 这里将来添加参数的处理
            if (Paras.Count > 0)
            {
                foreach (var item in Paras)
                {
                    if (StandardWording)
                    {
                        this.AddCode(new Code("this.{0} = {1};", item.Key, item.Key.ToFirstCharLower()));
                        continue;
                    }
                    
                    this.AddCode(new Code("this.{0}.Value = {1}.Value;", item.Key, item.Key.ToFirstCharLower()));
                }
            }
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
