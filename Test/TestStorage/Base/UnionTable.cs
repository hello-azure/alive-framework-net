using System;
using System.Collections.Generic;
using Alive.Foundation.Data;
using System.Linq.Expressions;
using TestData;
using Alive.Foundation.Storage;

namespace TestStorage.Base
{
    //class UnionTable:DbTableBase
    //{
    //    public override bool Add<T>(T t)
    //    {
    //        using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
    //        {
    //            NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

    //            var sql = new StringBuilder();
    //            sql.Append("insert into UserInfo(");
    //            sql.Append("ID,UserName,Password,RoleID");
    //            sql.Append(") values (");
    //            sql.Append("@ID,@UserName,@Password,@RoleID");
    //            sql.Append(") ");

    //            action.SQL = sql.ToString();
    //            action.SetParameter("@ID", (t as UserInfo).ID.Value);
    //            action.SetParameter("@UserName", (t as UserInfo).UserName.Value);
    //            action.SetParameter("@Password", (t as UserInfo).Password.Value);
    //            action.SetParameter("@RoleID", (t as UserInfo).RoleID.Value);

    //            DbResult result = action.Execute();

    //            return result.IsSuccessful;
    //        }
    //    }

    //    public override bool Add<T1, T2>(T1 t1, T2 t2)
    //    {
    //        using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
    //        {
    //            NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

    //            var sql = new StringBuilder();
    //            sql.Append("insert into UserInfo(");
    //            sql.Append("ID,UserName,Password,RoleID");
    //            sql.Append(") values (");
    //            sql.Append("@ID,@UserName,@Password,@RoleID");
    //            sql.Append(") ");

    //            action.SQL = sql.ToString();
    //            action.SetParameter("@ID", (t1 as UserInfo).ID.Value);
    //            action.SetParameter("@UserName", (t1 as UserInfo).UserName.Value);
    //            action.SetParameter("@Password", (t1 as UserInfo).Password.Value);
    //            action.SetParameter("@RoleID", (t1 as UserInfo).RoleID.Value);

    //            DbResult result1 = action.Execute();

    //            sql = new StringBuilder();
    //            sql.Append("insert into UserInfo(");
    //            sql.Append("ID,UserName,Password,RoleID");
    //            sql.Append(") values (");
    //            sql.Append("@ID,@UserName,@Password,@RoleID");
    //            sql.Append(") ");

    //            action.SQL = sql.ToString();
    //            action.SetParameter("@ID", (t2 as UserInfo).ID.Value);
    //            action.SetParameter("@UserName", (t2 as UserInfo).UserName.Value);
    //            action.SetParameter("@Password", (t2 as UserInfo).Password.Value);
    //            action.SetParameter("@RoleID", (t2 as UserInfo).RoleID.Value);

    //            DbResult result2 = action.Execute();

    //            return result1.IsSuccessful && result2.IsSuccessful;
    //        }
    //    }

    //    public override bool Update<T>(DataCondition<T> dataCondition)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool Update<T1, T2>(DataCondition<T1> dataCondition1, DataCondition<T2> dataCondition2)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool Remove<T>(System.Linq.Expressions.Expression<Func<T, bool>> condition)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool Remove<T1, T2>(System.Linq.Expressions.Expression<Func<T1, bool>> condition1, System.Linq.Expressions.Expression<Func<T2, bool>> condition2)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Alive.Foundation.Data.DataResult<Alive.Foundation.Data.BoList<T>> Select<T>(System.Linq.Expressions.Expression<Func<T, bool>> condition)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class test
    {
        public void A()
        { 
          
        }
    }

    public static class DbHelper
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 数据访问表字典
        /// </summary>
        private static readonly Dictionary<TableName, IBaseTable> tables = new Dictionary<TableName, IBaseTable>();

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        static DbHelper()
        {
            var types = typeof(DbHelper).Assembly.GetTypes();

            if (types.Length > 0)
            {
                foreach (var t in types)
                {
                    var indexModeAttributes = t.GetCustomAttributes(typeof(TableAttribute), true);

                    if (indexModeAttributes.Length > 0)
                    {
                        tables.Add((indexModeAttributes[0] as TableAttribute).Name,
                              (IBaseTable)typeof(DbHelper).Assembly.CreateInstance(t.FullName, true));
                    }
                }
            }
        }

        #endregion

