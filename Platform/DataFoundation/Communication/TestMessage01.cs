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
using System.Xml.Linq;

namespace Alive.Foundation.Data.Communication
{
    #region ==== 示例1 ====
    ///// <summary>
    ///// 黑白名单同步消息
    ///// </summary>
    //public class BlackWhiteListMessage : MessageBase
    //{
    //    #region ==== 私有字段 ====

    //    /// <summary>
    //    /// 黑白名单所属数据类型
    //    /// </summary>
    //    private string dataName = string.Empty;

    //    /// <summary>
    //    /// 要加入黑白名单的条件的 XML 字符串形式
    //    /// </summary>
    //    private string conditionXml = string.Empty;

    //    #endregion

    //    #region ==== 属性 ====

    //    /// <summary>
    //    /// 获得或设置一个值，表示当前条件为增加（true）或移除（false）
    //    /// </summary>
    //    public bool AddOrRemove
    //    {
    //        get;
    //        set;
    //    }

    //    #endregion

    //    #region ==== 受保护方法 ====

    //    #region 编码解码

    //    protected override System.Xml.Linq.XElement Encode()
    //    {
    //        return new XElement("BlackWhiteList",
    //             new XAttribute("dataName", this.dataName),
    //             new XAttribute("action", this.AddOrRemove ? "add" : "remove"),
    //             new XCData(this.conditionXml));
    //    }

    //    protected override void Decode(System.Xml.Linq.XElement xml)
    //    {
    //        this.dataName = xml.Attribute("dataName").Value;
    //        this.AddOrRemove = xml.Attribute("action").Value == "add";
    //        this.conditionXml = xml.Value;
    //    }

    //    #endregion

    //    #endregion
    //}

    #endregion
}
