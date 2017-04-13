/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Alive.Foundation.Utilities.MsOffice.AccessWriterUtilities;

namespace Alive.Foundation.Utilities.MsOffice
{
    /// <summary>
    /// Access数据库写入工具
    /// </summary>
    public class AccessWriter : IDisposable
    {
        #region ==== 私有变量 ====

        /// <summary>
        /// 创建的数据库的名称
        /// </summary>
        private string dbFileName;

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的数据链接对象
        /// </summary>
        /// <param name="fileName">Access文件名</param>
        public AccessWriter(string fileName)
        {
            this.dbFileName = fileName;   
        }

        #endregion

        #region ==== 接口实现 ====

        #region IDisposable 成员

        public void Dispose()
        {
        }

        #endregion

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 向数据库写入数据
        /// </summary>
        /// <param name="colTypeDesign">表字段的设计</param>
        /// <param name="fillTable">填充表</param>
        public void WriteData(ColumnDesign colTypeDesign, Func<DataTable, bool> fillTable)
        {
            // 创建数据库
            using (AccessBuilder builder = new AccessBuilder(dbFileName))
            {
                builder.CreateTable(colTypeDesign);
            }

            // 写入数据
            if (File.Exists(this.dbFileName))
            {
                using (var connection = new OleDbConnection(string.Format(
                    Properties.Resources.AccessConnStringFormat,
                    this.dbFileName)))
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter(
                        string.Format("SELECT * FROM [{0}]", colTypeDesign.TableName),
                        connection);
                    OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                    DataTable dbTable = new DataTable();

                    adapter.Fill(dbTable);
                    builder.GetInsertCommand();

                    if (fillTable(dbTable))
                    {
                        adapter.Update(dbTable);
                    }
                }
            }
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 拷贝数据
        /// </summary>
        /// <param name="source">数据源表</param>
        /// <param name="aim">等待写入数据的表</param>
        private void CopyData(DataTable source, DataTable aim)
        {
            foreach (DataRow row  in source.Rows)
            {
                DataRow newRow = aim.NewRow();

                foreach (DataColumn column in source.Columns)
                {
                    newRow[column] = row[column];
                }

                aim.Rows.Add(newRow);
            }
        }

        #endregion
    }
}
