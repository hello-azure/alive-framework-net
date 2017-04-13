/***********
 * 版权声明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators
{
    /// <summary>
    /// 限定符枚举
    /// </summary>
    public enum QualifierValue
    {
        Null,
        Public,
        Internal,
        Protected,
        Private,
        InternalProtected,

        Static,

        Override,
        Virtual,
        Abstract,

        Class,
        Struct,
        Enum,
        Interface
    }
}
