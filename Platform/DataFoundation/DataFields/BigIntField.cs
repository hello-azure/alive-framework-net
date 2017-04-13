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
    /// 64位整形字段    
    /// </summary>
    public class BigIntField : DataFieldBase<Int64>
    {
        #region ==== 属性 ====

        /// <summary>
        /// 获得字段的默认值。
        /// </summary>
        public override Int64 Default
        {
            get { return 0; }
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 设置字段值的字符串格式
        /// </summary>
        /// <param name="text">要设置字符串</param>
        protected override Int64 SetValueText(string text)
        {
            return Int64.Parse(text);
        }

        #endregion
    }
}
