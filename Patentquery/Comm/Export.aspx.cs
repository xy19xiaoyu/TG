using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text;
using Cpic.Cprs2010.Search.ResultData;
using TLC.BusinessLogicLayer;
using System.IO;
using TLC;
using Cpic.Cprs2010.Search;
using System.Data;
using log4net;
using System.Reflection;
using ProXZQDLL;

namespace Patentquery.Comm
{
    public partial class Export : System.Web.UI.Page
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static WSFLZT.Service ss = new WSFLZT.Service();

        public static DataTable List2DataTable(List<xmlDataInfo> listXml, string ColNames, string type, string NodeId)
        {
            try
            {                
                string UserName = "游客";
                if (HttpContext.Current.Session["UserInfo"] != null)
                {
                    UserName = ((TbUser)HttpContext.Current.Session["UserInfo"]).UserName;
                }
                List<string> strcolnames = ColNames.Split('|').ToList<string>();
                DataTable dt = new DataTable();
                DataColumn colid = new DataColumn("pid", typeof(int));
                DataColumn coltype = new DataColumn("type", typeof(string));
                DataColumn colipc1 = new DataColumn("ipc1", typeof(string));
                DataColumn colipc3 = new DataColumn("ipc3", typeof(string));
                DataColumn colipc4 = new DataColumn("ipc4", typeof(string));
                DataColumn colipc7 = new DataColumn("ipc7", typeof(string));
                DataColumn colipc = new DataColumn("ipc", typeof(string));
                DataColumn RealName = new DataColumn("UserName", typeof(string));

                dt.Columns.Add(colid);
                dt.Columns.Add(coltype);
                dt.Columns.Add(colipc1);
                dt.Columns.Add(colipc3);
                dt.Columns.Add(colipc4);
                dt.Columns.Add(colipc7);
                dt.Columns.Add(colipc);
                dt.Columns.Add(RealName);


                DataTable dtresult = new DataTable();
                DataColumn col1 = new DataColumn("申请号", typeof(string));
                DataColumn col2 = new DataColumn("公开（公告）号", typeof(string));
                DataColumn col3 = new DataColumn("分类号", typeof(string));
                DataColumn col4 = new DataColumn("主分类号", typeof(string));
                DataColumn col5 = new DataColumn("名称", typeof(string));
                DataColumn col6 = new DataColumn("公开（公告）日", typeof(string));
                DataColumn col7 = new DataColumn("申请日", typeof(string));
                DataColumn col8 = new DataColumn("代理人", typeof(string));
                DataColumn col9 = new DataColumn("专利代理机构", typeof(string));
                DataColumn col10 = new DataColumn("摘要", typeof(string));
                DataColumn col11 = new DataColumn("主权项", typeof(string));
                DataColumn col12 = new DataColumn("国省代码", typeof(string));
                DataColumn col13 = new DataColumn("申请（专利权）人", typeof(string));
                DataColumn col14 = new DataColumn("发明（设计）人", typeof(string));
                DataColumn col15 = new DataColumn("优先权", typeof(string));
                DataColumn col16 = new DataColumn("地址", typeof(string));
                DataColumn col19 = new DataColumn("法律状态", typeof(string));

                
                dtresult.Columns.Add(col1);
                dtresult.Columns.Add(col2);
                dtresult.Columns.Add(col3);
                dtresult.Columns.Add(col4);
                dtresult.Columns.Add(col5);
                dtresult.Columns.Add(col6);
                dtresult.Columns.Add(col7);
                dtresult.Columns.Add(col8);
                dtresult.Columns.Add(col9);
                dtresult.Columns.Add(col10);
                dtresult.Columns.Add(col11);
                dtresult.Columns.Add(col12);
                dtresult.Columns.Add(col13);
                dtresult.Columns.Add(col14);
                dtresult.Columns.Add(col15);
                dtresult.Columns.Add(col16);
                dtresult.Columns.Add(col19);
                if (NodeId != "")
                {
                    DataColumn col17 = new DataColumn("标注内容", typeof(string));
                    DataColumn col18 = new DataColumn("标注时间", typeof(string));
                    dtresult.Columns.Add(col17);
                    dtresult.Columns.Add(col18);
                }



                foreach (xmlDataInfo currentXmlDataInfo in listXml)
                {
                    DataRow tmprow = dt.NewRow();
                    DataRow dbrow = dtresult.NewRow();
                    #region

                    if (strcolnames.Contains("申请号"))
                    {
                        dbrow["申请号"] = currentXmlDataInfo.StrApNo;
                    }
                    if (strcolnames.Contains("公开（公告）号"))
                    {
                        dbrow["公开（公告）号"] = currentXmlDataInfo.StrPnOrGn.Replace("[公告]", "").Replace("[公开]", "");
                    }
                    if (strcolnames.Contains("分类号"))
                    {
                        dbrow["分类号"] = currentXmlDataInfo.StrIpc.Replace(",", " ");
                    }
                    if (strcolnames.Contains("主分类号"))
                    {
                        dbrow["主分类号"] = currentXmlDataInfo.StrMainIPC.Replace(",", " ");
                    }
                    if (strcolnames.Contains("名称"))
                    {
                        dbrow["名称"] = currentXmlDataInfo.StrTitle.Replace(",", " ").Trim();
                    }
                    if (strcolnames.Contains("公开（公告）日"))
                    {
                        dbrow["公开（公告）日"] = ShowDate(currentXmlDataInfo.StrPdOrGd);
                    }
                    if (strcolnames.Contains("申请日"))
                    {
                        dbrow["申请日"] = ShowDate(currentXmlDataInfo.StrApDate);
                    }
                    //代理人
                    if (strcolnames.Contains("代理人"))
                    {
                        dbrow["代理人"] = currentXmlDataInfo.StrDaiLiRen.Replace(",", " ").Trim();
                    }
                    //专利代理机构               
                    if (strcolnames.Contains("专利代理机构"))
                    {
                        dbrow["专利代理机构"] = currentXmlDataInfo.StrAgency.Replace(",", " ").Trim();
                    }
                    if (strcolnames.Contains("摘要"))
                    {
                        dbrow["摘要"] = currentXmlDataInfo.StrAbstr.Replace(",", " ").Trim();
                    }
                    //主权利要求
                    if (strcolnames.Contains("主权项"))
                    {
                        dbrow["主权项"] = currentXmlDataInfo.StrClaim.Replace(",", " ").Trim();
                    }
                    //StrCountryCode              
                    //国省代码
                    if (strcolnames.Contains("国省代码"))
                    {
                        dbrow["国省代码"] = currentXmlDataInfo.StrCountryCode.Replace(",", " ").Trim();
                    }
                    //申请（专利权）人
                    if (strcolnames.Contains("申请（专利权）人"))
                    {
                        dbrow["申请（专利权）人"] = currentXmlDataInfo.StrApply.Replace(",", " ").Trim();
                    }
                    if (strcolnames.Contains("发明（设计）人"))
                    {
                        dbrow["发明（设计）人"] = currentXmlDataInfo.StrInventor.Replace(",", " ").Trim();
                    }
                    // 优先权
                    if (strcolnames.Contains("优先权"))
                    {
                        dbrow["优先权"] = currentXmlDataInfo.StrPri.Replace(",", " ").Trim();
                    }
                    //地址
                    if (strcolnames.Contains("地址"))
                    {
                        dbrow["地址"] = currentXmlDataInfo.StrShenQingRenDiZhi.Replace(",", " ").Trim();
                    }

                    //法律状态
                    if (strcolnames.Contains("法律状态"))
                    {
                        dbrow["法律状态"] = ss.getFaLvZhuangTaiSimple(currentXmlDataInfo.StrApNo.Replace(".", "")).Trim();
                    }
                    if (NodeId != "")
                    {
                        List<string> s = UserCollectsHelper.getNote(currentXmlDataInfo.StrSerialNo, Convert.ToInt32(NodeId));
                        dbrow["标注内容"] = s[0].Trim();
                        dbrow["标注时间"] = s[1].Trim();
                    }
                    #endregion

                    tmprow["pid"] = currentXmlDataInfo.StrSerialNo;
                    if (type.ToUpper() == "CN")
                    {
                        if (currentXmlDataInfo.ZhuanLiLeiXing == "3")
                        {
                            tmprow["ipc3"] = getstr(currentXmlDataInfo.StrMainIPC, 2);
                            tmprow["ipc4"] = getstr(currentXmlDataInfo.StrMainIPC, 5);
                            tmprow["ipc"] = getstr(formatipc(currentXmlDataInfo.StrMainIPC), 15);
                        }
                        else
                        {
                            tmprow["ipc1"] = getstr(currentXmlDataInfo.StrMainIPC, 1);
                            tmprow["ipc3"] = getstr(currentXmlDataInfo.StrMainIPC, 3);
                            tmprow["ipc4"] = getstr(currentXmlDataInfo.StrMainIPC, 4);
                            tmprow["ipc7"] = getstr(currentXmlDataInfo.StrMainIPC, 7);
                            tmprow["ipc"] = getstr(formatipc(currentXmlDataInfo.StrMainIPC), 15);
                        }
                        tmprow["type"] = type.ToUpper() + currentXmlDataInfo.ZhuanLiLeiXing;

                    }
                    else
                    {
                        tmprow["type"] = type.ToUpper();
                        tmprow["ipc1"] = getstr(currentXmlDataInfo.StrMainIPC, 1);
                        tmprow["ipc3"] = getstr(currentXmlDataInfo.StrMainIPC, 3);
                        tmprow["ipc4"] = getstr(currentXmlDataInfo.StrMainIPC, 4);
                        tmprow["ipc7"] = getstr(currentXmlDataInfo.StrMainIPC, 7);
                        tmprow["ipc"] = getstr(formatipc(currentXmlDataInfo.StrMainIPC), 15);
                    }
                    tmprow["UserName"] = UserName;
                    dt.Rows.Add(tmprow);
                    dtresult.Rows.Add(dbrow);
                }
                
                if (!UserDownLoadHelper.RecordDownload(dt))
                {
                    logger.Info("RecordDownload失败");
                    return null;
                }
                return dtresult;
            }
            catch( Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        [WebMethod]
        public static string ExportData(string CpicIds, string ColNames, string NodeId, string type, string FileType)
        {
            try
            {
                SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                CpicIds = CpicIds.Replace(",undefined,", ",");
                List<int> listNo = CpicIds.Trim(',').Split(',').ToList<string>().ConvertAll<int>(x => Convert.ToInt32(x));
                List<xmlDataInfo> listXml = search.GetResult(listNo, type.ToUpper());
                string name = GlobalUtility.TimeRandomName() + "." + FileType;
                string strFileName = "/Images/Export/Patent" + name;
                string fullpath = System.Web.HttpContext.Current.Server.MapPath(strFileName);
                if (!Directory.Exists(Path.GetDirectoryName(fullpath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
                }
                ExcelLib.NPOIHelper eh = new ExcelLib.NPOIHelper();
                fullpath = eh.DataTable2File(List2DataTable(listXml, ColNames, type, NodeId), fullpath, FileType);
                return name;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return "";
            }
        }

        [WebMethod]
        public static string ExportData2(string type, string db, string id, string ColNames, string beginNum, string endNum, string strSort, string FileType)
        {
            try
            {                
                int userid = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]);
                int bnum = Convert.ToInt32(beginNum) - 1;
                if (bnum < 0) bnum = 0;
                int endnum = Convert.ToInt32(endNum);
                int length = endnum - bnum;
                SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                List<string> strcolnames = ColNames.Split('|').ToList<string>();
                List<int> listNo;
                List<int> fResult = new List<int>();
                switch (type.ToUpper())
                {
                    case "FI":
                    case "CN":
                    case "EN":
                        SearchPattern sp = new SearchPattern();
                        sp.SearchNo = id;
                        sp.Pattern = "";
                        if (db.ToUpper() == "CN")
                        {
                            sp.DbType = SearchDbType.Cn;
                        }
                        else
                        {
                            sp.DbType = SearchDbType.DocDB;
                        }
                        sp.UserId = userid;
                        ResultServices res = new ResultServices();
                        fResult = res.GetResultList(sp, "");
                        break;
                    case "ZT":
                    case "QY":
                        fResult = ztHelper.GetResultList(id, db);
                        break;
                    case "CO":
                        fResult = UserCollectsHelper.GetResultList(db, Convert.ToInt32(id));
                        break;
                    case "YJ0":
                    case "YJ1":
                        fResult = ProYJDLL.YJDB.getYJItemByWID(Convert.ToInt32(id), Convert.ToInt16(type.Substring(2).ToString()));
                        break;
                }
                
                listNo = fResult.Skip(bnum).Take(length).ToList<int>();
                List<xmlDataInfo> listXml = search.GetResult(listNo, db.ToUpper());
                string name = GlobalUtility.TimeRandomName() + "." + FileType;
                string strFileName = "/Images/Export/Patent" + name;
                string fullpath = System.Web.HttpContext.Current.Server.MapPath(strFileName);
                if (!Directory.Exists(Path.GetDirectoryName(fullpath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
                }
                ExcelLib.NPOIHelper eh = new ExcelLib.NPOIHelper();
                
                fullpath = eh.DataTable2File(List2DataTable(listXml, ColNames, type, ""), fullpath, FileType);
                
                return name;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return "";
            }
        }
        protected static string ShowDate(string date)
        {

            string left = string.Empty;
            string right = string.Empty;
            if (date.IndexOf("[公告]") > 0 || date.IndexOf("[公开]") > 0)
            {
                left = date.Substring(0, 4);
                right = date.Substring(4);

            }
            else
            {
                right = date;
            }
            right = date.Replace("[公告]", "").Replace("[公开]", "");
            string strReturn = string.Empty;
            if (right.Length == 8)
            {
                right = date.Substring(0, 4) + "年" + right.Substring(4, 2) + "月" + right.Substring(6, 2) + "日";
            }
            strReturn = left + right;
            return strReturn;
        }

        private static string getstr(string s, int length)
        {
            if (s.Length >= length)
            {
                return s.Substring(0, length);
            }
            else
            {
                return s.PadRight(length, ' ');
            }
        }
        private static string formatipc(string ipc)
        {
            var ind = ipc.IndexOf('(');
            if (ind > 0)
            {
                ipc = ipc.Substring(0, ind).Trim();
            }
            return ipc;
        }

    }
}
