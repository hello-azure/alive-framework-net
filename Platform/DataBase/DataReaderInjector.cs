using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections;
using Alive.Foundation.Data;
using Alive.Foundation.Utilities.Log;

namespace Alive.Foundation.Storage
{
    /// <summary>
    /// DbDataReader注射器。将一个DataReader中的数据，注射给指定的Bo中
    /// </summary>
    public class DataReaderInjector : BoInjector, IDisposable
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 包含有要注入的数据的DbDataReader
        /// </summary>
        DbDataReader reader = null;

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 获得一个值，用于标记NameMapping的key和value的含义。
        /// </summary>
        /// <value>
        /// false：key为字段名称（fieldName），value为别名（alias）。
        /// true：key为别名（alias），value为字段名称（fieldName）。
        /// </value>
        protected override bool IsAliasKey
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region ==== 构造函数 =====

        /// <summary>
        /// 创建一个新的DataReaderInjector的实例
        /// </summary>
        /// <param name="dataReader">包含要注入数据的DbDataReader的实例</param>
        internal DataReaderInjector(DbDataReader dataReader)
        {
            this.reader = dataReader;
        }

        #endregion

        #region ==== 接口实现 ====

        #region IDisposable 成员

        /// <summary>
        /// 释放当前对象所占用的资源
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }

        #endregion

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 将DbDataReader中的数据注入指定的BO对象中。
        /// </summary>
        /// <param name="bo">要注入的对象</param>
        public override void Inject(BusinessObject bo)
        {
            // 判断是否有数据可以注入
            if (this.reader == null || this.reader.IsClosed)
            {
                GlobalLogger<DataReaderInjector>.Error("1", "DataReader 不可用！");
                return;
            }

            // 开始注入数据
            IBoList list = bo as IBoList;

            if (list == null)
            {
                this.InjectSingle(bo);
            }
            else
            {
                this.InjectList(list);
            }
        }

        /// <summary>
        /// 关闭当前注射器所使用的DbDataReader
        /// </summary>
        public override void Close()
        {
            if (this.reader != null && !this.reader.IsClosed)
            {
                this.reader.Close();
            }
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 对单一的BO注入数据。
        /// </summary>
        /// <param name="bo">要注入的对象</param>
        private void InjectSingle(BusinessObject bo)
        {
            if (this.reader.Read())
            {
                this.InjectOne(bo);
            }
        }

        /// <summary>
        /// 对列表的BO注入数据。
        /// </summary>
        /// <param name="list">要注入的对象</param>
        private void InjectList(IBoList list)
        {
            while (this.reader.Read())
            {
                BusinessObject bo = list.Add();
                this.InjectOne(bo);
            }
        }

        /// <summary>
        /// 将Reader中的当前数据注入到指定的BO中。
        /// </summary>
        /// <param name="bo">要注入的对象</param>
        private void InjectOne(BusinessObject bo)
        {
            BoSetter setter = new BoSetter(bo);

            for (int i = 0; i < this.reader.FieldCount; i++)
            {
                string alias = this.reader.GetName(i);

                if (this.NameMapping.ContainsKey(alias))
                {
                    string fieldName = this.NameMapping[alias];

                    object value = reader[i];
                    value = (value.Equals(DBNull.Value)) ? null : value;

                    setter[fieldName].Object = value;
                }
            }
        }

        #endregion
    }
}
