/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.IO;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Common;
using Alive.Tools.CodeGenerator.Foundatation.Metadata;
using Alive.Tools.CodeGenerator.Foundatation.Generator.Templates;
using Alive.Foundation.Data;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators
{
    public abstract class FileGenerator
    {
        #region ==== 属性 ====

        /// <summary>
        /// 活儿或设置输出路径
        /// </summary>
        public string OutputPath
        {
            get;
            set;
        }

        /// <summary>
        /// 获得或设置文件名
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源类型
        /// </summary>
        public SourceType SourceType
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public TableInfo Source
        {
            get;
            set;
        }

        /// <summary>
        /// 全部数据源
        /// </summary>
        public TableInfoList Sources
        {
            get;
            set;
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 创建文件
        /// </summary>
        public void Generate()
        {
            string fullFileName = Path.Combine(this.OutputPath, this.FileName);

            if (!Directory.Exists(this.OutputPath))
            {
                Directory.CreateDirectory(this.OutputPath);
            }

            FileMode mode = File.Exists(fullFileName) ? FileMode.Truncate : FileMode.Create;

            using (FileStream file = new FileStream(fullFileName, mode))
            {
                ICodeGenerator generator = this.CreateGenerator();
                generator.Write(new StreamWriter(file), new IndentManager());
            }
        }

        #endregion

        #region ==== 受保护方法 ====

        protected abstract ICodeGenerator CreateGenerator();

        #endregion
    }
}
