using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cpic.Cprs2010.Search.ResultData;
using System.Web.Services;
using System.Data;
using ProYJDLL;
using log4net;
using System.Reflection;
using System.Text;
namespace Patentquery.Comm
{
    public partial class yujing : System.Web.UI.Page
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        [WebMethod]
        public static string GetPageList(string KeyWord, string KeyValue, string C_TYPE, string type, string country, string PageIndex, string PageSize)
        {
            KeyValue = HttpContext.Current.Server.HtmlEncode(KeyValue).Replace("'", "''");
            int count = 0;
            int userid = int.Parse(System.Web.HttpContext.Current.Session["UserID"].ToString());
            List<yjitem> res = YJDB.getYJ(KeyWord, KeyValue, Convert.ToInt32(C_TYPE), 1, country, Convert.ToInt32(PageIndex), Convert.ToInt32(PageSize), out count, userid);
            //List<yjitem> res = YJDB.getYJ("0","", Convert.ToInt32(1), 1, "CN");
            return JsonHelper.ListToJson<yjitem>(res, "rows", count.ToString());
        }
        [WebMethod]
        public static string GetPageListhis(string CID, string PageIndex, string PageSize)
        {
            int count = 0;
            List<yjitem> res = ProYJDLL.YJDB.getYJHis(int.Parse(CID), Convert.ToInt32(PageIndex), Convert.ToInt32(PageSize), out count);
            return JsonHelper.ListToJson<yjitem>(res, "rows", count.ToString());
        }
        [WebMethod]
        public static string addyj(string KeyWord, string KeyValue, string C_TYPE, string type, string country, string des, string yjname, string week, string sheng, string GuoJia, string ShiJie, string Status, string Hangye, string TopKeyValue)
        {
            sheng = sheng.Substring(0, 2);
            GuoJia = GuoJia.Substring(0, 2);
            
            //if (C_TYPE == "4" && country == "EN")
            //{
            //    KeyValue = KeyValue.Substring(1, 2);
            //}

             KeyValue = HttpContext.Current.Server.UrlDecode(KeyValue).Replace("'", "''").Trim(';');
             yjname = HttpContext.Current.Server.UrlDecode(yjname).Replace("'", "''");
             des = HttpContext.Current.Server.UrlDecode(des).Replace("'", "''");
             Hangye = HttpContext.Current.Server.UrlDecode(Hangye).Replace("'", "''").Trim(';');

             TopKeyValue = HttpContext.Current.Server.UrlDecode(TopKeyValue).Replace("'", "''").Trim(';');
            string result = "succ";

            string patent = "";
            string S_NAME = "";


            try
            {
                int userid = int.Parse(System.Web.HttpContext.Current.Session["UserID"].ToString());
                int C_ID = ProYJDLL.YJDB.YjInsert(userid, yjname, int.Parse(week), des, int.Parse(C_TYPE), country,int.Parse(Status));
                
                //string[] itemvalue = Hangye.Split(';');
                int yjisparent = 1;
               
                //判断是否有子项
                if (!string.IsNullOrEmpty(TopKeyValue))
                {
                    string[] item = TopKeyValue.Split(';');
                    for (int i = 0; i < item.Length; i++)
                    {
                        patent = ProYJDLL.YJDB.YjInsertItem(C_ID, KeyValue.Trim(), sheng, GuoJia, ShiJie, 0, C_TYPE, country, Hangye.ToString(), item[i].ToString());
                        S_NAME += patent + "+";
                    }
                    
                }
                S_NAME = S_NAME.TrimEnd('+');
                if (C_TYPE.Substring(1, 1) == "6")
                    S_NAME = TopKeyValue;
                if (C_TYPE.Substring(0, 1) == "6")
                {
                    //patent = "F XX (" + S_NAME + "/PA)*(US/CO)";
                }
                //总项检索
                patent = ProYJDLL.YJDB.YjInsertItem(C_ID, KeyValue.Trim(), sheng, GuoJia, ShiJie, yjisparent, C_TYPE, country, Hangye.ToString(), S_NAME);
                //S_NAME = "F XX " + S_NAME.TrimEnd('+');

                
                //{
                //    // patent = "F XX (" + S_NAME + "/PA)*(US/CO)";

                //}

                //总项检索  
                //ProYJDLL.YJDB.YjInsertItem(C_ID, KeyValue, sheng, GuoJia,ShiJie, 1, S_NAME, country,Hangye);
                PatentWarnning.YJini.SearchYJini(C_ID, 0);

            }
            catch (Exception ex)
            {
                result = "failed";
            }
            return result;
        }
        
