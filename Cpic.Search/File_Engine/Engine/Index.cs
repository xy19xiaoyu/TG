using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cpic.Cprs2010.Index;


namespace Cpic.Cprs2010.Engine
{
    /// <summary>
    /// 索引类，.eee文件实体类
    /// </summary>
    public class Index
    {
        #region 字段

        /// <summary>
        /// 索引文件所保存的检索入口的名称
        /// </summary>
        private Key _Key;

        /// <summary>
        /// 树的深度
        /// </summary>
        private int _Deep;

        /// <summary>
        /// B+树每个节点的子节点的数量{1,1000}
        /// </summary>
        private int _Setp = 1000;

        /// <summary>
        /// 文件长度
        /// </summary>
        private double _FileLength;

        /// <summary>
        /// 索引文件的路径
        /// </summary>
        private string _FilePath;

        /// <summary>
        /// 数据集起始位置
        /// </summary>
        public const int DataStart = 1000;

        /// <summary>
        /// 叶子节点的起始位置
        /// </summary>
        private long _FoliageNodeStart;

        /// <summary>
        /// 叶子节点的结束位置，通过这个位置读配置文件信息
        /// </summary>
        private long _FoliageNodeEnd;

        /// <summary>
        /// 根节点起始位置
        /// </summary>
        private long _RootNodeStart;

        /// <summary>
        /// 第二级节点的起始位置
        /// </summary>
        private long _MidNodeStart;

        /// <summary>
        /// 索引文件流
        /// </summary>
        private FileStream _fs;

        /// <summary>
        /// 节点长度
        /// </summary>
        private int _NodeLength;

        /// <summary>
        /// 叶子节点长度
        /// </summary>
        private int _FoliageNodeLength;


        #endregion

        #region 属性
        /// <summary>
        /// 索引文件所保存的检索入口的名称
        /// </summary>
        public Key Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        /// <summary>
        /// 树的深度
        /// </summary>
        public int Deep
        {
            get { return _Deep; }
            set { _Deep = value; }
        }

        /// <summary>
        /// B+树每个节点的子节点的数量{1,1000}
        /// </summary>
        public int Setp
        {
            get { return _Setp; }
        }

        /// <summary>
        /// 文件长度
        /// </summary>
        public double FileLength
        {
            get { return _FileLength; }
            set { _FileLength = value; }
        }

        /// <summary>
        /// 索引文件的路径
        /// </summary>
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        /// <summary>
        /// 叶子节点的起始位置
        /// </summary>
        public long FoliageNodeStart
        {
            get { return _FoliageNodeStart; }
            set { _FoliageNodeStart = value; }
        }

        /// <summary>
        /// 叶子节点的结束位置，通过这个位置读配置文件信息
        /// </summary>
        public long FoliageNodeEnd
        {
            get { return _FoliageNodeEnd; }
            set { _FoliageNodeEnd = value; }
        }

        /// <summary>
        /// 根节点起始位置
        /// </summary>
        public long RootNodeStart
        {
            get { return _RootNodeStart; }
            set { _RootNodeStart = value; }
        }

        /// <summary>
        /// 第二级节点的起始位置
        /// </summary>
        public long MidNodeStart
        {
            get { return _MidNodeStart; }
            set { _MidNodeStart = value; }
        }

        /// <summary>
        /// 索引文件流
        /// </summary>
        public FileStream fs
        {
            get { return _fs; }
            set { _fs = value; }
        }

        /// <summary>
        /// 节点长度
        /// </summary>
        public int NodeLength
        {
            get { return _NodeLength; }
            set { _NodeLength = value; }
        }

        /// <summary>
        /// 叶子节点长度
        /// </summary>
        public int FoliageNodeLength
        {
            get { return _FoliageNodeLength; }
            set { _FoliageNodeLength = value; }
        }
        /// <summary>
        /// 配置信息等
        /// </summary>
        public byte[] by4;
        /// <summary>
        /// 树节点
        /// </summary>
        public byte[] byTreeNode;
        /// <summary>
        /// 叶子节点
        /// </summary>
        public byte[] byFoliage;

