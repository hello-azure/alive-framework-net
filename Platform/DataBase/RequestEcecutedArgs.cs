/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alive.Foundation.Storage
{
    /// <summary>
    /// 数据请求事件
    /// </summary>
    public class RequestEcecutedArgs : EventArgs
    {
        #region====私有字段====

        /// <summary>
        /// 获取当前数据库操作请求结果
        /// </summary>
        private readonly DbResult result;

        #endregion

        #region====属性====

        /// <summary>
        /// 获取当前数据库操作请求结果
        /// </summary>
        public DbResult Result
        {
            get { return result; }
        }

        #endregion

        #region====构造函数====

        /// <summary>
        /// 初始化当前数据库操作请求结果
        /// </summary>
        /// <param name="result">设置数据库操作请求结果</param>
        public RequestEcecutedArgs(DbResult result)
        {
            this.result = result;
        }

        #endregion
    }
}
