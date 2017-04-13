/***********
 * ��Ȩ˵����
 *   ���ļ��� ����������ƽ̨ �����һ���֡�
 *   �汾��V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 ����һ��Ȩ��
 *   
 */
using Microsoft.Win32;

namespace Alive.Foundation.Utilities.Register 
{
	/// <summary>
	/// ע������
	/// </summary>
	public class RegistryOperate
    {
        #region ==== ��̬���� ====

        /// <summary>
		/// ע���д���ֵ
		/// </summary>
		/// <param name="key">ע����</param>
		/// <param name="value">ע����ֵ</param>
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
		/// ��ȡ��ֵ
		/// </summary>
		/// <param name="string">ע����</param>
		/// <param name="value">ע���ֵ</param>
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