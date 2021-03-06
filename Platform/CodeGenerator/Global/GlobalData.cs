﻿/***********
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
using Alive.Tools.CodeGenerator.Foundatation.Metadata;

namespace Alive.Tools.CodeGenerator
{
    /// <summary>
    /// 全局数据缓存
    /// </summary>
    public static class GlobalData
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// 数据源
        /// </summary>
        public static IDataSource DataSource
        {
            get;
            set;
        }

        #endregion
    }
}
