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

namespace Alive.Foundation.Data.DataFields
{
    /// <summary>
    /// 用于实现具体的业务字段的基础泛型。
    /// </summary>
    /// <typeparam name="TFieldValue">数据业务字段的实际类型</typeparam>
    public abstract class DataFieldBase<TFieldValue> : DataField
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 字段值。
        /// </summary>
        TFieldValue value;

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 获得或设置业务字段的值。
        /// </summary>
        public virtual TFieldValue Value
        {
            get 
            { 
                return this.value; 
            }
            set 
            {
                base.HasSet = true;
                this.value = value; 
            }
        }

        /// <summary>
        /// 获得字段是否进行set操作
        /// </summary>
        public override bool HasSet
        {
            get
            {
                return base.HasSet;
            }
            protected set
            {
                base.HasSet = value;
            }
        }

        /// <summary>
        /// 获得或设置业务字段的值（对象形式）。
        /// </summary>
        public override object ValueObject
        {
            get
            {
                return this.Value;
            }
            set
            {
                if (value != null)
                {
                    this.Value = (TFieldValue)value;
                }
                else
                {
                    this.Value = this.Default;
                }
            }
        }

        /// <summary>
        /// 获得或设置业务字段的值（字符串形式）。
        /// </summary>
        public override string ValueText
        {
            get
            {
                if (this.Value != null)
                {
                    return this.Value.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.Value = this.SetValueText(value);
                }
                else
                {
                    this.Value = this.Default;
                }
            }
        }

        /// <summary>
        /// 获得该类型字段的默认值。
        /// </summary>
        /// <value>FieldValueType引用类型时，统一为null。</value>
        public abstract TFieldValue Default
        {
            get;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的 DataFieldBase 的对象
        /// </summary>
        public DataFieldBase()
        {
            this.value = this.Default;
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 获得当前字段的值的类型。
        /// </summary>
        /// <returns>表示当前字段的值的类型的 Type 对象。</returns>
        public override Type GetValueType()
        {
            return typeof(TFieldValue);
        }

        /// <summary>
        /// 从对象的 XML 表示形式生成该对象。
        /// </summary>
        /// <param name="reader">对象从中进行反序列化的 XmlReader 流。</param>
        public override void ReadXml(System.Xml.XmlReader reader)
        {
            string text = reader.ReadElementContentAsString();

            if (!string.IsNullOrEmpty(text))
            {
                this.ValueText = text;
            }
        }

        /// <summary>
        /// 将对象转换为其 XML 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 XmlWriter 流。</param>
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!this.ValueObject.Equals(this.Default))
            {
                base.WriteXml(writer);
            }
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 以字符串格式设置字段的值。
        /// </summary>
        /// <param name="text">要设置的字符串</param>
        protected abstract TFieldValue SetValueText(string text);

        #endregion
    }
}
