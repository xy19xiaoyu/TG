using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpic.Cprs2010.Index;
using System.IO;
using Cpic.Cprs2010.Search;
using System.Threading;

namespace Cpic.Cprs2010.Engine
{
    public class FileFinder : ISearch, IDisposable
    {
        /// <summary>
        /// 所有检索入库的索引
        /// </summary>
        private Dictionary<string, List<FileIndex>> _Indexs;

        /// <summary>
        /// 所有检索入库的索引
        /// </summary>
        public Dictionary<string, List<FileIndex>> Indexs
        {
            get { return _Indexs; }
            set { _Indexs = value; }
        }
        /// <summary>
        /// 检索入口配置
        /// </summary>
        private DataInterfaceConfig _Config;

        /// <summary>
        /// 检索入口配置
        /// </summary>
        public DataInterfaceConfig Config
        {
            get { return _Config; }
            set { _Config = value; }
        }
        /// <summary>
        /// 索引文件夹根目录
        /// </summary>
        private string _IndexDir;

        /// <summary>
        /// 索引文件夹根目录
        /// </summary>
        public string IndexDir
        {
            get { return _IndexDir; }
            set { _IndexDir = value; }
        }
        private string ConfigFilePath;
        private string IndexDirectory;

        private string Year;
        private string Filtered;


