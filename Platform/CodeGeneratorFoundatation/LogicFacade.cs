/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alive.Foundation.Data;
using Alive.Foundation.Utilities.Log;
using Alive.Tools.CodeGenerator.Foundatation.Metadata;

namespace Alive.Tools.CodeGenerator.Foundatation
{
    /// <summary>
    /// 访问入口点
    /// </summary>
    public class LogicFacade
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 访问入口点实例
        /// </summary>
        private static readonly LogicFacade instance = new LogicFacade();

        /// <summary>
        /// 数据源字典
        /// </summary>
        private static readonly Dictionary<SourceType, IDbSource> sourceDic = new Dictionary<SourceType, IDbSource>();

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        static LogicFacade()
        {
            var types = typeof(LogicFacade).Assembly.GetTypes();

            if (types.Length > 0)
            {
                foreach (var t in types)
                {
                    var indexModeAttributes = t.GetCustomAttributes(typeof(SourceTypeAttribute), true);
                    
                    if (indexModeAttributes.Length > 0)
                    {
                        sourceDic.Add((indexModeAttributes[0] as SourceTypeAttribute).Type,
                                                      (IDbSource)typeof(LogicFacade).Assembly.CreateInstance(t.FullName, true));
                    }
                }
            }
        }

        #endregion

        #region ==== 公共属性 ====

        /// <summary>
        /// 访问入口点实例
        /// </summary>
        public static LogicFacade Instance
        {
            get 
            {
                return instance;
            }
        }

        #endregion

        #region ==== 公共方法 ====

        /// <summary>
        /// 获得数据源
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="connection">数据库连接</param>
        /// <returns>返回数据源</returns>
        public IDataSource GetData(SourceType type, string connection)
        {
            if (sourceDic.ContainsKey(type))
            {
                var result = sourceDic[type].GetData(type, connection);

                if (result.IsSuccessful)
                {
                    return result.ResultData;
                }

                GlobalLogger<LogicFacade>.Error(result.ErrorCode);
            }

            return null;
        }

        #endregion
    }
}
