#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: EPDSInterface.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-2-28 15:53:07
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
using log4net;
using System.Reflection;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Cfg.Port
{
    public enum EpdsInterfaceType
    {
        PubNo = 0,
        ApNo,
        CnDocNm
    }

    /// <summary>
    ///EPDSInterface 的摘要说明
    /// </summary>
    public class EPDSInterface
    {

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public EPDSInterface()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 调用BNS接口获取文件，返回为连接地址
        /// </summary>
        /// <param name="_strPubNo"></param>
        /// <param name="_bIsAllFile"></param>
        /// <param name="_eitype"></param>
        /// <returns></returns>
        public string[] GetBnsFiles(string _strPubNo, bool _bIsAllFile, EpdsInterfaceType _eitype)
        {
            logger.DebugFormat("***获取BNS文件,公开号为:[{0}]**开始", _strPubNo);

            //服务
            //EPDS_BSN_ServiceReference.EpdsInterface service = new EPDS_BSN_ServiceReference.EpdsInterface();
            EPDS_BSN_ServiceReference.Service service = new EPDS_BSN_ServiceReference.Service();

            //获取参数
            string publicNo = _strPubNo.Trim();
            string leibie = "";

            //调用服务,返回结果
            //string[] resultURL = service.BnsByPubNo(publicNo, _bIsAllFile);
            string[] resultURL = new string[0] { };
            switch (_eitype)
            {
                case EpdsInterfaceType.ApNo:
                    resultURL = service.BnsByAppNo(publicNo);
                    break;
                case EpdsInterfaceType.PubNo:
                    resultURL = service.BnsByPubNo(publicNo);
                    break;
                case EpdsInterfaceType.CnDocNm:
                    resultURL = service.BnsByPubNo(publicNo);
                    break;
            }


            logger.DebugFormat("***公开号为:[{0}]的BNS文件共[{1}]个", _strPubNo, resultURL.Length);

            return resultURL;
        }

        /// <summary>
        /// 获取PDF文件路径，如果不存在，则通过BSN接口下载至本地
        /// </summary>
        /// <param name="_strPubNo"></param>
        /// <param name="_bIsAllFile"></param>
        /// <param name="_eitype"></param>
        /// <returns></returns>
        public string[] GetPdfFilesByPath(string _strPubNo, EpdsInterfaceType _eitype)
        {
            logger.DebugFormat("***获取BNS文件,公开号为:[{0}]**开始", _strPubNo);

            //获取参数
            string publicNo = _strPubNo.Trim();

            string[] resultURL = new string[0] { };
            switch (_eitype)
            {
                case EpdsInterfaceType.ApNo:
                    resultURL = Cpic.Cprs2010.Cfg.Data.cnDataService.getImgPdfFile(publicNo);
                    break;
                case EpdsInterfaceType.PubNo:
                    resultURL = Cpic.Cprs2010.Cfg.Data.DocdbDataService.getImgPdfFile(publicNo);
                    break;
                case EpdsInterfaceType.CnDocNm:
                    //resultURL = service.BnsByPubNo(publicNo);
                    break;
            }

            ////----如果本地获取不到则通过接口获取,并下载至本地存储-----.//////
            if (true && (resultURL.Length < 1))
            {
                resultURL = DownLoadPdfFiles(_strPubNo, _eitype);
            }

            logger.DebugFormat("***公开号为:[{0}]的BNS文件共[{1}]个", _strPubNo, resultURL.Length);

            return resultURL;
        }

        /// <summary>
        /// 通过EPDS接口下载PDF文件
        /// </summary>
        /// <param name="_strPubNo"></param>
        /// <param name="_eitype"></param>
        /// <returns></returns>
        private string[] DownLoadPdfFiles(string _strPubNo, EpdsInterfaceType _eitype)
        {
            logger.DebugFormat("***下载BNS文件,公开号为:[{0}]**开始", _strPubNo);

            //获取参数
            string publicNo = _strPubNo.Trim();

            string[] resultURL = new string[0] { };

            try
            {
                resultURL = GetBnsFiles(publicNo, true, _eitype);
                if (resultURL.Length > 0 && resultURL[0].Contains(".pdf"))
                {
                    string strPdfPath = "";
                    switch (_eitype)
                    {
                        case EpdsInterfaceType.ApNo:
                            strPdfPath = Cpic.Cprs2010.Cfg.Data.cnDataService.getImgPdfFilePath(publicNo);
                            break;
                        case EpdsInterfaceType.PubNo:
                            strPdfPath = Cpic.Cprs2010.Cfg.Data.DocdbDataService.getImgPdfFilePath(publicNo);
                            break;
                        case EpdsInterfaceType.CnDocNm:
                            //resultURL = service.BnsByPubNo(publicNo);
                            break;
                    }

                    //如果目录不存在，则创建
                    if (!System.IO.Directory.Exists(strPdfPath))
                    {
                        System.IO.Directory.CreateDirectory(strPdfPath);
                    }

                    System.Net.WebClient mywebclient = new System.Net.WebClient();

                    for (int nIdx = 0; nIdx < resultURL.Length; nIdx++)
                    {
                        mywebclient.DownloadFile(resultURL[nIdx], string.Format(@"{0}\{1}_{2}.pdf", strPdfPath, publicNo, nIdx + 1));

                        resultURL[nIdx] = string.Format(@"{0}\{1}_{2}.pdf", strPdfPath, publicNo, nIdx + 1);
                    }
                }

                logger.DebugFormat("***下载结束,公开号为:[{0}]的BNS文件共[{1}]个", _strPubNo, resultURL.Length);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return resultURL;
        }
    }
}
