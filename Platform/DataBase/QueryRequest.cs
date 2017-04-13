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
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Alive.Foundation.Utilities.Log;
using Alive.Foundation.Storage.ParameterInfos;

namespace Alive.Foundation.Storage
{
    /// <summary>
    /// 执行一个带查询的数据库操作，并且返回查询结果的数据集合。
    /// </summary>
    public class QueryRequest : DbRequest
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryRequest()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">sql字符串</param>
        public QueryRequest(string sql)
        {
            this.SQL = sql;
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 执行当前数据库请求
        /// </summary>
        /// <returns>数据库操作的返回结果</returns>
        protected override DbResult DoExecute()
        {
            DbResult result = new DbResult();

            DbCommand command = this.Connection.CreateCommand() as DbCommand;
            if (this.IsExecuteProcedure)
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = this.ProcedureName;

                // 设置参数
                FillInputParameters(command);
                FillOutputParameters(command);
                FillProcedureResult(command);
            }
            else
            {
                command.CommandText = this.SQL;
                FillInputParameters(command);
                FillOutputParameters(command);
            }

            // 执行命令
            try
            {

                var reader = command.ExecuteReader();
                if (IsExecuteProcedure)
                {
                    result.OutputParameters = this.ReadOutputParameters(command);
                    result.ProcedureResult = this.ReadProcedureResult(command);
                }

                result.Injector = new DataReaderInjector(reader);
            }
            catch (Exception ex)
            {
                result.ErrorCode = RequestFailedError;
                GlobalLogger<QueryRequest>.Error(ex.ToString());
            }

            return result;
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 将输入参数设置到SqlCommand对象。
        /// </summary>
        /// <param name="command">要执行的SqlCommand对象</param>
        private void FillInputParameters(DbCommand command)
        {
            foreach (var parameterName in this.Parameters.Keys)
            {
                var parameter = command.CreateParameter();
                var paramInfo = this.Parameters[parameterName];

                parameter.ParameterName = parameterName;
                parameter.DbType = paramInfo.Type;
                parameter.Value = paramInfo.Value;
                parameter.Direction = ParameterDirection.Input;

                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// 将输出参数设置到SqlCommand对象。
        /// </summary>
        /// <param name="command">要执行的SqlCommand对象</param>
        private void FillOutputParameters(DbCommand command)
        {
            foreach (var item in this.OutputParameters)
            {
                string parameterName = ParameterInfo.FormateName(item);
                if (this.Parameters.ContainsKey(parameterName))
                {
                    command.Parameters[parameterName].Direction = ParameterDirection.InputOutput;
                }
                else
                {
                    var parameter = command.CreateParameter();

                    parameter.ParameterName = parameterName;
                    parameter.DbType = this.Outputs[parameterName].Type;
                    parameter.Size = this.Outputs[parameterName].Size;
                    parameter.Direction = ParameterDirection.Output;

                    command.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 将返回值设置到SqlCommand对象。
        /// </summary>
        /// <param name="command">要执行的SqlCommand对象</param>
        private static void FillProcedureResult(DbCommand command)
        {
            if (!command.Parameters.Contains(ProcedureResultName))
            {
                var parameter = command.CreateParameter();

                parameter.ParameterName = ProcedureResultName;
                parameter.Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add(parameter);
            }
            else
            {
                command.Parameters[ProcedureResultName].Direction = ParameterDirection.ReturnValue;
            }
        }

        /// <summary>
        /// 获得数据库操作执行后的输出参数
        /// </summary>
        /// <param name="command">执行数据库操作的SqlCommand对象</param>
        /// <returns>包含所有输出参数的参数名和值的字典。不会返回null</returns>
        private Dictionary<string, object> ReadOutputParameters(DbCommand command)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (string paraName in this.OutputParameters)
            {
                object para = command.Parameters[paraName].Value;

                if (para.Equals(DBNull.Value))
                {
                    para = null;
                }

                result.Add(paraName, para);
            }

            return result;
        }

        /// <summary>
        /// 获得存储过程执行完成的返回值。
        /// </summary>
        /// <param name="command">执行数据库操作的SqlCommand对象</param>
        /// <returns>存储过程返回值</returns>
        private int ReadProcedureResult(DbCommand command)
        {
            object result = command.Parameters[ProcedureResultName].Value;
            int resultValue = 0;

            if (result != null && !result.Equals(DBNull.Value))
            {
                resultValue = (int)result;
            }

            return resultValue;
        }

        #endregion
    }
}
