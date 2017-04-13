/***********
 * 版权说明：
 *   本文件是 Somme 服务程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alive.Foundation.Data;
using TestData;
using Alive.Foundation.Data.DataFields;


namespace TestStorage.Common
{
    /// <summary>
    /// 基础表
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public abstract class BaseTable<TData> : IBaseTable where TData : BusinessObject
    {

        #region ==== 属性 ====

        /// <summary>
        /// 操作表名
        /// </summary>
        protected abstract TableName TableName
        {
            get;
        }

        #endregion

        #region ==== 公共方法 ====

        /// <summary>
        /// 向表中插入一条数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Insert(TData data)
        {
            return false;
        }


        /// <summary>
        /// 向表中更新一条数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Update(TData data)
        {
            return false;
        }

        /// <summary>
        /// 删除表中一条数据
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Delete(Dictionary<Func<string>,DataField> set)
        {
            return false;
        }

        /// <summary>
        /// 获得表中全部数据
        /// </summary>
        /// <returns>获得全部数据</returns>
        public List<TData> GetData()
        {
            return null;
        }

        /// <summary>
        /// 获得表中全部数据
        /// </summary>
        /// <returns>获得全部数据</returns>
        public List<TData> GetData(Action<TData> filter)
        {
            return null;
        }

        #endregion
    }
}
