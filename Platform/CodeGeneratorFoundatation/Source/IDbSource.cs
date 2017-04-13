/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using Alive.Tools.CodeGenerator.Foundatation.Metadata;
using Alive.Foundation.Data;

namespace Alive.Tools.CodeGenerator.Foundatation
{
    /// <summary>
    /// 数据源接口
    /// </summary>
    internal interface IDbSource
    {
        /// <summary>
        /// 获得数据库表信息数据
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="connection">数据库连接</param>
        /// <returns>返回查询数据库表信息集合</returns>
        DataResult<TableInfoList> GetData(SourceType type, string connection);
    }
}
