using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text.RegularExpressions;
//using Cpic.Cprs2010.Search;
//using Cpic.Cprs2010.Search.SearchManager;
//using Cpic.Cprs2010.User;
//using log4net;
//using Cpic.Cprs2010.Cfg;
using System.Reflection;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Cpic.Cprs2010.Search;
using Cpic.Cprs2010.Cfg;

public partial class SSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static IEnumerable tableSearch(string strSearchQuery, string NodeId,string type,string stype)
    {
        string strResutlMsg = "";
        try
        {

           // string strQuery = System.Web.HttpUtility.UrlDecode(strSearchQuery.Trim());
            string strQuery =strSearchQuery.Trim();
            //检索
            int intCount = 0;
            int userid = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]);
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            string strHits = search.GetSearchDataHits(SearchInterface.XmPatentComm.strWebSearchGroupName,strQuery, userid, type.ToUpper());
            string strNo = string.Empty;

            if (strHits == "ERROR")
            {
                return "请求错误";
            }

            if (strHits.IndexOf("(") >= 0 && strHits.IndexOf(")") > 0)
            {
                strNo = strHits.Substring(strHits.IndexOf("(") + 1, strHits.IndexOf(")") - strHits.IndexOf("(") - 1);
            }
            if (strHits.IndexOf("<hits:") >= 0 && strHits.IndexOf(">") > 0)
            {
                intCount = Convert.ToInt32(strHits.Substring(strHits.IndexOf("<hits:") + 6, strHits.IndexOf(">") - strHits.IndexOf("<hits:") - 6).Trim());
            }

            if (intCount == 0) return "0";

            SearchPattern sp = new SearchPattern();
            sp.SearchNo = strNo;
            sp.Pattern = strQuery;
            if (type.ToUpper() == "CN")
            {
                sp.DbType = SearchDbType.Cn;
            }
            else
            {
                sp.DbType = SearchDbType.DocDB;
            }
            sp.UserId = userid;
            strResutlMsg = Merge(sp, NodeId, type, stype);

        }
        catch (Exception ex)
        {
            strResutlMsg = "请求错误";

        }
        return strResutlMsg;

    }


    [WebMethod]
    public static string Search(string strSearchQuery, string type)
    {
        string strResutlMsg = "";
        try
        {

            string strQuery = System.Web.HttpUtility.UrlDecode(strSearchQuery.Trim());
            //检索
            int intCount = 0;
            int userid = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]);
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            string strHits = search.GetSearchDataHits(SearchInterface.XmPatentComm.strWebSearchGroupName, strQuery, userid, type.ToUpper());
            string strNo = string.Empty;

            if (strHits == "ERROR")
            {
                return "请求错误";
            }
            if (strHits.IndexOf("(") >= 0 && strHits.IndexOf(")") > 0)
            {
                strNo = strHits.Substring(strHits.IndexOf("(") + 1, strHits.IndexOf(")") - strHits.IndexOf("(") - 1);
            }
            if (strHits.IndexOf("<hits:") >= 0 && strHits.IndexOf(">") > 0)
            {
                intCount = Convert.ToInt32(strHits.Substring(strHits.IndexOf("<hits:") + 6, strHits.IndexOf(">") - strHits.IndexOf("<hits:") - 6).Trim());
            }

            return intCount.ToString();

            

        }
        catch (Exception ex)
        {
            strResutlMsg = "请求错误";

        }
        return strResutlMsg;

    }
    private static string Merge(SearchPattern sp, string NodeId, string type, string stype)
    {

        ResultServices res = new ResultServices();
        List<int> fResult = res.GetResultList(sp, "");
        List<int> dbResult = ztHelper.GetResultList(NodeId,type);
        List<int> sresult ;
        if (stype == "1")
        {
            sresult = dbResult.Intersect(fResult).ToList<int>();
        }
        else
        {
            sresult = dbResult.Except(fResult).ToList<int>();
        }
        //结果文件绝对目录  
        string file = res.getResultFilePath(sp).Replace(sp.SearchNo+ ".cnp","998.cnp");
        if (File.Exists(file)) File.Delete(file);

        using (FileStream fs = new System.IO.FileStream(file, FileMode.CreateNew))
        {
            foreach (var id in sresult)
            {
                byte[] fmlNo = BitConverter.GetBytes(id);
                fs.Write(fmlNo, 0, fmlNo.Length);
            }
        }
        return sresult.Count().ToString();
    }
}