        /// <summary>
        /// 每次读取文件的大小
        /// </summary>
        public int readLength;

        public string strSearch;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public Index(string strFilePath, Key key)
        {

            this.FilePath = strFilePath;
            this.Key = key;
            NodeLength = Key.Length + 4;
            FoliageNodeLength = Key.Length + 8;

            by4 = new byte[4];
            byTreeNode = new byte[NodeLength];
            byFoliage = new byte[FoliageNodeLength];

            ///打开索引文件
            _fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);

            //读叶子节点起始位置
            readLength = _fs.Read(by4, 0, 4);
            _FoliageNodeStart = BitConverter.ToInt32(by4, 0);

            //读叶子节点的结束位置
            readLength = _fs.Read(by4, 0, 4);
            _FoliageNodeEnd = BitConverter.ToInt32(by4, 0);

            //得到树的深度
            _fs.Seek(_FoliageNodeEnd, SeekOrigin.Begin);
            readLength = _fs.Read(by4, 0, 4);
            Deep = BitConverter.ToInt32(by4, 0);

            //根据树的深度判断树的根节点的位置
            switch (Deep)
            {
                case 3:

                    //中间节点起始位置
                    readLength = _fs.Read(by4, 0, 4);
                    _MidNodeStart = _FoliageNodeEnd + BitConverter.ToInt32(by4, 0);

                    //跟节点的其实位置
                    readLength = _fs.Read(by4, 0, 4);
                    _RootNodeStart = _FoliageNodeEnd + BitConverter.ToInt32(by4, 0);
                    break;
                case 2:
                    //中间节点
                    _fs.Seek(_FoliageNodeEnd + 4, SeekOrigin.Begin);
                    readLength = _fs.Read(by4, 0, 4);
                    _RootNodeStart = _FoliageNodeEnd + BitConverter.ToInt32(by4, 0);
                    _MidNodeStart = _RootNodeStart;
                    break;
                case 1:
                    _RootNodeStart = _FoliageNodeStart;
                    break;
                default:
                    throw new Exception("索引树深度数据异常");
            }


        }
        #endregion

        #region 析构函数
        ~Index()
        {
            _fs.Dispose();
        }
        #endregion

        #region 方法


        /// <summary>
        /// 把二进制数组转换成字符串
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string GetString(byte[] b)
        {
            if (Key.Encoder == "gb2312")
            {
                return Encoding.GetEncoding("gb2312").GetString(b, 0, Key.Length).Trim().PadRight(Key.Length / 2, '　');
            }
            else
            {
                return Encoding.UTF8.GetString(b, 0, Key.Length).Trim().PadRight(Key.Length, ' ');
            }

        }

        public virtual List<int> SearchBetween (string FirstSearch, string LastSearch)
        {
            throw new Exception("此函数必须被重写");
        }
        /// <summary>
        /// 检索
        /// </summary>
        public virtual List<int> Search(string strSearch)
        {
            throw new Exception("此函数必须被重写");
        }

        /// <summary>
        /// 检索
        /// </summary>
        public virtual List<int> SearchLike(string strSearch)
        {
            throw new Exception("此函数必须被重写");
        }

        /// <summary>
        /// 检索
        /// </summary>
        public virtual List<byte[]> SearchEnWord(string strSearch)
        {
            throw new Exception("此函数必须被重写");
        }

        /// <summary>
        /// 检索
        /// </summary>
        public virtual List<byte[]> SearchLikeEnWord(string strSearch)
        {
            throw new Exception("此函数必须被重写");
        }

