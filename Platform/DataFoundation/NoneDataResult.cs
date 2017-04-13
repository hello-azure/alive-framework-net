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
    /// 无数据返回的数据访问执行结果
    /// </summary>
    public class NoneDataResult
    {
        #region ==== 属性 ====

        /// <summary>
        /// 获得一个值,表示本次数据访问执行操作是否成功
        /// </summary>
        public bool IsSuccessful
        {
            get
            {
                return string.IsNullOrEmpty(this.ErrorCode);
            }
        }

        /// <summary>
        /// 获得或设置本次数据访问操作的错误代码
        /// </summary>
        public string ErrorCode
        {
            get;
            set;
        }

        #endregion
    }
}
