using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace PanGu.Dict
{
    /// <summary>
    /// 通配符词汇
    /// </summary>
    class Wildcard
    {
        internal class WildcardInfo
        {
            public string Key;

            public List<WordInfo> Segments = new List<WordInfo>();

            public WildcardInfo(string wildcard, Segment segment, 
                Match.MatchOptions options, Match.MatchParameter parameter)
            {
                this.Key = wildcard.ToLower();

                this.Segments.Add(new WordInfo(wildcard, POS.POS_UNK, 0));

                foreach (WordInfo wordInfo in segment.DoSegment(wildcard, options, parameter))
                {
                    if (wordInfo.Word != wildcard)
                    {
                        this.Segments.Add(wordInfo);
                    }
                }
            }
        }

        object _LockObj = new object();

        bool _Init = false;
        Match.MatchOptions _Options;
        Match.MatchParameter _Parameter;

        List<WildcardInfo> _WildcardList = null; //通配符词汇列表

        internal const string WildcardFileName = "Wildcard.txt";

        private void LoadWildcard(string fileName)
        {
            lock (_LockObj)
            {
                _WildcardList = new List<WildcardInfo>();

                if (!System.IO.File.Exists(fileName))
                {
                    return;
                }

                Segment segment = new Segment();

                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine().Trim();

                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        _WildcardList.Add(new WildcardInfo(line, segment, _Options, _Parameter));
                    }
                }

                _Init = true;
            }
        }

        private void LoadWildcard(List<string> list)
        {
            lock (_LockObj)
            {
                _WildcardList = new List<WildcardInfo>();

                Segment segment = new Segment();
                //List<string> list = LoadWordSource.Instance.Wildcard();
                string line = string.Empty;

                if (list!=null&&list.Count!=0)
                {
                    foreach (string item in list)
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            continue;
                        }

                        line = item.Trim();
                        _WildcardList.Add(new WildcardInfo(line, segment, _Options, _Parameter));
                    }   
                }

                _Init = true;
            }
        }

        internal Wildcard(Match.MatchOptions options, Match.MatchParameter parameter)
        {
            _Options = options.Clone();
            _Options.SynonymOutput = false;
            _Options.WildcardOutput = false;

            _Parameter = parameter.Clone();
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
            LoadWildcard(list);
        }

        internal List<WildcardInfo> GetWildcards(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return null;
            }

            lock (_LockObj)
            {
                word = word.ToLower().Trim();

                List<WildcardInfo> result = new List<WildcardInfo>();

                foreach (WildcardInfo wi in _WildcardList)
                {
                    if (wi.Key.Contains(word))
                    {
                        result.Add(wi);
                    }
                }

                return result;
            }
        }
    }
}
