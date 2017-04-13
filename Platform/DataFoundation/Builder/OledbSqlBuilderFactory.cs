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
    /// OLEDB-SQL构建器
    /// </summary>
    [SourceTypeAttribute(SourceType.OLEDB)]
    internal class OledbSqlBuilderFactory:SqlBuilderFactory
    {
        #region ==== 接口实现 ====

        internal override IAddBuilder Insertor
        {
            get { return new OledbAddBuilder(); }
        }

        internal override IUpdateBuilder Updator
        {
            get { return new OledbUpdateBuilder(); }
        }

        internal override IRemoveBuilder Deleter
        {
            get { return new OledbRemoveBuilder(); }
        }

        internal override ISelectBuilder Selector
        {
            get { return new OledbSelectBuilder(); }
        }

        #endregion
    }
}
