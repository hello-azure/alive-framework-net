/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Alive.Foundation.Utilities.FileTool
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileOperator
    {
        #region ==== 公共方法 ====

        /// <summary>
        /// 保存信息到xml文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obje">实例</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool SaveXML<T>(T obje, string path)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(T));
                    xml.Serialize(fileStream, obje);
                }
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 从xml文件加载信息
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static T LoadXML<T>(string path)
        {
            T t = default(T);

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(T));
                    t = (T)xml.Deserialize(fileStream);

                }
            }
            catch
            {

            }

            return t;
        }

        /// <summary>
        /// 读取文件信息
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        public static List<string> Read(string filePath)
        {
            List<string> list = new List<string>();

            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    StreamReader reader = new StreamReader(fileStream, Encoding.Default);

                    while (!reader.EndOfStream)
                    {
                        list.Add(reader.ReadLine().Trim());
                    }
                }
            }
            catch
            {

            }

            return list;
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool CopyFile(string filePath)
        {
            return CopyFile(filePath, null);
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="copyPath">保存路径</param>
        /// <returns></returns>
        public static bool CopyFile(string filePath, string copyPath)
        {
            bool result = false;

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string copy = string.IsNullOrEmpty( copyPath) ? fileInfo.Name : copyPath;
                fileInfo.CopyTo(copy, true);
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        #endregion
    }
}
