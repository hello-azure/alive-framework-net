/***********
 * 版权说明：
 *   本文件是 Somme 服务程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TestData;
using Alive.Foundation.Data;

namespace TestStorage.Common.Tables
{
    /// <summary>
    ///DAttachmentInfo
    /// </summary>
    [Serializable]
    public class TDAttachmentInfo : BaseTable<UserInfo>
    {
        #region ==== 重写基类方法 ====

        /// <summary>
        /// 操作表名
        /// </summary>
        protected override TableName TableName
        {
            get
            {
                return TableName.UserInfo;
            }
        }

        #endregion

        void a()
        {
            Action<UserInfo> condition = (data) =>
                {

                };

            Func<UserInfo, string> condition1 = (data) =>
            {
                return "";
            };

            UserInfo u = new UserInfo();

            DataCondition c1 = new DataCondition();
            c1.SetCondition("ID", ConditionOperate.等于, 3,ConditionRelation.或);
           

        }

    }

    public class DataCondition
    {
        public void SetCondition(object filedName,ConditionOperate operate,object value)
        { 
        
        }

        public void SetCondition(object filedName, ConditionOperate operate, object value,ConditionRelation relation)
        {

        }
    }

    public enum ConditionOperate
    { 
        大于,小于,等于,大于等于,小于等于
    }

    public enum ConditionRelation
    { 
        与,或
    }
}