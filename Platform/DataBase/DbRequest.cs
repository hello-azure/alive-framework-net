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
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Alive.Foundation.Storage.ParameterInfos;
using Alive.Foundation.Data;
using System.Configuration;

namespace Alive.Foundation.Storage
{
    /// <summary>
    /// 数据库访问操作
    /// </summary>
    public abstract class DbRequest
    {
        #region ==== 常数 ====

        #region 错误代码

        /// <summary>
        /// 数据库操作失败时返回的错误代码。
        /// </summary>
        public static readonly string RequestFailedError = "RequestFailedError";

        #endregion

        ///// <summary>
        ///// 数据库类型
        ///// </summary>
        //internal static readonly string type = ConfigurationManager.AppSettings["DbType"].ToString();

        /// <summary>
        /// 存储过程返回值的参数名称。
        /// </summary>
        internal static readonly string ProcedureResultName = "@ProcedureResultName";

        /// <summary>
        /// 数据类型映射表
        /// </summary>
        internal static readonly Dictionary<string, DbType> TypeMapping = new Dictionary<string, DbType>() 
        {
           {"System.Int16",DbType.Int16},
           {"System.Int32",DbType.Int32},
           {"System.Int64",DbType.Int64},
           {"System.String",DbType.String},
           {"System.Boolean",DbType.Boolean},
           {"System.Byte",DbType.Byte},
           {"System.DateTime",DbType.DateTime},
           {"System.Decimal",DbType.Decimal},
           {"System.Double",DbType.Double},
           {"System.Single",DbType.Single},
           {"System.Guid",DbType.Guid}
        };

        #endregion

        #region ==== 私有字段 ====

        /// <summary>
        /// 当前数据库操作的输出参数名的列表
        /// </summary>
        private readonly IList<string> outputParamNames = new List<string>();

        /// <summary>
        /// 当前操作所需的参数。
        /// </summary>
        private readonly ParameterList myParams = new ParameterList();

        /// <summary>
        /// 当前操作所需的输出参数
        /// </summary>
        private readonly ParameterList myOutputs = new ParameterList();

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 标准SQL语句
        /// </summary>
        public string SQL
        {
            get;
            set;
        }

        /// <summary>
        /// 设置或获取当前数据库操作的存储过程名称
        /// </summary>
        public string ProcedureName
        {
            get;
            set;
        }

        /// <summary>
        /// 设置当前数据库操作的输出参数名
        /// </summary>
        internal IList<string> OutputParameters
        {
            get
            {
                return outputParamNames;
            }
        }

        /// <summary>
        /// 获得或设置当前数据库操作请求所使用的数据库连接
        /// </summary>
        internal ConnectionAdapter Connection
        {
            get;
            set;
        }

        /// <summary>
        /// 获得当前操作的所有参数
        /// </summary>
        internal ParameterList Parameters
        {
            get
            {
                return this.myParams;
            }
        }

        /// <summary>
        /// 获得当前操作的所有输出参数信息
        /// </summary>
        internal ParameterList Outputs
        {
            get
            {
                return this.myOutputs;
            }
        }

        /// <summary>
        /// 获得一个值，判断当前是否执行存储过程
        /// </summary>
        internal bool IsExecuteProcedure
        {
            get
            {
                return !string.IsNullOrEmpty(this.ProcedureName) && string.IsNullOrEmpty(this.SQL);
            }
        }

        #endregion

        #region ==== 事件定义 ====

        /// <summary>
        /// 数据库请求执行完毕触发事件
        /// </summary>
        internal event EventHandler<RequestEcecutedArgs> Executed;

        #endregion

        #region ==== 公共方法 ====

