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

namespace Alive.Foundation.Data
{
    /// <summary>
    /// 表示实现该接口的类是一个BusinessObject的列表
    /// </summary>
    public interface IBoList
    {
        /// <summary>
        /// 添加一个新的BusinessObject对象到列表中。
        /// </summary>
        /// <returns>新建的BusinessObject的实例</returns>
        BusinessObject Add();
    }
}
