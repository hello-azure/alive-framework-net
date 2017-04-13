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
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// 为列表的BusinessObject提供统一的泛型
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class BoList<TItem> : BusinessObject, IBoList, IList<TItem>, IXmlSerializable where TItem : BusinessObject, new()
    {
        #region       ====私有成员===

        private List<TItem> items = new List<TItem>();

        #endregion

        #region ==== 接口实现 ====

        #region IBoList 成员

        /// <summary>
        /// 在列表中创建一个新的TItem类型的实例，并且将其返回。
        /// </summary>
        /// <returns>TItem类型的新的实例</returns>
        public BusinessObject Add()
        {
            TItem result = new TItem();
            this.Add(result);
            return result;
        }

        #endregion

        #region      IXmlSerializable 成员
        /// <summary>
        /// 此方法是保留方法，请不要使用。
        /// 如果需要指定自定义架构，应向该类应用 XmlSchemaProviderAttribute。
        /// </summary>
        /// <returns>这个方法为IXmlSerializable接口的保留方法。除了return null之外，什么都不做。</returns>
        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }
        /// <summary>
        /// 从对象的 XML 表示形式生成该对象。
        /// </summary>
        /// <param name="reader">对象从中进行反序列化的 XmlReader 流。</param>
        public virtual void ReadXml(XmlReader reader)
        {
            reader.Read();

            while (reader.NodeType == XmlNodeType.Element)
            {
                string nodeXml = reader.ReadOuterXml();
                items.Add(DataSerializer.Decode<TItem>(nodeXml));
            }
        }
        /// <summary>
        /// 将对象转换为其 XML 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 XmlWriter 流。</param>
        public virtual void WriteXml(XmlWriter writer)
        {
            foreach (TItem list in items)
            {
                string nodeXml = DataSerializer.Encode(list);
                writer.WriteRaw(nodeXml);
            }
        }

        #endregion

        #region IList<TItem> 成员
        /// <summary>
        /// 确定 IList 中特定项的索引。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int IndexOf(TItem item)
        {
            return this.items.IndexOf(item);

        }
        /// <summary>
        /// 将一个项插入指定索引处的 IList。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public virtual void Insert(int index, TItem item)
        {
            this.items.Insert(index, item);
        }
        /// <summary>
        /// 移除指定索引处的 IList 项。
        /// </summary>
        /// <param name="index"></param>
        public virtual void RemoveAt(int index)
        {
            this.items.RemoveAt(index);
        }
        /// <summary>
        /// 获取或设置指定索引处的元素。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual TItem this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                items[index] = value;
            }
        }

        #endregion

        #region ICollection<TItem> 成员
        /// <summary>
        /// 将item值依次添加进IList类型的items中
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(TItem item)
        {
            this.items.Add(item);
        }
        /// <summary>
        /// 执行清除功能
        /// </summary>
        public virtual void Clear()
        {
            this.items.Clear();
        }
        /// <summary>
        /// 检测item值是否包含在items中
        /// </summary>
        /// <param name="item"></param>
        /// <returns> 返回bool型的检测结果</returns>
        public virtual bool Contains(TItem item)
        {
            return this.items.Contains(item);
        }
        /// <summary>
        /// 从特定的 Items 索引处开始，将 ICollection 的元素复制到一个 Items 中。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public virtual void CopyTo(TItem[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// 获取 ICollection 中包含的元素数。
        /// </summary>
        public virtual int Count
        {
            get { return this.items.Count; }
        }
        /// <summary>
        /// 判断数据是否只读类型
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// 从Items中删除指定的item数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Remove(TItem item)
        {
            return this.items.Remove(item);
        }

        #endregion

        #region IEnumerable<TItem> 成员
        /// <summary>
        /// 返回一个循环访问集合的枚举数。
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator<TItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员
        /// <summary>
        /// 将GetEnumerator()重写，返回一个循环访问集合的枚举数。  
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (items as System.Collections.IEnumerable).GetEnumerator();

        }

        #endregion

        #endregion
    }
}
