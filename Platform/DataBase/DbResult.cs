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
using Alive.Foundation.Data;

namespace Alive.Foundation.Storage
{
    /// <summary>
    /// 数据库操作执行结果的信息
    /// </summary>
    public class DbResult
    {
        #region====公共属性====
        /// <summary>
        /// 获得一个值，用来表示当前数据库操作执行成功与否
        /// </summary>
        public bool IsSuccessful
        {
            get
            {
                return string.IsNullOrEmpty(this.ErrorCode);
            }
        }

        /// <summary>
        /// 获得当前数据库操作执行失败的错误代码
        /// </summary>
        public string ErrorCode
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获得数据库操作获得的数据
        /// </summary>
        public BoInjector Injector
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获得一个包括了所有输出参数的列表
        /// </summary>
        public Dictionary<string, object> OutputParameters
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获得当前数据库操作的存储过程的返回值。
        /// </summary>
        public int ProcedureResult
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获得当前数据库操作的DataSet返回值
        /// </summary>
        public DataSet Data
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获得当前数据库操作获得的数据的第一行第一列的值。
        /// </summary>
        public object ScalarValue
        {
            get
            {
                object result = null;

                if (this.OutputParameters.ContainsKey(ScalarRequest.ScalarValueName))
                {
                    result = this.OutputParameters[ScalarRequest.ScalarValueName];
                }

                return result;
            }
        }

        #endregion
    }
}
