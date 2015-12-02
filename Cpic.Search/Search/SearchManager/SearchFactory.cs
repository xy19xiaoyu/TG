#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: SearchFactory.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-9-27 14:21:56
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
using Cpic.Cprs2010.SearchPool;
using Cpic.Cprs2010.SearchPool.SocketPool;
using Cpic.Cprs2010.Search;
using Cpic.Cprs2010.Log;
using Cpic.Cprs2010.SearchPool.FileSearchPool;
using System.IO;
using log4net;
using System.Reflection;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Search.SearchManager
{
    /// <summary>
    ///SearchManager 的摘要说明
    /// </summary>
    public static class SearchFactory
    {
        private static ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<SearchDbType, ISearchPool> ISearchPoolFactory = new Dictionary<SearchDbType, ISearchPool>();
        static readonly object padlock = new object();

        /// <summary>
        /// 工厂初始化
        /// </summary>
        public static void Ini(string msg)
        {
            Log.Log.log.Debug(Environment.NewLine + "SearchFactory.Ini() " + Environment.NewLine + "网站目录：" + System.Web.HttpContext.Current.Server.MapPath(".") + Environment.NewLine + "用户IP:" + msg + Environment.NewLine + "MainIP:" + CprsConfig.CnIP + Environment.NewLine + "Port:" + CprsConfig.CnPort + Environment.NewLine);
            SocketPool CnSearchPool;
            SocketPool DWPISearchPool;
            SocketPool DocDBSearchPool;


            //todo:3个池的初始化工作  
            //cn
            CnSearchPool = new SocketPool(20, 300, 20, CprsConfig.CnIP, CprsConfig.CnPort, 70, SearchDbType.Cn);
            //dwpi
            DWPISearchPool = new SocketPool(20, 300, 20, CprsConfig.DwpiIP, CprsConfig.DwpiPort, 70, SearchDbType.Dwpi);
            //docdb
            DocDBSearchPool = new SocketPool(20, 300, 20, CprsConfig.DocdbIP, CprsConfig.DocdbPort, 70, SearchDbType.DocDB);

            if (ISearchPoolFactory.ContainsKey(SearchDbType.Cn))
            {
                ISearchPoolFactory[SearchDbType.Cn] = CnSearchPool;
            }
            else
            {
                ISearchPoolFactory.Add(SearchDbType.Cn, CnSearchPool);
            }

            if (ISearchPoolFactory.ContainsKey(SearchDbType.Dwpi))
            {
                ISearchPoolFactory[SearchDbType.Dwpi] = DWPISearchPool;
            }
            else
            {
                ISearchPoolFactory.Add(SearchDbType.Dwpi, DWPISearchPool);
            }

            if (ISearchPoolFactory.ContainsKey(SearchDbType.DocDB))
            {
                ISearchPoolFactory[SearchDbType.DocDB] = DocDBSearchPool;
            }
            else
            {
                ISearchPoolFactory.Add(SearchDbType.DocDB, DocDBSearchPool);
            }

        }

        /// <summary>
        /// 工厂初始化
        /// </summary>
        public static void Ini(SearchPoolType _CnTy, SearchPoolType _DocdTy, SearchPoolType _DwpiTy, string msg)
        {
            Log.Log.log.Debug(Environment.NewLine + "SearchFactory.Ini() " + Environment.NewLine + "网站目录：" + System.Web.HttpContext.Current.Server.MapPath(".") + Environment.NewLine + "用户IP:" + msg + Environment.NewLine + "MainIP:" + CprsConfig.CnIP + Environment.NewLine + "Port:" + CprsConfig.CnPort + Environment.NewLine);
            ISearchPool CnSearchPool = null;
            ISearchPool DWPISearchPool = null;
            ISearchPool DocDBSearchPool = null;




            //todo:3个池的初始化工作  
            //cn
            switch (_CnTy)
            {
                case SearchPoolType.DefaultCn:
                case SearchPoolType.SocketCn:
                    CnSearchPool = new SocketPool(20, 300, 20, CprsConfig.CnIP, CprsConfig.CnPort, 70, SearchDbType.Cn);
                    break;
                case SearchPoolType.FileCN:
                    CnSearchPool = new FileSearchPool(2, 3, 1, "1", "1", 70, SearchDbType.Cn);
                    break;
            }


            //dwpi
            switch (_DwpiTy)
            {
                case SearchPoolType.DefaultDwpi:
                case SearchPoolType.SocketDwpi:
                    DWPISearchPool = new SocketPool(20, 300, 20, CprsConfig.DwpiIP, CprsConfig.DwpiPort, 70, SearchDbType.Dwpi);
                    break;
                case SearchPoolType.FileDwpi:
                    DWPISearchPool = new FileSearchPool(2, 3, 1, CprsConfig.DwpiIP, CprsConfig.DwpiPort, 70, SearchDbType.Dwpi);
                    break;
            }

            //docdb
            switch (_DocdTy)
            {
                case SearchPoolType.DefaultDocDB:
                case SearchPoolType.SocketDocDB:
                    DocDBSearchPool = new SocketPool(20, 300, 20, CprsConfig.DocdbIP, CprsConfig.DocdbPort, 70, SearchDbType.DocDB);
                    break;
                case SearchPoolType.FileDocdb:
                    DocDBSearchPool = new FileSearchPool(2, 3, 1, "1", "1", 70, SearchDbType.DocDB);
                    break;
            }


            if (ISearchPoolFactory.ContainsKey(SearchDbType.Cn))
            {
                ISearchPoolFactory[SearchDbType.Cn] = CnSearchPool;
            }
            else
            {
                ISearchPoolFactory.Add(SearchDbType.Cn, CnSearchPool);
            }

            if (ISearchPoolFactory.ContainsKey(SearchDbType.Dwpi))
            {
                ISearchPoolFactory[SearchDbType.Dwpi] = DWPISearchPool;
            }
            else
            {
                ISearchPoolFactory.Add(SearchDbType.Dwpi, DWPISearchPool);
            }

            if (ISearchPoolFactory.ContainsKey(SearchDbType.DocDB))
            {
                ISearchPoolFactory[SearchDbType.DocDB] = DocDBSearchPool;
            }
            else
            {
                ISearchPoolFactory.Add(SearchDbType.DocDB, DocDBSearchPool);
            }

        }

        /// <summary>
        /// 增加线程池
        /// </summary>
        /// <param name="_poolType"></param>
        /// <returns></returns>
        public static bool Grow(SearchDbType _poolType)
        {
            return ISearchPoolFactory[_poolType].Grow();
        }

        public static int getPoolLength(SearchDbType _poolType)
        {
            if (ISearchPoolFactory.ContainsKey(_poolType))
            {
                return ISearchPoolFactory[_poolType].Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 得到一个默认（中文）的检索连接
        /// </summary>
        /// <returns></returns>
        public static ISearch CreatSearch()
        {

            return ISearchPoolFactory[SearchDbType.Cn].getItem();
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <returns></returns>
        public static ResultInfo CreatDoSearch(SearchPattern _searchPattern)
        {
            ISearch mySearch = null;
            ResultInfo res = null;

            try
            {
                mySearch = CreatSearch(_searchPattern.DbType);
            }
            catch (Exception ex)
            {
                //strResutlMsg = ex.Message;               
            }

            if (mySearch == null)
            {
                res.HitMsg = "已超最大连接，请等待！";
            }
            else
            {
                res = mySearch.Search(_searchPattern);
                FreeSearch(mySearch);
            }
            return res;
        }


        /// <summary>
        /// 根据参数获取相应的检索连接
        /// </summary>
        /// <param name="_poolType"></param>
        /// <returns></returns>
        public static ISearch CreatSearch(SearchDbType _poolType)
        {
            return ISearchPoolFactory[_poolType].getItem();
        }
        /// <summary>
        /// 释放ISearchObject
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static bool FreeSearch(ISearch search)
        {
            return ISearchPoolFactory[search.Type].freeItem(search);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Dispose()
        {
            try
            {
                Log.Log.log.Debug(Environment.NewLine + "SearchFactory.Dispose() " + Environment.NewLine + "MainIP:" + CprsConfig.CnIP + Environment.NewLine + "Port:" + CprsConfig.CnPort + Environment.NewLine + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw new Exception("记录日志时发生错误！", ex);
            }
            try
            {
                foreach (var pool in ISearchPoolFactory)
                {
                    pool.Value.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("释放资源是发生错误", ex);
            }
        }

        /// <summary>
        /// 删除老CNP文件
        /// </summary>
        public static void del_OldCnpFile(SearchPattern _searchPattern)
        {
            try
            {
                ResultServices re = new ResultServices();
                string strFile = re.getResultFilePath(_searchPattern, false);
                FileInfo fileInfor = new FileInfo(strFile);

                string[] strArryFile = Directory.GetFiles(fileInfor.DirectoryName, fileInfor.Name.ToLower().Replace(".cnp", "") + ".*.*.cnp", SearchOption.TopDirectoryOnly);

                foreach (string strItme in strArryFile)
                {
                    try
                    {
                        File.Delete(strItme);
                    }
                    catch (Exception er)
                    {
                        log.Error(er.ToString());
                    }
                }

                fileInfor.Delete();
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 删除老CNP文件
        /// </summary>
        public static void del_OldCnpFile(int _nUid, string _sNo, SearchDbType _dbType)
        {
            try
            {
                SearchPattern _searchPattern = new SearchPattern();
                _searchPattern.UserId = _nUid;
                _searchPattern.SearchNo = _sNo;
                _searchPattern.DbType = _dbType;
                del_OldCnpFile(_searchPattern);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }
    }
}
