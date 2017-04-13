/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Common
{
    /// <summary>
    /// 文件缩进管理
    /// </summary>
    public class IndentManager
    {
        #region ==== 常量 ====

        /// <summary>
        /// 默认的缩进距离
        /// </summary>
        private const int IndentDistanceDefault = 4;

        #endregion

        #region ==== 私有字段 ====

        /// <summary>
        /// 缩进级别
        /// </summary>
        private int level = 0;

        /// <summary>
        /// 每级缩进字符
        /// </summary>
        private string indentString = string.Empty;

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个默认的缩进管理器
        /// </summary>
        public IndentManager()
        {
            for (int i = 0; i < IndentDistanceDefault; i++)
            {
                this.indentString += " ";
            }
        }

        /// <summary>
        /// 创建一个指定了缩进距离的缩进管理器
        /// </summary>
        /// <param name="intendDistance">指定的所近距离</param>
        public IndentManager(int intendDistance)
        {
            for (int i = 0; i < intendDistance; i++)
            {
                this.indentString += " ";
            }
        }

        #endregion

        #region ==== 内部方法 ====

        /// <summary>
        /// 增加缩进
        /// </summary>
        internal void IncreaseIndent()
        {
            this.level++;
        }

        /// <summary>
        /// 减少缩进
        /// </summary>
        internal void DecreaseIndent()
        {
            this.level--;
        }

        /// <summary>
        /// 将缩进的空白写入媒介
        /// </summary>
        /// <param name="writer">写入媒介的接口</param>
        internal void WriteSpace(TextWriter writer)
        {
            for (int i = 0; i < this.level; i++)
            {
                writer.Write(this.indentString);
            }
        }

        #endregion
    }
}
