/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Linq.Expressions;
using Alive.Foundation.Data;

namespace TestConsoleApp.Builders
{

    /// <summary>
    /// Where条件表达式树构建器接口
    /// </summary>
    /// <typeparam name="T">业务数据</typeparam>
    public interface IWhereExpressionBuilder<T>
        where T : BusinessObject
    {
        /// <summary>
        /// 构建Where条件表达式树结果
        /// </summary>
        /// <param name="expression">条件表达式树</param>
        /// <returns>Where条件表达式树结果</returns>
        WhereExpressionBuildResult BuildWhereExpression(Expression<Func<T, bool>> expression);
    }
}
