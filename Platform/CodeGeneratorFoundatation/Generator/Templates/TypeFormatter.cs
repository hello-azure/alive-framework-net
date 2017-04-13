/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alive.Foundation.Data;
using Alive.Foundation.Data.DataFields;
using Alive.Tools.CodeGenerator.Foundatation.Generator.BasicGenerators;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Templates
{
    /// <summary>
    /// 类型格式化
    /// </summary>
    internal class TypeFormatter
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 数据库与DataField数据类型对应关系字典
        /// </summary>
        private static readonly Dictionary<SourceType, Dictionary<string, string>> typeDataFieldMappingsDicts = new Dictionary<SourceType, Dictionary<string, string>>();

        /// <summary>
        /// 数据库与C#数据类型对应关系字典
        /// </summary>
        private static readonly Dictionary<SourceType, Dictionary<string, string>> typeStandardMappingsDicts = new Dictionary<SourceType, Dictionary<string, string>>();
       
        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        static TypeFormatter()
        {
            #region ==== 数据库与DataField数据类型对应关系 ====

            //SQLSERVER数据库与DataField数据类型对应关系
            Dictionary<string, string> sqlDataFieldMappingDict = new Dictionary<string, string>();
            sqlDataFieldMappingDict.Add("int", "IntField");
            sqlDataFieldMappingDict.Add("text", "StringField");
            sqlDataFieldMappingDict.Add("bigint", "BigIntField");
            sqlDataFieldMappingDict.Add("binary", "ByteArrayField");
            sqlDataFieldMappingDict.Add("bit", "BoolField");
            sqlDataFieldMappingDict.Add("char", "StringField");
            sqlDataFieldMappingDict.Add("datetime", "DateTimeField");
            sqlDataFieldMappingDict.Add("decimal", "DecimalField");
            sqlDataFieldMappingDict.Add("float", "DoubleField");
            sqlDataFieldMappingDict.Add("image", "ByteArrayField");
            sqlDataFieldMappingDict.Add("money", "DecimalField");
            sqlDataFieldMappingDict.Add("nchar", "StringField");
            sqlDataFieldMappingDict.Add("ntext", "StringField");
            sqlDataFieldMappingDict.Add("numeric", "DecimalField");
            sqlDataFieldMappingDict.Add("nvarchar", "StringField");
            sqlDataFieldMappingDict.Add("smalldatetime", "DateTimeField");
            sqlDataFieldMappingDict.Add("smallint", "SmallIntField");
            sqlDataFieldMappingDict.Add("smallmoney", "DecimalField");
            sqlDataFieldMappingDict.Add("timestamp", "DateTimeField");
            sqlDataFieldMappingDict.Add("tinyint", "ByteField");
            sqlDataFieldMappingDict.Add("varbinary", "ByteArrayField");
            sqlDataFieldMappingDict.Add("varchar", "StringField");
            sqlDataFieldMappingDict.Add("variant", "ObjcectField");
            sqlDataFieldMappingDict.Add("unique identifier", "GuidField");

            //ACCESS数据库与DataField数据类型对应关系
            Dictionary<string, string> accessDataFieldMappingDict = new Dictionary<string, string>();
            accessDataFieldMappingDict.Add("varwchar", "StringField");
            accessDataFieldMappingDict.Add("longvarwchar", "StringField");
            accessDataFieldMappingDict.Add("unsignedtinyint", "ByteField");
            accessDataFieldMappingDict.Add("boolean", "BoolField");
            accessDataFieldMappingDict.Add("datetime", "DateTimeField");
            accessDataFieldMappingDict.Add("十进制", "DecimalField");
            accessDataFieldMappingDict.Add("双", "DoubleField");
            accessDataFieldMappingDict.Add("guid", "GuidField");
            accessDataFieldMappingDict.Add("整数", "IntField");
            accessDataFieldMappingDict.Add("longvarbinary", "ByteArrayField");
            accessDataFieldMappingDict.Add("单个", "SingleField");
            accessDataFieldMappingDict.Add("smallint", "SmallIntField");
            accessDataFieldMappingDict.Add("varbinary*", "ByteArrayField");

            //ORACLW数据库与DataField数据类型对应关系
            Dictionary<string, string> oracleDataFieldMappingDict = new Dictionary<string, string>();
            oracleDataFieldMappingDict.Add("bfile", "ByteArrayField");
            oracleDataFieldMappingDict.Add("blob", "ByteArrayField");
            oracleDataFieldMappingDict.Add("char", "StringField");
            oracleDataFieldMappingDict.Add("clob", "StringField");
            oracleDataFieldMappingDict.Add("date", "DateTimeField");
            oracleDataFieldMappingDict.Add("float", "DecimalField");
            oracleDataFieldMappingDict.Add("integer", "DecimalField");
            oracleDataFieldMappingDict.Add("intervalyeartomonth", "IntField");
            oracleDataFieldMappingDict.Add("intervaldaytosecond", "TimeSpanField");
            oracleDataFieldMappingDict.Add("long", "StringField");
            oracleDataFieldMappingDict.Add("longraw", "ByteArrayField");
            oracleDataFieldMappingDict.Add("nchar", "StringField");
            oracleDataFieldMappingDict.Add("nclob", "StringField");
            oracleDataFieldMappingDict.Add("number", "DecimalField");
            oracleDataFieldMappingDict.Add("nvarchar2", "StringField");
            oracleDataFieldMappingDict.Add("raw", "ByteArrayField");
            oracleDataFieldMappingDict.Add("rowid", "StringField");
            oracleDataFieldMappingDict.Add("timestamp", "DateTimeField");
            oracleDataFieldMappingDict.Add("varchar2", "StringField");

            typeDataFieldMappingsDicts.Add(SourceType.SQLSERVER, sqlDataFieldMappingDict);
            typeDataFieldMappingsDicts.Add(SourceType.OLEDB, accessDataFieldMappingDict);
            typeDataFieldMappingsDicts.Add(SourceType.ORACLE, oracleDataFieldMappingDict);

            #endregion

            #region ==== 数据库与C#数据类型对应关系 ====
            
            //SQLSERVER数据库与C#数据类型对应关系
            Dictionary<string, string> sqlStandardMappingDict = new Dictionary<string, string>();
            sqlStandardMappingDict.Add("int", "int");
            sqlStandardMappingDict.Add("text", "string");
            sqlStandardMappingDict.Add("bigint", "Int64");
            sqlStandardMappingDict.Add("binary", "byte[]");
            sqlStandardMappingDict.Add("bit", "bool");
            sqlStandardMappingDict.Add("char", "string");
            sqlStandardMappingDict.Add("datetime", "DateTime");
            sqlStandardMappingDict.Add("decimal", "decimal");
            sqlStandardMappingDict.Add("float", "double");
            sqlStandardMappingDict.Add("image", "byte[]");
            sqlStandardMappingDict.Add("money", "decimal");
            sqlStandardMappingDict.Add("nchar", "string");
            sqlStandardMappingDict.Add("ntext", "string");
            sqlStandardMappingDict.Add("numeric", "decimal");
            sqlStandardMappingDict.Add("nvarchar", "string");
            sqlStandardMappingDict.Add("smalldatetime", "DateTime");
            sqlStandardMappingDict.Add("smallint", "Int16");
            sqlStandardMappingDict.Add("smallmoney", "decimal");
            sqlStandardMappingDict.Add("timestamp", "DateTime");
            sqlStandardMappingDict.Add("tinyint", "byte");
            sqlStandardMappingDict.Add("varbinary", "byte[]");
            sqlStandardMappingDict.Add("varchar", "string");
            sqlStandardMappingDict.Add("variant", "object");
            sqlStandardMappingDict.Add("unique identifier", "Guid");
            
            //ACCESS数据库与C#数据类型对应关系
            Dictionary<string, string> accessStandardMappingDict = new Dictionary<string, string>();
            accessStandardMappingDict.Add("varwchar", "string");
            accessStandardMappingDict.Add("longvarwchar", "string");
            accessStandardMappingDict.Add("unsignedtinyint", "byte");
            accessStandardMappingDict.Add("boolean", "bool");
            accessStandardMappingDict.Add("datetime", "DateTime");
            accessStandardMappingDict.Add("十进制", "decimal");
            accessStandardMappingDict.Add("双", "double");
            accessStandardMappingDict.Add("guid", "Guid");
            accessStandardMappingDict.Add("整数", "int");
            accessStandardMappingDict.Add("longvarbinary", "byte[]");
            accessStandardMappingDict.Add("单个", "Single");
            accessStandardMappingDict.Add("smallint", "Int16");
            accessStandardMappingDict.Add("varbinary*", "byte[]");
            
            //ORACLW数据库与C#数据类型对应关系
            Dictionary<string, string> oracleStandardMappingDict = new Dictionary<string, string>();
            oracleStandardMappingDict.Add("bfile", "byte[]");
            oracleStandardMappingDict.Add("blob", "byte[]");
            oracleStandardMappingDict.Add("char", "string");
            oracleStandardMappingDict.Add("clob", "string");
            oracleStandardMappingDict.Add("date", "DateTime");
            oracleStandardMappingDict.Add("float", "decimal");
            oracleStandardMappingDict.Add("integer", "decimal");
            oracleStandardMappingDict.Add("intervalyeartomonth", "int");
            oracleStandardMappingDict.Add("intervaldaytosecond", "TimeSpan");
            oracleStandardMappingDict.Add("long", "string");
            oracleStandardMappingDict.Add("longraw", "byte[]");
            oracleStandardMappingDict.Add("nchar", "string");
            oracleStandardMappingDict.Add("nclob", "string");
            oracleStandardMappingDict.Add("number", "decimal");
            oracleStandardMappingDict.Add("nvarchar2", "string");
            oracleStandardMappingDict.Add("raw", "byte[]");
            oracleStandardMappingDict.Add("rowid", "string");
            oracleStandardMappingDict.Add("timestamp", "DateTime");
            oracleStandardMappingDict.Add("varchar2", "string");

            typeStandardMappingsDicts.Add(SourceType.SQLSERVER, sqlStandardMappingDict);
            typeStandardMappingsDicts.Add(SourceType.OLEDB, accessStandardMappingDict);
            typeStandardMappingsDicts.Add(SourceType.ORACLE, oracleStandardMappingDict);

            #endregion
        }

        #endregion

        #region ==== 公共方法 ====

        /// <summary>
        /// 类型格式
        /// </summary>
        /// <param name="sourceType">数据源类型</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="isStandardType">true:C#标准数据类型 false:DataFiled强数据类型</param>
        /// <returns>类型名称</returns>
        internal static string Format(SourceType sourceType, string dataType, bool isStandardType)
        {
            var type = string.Empty;

            if (isStandardType)
            {
                type = typeStandardMappingsDicts[sourceType][dataType.ToLower().Trim()];
            }
            else
            {
                type = typeDataFieldMappingsDicts[sourceType][dataType.ToLower().Trim()];
            }

            if (type == null)
            {
                throw new DataTypeFormatException();
            }

            return type;
        }

        #endregion
    }
}
