﻿/***********
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
using Alive.Foundation.Data;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// OLEDB-REMOVE（SQL）创建器的接口
    /// </summary>
    internal class OledbSelectBuilder:ISelectBuilder
    {
        #region ==== 公共方法 ====

        /// <summary>
        /// 基于指定实体表对象,创建SELECT（SQL）
        /// </summary>
        /// <typeparam name="T">继承BusinessObject的类型</typeparam>
        /// <param name="t">实体对象</param>
        /// <returns>SQL字符串</returns>
        public string Build<T>(T t) where T : BusinessObject, new()
        {
            var columns = t.GetNameMapping();

            if (columns == null || columns.Count <= 0)
            {
                return null;
            }

            var columnBuilder = new StringBuilder();

            for (int i = 0; i < columns.Count; i++)
            {
                columnBuilder.Append(string.Format("[{0}]", columns[i]));
                columnBuilder.Append(",");
            }

            var sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append(columnBuilder.ToString());
            sb.Append(" FROM ");
            sb.Append(t.GetType().Name);

            return sb.ToString().Replace(", ", " ");
        }

        #endregion

    }
}
