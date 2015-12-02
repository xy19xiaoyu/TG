using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Cpic.Cprs2010.Search;

namespace Cpic.Cprs2010.Index
{
    public class DataInterfaceConfig
    {
        /// <summary>
        /// 分隔符
        /// </summary>
        private string _split;
        /// <summary>
        /// 配置文件的绝对目录
        /// </summary>
        private string _ConfigFilePath;

        /// <summary>
        /// 唯一序列号
        /// </summary>
        private int _SerialIndex;

        public int SerialIndex
        {
            get { return _SerialIndex; }
            set { _SerialIndex = value; }
        }

        /// <summary>
        /// 分隔符
        /// </summary>
        public string Split
        {
            get { return _split; }
            set { _split = value; }
        }


        private SearchDbType _Type;
        /// <summary>
        /// 数据类型
        /// CN,DWPI,DOCDB
        /// </summary>
        public SearchDbType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private string _Encoding;
        /// <summary>
        /// 编码
        /// </summary>
        public string Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConfigFilePath">配置文件的绝对目录</param>
        public DataInterfaceConfig(string ConfigFilePath)
        {
            _ConfigFilePath = ConfigFilePath;
            XDocument xd = XDocument.Load(_ConfigFilePath);
            Split = xd.Root.FirstAttribute.Value;
            SerialIndex = Convert.ToInt32(xd.Root.Attribute("SerialIndex").Value);
            switch (xd.Root.Attribute("DataType").Value)
            {
                case "CN":
                    Type = SearchDbType.Cn;
                    break;
                case "DOCDB":
                    Type = SearchDbType.DocDB;
                    break;
                case "DWPI":
                    Type = SearchDbType.Dwpi;
                    break;
            }
            _Encoding = xd.Root.Attribute("Encoding").Value;
            var resut = from x in xd.Root.Elements("key")
                        orderby Convert.ToInt32(x.Attribute("index").Value)
                        select new SearchEnter()
                        {
                            Index = Convert.ToInt32(x.Attribute("index").Value),
                            Name = x.Attribute("name").Value,
                            Split = x.Attribute("split").Value,
                            WordSplit = getWordSplitType(x.Attribute("worksplit").Value),
                            SingleFile = Convert.ToBoolean(x.Attribute("SingleFile").Value),
                            Length = Convert.ToInt32(x.Attribute("Length").Value),
                            DataType = x.Attribute("DataType").Value.Trim(),
                            WordLocation = Convert.ToBoolean(x.Attribute("WordLocation").Value),
                            SubKey = GetSubKey(x),
                            Encoding = x.Attribute("Encoding").Value.Trim()
                        };
            _SearchEnters = resut.ToList<SearchEnter>();
        }
        private List<SearchEnter> _SearchEnters;
        /// <summary>
        /// 得到检索入口列表
        /// </summary>
        public List<SearchEnter> SearchEnters
        {
            get
            {
                return _SearchEnters;
            }


        }
        private WordSplitType getWordSplitType(string value)
        {
            switch (value.ToUpper())
            {
                case "CN":
                    return WordSplitType.Cn;                    
                case "ENGLISH":
                    return WordSplitType.English;                    
                case "NONE":
                    return WordSplitType.None;
                default:
                    return WordSplitType.None;

                    
            }
        }
        private List<SubSearchEnter> GetSubKey(XElement x)
        {
            List<SubSearchEnter> lstsubenter = new List<SubSearchEnter>();
            if (x.HasElements)
            {
                foreach (var subx in x.Elements("subkey"))
                {
                    lstsubenter.Add(new SubSearchEnter() { Name = subx.Attribute("name").Value, Length = Convert.ToInt32(subx.Attribute("lenght").Value) });
                }
            }
            return lstsubenter;
        }

