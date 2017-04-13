/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.IO;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Common;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators
{
    /// <summary>
    /// 生成一行代码
    /// </summary>
    internal class Code : ICodeGenerator
    {
        #region ==== 属性 ====

        /// <summary>
        /// 代码内容
        /// </summary>
        public string CodeLine
        {
            get;
            set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的空白的代码行生成器
        /// </summary>
        public Code()
        { }

        /// <summary>
        /// 根据制定代码行创建一个新的生成器
        /// </summary>
        /// <param name="codeLine">要生成的代码</param>
        public Code(string codeLine)
        {
            this.CodeLine = codeLine;
        }

        /// <summary>
        /// 创建一个新的带格式的代码行生成器
        /// </summary>
        /// <param name="format">代码模板</param>
        /// <param name="args">替换内容</param>
        public Code(string format, params object[] args)
        {
            this.CodeLine = string.Format(format, args);
        }

        #endregion

        #region ==== 接口实现 ====

        #region ICodeGenerator 成员

        public void Write(TextWriter writer, IndentManager indent)
        {
            indent.WriteSpace(writer);
            writer.WriteLine(this.CodeLine);
            writer.Flush();
        }

        #endregion

        #endregion
    }
}
