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
    /// 字符串类型的字段
    /// </summary>
    public class StringField : DataFieldBase<string>
    {
        #region ==== 属性 ====

        /// <summary>
        /// 获得字段的默认值。
        /// </summary>
        public override string Default
        {
            get { return string.Empty; }
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 将对象转换为其 XML 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 XmlWriter 流。</param>
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (this.Value != this.Default)
            {
                writer.WriteCData(this.Value);
            }
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 设置字段值的字符串格式
        /// </summary>
        /// <param name="text">要设置字符串</param>
        protected override string SetValueText(string text)
        {
            return text;
        }

        #endregion
    }
}
