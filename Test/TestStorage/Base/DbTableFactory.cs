/***********
 * 版权说明：
 *   本文件是 Somme 服务程序的一部分。
 *   版本：V 1.0
 *   Copyright 北京立安泰华电子科技有限公司 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using Alive.Foundation.Utilities.Log;
using TestData;

namespace TestStorage.Base
{
    /// <summary>
    /// 数据表操作工厂
    /// </summary>
    public static class DbTableFactory<T> where T : Alive.Foundation.Data.BusinessObject, new()
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 数据操作表字典
        /// </summary>
        private static Dictionary<TableName, IBaseTable> tableDic = new Dictionary<TableName, IBaseTable>();

        #endregion

        #region ==== 构造函数 ====

        static DbTableFactory()
        {
            //GlobalLogger<DbTableFactory>.Info("DbTableFactory：加载表操作实例");

            //var types = typeof(DbTableFactory).Assembly.GetTypes();

            //if (types.Length > 0)
            //{
            //    foreach (var t in types)
            //    {
            //        var attributes = t.GetCustomAttributes(typeof(TableAttribute), true);

            //        if (attributes.Length > 0)
            //        {
            //            TableName name = (attributes[0] as TableAttribute).Name;
            //            DbTableBase instance = (DbTableBase)typeof(DbTableFactory).Assembly.CreateInstance(t.FullName, true);
            //            tableDic.Add(name, instance);
            //        }
            //    }
            //}
        }

        #endregion

        #region ==== 公共方法 ====

        /// <summary>
        /// 创建操作表的实例
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>操作表实例</returns>
        internal static IBaseTable Create(T t)
        {
            return null;
        }

        #endregion
    }
}
