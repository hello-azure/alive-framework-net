/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */

using System;
using System.Reflection;
using System.Xml.Linq;

namespace Alive.Foundation.Data.Communication
{
    /// <summary>
    /// 通信消息接口
    /// </summary>
    public abstract class MessageBase
    {
        #region ==== 属性 ====

        /// <summary>
        /// 获得一个值表示发送者的ID
        /// </summary>
        public string Sender
        {
            get;
            set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 创建一个新的消息
        /// </summary>
        public MessageBase()
        { }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 从XML中读取消息
        /// </summary>
        /// <param name="xml">XML文本</param>
        /// <returns>解码获得的对象</returns>
        public static MessageBase FromXml(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            string assemblyName = doc.Root.Attribute("AttrAssemblyName").Value;
            string typeName = doc.Root.Attribute("AttrTypeName").Value;

            if (string.IsNullOrEmpty(assemblyName)
                || string.IsNullOrEmpty(typeName))
            {
                return null;
            }

            Assembly assembly = Assembly.Load(assemblyName);
            var result = assembly.CreateInstance(typeName) as MessageBase;

            if (result != null)
            {
                result.Sender = doc.Root.Attribute("AttrSender").Value;
                result.Decode(doc.Root);
            }

            return result;
        }

        /// <summary>
        /// 消息转化为XML
        /// </summary>
        /// <param name="message">要编码的对象</param>
        /// <returns>XML文本</returns>
        public static string ToXml(MessageBase message)
        {
            XDocument doc = new XDocument(message.Encode());

            if (doc.Root == null)
            {
                doc.Add(new XElement("Message"));
            }

            Type messageType = message.GetType();

            doc.Root.Add(
                new XAttribute("AttrSender", message.Sender),
                new XAttribute("AttrAssemblyName", messageType.Assembly.FullName),
                new XAttribute("AttrTypeName", messageType.FullName));

            return doc.ToString();
        }

        #endregion

        #region ==== 受保护方法 ====

        /// <summary>
        /// 对当前对象进行XML编码
        /// </summary>
        /// <returns>编码后的对象</returns>
        protected abstract XElement Encode();

        /// <summary>
        /// 对当前对象进行解码
        /// </summary>
        /// <param name="xml">编码的对象</param>
        protected abstract void Decode(XElement xml);

        #endregion
    }
}