        #region ==== 公共方法 ====

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="t1"></param>
        /// <returns>true:成功 false:失败</returns>
        public static bool Add<T>(T t)
            where T : BusinessObject, new()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                try
                {
                    return tables[(TableName)Enum.Parse(typeof(TableName), t.GetType().Name, true)].Add(action, t).IsSuccessful;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="t1"></param>
        /// <returns>true:成功 false:失败</returns>
        public static bool Add<T>(BoList<T> t)
            where T : BusinessObject, new()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                try
                {
                    return tables[(TableName)Enum.Parse(typeof(TableName), t.GetType().Name, true)].Add(action, t as IBoList).IsSuccessful;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="t1"></param>
        /// <returns>true:成功 false:失败</returns>
        public static bool Add<T1, T2>(T1 t1, T2 t2)
            where T1 : BusinessObject, new()
            where T2 : BusinessObject, new()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                try
                {
                    return 
                        tables[(TableName)Enum.Parse(typeof(TableName), t1.GetType().Name, true)].Add(action, t1).IsSuccessful
                        &&
                        tables[(TableName)Enum.Parse(typeof(TableName), t2.GetType().Name, true)].Add(action, t2).IsSuccessful;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="t1"></param>
        /// <returns>true:成功 false:失败</returns>
        public static bool Add<T1, T2>(BoList<T1> t1, BoList<T2> t2)
            where T1 : BusinessObject, new()
            where T2 : BusinessObject, new()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                try
                {
                    return
                        tables[(TableName)Enum.Parse(typeof(TableName), t1.GetType().Name, true)].Add(action, t1 as IBoList).IsSuccessful
                        &&
                        tables[(TableName)Enum.Parse(typeof(TableName), t2.GetType().Name, true)].Add(action, t2 as IBoList).IsSuccessful;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="dataCondition">T表操作业务数据及条件</param>
        /// <returns>true:成功 false:失败</returns>
        public static bool Update<T>(DataCondition<T> dataCondition)
            where T : BusinessObject, new()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                try
                {
                    return 
                        tables[(TableName)Enum.Parse(typeof(TableName), dataCondition.Data.GetType().Name, true)].Update(
                        action, 
                        dataCondition.Data, 
                        new SqlWhereExpressionBuilder<T>().BuildWhereExpression(dataCondition.Condition).WhereExpression).IsSuccessful;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="dataCondition1">T1表操作业务数据及条件</param>
        /// <param name="dataCondition2">T2表操作业务数据及条件</param>
        /// <returns>true:成功 false:失败</returns>
        public static bool Update<T1, T2>(DataCondition<T1> dataCondition1, DataCondition<T2> dataCondition2)
            where T1 : BusinessObject, new()
            where T2 : BusinessObject, new()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                try
                {
                    return 
                        tables[(TableName)Enum.Parse(typeof(TableName), dataCondition1.Data.GetType().Name, true)].Update(
                        action,
                        dataCondition1.Data,
                        new SqlWhereExpressionBuilder<T1>().BuildWhereExpression(dataCondition1.Condition).WhereExpression).IsSuccessful 
                        &&
                        tables[(TableName)Enum.Parse(typeof(TableName), dataCondition2.Data.GetType().Name, true)].Update(
                        action,
                        dataCondition2.Data,
                        new SqlWhereExpressionBuilder<T2>().BuildWhereExpression(dataCondition2.Condition).WhereExpression).IsSuccessful;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 移除T表指定数据,支持事务操作
        /// </summary>
        /// <param name="condition">T表Where条件表达式树</param>
        /// <returns>true:成功 false:失败</returns>
        public static bool Remove<T>(Expression<Func<T, bool>> condition)
            where T : BusinessObject, new()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                try
                {
                    return 
                        tables[(TableName)Enum.Parse(typeof(TableName), new T().GetType().Name, true)].Remove(
                        action,
                        new SqlWhereExpressionBuilder<T>().BuildWhereExpression(condition).WhereExpression).IsSuccessful;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 移除T1,T2表指定数据,支持事务操作
        /// </summary>
        /// <param name="condition1">T1表Where条件表达式树</param>
        /// <param name="condition2">T2表Where条件表达式树</param>
        /// <returns>true:成功 false:失败</returns>
        public static bool Remove<T1, T2>(Expression<Func<T1, bool>> condition1, Expression<Func<T2, bool>> condition2)
            where T1 : BusinessObject, new()
            where T2 : BusinessObject, new()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                NoneQueryRequest action = mainDb.NewAction<NoneQueryRequest>();

                try
                {
                    return
                        tables[(TableName)Enum.Parse(typeof(TableName), new T1().GetType().Name, true)].Remove(
                        action,
                        new SqlWhereExpressionBuilder<T1>().BuildWhereExpression(condition1).WhereExpression).IsSuccessful
                        &&
                        tables[(TableName)Enum.Parse(typeof(TableName), new T2().GetType().Name, true)].Remove(
                        action,
                        new SqlWhereExpressionBuilder<T2>().BuildWhereExpression(condition2).WhereExpression).IsSuccessful;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 根据指定条件获得数据
        /// </summary>
        /// <param name="condition">Where条件表达式树</param>
        /// <returns>T表指定数据</returns>
        public static DataResult<IBoList> Select<T>(Expression<Func<T, bool>> condition)
            where T : BusinessObject, new()
        {
            using (DbOperator mainDb = new DbOperator(DataBaseName.Main))
            {
                QueryRequest action = mainDb.NewAction<QueryRequest>();

                return tables[(TableName)Enum.Parse(typeof(TableName), new T().GetType().Name, true)].Select(
                    action,
                    new SqlWhereExpressionBuilder<T>().BuildWhereExpression(condition).WhereExpression);
            }
        }

        #endregion
    }
}
