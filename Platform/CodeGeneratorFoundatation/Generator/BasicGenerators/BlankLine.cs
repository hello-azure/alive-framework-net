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
    /// 一个空行
    /// </summary>
    class BlankLine : ICodeGenerator
    {
        #region ==== 接口实现 ====

        #region ICodeGenerator 成员

        public void Write(TextWriter writer, IndentManager indent)
        {
            writer.WriteLine();
            writer.Flush();
        }

        #endregion

        #endregion
    }
}
