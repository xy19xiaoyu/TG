using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpic.Cprs2010.Index;
using System.IO;
using Cpic.Cprs2010.Search;

namespace Cpic.Cprs2010.Engine
{
    public class MemoryFinder:ISearch
    {
        /// <summary>
        /// 所有检索入库的索引
        /// </summary>
        private Dictionary<string, List<MemoryIndex>> _Indexs;

        /// <summary>
        /// 所有检索入库的索引
        /// </summary>
        public Dictionary<string, List<MemoryIndex>> Indexs
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

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConfigFilePath">配置文件路径</param>
        /// <param name="IndexDirectory">索引根目录存放位置</param>
        public MemoryFinder( )
        {
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

        /// <summary>
        /// Finder初始化
        /// </summary>
        /// <returns></returns>
        public bool Ini()
        {
            ConfigFilePath = System.Configuration.ConfigurationManager.AppSettings["ConfigFile"].ToString();
            IndexDirectory = Path.GetDirectoryName(ConfigFilePath);
            _Indexs = new Dictionary<string, List<MemoryIndex>>();
            _Config = new DataInterfaceConfig(ConfigFilePath);
            List<Key> lstKey = Config.GetSearchEnterKey();

            //1.得到所有的索引分段文件夹
            foreach (Key key in lstKey)
            {
                //得到所有的文件
                string[] indexfiles = Directory.GetFiles(IndexDirectory, key.Name + ".eee", SearchOption.AllDirectories);
                List<MemoryIndex> lstIndex = new List<MemoryIndex>();
                foreach (string indexfile in indexfiles)
                {

                    //实例化索引
                    MemoryIndex ix = new MemoryIndex(indexfile, key);
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

        #endregion




        #region ISearch 成员


        public ResultInfo Search(Cpic.Cprs2010.Search.SearchPattern _searchPattern)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

