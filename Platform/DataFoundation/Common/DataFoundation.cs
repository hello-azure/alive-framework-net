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
using System.Reflection;
using System.Xml.Linq;
using System.IO;
using Alive.Foundation.Data.DataFields;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// 跨界数据基础类
    /// </summary>
    [Serializable]
    public class DataFoundation
    {
        #region ==== 似有字段 ====

        /// <summary>
        /// 用于存储各个业务字段对象的字典。
        /// </summary>
        private readonly Dictionary<string, DataField> fields = new Dictionary<string, DataField>();

        /// <summary>
        /// 用于拷贝的反射信息
        /// </summary>
        private static Dictionary<string, Dictionary<string, PropertyInfo>> copyMap =
            new Dictionary<string,Dictionary<string,PropertyInfo>>();

        #endregion

        #region ==== 公有方法 ====

        #region Copy

        /// <summary>
        /// 将数据拷贝到<paramref name="aim"/>指定的数据对象。
        /// 该方法会或略所有标记有“CopyIgnoreAttribute”的属性，及全部索引器。
        /// 需要定义拷贝索引器的逻辑，请重写CopyIndexe。
        /// 需要定义某个字段的拷贝逻辑，请重写GetCopySource。
        /// </summary>
        /// <typeparam name="TData">目标数据类型</typeparam>
        /// <param name="aim">目标数据对象</param>
        /// <returns>返回一个值，表示拷贝的字段数。</returns>
        public int CopyDataTo<TData>(TData aim) where TData : DataFoundation
        {
            if (aim == null)
            {
                return 0;
            }

            return this.ActualCopyDataTo(aim);
        }

        #endregion

        #region 序列化

        /// <summary>
        /// 将当前对象转化为XElement
        /// </summary>
        /// <returns>XElement 对象</returns>
        public XElement ToXElement()
        {
            using (MemoryStream xml = new MemoryStream())
            {
                DataSerializer.Encode(this, xml);
                xml.Position = 0;

                return XElement.Load(xml);
            }
        }

        #endregion

        #endregion

        #region ==== 受保护方法 ====

        #region Copy

        /// <summary>
        /// 获得当前要拷贝的值。
        /// </summary>
        /// <param name="from">要拷贝的属性信息</param>
        /// <returns>要拷贝的值。</returns>
        protected virtual object GetCopySource(PropertyInfo from)
        {
            object result = from.GetValue(this, null);

            if (from.PropertyType.IsSubclassOf(typeof(DataFoundation)))
            {
                object newInstance = from.PropertyType.Assembly.CreateInstance(from.PropertyType.FullName);

                (result as DataFoundation).ActualCopyDataTo(newInstance);

                result = newInstance;
            }

            return result;
        }

        /// <summary>
        /// 拷贝索引器。如果项定义拷贝索引器的方法，请重写本方法。
        /// </summary>
        /// <typeparam name="TItem">索引器的类型</typeparam>
        /// <param name="from">拷贝数据源</param>
        /// <param name="to">拷贝目标</param>
        /// <param name="aim">拷贝目标所在的对象</param>
        /// <returns>返回一个值表示是否完成了拷贝</returns>
        protected virtual bool CopyIndexer(PropertyInfo from, PropertyInfo to, DataFoundation aim)
        {
            // 基类中忽略索引器。
            return false;
        }

        #endregion

        #region === GetField ====

        /// <summary>
        /// 获得<paramref name="name"/>所指定的业务字段对象。如果指定业务字段对象不存在，则创建一个新的。
        /// </summary>
        /// <typeparam name="FieldType">要获得的字段的类型</typeparam>
        /// <param name="name">要获得的字段的名称</param>
        /// <returns>获得的FieldType对象</returns>
        protected FieldType GetField<FieldType>(string name) where FieldType : DataField, new()
        {
            return this.GetField<FieldType>(name, false);
        }

        /// <summary>
        /// 获得参数<paramref name="name"/>所指定的业务字段对象。
        /// </summary>
        /// <typeparam name="FieldType">要获得的字段的类型</typeparam>
        /// <param name="name">要获得的字段的名称</param>
        /// <param name="canReturnNull">当指定的业务字段对象不存在时，false表示创建一个新的对象并返回，true表示直接返回null。</param>
        /// <returns>获得的FieldType对象</returns>
        protected FieldType GetField<FieldType>(string name, bool canReturnNull) where FieldType : DataField, new()
        {
            FieldType result = null;

            lock (this.fields)
            {
                if (this.fields.ContainsKey(name))
                {
                    result = this.fields[name] as FieldType;
                }
                else
                {
                    if (!canReturnNull)
                    {
                        this.fields.Add(name, new FieldType());
                        result = this.fields[name] as FieldType;
                    }
                }
            }

            return result;
        }

        #endregion

        #endregion

        #region ==== 似有方法 ====

        /// <summary>
        /// 将数据拷贝到<paramref name="aim"/>指定的数据对象。
        /// </summary>
        /// <param name="aim">目标数据对象</param>
        /// <returns>返回一个值，表示拷贝的字段数。</returns>
        private int ActualCopyDataTo(object aim)
        {
                int result = 0;

            var fromList = GetPropertyList(this.GetType());
            var toList = GetPropertyList(aim.GetType());

            var validList = from fromName in fromList.Keys
                            where toList.ContainsKey(fromName)
                            select fromName;

            foreach (var validName in validList)
            {
                var toItem = toList[validName];
                var fromItem = fromList[validName];
                var indexer = toItem.GetIndexParameters();
                object sourceValue = this.GetCopySource(fromItem);

                if (indexer.Length == 0)
                {
                    toItem.SetValue(
                        aim,
                        fromItem.GetValue(this, null),
                        null);

                    result++;
                }
                else
                {
                    if (this.CopyIndexer(fromItem, toItem, aim as DataFoundation))
                    {
                        result++;
                    }
                }
            }

            return result;
    }

        /// <summary>
        /// 获得制定类型的拷贝清单。
        /// </summary>
        /// <param name="type">要获得拷贝清单的类型</param>
        /// <returns>拷贝清单</returns>
        private static Dictionary<string, PropertyInfo> GetPropertyList(Type type)
        {
            lock (copyMap)
            {
                if (copyMap.ContainsKey(type.FullName))
                {
                    return copyMap[type.FullName];
                }

                Dictionary<string, PropertyInfo> result = new Dictionary<string, PropertyInfo>();

                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var ignore = property.GetCustomAttributes(typeof(CopyIgnoreAttribute), true);

                    if (ignore.Length == 0)
                    {
                        var nameAttr = property.GetCustomAttributes(typeof(BusinessFieldAttribute), true);

                        result.Add(
                            nameAttr.Length > 0 ? (nameAttr[0] as BusinessFieldAttribute).Name : property.Name,
                            property);
                    }
                }

                copyMap.Add(type.FullName, result);

                return result;
            }
        }

        #endregion

    }
}
