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
    /// <summary>
    /// 文件头注释
    /// </summary>
    internal class FileTitleComment : ICodeGenerator
    {
        #region ==== 属性 ====

        /// <summary>
        /// 注释行
        /// </summary>
        public IList<string> Lines
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 标准构造函数
        /// </summary>
        public FileTitleComment()
        {
            this.Lines = new List<string>();
        }

        #endregion

        #region ==== 接口实现 ====

        #region ICodeGenerator 成员

        public void Write(TextWriter writer, IndentManager indent)
        {
            indent.WriteSpace(writer);
            writer.WriteLine("/***********");

            foreach (var item in this.Lines)
            {
                indent.WriteSpace(writer);
                writer.Write(" * ");
                writer.WriteLine(item);
            }

            indent.WriteSpace(writer);
            writer.WriteLine(" *");
            indent.WriteSpace(writer);
            writer.WriteLine(" */");
            writer.WriteLine();
        }

        #endregion

        #endregion
    }
}
