/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.Collections.Generic;
using System.IO;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Common;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators
{
    /// <summary>
    /// 限定符标记
    /// </summary>
    internal class Qualifier : CodeDecorator
    {
        #region ==== 属性 ====

        /// <summary>
        /// 限定符序列
        /// </summary>
        public IList<QualifierValue> Items
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 标准构造函数
        /// </summary>
        public Qualifier()
        {
            this.Items = new List<QualifierValue>();
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
            foreach (var item in this.Items)
            {
                if (item == QualifierValue.Null)
                {
                    continue;
                }

                writer.Write(item.ToString().ToLower());
                writer.Write(" ");
            }
        }

        /// <summary>
        /// 在写入被装入代码之后，写入装饰内容
        /// </summary>
        /// <param name="writer">写入媒介的接口</param>
        /// <param name="indent">缩进管理器</param>
        protected override void OnWrittenContent(TextWriter writer, IndentManager indent)
        {
            // do nothing.
        }

        #endregion
    }
}
