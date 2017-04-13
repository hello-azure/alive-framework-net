/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alive.Foundation.Data.DataFields
{
    /// <summary>
    /// bool型的字段值
    /// </summary>
    public class BoolField : DataFieldBase<bool>
    {
        #region       ====属性====
        /// <summary>
        /// 获得字段的默认值。
        /// </summary>
        public override bool Default
        {
            get { return false; }
        }
        #endregion
        #region    ====受保护的方法====
        /// <summary>
        ///  设置字段值的字符串格式
        /// </summary>
        /// <param name="text">要设置的字符串</param>
        /// <returns></returns>
        protected override bool SetValueText(string text)
        {
            return bool.Parse(text);
        }
        #endregion
    }
}
