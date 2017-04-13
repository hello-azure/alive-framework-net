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

namespace Alive.Foundation.Data
{
    /// <summary>
    /// 序列化编码解码工具的版本
    /// </summary>
    public sealed class SerializerVersion
    {
        #region ==== 常数 ====

        /// <summary>
        /// 0x00010001
        /// </summary>
        internal const int Xml0001 = 0x00010001;

        #endregion

        #region ==== 只读字段 ====

        #region XML 格式

        /// <summary>
        /// Xml格式的编码解码器。版本号0.01
        /// </summary>
        public static readonly int XmlVersion1 = Xml0001;

        /// <summary>
        /// Xml格式的默认编码解码器
        /// </summary>
        public static readonly int XmlDefault = SerializerVersion.XmlVersion1;

        #endregion

        /// <summary>
        /// 默认版本
        /// </summary>
        public static readonly int Default = SerializerVersion.XmlDefault;

        #endregion
    }
}
