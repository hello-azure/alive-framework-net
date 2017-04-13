/***********
 * 版权说明：
 *   本文件是 Somme 服务程序的一部分。
 *   版本：V 1.0
 *   Copyright 万物生创意软件工作室 2013 保留一切权利
 *   
 */


namespace Alive.Engine.Search.Compiler
{
    /// <summary>
    /// 语法编译结果
    /// </summary>
    internal class CompileResult
    {
        #region ==== 构造函数 ====

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CompileResult()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isSuccessful">是否成功</param>
        public CompileResult(bool isSuccessful)
        {
            this.IsSuccessful = isSuccessful;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isSuccessful">是否成功</param>
        /// <param name="compiledSearchCondition">编译后的查询条件</param>
        public CompileResult(bool isSuccessful, string compiledSearchCondition)
        {
            this.IsSuccessful = isSuccessful;
            this.CompiledSearchCondition = compiledSearchCondition;
        }

          /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isSuccessful">是否成功</param>
        /// <param name="errorCode">错误的代码</param>
        /// <param name="compiledSearchCondition">编译后的查询条件</param>
        public CompileResult(bool isSuccessful, string errorCode, string compiledSearchCondition)
        {
            this.IsSuccessful = isSuccessful;
            this.ErrorCode = errorCode;
            this.CompiledSearchCondition = compiledSearchCondition;
        }

        #endregion

        #region ==== 内部属性 ====

        /// <summary>
        /// 获得或设置一个值，用来表示当前查询操作是否陈宫
        /// </summary>
        public bool IsSuccessful
        {
            get;
            set;
        }

        /// <summary>
        /// 获得或设置当前操作返回的错误的代码
        /// </summary>
        public string ErrorCode
        {
            get;
            set;
        }

        /// <summary>
        /// 编译后的查询条件
        /// </summary>
        public string CompiledSearchCondition
        {
            get;
            set;
        }

        #endregion
    }
}
