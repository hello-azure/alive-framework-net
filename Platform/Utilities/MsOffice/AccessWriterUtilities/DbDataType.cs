/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

namespace Alive.Foundation.Utilities.MsOffice
{
    /// <summary>
    /// 字段数据类型 对应 大小
    /// </summary>
    public enum DbDataType
    {
        Empty = 0,
        SmallInt = 2,
        Integer = 3,
        Single = 4,
        Double = 5,
        Currency = 6,
        Date = 7,
        BSTR = 8,
        IDispatch = 9,
        Error = 10,
        Boolean = 11,
        Variant = 12,
        IUnknown = 13,
        Decimal = 14,
        TinyInt = 16,
        UnsignedTinyInt = 17,
        UnsignedSmallInt = 18,
        UnsignedInt = 19,
        BigInt = 20,
        UnsignedBigInt = 21,
        FileTime = 64,
        GUID = 72,
        Binary = 128,
        Char = 129,
        WChar = 130,
        Numeric = 131,
        UserDefined = 132,
        DBDate = 133,
        DBTime = 134,
        DBTimeStamp = 135,
        Chapter = 136,
        PropVariant = 138,
        VarNumeric = 139,
        VarChar = 200,
        LongVarChar = 201,
        VarWChar = 202,
        LongVarWChar = 203,
        VarBinary = 204,
        LongVarBinary = 205,
    }
}
