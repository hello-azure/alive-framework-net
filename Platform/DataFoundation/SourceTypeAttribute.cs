/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// 数据源类型属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class SourceTypeAttribute:Attribute
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// 数据源类型
        /// </summary>
        public SourceType Type
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">数据源类型</param>
        public SourceTypeAttribute(SourceType type)
        {
            this.Type = type;
        }

        #endregion
    }
}