        /// <summary>
        /// 构造函数
        /// </summary>
        public FileFinder()
            : this(System.Configuration.ConfigurationManager.AppSettings["DocDBConfigFile"].ToString(), System.Configuration.ConfigurationManager.AppSettings["DocDBIndPath"].ToString())
        {


        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConfigFilePath">配置文件路径</param>
        /// <param name="IndexDirectory">索引根目录存放位置</param>
        public FileFinder(string ConfigFilePath, string IndexDirectory)
        {
            this.ConfigFilePath = ConfigFilePath;
            this.IndexDirectory = IndexDirectory;
            if (string.IsNullOrEmpty(ConfigFilePath) || string.IsNullOrEmpty(IndexDirectory))
                return;
            Ini();
        }
        /// <summary>
        /// 得到所有的见入口名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetSearchEnterList()
        {
            List<string> ListEnterName = new List<string>();
            List<SearchEnter> lstEnter = Config.SearchEnters;
            //1.得到所有的索引分段文件夹
            foreach (SearchEnter se in lstEnter)
            {
                if (se.Index == 0)
                {
                    continue;
                }
                ListEnterName.Add(se.Name);


                //如果索引里还有子索引
                if (se.SubKey.Count > 0)
                {
                    foreach (SubSearchEnter subse in se.SubKey)
                    {
                        ListEnterName.Add(subse.Name);
                    }
                }
            }
            return ListEnterName;
        }

        public override string ToString()
        {
            return string.Format("IndexDir:{0},IndexCount:{1}", this.IndexDir, Indexs.Count);
        }




        #region ISearch 成员

        private int _id;

        private SearchDbType _Type;

        private SearchStatus _Status;

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Finder 的状态
        /// </summary>
        public SearchStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public SearchDbType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        public string GetFilter()
        {
            return string.Empty;
            //if (Lib.reginfo.UserSelected == "1")
            //{
            //    return string.Empty;
            //}
            //char[] ips = reginfo.VIpcs.Trim().ToCharArray();
            //if (ips.Length == 8)
            //{
            //    return string.Empty;
            //}
            //else
            //{
            //    string strfilter = "*(";
            //    foreach (char c in ips)
            //    {
            //        strfilter += c.ToString().ToUpper() + "$/IC+";
            //    }
            //    strfilter = strfilter.TrimEnd('+');
            //    strfilter += ")";
            //    return strfilter;
            //}
        }


        /// <summary>
        /// Finder初始化
        /// </summary>
        /// <returns></returns>
        public bool Ini()
        {
            //IndexDirectory = Path.GetDirectoryName(ConfigFilePath) + "\\" + reginfo.VYear + "\\" + Lib.reginfo.UserSelected;

            _Indexs = new Dictionary<string, List<FileIndex>>();
            _Config = new DataInterfaceConfig(ConfigFilePath);
            Type = _Config.Type;
            List<Key> lstKey = Config.GetSearchEnterKey();

            //1.得到所有的索引分段文件夹
            foreach (Key key in lstKey)
            {
                //得到所有的文件
                List<string> indexfiles = Directory.GetFiles(IndexDirectory, key.Name + ".eee", SearchOption.AllDirectories).ToList<string>();
                indexfiles.Sort();
                List<FileIndex> lstIndex = new List<FileIndex>();
                foreach (string indexfile in indexfiles)
                {

                    //实例化索引
                    FileIndex ix = new FileIndex(indexfile, key);
                    //添加到集合里
                    lstIndex.Add(ix);
                }
                //添加到字典中
                _Indexs.Add(key.Name, lstIndex);

            }
            return true;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool logIn()
        {
            return true;
            ///throw new NotImplementedException("暂不需要实现这个接口函数");
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public bool logOut()
        {
            return true;
            ///throw new NotImplementedException("暂不需要实现这个接口函数");
        }


        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="_searchPattern">检索式</param>
        /// <returns>检索结果</returns>
        public List<int> Search(SearchPattern _searchPattern)
        {
            ///1.解析检索式 如果是复杂检索 则拆分成小的检索式
            ///2.把所有的小检索式送到对应的索引类中检索
            ///3.对所有结果的合并
            ///4.与 或 非 运算
            return new List<int>();
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="_searchPattern">检索式</param>
        /// <returns>检索结果</returns>
        public List<int> Search(string SearchEnter, string strSearch, bool SearchLeft)
        {
            Cpic.Cprs2010.Index.SearchEnter se = _Config.GetSearchEnter(SearchEnter);
            if (se == null)
            {
                throw new Exception("没有对应入口");
            }

            //todo:
            //1.根据检索入口得到检索入口配置信息
            //2.根据配置信息 找到对应的索引 进行检索
            //3.SearchLike Between 都放到FileIndex里去实现
            //4.最后只需要调用fileIndex.Search(S) 就可以拿到结果
            if (SearchLeft)
            {
                return SearchLike(SearchEnter, strSearch);
            }
            else
            {
                List<int> lstResult = new List<int>();
                if (se.WordSplit == WordSplitType.Cn)
                {
                    if (se.SingleFile == false)
                    {
                        switch (strSearch.Length)
                        {
                            case 1:
                                lstResult.AddRange(SearchBySearchEnter(SearchEnter + 1.ToString(), strSearch));
                                break;
                            case 2:
                                lstResult.AddRange(SearchBySearchEnter(SearchEnter + 2.ToString(), strSearch));
                                break;
                            case 3:
                                lstResult.AddRange(SearchBySearchEnter(SearchEnter + 3.ToString(), strSearch));
                                break;
                            default:
                                throw new Exception("没有对应入口");
                        }

                    }
                    else
                    {
                        lstResult.AddRange(SearchBySearchEnter(SearchEnter, strSearch));
                    }

                }
                else if (se.WordSplit == WordSplitType.English)
                {
                    if (se.SingleFile == false)
                    {
                        char fchar = strSearch[0];
                        if ((fchar >= '0' && fchar <= '9') || (fchar >= '０' && fchar <= '９'))
                        {
                            fchar = '0';
                        }
                        lstResult.AddRange(SearchBySearchEnter(SearchEnter + "_" + fchar, strSearch));
                    }
                    else
                    {
                        lstResult.AddRange(SearchBySearchEnter(SearchEnter, strSearch));
                    }

                }
                else if (Type == SearchDbType.Cn && se.Name == "AN")
                {
                    switch (strSearch.Length)
                    {
                        case 5:
                            lstResult = SearchBySearchEnter(SearchEnter + "5", strSearch);
                            break;
                        case 6:
                        case 7:
                            lstResult = SearchLikeBySearchEnter(SearchEnter + "8", strSearch);
                            break;
                        case 8:
                            lstResult = SearchBySearchEnter(SearchEnter + "8", strSearch);
                            break;
                        default:
                            lstResult = SearchBySearchEnter(SearchEnter, strSearch);
                            break;
                    }
                }
                else
                {
                    lstResult = SearchBySearchEnter(SearchEnter, strSearch);
                }
                return lstResult;
            }
        }

        /// <summary>
        /// 前方一直查询
        /// </summary>
        /// <param name="SearchEnter"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        private List<int> SearchLike(string SearchEnter, string strSearch)
        {
            //判断检索入口是否支持 前方一直查询
            List<int> lstResult = new List<int>();
            Cpic.Cprs2010.Index.SearchEnter se = _Config.GetSearchEnter(SearchEnter);
            if (se == null)
            {
                throw new Exception("没有对应入口");
            }

            if (se.WordSplit == WordSplitType.Cn && se.SingleFile == false)
            {
                lstResult.AddRange(SearchLikeBySearchEnter(SearchEnter + 3.ToString(), strSearch));
            }
            else if (se.WordSplit == WordSplitType.English)
            {
                if (se.SingleFile == false)
                {
                    char fchar = strSearch[0];
                    if ((fchar >= '0' && fchar <= '9') || (fchar >= '０' && fchar <= '９'))
                    {
                        fchar = '0';
                    }
                    lstResult.AddRange(SearchLikeBySearchEnter(SearchEnter + "_" + fchar, strSearch));
                }
                else
                {
                    lstResult.AddRange(SearchLikeBySearchEnter(SearchEnter, strSearch));
                }
            }
            else if (Type == SearchDbType.Cn && se.Name == "AN")
            {
                switch (strSearch.Length)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        lstResult = SearchLikeBySearchEnter(SearchEnter + "5", strSearch);
                        break;
                    case 5:
                        lstResult = SearchBySearchEnter(SearchEnter + "5", strSearch);
                        break;
                    case 6:
                    case 7:
                        lstResult = SearchLikeBySearchEnter(SearchEnter + "8", strSearch);
                        break;
                    case 8:
                        lstResult = SearchBySearchEnter(SearchEnter + "8", strSearch);
                        break;
                    case 12:
                        lstResult = SearchBySearchEnter(SearchEnter, strSearch);
                        break;
                    default:
                        lstResult = SearchLikeBySearchEnter(SearchEnter, strSearch);
                        break;
                }
            }
            else
            {
                lstResult = SearchLikeBySearchEnter(SearchEnter, strSearch);
            }
            return lstResult = lstResult.Distinct<int>().ToList<int>();
        }
        #endregion

        /// <summary>
        /// 根据索引入口检索
        /// </summary>
        /// <param name="IndexKeyName">AB_A，AB1 等而AB,准确的检索某一类索引</param>
        /// <param name="strSearch">要检索的关键字</param>
        /// <returns></returns>
        private List<int> SearchBySearchEnter(string IndexKeyName, string strSearch)
        {
            List<int> lstResult = new List<int>();
            List<int> itemResult;
            //List<Thread> lstsd = new List<Thread>();
            if (!Indexs.ContainsKey(IndexKeyName))
            {
                throw new Exception("检索入口错误");
            }
            List<FileIndex> lstfinder = Indexs[IndexKeyName];
            foreach (FileIndex finder in lstfinder)
            {
                if (finder.Key.value.Type == ValType.EnglishWord)
                {

                    finder.strSearch = strSearch;
                    finder.SearchEnWord();
                    //Thread thd = new Thread(new ThreadStart(finder.SearchEnWord));
                    //thd.Start();
                    //lstsd.Add(thd);                  
                }
                else
                {

                    finder.strSearch = strSearch;
                    finder.Search();
                    //Thread thd = new Thread(new ThreadStart(finder.Search));
                    //thd.Start();
                    //lstsd.Add(thd);

                }

            }
            //do
            //{
            //    int DoneCount = 0;
            //    foreach (Thread sp in lstsd)
            //    {
            //        if (sp.ThreadState == ThreadState.Stopped)
            //        {
            //            DoneCount += 1;
            //        }
            //    }
            //    if (DoneCount == lstsd.Count)
            //    {
            //        break;
            //    }
            //    System.Threading.Thread.Sleep(50);
            //}
            //while (true);

            foreach (FileIndex finder in lstfinder)
            {
                itemResult = new List<int>();
                if (finder.Key.value.Type == ValType.EnglishWord)
                {
                    List<byte[]> lstbyres = finder.bResult;
                    if (lstbyres != null)
                    {
                        foreach (byte[] b in lstbyres)
                        {
                            itemResult.Add(BitConverter.ToInt32(b, 0));
                        }
                        itemResult = itemResult.Distinct<int>().ToList<int>();
                    }
                }
                else
                {
                    itemResult = finder.Result;
                }

                if (itemResult != null)
                {
                    lstResult.AddRange(itemResult);
                }
            }
            //lstsd.Clear();


            lstResult = lstResult.Distinct<int>().ToList<int>();
            return lstResult;

        }

        /// <summary>
        /// 根据索引入口检索
        /// </summary>
        /// <param name="IndexKeyName">AB_A，AB1 等而AB,准确的检索某一类索引</param>
        /// <param name="strSearch">要检索的关键字</param>
        /// <returns></returns>
        private List<int> SearchLikeBySearchEnter(string IndexKeyName, string strSearch)
        {
            List<int> lstResult = new List<int>();
            List<int> itemResult;
            if (!Indexs.ContainsKey(IndexKeyName))
            {
                throw new Exception("检索入口错误");
            }
            List<FileIndex> lstfinder = Indexs[IndexKeyName];
            foreach (FileIndex finder in lstfinder)
            {
                itemResult = new List<int>();
                if (finder.Key.value.Type == ValType.EnglishWord)
                {

                    List<byte[]> lstbyres = finder.SearchLikeEnWord(strSearch);
                    if (lstbyres != null)
                    {
                        foreach (byte[] b in lstbyres)
                        {
                            itemResult.Add(BitConverter.ToInt32(b, 0));
                        }
                        itemResult = itemResult.Distinct<int>().ToList<int>();
                    }
                }
                else
                {
                    itemResult = finder.SearchLike(strSearch);
                }
                if (itemResult != null)
                {
                    lstResult.AddRange(itemResult);
                }
            }
            lstResult = lstResult.Distinct<int>().ToList<int>();
            return lstResult;

        }


        #region ISearch 成员


        public ResultInfo Search(Cpic.Cprs2010.Search.SearchPattern _searchPattern)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 仅仅使用与日期
        /// </summary>
        /// <param name="SearchEnter"></param>
        /// <param name="FirstSearch"></param>
        /// <param name="LastSearch"></param>
        /// <returns></returns>
        public List<int> SearchBetween(string SearchEnter, string FirstSearch, string LastSearch)
        {

            List<int> lstResult = new List<int>();
            List<int> itemResult;
            if (!Indexs.ContainsKey(SearchEnter))
            {
                throw new Exception("检索入口错误");
            }
            List<FileIndex> lstfinder = Indexs[SearchEnter];
            foreach (FileIndex finder in lstfinder)
            {
                itemResult = new List<int>();
                itemResult = finder.SearchBetween(FirstSearch, LastSearch);
                if (itemResult != null)
                {
                    lstResult.AddRange(itemResult);
                }
            }

            return lstResult = lstResult.Distinct<int>().ToList<int>();
        }


        #region IDisposable 成员

        public void Dispose()
        {
            foreach (var x in _Indexs)
            {
                foreach (FileIndex y in x.Value)
                {
                    y.Dispose();
                }

            }
        }

        #endregion
    }
}

