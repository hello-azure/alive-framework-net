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
using System.Linq.Expressions;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// 数据条件:用于设置数据访问的操作数据及条件
    /// </summary>
    public class DataCondition<T> where T : BusinessObject
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// T表业务数据
        /// </summary>
        public T Data
        {
            get;
            private set;
        }

        /// <summary>
        /// Where条件表达式树
        /// </summary>
        public Expression<Func<T, bool>> Condition
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">T表业务数据</param>
        /// <param name="condition">Where条件表达式树</param>
        public DataCondition(T data, Expression<Func<T, bool>> condition)
        {
            this.Data = data;
            this.Condition = condition;
        }

        #endregion
    }
}
