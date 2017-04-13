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

namespace Alive.Foundation.Utilities.Events
{
    /// <summary>
    /// 属性值发生变化的事件参数
    /// </summary>
    public class SingleValueArgs<TValue> : EventArgs
    {
        #region ==== 属性 ====

        /// <summary>
        /// 变更的数值
        /// </summary>
        public TValue Value
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的参数
        /// </summary>
        public SingleValueArgs()
        {
            this.Value = default(TValue);
        }

        /// <summary>
        /// 创建一个指定变更数值的参数
        /// </summary>
        /// <param name="value">变更的数值</param>
        public SingleValueArgs(TValue value)
        {
            this.Value = value;
        }

        #endregion
    }
}
