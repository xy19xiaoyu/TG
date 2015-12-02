#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: Default1.aspx.cs
*	创 建 人: xiwenlei(xiwl)
*	创建日期: 2010-10-29 17:13:38
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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Cpic.Cprs2010.Search;
using Cpic.Cprs2010.User;
using Cpic.Cprs2010.Search.SearchManager;
using log4net;
using System.Reflection;
using Cpic.Cprs2010.Search.ResultData;
using Cpic.Cprs2010.Cfg.Data;
using Cpic.Cprs2010.Cfg.Port;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ParseXml;
using System.IO;
using System.Text;
using System.Xml;
using Patentquery.Svc;

namespace Cprs2010Web.Svc
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum PatentDataType
    {
        /// <summary>
        /// 中文著录项目xml文本
        /// </summary>
        CnMabXmlTxt = 0,

        /// <summary>
        /// 中文说明书XML文本
        /// </summary>
        CnDesXmlTxt = 1,

        /// <summary>
        /// 中文权利要求XML文本
        /// </summary>
        CnClmXmlTxt = 2,

        /// <summary>
        /// 中文摘要附图网址
        /// </summary>
        CnAbsFuTuUrl = 3,

        /// <summary>
        /// 中文外观图形URL
        /// </summary>
        CnWGImgUrls = 4,

        /// <summary>
        /// 英文著录项目XML文本
        /// </summary>
        EnMabXmlTxt = 20,

        /// <summary>
        /// PDF URL
        /// </summary>
        PDFFileUrl = 100,
    }

    /// <summary>
    /// CprsGIISWebSvc 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class CprsGIISWebSvc : System.Web.Services.WebService
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string strSearchGroupName = "PORT";

        /// <summary>
        /// 获取一游客身份
        /// </summary>
        /// <returns>ER开头的为错误返回，错误信息为ER后；正常情况下位7位数字。</returns>
        [WebMethod(EnableSession = true)]
        public string getYKSearchUserID()
        {
            string strRetu = "";

            try
            {
                Cpic.Cprs2010.User.User UserYk = UserManager.getGuestUser(HttpContext.Current.Session.SessionID);

                if (UserYk != null)
                {
                    strRetu = UserYk.ID.ToString();
                }
                else
                {
                    strRetu = "ER:服务器资源不足，请稍后再试";
                }
            }
            catch (Exception ex)
            {
                strRetu = string.Format("ER:{0}", ex.Message);
                logger.Error(ex.ToString());
            }

            return strRetu;
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="_strULoginName"></param>
        /// <param name="_strPwd"></param>
        /// <param name="_strID"></param>
        /// <returns></returns>
        [WebMethod]
        private bool MappingUser(string _strULoginName, string _strPwd, out string _strUID)
        {
            bool bReturn = false;
            _strUID = "";


            //2.判断用户编码是否已经存在
            if (UserManager.ChcekUserName(_strULoginName))
            {
                //alert("请添加的帐号已经存在！");
                _strUID = string.Format("ER:{0}", "添加的帐号已经存在");
                return false;
            }

            //添加用户
            try
            {
                bReturn = UserManager.AddUser(_strULoginName.Trim(), "移动终端用户", _strPwd, "", UserManager.GetRoleCodeByRoleName("移动终端用户"), 0);

                if (bReturn)
                {
                    _strUID = DBA.SqlDbAccess.ExecuteScalar(CommandType.Text, string.Format("select id from TbUserInfo where User_logname='{0}'", _strULoginName)).ToString();
                }
                else
                {
                    _strUID = string.Format("ER:{0}", "映射帐号失败");
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                _strUID = string.Format("ER:{0}", ex.Message);
                logger.Error(ex.ToString());
            }

            return bReturn;
        }

        /// <summary>
        /// 获取检索结果数
        /// </summary>
        /// <param name="_strUID">用户ID</param>
        /// <param name="_SDbType"></param>
        /// <param name="_strSID">取值[000-999]</param>
        /// <param name="_strSearchQuery">检索式</param>
        /// <returns>ok</returns>
        [WebMethod]
        public string DoSearch(string _strUID, SearchDbType _SDbType, string _strSID, string _strSearchQuery)
        {
            string strRt = "";

            if (_strSearchQuery.TrimStart().Substring(0, 4).ToUpper().Equals("F YY"))
            {
                string strFyy = _strSearchQuery.TrimStart().Substring(4).Trim();
                string strDbTy = "CN";

                simpleSearch simp = new simpleSearch();
                if (_SDbType == SearchDbType.DocDB)
                {
                    strDbTy = "en";
                }
                _strSearchQuery = simp.simpleSearchNew(strDbTy, strFyy);

                if (!_strSearchQuery.StartsWith("F "))
                {
                    strRt = string.Format("ER:{0}", _strSearchQuery);
                    return strRt;
                }
            }

            // 检索式
            SearchPattern sp = new SearchPattern();
            sp.Pattern = _strSearchQuery.Trim();
            sp.DbType = _SDbType;
            sp.SearchNo = _strSID;
            sp.UserId = int.Parse(_strUID);
            //user.addSearchHis(sp);

            try
            {
                //--->up by xiwl 
                //// 得到检索链接
                //ISearch mySearch = SearchFactory.CreatSearch(sp.DbType);

                //ResultInfo rs = mySearch.Search(sp);
                //SearchFactory.FreeSearch(mySearch);
                //----------
                SearchInterface.ClsSearch cls = new SearchInterface.ClsSearch();
                ResultInfo rs = cls.DoSearch(strSearchGroupName, sp.Pattern, sp.UserId, sp.SearchNo, sp.DbType);
                //<<<<<<----up by xiwl

                //// 设置检索结果文件路径
                //ResultServices resultService = new ResultServices();
                //string resultFilePath = resultService.getResultFilePath(sp);
                //string strDY_CnpFileBasePath = ConfigurationManager.AppSettings["CPRS2010UserPath"].ToString();
                //// 将文件物理路径转换成为网络路径
                ////CPRS2010UserPath 对应的路径是 \\192.168.70.60\CPRS2010_User\
                //string userPath = ConfigurationManager.AppSettings["CPRS2010UserPath_UserInterest"].ToString();
                //resultFilePath = resultFilePath.Replace(strDY_CnpFileBasePath, userPath);

                //ResultInfoWebService resultInfoWebService = new ResultInfoWebService();
                //resultInfoWebService.ResultInfo = rs;
                //resultInfoWebService.ResultSearchFilePath = resultFilePath;

                //(800)F AN 2001 <hits: 176528>
                //(800)F AN 2001 <error: 检索项错误>
                Regex reg = new Regex(@"\.*<(.*)>.*");
                Match match = reg.Match(rs.HitMsg);
                if (match.Success)
                {
                    strRt = match.Groups[1].ToString();
                    if (strRt.Contains("hits:"))
                    {
                        strRt = strRt.Replace("hits:", "").Trim();
                    }
                    else
                    {
                        strRt = string.Format("ER:{0}", strRt.Replace("error:", "").Trim());
                    }
                }
                else
                {
                    strRt = string.Format("ER:{0}", rs.HitMsg);
                }
            }
            catch (Exception ex)
            {
                strRt = string.Format("ER:{0}", ex.Message);
                logger.Error(ex.ToString());
            }
            return strRt;
        }

        /// <summary>
        /// 检索结果概览信息查看接口
        /// </summary>
        /// <param name="_strUID">检索用户ID</param>
        /// <param name="_SDbType">数据源</param>
        /// <param name="_strSID">检索编号</param>
        /// <param name="_pageNo">页码</param>
        /// <param name="_pageSize">页码大小</param>
        /// <returns></returns>
        [WebMethod]
        public List<GeneralDataInfo> GetGeneralData(string _strUID, SearchDbType _SDbType, string _strSID, int _pageNo, int _pageSize)
        {
            List<GeneralDataInfo> iRt = null;
            try
            {

                SearchPattern sp = new SearchPattern();
                sp.DbType = _SDbType;
                sp.SearchNo = _strSID.PadLeft(3, '0');
                sp.UserId = int.Parse(_strUID);
                sp.GroupName = strSearchGroupName;
                switch (_SDbType)
                {
                    case SearchDbType.Cn:
                        cnDbResultData cnResult = new cnDbResultData();
                        iRt = cnResult.GetResultListDataInfo(sp, _pageSize, _pageNo, "");
                        break;
                    case SearchDbType.DocDB:
                        //DocdbDbResultData enResult = new DocdbDbResultData();
                        //iRt = enResult.GetResultListDataInfo(sp, _pageSize, _pageNo, "");
                        SearchInterface.ClsSearch cls = new SearchInterface.ClsSearch();
                        iRt = cls.GetResultListDocInfo(sp, _pageSize, _pageNo, "");
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
            }

            return iRt;
        }

        /// <summary>
        /// 检索结果概览信息查看接口
        /// </summary>
        /// <param name="_strUID">检索用户ID</param>
        /// <param name="_SDbType">数据源</param>
        /// <param name="_strSID">检索编号</param>
        /// <param name="_pageNo">页码</param>
        /// <param name="_pageSize">页码大小</param>
        /// <returns></returns>
        [WebMethod]
        public List<xmlDataInfo> GetGeneralData_Xml(string _strUID, SearchDbType _SDbType, string _strSID, int _pageNo, int _pageSize)
        {
            List<xmlDataInfo> iRt = null;
            try
            {
                // add by zhangqiuyi 20150624 为手机APP端预警数据概览接口添加方法
                if (_strSID.StartsWith("YJ"))
                {
                    string[] str = _strSID.Split('|');
                    if (str.Length == 3)
                    {
                        List<int> lstNo = ProYJDLL.YJDB.getYJItemByWID(Convert.ToInt32(str[1]), Convert.ToInt16(str[2]), _pageNo, _pageSize);
                        SearchInterface.ClsSearch cls = new SearchInterface.ClsSearch();
                        if (_SDbType == Cpic.Cprs2010.Search.SearchDbType.Cn)
                        {
                            iRt = cls.GetResult(lstNo, "CN");
                        }
                        else if (_SDbType == Cpic.Cprs2010.Search.SearchDbType.DocDB)
                        {
                            iRt = cls.GetResult(lstNo, "DocDB");
                        }
                    }
                    else
                        return iRt;
                }
                else
                {
                    SearchPattern sp = new SearchPattern();
                    sp.DbType = _SDbType;
                    sp.SearchNo = _strSID.PadLeft(3, '0');
                    sp.UserId = int.Parse(_strUID);
                    sp.GroupName = strSearchGroupName;
                    SearchInterface.ClsSearch cls = new SearchInterface.ClsSearch();
                    iRt = cls.GetResultList(sp, _pageSize, _pageNo, "");
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
            }

            return iRt;
        }

        /// <summary>
        /// 检索结果概览信息查看接口
        /// </summary>
        /// <param name="_lstNo">号单</param>
        /// <param name="_SDbType">数据源</param>
        /// <param name="_pageNo">页码</param>
        /// <param name="_pageSize">页码大小</param>
        /// <returns></returns>
        [WebMethod]
        public List<GeneralDataInfo> GetGeneralDataByLstNo(List<int> _lstNo, SearchDbType _SDbType, int _pageNo, int _pageSize)
        {
            List<GeneralDataInfo> iRt = null;
            try
            {
                switch (_SDbType)
                {
                    case SearchDbType.Cn:
                        cnDbResultData cnResult = new cnDbResultData();
                        iRt = cnResult.GetResultListDataInfo(_lstNo, _pageSize, _pageNo, "");
                        break;
                    case SearchDbType.DocDB:
                        DocdbDbResultData enResult = new DocdbDbResultData();
                        //iRt = enResult.GetResultFmlListDataInfo(_lstNo, _pageSize, _pageNo, "");
                        iRt = enResult.GetResulDoctListDataInfo(_lstNo, _pageSize, _pageNo, "");
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
            }

            return iRt;
        }

        /// <summary>
        /// get maxID
        /// </summary>
        /// <param name="_SDbType"></param>
        /// <returns></returns>
        [WebMethod]
        public int getDbMaxSerialNo(SearchDbType _SDbType)
        {
            int nRs = 1;
            try
            {
                ResultDataManagerDataContext db = new ResultDataManagerDataContext();
                switch (_SDbType)
                {
                    case SearchDbType.Cn:

                        nRs = Convert.ToInt32(db.CnGeneral_Info.Max(p => p.SerialNo));
                        break;
                    case SearchDbType.DocDB:
                        nRs = db.DocdbDocInfo.Max(p => p.ID);
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return nRs;
        }

        /// <summary>
        /// 检索结果概览信息查看接口
        /// </summary>
        /// <param name="_StrNo">号单，多个可使用[~,;]连接</param>
        /// <param name="_SDbType">数据源</param>
        /// <param name="_pageNo">页码</param>
        /// <param name="_pageSize">页码大小</param>
        /// <returns></returns>
        [WebMethod]
        public List<GeneralDataInfo> GetGeneralDataByString(string _StrNo, SearchDbType _SDbType, int _pageNo, int _pageSize)
        {
            List<GeneralDataInfo> iRt = null;
            try
            {
                List<string> tmplist = _StrNo.Split("~,;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                List<int> lstNO = tmplist.ConvertAll<int>(x => Convert.ToInt32(x));

                iRt = GetGeneralDataByLstNo(lstNO, _SDbType, _pageNo, _pageSize);
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
            }

            return iRt;
        }

        /// <summary>
        /// 检索结果概览信息查看接口 获取指定家族成员的信息
        /// </summary>
        /// <param name="_strUID">检索用户ID</param>
        /// <param name="_SDbType">数据源</param>
        /// <param name="_nCPIC">族号</param>
        /// <param name="_pageNo">页码</param>
        /// <param name="_pageSize">页码大小</param>
        /// <returns></returns>
        [WebMethod]
        private List<GeneralDataInfo> GetFmlMemberData(string _strUID, SearchDbType _SDbType, int _nCPIC, int _pageNo, int _pageSize)
        {
            List<GeneralDataInfo> iRt = null;
            try
            {
                switch (_SDbType)
                {
                    case SearchDbType.Cn:
                        cnDbResultData cnResult = new cnDbResultData();
                        List<int> lst = new List<int>();
                        lst.Add(_nCPIC);
                        iRt = cnResult.GetResultListDataInfo(lst, _pageSize, _pageNo, "");
                        break;
                    case SearchDbType.DocDB:
                        DocdbDbResultData enResult = new DocdbDbResultData();
                        iRt = enResult.GetRstDetailListDataInfo(_pageSize, _pageNo, _nCPIC.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
            }

            return iRt;
        }

        /// <summary>
        /// 专利数据查看接口
        /// </summary>
        /// <param name="_strPID"></param>
        /// <param name="_PdTpe"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetPatentData(string _strPID, PatentDataType _PdTpe)
        {
            string strRt = "";

            //解密_strPID
            string strPID = Cpic.Cprs2010.Cfg.UrlParameterCode_DE.DecryptionAll(_strPID.Trim());
            string strTmp = "";

            switch (_PdTpe)
            {
                case PatentDataType.CnMabXmlTxt:
                    //strTmp = cnDataService.getAbsXmlFile(strPID);
                    //strRt = System.IO.File.ReadAllText(strTmp, System.Text.Encoding.GetEncoding("gb2312"));
                    strRt = SearchInterface.ClsSearch.getCnMainXmlContent(strPID);
                    break;
                case PatentDataType.CnDesXmlTxt:
                    //string xmltext = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "0");
                    SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                    strRt = search.getInfoByPatentID(_strPID, "CN", "1");
                    break;
                case PatentDataType.CnClmXmlTxt:
                    SearchInterface.ClsSearch searchDes = new SearchInterface.ClsSearch();
                    strRt = searchDes.getInfoByPatentID(_strPID, "CN", "0");
                    break;
                case PatentDataType.CnAbsFuTuUrl: 
                    string flag = strPID.Length > 10 ? strPID.Substring(4, 1) : strPID.Substring(2, 1);
                    if (flag.Equals("3"))
                    {
                        SearchInterface.ClsSearch WgImg = new SearchInterface.ClsSearch();
                        strRt = WgImg.getInfoByPatentID(_strPID, "CN", "4").Split('|')[0];                        
                    }
                    else
                    {
                        strRt = string.Format("http://211.160.117.105/bns/comm/getimg.aspx?idx={0}&Ty=CNG", _strPID);
                    }
                    break;
                case PatentDataType.CnWGImgUrls:
                    //strRt = getCnWgImgUrls(strPID);  
                    SearchInterface.ClsSearch searchWgImg = new SearchInterface.ClsSearch();
                    strRt = searchWgImg.getInfoByPatentID(_strPID, "CN", "4");
                    break;
                case PatentDataType.EnMabXmlTxt:
                    //strTmp = DocdbDataService.getXmlFile(strPID);
                    //strRt = System.IO.File.ReadAllText(strTmp);
                    strRt = SearchInterface.ClsSearch.getEnMainXmlContent(strPID);
                    break;
                case PatentDataType.PDFFileUrl:
                    //TBD:PAD暂不调用接口
                    EPDSInterface bnsPort = new EPDSInterface();
                    EpdsInterfaceType EiType = EpdsInterfaceType.PubNo;
                    if ("0123456789".IndexOf(strPID.Substring(0, 1)) > 0)
                    {
                        EiType = EpdsInterfaceType.ApNo;
                    }
                    //获取PDF
                    string[] bnsFiles = bnsPort.GetBnsFiles(strPID, true, EiType);

                    //域名转换
                    string strDns = HttpContext.Current.Request.Url.DnsSafeHost;
                    for (int i = 0; i < bnsFiles.Length; i++)
                    {
                        strRt += bnsFiles[i] + "|";
                    }
                    strRt = strRt.TrimEnd('|');
                    break;

            }
            return strRt;
        }

        /// <summary>
        /// 专利数据查看接口
        /// </summary>
        /// <param name="_strPID"></param>
        /// <param name="_PdTpe"></param>
        /// <returns></returns>
        [WebMethod]
        public xmlDataInfo GetPatentXmlDataInfo(string _strPID, PatentDataType _PdTpe)
        {
            xmlDataInfo strRt = null;

            SearchInterface.ClsSearch cls = new SearchInterface.ClsSearch();

            switch (_PdTpe)
            {
                case PatentDataType.CnMabXmlTxt:
                    //strTmp = cnDataService.getAbsXmlFile(strPID);
                    //strRt = System.IO.File.ReadAllText(strTmp, System.Text.Encoding.GetEncoding("gb2312"));
                    strRt = cls.GetCnxmlDataInfo(_strPID);
                    break;
                case PatentDataType.EnMabXmlTxt:
                    //strTmp = DocdbDataService.getXmlFile(strPID);
                    //strRt = System.IO.File.ReadAllText(strTmp);
                    strRt = cls.GetEnxmlDataInfo(_strPID);
                    break;

            }
            return strRt;
        }

        [WebMethod]
        public string GetPatentData_ByKey(string strKey, string _strAnOrPn, PatentDataType _PdTpe)
        {
            string strRs = "";
            try
            {
                if (!strKey.Equals("Hwx4sIALyC5UwA/SWytnqGSoYChnyGHQZUhiyGIwYjBgMIRiAwYzBgBrpbcNIAAxl2"))
                {
                    return "您的key没有权限访问该接口!";
                }
                string strAnoPn = _strAnOrPn;
                switch (_PdTpe)
                {
                    case PatentDataType.CnMabXmlTxt:
                        strAnoPn = _strAnOrPn.Substring(0, 12);
                        break;
                    default:
                        break;
                }

                strAnoPn = Cpic.Cprs2010.Cfg.UrlParameterCode_DE.encrypt(strAnoPn);

                strRs = GetPatentData(strAnoPn, _PdTpe);

            }
            catch (Exception ex)
            {
            }
            return strRs;
        }

        /// <summary>
        /// 获取同组专利的公开号 
        /// </summary>
        /// <param name="_strPID">加密公开号</param>        
        /// <returns></returns>
        [WebMethod]
        public string GetFmlMemberPubIds(string _strPubNo)
        {
            string strRs = "";
            try
            {
                string strPID = Cpic.Cprs2010.Cfg.UrlParameterCode_DE.DecryptionAll(_strPubNo.Trim());
                SearchInterface.ClsSearch cls = new SearchInterface.ClsSearch();
                strRs = cls.GetTongZu(strPID);
            }
            catch (Exception ex)
            {
            }
            return strRs;
        }

        /// <summary>
        /// 专利家族检测接口
        /// </summary>
        /// <param name="_strPID"></param>
        /// <param name="_PdTpe"></param>
        /// <returns></returns>
        [WebMethod]
        public bool checkfml(int fmlid)
        {
            FMLHelper fh = new FMLHelper();
            return fh.checkfml(fmlid);
        }
        /// <summary>
        /// 得到专利家族接口
        /// </summary>
        /// <param name="_strPID"></param>
        /// <param name="_PdTpe"></param>
        /// <returns></returns>
        [WebMethod]
        public List<fmlinfo> getFml(int fmlid)
        {
            FMLHelper fh = new FMLHelper();
            return fh.getfml(fmlid);
        }
        [WebMethod]
        public bool QDST(int userid, string type, List<int> hit)
        {
            ResultServices s = new Cpic.Cprs2010.Search.ResultServices();
            SearchDbType t;
            type = type.ToUpper();
            if (type == "CN")
            {
                t = SearchDbType.Cn;
            }
            else
            {
                t = SearchDbType.DocDB;
            }
            string filename = s.getResultFilePathOnly(new SearchPattern() { DbType = t, UserId = userid, SearchNo = "998" });
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
            }

            try
            {
                if (File.Exists(filename)) File.Delete(filename);
                using (FileStream fsw = new FileStream(filename, FileMode.CreateNew, FileAccess.Write))
                {
                    foreach (var x in hit)
                    {
                        byte[] by = BitConverter.GetBytes(x);
                        fsw.Write(by, 0, by.Length);
                    }
                }
            }
            catch (Exception)
            {

            }
            return true;

        }
    }
}
