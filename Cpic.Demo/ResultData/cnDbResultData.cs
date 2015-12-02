#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: cnDbResultData.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-1-25 16:10:34
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
using System.Collections;
using Cpic.Cprs2010.Search;
using System.Data.Linq;
using log4net;
using System.Reflection;
using Cpic.Cprs2010.Cfg;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Search.ResultData
{
    /// <summary>
    ///cnDbResultData 的摘要说明
    /// </summary>
    public class cnDbResultData
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public cnDbResultData()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 得到某一结果文件的全部数据
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="SortExpression">排序</param>
        /// <returns>list int</returns>
        public IEnumerable GetResultList(SearchPattern sp, string SortExpression)
        {
            ResultServices res = new ResultServices();
            List<int> lstNo = res.GetResultList(sp, SortExpression);

            IEnumerable ien = GetResult(lstNo);

            return ien;

        }


        /// <summary>
        /// 得到某一结果文件的某一页数据 并按某一字段排序
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>list(int)</int></returns>
        public IEnumerable GetResultList(SearchPattern sp, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {

                ResultServices res = new ResultServices();
                List<int> lstNo = res.GetResultListByEnd(sp, PageSize, PageIndex, SortExpression);

                IEnumerable ien = GetResult(lstNo);
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 得到某一结果文件的某一页数据 并按某一字段排序
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>list(int)</int></returns>
        public List<GeneralDataInfo> GetResultListDataInfo(SearchPattern sp, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {

                ResultServices res = new ResultServices();
                List<int> lstNo = res.GetResultListByEnd(sp, PageSize, PageIndex, SortExpression);

                List<GeneralDataInfo> ien = GetResultListDataInfo(lstNo);
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 得到给定号单的结果数据
        /// </summary>
        /// <param name="_lstNo"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SortExpression"></param>
        /// <returns></returns>
        public IEnumerable GetResultList(List<int> _lstNo, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {
                List<int> lstNo = (_lstNo.Skip((PageIndex - 1) * PageSize).Take(PageSize)).ToList();

                return GetResult(lstNo);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 得到给定号单的结果数据
        /// </summary>
        /// <param name="_lstNo"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SortExpression"></param>
        /// <returns></returns>
        public List<GeneralDataInfo> GetResultListDataInfo(List<int> _lstNo, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {
                List<int> lstNo = (_lstNo.Skip((PageIndex - 1) * PageSize).Take(PageSize)).ToList();

                return GetResultListDataInfo(lstNo);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstNo"></param>
        /// <returns></returns>
        private IEnumerable GetResult(List<int> lstNo)
        {
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<CnGeneral_Info> tbCnDocInfo = db.CnGeneral_Info;

                var result = from item in tbCnDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.SerialNo))
                             orderby item.SerialNo descending
                             select new
                             {
                                 TI = item.Title,
                                 //AN = item.ApNo,
                                 AN = string.Format("{0}.{1}", item.ApNo.Trim(), CnAppLicationNo.getValidCode(item.ApNo)), //add by xiwl
                                 ANF = UrlParameterCode_DE.encrypt(item.ApNo.Trim()),  //add by xiwl
                                 AD = item.ApDate,
                                 IPC = item.Ipc1,
                                 ETI = item.titleen,
                                 //OAN = item.Old_ApNo,
                                 CPIC = item.SerialNo, //add by wsx-->up by xiwl on 20120315 id>CPIC
                             };

                //paging... (LINQ)
                //IEnumerable ien = result.Skip((PageNumber - 1) * PageSize).Take(PageSize);
                IEnumerable ien = result.DefaultIfEmpty();
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstNo"></param>
        /// <returns></returns>
        private List<GeneralDataInfo> GetResultListDataInfo(List<int> lstNo)
        {
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<CnGeneral_Info> tbCnDocInfo = db.CnGeneral_Info;

                var result = from item in tbCnDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.SerialNo))
                             orderby item.SerialNo descending
                             select new GeneralDataInfo
                             {
                                 StrTI = item.Title,
                                 //AN = item.ApNo,
                                 StrAN = string.Format("{0}.{1}", item.ApNo.Trim(), CnAppLicationNo.getValidCode(item.ApNo)), //add by xiwl
                                 StrPtCode = UrlParameterCode_DE.encrypt(item.ApNo.Trim()),  //add by xiwl
                                 StrAD = item.ApDate,
                                 StrIPC = item.Ipc1,
                                 StrTrsTI = item.titleen,
                                 //OAN = item.Old_ApNo,
                                 NCPIC = item.SerialNo, //add by wsx-->up by xiwl on 20120315 id>CPIC
                                 NID=item.SerialNo,
                             };

                //paging... (LINQ)
                //IEnumerable ien = result.Skip((PageNumber - 1) * PageSize).Take(PageSize);
                List<GeneralDataInfo> ien = result.ToList<GeneralDataInfo>();
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }


    }
}
