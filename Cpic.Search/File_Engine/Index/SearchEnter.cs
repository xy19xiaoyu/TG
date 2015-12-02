using System;
using System.Collections.Generic;
using System.Text;

namespace Cpic.Cprs2010.Index
{
    public class SearchEnter: IComparable
    {
        private int _Index;

        /// <summary>
        /// 在这条数据中出现的未知
        /// </summary>
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }
        private string _Name;

        /// <summary>
        /// 检索入口名字
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Split;

        /// <summary>
        /// 是否按某一个字符进行二次拆分
        /// </summary>
        public string Split
        {
            get { return _Split; }
            set { _Split = value; }
        }
        private WordSplitType _WordSplit;

        /// <summary>
        /// 切词类型
        /// </summary>
        public WordSplitType WordSplit
        {
            get { return _WordSplit; }
            set { _WordSplit = value; }
        }
        private bool _SingleFile;
        /// <summary>
        /// 是否存放在独立的一个文件中
        /// </summary>
        public bool SingleFile
        {
            get { return _SingleFile; }
            set { _SingleFile = value; }
        }

       
        private int _Length;
        /// <summary>
        /// 索引键子的长度
        /// </summary>
        public int Length
        {
            get { return _Length; }
            set {_Length =value;}
        }
        private string _DataType;
        /// <summary>
        /// 索引键子的类型 目前只有int 跟 string 两种数据类型
        /// </summary>
        public string DataType
        {
            get { return _DataType; }
            set { _DataType = value; }
        }
        private bool  _WordLocation;

        /// <summary>
        /// 切词是是否记录单位位置
        /// </summary>
        public bool WordLocation
        {
            get { return _WordLocation; }
            set{_WordLocation = value;}
        }

        private string _Encoding;
        public string Encoding
        {
            get
            {
                return _Encoding;
            }
            set
            {
                _Encoding = value;
            }
        }
        #region IComparable 成员

        public int CompareTo(object obj)
        {
            if (obj is  SearchEnter)
            {
                return this.Index - ((SearchEnter)obj).Index;
            }
            else
            {
                throw new Exception("比较类不是SearchEnter");
            }
        }

        #endregion

        #region IComparable 成员

        int IComparable.CompareTo(object obj)
        {
            if (obj is SearchEnter)
            {
                return this.Index - ((SearchEnter)obj).Index;
            }
            else
            {
                throw new Exception("比较类不是SearchEnter");
            }
        }

        #endregion

        private List<SubSearchEnter> _SubKey;

        public List<SubSearchEnter> SubKey
        {
            get { return _SubKey; }
            set { _SubKey = value; }
        }

        public override string ToString()
        {
            return string.Format("name:{0};Length:{1};DataType:{2};Split:{3}", this.Name, this.Length, this.DataType, this.Split);
        }
    }
}
