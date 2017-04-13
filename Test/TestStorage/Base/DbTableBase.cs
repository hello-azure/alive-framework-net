using System.Collections.Generic;
using System.Configuration;
using Alive.Foundation.Data;
using System;
using TestData;
using System.Linq.Expressions;

namespace TestStorage.Base
{
    public abstract class DbTableBase
    {
        #region ==== 抽象方法 ====

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="t1"></param>
        /// <returns>true:成功 false:失败</returns>
        public abstract bool Add<T>(T t)
            where T : BusinessObject, new();

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="t1"></param>
        /// <returns>true:成功 false:失败</returns>
        public abstract bool Add<T1, T2>(T1 t1, T2 t2)
            where T1 : BusinessObject, new()
            where T2 : BusinessObject, new();

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="dataCondition">T表操作业务数据及条件</param>
        /// <returns>true:成功 false:失败</returns>
        public abstract bool Update<T>(DataCondition<T> dataCondition)
            where T : BusinessObject, new();

        /// <summary>
        /// 向表中插入数据
        /// </summary>
        /// <param name="dataCondition1">T1表操作业务数据及条件</param>
        /// <param name="dataCondition2">T2表操作业务数据及条件</param>
        /// <returns>true:成功 false:失败</returns>
        public abstract bool Update<T1, T2>(DataCondition<T1> dataCondition1, DataCondition<T2> dataCondition2)
            where T1 : BusinessObject, new()
            where T2 : BusinessObject, new();

        /// <summary>
        /// 移除T表指定数据,支持事务操作
        /// </summary>
        /// <param name="condition">T表Where条件表达式树</param>
        /// <returns>true:成功 false:失败</returns>
        public abstract bool Remove<T>(Expression<Func<T, bool>> condition)
            where T : BusinessObject, new();

        /// <summary>
        /// 移除T1,T2表指定数据,支持事务操作
        /// </summary>
        /// <param name="condition1">T1表Where条件表达式树</param>
        /// <param name="condition2">T2表Where条件表达式树</param>
        /// <returns>true:成功 false:失败</returns>
        public abstract bool Remove<T1, T2>(Expression<Func<T1, bool>> condition1, Expression<Func<T2, bool>> condition2)
            where T1 : BusinessObject, new()
            where T2 : BusinessObject, new();

        /// <summary>
        /// 根据指定条件获得数据
        /// </summary>
        /// <param name="condition">Where条件表达式树</param>
        /// <returns>T表指定数据</returns>
        public abstract DataResult<BoList<T>> Select<T>(Expression<Func<T, bool>> condition)
            where T : BusinessObject,new();

        #endregion
    }

    public class DataCondition<T> where T:BusinessObject
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// T表业务数据
        /// </summary>
        public T Data
        {
            get;
            private set;
        }

        /// <summary>
        /// Where条件表达式树
        /// </summary>
        public Expression<Func<T, bool>> Condition
        {
            get;
            private set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">T表业务数据</param>
        /// <param name="condition">Where条件表达式树</param>
        public DataCondition(T data, Expression<Func<T, bool>> condition)
        {
            this.Data = data;
            this.Condition = condition;
        }

        #endregion
    }

}
