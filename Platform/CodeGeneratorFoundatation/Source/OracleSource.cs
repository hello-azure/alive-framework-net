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
using Alive.Tools.CodeGenerator.Foundatation.Metadata;
using Alive.Foundation.Data;

namespace Alive.Tools.CodeGenerator.Foundatation
{
    /// <summary>
    /// Access数据库数据源
    /// </summary>
    [SourceTypeAttribute(SourceType.ORACLE)]
    internal class OracleSource:IDbSource
    {
        #region ====接口实现 ====

        public DataResult<TableInfoList> GetData(SourceType type, string connection)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
