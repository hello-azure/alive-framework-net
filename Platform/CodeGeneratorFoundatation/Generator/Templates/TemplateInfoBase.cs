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
using Alive.Tools.CodeGenerator.Foundatation.Generator.Decorators;

namespace Alive.Tools.CodeGenerator.Foundatation.Generator.Templates
{
    /// <summary>
    /// 模板信息抽象类
    /// </summary>
    public abstract class TemplateInfoBase
    {
        #region ==== 抽象属性 ====

        /// <summary>
        /// 模板文件头注释设置
        /// </summary>
        public virtual List<string> STitleComments
        {
            get;
            protected set;
        }

        /// <summary>
        /// 模板文件头注释设置
        /// </summary>
        public virtual List<string> SUsings
        {
            get;
            protected set;
        }

        /// <summary>
        /// 模板命名空间名字设置
        /// </summary>
        public virtual string SNameSpace
        {
            get;
            protected set;
        }

        /// <summary>
        /// 模板类访问权限设置
        /// </summary>
        public virtual QualifierValue SClassVisibility
        {
            get;
            protected set;
        }

        /// <summary>
        /// 模板类属性设置
        /// </summary>
        public virtual List<string> SAttributes
        {
            get;
            protected set;
        }

        /// <summary>
        /// 模板继承的基类或接口设置
        /// </summary>
        public virtual string SBaseClass
        {
            get;
            protected set;
        }

        /// <summary>
        /// 模板类注释信息设置
        /// </summary>
        public virtual List<string> SDocumentComment
        {
            get;
            protected set;
        }

        /// <summary>
        /// 类或枚举或接口名称
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        #endregion
    }

    /// <summary>
    /// 构造器信息
    /// </summary>
    internal class ConstructorInfo
    {
        #region ==== 属性 ====

        /// <summary>
        /// 构造器访问可见性
        /// </summary>
        internal QualifierValue Visibility
        {
            get;
            set;
        }

        /// <summary>
        /// 构造器参数类型
        /// </summary>
        internal ParaType ParaType
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数参数类型
        /// </summary>
        internal DataType ParaDataType
        {
            get;
            set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        internal ConstructorInfo()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="visibility">构造器访问可见性</param>
        /// <param name="paraType">构造器参数类型</param>
        internal ConstructorInfo(QualifierValue visibility, ParaType paraType)
        {
            this.Visibility = visibility;
            this.ParaType = paraType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="visibility">构造器访问可见性</param>
        /// <param name="paraType">构造器参数类型</param>
        /// <param name="paraDataType">构造器参数数据类型</param>
        internal ConstructorInfo(QualifierValue visibility, ParaType paraType, DataType paraDataType)
        {
            this.Visibility = visibility;
            this.ParaType = paraType;
            this.ParaDataType = paraDataType;
        }

        #endregion
    }

    internal class PropertyInfo
    {
        #region ==== 公共属性 ====

        /// <summary>
        /// 模板属性可见性设置
        /// </summary>
        internal QualifierValue Visibility
        {
            get;
            set;
        }

        /// <summary>
        /// 模板属性是否注释
        /// </summary>
        internal bool IsComment
        {
            get;
            set;
        }

        /// <summary>
        /// 模板属性数据类型
        /// </summary>
        internal DataType DataType
        {
            get;
            set;
        }

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        internal PropertyInfo()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Visibility">模板属性可见性</param>
        /// <param name="DataType">模板属性数据类型</param>
        /// <param name="IsComment">模板属性是否注释</param>
        internal PropertyInfo(QualifierValue visibility, DataType dataType, bool isComment)
        {
            this.Visibility = visibility;
            this.DataType = dataType;
            this.IsComment = isComment;
        }

        #endregion
    }

    /// <summary>
    /// 参数类型
    /// </summary>
    internal enum ParaType
    { 
        /// <summary>
        /// 默认无参
        /// </summary>
        Default,

        /// <summary>
        /// 基于字段全参
        /// </summary>
        Full
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    internal enum DataType
    {
        /// <summary>
        /// C#标准数据类型
        /// </summary>
        Starndard,

        /// <summary>
        /// Alive平台强类型
        /// </summary>
        DataField
    }
}
