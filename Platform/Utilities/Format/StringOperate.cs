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

namespace Alive.Foundation.Utilities.Format
{
    /// <summary>
    /// 该类是String类型的扩展方法集
    /// </summary>
    public static class StringOperate
    {
        /// <summary>
        /// 将字符串的第一个字符转换小写字符
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>字符串</returns>
        public static string ToFirstCharLower(this string text)
        {
            return string.Format("{0}{1}", text.Substring(0, 1).ToLower(), text.Substring(1, text.Length - 1));
        }
    }
}
