using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cpic.Cprs2010.Index;


namespace Cpic.Cprs2010.Engine
{
    /// <summary>
    /// 索引类，.eee文件实体类 基于读文件的检索
    /// </summary>
    public class FileIndex : Index,IDisposable
    {
        #region 字段

        #endregion

        #region 属性
        public string strSearch;
        public List<int> Result;
        public List<byte[]> bResult;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public FileIndex(string strFilePath, Key key)
            : base(strFilePath, key)
        {
        }
        #endregion

        #region 析构函数
        ~FileIndex()
        {
            fs.Dispose();
        }
        #endregion

        #region 方法

        #region 共有
        public override List<int> SearchBetween(string Min, string Max)
        {
            BTNode FirstNode;
            BTNode LastNode;
            //得到第一个前方一直的节点
            FirstNode = FindFirstIntNode(Min);
            //判断有没有前方一直的
            if (FirstNode == null)
            {
                return null;
            }
            else
            {
                //如果找到的第一个大于 最小值的节点大于 最大值 返回null
                if (FirstNode.CompareToInt(Max) >= 0)
                {
                    return null;
                }
                //得到第最后一个前方一直的节点
                LastNode = FindMaxNode(Max);
                if (LastNode == null)
                {
                    throw new Exception("您输入的区间范围异常！请重新选择区间！");
                }
                //取值： 从第一个前方一致的节点的值的起始位置----最后一个前方一直的节点的值的结束位置
                return GetHisContentBetween(FirstNode.Value, LastNode.Value + Key.value.Length * LastNode.Hit);
            }
        }

        public void Search()
        {
            Result = Search(this.strSearch);         
        }

        /// <summary>
        /// 精确检索 中文
        /// </summary>
        public override List<int> Search(string strSearch)
        {
            BTNode Node = FindSingleNode(strSearch);
            if (Node == null)
            {
                return null;
            }
            else
            {
                return GetHisContent(Node.Value, Node.Hit);
            }
        }

        public void SearchEnWord()
        {
            bResult = SearchEnWord(this.strSearch);     
        }
        /// <summary>
        /// 精确检索 英文切词索引AB CL TI
        /// </summary>
        public override List<byte[]> SearchEnWord(string strSearch)
        {
            BTNode Node = FindSingleNode(strSearch);

            if (Node == null)
            {
                return null;
            }
            else
            {

                return GetHisContentEnWord(Node.Value, Node.Hit);
            }
        }

        /// <summary>
        /// 前方一致检索
        /// </summary>
        public override List<int> SearchLike(string strSearch)
        {

            BTNode FirstNode;
            BTNode LastNode;
            //得到第一个前方一直的节点
            FirstNode = FindFirstNode(strSearch);
            //判断有没有前方一直的
            if (FirstNode == null)
            {
                return null;
            }
            else
            {
                //得到第最后一个前方一直的节点
                LastNode = FindLastNode(strSearch);
                //取值： 从第一个前方一致的节点的值的起始位置----最后一个前方一直的节点的值的结束位置
                return GetHisContentBetween(FirstNode.Value, LastNode.Value + Key.value.Length * LastNode.Hit);
            }
        }

        /// <summary>
        /// 模糊检索 前方一直英文切词索引
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        public override List<byte[]> SearchLikeEnWord(string strSearch)
        {
            BTNode FirstNode;
            BTNode LastNode;
            //得到第一个前方一直的节点
            FirstNode = FindFirstNode(strSearch);
            //判断有没有前方一直的
            if (FirstNode == null)
            {
                return null;
            }
            else
            {
                //得到第最后一个前方一直的节点
                LastNode = FindLastNode(strSearch);
                //取值： 从第一个前方一致的节点的值的起始位置----最后一个前方一直的节点的值的结束位置
                return GetHisContentEnWord1(FirstNode.Value, LastNode.Value + FoliageNodeLength * LastNode.Hit);
            }
        }
        #endregion

        #region 私有

        #region 查找 一个绝对相等的节点

