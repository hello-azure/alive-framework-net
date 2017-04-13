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
    /// Region生成器
    /// </summary>
    internal class Region : CodeDecorator
    {
        #region ==== 属性 ====

        /// <summary>
        /// 命名空间的值
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        #endregion

        #region ==== 内部方法 ====

        /// <summary>
        /// 添加要包含的内容
        /// </summary>
        /// <param name="item">包含的内容</param>
        internal void AddContent(ICodeGenerator item)
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
            writer.Write("#region ");
            writer.WriteLine(this.Name);
            writer.WriteLine();
        }

        /// <summary>
        /// 在写入被装入代码之后，写入装饰内容
        /// </summary>
        /// <param name="writer">写入媒介的接口</param>
        /// <param name="indent">缩进管理器</param>
        protected override void OnWrittenContent(TextWriter writer, IndentManager indent)
        {
            if (this.Content.Count >0 && !(this.Content[this.Content.Count - 1] is BlankLine))
            {
                writer.WriteLine();
            }

            indent.WriteSpace(writer);
            writer.WriteLine("#endregion");
        }

        #endregion
    }
}
