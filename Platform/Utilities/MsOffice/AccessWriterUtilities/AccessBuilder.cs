/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.IO;
using ADOX;

namespace Alive.Foundation.Utilities.MsOffice.AccessWriterUtilities
{
    /// <summary>
    /// 创建Access数据库和表的工具
    /// </summary>
    internal class AccessBuilder : IDisposable
    {
        #region ==== 属性 ====

        /// <summary>
        /// 获得数据链接
        /// </summary>
        protected CatalogClass Connection
        {
            get;
            private set;
        }
        
        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的数据链接对象
        /// </summary>
        /// <param name="fileName">Access文件名</param>
        public AccessBuilder(string fileName)
        {
            this.Connection = new CatalogClass();

            if (File.Exists(fileName))
            {
                ADODB.Connection conn = new ADODB.Connection();

                conn.Open(string.Format(
                    Properties.Resources.AccessConnStringFormat,
                    fileName),
                    string.Empty, string.Empty, 0);

                this.Connection.ActiveConnection = conn;
            }
            else
            {
                this.Connection.Create(string.Format(
                    Properties.Resources.AccessConnStringFormat,
                    fileName));
            }
        }

        #endregion

        #region ==== 接口实现 ====

        #region IDisposable 成员

        public void Dispose()
        {
            if (this.Connection != null)
            {
                (this.Connection.ActiveConnection as ADODB.Connection).Close();
                this.Connection.ActiveConnection = null;
                this.Connection = null;
            }
        }

        #endregion

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="colTypeDesign">数据字段的属性</param>
        public void CreateTable(ColumnDesign colTypeDesign)
        {
            if (colTypeDesign == null)
            {
                return;
            }

            // 创建表
            TableClass newTable = new TableClass();
            newTable.Name = colTypeDesign.TableName;
            newTable.ParentCatalog = this.Connection;

            // 创建字段
            foreach (string colName in colTypeDesign.Keys)
            {
                var design = colTypeDesign[colName];
                var newCol = this.CreateColumn(colName, design);

                if (design.IsPK)
                {
                    newTable.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, colName, "", "");
                }

                newTable.Columns.Append(newCol, (DataTypeEnum)design.Type, design.Size);
            }

            this.Connection.Tables.Append(newTable);
        }
        
        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <returns>创建的字段</returns>
        private ColumnClass CreateColumn(string columnName, ColumnDesignItem design)
        {
            ColumnClass result = new ColumnClass();
            result.ParentCatalog = this.Connection;
            result.Type = (DataTypeEnum)design.Type;
            result.Name = columnName;
            result.Properties["Jet OLEDB:Allow Zero Length"].Value = !design.IsRequired;

            return result;
        }

        #endregion
    }
}
