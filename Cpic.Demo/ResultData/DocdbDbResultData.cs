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
    public class DocdbDbResultData
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DocdbDbResultData()
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

            IEnumerable ien = GetResultFmlList(lstNo, lstNo.Count, 1, SortExpression);
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
                List<int> lstNo = res.GetResultList(sp, PageSize, PageIndex, SortExpression);

                IEnumerable ien = GetResultFmlList(lstNo, lstNo.Count, 1, SortExpression);
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
                List<int> lstNo = res.GetResultList(sp, PageSize, PageIndex, SortExpression);

                List<GeneralDataInfo> ien = GetResultFmlListDataInfo(lstNo, lstNo.Count, 1, SortExpression);
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
        public List<GeneralDataInfo> GetResultListDocInfo(SearchPattern sp, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {
                ResultServices res = new ResultServices();
                List<int> lstNo = res.GetResultList(sp, PageSize, PageIndex, SortExpression);

                List<GeneralDataInfo> ien = GetResulDoctListDataInfo(lstNo, lstNo.Count, 1, SortExpression);
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 得到给定家族号单的结果数据 并按某一字段排序
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>list(int)</int></returns>
        public IEnumerable GetResultFmlList(List<int> _lstNo, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<DocdbFamilyInfo> tbCnDocInfo = db.DocdbFamilyInfo;

                ResultServices res = new ResultServices();
                List<int> lstNo = (_lstNo.Skip((PageIndex - 1) * PageSize).Take(PageSize)).ToList();

                var result = from item in tbCnDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.CPIC))
                             select new
                             {
                                 CPIC = item.CPIC,
                                 TI = item.Title,
                                 AN = item.AppNo,
                                 AD = item.AppDate,
                                 IPC = item.IPC,
                                 PIDNum = item.PubIDs.Length - (item.PubIDs.Replace(",", "").Length) + 1,//PIDNum = item.PubIDNum,
                                 //PubIds=item.PubIDs,
                                 //PubID = item.PubIDs.Contains(",") ? item.PubIDs.Substring(0, item.PubIDs.IndexOf(",")) : item.PubIDs,
                             };

                //paging... (LINQ)
                //IEnumerable ien = result.Skip((PageIndex - 1) * PageSize).Take(PageSize);
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
        /// 得到给定号单(成员)的结果数据 并按某一字段排序
        /// </summary>
        /// <param name="lstNo"></param>
        /// <returns></returns>
        public List<GeneralDataInfo> GetResulDoctListDataInfo(List<int> _lstNo, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<DocdbDocInfo> tbDocdbDocInfo = db.DocdbDocInfo;

                ResultServices res = new ResultServices();
                List<int> lstNo = (_lstNo.Skip((PageIndex - 1) * PageSize).Take(PageSize)).ToList();

                var result = from item in tbDocdbDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.ID))
                             orderby item.ID descending
                             select new GeneralDataInfo
                             {
                                 StrTI = item.Title,                                                                  
                                 StrPubID=item.PubID.Trim(),
                                 StrPtCode = UrlParameterCode_DE.encrypt(item.PubID.Trim()),  //add by xiwl
                                 StrAD = item.AppDate,
                                 StrIPC = item.IPC,  
                                 NCPIC =Convert.ToInt64(item.CPIC), //add by wsx-->up by xiwl on 20120315 id>CPIC  
                                 NID=item.ID,                                   
                                 StrAN = item.AppNo,                                                                  
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

        /// <summary>
        /// 得到给定家族号单的结果数据 并按某一字段排序
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>list(int)</int></returns>
        public List<GeneralDataInfo> GetResultFmlListDataInfo(List<int> _lstNo, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<DocdbFamilyInfo> tbCnDocInfo = db.DocdbFamilyInfo;

                ResultServices res = new ResultServices();
                List<int> lstNo = (_lstNo.Skip((PageIndex - 1) * PageSize).Take(PageSize)).ToList();

                var result = from item in tbCnDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.CPIC))
                             select new GeneralDataInfo
                             {
                                 NCPIC = item.CPIC,
                                 StrTI = item.Title,
                                 StrAN = item.AppNo,
                                 StrAD = item.AppDate,
                                 StrIPC = item.IPC,
                                 NMembers = item.PubIDs.Length - (item.PubIDs.Replace(",", "").Length) + 1,//PIDNum = item.PubIDNum,
                                 //PubIds=item.PubIDs,
                                 //PubID = item.PubIDs.Contains(",") ? item.PubIDs.Substring(0, item.PubIDs.IndexOf(",")) : item.PubIDs,
                             };

                //paging... (LINQ)
                //IEnumerable ien = result.Skip((PageIndex - 1) * PageSize).Take(PageSize);
                List<GeneralDataInfo> ien = result.ToList<GeneralDataInfo>();
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
        public IEnumerable GetResultDetailList(int PageSize, int PageIndex, List<string> LsPubs)
        {
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();

                Table<DocdbDocInfo> tbDocDocInfo = db.DocdbDocInfo;

                //ResultServices res = new ResultServices();
                //List<int> lstNo = res.GetResultList(sp, PageSize, PageIndex, SortExpression);

                string strPubIds = String.Join(",", LsPubs.ToArray());
                var result = from item in tbDocDocInfo
                             where LsPubs.Contains(item.PubID)
                             orderby strPubIds.IndexOf(item.PubID)
                             select new
                             {
                                 AN = item.AppNo,
                                 AD = item.AppDate,
                                 IPC = item.IPC.Substring(0, 50),
                                 PubID = item.PubID,
                                 PubIDF = UrlParameterCode_DE.encrypt(item.PubID),
                                 TI = item.Title,
                             };

                //paging... (LINQ)
                IEnumerable ien = result.Skip((PageIndex - 1) * PageSize).Take(PageSize);
                //IEnumerable ien = result.DefaultIfEmpty();
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取指定公开的的数据
        /// </summary>
        /// <param name="PageSize">每页数据</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="LsPubs">公开号</param>
        /// <returns></returns>
        public List<GeneralDataInfo> GetRstDetailListDataInfo(int PageSize, int PageIndex, List<string> LsPubs)
        {
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();

                Table<DocdbDocInfo> tbDocDocInfo = db.DocdbDocInfo;

                //ResultServices res = new ResultServices();
                //List<int> lstNo = res.GetResultList(sp, PageSize, PageIndex, SortExpression);

                string strPubIds = String.Join(",", LsPubs.ToArray());
                var result = from item in tbDocDocInfo
                             where LsPubs.Contains(item.PubID)
                             orderby strPubIds.IndexOf(item.PubID)
                             select new GeneralDataInfo
                             {
                                 StrAN = item.AppNo,
                                 StrAD = item.AppDate,
                                 StrIPC = item.IPC.Substring(0, 50),
                                 StrPubID = item.PubID,
                                 StrPtCode = UrlParameterCode_DE.encrypt(item.PubID),
                                 StrTI = item.Title,
                             };

                //paging... (LINQ)
                List<GeneralDataInfo> ien = result.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<GeneralDataInfo>();
                //IEnumerable ien = result.DefaultIfEmpty();
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }


        /// <summary>
        /// 获取公开号
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="_strsFamilyID"></param>
        /// <returns></returns>
        private List<string> getPubId(int PageSize, int PageIndex, string _strsFamilyID)
        {
            return getPubId(PageSize, PageIndex, _strsFamilyID, new List<string>(), "");

            #region 关闭代码
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();

                Table<DocdbDocInfo> tbDocDocInfo = db.DocdbDocInfo;

                var result = from item in tbDocDocInfo
                             where item.CPIC.Equals(_strsFamilyID)
                             orderby item.PubID
                             select new
                             {
                                 //AN = item.AppNo,
                                 //AD = item.AppDate,
                                 //IPC = item.IPC.Substring(0, 50),
                                 PubID = item.PubID,
                                 //Ti = item.Title,
                             };

                //paging... (LINQ)
                var ien = result.Skip((PageIndex - 1) * PageSize).Take(PageSize);

                List<string> LsPubs = new List<string>();
                foreach (var item in ien)
                {
                    LsPubs.Add(item.PubID);
                }

                return LsPubs;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
            #endregion
        }

        /// <summary>
        /// 获取公开号
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="_strsFamilyID"></param>
        /// <returns></returns>
        private List<string> getPubId(int PageSize, int PageIndex, string _strsFamilyID, List<string> _LststrTopFiledId, string _strTopFiledType)
        {
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();

                Table<DocdbDocInfo> tbDocDocInfo = db.DocdbDocInfo;

                var result = from item in tbDocDocInfo
                             where item.CPIC.Equals(_strsFamilyID)
                             select new
                             {
                                 PubID = item.PubID,
                                 TopId = OrderbyTopPubId(item, _LststrTopFiledId, _strTopFiledType)
                             };

                var resultOrdby = from item in result.ToList()
                                  orderby item.TopId, item.PubID
                                  select item;

                //paging... (LINQ)
                var skpResult = resultOrdby.Skip((PageIndex - 1) * PageSize).Take(PageSize);
                //IEnumerable ien = result.DefaultIfEmpty();

                List<string> LsPubs = new List<string>();
                foreach (var item in skpResult)
                {
                    LsPubs.Add(item.PubID);
                }

                //如果取不到申请号，通过tbDocdbFamilyInfo表中的pubIDS取-----------------
                if (LsPubs.Count == 0)
                {
                    Table<DocdbFamilyInfo> tbDocdbFamilyInfo = db.DocdbFamilyInfo;

                    var result2 = from item in tbDocdbFamilyInfo
                                  where _strsFamilyID.Equals(item.CPIC)
                                  select new
                                  {
                                      PubIds = item.PubIDs,
                                  };
                    var result22 = from item in result2.First().PubIds.Split(',').ToList()
                                   select new
                                   {
                                       PubID = item,
                                       TopId = 1
                                   };

                    //paging... (LINQ)
                    var skpResult2 = result22.Skip((PageIndex - 1) * PageSize).Take(PageSize);
                    //IEnumerable ien = result.DefaultIfEmpty();
                    foreach (var item in skpResult2)
                    {
                        LsPubs.Add(item.PubID);
                    }
                }


                return LsPubs;
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
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="_strsFamilyID"></param>
        /// <returns></returns>
        public IEnumerable GetResultDetailList(int PageSize, int PageIndex, string _strsFamilyID)
        {
            try
            {

                List<string> LsPubs = getPubId(PageSize, PageIndex, _strsFamilyID);

                IEnumerable ien = GetResultDetailList(PageSize, 1, LsPubs);
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
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="_strsFamilyID"></param>
        /// <returns></returns>
        public List<GeneralDataInfo> GetRstDetailListDataInfo(int PageSize, int PageIndex, string _strsFamilyID)
        {
            try
            {
                List<string> LsPubs = getPubId(PageSize, PageIndex, _strsFamilyID);
                return GetRstDetailListDataInfo(PageSize, 1, LsPubs);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 得到指定家族下的成员专利信息
        /// </summary>
        /// <param name="PageSize">每页数量</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="_strsFamilyID">家族号</param>
        /// <param name="_strTopFiledId">TOP显示值</param>
        /// <param name="_strTopFiledType">TOP值类型[0:公开号,1:申请号,01:申请号||公开号]</param>
        /// <returns></returns>
        public List<GeneralDataInfo> GetRstDetailListDataInfo(int PageSize, int PageIndex, string _strsFamilyID, List<string> _LststrTopFiledId, string _strTopFiledType)
        {
            try
            {
                List<string> LsPubs = getPubId(PageSize, PageIndex, _strsFamilyID, _LststrTopFiledId, _strTopFiledType);
                return GetRstDetailListDataInfo(PageSize, 1, LsPubs);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 得到指定家族下的成员专利信息
        /// </summary>
        /// <param name="PageSize">每页数量</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="_strsFamilyID">家族号</param>
        /// <param name="_strTopFiledId">TOP显示值</param>
        /// <param name="_strTopFiledType">TOP值类型[0:公开号,1:申请号,01:申请号||公开号]</param>
        /// <returns></returns>
        public IEnumerable GetResultDetailList(int PageSize, int PageIndex, string _strsFamilyID, List<string> _LststrTopFiledId, string _strTopFiledType)
        {
            try
            {
                List<string> LsPubs = getPubId(PageSize, PageIndex, _strsFamilyID, _LststrTopFiledId, _strTopFiledType);

                IEnumerable ien = GetResultDetailList(PageSize, 1, LsPubs);
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
        /// <param name="Item"></param>
        /// <param name="_strTopFiledId"></param>
        /// <param name="_TopFiledType"></param>
        /// <returns></returns>
        private int OrderbyTopPubId(DocdbDocInfo _DocItem, List<string> _LststrTopFiledId, string _TopFiledType)
        {
            int nRId = 1;
            try
            {
                //0:公开号,1:申请号,3:申请号||公开号
                switch (_TopFiledType)
                {
                    case "0":  //公开号
                        foreach (string strItem in _LststrTopFiledId)
                        {
                            nRId = _DocItem.PubID.ToUpper().StartsWith(strItem) ? 0 : 1;
                            if (nRId == 0) break;
                        }
                        break;
                    case "1":  //申请号
                        foreach (string strItem in _LststrTopFiledId)
                        {
                            nRId = _DocItem.AppNo.ToUpper().StartsWith(strItem) ? 0 : 1;
                            if (nRId == 0) break;
                        }
                        break;
                    case "01":
                        foreach (string strItem in _LststrTopFiledId)
                        {
                            nRId = (_DocItem.AppNo.ToUpper().StartsWith(strItem) || _DocItem.PubID.ToUpper().StartsWith(strItem)) ? 0 : 1;
                            if (nRId == 0) break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return nRId;
        }
    }
}
