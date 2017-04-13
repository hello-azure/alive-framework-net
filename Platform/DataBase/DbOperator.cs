/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Alive.Foundation.Storage
{
    /// <summary>
    /// 数据库访问接口类
    /// </summary>
    public class DbOperator : IDisposable
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        /// connectionAdapter
        internal ConnectionAdapter connection;

        /// <summary>
        /// 取或设置当前资源释放真假
        /// </summary>
        private bool isDisposed;

        #endregion

        #region ==== 属性 ====

        public bool HasError
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 根据数据库创建一个新的数据库事务
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        public DbOperator(DataBaseName dbName)
        {
            this.HasError = false;

            switch (dbName)
            {
                case DataBaseName.Main:
                    this.connection = ConnectionAdapter.GetConnection(dbName);
                    break;
                default:
                    break;
            }

            if (this.connection != null)
            {
                this.connection.BeginTransaction();
            }
        }

        /// <summary>
        /// 根据数据库创建一个新的数据库事务
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="connection">数据库连接字符串</param>
        public DbOperator(string dbType, string connection)
        {
            this.HasError = false;

            this.connection = ConnectionAdapter.GetConnection(dbType, connection);

            if (this.connection != null)
            {
                this.connection.BeginTransaction();
            }
        }

        #endregion

        #region ==== 析构函数 ====

        /// <summary>
        /// 调用Finalize 方法和基类的 Finalize 方法
        /// </summary>
        ~DbOperator()
        {
            Dispose(false);
        }

        #endregion

        #region ==== 接口实现 ====

        #region IDisposeable成员

        /// <summary>
        /// 释放当前对象内容和占用的资源
        /// </summary>
        public void Dispose()
        {
            // TODO: HasError从来没有被赋为true！
            // TODO: 事务应该每次手工合并。
            if (!this.HasError)
            {
                this.connection.EndTransactoin();
            }
            else
            {
                this.connection.Rollback();
            }

            this.Dispose(true);
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
                }

                if (this.connection != null)
                {
                    this.connection.Dispose();
                }

                this.isDisposed = true;
            }
        }

        #endregion

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 获取一个当前事务的新的操作请求对象。
        /// </summary>
        /// <typeparam name="TRequest">TRequest：必须是DbRequest的一个子类。</typeparam>
        /// <returns>指定的操作对象的新的实例</returns>
        public TRequest NewAction<TRequest>() where TRequest : DbRequest, new()
        {
            TRequest req = new TRequest();
            req.Executed += new EventHandler<RequestEcecutedArgs>(RequestExecuted);
            req.Connection = this.connection;
            return req;

        }

        #endregion

        #region ==== 事件句柄 ====

        /// <summary>
        /// 处理当前数据库访问请求的执行结果
        /// </summary>
        /// <param name="sender">触发时间的数据库操作请求对象</param>
        /// <param name="e">数据库访问请求执行结果</param>
        private void RequestExecuted(object sender, RequestEcecutedArgs e)
        {
            this.HasError = this.HasError || !e.Result.IsSuccessful;
        }

        #endregion
    }
}
