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
using System.Collections.Generic;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators
{
    /// <summary>
    /// 文档注释（三线注释）
    /// </summary>
    internal class DocumentComment : ICodeGenerator
    {
        #region ==== 属性 ====

        /// <summary>
        /// 注释行<注释信息,是否为参数注释>
        /// </summary>
        public IDictionary<string,bool> SummaryLines
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DocumentComment()
        {
            this.SummaryLines = new Dictionary<string, bool>();
        }

        #endregion

        #region ==== 接口实现 ====

        #region ICodeGenerator 成员

        public void Write(TextWriter writer, IndentManager indent)
        {
            if (SummaryLines.Count <= 0)
            {
                return;
            }

            indent.WriteSpace(writer);
            writer.WriteLine("/// <summary>");

            foreach (var line in this.SummaryLines)
            {
                //表示该注释不是参数注释
                if (!line.Value)
                {
                    indent.WriteSpace(writer);
                    writer.Write("/// ");
                    writer.WriteLine(line.Key);
                }
            }

            indent.WriteSpace(writer);
            writer.WriteLine("/// </summary>");

            foreach (var line in this.SummaryLines)
            {      
                //表示该注释不是参数注释
                if (line.Value)
                {
                    indent.WriteSpace(writer);
                    writer.Write("/// ");
                    writer.WriteLine(line.Key);
                }
            }            
        }

        #endregion

        #endregion
   }
}