        [WebMethod]
        public static string edityj(string KeyWord, string KeyValue, string C_TYPE, string type, string country, string des, string yjname, string week, string CID, string sheng, string GuoJia, string ShiJie, string Status, string Hangye, string TopKeyValue)
        {
            sheng = sheng.Substring(0, 2);
            GuoJia = GuoJia.Substring(0, 2);
            

            KeyValue = HttpContext.Current.Server.UrlDecode(KeyValue).Replace("'", "''").Trim(';');
            yjname = HttpContext.Current.Server.UrlDecode(yjname).Replace("'", "''");
            des = HttpContext.Current.Server.UrlDecode(des).Replace("'", "''");

            Hangye = HttpContext.Current.Server.UrlDecode(Hangye).Replace("'", "''").Trim(';');
            TopKeyValue = HttpContext.Current.Server.UrlDecode(TopKeyValue).Replace("'", "''").Trim(';');


            string result = "succ";

            string patent = "";
            string S_NAME = "";

            try
            {
                int userid = int.Parse(System.Web.HttpContext.Current.Session["UserID"].ToString());
                ProYJDLL.YJDB.YjUpdate(int.Parse(CID), yjname, int.Parse(week), des, int.Parse(C_TYPE), country);
                ProYJDLL.YJDB.YjDeleteItem(int.Parse(CID));
                //string[] item = KeyValue.Split(';');
                int yjisparent = 1;
                //判断是否有子项
                
                
                
                //判断是否有子项
                if (!string.IsNullOrEmpty(TopKeyValue))
                {
                    string[] item = TopKeyValue.Split(';');
                    for (int i = 0; i < item.Length; i++)
                    {
                        patent = ProYJDLL.YJDB.YjInsertItem(int.Parse(CID), KeyValue.Trim(), sheng, GuoJia, ShiJie, 0, C_TYPE, country, Hangye.ToString(), item[i].ToString());
                        S_NAME += patent + "+";
                    }
                }
                S_NAME = S_NAME.TrimEnd('+');
                if (C_TYPE.Substring(1, 1) == "6")
                    S_NAME = TopKeyValue;
                //S_NAME = "F XX " + S_NAME.TrimEnd('+');
                //patent = ProYJDLL.YJDB.YjInsertItem(int.Parse(CID), KeyValue, sheng, GuoJia,ShiJie, 1, S_NAME, country,"");
                patent = ProYJDLL.YJDB.YjInsertItem(int.Parse(CID), KeyValue.Trim(), sheng, GuoJia, ShiJie, yjisparent, C_TYPE, country, Hangye.ToString(), S_NAME);
                PatentWarnning.YJini.SearchYJini(int.Parse(CID), 0);
            }
            catch (Exception ex)
            {
                result = "failed";
            }
            return result;
        }
        [WebMethod]
        public static string delYJ(string cids)
        {
            string result = "succ";
            cids = cids.Trim(',');
            try
            {
                int userid = int.Parse(System.Web.HttpContext.Current.Session["UserID"].ToString());
                string[] ids = cids.Split(',');
                foreach (string id in ids)
                {
                    int CID = int.Parse(id);
                    ProYJDLL.YJDB.YjDelete(CID);
                    ProYJDLL.YJDB.YjDeleteItemAll(CID);
                }
            }
            catch (Exception ex)
            {
                result = "failed";
            }
            return result;
        }
        [WebMethod]
        public static string handupdata(string C_ID)
        {
            string result = "succ";
            try
            {
                //List<C_W_SECARCH> lst = ProYJDLL.YJDB.getYJItemByCIDAll(int.Parse(C_ID));
                //ProYJDLL.YJDB.YjDeleteItemAll(int.Parse(C_ID));
                //ProYJDLL.YJDB.InsertYJItem(lst);

                PatentWarnning.YJini.SearchYJini(Convert.ToInt32(C_ID), 1);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = "failed";
            }
            return result;
        }
        [WebMethod]
        public static string getHot(string C_TYPE, string country)
        {
            DataTable res = ProYJDLL.YJDB.getReDianYuJing(Convert.ToInt32(C_TYPE), country);
            return JsonHelper.DatatTableToJson1(res, "rows", res.Rows.Count);
        }

        [WebMethod]
        public static string updateStatus(string C_ID, string STATUS)
        {
            string result = "succ";
            try
            {
                //List<C_W_SECARCH> lst = ProYJDLL.YJDB.getYJItemByCIDAll(int.Parse(C_ID));
                //ProYJDLL.YJDB.YjDeleteItemAll(int.Parse(C_ID));
                ProYJDLL.YJDB.YjUpdate(int.Parse(C_ID), int.Parse(STATUS));

                //PatentWarnning.YJini.SearchYJini(Convert.ToInt32(C_ID), 1);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = "failed";
            }
            return result;
        }

        [WebMethod]
        public static string GetYjByID(string CID)
        {
            int C_ID = int.Parse(CID);
            List<C_W_SECARCH> res = YJDB.getYJItem(C_ID);
            StringBuilder sb = new StringBuilder();
            sb.Append("[{");
            string sname = "";
            string nid = "";
            foreach (C_W_SECARCH cw in res)
            {
                sname += cw.S_NAME + ";";
                nid = cw.NID;
            }

            sb.Append(string.Format("\"sname\":\"{1}\",\"nid\":\"{2}\"", "{", sname, nid));            
            sb.Append("}]");
            return sb.ToString();
        }
    }
}