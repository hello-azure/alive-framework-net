/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Alive.Foundation.Data
{
    /// <summary>
    /// 系统内通讯时数据的编码解码工具
    /// </summary>
    /// <typeparam name="T">要序列化的数据类型</typeparam>
    public sealed class DataSerializer
    {
        #region ==== 公有方法 ====

        #region 编码

        /// <summary>
        /// 序列化指定的<paramref name="data"/>。
        /// </summary>
        /// <param name="data">要进行序列化的数据实例</param>
        /// <returns>序列化得到的字符串</returns>
        public static string Encode(object data)
        {
            return Encode(data, SerializerVersion.Default);
        }

        /// <summary>
        /// 使用指定版本的编码器，序列化指定的<paramref name="data"/>。
        /// </summary>
        /// <param name="data">要进行序列化的数据实例</param>
        /// <param name="version">要使用的编码器版本</param>
        /// <returns>序列化得到的字符串</returns>
        public static string Encode(object data, int version)
        {
            MemoryStream stream = new MemoryStream();
            StreamReader reader = new StreamReader(stream);

            Encode(data, stream, version);

            stream.Position = 0;

            return reader.ReadToEnd();
        }

        /// <summary>
        /// 序列化指定的<paramref name="data"/> 到指定的数据流中。
        /// </summary>
        /// <param name="data">要进行序列化的数据实例</param>
        /// <param name="output">接收序列化的数据流</param>
        public static void Encode(object data, Stream output)
        {
            Encode(data, output, SerializerVersion.Default);
        }

        /// <summary>
        /// 使用指定版本的编码器，序列化指定的<paramref name="data"/> 到指定的数据流中。
        /// </summary>
        /// <param name="data">要进行序列化的数据实例</param>
        /// <param name="output">接收序列化的数据流</param>
        /// <param name="version">要使用的编码器版本</param>
        public static void Encode(object data, Stream output, int version)
        {
            ISerializer serializer = GetSerializer(version, data.GetType());

            serializer.Serialize(output, data);
        }

        #endregion

        #region 解码

        /// <summary>
        /// 反序列化指定的<paramref name="stream"/>。
        /// </summary>
        /// <param name="stream">要进行反序列化的字符串</param>
        /// <returns>反序列化后得到的数据实例</returns>
        /// <typeparam name="T">要反序列化的数据类型</typeparam>
        public static T Decode<T>(string stream)
        {
            return Decode<T>(stream, SerializerVersion.Default);
        }

        /// <summary>
        /// 反序列化指定的<paramref name="stream"/>。
        /// </summary>
        /// <param name="stream">要进行反序列化的字符串</param>
        /// <param name="objectType">要反序列化的对象类型</param>
        /// <returns>反序列化后得到的数据实例</returns>
        public static object Decode(string stream, Type objectType)
        {
            return Decode(stream, SerializerVersion.Default, objectType);
        }

        /// <summary>
        /// 反序列化指定的<paramref name="stream"/>。
        /// </summary>
        /// <param name="stream">要进行反序列化的流</param>
        /// <returns>反序列化后得到的数据实例</returns>
        /// <typeparam name="T">要反序列化的数据类型</typeparam>
        public static T Decode<T>(Stream stream)
        {
            return Decode<T>(stream, SerializerVersion.Default);
        }

        /// <summary>
        /// 反序列化指定的<paramref name="stream"/>。
        /// </summary>
        /// <param name="stream">要进行反序列化的流</param>
        /// <param name="objectType">要反序列化的对象类型</param>
        /// <returns>反序列化后得到的数据实例</returns>
        public static object Decode(Stream stream, Type objectType)
        {
            return Decode(stream, SerializerVersion.Default, objectType);
        }

        /// <summary>
        /// 使用指定版本的解码器，反序列化指定的<paramref name="stream"/>。
        /// </summary>
        /// <param name="stream">要进行反序列化的字符串</param>
        /// <param name="version">要使用的解码器版本</param>
        /// <returns>反序列化后得到的数据实例</returns>
        /// <typeparam name="T">要反序列化的数据类型</typeparam>
        public static T Decode<T>(string stream, int version)
        {
            MemoryStream dataStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(dataStream);

            writer.WriteLine(stream);
            writer.Flush();
            dataStream.Position = 0;

            return Decode<T>(dataStream, version);
        }

        /// <summary>
        /// 使用指定版本的解码器，反序列化指定的<paramref name="stream"/>。
        /// </summary>
        /// <param name="stream">要进行反序列化的字符串</param>
        /// <param name="version">要使用的解码器版本</param>
        /// <param name="objectType">要反序列化的对象类型</param>
        /// <returns>反序列化后得到的数据实例</returns>
        public static object Decode(string stream, int version, Type objectType)
        {
            MemoryStream dataStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(dataStream);

            writer.WriteLine(stream);
            writer.Flush();
            dataStream.Position = 0;

            return Decode(dataStream, version, objectType);
        }

        /// <summary>
        /// 使用指定版本的解码器，反序列化指定的<paramref name="stream"/>。
        /// </summary>
        /// <param name="stream">要进行反序列化的流</param>
        /// <param name="version">要使用的解码器版本</param>
        /// <returns>反序列化后得到的数据实例</returns>
        /// <typeparam name="T">要反序列化的数据类型</typeparam>
        public static T Decode<T>(Stream stream, int version)
        {
            T result = (T)Decode(stream, version, typeof(T));
            return result;
        }

        /// <summary>
        /// 使用指定版本的解码器，反序列化指定的<paramref name="stream"/>成为指定类型的对象。
        /// </summary>
        /// <param name="stream">要进行反序列化的流</param>
        /// <param name="version">要使用的解码器版本</param>
        /// <param name="objectType">要反序列化的对象类型</param>
        /// <returns>反序列化后得到的数据实例</returns>
        public static object Decode(Stream stream, int version, Type objectType)
        {
            ISerializer serializer = GetSerializer(version, objectType);
            return serializer.Deserialize(stream);

        }

        #endregion

        #endregion ^^ 公有方法 ^^

        #region ==== 私有方法 ====

        /// <summary>
        /// 获得指定版本的编解码器
        /// </summary>
        /// <param name="version">编解码器的版本编号</param>
        /// <returns></returns>
        private static ISerializer GetSerializer(int version, Type objectType)
        {
            ISerializer result = null;

            switch (version)
            {
                case SerializerVersion.Xml0001:
                    result = new Xml0001(objectType);
                    break;
                default:
                    result = GetSerializer(SerializerVersion.Default, objectType);
                    break;
            }

            return result;
        }

        #endregion
    }
}
