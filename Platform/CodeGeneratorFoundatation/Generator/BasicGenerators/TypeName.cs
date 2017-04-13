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
using System;
using System.Text;
using Alive.Foundation.Data.DataFields;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators
{
    /// <summary>
    /// 类型名称
    /// </summary>
    internal class TypeName : ICodeGenerator
    {
        #region ==== 私有字段 ===

        /// <summary>
        /// 类型信息
        /// </summary>
        private string type = null;

        #endregion

        #region ==== 属性 ====

        public Using MyUsings
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">C#标准类型信息</param>
        public TypeName(string type)
        {
            this.type = type;
        }

        #endregion

        #region ==== 接口实现 ====

        #region ICodeGenerator 成员

        public void Write(TextWriter writer, IndentManager indent)
        {
            writer.Write(this.type);
            writer.Flush();
        }

        #endregion

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 返回当前类型的名称
        /// </summary>
        /// <returns>当前类型的名称</returns>
        public override string ToString()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                this.Write(writer, new IndentManager());
                writer.Flush();
                stream.Position = 0;

                StreamReader reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
        }

        #endregion
    }
}
