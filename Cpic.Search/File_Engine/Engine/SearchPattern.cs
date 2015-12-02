using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpic.Cprs2010.Index;
using Cpic.Cprs2010.Search;

namespace Cpic.Cprs2010.Engine
{
    public class SearchPattern
    {
        #region 私有字段

        private int _Id;
        private bool _IsLeft;
        private string _SearchEnter;
        private string SearchValue;
        /// <summary>
        /// 检索式
        /// </summary>
        private string _SearchCommand;
        private List<Cpic.Cprs2010.Engine.SearchPattern> _SubSearchPattern;
        /// <summary>
        /// 检索式的结果
        /// </summary>
        private Result _Result;
        /// <summary>
        /// 运算符
        /// </summary>
        private string _Operator;

        /// <summary>
        /// 检索类型 目前只分中英文
        /// </summary>
        private DataInterfaceConfig _Config;

        #endregion
        private readonly string resultfile = "{0}\\{1}\\{2}.cnp";

        #region 属性


        /// <summary>
        /// id
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 是否是前方一直检索
        /// </summary>
        public bool IsLeft
        {
            get { return _IsLeft; }
            set { _IsLeft = value; }
        }

        /// <summary>
        /// 检索入口
        /// </summary>
        public string SearchEnter
        {
            get { return _SearchEnter; }
            set { _SearchEnter = value; }
        }

        /// <summary>
        /// 检索式
        /// </summary>
        public string SearchCommand
        {
            get { return _SearchCommand; }
            set { _SearchCommand = value; }
        }

        /// <summary>
        /// 子检索式
        /// </summary>
        public List<Cpic.Cprs2010.Engine.SearchPattern> SubSearchPattern
        {
            get { return _SubSearchPattern; }
            set { _SubSearchPattern = value; }
        }

        /// <summary>
        /// 运算符
        /// </summary>
        /// <remarks>
        /// + 并集
        /// * 交集
        /// - 差集
        /// </remarks>
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }



        public Result Result
        {
            get { return _Result; }
            set { _Result = value; }
        }


        /// <summary>
        /// 数据类型
        /// </summary>
        public DataInterfaceConfig Config
        {
            get { return _Config; }
            set { _Config = value; }
        }
        #endregion
        #region 构造函数
        public SearchPattern(DataInterfaceConfig config)
        {
            this.Config = config;
        }
        #endregion

