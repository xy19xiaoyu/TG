using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cpic.Cprs2010.Search;
using Cpic.Cprs2010.Search.SearchManager;
using Cpic.Cprs2010.Search.ResultData;
using ParseXml;
using log4net;
using System.Reflection;
using System.Data.Linq;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.Services;
using SearchInterface.WSFLZT;
using System.Data.OleDb;
using Cpic.Cprs2010.Cfg;
using Cpic.Cprs2010.Cfg.Data;

namespace SearchInterface
{
    /// <summary>
    /// xxt 2013-06
    /// </summary>
    public class ClsSearch
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static List<string> lstCnLx = new List<string>();
        static List<string> lstEnCC = new List<string>();

        static ClsSearch()
        {
            try
            {
                lstCnLx.Clear();
                lstCnLx.Add("DI");
                lstCnLx.Add("AI");
                lstCnLx.Add("UM");
                lstCnLx.Add("DP");


                //
                lstEnCC.Clear();
                lstEnCC.Add("CN");
                lstEnCC.Add("US");
                lstEnCC.Add("EP");
                lstEnCC.Add("JP");
                lstEnCC.Add("DE");
                lstEnCC.Add("GB");
                lstEnCC.Add("RU");
                lstEnCC.Add("FR");
                lstEnCC.Add("KR");
                lstEnCC.Add("CH");
                lstEnCC.Add("WO");
                lstEnCC.Add("OT");
            }
            catch (Exception ex)
            {
            }
        }

        #region 对外检索接口