        #region SetParameter

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="name">设置参数名称</param>
        /// <param name="datatype">设置参数类型</param>
        /// <param parameterName="value">设置参数值</param>
        public void SetParameter(string parameterName, DbType datatype, object value)
        {
            ParameterInfo info = null;

            if (!this.myParams.TryGetValue(parameterName, out info))
            {
                info = new ParameterInfo();
                info.Name = parameterName;
                this.myParams.Add(info);
            }

            info.Type = datatype;
            info.Value = value;
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, Int16 value)
        {
            this.SetParameter(parameterName, DbType.Int16, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, int value)
        {
            this.SetParameter(parameterName, DbType.Int32, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, Int64 value)
        {
            this.SetParameter(parameterName, DbType.Int64, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, string value)
        {
            this.SetParameter(parameterName, DbType.String, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, bool value)
        {
            this.SetParameter(parameterName, DbType.Boolean, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, byte value)
        {
            this.SetParameter(parameterName, DbType.Byte, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, DateTime value)
        {
            this.SetParameter(parameterName, DbType.DateTime, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, decimal value)
        {
            this.SetParameter(parameterName, DbType.Decimal, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, double value)
        {
            this.SetParameter(parameterName, DbType.Double, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, Single value)
        {
            this.SetParameter(parameterName, DbType.Single, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="parameterName">设置参数名称</param>
        /// <param name="value">设置参数值</param>
        public void SetParameter(string parameterName, Guid value)
        {
            this.SetParameter(parameterName, DbType.Guid, value);
        }

        /// <summary>
        /// 设置当前数据库操作所需要的参数
        /// </summary>
        /// <param name="data">业务数据对象</param>
        public void SetParameters(BusinessObject data)
        {
            if (data != null)
            {
                var columns = data.GetNameMapping();

                if (columns != null && columns.Count > 0)
                {
                    string type = ConfigurationManager.AppSettings["DbType"].ToString();

                    foreach (var column in columns)
                    {
                        if (data.GetType(column).HasSet)
                        {
                            var paraTag = type == "ORACLE" ? ":" : "@";
                            var key = data.GetValueType(column).FullName;

                            if (TypeMapping.Keys.Contains(key))
                            {
                                this.SetParameter(string.Format("{0}{1}", paraTag, column),
                                    TypeMapping[key],
                                    data.GetType(column).ValueObject);
                            }
                            else
                            {
                                this.SetParameter(string.Format("{0}{1}", paraTag, column),
                                    DbType.Object,
                                    data.GetType(column).ValueObject);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region SetOutput

        /// <summary>
        /// 设置一个输出参数的信息
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dataType">参数类型</param>
        /// <param name="size">参数所占大小（只对字符有效）</param>
        public void SetOutput(string parameterName, DbType dataType, int size)
        {
            ParameterInfo info = null;

            if (!this.myParams.TryGetValue(parameterName, out info))
            {
                info = new ParameterInfo();
                info.Name = parameterName;
                this.myOutputs.Add(info);
            }

            info.Type = dataType;

            if (size > 0)
            {
                info.Size = size;
            }

            this.OutputParameters.Add(ParameterInfo.FormateName(parameterName));
        }

        /// <summary>
        /// 设置一个无需指定大小的输出参数的信息
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dataType">参数类型</param>
        public void SetOutput(string parameterName, DbType dataType)
        {
            this.SetOutput(parameterName, dataType, 0);
        }

        /// <summary>
        /// 设置一个字符串类型的输出参数的信息
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="size">参数所占大小（只对字符有效）</param>
        public void SetOutput(string parameterName, int size)
        {
            this.SetOutput(parameterName, DbType.String, size);
        }

        /// <summary>
        /// 设置一个整数类型的输出参数的信息
        /// </summary>
        /// <param name="parameterName">参数名</param>
        public void SetOutput(string parameterName)
        {
            this.SetOutput(parameterName, DbType.Int32, 0);
        }

        #endregion

        /// <summary>
        /// 执行当前数据库请求
        /// </summary>
        /// <returns> 数据库操作的返回结果</returns>
        public DbResult Execute()
        {
            DbResult result = this.DoExecute();

            if (this.Executed != null)
            {
                this.Executed(this, new RequestEcecutedArgs(result));
            }

            return result;
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 实际执行数据库请求
        /// 由子类实现
        /// </summary>
        /// <returns>DbResult对象</returns>
        protected abstract DbResult DoExecute();

        #endregion
    }
}
