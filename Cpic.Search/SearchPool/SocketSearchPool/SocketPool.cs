#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: CnSocketPool.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-9-27 13:37:13
*	版    本: V1.0
*	备注描述: $Myparameter1$           
*
* 修改历史: 
*   ****NO_1:
*	修 改 人: 
*	修改日期: 
*	描    述: $Myparameter1$           
******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpic.Cprs2010.Search.SocketSearch;
using Cpic.Cprs2010.Search;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.SearchPool.SocketPool
{
    /// <summary>
    ///CnSocketPool 的摘要说明
    /// </summary>
    public class SocketPool : SearchPool.ISearchPool, IDisposable
    {

        #region 字段

        /// <summary>
        /// IP地址
        /// </summary>
        private string _IP;
        /// <summary>
        /// 端口号
        /// </summary>
        private string _Port;
        private Queue<Search.ISearch> _FreeItems;
        private int _Growth = 0;
        private int _Max;
        private int _Min;
        private SearchDbType _type;
        private List<Search.ISearch> _UsedItems;
        /// <summary>
        /// 请求连接等待最长时间单位（秒）
        /// </summary>
        private int WaitSecond;

        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min">池最小的数量</param>
        /// <param name="max">池最大的数量</param>
        /// <param name="growth">池每次增长的增长值</param>
        /// <param name="ip">对应的Main的 IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="type">等待时间</param>
        /// <param name="waitsecond">检索类型</param>
        public SocketPool(int min, int max, int growth, string ip, string port, int waitsecond, SearchDbType type)
        {

            IP = ip;
            Port = port;
            _Min = min;
            _Max = max;
            _Growth = growth;
            _type = type;
            this.WaitSecond = waitsecond;
            Ini();

        }
        #endregion
        #region ISearchPool 成员
        #region 属性
        /// <summary>
        /// 最大值
        /// </summary>
        public int Max
        {
            get
            {
                return _Max;
            }
            set
            {
                _Max = value;
            }
        }

        /// <summary>
        /// 空闲的ISearchItem
        /// </summary>
        public Queue<Search.ISearch> freeItems
        {
            get
            {
                return _FreeItems;
            }
            set
            {
                _FreeItems = value;
            }
        }

        /// <summary>
        /// 已经使用的ISearch
        /// </summary>
        public List<Search.ISearch> UsedItems
        {
            get
            {
                return _UsedItems;
            }
            set
            {
                _UsedItems = value;
            }
        }

        /// <summary>
        /// 最小值，即初始阶段的数量
        /// </summary>
        public int Min
        {
            get
            {
                return _Min;
            }
            set
            {
                _Min = value;
            }
        }
        /// <summary>
        /// 每次增长值
        /// </summary>
        public int Growth
        {
            get
            {
                return _Growth;
            }
            set
            {
                Growth = value;
            }
        }
        /// <summary>
        /// 当前所有的ISearchItem
        /// </summary>
        public int Count
        {
            get
            {
                return _UsedItems.Count + _FreeItems.Count;
            }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }

        /// <summary>
        /// 端口
        /// </summary>
        public string Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void Ini()
        {
            //初始化空闲的ISearch 队列
            _FreeItems = new Queue<Search.ISearch>();

            if (!string.IsNullOrEmpty(IP) && !string.IsNullOrEmpty(Port))
            {
                //初始化最小Min 个ISearch 对象
                for (int i = 1; i <= Min; i++)
                {
                    SocketSearch ss = new SocketSearch(i, IP, Port, _type); //初始化
                    ss.logIn(); //登录Main
                    _FreeItems.Enqueue(ss); //添加到队列中
                }
            }
            _UsedItems = new List<Search.ISearch>(); //初始化一个空的已经使用的ISearch对象列表  准备放已使用的ISearchObject
        }

        /// <summary>
        /// 得到一个空闲的ISearch
        /// </summary>
        /// <returns></returns>
        public Search.ISearch getItem()
        {
            // 去使用一个ISearch
            return useItem();
        }

        /// <summary>
        /// 使用一个ISearch
        /// </summary>
        /// <returns></returns>
        public Search.ISearch useItem()
        {
            //记录当前时间
            DateTime startDate = DateTime.Now;
            TimeSpan sp;
            Search.ISearch tmpIs;
            do
            {
                //判断当前池中是否还有空闲的ISearch对象
                if (_FreeItems.Count >= 1) //有
                {
                    lock (_FreeItems)
                    {                    
                        tmpIs = _FreeItems.Dequeue(); //从队列中拿到第一个ISearch
                        _UsedItems.Add(tmpIs); //放到已经使用列表中
                    }
                    return tmpIs; //返回
                }
                else //没有
                {
                    //得到时间差
                    sp = DateTime.Now - startDate;
                    //判断等待时间是否超过60s秒
                    if (sp.TotalSeconds > WaitSecond)
                    {
                        //超时
                        return null;
                    }
                    //再等100ss
                    System.Threading.Thread.Sleep(100);
                }

            } while (true);


        }

        /// <summary>
        /// 释放一个ISearch
        /// </summary>
        /// <param name="tmpIs"></param>
        /// <returns></returns>
        public bool freeItem(Search.ISearch tmpIs)
        {
            lock (_FreeItems)
            {
                _FreeItems.Enqueue(tmpIs); //添加到未使用队列的尾部
                _UsedItems.Remove(tmpIs); //从已使用列表中移除
            }
            return true;
        }

        /// <summary>
        /// ISearchPool 增长
        /// </summary>
        /// <returns></returns>
        public bool Grow()
        {
            //判断当前池的数量是否已经是最大值
            if (Count < Max)
            {
                //向空闲的ISearch队列中添加  Growth 个ISearch对象
                for (int i = 1; i <= Growth; i++)
                {
                    ISearch s = new SocketSearch(Count +1, IP, Port, _type);
                    s.logIn();
                    lock (_FreeItems)
                    {
                        _FreeItems.Enqueue(s);
                    }
                    if (Count >= Max)
                    {
                        break;
                    }
                }
                return true;
            }
            else //池已经达到最大
            {
                return false;
            }


        }

        #endregion


        #region IDisposable 成员

        public void Dispose()
        {
            if (_FreeItems.Count > 0)
            {
                do
                {
                    //释放空闲的ISearch Object
                    Search.ISearch tmp = _FreeItems.Dequeue();
                    tmp.logOut();
                } while (_FreeItems.Count > 0);
            }

            if (_UsedItems.Count > 0)
            {

                //释放已经使用ISearchObject
                for (int i = _UsedItems.Count - 1; i >= 0; i--)
                {
                    Search.ISearch tmp = _UsedItems[i];
                    tmp.logOut();
                }
            }
        }




        #endregion




    }
}
