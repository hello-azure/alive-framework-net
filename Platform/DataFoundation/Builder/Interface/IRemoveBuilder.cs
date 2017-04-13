using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alive.Foundation.Data;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// DELETE（SQL）创建器的接口
    /// </summary>
    internal interface IRemoveBuilder
    {
        #region ==== 公共方法 ====

        /// <summary>
        /// 基于指定实体表对象,创建DELETE（SQL）
        /// </summary>
        /// <typeparam name="T">继承BusinessObject的类型</typeparam>
        /// <param name="t">实体对象</param>
        /// <returns>SQL字符串</returns>
        string Build<T>(T t) where T : BusinessObject, new();

        #endregion
    }
}
