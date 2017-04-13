/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Alive.Foundation.Utilities.DataBase
{
    /// <summary>
    /// 数据库备份
    /// </summary>
    public class SQLServer
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 获得当前数据库连接字符串
        /// </summary>
        private static readonly string connString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        #endregion

        #region ==== 构造函数 ====

        public SQLServer()
        {
            
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="backupName">备份名称</param>
        /// <returns></returns>
        public static bool BackUp(string databaseName, string backupName)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string name = databaseName + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString();

                //删除逻辑备份设备，但不会删掉备份的数据库文件
                SqlCommand cmd1 = new SqlCommand("sp_dropdevice", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd1.Parameters.Add("@logicalname", SqlDbType.VarChar, 20);
                para.Direction = ParameterDirection.Input;
                para.Value = databaseName;

                //如果逻辑设备不存在,略去错误
                try
                {
                    cmd1.ExecuteNonQuery();
                }
                catch
                { }

                //创建逻辑备份设备
                SqlCommand cmd2 = new SqlCommand("sp_addumpdevice", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                para = cmd2.Parameters.Add("@devtype", SqlDbType.VarChar, 20);
                para.Direction = ParameterDirection.Input;
                para.Value = "disk";

                //逻辑设备名
                para = cmd2.Parameters.Add("@logicalname", SqlDbType.VarChar, 20);
                para.Direction = ParameterDirection.Input;
                para.Value = databaseName;

                para = cmd2.Parameters.Add("physicalname", SqlDbType.NVarChar, 260);
                para.Direction = ParameterDirection.Input;
                para.Value = backupName;

                try
                {
                    cmd2.ExecuteNonQuery();
                }
                catch
                { }

                //备份数据库到指定文件
                string sql = "BACKUP DATABASE " + databaseName + " TO " + databaseName + " WITH INIT";
                SqlCommand cmd3 = new SqlCommand(sql, conn);
                cmd3.CommandType = CommandType.Text;

                try
                {
                    cmd3.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 还原指定的数据库文件
        /// </summary>
        /// <param name="databaseName">要还原的数据库</param>
        /// <param name="databaseFile">数据库备份文件及路径</param>
        /// <returns></returns>
        public static bool Restore(string databaseName, string databaseFile)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                //Kill Database Process
                SqlCommand cmd = new SqlCommand("use master  SELECT SPID FROM SYSPROCESSES,SYSDATABASES WHERE SYSPROCESSES.DBID=SYSDATABASES.DBID AND SYSDATABASES.NAME='" + databaseName + "'", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                ArrayList list = new ArrayList();

                while (dr.Read())
                {
                    list.Add(dr.GetInt16(0));
                }

                dr.Close();

                for (int i = 0; i < list.Count; i++)
                {
                    cmd = new SqlCommand(string.Format("KILL {0}", list[i]),conn);
                    cmd.ExecuteNonQuery();
                }

                //还原指定的数据库文件
                string sql = "RESTORE DATABASE " + databaseName + " FROM DISK ='" + databaseFile + "'";
                SqlCommand restoreCmd = new SqlCommand(sql, conn);
                restoreCmd.CommandType = CommandType.Text;
      

                try
                {
                    restoreCmd.ExecuteNonQuery();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
