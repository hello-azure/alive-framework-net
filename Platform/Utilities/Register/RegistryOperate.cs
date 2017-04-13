/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */
using Microsoft.Win32;

namespace Alive.Foundation.Utilities.Register 
{
	/// <summary>
	/// 注册表操作
	/// </summary>
	public class RegistryOperate
    {
        #region ==== 静态方法 ====

        /// <summary>
		/// 注册表写入键值
		/// </summary>
		/// <param name="key">注册表键</param>
		/// <param name="value">注册表键值</param>
		public static bool Write(string key, string value)
        {
            try
            {
                RegistryKey rsg = null;

                if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\MICROSOFT").SubKeyCount <= 0)
                {
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\MICROSOFT");
                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\MICROSOFT");
                }

                rsg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\MICROSOFT", true);
                rsg.SetValue(key, value);
                rsg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\MICROSOFT", false);
                rsg.Close();

                return true;
            }
            catch
            {
                return false;
            }
		}

		/// <summary>
		/// 读取键值
		/// </summary>
		/// <param name="string">注册表键</param>
		/// <param name="value">注册表值</param>
		public bool Read(string key, out string value)
        {
            value = null;

            try
            {
                bool result = true;
                RegistryKey rsg = null;
                rsg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\MICROSOFT", true);

                if (rsg.GetValue(key) != null)
                {
                    value = rsg.GetValue(key).ToString();
                }
                else
                {
                    result = false;
                }

                rsg.Close();

                return result;
            }
            catch
            {
                return false;
            }
        }

        #endregion

    }

}