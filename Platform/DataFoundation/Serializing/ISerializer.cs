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
using System.IO;

namespace Alive.Foundation.Data
{
    internal interface ISerializer
    {
        /// <summary>
        /// 使用指定的 System.IO.Stream 序列化指定的 System.Object。
        /// </summary>
        /// <param name="stream">用于保存序列化结果的 System.IO.Stream。</param>
        /// <param name="o">将要序列化的 System.Object。</param>
        /// <exception cref="System.InvalidOperationException">
        /// 序列化期间发生错误。使用 System.Exception.InnerException 属性时可使用原始异常。
        /// </exception>
        void Serialize(Stream stream, object o);
        /// <summary>
        /// 反序列化指定 System.IO.Stream 包含的数据信息。
        /// </summary>
        /// <param name="stream">包含要反序列化的信息的 System.IO.Stream。</param>
        /// <returns>正被反序列化的 System.Object。</returns>
        object Deserialize(Stream stream);
    }
}
