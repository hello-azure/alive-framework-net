﻿/***********
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
    /// 所有BO视图生成器的基类。
    /// 系统中各个模块可以根据需要继承该类，用于根据BO生成视图或其他数据类型。
    /// </summary>
    public abstract class BoViewBuilder<TView>
    {
        #region ==== 私有字段 ====

        private Dictionary<string, string> nameMapping = null;

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 获得当前注射器的字段名称地图。
        /// </summary>
        protected Dictionary<string, string> NameMapping
        {
            get
            {
                if (nameMapping == null)
                {
                    nameMapping = new Dictionary<string, string>();
                }

                return nameMapping;
            }
        }

        /// <summary>
        /// 获得一个值，用于标记NameMapping的key和value的含义。
        /// </summary>
        /// <value>
        /// false：key为字段名称（fieldName），value为别名（alias）。
        /// true：key为别名（alias），value为字段名称（fieldName）。
        /// </value>
        protected virtual bool IsAliasKey
        {
            get { return false; }
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 设置字段的别名。
        /// </summary>
        /// <param name="fieldName">要设置的字段名。字段名为空时会触发异常。</param>
        /// <param name="alias">要设置的字段别名。字段别名为空时会触发异常。</param>
        public void SetFieldMapping(string fieldName, string alias)
        {
            if (this.IsAliasKey)
            {
                this.SetAFieldMapping(alias, fieldName);
            }
            else
            {
                this.SetAFieldMapping(fieldName, alias);
            }
        }

        /// <summary>
        /// 初始化所有字段别名
        /// </summary>
        /// <typeparam name="TBo">将要注入的BO对象的类型</typeparam>
        public void InitFieldMapping<TBo>() where TBo : BusinessObject, new()
        {
            TBo bo = new TBo();
            InitFieldMapping(bo);
        }

        /// <summary>
        /// 初始化所有字段别名
        /// </summary>
        /// <param name="bo">将要注入的BO对象</param>
        public void InitFieldMapping(BusinessObject bo)
        {
            var fieldList = bo.GetNameMapping();

            foreach (var fieldName in fieldList)
            {
                this.SetFieldMapping(fieldName, fieldName);
            }
        }

        /// <summary>
        /// 生成BO所对应的视图
        /// </summary>
        /// <returns>生成得到的视图对象</returns>
        public abstract TView Build();

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 设置字段名和字段别名的对应关系。
        /// </summary>
        /// <param name="key">对应关系的键。</param>
        /// <param name="value">对应值。</param>
        private void SetAFieldMapping(string key, string value)
        {
            if (this.NameMapping.ContainsKey(key))
            {
                this.NameMapping[key] = value;
            }
            else
            {
                this.NameMapping.Add(key, value);
            }
        }

        #endregion

        #region ==== 类型定义 ====

        /// <summary>
        /// 用于注射器子类设置BO使用
        /// </summary>
        protected class BoGetter
        {
            #region ==== 私有字段 ====

            BusinessObject bo = null;

            #endregion

            #region ==== 属性/索引器 ====

            /// <summary>
            /// 设置或获得指定字段的设置
            /// </summary>
            /// <param name="fieldName">要设置的字段名</param>
            /// <returns>一个包含指定字段的设置信息的BoFieldSetter的新的对象</returns>
            public BoFieldGetter this[string fieldName]
            {
                get
                {
                    return new BoFieldGetter(this.bo, fieldName);
                }
            }

            #endregion

            #region ==== 构造函数 ====

            /// <summary>
            /// 创建一个BoSetter的新的实例
            /// </summary>
            /// <param name="bo"></param>
            public BoGetter(BusinessObject bo)
            {
                this.bo = bo;
                this.bo.Initialize();
            }

            #endregion

        }

        /// <summary>
        /// 用于注射器子类设置BO的业务字段使用
        /// </summary>
        protected class BoFieldGetter
        {
            #region ==== 私有字段 ====

            BusinessObject bo = null;
            string fieldName = string.Empty;

            #endregion

            #region ==== 构造函数 ====

            public BoFieldGetter(BusinessObject bo, string fieldName)
            {
                this.bo = bo;
                this.fieldName = fieldName;
            }

            #endregion

            #region ==== 属性 ====

            /// <summary>
            /// 设置或获得业务字段值的字符串形式
            /// </summary>
            public string Text
            {
                get
                {
                    return this.bo.GetValueText(this.fieldName);
                }
            }

            /// <summary>
            /// 设置或获得业务字段值的对象
            /// </summary>
            public object Object
            {
                get
                {
                    return this.bo.GetValueObject(this.fieldName);
                }
            }

            #endregion

            #region ==== 公有方法 ====

            /// <summary>
            /// 获得字段的值的类型
            /// </summary>
            /// <returns>表示字段的值的类型的 Type 对象</returns>
            public Type GetValueType()
            {
                return this.bo.GetValueType(this.fieldName);
            }

            #endregion
        }

        #endregion
    }
}
