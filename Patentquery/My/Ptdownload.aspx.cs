using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Cpic.Cprs2010.Cfg.Data;
using System.IO;
using System.Xml;
using ParseXml;
using DBA;
using RTFReplace;


public partial class My_Ptdownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strApNo = Request.QueryString["Id"].Trim();
        string doctype = Request.QueryString["tp"].Trim();


        SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();

        Cpic.Cprs2010.Search.ResultData.xmlDataInfo currentXmlDataInfo = null;
        string templatefilePath = "";
        String temfile = "";
        FileInfo DownloadFile = null;
        String guid = "";
        RTFChange rtf = new RTFReplace.RTFChange();

        temfile = System.Web.HttpContext.Current.Server.MapPath("") + "\\PatentDetails_" + Guid.NewGuid().ToString() + ".Doc";

        string strDowFileName = "file";

        switch (doctype.ToUpper())
        {
            case "CN":
                #region cnDown
                templatefilePath = System.Web.HttpContext.Current.Server.MapPath("cn_template.rtf");

                FileInfo templateFile = new FileInfo(templatefilePath);

                templateFile.CopyTo(temfile, true);
                DownloadFile = new FileInfo(temfile);
                currentXmlDataInfo = search.GetCnxmlDataInfo(strApNo);


                rtf.RTFileChange(temfile, "%applyNo%", currentXmlDataInfo.StrApNo);
                rtf.RTFileChange(temfile, "%applyDate %", currentXmlDataInfo.StrApDate);
                rtf.RTFileChange(temfile, "%pubNo%", currentXmlDataInfo.StrPubNo);
                rtf.RTFileChange(temfile, "%pubDate%", currentXmlDataInfo.StrPubDate);
                rtf.RTFileChange(temfile, "%announceNo%", currentXmlDataInfo.StrAnnNo);
                rtf.RTFileChange(temfile, "%announceDate%", currentXmlDataInfo.StrAnnDate);
                rtf.RTFileChange(temfile, "%grountDate%", currentXmlDataInfo.StrAnnDate);
                rtf.RTFileChange(temfile, "%grantpubDate%", currentXmlDataInfo.StrAnnDate);
                rtf.RTFileChange(temfile, "%city%", currentXmlDataInfo.StrCountryCode);
                rtf.RTFileChange(temfile, "%field%", currentXmlDataInfo.StrFiled);
                rtf.RTFileChange(temfile, "%agency%", currentXmlDataInfo.StrAgency);
                rtf.RTFileChange(temfile, "%agent%", currentXmlDataInfo.StrDaiLiRen);
                rtf.RTFileChange(temfile, "%agencyAddress%", currentXmlDataInfo.StrAgency_Addres);
                rtf.RTFileChange(temfile, "%apply%", currentXmlDataInfo.StrApply);
                rtf.RTFileChange(temfile, "%address%", currentXmlDataInfo.StrShenQingRenDiZhi);                  //tbd
                rtf.RTFileChange(temfile, "%code%", "");                //tdb
                rtf.RTFileChange(temfile, "%inventor%", currentXmlDataInfo.StrInventor);

                rtf.RTFileChange(temfile, "%ipc%", currentXmlDataInfo.StrIpc);
                rtf.RTFileChange(temfile, "%ecla%", "");
                rtf.RTFileChange(temfile, "%ucla%", "");
                rtf.RTFileChange(temfile, "%title%", currentXmlDataInfo.StrTitle);
                rtf.RTFileChange(temfile, "%abs%", currentXmlDataInfo.StrAbstr);
                rtf.RTFileChange(temfile, "%claim%", currentXmlDataInfo.StrClaim);
                rtf.RTFileChange(temfile, "%pri%", currentXmlDataInfo.StrPri);

                rtf.RTFileChange(temfile, "%url%", Request.UrlReferrer.ToString());

                #endregion
                strDowFileName = currentXmlDataInfo.StrApNo;
                break;
            case "DEN":
                #region DENDown
                templatefilePath = System.Web.HttpContext.Current.Server.MapPath("DEN_template.rtf");

                templateFile = new FileInfo(templatefilePath);

                templateFile.CopyTo(temfile, true);
                DownloadFile = new FileInfo(temfile);
                currentXmlDataInfo = search.GetEnxmlDataInfo(strApNo);

                rtf.RTFileChange(temfile, "%applyNo%", currentXmlDataInfo.StrApNo);
                rtf.RTFileChange(temfile, "%applyDate %", currentXmlDataInfo.StrApDate);
                rtf.RTFileChange(temfile, "%pubNo%", currentXmlDataInfo.StrPubNo);
                rtf.RTFileChange(temfile, "%pubDate%", currentXmlDataInfo.StrPubDate);

                rtf.RTFileChange(temfile, "%apply%", currentXmlDataInfo.StrApply);
                rtf.RTFileChange(temfile, "%inventor%", currentXmlDataInfo.StrInventor);
                rtf.RTFileChange(temfile, "%ipc%", currentXmlDataInfo.StrIpc);

                rtf.RTFileChange(temfile, "%title%", currentXmlDataInfo.StrTitle);
                rtf.RTFileChange(temfile, "%abs%", currentXmlDataInfo.StrAbstr);
                rtf.RTFileChange(temfile, "%refdoc%", currentXmlDataInfo.StrRefDoc);
                rtf.RTFileChange(temfile, "%pri%", currentXmlDataInfo.StrPri);
                rtf.RTFileChange(temfile, "%url%", Request.UrlReferrer.ToString());

                #endregion
                strDowFileName = currentXmlDataInfo.StrPubNo;
                break;
        }

        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.ClearHeaders();
        System.Web.HttpContext.Current.Response.Buffer = false;
        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
        System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(strDowFileName + ".doc", System.Text.Encoding.UTF8));
        System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
        System.Web.HttpContext.Current.Response.WriteFile(temfile);
        System.Web.HttpContext.Current.Response.Flush();
        System.Web.HttpContext.Current.Response.End();
    }
}

