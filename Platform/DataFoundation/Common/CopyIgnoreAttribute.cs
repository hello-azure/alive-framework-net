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
    /// 标识数据拷贝时忽略的指令
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CopyIgnoreAttribute : Attribute
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的测试签名的实例
        /// </summary>
        public CopyIgnoreAttribute()
        {
        }

        #endregion
 }
}
