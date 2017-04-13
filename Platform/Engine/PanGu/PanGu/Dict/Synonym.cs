using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace PanGu.Dict
{
    /// <summary>
    /// 同义词
    /// </summary>
    class Synonym
    {
        object _LockObj = new object();

        bool _Init = false;

        List<string[]> _GroupList = null;//同义词组，文件中一行为一组，一组同义词以 “,” 分割

        Dictionary<string, List<int>> _WordToGroupId = null; //单词到同义词组的对应关系，一个单词可能对于多个同义词组

        internal const string SynonymFileName = "Synonym.txt";

        /// <summary>
        /// 从文件中加载词源
        /// </summary>
        /// <param name="fileName">文件路径</param>
        private void LoadSynonym(string fileName)
        {
            _GroupList = new List<string[]>();
            _WordToGroupId = new Dictionary<string, List<int>>();

            if (!System.IO.File.Exists(fileName))
            {
                return;
            }

            using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
            {

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine().Trim().ToLower();

                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    
                    string[] words = line.Split(new char[] { ',' });
                    _GroupList.Add(words);
                    int groupId = _GroupList.Count - 1;

                    for (int i = 0; i < words.Length; i++)
                    {
                        words[i] = words[i].Trim();

                        List<int> idList;
                        if (_WordToGroupId.TryGetValue(words[i], out idList))
                        {
                            if (idList[idList.Count-1] == groupId)
                            {
                                continue;
                            }

                            idList.Add(groupId);
                        }
                        else
                        {
                            idList = new List<int>(1);
                            idList.Add(groupId);
                            _WordToGroupId.Add(words[i], idList);
                        }
                    }
                }
            }

            _Init = true;
        }

        /// <summary>
        /// 从数据库中加载词源
        /// </summary>
        private void LoadSynonym(List<string> list)
        {
            _GroupList = new List<string[]>();
            _WordToGroupId = new Dictionary<string, List<int>>();
            string line = string.Empty;

            if (list!=null&&list.Count!=0)
            {
                foreach (string item in list)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }

                    line = item.Trim().ToLower();
                    string[] words = line.Split(new char[] { ',' });
                    _GroupList.Add(words);
                    int groupId = _GroupList.Count - 1;

                    for (int i = 0; i < words.Length; i++)
                    {
                        words[i] = words[i].Trim();

                        List<int> idList;
                        if (_WordToGroupId.TryGetValue(words[i], out idList))
                        {
                            if (idList[idList.Count - 1] == groupId)
                            {
                                continue;
                            }

                            idList.Add(groupId);
                        }
                        else
                        {
                            idList = new List<int>(1);
                            idList.Add(groupId);
                            _WordToGroupId.Add(words[i], idList);
                        }
                    }
                }
            }

            _Init = true;
        }

        internal bool Inited
        {
            get
            {
                lock (_LockObj)
                {
                    return _Init;
                }
            }
        }

        public void Load(List<string> list)
        {
            LoadSynonym(list);  
        }


        public List<string> GetSynonyms(string word)
        {
            word = word.Trim().ToLower();

            List<int> idList;
            if (_WordToGroupId.TryGetValue(word, out idList))
            {
                List<string> result = new List<string>();

                foreach (int groupId in idList)
                {
                    foreach (string w in _GroupList[groupId])
                    {
                        if (w == word)
                        {
                            continue;
                        }

                        if (result.Contains(w))
                        {
                            continue;
                        }

                        result.Add(w);
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
