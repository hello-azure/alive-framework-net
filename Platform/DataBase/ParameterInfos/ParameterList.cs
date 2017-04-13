using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alive.Foundation.Storage.ParameterInfos
{
    /// <summary>
    /// 数据库操作的参数信息列表。
    /// </summary>
    internal class ParameterList : Dictionary<string, ParameterInfo>
    {
        #region ==== 属性 ====

        /// <summary>
        /// 获得或设置指定名称的参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new ParameterInfo this[string key]
        {
            get
            {
                return base[ParameterInfo.FormateName(key)];
            }
            set
            {
                base[ParameterInfo.FormateName(key)] = value;
            }
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 将指定的参数添加到字典中
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="value">参数信息</param>
        public new void Add(string key, ParameterInfo value)
        {
            base.Add(ParameterInfo.FormateName(key), value);
        }

        /// <summary>
        /// 将指定的参数添加到字典中
        /// </summary>
        /// <param name="value">要添加的参数信息</param>
        public void Add(ParameterInfo value)
        {
            this.Add(value.Name, value);
        }

        /// <summary>
        /// 确定是否包含指定名称的参数。
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>如果列表中已经存在指定名称，则返回true。否则，返回false。</returns>
        public new bool ContainsKey(string key)
        {
            return base.ContainsKey(ParameterInfo.FormateName(key));
        }

        /// <summary>
        /// 从列表中移除指定名称的参数。
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>如果列表中已经存在指定名称，则返回true。否则，返回false。</returns>
        public new bool Remove(string key)
        {
            return base.Remove(ParameterInfo.FormateName(key));
        }

        /// <summary>
        /// 获取与指定名称相关联的参数信息。
        /// </summary>
        /// <param name="key">参数信息</param>
        /// <param name="value">如果参数存在，则返回参数信息。否则，返回null。</param>
        /// <returns>参数信息存在时，返回true，否则返回false</returns>
        public new bool TryGetValue(string key, out ParameterInfo value)
        {
            return base.TryGetValue(ParameterInfo.FormateName(key), out value);
        }

        #endregion
    }
}
