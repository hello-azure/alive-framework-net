/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

namespace Alive.Foundation.Data
{
    /// <summary>
    /// SQL Server Where表达式树构建器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SqlWhereExpressionBuilder<T> : WhereExpressionBuilder<T>
        where T : BusinessObject
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlWhereExpressionBuilder()
            : base()
        { }

        #endregion
    }
}