        /// <summary>
        /// 得到检索入口配置
        /// </summary>
        /// <param name="SearchEnter"></param>
        /// <returns></returns>
        public SearchEnter GetSearchEnter(string SearchEnter)
        {
            var x = from y in _SearchEnters
                    where y.Name == SearchEnter
                    select y;

            if (x.Count() > 0)
            {
                return x.First();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到配置文件下的所有索引入口
        /// </summary>
        /// <returns></returns>
        public List<Key> GetSearchEnterKey()
        {
            List<Key> results = new List<Key>();
            List<SearchEnter> lstEnter = SearchEnters;
            foreach (SearchEnter se in lstEnter)
            {
                if (se.Index == 0)
                {
                    continue;
                }
                if (se.SingleFile == true)
                {
                    Key key = new Key()
                    {
                        Name = se.Name,
                        Length = se.Length,
                        Encoder = getKeyType(se.WordSplit),
                    };
                    if (se.WordSplit == WordSplitType.None)
                    {
                        if (se.Name == "TX" || se.Name == "KW")
                        {
                            key.Encoder = "gb2312";
                        }
                    }
                    //索引的值类型
                    Value value = new Value();
                    value.Type = ValType.Com;

                    if (se.WordLocation == true)
                    {
                        value.Length = 8;
                    }
                    else
                    {
                        value.Length = 4;
                    }
                    key.value = value;
                    key.ToString();
                    results.Add(key);
                    if (se.SubKey.Count > 0)
                    {
                        //添加子索引
                        foreach (SubSearchEnter subse in se.SubKey)
                        {

                            //索引的键字类型
                            Key subkey = new Key()
                            {
                                Name = subse.Name,
                                Length = subse.Length,
                                Encoder = getKeyType(se.WordSplit)
                            };
                            //索引的值类型
                            Value subvalue = new Value();
                            subvalue.Length = 4;
                            subvalue.Type = ValType.Com;
                            subkey.value = subvalue;

                            results.Add(subkey);
                        }
                    }

                }
                else
                {
                    if (se.WordSplit == WordSplitType.Cn)
                    {
                        //中文1-3字词索引
                        for (int i = 1; i <= 3; i++)
                        {
                            //索引的键字类型
                            Key wordkey = new Key()
                            {
                                Name = se.Name + i.ToString(),
                                Length = i * 2,
                                Encoder = getKeyType(se.WordSplit)
                            };
                           

                            //索引的值类型
                            Value wordvalue = new Value();
                            if (se.WordLocation == true)
                            {
                                wordvalue.Length = 8;
                            }
                            else
                            {
                                wordvalue.Length = 4;
                            }

                            wordvalue.Type = ValType.Com;
                            wordkey.value = wordvalue;
                            results.Add(wordkey);
                        }

                    }
                    else
                    {
                        //英文 的数字索引
                        Key key = new Key()
                        {
                            Name = se.Name + "_0",
                            Length = 16,
                            Encoder = "utf-8"
                        };
                        //索引的值类型
                        Value value = new Value();
                        value.Length = 8;
                        value.Type = ValType.EnglishWord;
                        key.value = value;
                        results.Add(key);
                        //A-Z 索引
                        for (int i = 1; i <= 26; i++)
                        {
                            //索引的键字类型
                            Key wordkey = new Key()
                            {
                                Name = se.Name + "_" + (char)(i + 64),
                                Length = 16,
                                Encoder = "utf-8"
                            };
                            //索引的值类型
                            Value wordvalue = new Value();
                            wordvalue.Length = 8;
                            wordvalue.Type = ValType.EnglishWord;
                            wordkey.value = wordvalue;
                            results.Add(wordkey);
                        }
                      
                    }
                }

            }
            return results;
        }

        public override string ToString()
        {
            return string.Format("DataType:{0};ConfigFilePath:{1}", this.Type, this._ConfigFilePath);
        }
        /// <summary>
        /// 得到索引的键字的数据类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string getKeyType(WordSplitType type)
        {
            switch (type)
            {
                case WordSplitType.Cn:
                    return "gb2312";
                case WordSplitType.English:
                    return "utf-8";
                default:
                    return "utf-8";
            }
        }
    }
}
