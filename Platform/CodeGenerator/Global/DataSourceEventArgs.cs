/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alive.Tools.CodeGenerator.Foundatation.Metadata;


namespace Alive.Tools.CodeGenerator
{
    public class DataSourceEventArgs:EventArgs
    {
          #region ==== 公有属性 ====

        /// <summary>
        /// 数据源
        /// </summary>
        public TableInfoList Tables
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tables">数据源</param>
        public DataSourceEventArgs(TableInfoList tables)
        {
            this.Tables = tables;
        }

        #endregion
    }
}
