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

namespace Alive.Tools.CodeGenerator.Foundatation.Builder
{
    /// <summary>
    /// 构建器类型
    /// </summary>
    internal enum BuildType
    {
        /// <summary>
        /// 基于数据库的Insert操作
        /// </summary>
        Add, 

        /// <summary>
        /// 基于数据库的Update操作
        /// </summary>
        Update, 

        /// <summary>
        /// 基于数据库的Delete操作
        /// </summary>
        Remove, 

        /// <summary>
        /// 基于数据库的查询操作
        /// </summary>
        Select
    }
}
