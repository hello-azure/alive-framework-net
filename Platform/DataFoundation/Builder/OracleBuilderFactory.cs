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
    /// ORACLE-SQL构建器
    /// </summary>
    [SourceTypeAttribute(SourceType.ORACLE)]
    internal class OracleBuilderFactory:SqlBuilderFactory
    {
        #region ==== 接口实现 ====

        internal override IAddBuilder Insertor
        {
            get { return new OracleAddBuilder(); }
        }

        internal override IUpdateBuilder Updator
        {
            get { return new OracleUpdateBuilder(); }
        }

        internal override IRemoveBuilder Deleter
        {
            get { return new OracleRemoveBuilder(); }
        }

        internal override ISelectBuilder Selector
        {
            get { return new OracleSelectBuilder(); }
        }

        #endregion
       
    }
}
