/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.Builders
{
    /// <summary>
    /// Where条件表达式树结果
    /// </summary>
    public sealed class WhereExpressionBuildResult
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// Where子句条件表达式
        /// </summary>
        public string WhereExpression
        { 
            get; 
            set; 
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        public WhereExpressionBuildResult() 
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="whereExpression">Where子句条件表达式</param>
        public WhereExpressionBuildResult(string whereExpression)
        {
            WhereExpression = whereExpression;
        }

        #endregion

        #region ==== 重写方法 ====

        /// <summary>
        /// 将 Where条件表达式树转换为where子句字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(WhereExpression);
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        #endregion
    }
}
