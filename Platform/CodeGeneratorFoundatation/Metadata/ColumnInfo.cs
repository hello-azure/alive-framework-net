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
    /// 列信息
    /// </summary>
    public class ColumnInfo:BusinessObject
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// 字段名称
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
        public StringField Type
        {
            get
            {
                return this.GetField<StringField>("Type");
            }
            set
            {
                this.GetField<StringField>("Type").Value = value.Value;
            }
        }

        /// <summary>
        /// 字段长度
        /// </summary>
        public IntField Length
        {
            get
            {
                return this.GetField<IntField>("Length");
            }
            set
            {
                this.GetField<IntField>("Length").Value = value.Value;
            }
        }

        #endregion
    }
}
