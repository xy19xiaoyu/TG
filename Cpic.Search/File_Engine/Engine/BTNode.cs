using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpic.Cprs2010.Index;

namespace Cpic.Cprs2010.Engine
{
    public class BTNode
    {
        private int _id;

        /// <summary>
        /// ID
        /// </summary>
        public int ID
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
        /// 深度
        /// </summary>
        private int _Depth;

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth
        {
            get { return _Depth; }
            set { _Depth = value; }
        }
        /// <summary>
        /// 节点值
        /// </summary>
        private string _Key;

        /// <summary>
        /// 节点值
        /// </summary>
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        /// <summary>
        /// 偏移量
        /// </summary>
        private int _Value;

        /// <summary>
        /// 偏移量
        /// </summary>
        public int Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        /// <summary>
        /// 命中篇数
        /// </summary>
        private int _Hit;

        /// <summary>
        /// 命中篇数
        /// </summary>
        public int Hit
        {
            get { return _Hit; }
            set { _Hit = value; }
        }

        public BTNode(int _depth, string _key, int _value, int _hit)
        {
            this.Depth = _depth;
            this.Key = _key;
            this.Value = _value;
            this.Hit = _hit;
        }

        public BTNode()
        {
        }
        /// <summary>
        /// 是否是叶子节点
        /// </summary>
        public bool IsFoliage
        {
            get
            {
                if (Depth == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private List<BTNode> _ChildrenNodes;




        /// <summary>
        /// 子节点
        /// </summary>
        public List<BTNode> ChildrenNodes
        {
            get
            {
                return _ChildrenNodes;
            }
            set
            {
                _ChildrenNodes = value;
            }
        }
        
        /// <summary>
        /// 这个节点是否有子节点
        /// </summary>
        public bool HasChildren
        {
            get
            {
                if (_Depth == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 绝对相等的比较
        /// </summary>
        /// <param name="Encoding"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public int CompareTo(string Encoding, string str)
        {
            if (Encoding.ToUpper() == "UTF-8")
            {
                if (str.Length < Key.Length)
                {
                    str = str.PadRight(Key.Length, ' ');
                }
            }
            else
            {
                if (str.Length < Key.Length)
                {
                    str = str.PadRight(Key.Length, '　');
                }
            }
            byte[] bythis = System.Text.Encoding.GetEncoding(Encoding).GetBytes(Key);
            byte[] bystr = System.Text.Encoding.GetEncoding(Encoding).GetBytes(str);
            for (int i = 0; i <= bythis.Length -1; i++)
            {
                if (bythis[i] == bystr[i])
                {
                    continue;
                }
                else if (bythis[i] > bystr[i])
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            return 0;

        }

        /// <summary>
        /// 前方一直的比较
        /// </summary>
        /// <param name="Encoding"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public int CompareToLeft(string Encoding, string str)
        {

            byte[] bythis = System.Text.Encoding.GetEncoding(Encoding).GetBytes(Key);
            byte[] bystr = System.Text.Encoding.GetEncoding(Encoding).GetBytes(str);
            for (int i = 0; i <= bystr.Length - 1; i++)
            {
                if (i >= bythis.Length)
                {
                    break;
                }
                if (bythis[i] == bystr[i])
                {
                    continue;
                }
                else if (bythis[i] > bystr[i])
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            return 0;

        }

        public int CompareToInt(string str)
        {
            if (Key.Length < str.Length)
            {
                throw new Exception("检索格式错误！请重新输入！");
            }
            int intthis = Convert.ToInt32(Key.Substring(0,str.Length));
            int intstr = Convert.ToInt32(str);
            return intthis.CompareTo(intstr);           
        }
        public override string ToString()
        {
            if (_Depth == 1)
            {
                return string.Format("Key:{0};Value:{1};Hit:{2};id={3}", Key, Value, Hit,ID);
            }
            else
            {
                return string.Format("Key:{0};Value:{1};Deep:{2};id={3}", Key, Value, Depth,ID);
            }

        }
    }
}
