/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using Alive.Tools.CodeGenerator.Foundatation.Generator.Common;
using System.IO;
using Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators
{
    /// <summary>
    /// 属性
    /// </summary>
    class Property : CodeDecorator
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
        /// 属性类型
        /// </summary>
        public TypeName TypeName
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

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Property()
        {
            this.Visibility = QualifierValue.Public;
        }

        #endregion

        #region ==== 内部方法 ====

        /// <summary>
        /// 添加访问器
        /// </summary>
        /// <param name="item">访问器生成器</param>
        internal void AddGetterSetter(GetterSetter item)
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

            // 类型
            this.TypeName.Write(writer, indent);
            writer.Write(" ");

            // 属性名
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
            indent.DecreaseIndent();

            indent.WriteSpace(writer);
            writer.WriteLine("}");
        }

        #endregion
    }
}
