/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Alive.Foundation.Storage.ParameterInfos
{
    /// <summary>
    /// 数据库操作的参数信息。
    /// </summary>
    internal class ParameterInfo
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 参数名称
        /// </summary>
        private string name;

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 设置或获得参数名称
        /// </summary>
        public string Name
        {
            get
            {
                return name.Substring(1);
            }
            set
            {
                this.name = FormateName(value);
            }
        }

        /// <summary>
        /// 设置或获得参数的数据类型。
        /// </summary>
        public DbType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 设置或获得参数的大小
        /// </summary>
        public int Size
        {
            get;
            set;
        }

        /// <summary>
        /// 设置或获得参数的值。
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        #endregion

        #region ==== 公有方法 ====

        public static string FormateName(string value)
        {
            string result = value;
            string dbType = ConfigurationManager.AppSettings["DbType"].ToString();

            switch (dbType.ToUpper())
            {
                case "ORACLE":
                    if (!value.StartsWith(":"))
                    {
                        result = ":" + value;
                    }
                    break;
                default:
                    if (!value.StartsWith("@"))
                    {
                        result = "@" + value;
                    }
                    break;
            }  
                    
            return result;
        }

        #endregion
    }
}
