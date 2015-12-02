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

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Search.ResultData
{
    /// <summary>
    ///cnDbResultData 的摘要说明
    /// </summary>
    public class DwpiDbResultData
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DwpiDbResultData()
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
            ResultDataManagerDataContext db = new ResultDataManagerDataContext();
            Table<DocdbFamilyInfo> tbCnDocInfo = db.DocdbFamilyInfo;

            ResultServices res = new ResultServices();
            List<int> lstNo = res.GetResultList(sp, SortExpression);

            var result = from item in tbCnDocInfo
                         where lstNo.Contains(Convert.ToInt32(item.CPIC))
                         select new
                         {
                             TI = item.Title,
                             AN = item.AppNo,
                             AD = item.AppDate,
                             IPC = item.IPC,
                         };


            //paging... (LINQ)
            //IEnumerable ien = result.Skip((PageNumber - 1) * PageSize).Take(PageSize);
            IEnumerable ien = result.DefaultIfEmpty();

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
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<DwpiAccessionNoSerialNo> tbCnDocInfo = db.DwpiAccessionNoSerialNo;

                ResultServices res = new ResultServices();
                List<int> lstNo = res.GetResultListByEnd(sp, PageSize, PageIndex, SortExpression);

                var result = from item in tbCnDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.SerialNo))
                             orderby item.SerialNo descending
                             select new
                             {
                                 //TI = item.Title.Substring(0,100)+"......",//标题
                                 //AN = GetString(item.AppNo,";",1), //申请号
                                 //AD = GetString(item.PublicNo,";",3),//公开号
                                 //IPC = GetString(item.IPC,";",4),    //IPC
                                 //CPIC=item.AccessionNo, //入藏号
                                 TI = item.Title,//标题
                                 AN = item.AppNo, //申请号
                                 AD = item.PublicNo,//公开号
                                 IPC = item.IPC,    //IPC
                                 CPIC = item.AccessionNo, //入藏号
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
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<DwpiAccessionNoSerialNo> tbCnDocInfo = db.DwpiAccessionNoSerialNo;

                List<int> lstNo = (_lstNo.Skip((PageIndex - 1) * PageSize).Take(PageSize)).ToList();

                var result = from item in tbCnDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.SerialNo))
                             orderby item.SerialNo descending
                             select new
                             {
                                 TI = item.Title,//标题
                                 AN = item.AppNo, //申请号
                                 AD = item.PublicNo,//公开号
                                 IPC = item.IPC,    //IPC
                                 CPIC = item.AccessionNo, //入藏号
                             };

                //paging... (LINQ)
                IEnumerable ien = null;
                ien = result.DefaultIfEmpty();
                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public string GetString(string source, string findchar, int count)
        {
            int index = 0;
            try
            {
                System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(source, findchar);
                index = matches[count].Index;
            }
            catch
            {
                index = -1;
            }
            if (index > 0)
            {
                return source.Substring(0, index) + "...";
            }
            else
            {
                return source;
            }

        }
    }
}
