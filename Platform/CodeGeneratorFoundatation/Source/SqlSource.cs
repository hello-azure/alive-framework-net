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
using Alive.Foundation.Storage;
using Alive.Foundation.Data;

namespace Alive.Tools.CodeGenerator.Foundatation
{
    /// <summary>
    /// SQLServer数据库数据源
    /// </summary>
    [SourceTypeAttribute(SourceType.SQLSERVER)]
    internal class SqlSource:IDbSource
    {
        #region ====接口实现 ====

        public DataResult<TableInfoList> GetData(SourceType type, string connection)
        {
            using (DbOperator mainDb = new DbOperator(type.ToString(),connection))
            {
                DataResult<TableInfoList> result = new DataResult<TableInfoList>();

                QueryRequest action = mainDb.NewAction<QueryRequest>();
                action.SQL = string.Format("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.Tables ORDER BY TABLE_TYPE,TABLE_NAME");
                DbResult dbResult = action.Execute();

                if (dbResult.IsSuccessful)
                {
                    dbResult.Injector.SetFieldMapping("Name", "TABLE_NAME");
                    TableInfoList tables = new TableInfoList();
                    dbResult.Injector.Inject(tables);
                    dbResult.Injector.Close();

                    if (tables != null && tables.Count >= 0)
                    {
                        foreach (var table in tables)
                        {
                            action.SQL = string.Format("SELECT COLUMN_NAME,DATA_TYPE,ORDINAL_POSITION FROM INFORMATION_SCHEMA.Columns WHERE TABLE_NAME='{0}' ORDER BY ORDINAL_POSITION", table.Name.Value);
                            dbResult = action.Execute();

                            if (dbResult.IsSuccessful)
                            {
                                dbResult.Injector.SetFieldMapping("Name", "COLUMN_NAME");
                                dbResult.Injector.SetFieldMapping("Type", "DATA_TYPE");
                                ColumnInfoList columns = new ColumnInfoList();
                                dbResult.Injector.Inject(columns);
                                dbResult.Injector.Close();
                                table.Columns = columns;
                            }
                        }
                    }

                    result.ResultData = tables;
                }

                return result;
            }
        }

        #endregion
    }
}
