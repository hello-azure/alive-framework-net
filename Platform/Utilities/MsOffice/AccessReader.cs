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

namespace Alive.Foundation.Utilities.MsOffice
{
    /// <summary>
    /// Access数据库读取工具
    /// </summary>
    public class AccessReader : IDisposable
    {
        #region ==== 属性 ====

        /// <summary>
        /// 获得数据连接
        /// </summary>
        protected OleDbConnection Connection
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的数据连接对象
        /// </summary>
        /// <param name="fileName">Access 文件名（包含路径）</param>
        public AccessReader(string fileName)
        {
            if (File.Exists(fileName))
            {
                this.Connection = new OleDbConnection(string.Format(
                    Properties.Resources.AccessConnStringFormat,
                    fileName));
            }
        }

        #endregion

        #region ==== 接口实现 ====

        #region IDisposable 成员

        public void Dispose()
        {
            if (this.Connection != null)
            {
                this.Connection.Close();
            }
        }

        #endregion

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="tableName">要读取的表</param>
        /// <returns>获得的数据集</returns>
        public DataTable ReadData(string tableName)
        {
            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(
                    string.Format("SELECT * FROM [{0}]", tableName),
                    this.Connection);

                DataSet result = new DataSet();

                adapter.Fill(result, tableName);

                return result.Tables[tableName];
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        #endregion
    }
}
