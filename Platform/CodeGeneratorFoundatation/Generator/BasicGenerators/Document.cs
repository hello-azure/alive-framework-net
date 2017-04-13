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

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators
{
    class Document : ICodeGenerator
    {
         #region ==== 属性 ====

        /// <summary>
        /// 获得被装饰的内容
        /// </summary>
        public IList<ICodeGenerator> Content
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个 Document 的新的实例
        /// </summary>
        public Document()
        {
            this.Content = new List<ICodeGenerator>();
        }

        #endregion

        #region ==== 接口实现 ====

        #region ICodeGenerator 成员

        public void Write(TextWriter writer, IndentManager indent)
        {
            this.WriteContent(writer, indent);
            writer.Flush();
        }

        #endregion

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        ///写入被装饰代码
        /// </summary>
        /// <param name="writer">写入媒介的接口</param>
        /// <param name="indent">缩进管理器</param>
        protected virtual void WriteContent(TextWriter writer, IndentManager indent)
        {
            foreach (var item in this.Content)
            {
                item.Write(writer, indent);
            }
        }

        #endregion
   }
}
