using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using TLC;
using System.Web.Services;
using Cpic.Cprs2010.Cfg;
using TLC.BusinessLogicLayer;
using System.Data;
using SearchInterface;
using Cpic.Cprs2010.Search.ResultData;
using System.Net;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Data.Linq;

namespace Patentquery.Comm
{
    public partial class getSimilar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request["appno"] == null)
            //{
            //    return;
            //}
            //if (string.IsNullOrEmpty(Request["appno"]))
            //{
            //    return;
            //}
            //string strappno = Request["appno"].ToString();
            //strappno = strappno.Replace(".", "");
            //if (strappno.Length == 13)
            //{
            //    strappno = strappno.Substring(0, 12);
            //}
            //else if (strappno.Length == 9)
            //{
            //    strappno = strappno.Substring(1, 8);
            //}
            //HttpWebRequest res = (HttpWebRequest)HttpWebRequest.Create("http://202.106.92.137/cprs2010/presearch/appnosearch.aspx?appno=" + strappno);
            //WebResponse sp = res.GetResponse();
            //Stream s = sp.GetResponseStream();

            //Encoding cod = Encoding.GetEncoding("utf-8");
            //StreamReader sr = new StreamReader(s, cod);
            //string data = sr.ReadToEnd();            
            //if (data == "0")
            //{
            //    data = "{\"total\":\"0\",\"rows\":\"[]\"}";
            //}
            //Response.Write(data);
        }
        [WebMethod]
        public static string getSimilars(string appno, string pageNumber, string pageSize)
        {
            int pindex = Convert.ToInt32(pageNumber);
            int Psize = Convert.ToInt32(pageSize);
            string strappno = appno;
            strappno = strappno.Replace(".", "");
            if (strappno.Length == 13)
            {
                strappno = strappno.Substring(0, 12);
            }
            else if (strappno.Length == 9)
            {
                strappno = strappno.Substring(0, 8);
            }
            HttpWebRequest res = (HttpWebRequest)HttpWebRequest.Create("http://202.106.92.137/cprs2010/presearch/appnosearch.aspx?appno=" + strappno);
            WebResponse sp = res.GetResponse();
            Stream s = sp.GetResponseStream();

            Encoding cod = Encoding.GetEncoding("utf-8");
            StreamReader sr = new StreamReader(s, cod);
            string data = sr.ReadToEnd();

            if (data == "0")
            {
                data = "{\"total\":\"0\",\"rows\":\"[]\"}";
            }
            else
            {
                string left;
                string right;
                int index = data.IndexOf("[");
                int index2 = data.IndexOf("]");
                if (index > 0)
                {
                    left = data.Substring(0, index + 1);
                    right = data.Substring(index + 1, index2 - index - 1);
                    List<string> lstitem = Regex.Split(right, "},{").ToList<string>();
                    List<string> x = (from y in lstitem
                                      select y).Skip(Psize * (pindex - 1)).Take(Psize).ToList<string>();
                    string strsult = "";
                    foreach (var item in x)
                    {
                        strsult = strsult + "{" + item.Trim('{').Trim('}') + "},";
                    }
                    strsult = strsult.Trim(',');
                    strsult += "]}";
                    data = left + strsult;
                }
            }


            return data;
        }
        [WebMethod]
        public static string getFamily(string appno, string pageNumber, string pageSize, string CPIC)
        {
            int pindex = Convert.ToInt32(pageNumber);
            int Psize = Convert.ToInt32(pageSize);
            string strappno = appno;
            int intid = Convert.ToInt32(CPIC);
            ResultDataManagerDataContext db = new ResultDataManagerDataContext();
            Table<Cpic.Cprs2010.Search.ResultData.DocdbDocInfo> tbDocdbInfo = db.DocdbDocInfo;

            var tmp = (from item in tbDocdbInfo
                       where intid != 0 && item.CPIC.Equals(intid)
                       select item).Skip(Psize * (pindex - 1)).Take(Psize);
            List<enfml> result = (from item in tmp
                                  select new enfml
                                  {
                                      ANX = string.Empty,
                                      apno = item.PubID,
                                      title = item.Title
                                  }).ToList<enfml>();
            int count = (from item in tbDocdbInfo
                         where intid != 0 && item.CPIC.Equals(intid)
                         select item).Count();

            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();

            foreach (var x in result)
            {

                x.ANX = UrlParameterCode_DE.encrypt(x.apno);

                try
                {
                    if (string.IsNullOrEmpty(x.title.Trim()))
                    {
                        x.title = search.GetEnxmlDataInfo(x.ANX).StrTitle;
                    }
                }
                catch (Exception ex) { }
            }

            return JsonHelper.ListToJson<enfml>(result, "rows", count.ToString());

        }
    }

    public class enfml
    {
        private string _ANX;

        public string ANX
        {
            get { return _ANX; }
            set { _ANX = value; }
        }
        private string _apno;

        public string apno
        {
            get { return _apno; }
            set { _apno = value; }
        }
        private string _title;

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
    }

}