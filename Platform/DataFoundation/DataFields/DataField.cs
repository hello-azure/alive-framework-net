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
using System.Xml.Serialization;
using System.Reflection;

namespace Alive.Foundation.Data.DataFields
{
    /// <summary>
    /// 业务字段类型的基础类。
    /// </summary>
    public class DataField : IXmlSerializable
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 字段名。
        /// </summary>
        private string name;

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 获得字段是否进行set操作
        /// </summary>
        public virtual bool HasSet
        {
            get;
            protected set;
        }

        /// <summary>
        /// 获得或设置字段的值的对象形式。
        /// </summary>
        public virtual object ValueObject
        {
            get;
            set;
        }

        /// <summary>
        /// 获得或设置字段的值的字符串形式。
        /// </summary>
        public virtual string ValueText
        {
            get;
            set;
        }

        /// <summary>
        /// 设置或获得字段名称。
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        #endregion

        #region IXmlSerializable 成员

        /// <summary>
        /// 此方法是保留方法，请不要使用。
        /// 如果需要指定自定义架构，应向该类应用 XmlSchemaProviderAttribute。
        /// </summary>
        /// <returns>在实现 IXmlSerializable 接口时，应从此方法返回 null</returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 从对象的 XML 表示形式生成该对象。
        /// </summary>
        /// <param name="reader">对象从中进行反序列化的 XmlReader 流。</param>
        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            this.ValueText = reader.ReadElementContentAsString();
        }

        /// <summary>
        /// 将对象转换为其 XML 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 XmlWriter 流。</param>
        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteValue(this.ValueText);
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 返回表示当前字段值的字符串。
        /// </summary>
        /// <returns>表示当前字段值的字符串。默认与ValueText属性相同。</returns>
        public override string ToString()
        {
            return this.ValueText;
        }

        /// <summary>
        /// 获得当前字段的值的类型。
        /// </summary>
        /// <returns>表示当前字段的值的类型的 Type 对象。</returns>
        public virtual Type GetValueType()
        {
            return typeof(object);
        }

        /// <summary>
        /// 创建一个与当前对象包含相同值的新的实例
        /// </summary>
        /// <returns>新创建的实例</returns>
        public virtual DataField Clone()
        {
            Type myType = this.GetType();
            DataField result = Activator.CreateInstance(myType) as DataField;
            result.ValueObject = this.ValueObject;

            return result;
        }

        #endregion
    }
}
