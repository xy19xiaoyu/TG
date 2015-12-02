using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cpic.Cprs2010.Search;
using TLC;
using System.IO;

namespace Patentquery.Comm
{
    public partial class autost : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            string webPath = "W";  //外网
            string strTjUrl = getTjRul();

            if (strTjUrl.Contains("192.168"))
            {
                webPath = "N";
            }

            string url = strTjUrl + "PatentAnalyze/pages/reportQuery.page?fileName={0}&fileRowNum={1}&userId={2}&userCheckCode={3}&webPath=" + webPath;

            int CreateUserId = Convert.ToInt32(Session["Userid"].ToString());
            string db = (Request["db"] == null ? string.Empty : Request["db"]);
            string type = (Request["type"] == null ? string.Empty : Request["type"]);
            string Nm = (Request["Nm"] == null ? string.Empty : Request["Nm"]);
            type = type.ToUpper();
            string filename = "";
            string SearchNo = string.Empty;
            string id = (Request["id"] == null ? "0" : Request["id"].ToString());

            //判断检索类型 1:检索   2:专题库   3:收藏
            ResultServices s = new Cpic.Cprs2010.Search.ResultServices();
            SearchDbType t;
            List<int> lstid = new List<int>();
            switch (type)
            {
                case "CN":
                    type = "Cn";
                    SearchNo = id.ToString().PadLeft(3, '0');
                    break;
                case "EN":
                    type = "DocDB";
                    SearchNo = id.ToString().PadLeft(3, '0');
                    break;
                case "ZT":
                case "QY":
                    lstid = ztHelper.GetResultList(id, db);
                    SearchNo = id.ToString();
                    break;
                case "CO":
                    lstid = UserCollectsHelper.GetResultList(db, Convert.ToInt32(id));
                    SearchNo = id.ToString();
                    break;
                case "YJ0":
                    lstid = ProYJDLL.YJDB.getYJItemByWID(Convert.ToInt32(id), 0);
                    SearchNo = id.ToString();
                    type = type.Substring(0, 2);
                    break;
                case "YJ1":
                    lstid = ProYJDLL.YJDB.getYJItemByWID(Convert.ToInt32(id), 1);
                    SearchNo = id.ToString();
                    type = type.Substring(0, 2);
                    return;
            }
            t = (SearchDbType)Enum.Parse(typeof(SearchDbType), type);
            filename = s.getResultFilePathOnly(new SearchPattern() { DbType = t, UserId = CreateUserId, SearchNo = SearchNo });
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
            }
            if (db.ToUpper() == "EN")
            {
                url = getTjRul() + "PatentAnalyze/pages/reportWorldQuery.page?fileName={0}&fileRowNum={1}&userId={2}&userCheckCode={3}&webPath=" + webPath;
            }
            switch (type)
            {
                case "ZT":
                case "QY":
                case "CO":
                case "YJ":
                    try
                    {
                        if (File.Exists(filename)) File.Delete(filename);
                        using (FileStream fsw = new FileStream(filename, FileMode.CreateNew, FileAccess.Write))
                        {
                            foreach (var x in lstid)
                            {
                                byte[] by = BitConverter.GetBytes(x);
                                fsw.Write(by, 0, by.Length);
                            }
                        }
                        Nm = lstid.Count.ToString();
                    }
                    catch (Exception)
                    {

                    }
                    break;
            }

            string strurl = string.Format(url, filename.Replace(CprsConfig.CPRS2010UserPath, ""), Nm, CreateUserId, "");
            ///Response.Redirect(strurl);
            ifst.Attributes["src"] = strurl;
        }

        private string getTjRul()
        {
            string strRs = "http://115.238.84.42:8082/";
            try
            {

                string strUrl = Request.Url.ToString().ToUpper();

                foreach (string strItem in SearchInterface.XmPatentComm.strDicTJSeverURL.Keys)
                {
                    if (strUrl.Contains(strItem))
                    {
                        strRs = "http://" + SearchInterface.XmPatentComm.strDicTJSeverURL[strItem] + "/";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return strRs;
        }
    }
}