        /// <summary>
        /// 解析检索式
        /// </summary>
        public void Init()
        {
            _Result = new Result();
            int index;
            char PreOperator = ' ';
            int PreOperatorIndex = 0;
            int tmpindx = 0;
            ///堆栈 用来验证括号是否成对出现
            System.Collections.Generic.Stack<char> stKuoHao = new Stack<char>();
            char[] chrcommand;
            string Command = SearchCommand.Replace("F XX ", "").Replace("F YY ", "");
            if (Command.StartsWith("F "))
            {
                Command = Command.TrimStart("F ".ToCharArray()).Trim();
            }

            //没有括号 判断有没有+-*运算
            if (Command.IndexOf("+") < 0 && Command.IndexOf("*") < 0 && Command.IndexOf("-") < 0) //最简单的检索命令
            {
                #region 没有括号 判断有没有+-*运算 的简单检索式
                //F XX  20080101/GN
                Command = Command.Trim().TrimStart('(').TrimEnd(')');
                index = Command.LastIndexOf("/");
                if (index > 0)
                {
                    #region 20080101/GN
                    if (Command.Substring(0, 2) == "IC" || Command.Substring(0, 2) == "EC")
                    {
                        SearchEnter = Command.Substring(0, 2);
                        SearchValue = Command.Substring(2).Trim().Replace("/", "").ToUpper();
                    }
                    else
                    {
                        SearchEnter = Command.Substring(index + 1);
                        SearchValue = Command.Substring(0, index).Trim().Replace("/", "").ToUpper();
                    }
                    #endregion
                }
                else
                {
                    #region GN 20080101
                    SearchEnter = Command.Substring(0, 2);
                    SearchValue = Command.Substring(2).Trim().ToUpper();
                    #endregion
                }


                #region 默认前方一直的检索入口有
                switch (SearchEnter)
                {
                    case "AN":
                    case "PN":
                    case "GN":
                    case "IC":
                    case "EC":
                    case "AD":
                    case "PD":
                    case "GD":
                    case "PR":
                    case "IN":
                    case "PA":
                        SearchValue = SearchValue.TrimEnd('$');
                        IsLeft = true;
                        break;
                    default:
                        if (SearchValue.EndsWith("$"))
                        {
                            SearchValue = SearchValue.TrimEnd('$');
                            IsLeft = true;
                        }
                        break;

                }
                #endregion
                #region 如果有需要切词的
                Cpic.Cprs2010.Index.SearchEnter se = Config.GetSearchEnter(SearchEnter);

                if (se.WordSplit == WordSplitType.Cn)
                {
                    #region 处理中文切词
                    if (SearchValue.Length > 3)
                    {
                        List<string> Sentence;
                        //切成没有符号的短句
                        Sentence = WordSplit.getSentenceList(SearchValue);
                        Sentence = Sentence.Distinct<string>().ToList<string>();
                        List<string> ThreeWord = new List<string>();
                        List<char> tmpOneWord;
                        //循环每个短句
                        for (int i = 0; i <= Sentence.Count - 1; i++)
                        {
                            //如果为空进行下次循环
                            if (string.IsNullOrEmpty(Sentence[i]))
                            {
                                continue;
                            }
                            if (Sentence[i].Length <= 3)
                            {
                                ThreeWord.Add(Sentence[i]);

                            }
                            else
                            {
                                //短句中的一字词
                                tmpOneWord = WordSplit.getOneWordList(Sentence[i]);
                                //短句中的三字词
                                ThreeWord.AddRange(WordSplit.getThreeWordList1(tmpOneWord));
                            }
                        }
                        if (ThreeWord.Count > 0)
                        {
                            SubSearchPattern = new List<SearchPattern>();
                        }
                        for (int i = 0; i <= ThreeWord.Count - 1; i++)
                        {
                            //得到第一个运算符
                            SearchPattern subsp = new SearchPattern(Config);
                            subsp.Id = tmpindx;
                            subsp.SearchCommand = SearchEnter + " " + ThreeWord[i];
                            subsp.Operator = "*";
                            subsp.Init();
                            SubSearchPattern.Add(subsp);

                        }
                    }
                    else if(SearchValue.Length <3)
                    {
                        IsLeft = true;
                    }
                    #endregion
                }
                else if(se.WordSplit == WordSplitType.English)
                {
                    #region 处理英文切词
                    List<string> EnWord;
                    EnWord = WordSplit.getEnglistWordList(SearchValue);
                    if (EnWord.Count == 1)
                    {
                        SearchValue = EnWord[0];
                    }
                    else
                    {
                        SubSearchPattern = new List<SearchPattern>();
                        if (EnWord.Count == 0)
                        {
                            //得到第一个运算符
                            SearchPattern subsp = new SearchPattern(Config);
                            subsp.Id = tmpindx;
                            subsp.SearchCommand = SearchEnter + " " + "xxxxxxxxxxxxxxxx";
                            subsp.Operator = "*";
                            subsp.Init();
                            SubSearchPattern.Add(subsp);
                        }
                        for (int i = 0; i <= EnWord.Count - 1; i++)
                        {
                            //得到第一个运算符
                            SearchPattern subsp = new SearchPattern(Config);
                            subsp.Id = tmpindx;
                            subsp.SearchCommand = SearchEnter + " " + EnWord[i];
                            subsp.Operator = "*";
                            subsp.Init();
                            SubSearchPattern.Add(subsp);

                        }
                    }
                    #endregion
                }              

                #endregion




                #endregion

            }
            else
            {
                #region 处理复杂检索式
                //F XX  20080101/GN +(200220/AN + 计算机/TI)
                SubSearchPattern = new List<SearchPattern>();
                chrcommand = Command.Trim().ToCharArray();
                for (int i = 0; i <= chrcommand.Length - 1; i++)
                {
                    if (chrcommand[i] == '(')
                    {
                        stKuoHao.Push(chrcommand[i]);
                    }
                    if (chrcommand[i] == ')')
                    {
                        stKuoHao.Pop();
                    }
                    if (chrcommand[i] == '+' || chrcommand[i] == '*' || chrcommand[i] == '-')
                    {
                        if (stKuoHao.Count > 0)
                        {
                            continue;
                        }
                        //得到第一个运算符
                        SearchPattern subsp = new SearchPattern(Config);
                        subsp.Id = tmpindx;
                        subsp.SearchCommand = Command.Substring(PreOperatorIndex, i - PreOperatorIndex).Trim();
                        if (subsp.SearchCommand.StartsWith("("))
                        {
                            subsp.SearchCommand = subsp.SearchCommand.Substring(1, subsp.SearchCommand.Length - 2).Trim();
                        }
                        subsp.Operator = PreOperator.ToString().Trim();
                        PreOperator = chrcommand[i];
                        PreOperatorIndex = i + 1;
                        subsp.Init();
                        SubSearchPattern.Add(subsp);
                        tmpindx += 1;
                    }
                    //如果是最后一个检索项目
                    if (i == chrcommand.Length - 1)
                    {
                        if (stKuoHao.Count > 0)
                        {
                            throw new Exception("检索式括号没有成对出现！");
                        }
                        SearchPattern subsp = new SearchPattern(Config);
                        subsp.Id = tmpindx;
                        subsp.SearchCommand = Command.Substring(PreOperatorIndex).Trim();
                        if (subsp.SearchCommand.StartsWith("("))
                        {
                            subsp.SearchCommand = subsp.SearchCommand.Substring(1, subsp.SearchCommand.Length - 2).Trim();
                        }
                        subsp.Operator = PreOperator.ToString();
                        subsp.Init();
                        SubSearchPattern.Add(subsp);
                        tmpindx += 1;

                    }
                }
                #endregion
            }

        }
        public bool DoSearch(FileFinder fd)
        {
            if (SubSearchPattern != null)
            {
                foreach (SearchPattern sp in SubSearchPattern)
                {
                    sp.DoSearch(fd);
                }
            }
            else
            {
                //判断是否是日期的范围检索
                int index = SearchValue.IndexOf(">");

                if (index > 0)
                {
                    Result.Content = fd.SearchBetween(SearchEnter, SearchValue.Substring(0, index).Trim(), SearchValue.Substring(index + 1).Trim());
                }
                else
                {
                    DateTime tm = DateTime.Now;
                    Result.Content = fd.Search(SearchEnter, SearchValue, IsLeft);
                    TimeSpan sp = DateTime.Now - tm;
                    //Console.WriteLine(sp.ToString());
                }
            }
            return true;
        }
        /// <summary>
        /// 得到检索结果
        /// </summary>
        /// <returns></returns>
        public Cpic.Cprs2010.Search.ResultInfo Search(Cpic.Cprs2010.Search.SearchPattern _searchPattern, FileFinder fd)
        {
            string ResultFile = string.Format(resultfile, CprsConfig.GetUserPath(_searchPattern.UserId, _searchPattern.GroupName), Enum.GetName(typeof(SearchDbType), _searchPattern.DbType), _searchPattern.SearchNo);
            SearchCommand = _searchPattern.Pattern;
            ResultInfo re = new ResultInfo();
            re.SearchPattern = _searchPattern;
            Result rs;

            try
            {
                //初始化
                Init();
                //检索
                DoSearch(fd);
                rs = GetResult();
                // 创建文件夹
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(ResultFile)))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(ResultFile));
                }
                else
                {
                    if (System.IO.File.Exists(ResultFile))
                    {
                        System.IO.File.Delete(ResultFile);
                    }
                }

                using (System.IO.FileStream fsw = new System.IO.FileStream(ResultFile, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    foreach (int i in rs.Content)
                    {
                        byte[] byhis = BitConverter.GetBytes(i);
                        fsw.Write(byhis, 0, byhis.Length);
                    }
                }
                re.HitCount = rs.Content.Count();
                re.HitMsg = "(" + _searchPattern.SearchNo + ")" + SearchCommand + " <hits: " + re.HitCount + ">";
            }
            catch (Exception ex)
            {
                re.HitCount =0;
                re.HitMsg = "(" + _searchPattern.SearchNo + ")" + SearchCommand + " <hits: " + re.HitCount + ">";
            }
            return re;
        }
        /// <summary>
        /// 得到检索结果
        /// </summary>
        /// <returns></returns>
        public Result GetResult()
        {
            Result rs = new Result();
            if (SubSearchPattern != null)
            {
                rs = SubSearchPattern[0].GetResult();

                for (int i = 1; i <= SubSearchPattern.Count - 1; i++)
                {
                    switch (SubSearchPattern[i].Operator)
                    {
                        case "+":
                            rs.Content = rs + SubSearchPattern[i].GetResult();
                            break;
                        case "*":
                            rs.Content = rs * SubSearchPattern[i].GetResult();
                            break;
                        case "-":
                            rs.Content = rs - SubSearchPattern[i].GetResult();
                            break;
                    }
                }
            }
            else
            {
                rs = Result;
            }
            rs.Content = rs.Content.Distinct().ToList<int>();
            rs.Content.Sort();
            return rs;
        }
        public override string ToString()
        {
            if (SubSearchPattern != null)
            {
                return string.Format("ID:{0},检索式:{1},操作符:{2}", Id, SearchCommand, Operator);
            }
            else
            {
                return string.Format("ID:{0},检索式:{1},入口:{2},检索值:{3},操作符:{4},是否前方一直:{5}", Id, SearchCommand, SearchEnter, SearchValue, Operator, IsLeft);
            }
        }
    }

}
