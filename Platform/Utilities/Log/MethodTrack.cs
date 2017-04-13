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

namespace Alive.Foundation.Utilities.Log
{
    public class MethodTrack : IDisposable
    {
        public string MethodName
        {
            get;
            protected set;
        }

        public Type TypeInfo
        {
            get;
            protected set;
        }

        internal MethodTrack(Type t, string methodName)
        {
            this.MethodName = methodName;
            this.TypeInfo = t;
            GlobalLogger<MethodTrack>.Debug("进入 '{0}' 类的'{1}' 方法", t.FullName, methodName);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GlobalLogger<MethodTrack>.Debug("离开 '{0}' 类的'{1}' 方法", this.TypeInfo.FullName, this.MethodName);
        }

        #endregion
    }
}
