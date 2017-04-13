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
using System.Reflection;
using Alive.Foundation.Data.DataFields;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// 系统中所有业务数据对象的基础类。
    /// </summary>
    public abstract class BusinessObject : DataFoundation
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 一个用于表示当前业务数据对象的业务字段是否初始化的值。
        /// </summary>
        private bool isInitialized = false;

        #endregion

        #region ==== 内部方法 ====

        /// <summary>
        /// 以对象方式设置指定业务字段的值。
        /// </summary>
        /// <param name="fieldName">要设置的字段的名称</param>
        /// <param name="value">要设置的值</param>
        internal void SetValueObject(string fieldName, object value)
        {
            DataField field = this.GetField<DataField>(fieldName);
            field.ValueObject = value;

        }

        /// <summary>
        /// 以对象方式获得指定业务字段的值。 
        /// </summary>
        /// <param name="fieldName">要获得的字段的名称</param>
        /// <returns>
        /// 获得的值的对象。
        /// 如果指定的字段名称不存在，则返回null。
        /// </returns>
        internal object GetValueObject(string fieldName)
        {
            DataField field = this.GetField<DataField>(fieldName, true);
            object result = null;

            if (field != null)
            {
                result = field.ValueObject;
            }

            return result;
        }

        /// <summary>
        /// 以字符串方式设置置顶业务字段的值
        /// </summary>
        /// <param name="fieldName">要设置的字段的名称</param>
        /// <param name="value">要设置的值</param>
        internal void SetValueText(string fieldName, string value)
        {
            DataField field = this.GetField<DataField>(fieldName);
            field.ValueText = value;
        }

        /// <summary>
        /// 以字符串方式获得指定业务字段的值
        /// </summary>
        /// <param name="fieldName">要获得的字段的名称</param>
        /// <returns>
        /// 获得的值的字符串。
        /// 如果指定的字段名称不存在，则返回string.Empty。
        /// </returns>
        public string GetValueText(string fieldName)
        {
            DataField field = this.GetField<DataField>(fieldName, true);
            string result = string.Empty;

            if (field != null)
            {
                result = field.ValueText;
            }

            return result;
        }

        /// <summary>
        /// 获得当前业务数据对象所有字段的列表。子类可以根据需要做特定修改。
        /// </summary>
        /// <returns>
        /// 所有业务字段的字段名列表。
        /// 可能返回一个空的列表，但是不会返回null。
        /// </returns>
        public virtual List<string> GetNameMapping()
        {
            Type thisType = this.GetType();
            Type baseType = typeof(DataField);
            PropertyInfo[] properties = thisType.GetProperties();
            List<string> result = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.IsSubclassOf(baseType))
                {
                    result.Add(property.Name);
                }
            }

            return result;
        }

        /// <summary>
        /// 初始化当前业务对象的所有业务字段。
        /// </summary>
        internal virtual void Initialize()
        {
            if (this.isInitialized)
            {
                return;
            }

            Type thisType = this.GetType();
            Type baseType = typeof(DataField);
            PropertyInfo[] properties = thisType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.IsSubclassOf(baseType))
                {
                    property.GetValue(this, null);
                }
            }

            this.isInitialized = true;
        }

        /// <summary>
        /// 获得业务字段的值的类型。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns>表示业务字段的值的类型的 Type 对象</returns>
        public virtual DataField GetType(string fieldName)
        {
            return this.GetField<DataField>(fieldName);
        }

        /// <summary>
        /// 获得业务字段的值的类型。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns>表示业务字段的值的类型的 Type 对象</returns>
        public virtual Type GetValueType(string fieldName)
        {
            DataField field = this.GetField<DataField>(fieldName);
            Type result = null;

            if (field != null)
            {
                result = field.GetValueType();
            }

            return result;
        }

        #endregion
    }
}
