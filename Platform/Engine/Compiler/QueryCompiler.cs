/***********
 * 版权说明：
 *   本文件是 Somme 服务程序的一部分。
 *   版本：V 1.0
 *   Copyright 万物生创意软件工作室 2013 保留一切权利
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;

namespace Alive.Engine.Search.Compiler
{
    /// <summary>
    /// 语法编译器，对语法进行标准化及纠正
    /// </summary>
    internal class QueryCompiler
    {
        #region ==== 私有字段 ====

        /// <summary>
        /// QueryParser实例
        /// </summary>
        private QueryParser parser = null;

        /// <summary>
        /// 需要转换大写的语法关键字组
        /// </summary>
        private static readonly List<string> syntaxKeywords =
            new List<string> { 
                "and",
                "or", 
                "not", 
                "to" 
            };

        /// <summary>
        /// 需要进行中文输入发下转换英文书法下的语法关键字组
        /// </summary>
        private static readonly Dictionary<string, string> cnSyntaxKeywords =
            new Dictionary<string, string> { 
              {"！","!"},
              {"：",":"},
              {"（","("},
              {"）",")"},
              {"【","["},
              {"】","]"},
              {"“","\""},
              {"”","\""}
            };

        /// <summary>
        /// 通配符
        /// </summary>
        private static readonly List<string> wildcardKeywords = new List<string> { 
            "?", 
            "*" 
        };

        /// <summary>
        /// 前置词要求的语法关键字组
        /// </summary>
        private static readonly List<string> preKeywords = new List<string>() {
            ":"
        };

        /// <summary>
        /// 后再次要求的语法关键字组
        /// </summary>
        private static readonly List<string> endKeywords = new List<string>() {
            "NOT",
            "!" 
        };

        /// <summary>
        /// 前置与后置要求的语法关键字组
        /// </summary>
        private static readonly List<string> preEndKeywords = new List<string>() { 
            "AND", 
            "&&", 
            "OR", 
            "||", 
            "TO"
        };

        /// <summary>
        /// 相同字符的配对语法关键字组
        /// </summary>
        private static readonly List<string> samePairKeywords = new List<string>() { 
            "\""
        };

        /// <summary>
        /// Lucene内置语法关键字组
        /// </summary>
        private static readonly List<string> totalKeywords = new List<string>{
            "+",
            "-",
            "&&",
            "||",
            "!",
            "(",
            ")",
            "{",
            "}",
            "[",
            "]",
            "^",
            "\"",
            "~",
            "*",
            "?",
            ":",
            "\\",
            "AND",
            "OR",
            "NOT",
            "TO",
            "",
            ""
        };

        #endregion

        #region ==== 构造函数 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parser">QueryParser实例</param>
        public QueryCompiler(QueryParser parser)
        {
            this.parser = parser;
        }

        #endregion


        #region ==== 内部方法 ====

        /// <summary>
        /// 编译查询语句，进行容错处理
        /// </summary>
        /// <param name="searchword">查询语句</param>
        /// <returns>true:纠错成功 false:纠错失败</returns>
        internal CompileResult Compile(string searchword)
        {
            CompileResult result = new CompileResult();
            string compiledSearchCondition = string.Empty;

            this.Compile1(searchword, out compiledSearchCondition);
            this.Compile2(compiledSearchCondition, out compiledSearchCondition);
            this.Compile3(compiledSearchCondition, out compiledSearchCondition);
            this.Compile4(compiledSearchCondition, out compiledSearchCondition);
            this.Compile5(compiledSearchCondition, out compiledSearchCondition);
            this.Compile6(compiledSearchCondition, out compiledSearchCondition);
            this.Compile7(compiledSearchCondition, out compiledSearchCondition);
            return new CompileResult(this.CompileEnd(compiledSearchCondition, out compiledSearchCondition), compiledSearchCondition);
        }

        #endregion

        /// <summary>
        /// 将"and, "or", "not", "to"转换为大写
        /// </summary>
        /// <param name="searchword"></param>
        void Compile1(string searchword, out string complileResult)
        {
            complileResult = searchword;
            var tempTag = "@@@";

            if (syntaxKeywords != null && syntaxKeywords.Count > 0)
            {

                foreach (var sk in syntaxKeywords)
                {
                    Regex reg = new Regex(@sk, RegexOptions.IgnoreCase);
                    Match m = reg.Match(complileResult);

                    while (m.Success)
                    {
                        complileResult = complileResult.Replace(m.Value, string.Format(" {0} ", tempTag));
                        m = reg.Match(complileResult);
                    }

                    complileResult = complileResult.Replace(tempTag, string.Format(" {0} ", sk.ToUpper()));
                }
            }
        }

        /// <summary>
        /// 将中文输入法下:,(,),[,],"转换英文输入法下的字符格式
        /// </summary>
        /// <param name="searchword"></param>
        void Compile2(string searchword, out string complileResult)
        {
            complileResult = searchword;

            if (cnSyntaxKeywords != null && cnSyntaxKeywords.Count > 0)
            {
                foreach (var csk in cnSyntaxKeywords)
                {
                    Regex reg = new Regex(@csk.Key, RegexOptions.IgnoreCase);
                    Match m = reg.Match(complileResult);

                    while (m.Success)
                    {
                        complileResult = complileResult.Replace(m.Value, csk.Value);
                        m = reg.Match(complileResult);
                    }
                }
            }
        }

        /// <summary>
        /// 处理不合法的通配符（通配符置首或者前置字符为0或多个空格符）
        /// </summary>
        /// <param name="searchword"></param>
        void Compile3(string searchword, out string complileResult)
        {
            complileResult = searchword;

            if (wildcardKeywords != null && wildcardKeywords.Count > 0)
            {
                foreach (var wk in wildcardKeywords)
                {

                    if (complileResult.StartsWith(wk))
                    {
                        searchword = complileResult.Substring(1, complileResult.Length);
                    }

                    Regex reg = new Regex(string.Format(@"(\s+)\{0}", wk), RegexOptions.IgnoreCase);
                    Match m = reg.Match(complileResult);

                    while (m.Success)
                    {
                        complileResult = complileResult.Replace(m.Value, "");
                        m = reg.Match(complileResult);
                    }
                }
            }
        }

        /// <summary>
        /// 处理后置限定词的语法关键字
        /// </summary>
        /// <param name="searchword"></param>
        void Compile4(string searchword, out string complileResult)
        {
            complileResult = searchword;

            if (preKeywords != null && preKeywords.Count > 0)
            {
                foreach (var pk in preKeywords)
                {
                    while (complileResult.Contains(pk))
                    {
                        if (!complileResult.Trim().StartsWith(pk))
                        {
                            break;
                        }

                        complileResult = complileResult.Replace(pk, "");
                    }
                }
            }
        }

        /// <summary>
        /// 处理后置限定词的语法关键字
        /// </summary>
        /// <param name="searchword"></param>
        void Compile5(string searchword, out string complileResult)
        {
            complileResult = searchword;

            if (endKeywords != null && endKeywords.Count > 0)
            {
                foreach (var ek in endKeywords)
                {
                    while (complileResult.Contains(ek))
                    {
                        if (!complileResult.Trim().EndsWith(ek))
                        {
                            break;
                        }

                        complileResult = complileResult.Replace(ek, "");
                    }
                }
            }
        }

        /// <summary>
        /// 处理限制前置或后置限定词的语法关键字
        /// </summary>
        /// <param name="searchword"></param>
        void Compile6(string searchword, out string complileResult)
        {
            complileResult = searchword;

            if (preEndKeywords != null && preEndKeywords.Count > 0)
            {
                foreach (var pek in preEndKeywords)
                {
                    if (!complileResult.ToUpper().Contains(pek))
                    {
                        continue;
                    }

                    //var temp = complileResult.Remove(complileResult.ToUpper().IndexOf(pek), pek.Length);
                    var datas = complileResult.Split(pek.ToCharArray());
                    var temp = string.Empty;

                    if (datas != null && datas.Length > 0)
                    {
                        for (int i = 0; i < datas.Length; i++)
                        {
                            if (i < datas.Length - 1)
                            {
                                if (!string.IsNullOrEmpty(datas[i].Trim())
                                    && string.IsNullOrEmpty(datas[i + 1].Trim()))
                                {
                                    temp += string.Format("{0} {1} {2}", datas[i], pek, datas[i + 1]);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(datas[i].Trim()))
                                {
                                    temp += string.Format("{0}", datas[i]);
                                }
                            }
                        }
                    }

                    complileResult = temp;
                }
            }
        }

        /// <summary>
        /// 处理"数目不匹配,最后一个"将被去掉
        /// </summary>
        /// <param name="searchword"></param>
        void Compile7(string searchword, out string complileResult)
        {
            complileResult = searchword;

            if (samePairKeywords != null && samePairKeywords.Count > 0)
            {
                foreach (var spk in samePairKeywords)
                {
                    if (!complileResult.ToUpper().Contains(spk))
                    {
                        continue;
                    }

                    var temp = complileResult;
                    var datas = temp.Split(spk.ToCharArray(), StringSplitOptions.None);
                    temp = string.Empty;

                    if (datas != null && datas.Length > 0)
                    {
                        if (datas.Length % 2 == 0)
                        {
                            for (int i = 0; i < datas.Length; i++)
                            {
                                if (i < datas.Length - 1)
                                {
                                    temp += string.Format("{0}{1}{2}", datas[i], spk, datas[i + 1]);
                                }
                                else
                                {
                                    temp += string.Format(" {0}", datas[i]);
                                }
                            }
                        }
                    }

                    complileResult = temp;
                }
            }
        }

        /// <summary>
        /// 最后的编译处理
        /// </summary>
        /// <param name="searchword">查询语句</param>
        /// <returns>true:纠错成功 false:纠错失败</returns>
        bool CompileEnd(string searchword, out string complileResult)
        {
            complileResult = searchword;

            if (Check(searchword))
            {
                return true;
            }

            if (endKeywords != null && endKeywords.Count > 0)
            {
                foreach (var tk in totalKeywords)
                {
                    if (complileResult.Contains(tk))
                    {
                        complileResult = complileResult.Replace(tk, " ");
                    }
                }
            }

            return Check(complileResult);
        }

        /// <summary>
        /// 检查语法正确性
        /// </summary>
        /// <param name="searchword">查询语句</param>
        /// <returns>true:语法合法 false:语法非法</returns>
        bool Check(string searchword)
        {
            bool result = true;

            try
            {

                parser.Parse(searchword).ToString();
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
