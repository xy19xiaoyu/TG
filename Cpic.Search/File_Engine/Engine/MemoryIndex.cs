using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cpic.Cprs2010.Index;
using Cpic.Cprs2010.Search;

namespace Cpic.Cprs2010.Engine
{
    /// <summary>
    /// 索引类，.eee文件实体类，把索引文件索引 加载到内存中 检测
    /// </summary>
    public class MemoryIndex : Index
    {
        #region 字段

        /// <summary>
        /// 树的顶层节点列表
        /// </summary>
        private List<BTNode> _BTree;
        
        /// <summary>
        /// 树的顶层节点列表
        /// </summary>
        public List<BTNode> BTree
        {
            get { return _BTree; }
            set { _BTree = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MemoryIndex(string strFilePath, Key key)
            : base(strFilePath, key)
        {
              BTree = new List<BTNode>();

            fs.Seek(RootNodeStart, SeekOrigin.Begin);
            //加载根节点

            if (Deep == 1)
            {
                //树只有一层
                for (int i = 0; i < Setp; i++)
                {
                    BTNode Node = ReadFoliageNode();
                    if (Node == null || Node.Value == -1)
                    {
                        break;
                    }
                    //id
                    Node.ID = i;
                    //节点深度
                    Node.Depth = Deep;
                    BTree.Add(Node);
                }

            }
            else
            {
                //2曾或者三层
                for (int i = 0; i < Setp; i++)
                {
                    //每次设置文件指针到 根节点起始位置+NodeLength*i
                    fs.Seek(RootNodeStart + (NodeLength* i), SeekOrigin.Begin);
                    BTNode Node = ReadTreeNode();
                    if (Node == null || Node.Value == -1)
                    {
                        break;
                    }
                    //id
                    Node.ID = i;
                    //节点深度
                    Node.Depth = Deep;
                    //位移
                    Node.Value = i * 1000;
                    //加载子节点
                    Node.ChildrenNodes = GetChildNodes(Node);
                    BTree.Add(Node);
                }

            }
        }
        #endregion

        #region 析构函数
        ~MemoryIndex()
        {
            fs.Dispose();
        }
        #endregion

        #region 方法

        #region 私有
        /// <summary>
        /// 得到一个节点所有的子节点
        /// </summary>
        /// <param name="_fs">索引文件</param>
        /// <param name="btn">BTreeNode</param>
        /// <returns></returns>
        private List<BTNode> GetChildNodes(BTNode btn)
        {

            long tmpStartPointer;
            List<BTNode> childNodes = new List<BTNode>();


            if (btn.IsFoliage)
            {
                return null;
            }

            if (btn.Depth == 3)
            {
                //如果这个节点是第三级节点 那么的它的子节点的起始位置就是 二级节点的起始位置+偏移量
                tmpStartPointer = MidNodeStart + (NodeLength) * btn.Value;
            }
            else
            {
                //如果这个节点是第二级节点 那么的它的子节点的起始位置就是 叶子节点的起始位置+偏移量
                tmpStartPointer = FoliageNodeStart + (NodeLength + 4) * btn.Value;
            }

            fs.Seek(tmpStartPointer, SeekOrigin.Begin);
            //加载根节点
            if (btn.Depth == 2)
            {
                //加载叶子节点
                for (int i = 0; i < Setp; i++)
                {
                    BTNode Node = ReadFoliageNode();
                    if (Node == null || Node.Value == -1)
                    {
                        break;
                    }
                    //id
                    Node.ID = i;
                    //节点深度
                    Node.Depth = btn.Depth - 1;
                    childNodes.Add(Node);
                }

            }
            else
            {
                for (int i = 0; i < Setp; i++)
                {
                    //重新设置文件指针到
                    fs.Seek(tmpStartPointer + (NodeLength) * i, SeekOrigin.Begin);
                    BTNode Node = ReadTreeNode();
                    if (Node == null || Node.Value == -1)
                    {
                        break;
                    }
                    //id
                    Node.ID = i;
                    //节点深度
                    Node.Depth = btn.Depth - 1;
                    //加载子节点
                    Node.ChildrenNodes = GetChildNodes(Node);
                    childNodes.Add(Node);
                }
            }

            return childNodes;
        }
        /// <summary>
        /// 得到一个节点
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        private BTNode FindSingleNode(List<BTNode> NodeList, string strSearch)
        {
            BTNode result = null;
            int compare;
            foreach (BTNode Node in NodeList)
            {
                compare = Node.CompareTo(Key.Encoder.ToString().Replace("8","-8"), strSearch);
                switch (compare)
                {
                    case -1:
                        if (Node.ID == NodeList.Count - 1 && Node.Depth != 1)
                        {
                            //如果是最后一个节点 需要对它的子节点进行检索
                            return FindSingleNode(Node.ChildrenNodes, strSearch);
                        }
                        break;
                    case 0:
                        //找到了
                        switch (Node.Depth)
                        {
                            case 1:
                                //这个节点在一层
                                result = Node;
                                break;
                            case 2:
                                //如果这个节点在二层
                                result = Node.ChildrenNodes[0];
                                break;
                            case 3:
                                //这个节点在三层
                                result = Node.ChildrenNodes[0].ChildrenNodes[0];
                                break;
                        }
                        return result;
                    case 1:
                        //这个节点值大于当前检值则 这个检索值的肯定出现在 这个节点的上一个节点上
                        if (Node.ID == 0)
                        {
                            //如果这个节点是第一个 那就是没有       
                            return null;
                        }
                        else if (Node.Depth == 1) //如果是叶子节点
                        {
                            return null;
                        }
                        else
                        {
                            return FindSingleNode(NodeList[Node.ID - 1].ChildrenNodes, strSearch);
                        }
                }
            }
            return null;
        }

        /// <summary>
        /// 返回量个BTNode叶子节点
        /// </summary>
        private List<BTNode> FindNodes(string strSearch)
        {
            throw new System.NotImplementedException();
        }
        #endregion 
        
        #region 共有  
        /// <summary>
        /// 检索
        /// </summary>
        public override List<int> Search(string strSearch)
        {
            BTNode result = null;

            result = FindSingleNode(BTree, strSearch);

            if (result == null)
            {
                return null;
            }
            return GetHisContent(result.Value, result.Hit);
        }
        /// <summary>
        /// 模糊检索 前方一直
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        public override List<int> SearchLike(string strSearch)
        {
            return base.SearchLike(strSearch);
        }

        /// <summary>
        /// 检索
        /// </summary>
        public override List<byte[]> SearchEnWord(string strSearch)
        {
            BTNode result = null;

            result = FindSingleNode(BTree, strSearch);

            if (result == null)
            {
                return null;
            }
            return GetHisContentEnWord(result.Value, result.Hit);
        }
        /// <summary>
        /// 模糊检索 前方一直
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        public override List<byte[]> SearchLikeEnWord(string strSearch)
        {
            return base.SearchLikeEnWord(strSearch);
        }
        #endregion

        #endregion
    }
}
