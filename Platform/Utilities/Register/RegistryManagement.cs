/***********
 * 版权说明：
 *   本文件是 万物生基础平台 程序的一部分。
 *   版本：V 1.0
 *   Copyright AliveSoft Xiaoqiang.HE 2013 保留一切权利
 *   
 */
using System;
using System.Security.Cryptography;
using System.Text;

namespace Alive.Foundation.Utilities.Register
{
	/// <summary>
	/// 软件注册管理
	/// </summary>
	public class RegistryManagement
    {

        #region ==== 私有字段 ====

        /// <summary>
		/// 公钥
		/// </summary>
		private static readonly string pubKey = "<RSAKeyValue><Modulus>r/mGlsX+SFZaKmO0/fNewpbebQzuqAGxloAb3o+vK9Jz1RLFfDh9isg60sX6uUYGBku3xri7kiinFtOVjVYS4/EcSLDqufesm3Srj8B9IMBk8SirVZwspEQq2NxguzgN6HdNmp3OrgKzg1f0humWFGXn7pDOZIj7bydwGRWOpf0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
		
        /// <summary>
		/// 私钥
		/// </summary>
        private static readonly string priKey = "<RSAKeyValue><Modulus>r/mGlsX+SFZaKmO0/fNewpbebQzuqAGxloAb3o+vK9Jz1RLFfDh9isg60sX6uUYGBku3xri7kiinFtOVjVYS4/EcSLDqufesm3Srj8B9IMBk8SirVZwspEQq2NxguzgN6HdNmp3OrgKzg1f0humWFGXn7pDOZIj7bydwGRWOpf0=</Modulus><Exponent>AQAB</Exponent><P>8odnLl3ZjmNiFx8lBD0X3XSyjzU03+nRRe/WqGS1JrzrNrSFr95hSEWDgOBZSqHM1RzKaA4R4GgGOgKGfSJ3oQ==</P><Q>ub/EVRfg9QTJn4+ZfeJAWmwKhyqM5pHyT1cje3UAR0OhzDO/g0gG3XPq/SX7TNJsH2F4rAvQolVA3PdlSFFg3Q==</Q><DP>7Uj88qhvypgByI14MgVz6//ZE7QC33Bsh/h4FJkzg4sitos4oTD6DTO1zcmykwpq1bctcVESvHQKY4dE/flTAQ==</DP><DQ>YFHQvU9kl6mj49nS/jQUqs7bprup1OZZgErvW1WQj2PCwuESrkDrTmqNsDfB2FRFPQCOUqjNx1/uAqdHHfkVAQ==</DQ><InverseQ>jXdSw6DS4tBhuUtxbskvNfd9ZPfOBdSTSmhziHaRpeAnz2iSlknlW4P/vcNIOXXQQJ7sk6GmfDfB59BUxBJOIg==</InverseQ><D>iWXIgjyBODXEHMjQ7l9xI3nsnTS1upPn9tx75iBF429kZX9Mlpr82rlPxHY0NyjHV28TRKPEQHBhVd9KK5qDEAwyYZ+2dCZxsNWkajPJweNlZcrLG9iDTEsBPErTkd+a4kdVP4YRcMsCr/kglEWwjchVe0HZJpR9P4bq3qJm6wE=</D></RSAKeyValue>";

        #endregion

        #region ==== 静态方法 ====

        /// <summary>
		/// 生成一个序列号
		/// </summary>
		public static string CreateSerialNumber()
        {
            string str = Guid.NewGuid().ToString();
            string[] strs = str.Split('-');

            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = strs[i].Substring(0, 4);
            }

            return string.Format("{0}-{1}-{2}-{3}-{4}", strs[0], strs[1], strs[2], strs[3], strs[4]);
		}

		/// <summary>
		/// 生成激活码
		/// </summary>
		/// <param name="sno">软件序列号</param>
		public string CreateActivationNumber(string sno)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(priKey);
                RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsa);
                f.SetHashAlgorithm("SHA1");
                byte[] source = ASCIIEncoding.ASCII.GetBytes(sno);
                SHA1Managed sha = new SHA1Managed();
                byte[] result = sha.ComputeHash(source);
                byte[] b = f.CreateSignature(result);

                return Convert.ToBase64String(b); 
            }
		}

		/// <summary>
		/// 注册软件
		/// </summary>
		/// <param name="sno">序列号</param>
		/// <param name="activationNo">激活码</param>
		public static bool Register(string sno, string activationNo)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(pubKey);

                    RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsa);
                    f.SetHashAlgorithm("SHA1");

                    byte[] key = Convert.FromBase64String(activationNo);
                    SHA1Managed sha = new SHA1Managed();
                    byte[] name = sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(sno));

                    if (!f.VerifySignature(name, key))
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }

        #endregion

    }
}