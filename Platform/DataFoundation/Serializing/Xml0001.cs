/***********
 * 版权说明：
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
using System.IO;
using System.Xml;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// Xml格式的编码解码器。版本号0.1
    /// </summary>
    internal class Xml0001 : ISerializer
    {
        #region ==== 私有字段 ====

        private readonly XmlSerializer mySerializer;
        private readonly XmlAttributeOverrides myOverrides;

        #endregion ^^ 私有字段 ^^

        #region ==== 构造函数 ====

        /// <summary>
        /// 静态构造函数。初始化静态成员。
        /// </summary>
        internal Xml0001(Type objectType)
        {
            myOverrides = new XmlAttributeOverrides();
            mySerializer = new XmlSerializer(objectType, myOverrides);
        }

        #endregion ^^ 构造函数 ^^

        #region ==== 接口实现 ====

        #region ISerializer 成员

        /// <summary>
        /// 使用指定的 System.IO.Stream 序列化指定的 System.Object。
        /// </summary>
        /// <param name="stream">用于保存序列化结果的 System.IO.Stream。</param>
        /// <param name="o">将要序列化的 System.Object。</param>
        /// <exception cref="System.InvalidOperationException">
        /// 序列化期间发生错误。使用 System.Exception.InnerException 属性时可使用原始异常。
        /// </exception>
        public void Serialize(Stream stream, object o)
        {
            if (o == null)
            {
                return;
            }

            DataFoundation data = o as DataFoundation;

            // 去掉声明
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(stream, settings);

            // 去掉命名空间
            var emptyNameSpace = new XmlSerializerNamespaces();
            emptyNameSpace.Add(string.Empty, string.Empty);

            // 开始序列化
            mySerializer.Serialize(writer, o, emptyNameSpace);
        }

        /// <summary>
        /// 反序列化指定 System.IO.Stream 包含的数据信息。
        /// </summary>
        /// <param name="stream">包含要反序列化的信息的 System.IO.Stream。</param>
        /// <returns>正被反序列化的 System.Object。</returns>
        public object Deserialize(Stream stream)
        {
            return mySerializer.Deserialize(stream);
        }

        #endregion

        #endregion
    }
}
