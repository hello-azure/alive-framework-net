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

namespace Alive.Foundation.Utilities.Algorithm
{
    /// <summary>
    /// 字符串相似度算法：编辑距离算法，此算法由俄国科学家Levenshtein提出
    /// </summary>
    public class LevenshteinDistance
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// 实例对象
        /// </summary>
        private static LevenshteinDistance instance = null;

        #endregion

        #region ==== 属性 ====

        /// <summary>
        /// 实例对象
        /// </summary>
        public static LevenshteinDistance Instance
        {
            get
            {
                if (instance == null)
                {
                    return new LevenshteinDistance();
                }

                return instance;
            }
        }

        #endregion

        #region ==== 公有方法 ====

        /// <summary>
        /// 计算字符串相似度
        /// </summary>
        /// <param name="str1">字符串1</param>
        /// <param name="str2">字符串2</param>
        /// <returns></returns>
        public decimal GetLevenshteinSimilarity(string str1,string str2)
        {
            int maxLenth = str1.Length > str2.Length ? str1.Length : str2.Length;
            int val = GetLevenshteinDistance(str1, str2);
            return 1 - (decimal)val / maxLenth;
        }

        #endregion

        #region ==== 私有方法 ====

        /// <summary>
        /// 取得最小的一位数
        /// </summary>
        /// <param name="first">第一个数</param>
        /// <param name="second">第二个树</param>
        /// <param name="third">第二个数</param>
        /// <returns>最小数</returns>
        int GetLowerOfThree(int first, int second, int third)
        {
            int min = first;

            if (second < min)
            {
                min = second;
            }

            if (third < min)
            {
                min = third;
            }

            return min;
        }

        /// <summary>
        /// 编辑距离
        /// </summary>
        /// <param name="str1">第一字符串</param>
        /// <param name="str2">第二个字符串</param>
        /// <returns>距离</returns>
        int GetLevenshteinDistance(string str1, string str2)
        {
            int[,] Matrix;
            int n = str1.Length;
            int m = str2.Length;

            int temp = 0;
            char ch1;
            char ch2;
            int i = 0;
            int j = 0;

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            Matrix = new int[n + 1, m + 1];

            for (i = 0; i <= n; i++)
            {
                Matrix[i, 0] = i;
            }

            for (j = 0; j <= m; j++)
            {
                Matrix[0, j] = j;
            }

            for (i = 1; i <= n; i++)
            {
                ch1 = str1[i - 1];

                for (j = 1; j <= m; j++)
                {
                    ch2 = str2[j - 1];

                    if (ch1.Equals(ch2))
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = 1;
                    }

                    Matrix[i, j] = GetLowerOfThree(Matrix[i - 1, j] + 1, Matrix[i, j - 1] + 1, Matrix[i - 1, j - 1] + temp);  
                }
            }

            return Matrix[n, m];
        }

        #endregion

    }
}
