/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System.Collections.Generic;

namespace Alive.Foundation.Utilities.MsOffice
{
    /// <summary>
    /// 字段 名—属性 字典
    /// </summary>
    public class ColumnDesign : Dictionary<string, ColumnDesignItem>
    {
        #region ==== 属性 ====

        /// <summary>
        /// 设置、获得字段所在的表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        #endregion
    }

    /// <summary>
    /// 字段属性设置
    /// </summary>
    public struct ColumnDesignItem
    {
        #region ==== 公有变量 ====

        /// <summary>
        /// 字段数据类型
        /// </summary>
        public DbDataType Type;

        /// <summary>
        /// 数据字段大小占用最大值
        /// </summary>
        public int Size;

        /// <summary>
        /// 该字段是否为必填
        /// </summary>
        public bool IsRequired;

        /// <summary>
        /// 该字段是否为主键
        /// </summary>
        public bool IsPK;

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 字段设计
        /// </summary>
        /// <param name="type">字段数据类型</param>
        /// <param name="size">字段大小占用最大值</param>
        public ColumnDesignItem(DbDataType type, int size)
            : this(type, size, false)
        { }

        /// <summary>
        /// 字段设计
        /// </summary>
        /// <param name="type">字段数据类型</param>
        /// <param name="size">字段大小占用最大值</param>
        /// <param name="isRequired">字段是否为必填</param>
        public ColumnDesignItem(DbDataType type, int size, bool isRequired)
            : this(type, size, isRequired, false)
        { }

        /// <summary>
        /// 字段设计
        /// </summary>
        /// <param name="type">字段数据类型</param>
        /// <param name="size">字段大小占用最大值</param>
        /// <param name="isRequired">字段是否为必填</param>
        /// <param name="isPK">是否为主键</param>
        public ColumnDesignItem(DbDataType type, int size, bool isRequired, bool isPK)
        {
            this.Type = type;
            this.Size = size;
            this.IsRequired = isRequired;
            this.IsPK = isPK;
        }

        #endregion
    }
}
