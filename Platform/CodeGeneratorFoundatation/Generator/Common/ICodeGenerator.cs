/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.IO;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Common
{
    /// <summary>
    /// 代码生成接口
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// 将代码写入媒介
        /// </summary>
        /// <param name="writer">写入媒介的接口</param>
        /// <param name="indent">缩进管理器</param>
        void Write(TextWriter writer, IndentManager indent);
    }
}
