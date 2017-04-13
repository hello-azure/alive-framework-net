/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 保留一切权利
 *   
 */

using System.Xml.Linq;

namespace Alive.Foundation.Data.Communication
{
    #region ==== 示例2 ====

    ///// <summary>
    ///// 测试消息类
    ///// </summary>
    //public class TempMessage : MessageBase
    //{
    //    #region ==== 属性 ====

    //    /// <summary>
    //    ///  消息
    //    /// </summary>
    //    public string Message
    //    {
    //        get;
    //        set;
    //    }

    //    #endregion

    //    #region ==== 受保护方法 ====

    //    #region 编码解码

    //    protected override System.Xml.Linq.XElement Encode()
    //    {
    //        return new XElement("TestMessage", this.Message);
    //    }

    //    protected override void Decode(System.Xml.Linq.XElement xml)
    //    {
    //        this.Message = xml.Value;
    //    }

    //    #endregion

    //    #endregion
    //}
    #endregion
}
