using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Alive.Foundation.Storage
{
    /// <summary>
    /// 数据库连接适配器
    /// </summary>
    internal class ConnectionAdapter : IDisposable
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 当前资源释放真假
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// 当前数据库连接对象
        /// </summary>
        private DbConnection conn = null;

        /// <summary>
        /// 数据库操作事务对象
        /// </summary>
        private DbTransaction transaction = null;

        #endregion

        #region ==== 公共属性 ====

        public DbConnection Connection
        {
            get { return this.conn; }
            private set { this.conn = value; }
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 私有构造函数，在ConnectionPool中被实例化
        /// </summary>
        private ConnectionAdapter()
        {

        }

        #endregion

        #region ==== 析构函数 ====

        /// <summary>
        /// 调用Finalize 方法和基类的 Finalize 方法
        /// </summary>
        ~ConnectionAdapter()
        {
            Dispose(false);
        }

        #endregion

        #region ==== 接口实现 ====

        #region IDisposable成员

        /// <summary>
        /// 释放当前对象所占用的资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放对象资源
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    //手动释放托管资源代码
                    ConnectionPool.Recycle(this);

                    if (this.transaction != null)
                    {
                        this.transaction.Dispose();
                        this.transaction = null;
                    }
                }
                isDisposed = true;
            }
        }

        #endregion

        #endregion

        #region ==== 内部方法 ====

        /// <summary>
        /// 为当前连接开始一个事务
        /// </summary>
        internal void BeginTransaction()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
            }

            this.transaction = conn.BeginTransaction();
        }

        /// <summary>
        /// 提交当前事务的所有操作
        /// </summary>
        internal void EndTransactoin()
        {
            this.transaction.Commit();
        }

        /// <summary>
        /// 回滚当前事务所有已经执行了的操作
        /// </summary>
        internal void Rollback()
        {
            this.transaction.Rollback();
        }

        /// <summary>
        /// 创建一个基于当前事务的SqlCommand对象
        /// </summary>
        internal IDbCommand CreateCommand()
        {
            IDbCommand result = this.conn.CreateCommand();
            result.Transaction = this.transaction;
            return result;
        }

        /// <summary>
        /// 获得一个ConnectionAdapter的实例,并打开数据库连接
        /// </summary>
        /// <param name="dbName">要访问的数据库标识符</param>
        /// <returns>ConnectionAdapter实例</returns>
        internal static ConnectionAdapter GetConnection(DataBaseName dbName)
        {
            return ConnectionPool.GetConnection(dbName);
        }

        /// <summary>
        /// 获得一个ConnectionAdapter的实例,并打开数据库连接
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="connection">数据库连接字符串</param>
        /// <returns>ConnectionAdapter实例</returns>
        internal static ConnectionAdapter GetConnection(string dbType,string connection)
        {
            return ConnectionPool.GetConnection(dbType,connection);
        }

        #endregion

        #region ==== 类型定义 ====

        /// <summary>
        /// 数据库连接池
        /// </summary>
        private class ConnectionPool
        {
            #region ==== 属性 ====

            /// <summary>
            /// 获得Main数据库的连接字符串
            /// </summary>
            /// <remarks>
            /// 连接字符串按照.net标准格式配置在应用程序（测试程序或产品服务器程序）的配置文件中。
            /// </remarks>
            private static string MainConnectionString
            {
                get
                {
                    return ConfigurationManager.ConnectionStrings["Main"].ConnectionString;                    
                }
            }

            #endregion

            #region ==== 内部方法 ====

            /// <summary>
            /// 获得一个可用的数据库连接
            /// </summary>
            /// <param name="dbName">要访问的数据库标识符</param>
            /// <returns>数据库连接适配器对象</returns>
            internal static ConnectionAdapter GetConnection(DataBaseName dbName)
            {
                // 处理连接字符串
                string connStr = string.Empty;

                switch (dbName)
                {
                    case DataBaseName.Main:
                        connStr = MainConnectionString;
                        break;
                    default:
                        break;
                }

                ConnectionAdapter connectionAdapter = new ConnectionAdapter();
                connectionAdapter.conn = CreateConnection(ConfigurationManager.AppSettings["DbType"].ToString());

                try
                {
                    switch (connectionAdapter.conn.State)
                    {
                        case ConnectionState.Open:
                            break;
                        default:
                            connectionAdapter.conn.ConnectionString = connStr;
                            connectionAdapter.conn.Open();
                            break;
                    }
                }
                catch (DbConnectionException ex)
                {
                    throw ex;
                }

                return connectionAdapter;
            }

            /// <summary>
            /// 获得一个可用的数据库连接
            /// </summary>
            /// <param name="dbType">数据库类型</param>
            /// <param name="connection">数据库连接字符串</param>
            /// <returns>数据库连接适配器对象</returns>
            internal static ConnectionAdapter GetConnection(string dbType,string connection)
            {                
                ConnectionAdapter connectionAdapter = new ConnectionAdapter();
                connectionAdapter.conn = CreateConnection(dbType);

                try
                {
                    switch (connectionAdapter.conn.State)
                    {
                        case ConnectionState.Open:
                            break;
                        default:
                            connectionAdapter.conn.ConnectionString = connection;
                            connectionAdapter.conn.Open();
                            break;
                    }
                }
                catch (DbConnectionException ex)
                {
                    throw ex;
                }

                return connectionAdapter;
            }

            /// <summary>
            /// 根据数据库类型创建数据库连接
            /// </summary>
            /// <param name="dbType">数据库类型</param>
            /// <returns>数据库连接</returns>
            protected static DbConnection CreateConnection(string dbType)
            {  
                switch (dbType.ToUpper())
                {
                    case "SQLSERVER":
                        return new SqlConnection();
                        break;
                    case "ORACLE":
                        return new OracleConnection();
                        break;
                    case "OLEDB":
                        return new OleDbConnection();
                        break;
                    default:
                        throw (new DbTypeException());
                        break;
                }                
            }

            /// <summary>
            /// 回收一个数据库连接
            /// </summary>
            /// <param name="conn">数据库连接适配器实例</param>
            internal static void Recycle(ConnectionAdapter connection)
            {
                if (connection.conn != null)
                {
                    switch (connection.conn.State)
                    {
                        case ConnectionState.Closed:
                            break;
                        default:
                            connection.conn.Close();
                            break;
                    }
                }
            }

            #endregion
        }

        #endregion
    }
}