        /// <summary>
        /// 得到一个节点
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        private BTNode FindSingleNode(string strSearch)
        {
            //1.跳转到索引的根节点起始位置
            fs.Seek(RootNodeStart, SeekOrigin.Begin);
            int compare;
            BTNode PreNode = null;
            switch (Deep)
            {
                case 3:
                    //从根节点开始取值
                    for (int i = 0; i < Setp; i++)
                    {
                        //每次重新设置文件指针到第三层节点 +NodeLength*i 的位置
                        fs.Seek(RootNodeStart + NodeLength * i, SeekOrigin.Begin);
                        //读取一个根节点
                        BTNode Node = ReadTreeNode();

                        if (Node == null || Node.Value == -1)
                        {
                            //这是最后一个节点了，那么 文件指针往前 移一个节点
                            return FindMidNode(MidNodeStart + NodeLength * PreNode.Value, strSearch);
                        }

                        Node.ID = i;
                        Node.Depth = 3;
                        Node.Value = i * Setp;

                        compare = Node.CompareTo(Key.Encoder, strSearch);

                        //比较大小
                        switch (compare)
                        {
                            case -1:
                                PreNode = Node;
                                //还没找到，下一个
                                break;
                            case 0:
                                //找到了 去它的叶子节点拿值
                                return FindMidNode(MidNodeStart + NodeLength * Node.Value, strSearch);
                            case 1:
                                //第一个就已经不符合要求 
                                if (PreNode == null)
                                {
                                    return null;
                                }
                                //已经到了比它大的节点了 那有可能会出现在上一个节点的子节点                               
                                return FindMidNode(MidNodeStart + NodeLength * PreNode.Value, strSearch);
                        }

                    }
                    break;
                case 2:
                    return FindMidNode(MidNodeStart, strSearch);
                case 1:
                    return FindFoliageNode(FoliageNodeStart, strSearch);
            }
            return null;
        }
        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindMidNode(long Position, string strSearch)
        {
            int compare;
            BTNode PreNode = null;
            fs.Seek(Position, SeekOrigin.Begin);
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {
                //每次设置文件指针到中间节点的其实位置＋ 节点程度×I
                fs.Seek(Position + NodeLength * i, SeekOrigin.Begin);
                //读取一个中间节点
                BTNode Node = ReadTreeNode();

                if (Node == null || Node.Value == -1)
                {
                    //这是最后一个节点了，那么检索的东西有可能在它的叶子节点中 
                    return FindFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, strSearch);
                }
                Node.ID = i;
                Node.Depth = 2;
                compare = Node.CompareTo(Key.Encoder, strSearch);
                //比较大小
                switch (compare)
                {
                    case -1:
                        PreNode = Node;
                        //还没找到，下一个
                        break;
                    case 0:
                        //找到了 去它的叶子节点拿值
                        return FindFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, strSearch);
                    case 1:
                        //第一个就已经不符合要求 
                        if (PreNode == null)
                        {
                            return null;
                        }
                        //已经到了比它大的节点了 那有可能会出现在上一个中间节点的叶子节点
                        return FindFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, strSearch);
                }
                //如果到最后一个节点
                if (i == Setp - 1)
                {
                    //到这个节点的子节点去找
                    return FindFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, strSearch);
                }

            }
            return null;
        }

        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindFoliageNode(long Position, string strSearch)
        {
            int compare;
            fs.Seek(Position, SeekOrigin.Begin);
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {

                //读取一个叶子节点
                BTNode Node = ReadFoliageNode();

                if (Node == null || Node.Value == -1)
                {
                    return null;
                }
                Node.ID = i;
                Node.Depth = 1;

                compare = Node.CompareTo(Key.Encoder, strSearch);
                //比较大小
                switch (compare)
                {

                    case -1:
                        //还没找到，下一个
                        break;
                    case 0:
                        return Node;
                    case 1:
                        //已经到了比它大的节点了 没有了
                        return null;
                }

            }
            return null;
        }
        #endregion

        #region 查找第一个左边匹配的节点


        /// <summary>
        /// 前方一直 得到第一个左边匹配的节点 和最后一个左边一直的节点
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        private BTNode FindFirstNode(string strSearch)
        {
            //1.跳转到索引的根节点起始位置
            fs.Seek(RootNodeStart, SeekOrigin.Begin);
            int compare;
            BTNode PreNode = null;
            switch (Deep)
            {
                case 3:
                    //从根节点开始取值
                    for (int i = 0; i < Setp; i++)
                    {
                        //每次重新设置文件指针到第三层节点 +NodeLength*i 的位置
                        fs.Seek(RootNodeStart + NodeLength * i, SeekOrigin.Begin);
                        //读取一个根节点
                        BTNode Node = ReadTreeNode();

                        if (Node == null || Node.Value == -1)
                        {
                            //这是最后一个节点了，那么 文件指针往前 移一个节点
                            return FindFirstMidNode(MidNodeStart + NodeLength * PreNode.Value, strSearch);
                        }
                        Node.ID = i;
                        Node.Depth = 3;
                        Node.Value = i * Setp;

                        compare = Node.CompareToLeft(Key.Encoder, strSearch);

                        //比较大小
                        switch (compare)
                        {
                            case -1:
                                //还没找到，下一个
                                PreNode = Node;
                                break;
                            case 0:
                                if (PreNode == null)
                                {
                                    //找到了 去它的叶子节点拿值
                                    return FindFirstMidNode(MidNodeStart + NodeLength * Node.Value, strSearch);
                                }
                                //先判断它上个节点是否也有 前方一直的，如果有返回，如果没有 去它这个及子节点拿
                                BTNode tmpNode = FindFirstMidNode(MidNodeStart + NodeLength * PreNode.Value, strSearch);
                                if (tmpNode == null)
                                {

                                    //找到了 去它的叶子节点拿值
                                    return FindFirstMidNode(MidNodeStart + NodeLength * Node.Value, strSearch);
                                }
                                else
                                {
                                    return Node;
                                }

                            case 1:
                                //第一个就已经不符合要求 
                                if (PreNode == null)
                                {
                                    return null;
                                }
                                //已经到了比它大的节点了 那有可能会出现在上一个节点的子节点                               
                                return FindFirstMidNode(MidNodeStart + NodeLength * PreNode.Value, strSearch);
                        }

                    }
                    break;
                case 2:
                    return FindFirstMidNode(MidNodeStart, strSearch);
                case 1:
                    return FindFirstFoliageNode(FoliageNodeStart, strSearch);
            }
            return null;
        }

        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindFirstMidNode(long Position, string strSearch)
        {
            int compare;
            fs.Seek(Position, SeekOrigin.Begin);
            BTNode PreNode = null;
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {
                //每次设置文件指针到中间节点的其实位置＋ 节点程度×I
                fs.Seek(Position + NodeLength * i, SeekOrigin.Begin);
                //读取一个中间节点
                BTNode Node = ReadTreeNode();
                if (Node == null || Node.Value == -1)
                {
                    return FindFirstFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, strSearch);
                }
                Node.ID = i;
                Node.Depth = 2;

                compare = Node.CompareToLeft(Key.Encoder, strSearch);
                //比较大小
                switch (compare)
                {
                    case -1:
                        PreNode = Node;
                        //还没找到，下一个
                        break;
                    case 0:
                        if (PreNode == null)
                        {
                            return FindFirstFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, strSearch);
                        }
                        //先判断它上个节点是否也有 前方一直的，如果有返回，如果没有 去它这个及子节点拿
                        BTNode tmpNode = FindFirstFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, strSearch);
                        if (tmpNode == null)
                        {
                            //找到了 去它的叶子节点拿值
                            return FindFirstFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, strSearch);
                        }
                        else
                        {
                            return tmpNode;
                        }
                    case 1:

                        //第一个就已经不符合要求 
                        if (PreNode == null)
                        {
                            return null;
                        }
                        //已经到了比它大的节点了 那有可能会出现在上一个中间节点的叶子节点  
                        return FindFirstFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, strSearch);
                }
                //如果到最后一个节点
                if (i == Setp - 1)
                {
                    //到这个节点的子节点去找
                    return FindFirstFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, strSearch);
                }

            }
            return null;
        }

        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindFirstFoliageNode(long Position, string strSearch)
        {
            int compare;
            fs.Seek(Position, SeekOrigin.Begin);
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {

                //读取一个叶子节点
                BTNode Node = ReadFoliageNode();
                if (Node == null || Node.Value == -1)
                {
                    return null;
                }
                Node.ID = i;
                Node.Depth = 1;

                compare = Node.CompareToLeft(Key.Encoder, strSearch);
                //比较大小
                switch (compare)
                {

                    case -1:
                        //还没找到，下一个
                        break;
                    case 0:
                        //直到第一个左边相等的节点
                        return Node;
                    case 1:
                        //已经到了比它大的节点了 没有了
                        return null;
                }

            }
            return null;
        }
        #endregion

        #region 查找最后一个左边匹配的节点


        /// <summary>
        /// 前方一直 得到第一个左边匹配的节点 和最后一个左边一直的节点
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        private BTNode FindLastNode(string strSearch)
        {
            //1.跳转到索引的根节点起始位置
            fs.Seek(RootNodeStart, SeekOrigin.Begin);
            int compare;
            BTNode PreNode = null;
            switch (Deep)
            {
                case 3:
                    //从根节点开始取值
                    for (int i = 0; i < Setp; i++)
                    {
                        //每次重新设置文件指针到第三层节点 +NodeLength*i 的位置
                        fs.Seek(RootNodeStart + NodeLength * i, SeekOrigin.Begin);
                        //读取一个根节点
                        BTNode Node = ReadTreeNode();

                        if (Node == null || Node.Value == -1)
                        {
                            //这是最后一个节点了，那么 文件指针往前 移一个节点
                            return FindLastMidNode(MidNodeStart + NodeLength * PreNode.Value, strSearch);
                        }
                        Node.ID = i;
                        Node.Depth = 3;
                        Node.Value = i * Setp;

                        compare = Node.CompareToLeft(Key.Encoder, strSearch);

                        //比较大小
                        switch (compare)
                        {
                            case -1:
                                PreNode = Node;
                                //还没找到，下一个
                                break;
                            case 0:
                                if (i == Setp - 1)
                                {
                                    return FindLastMidNode(MidNodeStart + NodeLength * Node.Value, strSearch);
                                }
                                else
                                {
                                    PreNode = Node;
                                }
                                //如果前方以一致 继续找下一个，直到找到第一个不一致的
                                break;
                            case 1:
                                //第一个就已经不符合要求 
                                if (PreNode == null)
                                {
                                    return null;
                                }
                                //已经到了比它大的节点了 最后一个不前方一直的 那有可能会出现在上一个节点的子节点                               
                                return FindLastMidNode(MidNodeStart + NodeLength * PreNode.Value, strSearch);
                        }

                    }
                    break;
                case 2:
                    return FindLastMidNode(MidNodeStart, strSearch);
                case 1:
                    return FindLastFoliageNode(FoliageNodeStart, strSearch);
            }
            return null;
        }

        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindLastMidNode(long Position, string strSearch)
        {
            int compare;
            fs.Seek(Position, SeekOrigin.Begin);
            BTNode PreNode = null;
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {
                //每次设置文件指针到中间节点的其实位置＋ 节点程度×I
                fs.Seek(Position + NodeLength * i, SeekOrigin.Begin);
                //读取一个中间节点
                BTNode Node = ReadTreeNode();

                if (Node == null || Node.Value == -1)
                {
                    //这是最后一个节点了，那么检索的东西有可能在它的叶子节点中 
                    return FindLastFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, strSearch);
                }
                Node.ID = i;
                Node.Depth = 2;
                compare = Node.CompareToLeft(Key.Encoder, strSearch);
                //比较大小
                switch (compare)
                {
                    case -1:
                        PreNode = Node;
                        //还没找到，下一个
                        break;
                    case 0:
                        if (i == Setp - 1)
                        {
                            return FindLastFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, strSearch);
                        }
                        else
                        {
                            PreNode = Node;
                        }
                        //如果前方以一致 继续找下一个，直到找到第一个不一致的
                        break;
                    case 1:
                        //第一个就已经不符合要求 
                        if (PreNode == null)
                        {
                            return null;
                        }
                        //已经到了比它大的节点了 那有可能会出现在上一个中间节点的叶子节点
                        return FindLastFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, strSearch);
                }

                //如果到最后一个节点
                if (i == Setp - 1)
                {
                    //到这个节点的子节点去找
                    return FindLastFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, strSearch);
                }

            }
            return null;
        }

        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindLastFoliageNode(long Position, string strSearch)
        {
            int compare;
            BTNode PreNode = null;
            fs.Seek(Position, SeekOrigin.Begin);
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {
                //读取一个叶子节点
                BTNode Node = ReadFoliageNode();
                //如果读到最后一个节点,说明这个索引的最后一个节点也是符合检索词的前方一直
                if (Node == null || Node.Value == -1)
                {
                    return PreNode;
                }
                Node.ID = i;
                Node.Depth = 1;

                compare = Node.CompareToLeft(Key.Encoder, strSearch);
                //比较大小
                switch (compare)
                {

                    case -1:
                        PreNode = Node;
                        //还没找到，下一个
                        break;
                    case 0:
                        //前方一直就继续，直到找到第一个大于的
                        if (i == Setp - 1)
                        {
                            return Node;
                        }
                        else
                        {
                            PreNode = Node;
                        }
                        break;
                    case 1:
                        //已经到了比它大的节点了 没有了
                        return PreNode;
                }

            }
            return null;
        }
        #endregion


        #region 查找第一个小于等于的整数节点


        /// <summary>
        /// 查找第一个小于等于的整数节点
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        private BTNode FindFirstIntNode(string Min)
        {
            //1.跳转到索引的根节点起始位置
            fs.Seek(RootNodeStart, SeekOrigin.Begin);
            int compare;
            BTNode PreNode = null;
            switch (Deep)
            {
                case 3:
                    //从根节点开始取值
                    for (int i = 0; i < Setp; i++)
                    {
                        //每次重新设置文件指针到第三层节点 +NodeLength*i 的位置
                        fs.Seek(RootNodeStart + NodeLength * i, SeekOrigin.Begin);
                        //读取一个根节点
                        BTNode Node = ReadTreeNode();

                        if (Node == null || Node.Value == -1)
                        {
                            //这是最后一个节点了，那么 文件指针往前 移一个节点
                            return FindFirstIntMidNode(MidNodeStart + NodeLength * PreNode.Value, Min);
                        }
                        Node.ID = i;
                        Node.Depth = 3;
                        Node.Value = i * Setp;

                        compare = Node.CompareToInt(Min);

                        //比较大小
                        switch (compare)
                        {
                            case -1:
                                //还没找到，下一个
                                PreNode = Node;
                                break;
                            case 0:
                                if (PreNode == null)
                                {
                                    //找到了 去它的叶子节点拿值
                                    return FindFirstIntMidNode(MidNodeStart + NodeLength * Node.Value, Min);
                                }
                                //先判断它上个节点是否也有 前方一直的，如果有返回，如果没有 去它这个及子节点拿
                                BTNode tmpNode = FindFirstIntMidNode(MidNodeStart + NodeLength * PreNode.Value, Min);
                                if (tmpNode == null)
                                {

                                    //找到了 去它的叶子节点拿值
                                    return FindFirstIntMidNode(MidNodeStart + NodeLength * Node.Value, Min);
                                }
                                else
                                {
                                    return Node;
                                }
                            case 1:
                                //第一个就已经不符合要求 
                                if (PreNode == null)
                                {
                                    return null;
                                }
                                //已经到了比它大的节点了 那有可能会出现在上一个节点的子节点                               
                                return FindFirstIntMidNode(MidNodeStart + NodeLength * PreNode.Value, Min);
                        }

                    }
                    break;
                case 2:
                    return FindFirstIntMidNode(MidNodeStart, Min);
                case 1:
                    return FindMinFoliageNode(FoliageNodeStart, Min);
            }
            return null;
        }

        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindFirstIntMidNode(long Position, string Min)
        {
            int compare;
            fs.Seek(Position, SeekOrigin.Begin);
            BTNode PreNode = null;
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {
                //每次设置文件指针到中间节点的其实位置＋ 节点程度×I
                fs.Seek(Position + NodeLength * i, SeekOrigin.Begin);
                //读取一个中间节点
                BTNode Node = ReadTreeNode();
                if (Node == null || Node.Value == -1)
                {
                    return FindMinFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, Min);
                }
                Node.ID = i;
                Node.Depth = 2;

                compare = Node.CompareToInt(Min);
                //比较大小
                switch (compare)
                {
                    case -1:
                        PreNode = Node;
                        //还没找到，下一个
                        break;
                    case 0:
                        if (PreNode == null)
                        {
                            return FindMinFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, Min);
                        }
                        //先判断它上个节点是否也有 前方一直的，如果有返回，如果没有 去它这个及子节点拿
                        BTNode tmpNode = FindMinFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, Min);
                        if (tmpNode == null)
                        {
                            //找到了 去它的叶子节点拿值
                            return FindMinFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, Min);
                        }
                        else
                        {
                            return tmpNode;
                        }
                    case 1:

                        //第一个就已经不符合要求 
                        if (PreNode == null)
                        {
                            return null;
                        }
                        //已经到了比它大的节点了 那有可能会出现在上一个中间节点的叶子节点  
                        return FindMinFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, Min);
                }
                //如果到最后一个节点
                if (i == Setp - 1)
                {
                    //到这个节点的子节点去找
                    return FindMinFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, Min);
                }

            }
            return null;
        }

        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindMinFoliageNode(long Position, string Min)
        {
            int compare;
            BTNode PreNode = null;
            fs.Seek(Position, SeekOrigin.Begin);
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {

                //读取一个叶子节点
                BTNode Node = ReadFoliageNode();
                if (Node == null || Node.Value == -1)
                {
                    return null;
                }
                Node.ID = i;
                Node.Depth = 1;

                compare = Node.CompareToInt(Min);
                //比较大小
                switch (compare)
                {

                    case -1:
                        PreNode = Node;
                        //还没找到，下一个
                        break;
                    case 0:
                        //直到第一个左边相等的节点
                        return Node;
                    case 1:
                        //已经到了比它大的节点了 没有了
                        return Node;
                }

            }
            return null;
        }
        #endregion

        #region 查找最后一个左边匹配的节点


        /// <summary>
        /// 前方一直 得到第一个左边匹配的节点 和最后一个左边一直的节点
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        private BTNode FindMaxNode(string Max)
        {
            //1.跳转到索引的根节点起始位置
            fs.Seek(RootNodeStart, SeekOrigin.Begin);
            int compare;
            BTNode PreNode = null;
            switch (Deep)
            {
                case 3:
                    //从根节点开始取值
                    for (int i = 0; i < Setp; i++)
                    {
                        //每次重新设置文件指针到第三层节点 +NodeLength*i 的位置
                        fs.Seek(RootNodeStart + NodeLength * i, SeekOrigin.Begin);
                        //读取一个根节点
                        BTNode Node = ReadTreeNode();

                        if (Node == null || Node.Value == -1)
                        {
                            //这是最后一个节点了，那么 文件指针往前 移一个节点
                            return FindMaxMidNode(MidNodeStart + NodeLength * PreNode.Value, Max);
                        }
                        Node.ID = i;
                        Node.Depth = 3;
                        Node.Value = i * Setp;

                        compare = Node.CompareToInt(Max);

                        //比较大小
                        switch (compare)
                        {
                            case -1:
                                PreNode = Node;
                                //还没找到，下一个
                                break;
                            case 0:
                                if (i == Setp - 1)
                                {
                                    return FindMaxMidNode(MidNodeStart + NodeLength * Node.Value, Max);
                                }
                                else
                                {
                                    PreNode = Node;
                                }
                                //如果前方以一致 继续找下一个，直到找到第一个不一致的
                                break;
                            case 1:
                                //第一个就已经不符合要求 
                                if (PreNode == null)
                                {
                                    return null;
                                }
                                //已经到了比它大的节点了 最后一个不前方一直的 那有可能会出现在上一个节点的子节点                               
                                return FindMaxMidNode(MidNodeStart + NodeLength * PreNode.Value, Max);
                        }

                    }
                    break;
                case 2:
                    return FindMaxMidNode(MidNodeStart, Max);
                case 1:
                    return FindMaxFoliageNode(FoliageNodeStart, Max);
            }
            return null;
        }

        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindMaxMidNode(long Position, string Max)
        {
            int compare;
            fs.Seek(Position, SeekOrigin.Begin);
            BTNode PreNode = null;
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {
                //每次设置文件指针到中间节点的其实位置＋ 节点程度×I
                fs.Seek(Position + NodeLength * i, SeekOrigin.Begin);
                //读取一个中间节点
                BTNode Node = ReadTreeNode();

                if (Node == null || Node.Value == -1)
                {
                    //这是最后一个节点了，那么检索的东西有可能在它的叶子节点中 
                    return FindMaxFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, Max);
                }
                Node.ID = i;
                Node.Depth = 2;
                compare = Node.CompareToInt(Max);
                //比较大小
                switch (compare)
                {
                    case -1:
                        PreNode = Node;
                        //还没找到，下一个
                        break;
                    case 0:
                        if (i == Setp - 1)
                        {
                            return FindMaxFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, Max);
                        }
                        else
                        {
                            PreNode = Node;
                        }
                        //如果前方以一致 继续找下一个，直到找到第一个不一致的
                        break;
                    case 1:
                        //第一个就已经不符合要求 
                        if (PreNode == null)
                        {
                            return null;
                        }
                        //已经到了比它大的节点了 那有可能会出现在上一个中间节点的叶子节点
                        return FindMaxFoliageNode(FoliageNodeStart + FoliageNodeLength * PreNode.Value, Max);
                }

                //如果到最后一个节点
                if (i == Setp - 1)
                {
                    //到这个节点的子节点去找
                    return FindMaxFoliageNode(FoliageNodeStart + FoliageNodeLength * Node.Value, Max);
                }

            }
            return null;
        }

        /// <summary>
        /// 根据当前文件指针按叶子节点查找
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        private BTNode FindMaxFoliageNode(long Position, string Max)
        {
            int compare;
            BTNode PreNode = null;
            fs.Seek(Position, SeekOrigin.Begin);
            //从根节点开始取值
            for (int i = 0; i < Setp; i++)
            {

                //读取一个叶子节点
                BTNode Node = ReadFoliageNode();
                //如果读到最后一个节点,说明这个索引的最后一个节点也是符合检索词的前方一直
                if (Node == null || Node.Value == -1)
                {
                    return PreNode;
                }
                Node.ID = i;
                Node.Depth = 1;

                compare = Node.CompareToInt(Max);
                //比较大小
                switch (compare)
                {

                    case -1:
                        PreNode = Node;
                        //还没找到，下一个
                        break;
                    case 0:
                        //前方一直就继续，直到找到第一个大于的
                        if (i == Setp - 1)
                        {
                            return Node;
                        }
                        else
                        {
                            PreNode = Node;
                        }
                        break;
                    case 1:
                        //已经到了比它大的节点了 没有了
                        return PreNode;
                }

            }
            return null;
        }
        #endregion

        #endregion

        #endregion


        #region IDisposable 成员

        public void Dispose()
        {
            fs.Dispose();
        }

        #endregion
    }
}
