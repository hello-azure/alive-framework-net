/***********
 * 版权说明：
 *   本文件是 Somme 服务程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestData;

namespace TestStorage.Base
{
    /// <summary>
    /// 表属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    internal class TableAttribute : Attribute
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// 操作的表名
        /// </summary>
        public TableName Name
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">表名</param>
        public TableAttribute(TableName name)
        {
            this.Name = name;
        }

        #endregion
    }
}
