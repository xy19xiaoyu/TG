#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: FileMeerySearch.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-11-2 15:28:31
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
using Cpic.Cprs2010.Engine;
using log4net;
using System.Reflection;
[assembly: log4net.Config.DOMConfigurator()]

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Search.FileMeerySearch
{
    /// <summary>
    ///FileMeerySearch 的摘要说明
    /// </summary>
    public class FileMeerySearch : ISearch
    {

        #region "字段"
        private int _id;
        private Search.SearchStatus _Status;
        private string _Ip;
        private string _Port;
        private SearchDbType _Type;
        public Cpic.Cprs2010.Engine.FileFinder fd;
        private ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region 构造函数
        public FileMeerySearch(int id, string Ip, string Port, SearchDbType type)
        {
            _id = id;
            _Ip = Ip;
            _Port = Port;
            _Type = type;
            Ini();
        }
        #endregion

        #region 属性
        public string IP
        {
            get
            {
                return _Ip;
            }
            set
            {
                _Ip = value;
            }
        }
        public string Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
            }
        }
        #endregion


        #region ISearch 成员

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

        public Search.SearchStatus Status
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

        public bool Ini()
        {
            string ConfigFilePath = "";
            string IndexDirectory = "";

            switch (Type)
            {
                case SearchDbType.Cn:
                    ConfigFilePath = System.Configuration.ConfigurationManager.AppSettings["CNConfigFile"].ToString();
                    IndexDirectory = System.Configuration.ConfigurationManager.AppSettings["CNIndPath"].ToString();
                    break;
                case SearchDbType.DocDB:
                    ConfigFilePath = System.Configuration.ConfigurationManager.AppSettings["DocDBConfigFile"].ToString();
                    IndexDirectory = System.Configuration.ConfigurationManager.AppSettings["DocDBIndPath"].ToString();
                    break;
                case SearchDbType.Dwpi:
                    ConfigFilePath = System.Configuration.ConfigurationManager.AppSettings["DwpiConfigFile"].ToString();
                    IndexDirectory = System.Configuration.ConfigurationManager.AppSettings["DwpiIndPath"].ToString();
                    break;

            }
            if (string.IsNullOrEmpty(ConfigFilePath) || string.IsNullOrEmpty(IndexDirectory))
            {
                return false;
            }
            fd = new FileFinder(ConfigFilePath, IndexDirectory);
            //todo:nothing;
            return true;
        }

        public bool logIn()
        {
            //todo:nothing;
            return true;
        }

        public bool logOut()
        {
            //todo:nothing;
            return true;
        }

        public ResultInfo Search(SearchPattern _searchPattern)
        {
            Cpic.Cprs2010.Engine.SearchPattern sp = new Cpic.Cprs2010.Engine.SearchPattern(fd.Config);
            return sp.Search(_searchPattern, fd);
        }

        #endregion

        #region ISearch 成员

        public SearchDbType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                this._Type = value;
            }
        }
        #endregion
    }
}
