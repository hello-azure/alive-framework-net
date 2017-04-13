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
using Alive.Foundation.Data;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// SQL构建器基类
    /// </summary>
    internal abstract class SqlBuilderFactory
    {
        #region ==== 抽象属性 ====

        /// <summary>
        /// 插入器:创建insert...
        /// </summary>
        internal abstract IAddBuilder Insertor
        {
            get;
        }

        /// <summary>
        /// 更新器:创建update...
        /// </summary>
        internal abstract IUpdateBuilder Updator
        {
            get;
        }

        /// <summary>
        /// 删除器:创建delete...
        /// </summary>
        internal abstract IRemoveBuilder Deleter
        {
            get;
        }

        /// <summary>
        /// 查询器:创建select...
        /// </summary>
        internal abstract ISelectBuilder Selector
        {
            get;
        }

        #endregion
       
    }
}