        /// <summary>
        /// 得到命中篇数
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        public List<int> GetHisContent(long Position, int Hits)
        {
           
            List<int> lstRsult = new List<int>();
            //重新设置文件指针到
            fs.Seek(Position, SeekOrigin.Begin);
            for (int i = 0; i <= Hits - 1; i++)
            {
                byte[] byhis = new byte[Key.value.Length];
                readLength = fs.Read(byhis, 0, Key.value.Length);
                if (readLength < Key.value.Length)
                {
                    break;
                }
                lstRsult.Add(BitConverter.ToInt32(byhis, 0));
            }
            return lstRsult;
        }

        /// <summary>
        /// 得到命中篇数
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        public List<int> GetHisContentBetween(long Position, long LastPosition)
        {
           
            List<int> lstRsult = new List<int>();
            //重新设置文件指针到
            fs.Seek(Position, SeekOrigin.Begin);
            while(fs.Position < LastPosition)
            {
                byte[] byhis = new byte[Key.value.Length];
                readLength = fs.Read(byhis, 0, Key.value.Length);
                if (readLength < Key.value.Length)
                {
                    break;
                }
                lstRsult.Add(BitConverter.ToInt32(byhis, 0));
            }
            return lstRsult;
        }

        /// <summary>
        /// 得到命中篇数
        /// </summary>        
        /// <returns></returns>
        public List<byte[]> GetHisContentEnWord(long Position, int Hits)
        {
           
            List<byte[]> lstRsult = new List<byte[]>();
            //重新设置文件指针到
            fs.Seek(Position, SeekOrigin.Begin);
            for (int i = 0; i <= Hits - 1; i++)
            {
                byte[] byhis = new byte[Key.value.Length];
                readLength = fs.Read(byhis, 0, Key.value.Length);
                if (readLength < Key.value.Length)
                {
                    break;
                }
                lstRsult.Add(byhis);
            }
            return lstRsult;
        }


        /// <summary>
        /// 得到命中篇数
        /// </summary>        
        /// <returns></returns>
        public List<byte[]> GetHisContentEnWord1(long Position, long LastPosition)
        {
           
            List<byte[]> lstRsult = new List<byte[]>();
            //重新设置文件指针到
            fs.Seek(Position, SeekOrigin.Begin);
            while (fs.Position < LastPosition)
            {
                byte[] byhis = new byte[Key.value.Length];
                readLength = fs.Read(byhis, 0, Key.value.Length);
                if (readLength < Key.value.Length)
                {
                    break;
                }
                lstRsult.Add(byhis);
            }
            return lstRsult;
        }


        /// <summary>
        /// 从文件当前指针得到一个节点
        /// </summary>
        /// <returns>
        /// null 没度到
        /// </returns>
        public BTNode ReadTreeNode()
        {
            readLength = fs.Read(byTreeNode, 0, NodeLength);
            if (readLength < NodeLength)
            {
                return null;
            }
            return new BTNode()
            {
                Key = GetString(byTreeNode),
                Value = BitConverter.ToInt32(byTreeNode, Key.Length)
            };
        }

        /// <summary>
        /// 从文件当前指针得到一个叶子节点
        /// </summary>
        /// <returns>
        /// null 没有找到
        /// </returns>
        public BTNode ReadFoliageNode()
        {
            //到叶子节点的最后一位了            
            if (fs.Position >= FoliageNodeEnd)
            {
                return null;
            }
            readLength = fs.Read(byFoliage, 0, FoliageNodeLength);
            if (readLength < FoliageNodeLength)
            {
                return null;
            }
            return new BTNode()
            {

                Key = GetString(byFoliage),
                Value = BitConverter.ToInt32(byFoliage, Key.Length),
                Hit = BitConverter.ToInt32(byFoliage, NodeLength),
            };
        }
        public override string ToString()
        {
            return string.Format("Name:{0};KeyLength:{1};Deep:{2};FilePath:{3}", Key.Name, Key.Length, Deep, FilePath);
        }
        #endregion

    }
}
