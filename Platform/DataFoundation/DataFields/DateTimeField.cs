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
    /// 日期格式字段
    /// </summary>
    public class DateTimeField : DataFieldBase<DateTime>
    {
        #region ==== 属性 ====

        /// <summary>
        /// 获得字段的默认值。
        /// </summary>
        public override DateTime Default
        {
            get { return DateTime.MaxValue; }
        }

        /// <summary>
        /// 获得或设置字段的字符串形式的值
        /// </summary>
        public override string ValueText
        {
            get
            {
                return this.FormatDate(this.Value);
            }
            set
            {
                base.ValueText = value;
            }
        }

        /// <summary>
        /// 设置或获得协调世界时（UTC）的字符串形式
        /// </summary>
        public string UtcText
        {
            get
            {
                return this.FormatDate(this.UtcValue);
            }
            set
            {
                this.UtcValue = this.SetValueText(value);
            }
        }

        /// <summary>
        /// 获得或设置字段的值。
        /// </summary>
        public override DateTime Value
        {
            get
            {
                DateTime utc = DateTime.SpecifyKind(base.Value, DateTimeKind.Utc);
                return TimeZoneInfo.ConvertTime(utc, TimeZoneInfo.Local);
            }
            set
            {
                base.Value = TimeZoneInfo.ConvertTime(value, TimeZoneInfo.Utc);
            }
        }

        /// <summary>
        /// 设置或获得字段的协调世界时（UTC）
        /// </summary>
        public DateTime UtcValue
        {
            get { return base.Value; }
            set { base.Value = value; }
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 从对象的 XML 表示形式生成该对象。
        /// </summary>
        /// <param name="reader">对象从中进行反序列化的 XmlReader 流。</param>
        public override void ReadXml(System.Xml.XmlReader reader)
        {
            string value = reader.ReadElementContentAsString();

            if (!string.IsNullOrEmpty(value))
            {
                this.UtcText = value;
            }
        }

        /// <summary>
        /// 将对象转换为其 XML 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 XmlWriter 流。</param>
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (this.Value != this.Default)
            {
                writer.WriteValue(this.UtcText);
            }
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 设置字段值的字符串格式
        /// </summary>
        /// <param name="text">要设置字符串</param>
        protected override DateTime SetValueText(string text)
        {
            return DateTime.Parse(text);
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 格式化输出日期时间
        /// </summary>
        /// <param name="date">要格式化的时间</param>
        /// <returns>格式化的字符串。</returns>
        private string FormatDate(DateTime date)
        {
            string result = string.Format(
                "{0}-{1}-{2} {3}:{4}:{5}",
                date.Year,
                date.Month,
                date.Day,
                date.Hour,
                date.Minute,
                date.Second);
            return result;
        }

        #endregion
    }
}
