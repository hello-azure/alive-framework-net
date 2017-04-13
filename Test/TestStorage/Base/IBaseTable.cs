/***********
 * 版权说明：
 *   本文件是 Somme 服务程序的一部分。
 *   版本：V 1.0
 *   Copyright 北京立安泰华电子科技有限公司 2013 保留一切权利
 *   
 */

using Alive.Foundation.Data;
using Alive.Foundation.Storage;

namespace TestStorage.Base
{
    /// <summary>
    /// 数据表操作基类接口
    /// </summary>
    internal interface IBaseTable
    {
        /// <summary>
        /// 向数据库表中添加记录
        /// </summary>
        /// <param name="action">数据请求</param>
        /// <param name="data">业务数据</param>
        /// <returns>数据库执行结果</returns>
        DbResult Add(NoneQueryRequest action, BusinessObject data);

        /// <summary>
        /// 向数据库表中添加多条记录
        /// </summary>
        /// <param name="action">数据请求</param>
        /// <param name="datas">业务数据集合</param>
        /// <returns>数据库执行结果</returns>
        DbResult Add(NoneQueryRequest action, IBoList datas);

        /// <summary>
        /// 向数据库表中更新记录
        /// </summary>
        /// <param name="action">数据请求</param>
        /// <param name="data">业务数据</param>
        /// <param name="whereCondition">Where子句执行条件</param>
        /// <returns>数据库执行结果</returns>
        DbResult Update(NoneQueryRequest action, BusinessObject data, string whereCondition);

        /// <summary>
        /// 删除数据库表记录
        /// </summary>
        /// <param name="action">数据请求</param>
        /// <param name="whereCondition">Where子句执行条件</param>
        /// <returns>数据库执行结果</returns>
        DbResult Remove(NoneQueryRequest action, string whereCondition);

        /// <summary>
        /// 查询数据库表记录
        /// </summary>
        /// <param name="action">数据请求</param>
        /// <param name="whereCondition">Where子句执行条件</param>
        /// <returns>数据库执行结果</returns>
        DataResult<IBoList> Select(QueryRequest action, string whereCondition);
    }

    public class ATable : IBaseTable
    {


        #region IBaseTable Members

        public DbResult Add(NoneQueryRequest action, BusinessObject data)
        {
            throw new System.NotImplementedException();
        }

        public DbResult Add(NoneQueryRequest action, IBoList datas)
        {
            DbResult result = null;
            result.IsSuccessful
        }

        public DbResult Update(NoneQueryRequest action, BusinessObject data, string whereCondition)
        {
            throw new System.NotImplementedException();
        }

        public DbResult Remove(NoneQueryRequest action, string whereCondition)
        {
            throw new System.NotImplementedException();
        }

        public DataResult<IBoList> Select(QueryRequest action, string whereCondition)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }

    //public class BTable : IBaseTable
    //{

    //    #region IBaseTable Members

    //    public void Add(NoneQueryRequest action, BusinessObject data, out StringBuilder sql, out DbResult result)
    //    {
    //        sql = new StringBuilder();
    //        sql.Append("insert into UserInfo(");
    //        sql.Append("ID,UserName,Password,RoleID");
    //        sql.Append(") values (");
    //        sql.Append("@ID,@UserName,@Password,@RoleID");
    //        sql.Append(") ");

    //        action.SQL = sql.ToString();
    //        action.SetParameter("@ID", (data as UserInfo).ID.Value);
    //        action.SetParameter("@UserName", (data as UserInfo).UserName.Value);
    //        action.SetParameter("@Password", (data as UserInfo).Password.Value);
    //        action.SetParameter("@RoleID", (data as UserInfo).RoleID.Value);

    //        result = action.Execute();
    //    }

    //    #endregion
    //}

}
