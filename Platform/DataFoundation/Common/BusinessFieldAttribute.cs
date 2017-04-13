/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// 标识业务字段的属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class BusinessFieldAttribute : Attribute
    {
          #region ==== 属性 ====

        /// <summary>
        /// 开发者姓名
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个业务字段标签
        /// </summary>
        /// <param name="name">业务名称</param>
        public BusinessFieldAttribute(string name)
        {
            this.Name = name;
        }

        #endregion
  }
}
