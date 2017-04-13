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
    /// 命名空间引用
    /// </summary>
    internal class Using : ICodeGenerator
    {
         #region ==== 私有字段 ====

        /// <summary>
        /// 引用的命名空间
        /// </summary>
        private List<string> lines = new List<string>();

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 添加一个引用。如果引用已经存在，则忽略。
        /// </summary>
        /// <param name="nameSpace">要添加的命名空间</param>
        public void Add(string nameSpace)
        {
            if (!this.lines.Contains(nameSpace))
            {
                this.lines.Add(nameSpace);
            }
        }

        /// <summary>
        /// 将制定的引用合并到当前序列中
        /// </summary>
        /// <param name="source">提供信息的序列</param>
        public void Merge(Using source)
        {
            foreach (var item in source.lines)
            {
                this.Add(item);
            }
        }

        #endregion

        #region ==== 接口实现 ====

        #region ICodeGenerator 成员

        public void Write(TextWriter writer, IndentManager indent)
        {
            this.lines.Sort(new SortRule());

            foreach (var item in this.lines)
            {
                indent.WriteSpace(writer);
                writer.Write("using ");
                writer.Write(item);
                writer.WriteLine(";");
            }

            if (this.lines.Count > 0)
            {
                writer.WriteLine();
            }
        }

        #endregion

        #endregion

        #region ==== 定义 ====

        /// <summary>
        /// using项目的排序规则
        /// </summary>
        public class SortRule : IComparer<string>
        {
            #region IComparer<string> 成员

            public int Compare(string x, string y)
            {
                bool xIsSystem = x.StartsWith("System");
                bool yIsSystem = y.StartsWith("System");

                if (xIsSystem)
                {
                    if (yIsSystem)
                    {
                        return x.CompareTo(y);
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (yIsSystem)
                    {
                        return 1;
                    }
                    else
                    {
                        return y.CompareTo(x);
                    }
                }
            }

            #endregion
        }

        #endregion
    }
}