        // 验证和修改申请号
        private string getApplyNo(string apNo)
        {
            string _apNo = apNo;
            int year1 = int.Parse(apNo.Substring(0, 2));
            if (year1 > 50)
            {
                _apNo = "19" + _apNo;
                if (_apNo.Length > 5)
                    _apNo = _apNo.Substring(0, 5) + "00" + _apNo.Substring(5);
            }
            string year2 = apNo.Substring(0, 1);
            if (year2 == "0")
            {
                _apNo = "20" + _apNo;
                if (_apNo.Length > 5)
                    _apNo = _apNo.Substring(0, 5) + "00" + _apNo.Substring(5);
            }
            return _apNo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPattern">检索式，页面生成的检索式</param>
        /// <param name="UserID">用户ID，7位，数据库中用户表存放的ID</param>
        /// <param name="strSearchDbType">中国专利检索为CN，世界EN</param>
        /// <returns></returns>
        public Cpic.Cprs2010.Search.ResultInfo DoSearch(string strGroupName, string strPattern, int UserID, string nSNO, Cpic.Cprs2010.Search.SearchDbType _SDbType)
        {
            if (_SDbType == Cpic.Cprs2010.Search.SearchDbType.Cn)
            {
                Regex reg = new Regex(@"(\d{12}\.?[\d|x|X])\s*\/AN|AN\s+(\d{12}\.?[\d|x|X])", RegexOptions.IgnoreCase);
                MatchCollection mc = reg.Matches(strPattern);
                foreach (Match m in mc)
                {
                    string eachAn = m.Groups[1].Value;
                    if (eachAn == "" || eachAn == null)
                        eachAn = m.Groups[2].Value;
                    // 送验证
                    bool ifValidate = CnAppLicationNo.Check_ApNoAddVCode(eachAn);
                    if (!ifValidate)
                    {
                        return new Cpic.Cprs2010.Search.ResultInfo() { HitMsg = "申请号校验位错误" };
                    }
                    string newAn = eachAn.Length == 13 ? eachAn.Substring(0, 12) : eachAn.Substring(0, eachAn.IndexOf('.'));
                    strPattern = strPattern.Replace(eachAn, newAn);
                }


                //处理CS:DS
                Regex regCs = new Regex(@"(\w*)\s*\/([C|D]S)|([C|D]S)\s+(\w*)", RegexOptions.IgnoreCase);
                MatchCollection mcCs = regCs.Matches(strPattern);
                foreach (Match m in mcCs)
                {
                    string strWord = m.Groups[1].Value;
                    string strKey = m.Groups[2].Value;
                    if (strWord == "" || strWord == null)
                    {
                        strWord = m.Groups[3].Value;
                        strKey = m.Groups[4].Value;
                    }
                    // 送验证
                    string strRs = getNewSearchWord(strWord, strKey);
                    if (string.IsNullOrEmpty(strRs))
                    {
                        //return new Cpic.Cprs2010.Search.ResultInfo() { HitMsg = "申请号校验位错误" };
                    }
                    else
                    {
                        strPattern = strPattern.Replace(m.Groups[0].Value, strRs);
                    }
                }
            }
            Cpic.Cprs2010.Search.SearchPattern schPatItem = new Cpic.Cprs2010.Search.SearchPattern();

            schPatItem.SearchNo = nSNO.PadLeft(3, '0');  //检索编号[001-999]
            schPatItem.Pattern = FormatQueryPattenrn(strPattern, _SDbType);      //检索式：F XX 2010/AD
            schPatItem.UserId = UserID;   //用户ID
            schPatItem.DbType = _SDbType;
            schPatItem.GroupName = strGroupName;

            Cpic.Cprs2010.Search.ResultInfo res = Cpic.Cprs2010.Search.SearchManager.SearchFactory.CreatDoSearch(schPatItem);
            res.SearchPattern.Pattern = strPattern;
            return res;
        }

        private string getNewSearchWord(string strWord, string _strKey)
        {
            string strRs = "";

            string strKeys = WordLib.WordOperator.GetWord(strWord).Trim(';');
            //foreach (string strItme in strKeys.Split(';'))
            //{
            //    strRs=
            //}
            if (!string.IsNullOrEmpty(strKeys))
            {
                strRs = strKeys.Replace(";", "/" + _strKey.Trim() + "+") + "/" + _strKey.Trim();
            }
            return strRs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPattern">检索式，页面生成的检索式</param>
        /// <param name="UserID">用户ID，7位，数据库中用户表存放的ID</param>
        /// <param name="strSearchDbType">中国专利检索为CN，世界EN</param>
        /// <returns></returns>
        public Cpic.Cprs2010.Search.ResultInfo DoSearch(string strGroupName, string strPattern, int UserID, Cpic.Cprs2010.Search.SearchDbType _SDbType)
        {
            string strSearchNo = GetSearchNo(UserID, _SDbType);   //检索编号[001-999]

            if (strPattern.ToLower().EndsWith(".txt") || strPattern.ToLower().EndsWith(".ini"))
            {
                Cpic.Cprs2010.Search.SearchManager.SearchFactory.del_OldCnpFile(UserID, strSearchNo, _SDbType);
                ImporResult im = new ImporResult();
                //@"D:\XZQ\src\源代码\Patentquery\ZtHeadImg\201405\c4b881d8fff1413fa8316f5300deb808.txt"
                return im.ImportLis2tResult(System.Web.HttpContext.Current.Server.MapPath("~/ZtHeadImg/" + strPattern), UserID, strSearchNo, _SDbType);
                //strPattern,strSearchNo,_SDbType
            }

            return DoSearch(strGroupName, strPattern, UserID, strSearchNo, _SDbType);
        }



        private string FormatQueryPattenrn(string strPattern, Cpic.Cprs2010.Search.SearchDbType _SDbType)
        {
            string strRs = strPattern;
            try
            {
                Regex reg = new Regex("(.*?)(@..=.*|@YX.*|@SX.*)");

                Match rs = reg.Match(strPattern);
                string strQ1 = "";
                string strQ2 = "";
                string strEndFlag = "";
                if (rs.Success)
                {
                    strQ1 = rs.Groups[1].Value.Trim();
                    strQ2 = rs.Groups[2].Value.Trim().ToUpper();

                    List<string> lst = new List<string>();

                    switch (_SDbType)
                    {
                        case Cpic.Cprs2010.Search.SearchDbType.Cn:
                            lst = lstCnLx;
                            break;
                        case Cpic.Cprs2010.Search.SearchDbType.DocDB:
                            lst = lstEnCC;
                            break;
                    }

                    foreach (string strItem in lst)
                    {
                        strEndFlag += strQ2.Contains(strItem) ? "1" : "0";
                    }

                    //第一位：发明标识，0全，1，公开，2，SQ
                    //第二位：新型标识，0，无，1启用
                    //第三位：外观标识，0，无，1启用
                    if (_SDbType == Cpic.Cprs2010.Search.SearchDbType.Cn)
                    {
                        switch (strEndFlag.Substring(0, 2))
                        {
                            case "00":
                                strEndFlag = "0" + strEndFlag.Substring(2);
                                break;
                            case "01":
                                strEndFlag = "2" + strEndFlag.Substring(2);
                                break;
                            case "10":
                                strEndFlag = "1" + strEndFlag.Substring(2);
                                break;
                            case "11":
                                strEndFlag = "1" + strEndFlag.Substring(2);
                                break;
                            default:
                                break;
                        }

                        if (strQ2.Contains("@YX") || strQ2.Contains("@SX"))  //EG法律状态标识，0，全，1,YX.eee, 2,SX.eee
                        {
                            if (strEndFlag.Equals("000"))
                            {
                                strEndFlag = "111";
                            }

                            strEndFlag += strQ2.Contains("@YX") ? "1" : "2";
                        }
                    }

                    strEndFlag = _SDbType == Cpic.Cprs2010.Search.SearchDbType.DocDB ? Convert.ToInt32(strEndFlag, 2).ToString().PadLeft(4, '0') : strEndFlag;
                    strRs = strQ1 + "###" + strEndFlag + "###";
                }
            }
            catch (Exception ex)
            {
            }

            return strRs.Replace("、", " ");
        }

        /// <summary>
        /// 检索取得命中篇数
        /// </summary>
        /// <param name="strPattern">检索式，页面生成的检索式</param>
        /// <param name="UserID">用户ID，7位，数据库中用户表存放的ID</param>
        /// <param name="strSearchDbType">中国专利检索为CN，世界EN</param>
        /// <returns>
        /// 正确结果:(001)F XX (电脑/TI)  <hits: 26072>
        /// 错误或异常结果:ERROR
        /// </returns>
        public string GetSearchDataHits(string strGroupName, string strPattern, int UserID, string strSearchDbType)
        {
            strSearchDbType = strSearchDbType.ToUpper();
            Cpic.Cprs2010.Search.SearchDbType sdbty = Cpic.Cprs2010.Search.SearchDbType.Cn;
            switch (strSearchDbType)
            {
                case "CN":
                    sdbty = Cpic.Cprs2010.Search.SearchDbType.Cn;
                    break;
                case "EN":
                case "WD":
                    sdbty = Cpic.Cprs2010.Search.SearchDbType.DocDB;
                    break;
                default:
                    return "ERROR";
            }

            Cpic.Cprs2010.Search.ResultInfo res = DoSearch(strGroupName, strPattern, UserID, sdbty);

            string hits = res.HitMsg;

            if (hits.IndexOf("<hit") == -1) // 返回结果正确
            {
                hits = "ERROR";
            }

            return hits;
        }

        /// <summary>
        /// 得到某一结果文件的某一页数据 并按某一字段排序
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>list(int)</int></returns>
        public List<GeneralDataInfo> GetResultListDocInfo(Cpic.Cprs2010.Search.SearchPattern sp, int PageSize, int PageIndex, string SortExpression)
        {
            try
            {

                List<GeneralDataInfo> ien = new List<GeneralDataInfo>();
                List<xmlDataInfo> lstXml = GetResultList(sp, PageSize, PageIndex, SortExpression);

                GeneralDataInfo genTmp = null;
                foreach (xmlDataInfo xmlItem in lstXml)
                {
                    genTmp = new GeneralDataInfo();
                    genTmp.NCPIC = long.Parse(xmlItem.CPIC);
                    genTmp.StrTI = xmlItem.StrTitle;
                    genTmp.StrPubID = xmlItem.StrPubNo;
                    genTmp.StrPtCode = xmlItem.StrANX;
                    genTmp.StrAD = xmlItem.StrApDate;
                    genTmp.StrIPC = xmlItem.StrIpc;
                    genTmp.NID = long.Parse(xmlItem.StrSerialNo);
                    genTmp.StrAN = xmlItem.StrApNo;

                    ien.Add(genTmp);
                }

                return ien;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 读取检索结果的概要信息，按页读取
        /// </summary>
        /// <param name="SearchNo">检索编号（3位数字）</param>
        /// <param name="UserID">用户ID，7位，数据库中用户表存放的ID</param>
        /// <param name="strSearchDbType">中国专利检索为CN，世界EN</param>
        /// <param name="PageSize">每页显示的记录数</param>
        /// <param name="PageIndex">读取数据页码</param>
        /// <param name="SortExpression">排序字段，不排序可输入空字符串</param>
        /// <returns></returns>     
        public List<xmlDataInfo> getSearchData(string strGroupName, string SearchNo, int UserID, string strSearchDbType, int PageSize, int PageIndex, string SortExpression)
        {
            List<xmlDataInfo> lstXml = new List<xmlDataInfo>();
            xmlDataInfo item = new xmlDataInfo();

            Cpic.Cprs2010.Search.SearchPattern schPatItem = new Cpic.Cprs2010.Search.SearchPattern();
            schPatItem.SearchNo = SearchNo; ;  //检索编号[001-999]
            //检索式：F XX 2010/AD
            schPatItem.UserId = UserID;   //用户ID
            schPatItem.GroupName = strGroupName;
            strSearchDbType = strSearchDbType.ToUpper();

            switch (strSearchDbType.ToUpper())
            {
                case "CN":
                    schPatItem.DbType = Cpic.Cprs2010.Search.SearchDbType.Cn;
                    break;
                case "EN":
                case "WD":
                    schPatItem.DbType = Cpic.Cprs2010.Search.SearchDbType.DocDB; //检索数据
                    break;
                default:
                    return null;
            }

            lstXml = GetResultList(schPatItem, PageSize, PageIndex, SortExpression);


            return lstXml;
        }

        /// <summary>
        /// 根据申请号，查询审查员引证信息
        /// </summary>
        /// <param name="appNr">授权公告号</param>
        public string getYZInf(string appNr)
        {
            YZInf.Service1Client yzInf = new YZInf.Service1Client();
            string YZInf = yzInf.GetYZInfWithAppNr(appNr);

            return YZInf;
        }

        /// <summary>
        /// 根据专利号，查询详细信息
        /// </summary>
        /// <param name="PatentID">专利号。中国专利为申请号、世界专利为公开号</param>
        /// <param name="SearchDbType">中国的CN，世界EN</param>
        /// <param name="LeiXing">
        /// 0：权利要求xml文件内容
        /// 1: 说明书,xml文件内容
        /// 2：PDF全文,,多个用|切分    
        /// 3：法律状态
        /// 4: 外观图形路径,多个用|切分
        /// 5: 著录项目信息
        /// </param>
        /// <returns>文件路径</returns>
        public string getInfoByPatentID(string PatentID, string SearchDbType, string LeiXing)
        {
            string FilePath = "";

            WSYQ.CprsGIISWebSvcSoapClient wsyq = new WSYQ.CprsGIISWebSvcSoapClient();
            BNS.ServiceSoapClient bns = new BNS.ServiceSoapClient();

            List<string> pdf = new List<string>();

            string strJMPatentID = UrlParameterCode_DE.DecryptionAll(PatentID);
            switch (LeiXing)
            {
                case "0":
                    FilePath = wsyq.GetPatentData(PatentID, WSYQ.PatentDataType.CnClmXmlTxt);
                    break;
                case "1":
                    FilePath = wsyq.GetPatentData(PatentID, WSYQ.PatentDataType.CnDesXmlTxt);
                    break;
                case "2":
                    if (SearchDbType == "CN")
                    {
                        pdf = bns.BnsByAppNo(strJMPatentID);
                    }
                    if (SearchDbType == "EN")
                    {
                        pdf = bns.BnsByPubNo(strJMPatentID);
                    }

                    foreach (string s in pdf)
                    {
                        FilePath = FilePath + s + "|";
                    }

                    FilePath = FilePath.TrimEnd('|');

                    break;
                case "4":
                    //FilePath = wsyq.GetPatentData(UrlParameterCode_DE.encrypt(cnDataService.ApNo8To12(strJMPatentID)), WSYQ.PatentDataType.CnWGImgUrls);
                    FilePath = bns.getCnWgImgUrlsByAN(strJMPatentID);
                    break;
                case "5":
                    FilePath = getCnMainXmlContent(strJMPatentID);  //wsyq.GetPatentData(PatentID, WSYQ.PatentDataType.CnMabXmlTxt);
                    break;
            }
            return FilePath;
        }

        /// <summary>
        /// 根据申请号取得法律状态的详细信息
        /// </summary>
        /// <param name="AppNo"></param>
        /// <returns></returns>
        public SearchInterface.WSFLZT.CnLegalStatus[] getFalvZhuangTai(string AppNo)
        {
            SearchInterface.WSFLZT.CnLegalStatus[] lst = null;
            try
            {
                AppNo = UrlParameterCode_DE.DecryptionAll(AppNo);
                AppNo += CnAppLicationNo.getValidCode(AppNo);
                WSFLZT.ServiceSoapClient wf = new WSFLZT.ServiceSoapClient();

                lst = wf.GetCnLegalDetail(AppNo);

                foreach (SearchInterface.WSFLZT.CnLegalStatus item in lst)
                {
                    item.LegalDate = XmPatentComm.FormatDateVlue(item.LegalDate);
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }


        #endregion

        /// <summary>
        /// 检索编号生成程序
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private string GetSearchNo(int UserID, Cpic.Cprs2010.Search.SearchDbType _SDbType)
        {
            string SearchNo = "";
            DataTable dt = new DataTable();
            //string sql = "select top 1 SearchNo from TbSearchNo  Where UserId=" + UserID + " Order By ID DESC";

            string sql = string.Format("select top 1 Number as SearchNo from TLC_Patterns  Where UserId={0} and [Types]={1} order by CreateDate desc",
                UserID, Convert.ToByte(_SDbType.GetHashCode()));
            try
            {
                dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                return "001";
            }

            if (dt.Rows.Count <= 0)
            {
                SearchNo = "001";
            }
            else
            {
                SearchNo = dt.Rows[0]["SearchNo"].ToString().Trim();
                SearchNo = ((Convert.ToInt32(SearchNo) % 999) + 1).ToString().Trim();
            }

            //编号存入数据库
            //SearNoInDB(SearchNo, UserID);

            return SearchNo.ToString().PadLeft(3, '0');
        }

        /// <summary>
        /// 超过最大历史保存数时，从最早开始覆盖
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private string getMinNum(int UserID)
        {
            string SearchNo = "";

            DataSet ds = new DataSet();
            string sql = "Select top 1 * From ( select row_number() over(order by SearchNo) AS rowID,SearchNo from TbSearchNo Where UserId=" + UserID + " And id in (select top 9 ID from TbSearchNo Order by id desc))  a Where a.rowID<>a.SearchNo";
            try
            {
                ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                return "000";
            }

            if (ds.Tables[0].Rows.Count <= 0)
            {
                sql = "select top 1 SearchNo from TbSearchNo  Where UserId=" + UserID + " Order By ID ASC";
                try
                {
                    ds = DBA.DbAccess.GetDataSet(CommandType.Text, sql);
                }
                catch (Exception ex)
                {
                    return "000";
                }
                SearchNo = ds.Tables[0].Rows[0]["SearchNo"].ToString();
            }
            else
            {
                SearchNo = ds.Tables[0].Rows[0]["rowID"].ToString().PadLeft(3, '0');
            }

            SearchNo = (Convert.ToInt32(SearchNo) % 10).ToString().PadLeft(3, '0');
            return SearchNo;
        }
        /// <summary>
        /// 检索编号存入数据库
        /// </summary>
        /// <param name="SearchNo"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private bool SearNoInDB(string SearchNo, int UserID)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@SearchNo", SearchNo), new OleDbParameter("@UserID", UserID) };
            string sql = "Insert Into TbSearchNo(SearchNo,UserID) Values(?,?)";
            if (DBA.DbAccess.ExecNoQuery(CommandType.Text, sql, param) < 0)
            {
                return false;
            }

            return true;
        }

        #region 检索数据
        /// <summary>
        /// 得到某一结果文件的某一页数据 并按某一字段排序
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>list(int)</int></returns>
        public List<xmlDataInfo> GetResultList(Cpic.Cprs2010.Search.SearchPattern sp, int PageSize, int PageIndex, string SortExpression)
        {
            List<xmlDataInfo> ien = new List<xmlDataInfo>();
            try
            {
                ResultServices res = new ResultServices();

                List<int> lstNo = res.GetResultListByEnd(sp, PageSize, PageIndex, SortExpression);
                if (sp.DbType == Cpic.Cprs2010.Search.SearchDbType.Cn)
                {
                    ien = GetResult(lstNo, "CN");
                }
                else if (sp.DbType == Cpic.Cprs2010.Search.SearchDbType.DocDB)
                {
                    ien = GetResult(lstNo, "DocDB");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                //return null;
            }
            return ien;
        }

        /// <summary>
        /// 专利年龄
        /// </summary>
        /// <param name="lstNo"></param>
        /// <returns></returns>
        public List<xmlDataInfo> GetRestultAge(List<int> lstNo)
        {
            return GetCNRestulAge(lstNo);
        }
        /// <summary>
        /// 世界专利检索 xxt 20130607添加
        /// </summary>
        /// <param name="lstNo"></param>
        /// <param name="DBType"></param>
        /// <returns></returns>
        public List<xmlDataInfo> GetResult(List<int> lstNo, string DBType)
        {
            if (string.IsNullOrEmpty(DBType) || DBType.ToUpper().Equals("CN"))
            {
                return GetCNResult(lstNo);
            }

            List<xmlDataInfo> lstxml = new List<xmlDataInfo>();
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<DocdbDocInfo> tbDocDocInfo = db.DocdbDocInfo;

                var result = from item in tbDocDocInfo
                             where lstNo.Contains(item.ID)
                             orderby item.ID descending
                             select new xmlDataInfo
                             {
                                 CPIC = item.CPIC.ToString(),
                                 StrTitle = item.Title,
                                 StrApNo = item.AppNo,
                                 StrApDate = item.AppDate,
                                 StrANX = UrlParameterCode_DE.encrypt(item.PubID.Trim()),
                                 StrIpc = item.IPC.Substring(0, 50),
                                 StrPubNo = item.PubID,
                                 StrSerialNo = item.ID.ToString(),
                             };

                lstxml = result.ToList();
                //循环读取对应的XML补充其它字段数据信息
                foreach (var item in lstxml)
                {
                    UpdateByEnXmlFile(item, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return lstxml;
        }

        /// <summary>
        /// 根据公开号,取得著录项目信息 xxt 20130607 添加
        /// </summary>
        /// <param name="PubNo"></param>
        /// <returns></returns>
        public List<xmlDataInfo> GetResultByPubNo(string PubNoX)
        {
            List<xmlDataInfo> lstxml = new List<xmlDataInfo>();

            try
            {
                xmlDataInfo RsItem = GetEnxmlDataInfo(PubNoX);
                if (RsItem != null)
                {
                    lstxml.Add(RsItem);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return lstxml;
        }

        public xmlDataInfo GetEnxmlDataInfo(string strPubX)
        {
            string PubNo = UrlParameterCode_DE.DecryptionAll(strPubX);
            xmlDataInfo resXmlInfo = null;
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<DocdbDocInfo> tbDocDocInfo = db.DocdbDocInfo;

                var result = from item in tbDocDocInfo
                             where item.PubID == PubNo
                             orderby item.ID descending
                             select new xmlDataInfo
                             {
                                 StrTitle = item.Title,
                                 StrApNo = item.AppNo,
                                 StrApDate = item.AppDate,
                                 StrANX = strPubX,
                                 StrIpc = item.IPC.Substring(0, 50),
                                 StrPubNo = item.PubID,
                                 CPIC = item.CPIC.ToString(),
                                 StrSerialNo = item.ID.ToString(),
                             };

                resXmlInfo = result.First();
                UpdateByEnXmlFile(resXmlInfo, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return resXmlInfo;
        }

        // add by zhangqiuyi 获取世界专利指定类型引证文献信息
        public string GetEnCitedWithSrepPhase(string strPubX, string srepPhase, string type)
        {
            string strRefDoc = "";
            string PubNo = UrlParameterCode_DE.DecryptionAll(strPubX);
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<DocdbDocInfo> tbDocDocInfo = db.DocdbDocInfo;

                var result = from item in tbDocDocInfo
                             where item.PubID == PubNo
                             orderby item.ID descending
                             select new xmlDataInfo
                             {
                                 StrTitle = item.Title,
                                 StrApNo = item.AppNo,
                                 StrApDate = item.AppDate,
                                 StrANX = strPubX,
                                 StrIpc = item.IPC.Substring(0, 50),
                                 StrPubNo = item.PubID,
                                 CPIC = item.CPIC.ToString(),
                                 StrSerialNo = item.ID.ToString(),
                             };

                xmlDataInfo resXmlInfo = result.First();

                XmlParser xmlParser = new XmlParser("", "UTF-8");
                DocDBIndexExtract docDBIndexExtract = new DocDBIndexExtract(xmlParser);
                String xmlContent = getEnMainXmlContent(resXmlInfo.StrPubNo);
                if (!xmlContent.Contains("exch:"))
                {
                    docDBIndexExtract.exch = "";
                }

                using (StringReader xmlString = new StringReader(xmlContent))
                {
                    using (XmlReader reader = XmlReader.Create(xmlString, xmlParser.Settings, xmlParser.Context))
                    {
                        XDocument xRoot = XDocument.Load(reader, LoadOptions.None);

                        if (type.Equals("not"))
                            strRefDoc = docDBIndexExtract.getCitedNotContain(xRoot, srepPhase);
                        else
                            strRefDoc = docDBIndexExtract.getCited(xRoot, srepPhase);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            if (strRefDoc.Equals(""))
            {
                return "暂无数据";
            }
            return strRefDoc;
        }

        /// <summary>
        /// 中国专利检索
        /// </summary>
        /// <param name="lstNo"></param>
        /// <returns></returns>
        private List<xmlDataInfo> GetCNResult(List<int> lstNo)
        {
            List<xmlDataInfo> lstxml = new List<xmlDataInfo>();
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<CnGeneral_Info> tbCnDocInfo = db.CnGeneral_Info;

                var result = from item in tbCnDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.SerialNo))
                             orderby item.SerialNo descending
                             select new xmlDataInfo
                             {
                                 StrTitle = item.Title,
                                 StrApNo = item.ApNo.Trim(),
                                 StrApDate = item.ApDate,
                                 StrANX = UrlParameterCode_DE.encrypt(item.ApNo.Trim()),  //add by xiwl
                                 StrMainIPC = item.Ipc1,
                                 StrSerialNo = item.SerialNo.ToString(),
                                 //StrFtUrl = string.Format("http://202.106.92.181/cprs2010/comm/getimg.aspx?idx={0}&Ty=CNG", UrlParameterCode_DE.encrypt(item.ApNo.Trim())),
                                 CPIC = item.SerialNo.ToString(),    
                                 ZLNl=item.zlnl.ToString(),
                             };

                lstxml = result.ToList();
                //循环读取对应的XML补充其它字段数据信息
                foreach (var item in lstxml)
                {
                    UpdateByCnXmlFile(item, true);
                }


            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return lstxml;
        }

        private List<xmlDataInfo> GetCNRestulAge(List<int> lstNo)
        {
            List<xmlDataInfo> lstxml = new List<xmlDataInfo>();
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<CnGeneral_Info> tbCnDocInfo = db.CnGeneral_Info;

                var result = from item in tbCnDocInfo
                             where lstNo.Contains(Convert.ToInt32(item.SerialNo))
                             orderby item.SerialNo descending
                             select new xmlDataInfo
                             {                                 
                                 StrApNo = item.ApNo.Trim(),

                                 ZLNl = item.zlnl.ToString(),
                             };

                lstxml = result.ToList();
               

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return lstxml;
        }

    

        /// <summary>
        /// 根据申请号,判断专利类型 xxt 20130608 添加
        /// </summary>
        /// <param name="AppNo"></param>
        /// <returns></returns>
        public static string getZhuanLiLeiXing(string AppNo)
        {
            string flag = "";

            if (AppNo.Length > 10)
            {
                flag = AppNo.Substring(4, 1);
            }
            if (AppNo.Length <= 10)
            {
                flag = AppNo.Substring(2, 1);
            }

            return flag;
        }

        /// <summary>
        /// 根据申请号,实用新型|外观设计|发明专利
        /// </summary>
        /// <param name="AppNo"></param>
        /// <returns></returns>
        public static string getZhuanLiLeiXingName(string AppNo)
        {
            string flag = "";
            switch (getZhuanLiLeiXing(AppNo))
            {
                case "1":
                case "8":
                    flag = "发明专利";
                    break;
                case "2":
                case "9":
                    flag = "实用新型";
                    break;
                case "3":
                    flag = "外观设计";
                    break;
                default:
                    break;
            }
            return flag;
        }

        /// <summary>
        /// 根据申请号读取著录项目信息  xxt 20130607 添加
        /// </summary>
        /// <param name="lstNo"></param>
        /// <returns></returns>
        public List<xmlDataInfo> GetResultByAppNo(string AppNoX)
        {
            List<xmlDataInfo> lstxml = new List<xmlDataInfo>();

            try
            {
                xmlDataInfo RsItem = GetCnxmlDataInfo(AppNoX);
                if (RsItem != null)
                {
                    lstxml.Add(RsItem);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return lstxml;
        }

        /// <summary>
        /// 根据加密申请号获取著录项目数据信息
        /// </summary>
        /// <param name="AppNoX"></param>
        /// <returns></returns>
        public xmlDataInfo GetCnxmlDataInfo(string AppNoX)
        {
            string AppNo = UrlParameterCode_DE.DecryptionAll(AppNoX);
            xmlDataInfo resXmlInfo = null;
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                Table<CnGeneral_Info> tbCnDocInfo = db.CnGeneral_Info;

                var result = from item in tbCnDocInfo
                             where item.ApNo.Equals(AppNo)
                             select new xmlDataInfo
                             {
                                 StrTitle = item.Title,
                                 StrApNo = item.ApNo.Trim(),
                                 StrApDate = item.ApDate,
                                 StrANX = UrlParameterCode_DE.encrypt(item.ApNo.Trim()),  //add by xiwl
                                 StrMainIPC = item.Ipc1,
                                 StrSerialNo = item.SerialNo.ToString(),
                                 //StrFtUrl = string.Format("http://202.106.92.181/cprs2010/comm/getimg.aspx?idx={0}&Ty=CNG", UrlParameterCode_DE.encrypt(item.ApNo.Trim())),
                                 CPIC = item.SerialNo.ToString(),
                             };

                resXmlInfo = result.First();
                UpdateByCnXmlFile(resXmlInfo, false);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return resXmlInfo;
        }

        /// <summary>
        /// 根据加密申请号获取著录项目数据信息
        /// </summary>
        /// <param name="AppNoX"></param>
        /// <returns></returns>
        public xmlDataInfo GetCnxmlDataInfoByAn(string AppNo, bool _isMegPubAnn)
        {
            xmlDataInfo resXmlInfo = new xmlDataInfo();
            try
            {
                resXmlInfo.StrApNo = AppNo;
                resXmlInfo.StrANX = UrlParameterCode_DE.encrypt(AppNo);
                if (!UpdateByCnXmlFile(resXmlInfo, _isMegPubAnn))
                {
                    resXmlInfo = null;
                }
            }
            catch (Exception ex)
            {
                resXmlInfo = null;
                logger.Error(ex.ToString());
            }
            return resXmlInfo;
        }

        /// <summary>
        /// 填充中文数据
        /// </summary>
        /// <param name="_item"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        private bool UpdateByCnXmlFile(xmlDataInfo _item, bool _isMegPubAnn)
        {
            bool bRs = false;

            xmlDataInfo xmlInfoitem = _item;
            try
            {
                XmlParser xmlParser = new XmlParser("", "GB2312");
                CnIndexExtract cnIndexExtract = new CnIndexExtract();

                String xmlContent = getCnMainXmlContent(xmlInfoitem.StrApNo);
                xmlContent = Util.StringUtil.ReplaceLowOrderASCIICharacters(xmlContent);
                using (StringReader xmlString = new StringReader(xmlContent))
                {
                    using (XmlReader reader = XmlReader.Create(xmlString, xmlParser.Settings, xmlParser.Context))
                    {
                        XDocument xRoot = XDocument.Load(reader, LoadOptions.None);

                        xmlInfoitem.StrAgency = Cpic.Cprs2010.Cfg.DmManager.getAG_Display(cnIndexExtract.getAgency(xRoot));
                        xmlInfoitem.StrAgency_Addres = Cpic.Cprs2010.Cfg.DmManager.getAG_Addres(cnIndexExtract.getAgency(xRoot));
                        xmlInfoitem.StrDaiLiRen = cnIndexExtract.getAgents(xRoot);
                        xmlInfoitem.StrAnnDate = XmPatentComm.FormatDateVlue(cnIndexExtract.getAnnouncementDate(xRoot));
                        xmlInfoitem.StrAnnNo = cnIndexExtract.getAnnouncementNo(xRoot);
                        xmlInfoitem.StrApply = cnIndexExtract.getApply(xRoot);
                        xmlInfoitem.StrCountryCode = Cpic.Cprs2010.Cfg.DmManager.getCC_Display(cnIndexExtract.getProvinceCode(xRoot));
                        xmlInfoitem.StrFiled = cnIndexExtract.getField(xRoot);
                        xmlInfoitem.StrInventor = cnIndexExtract.getInventor(xRoot);
                        xmlInfoitem.StrPri = cnIndexExtract.getPriorNo(xRoot);

                        xmlInfoitem.StrPubDate = XmPatentComm.FormatDateVlue(cnIndexExtract.getPublicDate(xRoot));
                        xmlInfoitem.StrPubNo = cnIndexExtract.getPublicNo(xRoot);
                        if (_isMegPubAnn)
                        {
                            //xmlInfoitem.StrPubDate = getPubApdDate(xmlInfoitem.StrPubDate, xmlInfoitem.StrAnnDate);
                            //xmlInfoitem.StrPubNo = getPubApdNo(xmlInfoitem.StrPubNo, xmlInfoitem.StrAnnNo);
                        }
                        else
                        {
                            xmlInfoitem.StrTitle = cnIndexExtract.getTitle(xRoot);
                            //xmlInfoitem.StrApNo = cnIndexExtract.getApplyNo(xRoot);
                            xmlInfoitem.StrApDate = XmPatentComm.FormatDateVlue(cnIndexExtract.getApplyDate(xRoot));
                            //xmlInfoitem.StrANX = UrlParameterCode_DE.encrypt(xmlInfoitem.StrApNo.Trim());  //add by xiwl                           
                        }
                        xmlInfoitem.StrPdOrGd = getPubApdDate(xmlInfoitem.StrPubDate, xmlInfoitem.StrAnnDate);
                        xmlInfoitem.StrPnOrGn = getPubApdNo(xmlInfoitem.StrPubNo, xmlInfoitem.StrAnnNo);

                        xmlInfoitem.StrIpc = cnIndexExtract.getIPC(xRoot);
                        xmlInfoitem.StrFtUrl = string.Format("http://211.160.117.105/bns/comm/getimg.aspx?idx={0}&Ty=CNG", xmlInfoitem.StrANX);
                        //item.StrAbstr = cnIndexExtract.getAbstract(xRoot);//.Length >= 140 ? cnIndexExtract.getAbstract(xRoot).Substring(0, 140) : string.IsNullOrEmpty(cnIndexExtract.getAbstract(xRoot)) ? "无" : cnIndexExtract.getAbstract(xRoot);
                        xmlInfoitem.StrAbstr = cnIndexExtract.getAbstract(xRoot);
                        xmlInfoitem.StrAbstr = string.IsNullOrEmpty(xmlInfoitem.StrAbstr) ? "无" : xmlInfoitem.StrAbstr;

                        xmlInfoitem.StrClaim = cnIndexExtract.getMainClaim(xRoot);
                        xmlInfoitem.Brief = GetBriefInfo(xmlInfoitem.StrApNo);
                        xmlInfoitem.TongZu = "";   // GetTongZu(xmlInfoitem.StrPubNo, xmlInfoitem.CPIC);
                        xmlInfoitem.ZhuanLiLeiXing = getZhuanLiLeiXing(xmlInfoitem.StrApNo);

                        xmlInfoitem.StrMainIPC = cnIndexExtract.getMainIPC(xRoot);

                        if (xmlInfoitem.ZhuanLiLeiXing.Equals("3"))
                        {
                            string pubDate = System.Web.HttpUtility.UrlEncode(cnIndexExtract.getAnnouncementDate(xRoot));
                            xmlInfoitem.StrFtUrl = string.Format("http://211.160.117.105/bns/comm/getimg.aspx?idx={0}&Ty=CNG&ImgTp=wg&pd={1}", xmlInfoitem.StrANX, pubDate);

                            xmlInfoitem.StrMainIPC = XmPatentComm.RevertDegIpc(xmlInfoitem.StrMainIPC);
                            string strIpcTmp = "";
                            foreach (string strItem in xmlInfoitem.StrIpc.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                            {
                                strIpcTmp += XmPatentComm.RevertDegIpc(strItem) + ";";
                            }
                            xmlInfoitem.StrIpc = strIpcTmp.TrimEnd(';');
                        }
                        xmlInfoitem.StrApNo = string.Format("{0}.{1}", xmlInfoitem.StrApNo.Trim(), CnAppLicationNo.getValidCode(xmlInfoitem.StrApNo)); //add by xiwl;   
                        xmlInfoitem.FaLvZhuangTai = "../my/frmFLZT.aspx?AppNo=" + xmlInfoitem.StrANX;

                        xmlInfoitem.StrShenQingRenDiZhi = cnIndexExtract.getAddress(xRoot);

                        _item.StrApDate = XmPatentComm.FormatDateVlue(_item.StrApDate);

                        xmlInfoitem.StrFtUrl += _isMegPubAnn ? "" : "&bg=";
                    }
                }
                bRs = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                bRs = false;
            }
            return bRs;
        }

        public static string getCnMainXmlContent(string strApNo)
        {
            String xmlContent = "";
            try
            {
                //strApNo = "201210002022";
                String xmlpath = cnDataService.getAbsXmlFile(strApNo);
                xmlContent = System.IO.File.ReadAllText(xmlpath, System.Text.Encoding.GetEncoding("gb2312"));
            }
            catch (Exception err)
            {
                logger.Error(err.ToString());
                WSYQ.CprsGIISWebSvcSoapClient wsyq = new WSYQ.CprsGIISWebSvcSoapClient();
                xmlContent = wsyq.GetPatentData(UrlParameterCode_DE.encrypt(strApNo), WSYQ.PatentDataType.CnMabXmlTxt);
            }
            return xmlContent;
        }

        public static string getEnMainXmlContent(string strPubNo)
        {
            String xmlContent = "";

            try
            {
                //strApNo = "US2012000001A1";
                String xmlpath = DocdbDataService.getXmlFile(strPubNo);

                if (File.Exists(xmlpath))
                {
                    xmlContent = File.ReadAllText(xmlpath, Encoding.GetEncoding("gb2312"));

                    if (xmlContent.Contains("cpic-format-docment"))   //US2012000600A1
                    {
                        xmlContent = xmlContent.Replace("cpic-format-docment", "exchange-document");
                        xmlContent = xmlContent.Replace("data-format=\"dcdb", "data-format=\"docdb");
                        xmlContent = xmlContent.Replace("data-format=\"ecdb", "data-format=\"epodoc");
                        xmlContent = xmlContent.Replace("-dcdb=\"", "-docdb=\"");
                    }
                }
                else
                {
                    logger.Debug("----------" + xmlpath + "NO FIle");
                    WSYQ.CprsGIISWebSvcSoapClient wsyq = new WSYQ.CprsGIISWebSvcSoapClient();
                    xmlContent = wsyq.GetPatentData(UrlParameterCode_DE.encrypt(strPubNo), WSYQ.PatentDataType.EnMabXmlTxt);
                }
            }
            catch (Exception err)
            {
                logger.Error(err.ToString());
            }
            return xmlContent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_item"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        private bool UpdateByEnXmlFile(xmlDataInfo _item, bool _isDetails)
        {
            bool bRs = false;
            xmlDataInfo item = _item;
            try
            {

                XmlParser xmlParser = new XmlParser("", "UTF-8");
                DocDBIndexExtract docDBIndexExtract = new DocDBIndexExtract(xmlParser);
                String xmlContent = getEnMainXmlContent(item.StrPubNo);
                if (!xmlContent.Contains("exch:"))
                {
                    docDBIndexExtract.exch = "";
                }
                //logger.Debug(xmlContent);
                //xmlContent = Util.StringUtil.ReplaceLowOrderASCIICharacters(xmlContent);
                using (StringReader xmlString = new StringReader(xmlContent))
                {
                    using (XmlReader reader = XmlReader.Create(xmlString, xmlParser.Settings, xmlParser.Context))
                    {
                        XDocument xRoot = XDocument.Load(reader, LoadOptions.None);

                        //item.StrCountryCode = Cpic.Cprs2010.Cfg.DmManager.getCC_Display(docDBIndexExtract.getAppliesCountry(xRoot));


                        if (_isDetails)
                        {
                            item.StrApply = docDBIndexExtract.getAppliesAndCC(xRoot);
                            item.StrInventor = docDBIndexExtract.getInventorsAndCC(xRoot);
                            //item.TongZu = GetTongZu(item.StrPubNo, item.CPIC,1);
                        }
                        else
                        {
                            item.StrApply = docDBIndexExtract.getApplies(xRoot);
                            item.StrInventor = docDBIndexExtract.getInventors(xRoot);
                        }

                        item.StrPubDate = docDBIndexExtract.getPublicDate(xRoot);
                        item.StrFtUrl = getEnFtUrl(item.StrPubDate, item.StrPubNo);

                        /////------------
                        item.StrEpoApNo = docDBIndexExtract.getEpoApplyNo(xRoot);
                        item.StrOriginalApNo = docDBIndexExtract.getOriginalApplyNo(xRoot);
                        item.StrDocdbApNo = docDBIndexExtract.getDocdbApplyNo(xRoot);

                        item.StrDocdbPubNo = docDBIndexExtract.getDocdbPublicDocNo(xRoot);
                        item.StrEpoPubNo = docDBIndexExtract.getEpoPublicDocNo(xRoot);
                        item.StrOriginalPubNo = docDBIndexExtract.getOriginalPublicDocNo(xRoot);
                        //item.StrPubNo = docDBIndexExtract.getPublicDocNo(xRoot);
                        item.StrAbstr = docDBIndexExtract.getAbstract(xRoot);
                        item.StrAbsFmy = docDBIndexExtract.getFmyAbstract(xRoot);
                        item.StrRefDoc = docDBIndexExtract.getCited(xRoot);
                        item.StrPri = docDBIndexExtract.getEpoPriorAndPrd(xRoot);

                        item.StrEcla = docDBIndexExtract.getECLA(xRoot);

                        if (string.IsNullOrEmpty(item.StrTitle.Trim()))
                        {
                            item.StrTitle = docDBIndexExtract.getInventionTitle(xRoot);
                        }
                        if (string.IsNullOrEmpty(item.StrApDate))
                        {
                            item.StrApDate = docDBIndexExtract.getApplyDate(xRoot);
                        }
                    }
                }

                item.StrApDate = XmPatentComm.FormatDateVlue(item.StrApDate);
                item.StrPubDate = item.StrPdOrGd = XmPatentComm.FormatDateVlue(item.StrPubDate);
                item.StrPnOrGn = item.StrPubNo;
                item.StrMainIPC = item.StrIpc.Split(';')[0];
                bRs = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                bRs = false;
            }
            return bRs;
        }

        private string getEnFtUrl(string strPud, string pubId)
        {
            string strRt = "";
            try
            {
                string strCC = pubId.Substring(0, 2);
                string kind = pubId.Substring(pubId.Length - 2);
                try
                {
                    Convert.ToInt32(pubId.Substring(pubId.Length - 1));
                }
                catch (Exception ex)
                {
                    kind = pubId.Substring(pubId.Length - 1);
                }

                string strPidTo2 = pubId.Substring(2);

                strRt = string.Format("http://worldwide.espacenet.com/espacenetImage.jpg?flavour=firstPageClipping&locale=en_EP&FT=D&date={0}&CC={1}&NR={2}&KD={3}",
                     strPud, strCC, strPidTo2, kind);
            }
            catch (Exception ex)
            {
            }
            return strRt;
        }

        public static string getEpoPatDetailUrl(string pubId, string strPud)
        {
            string strRt = "";
            try
            {
                string strCC = pubId.Substring(0, 2);
                string kind = pubId.Substring(pubId.Length - 2);
                try
                {
                    Convert.ToInt32(pubId.Substring(pubId.Length - 1));
                }
                catch (Exception ex)
                {
                    kind = pubId.Substring(pubId.Length - 1);
                }

                string strPidTo2 = pubId.Substring(2);

                //http://worldwide.espacenet.com/publicationDetails/biblio?DB=EPODOC&locale=cn_EP&FT=D&CC=EP&NR=2039599A3&KC=A3
                strRt = string.Format("http://worldwide.espacenet.com/publicationDetails/biblio?DB=EPODOC&locale=cn_EP&FT=D&CC={0}&NR={1}&KC={2}",
                      strCC, strPidTo2, kind);
            }
            catch (Exception ex)
            {
            }
            return strRt;
        }

        /// <summary>
        /// 获取同组专利的公开号 xxt20130606
        /// </summary>
        /// <param name="PubNo">公开号</param>
        /// <returns>同组专利的公开号,不包含本身</returns>
        public string GetTongZu(string PubNo)
        {
            ResultDataManagerDataContext db = new ResultDataManagerDataContext();
            Table<DocdbDocInfo> tbDocdbInfo = db.DocdbDocInfo;

            string pubNos = "";

            var result = from item in tbDocdbInfo
                         where item.PubID.Equals(PubNo)
                         select item.CPIC;

            pubNos = GetTongZu(PubNo, result.First().Value.ToString(), 0);
            return pubNos;
        }

        /// <summary>
        /// 获取同组专利的公开号 xxt20130606
        /// </summary>
        /// <param name="PubNo">公开号</param>
        /// <returns>同组专利的公开号,不包含本身</returns>
        private string GetTongZu(string PubNo, string CPIC, int nTopN)
        {
            ResultDataManagerDataContext db = new ResultDataManagerDataContext();
            Table<DocdbDocInfo> tbDocdbInfo = db.DocdbDocInfo;

            string pubNos = "";

            var result = from item in tbDocdbInfo
                         where item.CPIC.Equals(CPIC) && item.PubID != PubNo
                         select item.PubID;

            if (nTopN > 0)
            {
                result = result.Take(nTopN);
            }
            foreach (var pub in result)
            {
                pubNos = pubNos + pub + ";";
            }
            pubNos = pubNos.TrimEnd(';');
            return pubNos;
        }

        private string GetBriefInfo(string appNo)
        {
            string breif = "";
            Regex reg = new Regex("(^.{2}3.{5}$)|(^.{2}3.{5}\\..$)|(^.{4}3.{7}$)|(^.{4}3.{7}\\..$)");
            Match match = reg.Match(appNo);
            breif = match.Success ? "简要说明:" : "摘要:";
            return breif;
        }

        private string getPubApdNo(string _strPubNo, string _strApdNo)
        {
            string strRet = "";
            try
            {
                if ((string.IsNullOrEmpty(_strPubNo) || _strPubNo.StartsWith("000")))
                {
                    strRet = string.Format("[公告]{0}", _strApdNo);
                }
                else if (string.IsNullOrEmpty(_strApdNo) && !_strApdNo.StartsWith("000"))
                {
                    strRet = string.Format("[公开]{0}", _strPubNo);
                }
                else
                {
                    strRet = string.Format("[公告]{0}", _strApdNo);
                }
            }
            catch (Exception ex)
            {
            }
            return strRet;
        }

        private string getPubApdDate(string _strPubDate, string _stGudDate)
        {
            string strRet = "";
            try
            {
                if ((string.IsNullOrEmpty(_strPubDate) || _strPubDate.StartsWith("000")))
                {
                    strRet = string.Format("[公告]{0}", _stGudDate);
                }
                else if (string.IsNullOrEmpty(_stGudDate) && !_stGudDate.StartsWith("000"))
                {
                    strRet = string.Format("[公开]{0}", _strPubDate);
                }
                else
                {
                    strRet = string.Format("[公告]{0}", _stGudDate);
                }
            }
            catch (Exception ex)
            {
            }
            return strRet;
        }

        #endregion

        /// <summary>
        /// 标引
        /// </summary>
        /// <param name="AppNo"></param>
        /// <returns></returns>
        public string GetBiaoYin(string AppNo)
        {
            List<string> strBiaoYin;
            BiaoYin.WebSerIndexingSoapClient by = new BiaoYin.WebSerIndexingSoapClient();
            strBiaoYin = by.getAutoIndexWord(AppNo);

            string content = "";
            for (int i = 0; i < strBiaoYin.Count; i++)
            {
                content += strBiaoYin[i].ToString() + ";";
            }
            content = content.TrimEnd(';');
            return content;
        }
    }
}
