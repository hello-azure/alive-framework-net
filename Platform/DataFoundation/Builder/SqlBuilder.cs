/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// SQL构建器
    /// </summary>
    public class SqlBuilder
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 当前程序数据库类型
        /// </summary>
        private static readonly SourceType type;

        /// <summary>
        /// 数据库类型字典
        /// </summary>
        private static Dictionary<string, SourceType> sourceTypeDict = new Dictionary<string, SourceType>();

        /// <summary>
        /// SQL创建工厂字典
        /// </summary>
        private static readonly Dictionary<SourceType, SqlBuilderFactory> buildFactoryDic = new Dictionary<SourceType, SqlBuilderFactory>();

        #endregion 

        #region ==== 构造函数 ====

          /// <summary>
        /// 构造函数
        /// </summary>
        static SqlBuilder()
        {
            sourceTypeDict.Clear();
            sourceTypeDict.Add("SQLSERVER", SourceType.SQLSERVER);
            sourceTypeDict.Add("OLEDB", SourceType.OLEDB);
            sourceTypeDict.Add("ORACLE", SourceType.ORACLE);

            var key = ConfigurationManager.AppSettings["DbType"].ToString();

            if (sourceTypeDict.ContainsKey(key))
            {
                type = sourceTypeDict[key];
            }

            var types = typeof(SqlBuilder).Assembly.GetTypes();

            if (types.Length > 0)
            {
                foreach (var t in types)
                {
                    var indexModeAttributes = t.GetCustomAttributes(typeof(SourceTypeAttribute), true);

                    if (indexModeAttributes.Length > 0)
                    {
                        buildFactoryDic.Add((indexModeAttributes[0] as SourceTypeAttribute).Type,
                              (SqlBuilderFactory)typeof(SqlBuilder).Assembly.CreateInstance(t.FullName, true));
                    }
                }
            }
        }

        #endregion

        #region ==== 公共方法 ====

        /// <summary>
        /// 获得数据库插入器代码
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="type">数据源类型</param>
        /// <param name="t">模板对象</param>
        /// <returns>SQL:INSERT</returns>
        public static string GetInsertor<T>(T t) where T : BusinessObject, new()
        {
            if (!buildFactoryDic.Keys.Contains(type))
            {
                return null;
            }

            return buildFactoryDic[type].Insertor.Build<T>(t);
        }

        /// <summary>
        /// 获得数据库更新器代码
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="type">数据源类型</param>
        /// <param name="t">模板对象</param>
        /// <returns>SQL:UPDATE</returns>
        public static string GetUpdator<T>(T t) where T : BusinessObject, new()
        {
            if (!buildFactoryDic.Keys.Contains(type))
            {
                return null;
            }

            return buildFactoryDic[type].Updator.Build<T>(t);
        }

        /// <summary>
        /// 获得数据库更新器代码
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="type">数据源类型</param>
        /// <param name="t">模板对象</param>
        /// <param name="whereCondition">WHERE子句条件</param>
        /// <returns>SQL:UPDATE</returns>
        public static string GetUpdator<T>(T t, string whereCondition) where T : BusinessObject, new()
        {
            if (!buildFactoryDic.Keys.Contains(type))
            {
                return null;
            }

            return buildFactoryDic[type].Updator.Build<T>(t) + whereCondition;
        }


        /// <summary>
        /// 获得数据库删除器代码
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="type">数据源类型</param>
        /// <param name="t">模板对象</param>
        /// <returns>SQL:DELETE</returns>
        public static string GetDeleter<T>(T t) where T : BusinessObject, new()
        {
            if (!buildFactoryDic.Keys.Contains(type))
            {
                return null;
            }

            return buildFactoryDic[type].Deleter.Build<T>(t);
        }

        /// <summary>
        /// 获得数据库删除器代码
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="type">数据源类型</param>
        /// <param name="t">模板对象</param>
        /// <param name="whereCondition">WHERE子句条件</param>
        /// <returns>SQL:DELETE</returns>
        public static string GetDeleter<T>(T t, string whereCondition) where T : BusinessObject, new()
        {
            if (!buildFactoryDic.Keys.Contains(type))
            {
                return null;
            }

            return buildFactoryDic[type].Deleter.Build<T>(t) + whereCondition;
        }

        /// <summary>
        /// 获得数据库查询器代码
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="type">数据源类型</param>
        /// <param name="t">模板对象</param>
        /// <returns>SQL:SELECT</returns>
        public static string GetSelector<T>(T t) where T : BusinessObject, new()
        {
            if (!buildFactoryDic.Keys.Contains(type))
            {
                return null;
            }

            return buildFactoryDic[type].Selector.Build<T>(t);
        }

        /// <summary>
        /// 获得数据库查询器代码
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="type">数据源类型</param>
        /// <param name="t">模板对象</param>
        /// <param name="whereCondition">WHERE子句条件</param>
        /// <returns>SQL:SELECT</returns>
        public static string GetSelector<T>(T t, string whereCondition) where T : BusinessObject, new()
        {
            if (!buildFactoryDic.Keys.Contains(type))
            {
                return null;
            }

            return buildFactoryDic[type].Selector.Build<T>(t) + whereCondition;
        }

        #endregion
    }
}
