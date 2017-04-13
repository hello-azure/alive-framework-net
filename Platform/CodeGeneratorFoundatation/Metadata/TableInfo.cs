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
using Alive.Foundation.Data;
using Alive.Foundation.Data.DataFields;

namespace Alive.Tools.CodeGenerator.Foundatation.Metadata
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo:BusinessObject
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// 表名称
        /// </summary>
        public StringField Name
        {
            get
            {
                return this.GetField<StringField>("Name");
            }
            set
            {
                this.GetField<StringField>("Name").Value = value.Value;
            }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public ColumnInfoList Columns
        {
            get;
            set;
        }

        /// <summary>
        /// 表类型
        /// </summary>
        public TableType Type
        {
            get;
            set;
        }

        #endregion
    }
}
