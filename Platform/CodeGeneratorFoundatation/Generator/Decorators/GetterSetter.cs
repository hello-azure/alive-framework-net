/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.IO;
using Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Common;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators
{
    /// <summary>
    /// 属性/索引器/事件的访问器
    /// </summary>
    internal class GetterSetter : CodeDecorator
    {
        #region ==== 属性 ====

        /// <summary>
        /// 访问器类型
        /// </summary>
        public GetterSetterType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 设置可见类型
        /// </summary>
        public QualifierValue Visibility
        {
            get;
            set;
        }

        /// <summary>
        /// 标准写法
        /// </summary>
        public bool StandardWording
        {
            get;
            set;
        }

        /// <summary>
        /// 访问器名称
        /// </summary>
        protected string Name
        {
            get
            {
                switch (this.Type)
                {
                    case GetterSetterType.Getter:
                        return "get";
                    case GetterSetterType.Setter:
                        return "set";
                    case GetterSetterType.Adder:
                        return "add";
                    case GetterSetterType.Remover:
                        return "remove";
                    default:
                        return string.Empty;
                }
            }
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GetterSetter()
        {
            this.Type = GetterSetterType.Getter;
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
            indent.WriteSpace(writer);

            // 可见性
            Qualifier qulifier = new Qualifier();
            qulifier.Items.Add(this.Visibility);

            qulifier.Write(writer, indent);

            if (StandardWording)
            {
                writer.WriteLine(string.Format("{0};", this.Name));
                return;
            }

            // 类型
            writer.WriteLine(this.Name);

            // 大括号
            indent.WriteSpace(writer);
            writer.WriteLine("{");
            indent.IncreaseIndent();
        }

        /// <summary>
        /// 在写入被装入代码之后，写入装饰内容
        /// </summary>
        /// <param name="writer">写入媒介的接口</param>
        /// <param name="indent">缩进管理器</param>
        protected override void OnWrittenContent(TextWriter writer, IndentManager indent)
        {
            if (StandardWording)
            {
                return;
            }

            indent.DecreaseIndent();

            indent.WriteSpace(writer);
            writer.WriteLine("}");
        }

        #endregion
    }

    /// <summary>
    /// 访问器类型
    /// </summary>
    internal enum GetterSetterType
    {
        Getter,
        Setter,
        Adder,
        Remover
    }
}
