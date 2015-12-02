using System;
using System.Collections;
using System.Collections.Generic;
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
using System.IO;
using System.Text;
using Cpic.Cprs2010.Search.ResultData;
using TLC.BusinessLogicLayer;
using Patentquery.My;
using SearchInterface;
using System.Data.Linq;
using System.Text.RegularExpressions;
using ProXZQDLL;

public partial class My_EnPatentDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                {
                    string strFormatUrl = "<a href='frmDoSq.aspx?db=EN&Query=F XX ({0}/{1})' target='_blank'>{2}</a>";
                    //string strFormatUrl = "<span re='frmDoSq.aspx?db=EN&Query=F XX ({0}/{1})' >{2}</span>";
                    string strFoumatTrans = "<span>{0}</span> &nbsp;&nbsp;<a href='javascript:void(0);' onclick=\"transABS(this,'','EN')\"><img title='翻译' src='../images/Trans_20.jpg' /></a>";
                    //frmDoSq.aspx?db=EN&Query=F XX (20030623/AD)

                    SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                    xmlDataInfo currentXmlDataInfo = search.GetEnxmlDataInfo(Request.QueryString["Id"].Trim());

                    LiteralTitle.Text = string.IsNullOrEmpty(currentXmlDataInfo.StrTitle) ? "无" : string.Format(strFoumatTrans, currentXmlDataInfo.StrTitle);
                    LiteralApDate.Text = string.Format(strFormatUrl, Server.UrlEncode(frmPatDetails.EncodeDate(currentXmlDataInfo.StrApDate)),
                        "AD", currentXmlDataInfo.StrApDate);

                    LiteralInventor.Text = XmPatentComm.getSplitString(currentXmlDataInfo.StrInventor, "IN", "EN");
                    LiteralApply.Text = XmPatentComm.getSplitString(currentXmlDataInfo.StrApply, "PA", "EN");
                    LiteralSimilar.Text = FormateDisPlayTz(currentXmlDataInfo.StrPubNo, currentXmlDataInfo.CPIC); // ShowSimilar(currentXmlDataInfo.TongZu);

                    LiteralIpc.Text = XmPatentComm.getSplitString(currentXmlDataInfo.StrIpc, "IC", "EN");

                    //LiteralPubNo.Text = getDispLayValues(currentXmlDataInfo.StrPubNo, currentXmlDataInfo.StrEpoPubNo, currentXmlDataInfo.StrOriginalPubNo, "PN");
                    //LiteralApNo.Text = getDispLayValues(currentXmlDataInfo.StrDocdbApNo, currentXmlDataInfo.StrEpoApNo, currentXmlDataInfo.StrOriginalApNo, "AN");

                    LiteralPubNo.Text = getDispLayValues(currentXmlDataInfo.StrPubNo, currentXmlDataInfo.StrOriginalPubNo, "PN");
                    LiteralApNo.Text = getDispLayValues(currentXmlDataInfo.StrEpoApNo, currentXmlDataInfo.StrOriginalApNo, "AN");
                    btnActiveTab.Attributes.Add("apno", currentXmlDataInfo.StrEpoApNo);
                    LinkButtonDownload.ToolTip = currentXmlDataInfo.StrPubNo;

                    if (currentXmlDataInfo.StrPubDate.StartsWith("000") || currentXmlDataInfo.StrPubDate == "")
                    {
                        LiteralPubDate.Text = "";
                    }
                    else
                    {
                        LiteralPubDate.Text = string.Format(strFormatUrl, Server.UrlEncode(frmPatDetails.EncodeDate(currentXmlDataInfo.StrPubDate)),
                            "PD", currentXmlDataInfo.StrPubDate);
                    }

                    LiteralImageFt.Text = string.Format("<img id='ImageFt' src='../Images/loding_imgFt.gif' onload=\"resizeFt(this,'{0}')\" alt='摘要附图'/>", currentXmlDataInfo.StrFtUrl);

                    LiteralPri.Text = currentXmlDataInfo.StrPri;

                    litAbs.Text = string.IsNullOrEmpty(currentXmlDataInfo.StrAbstr) ? "" : string.Format(strFoumatTrans, currentXmlDataInfo.StrAbstr);

                    litRef.Text = currentXmlDataInfo.StrRefDoc;

                    litFmyAbs.Text = currentXmlDataInfo.StrAbsFmy;

                    litEcla.Text = currentXmlDataInfo.StrEcla;

                    btnActiveTab.ToolTip = currentXmlDataInfo.StrSerialNo;

                    //自定义标注
                    BindUserCollect(currentXmlDataInfo.StrSerialNo);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    private string getDispLayValues(string _strIdexNo, string _strOrgNo, string _strType)
    {
        string strRs = "";
        #region 只显示索引格格加原始
        try
        {

            //http://worldwide.espacenet.com/publicationDetails/biblio?DB=EPODOC&locale=cn_EP&FT=D&CC=EP&NR=2039599A3&KC=A3

            if (!string.IsNullOrEmpty(_strIdexNo))
            {
                if (_strType == "PN")
                {
                    strRs = _strIdexNo + string.Format("  [<a href='{0}' target='_blank'>欧局查看</a>]", ClsSearch.getEpoPatDetailUrl(_strIdexNo, ""));
                }
                else
                {
                    strRs = _strIdexNo + "  <span title='欧洲专利局加工整理的格式'>[欧局]</span>";
                }
            }

            if (!string.IsNullOrEmpty(_strOrgNo))
            {
                strRs += "<br/>" + _strOrgNo + "  <span title='申请国家的数据格式'>[原始]</span>";
            }
            //strRs = "<a href='http://www.epo.org/' target='_blank'>" + _strEpoNo + "</a>";

            if (strRs.StartsWith("<br/"))
            {
                strRs = strRs.Substring(5);
            }
        }
        catch (Exception ex)
        {
        }
        #endregion
        return strRs;

    }

    private string getDispLayValues(string _strDocdbNo, string _strEpoNo, string _strOrgNo, string _strType)
    {
        string strRs = "";

        #region 三种格式都显示
        if (!string.IsNullOrEmpty(_strDocdbNo))
        {
            strRs += "<br/>标准:" + _strDocdbNo;
        }
        if (!string.IsNullOrEmpty(_strEpoNo))
        {
            strRs += "<br/><a href='http://www.epo.org/' target='_blank'>欧局</a>:" + _strEpoNo;
        }
        if (!string.IsNullOrEmpty(_strOrgNo))
        {
            strRs += "<br/>原始:" + _strOrgNo;
        }
        //strRs = "<a href='http://www.epo.org/' target='_blank'>" + _strEpoNo + "</a>";

        if (!string.IsNullOrEmpty(strRs))
        {
            strRs = strRs.Substring(5);
        }
        return strRs;
        #endregion
    }

    /// <summary>
    /// 标注内容更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txNote = (TextBox)GridView1.Rows[e.RowIndex].Controls[0].Controls[1];

        string strUpSql = "update TLC_Collects set Note='{1}' where CollectId={0}";

        DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strUpSql, e.Keys[0], txNote.Text));

        ScriptManager.RegisterStartupScript(this, this.GetType(), "GridView1", "alert('修改成功!')", true);
    }

    protected void LinkButtonDownload_Click(object sender, EventArgs e)
    {
        String strFileName = LinkButtonDownload.ToolTip.Trim(); //"PatentDetails_" + DateTime.Today.ToString("yyyyMMdd");
        switch (hidActiveTabTi.Value)
        {
            case "著录项目信息"://著录信息
                Response.Redirect(string.Format("ptdownload.aspx?Id={0}&tp=DEN", Request.QueryString["Id"]));
                break;
            case "PDF全文"://PDF
                //Response.Redirect(search.getInfoByPatentID(Request.QueryString["Id"], "CN", "2"));
                break;
            case "法律状态"://法律状态     
                System.Net.WebClient MyWebClient = new System.Net.WebClient();
                MyWebClient.Encoding = System.Text.Encoding.UTF8;
                string strLegalUrl = string.Format("{0}comm/epo_legal.aspx?pubno={1}", XmPatentComm.strUrlDome, Cpic.Cprs2010.Cfg.UrlParameterCode_DE.DecryptionAll(Request.QueryString["Id"]));
                string strRs = MyWebClient.DownloadString(strLegalUrl);

                Page.Response.Clear();
                bool success = frmPatDetails.ResponseFile(Page.Request, Page.Response, strFileName + "_L.html", strRs, 1024000);
                if (success)
                {
                    //LiteralHint.Text = "成功";
                }
                break;
        }
    }

    /// <summary>
    /// TAb切换
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnActiveTab_Click(object sender, EventArgs e)
    {
        SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();

        switch (hidActiveTabTi.Value)
        {
            // <li><a href="#tabMianXml">著录项目信息</a></li>
            //<li><a href="#TabDegImgs">外观图形</a></li>
            //<li><a href="#DivtabPdf">全文PDF</a></li>
            //<li><a href="#divTabDes">说明书</a></li>
            //<li><a href="#divTabClams">权利要求</a></li>
            //<li><a href="#divTabLegal">法律状态</a></li>           
            case "全文PDF":
                if (LiteralPdf.Text == "Loading......")
                {
                    if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                    {
                        LiteralPdf.Text = "<div id='divPfpage'>Loading......</div>";
                        string strCprsPdfUrlPage = btnActiveTab.Attributes["apno"].StartsWith("CN") ? "EAP" + btnActiveTab.Attributes["apno"] : Request.QueryString["Id"].Trim();
                        //bns_xm/comm/;cprs2010/docdb/
                        strCprsPdfUrlPage = string.Format("http://202.106.92.181/bns_xm/comm/GetBns.aspx?PNo={0}&type=WD{1}", strCprsPdfUrlPage, getEpoPdfUrlParameter());
                        //strCprsPdfUrlPage = string.Format("http://211.160.117.105/bns_xm/comm/GetBns.aspx?PNo={0}&type=WD{1}", strCprsPdfUrlPage, getEpoPdfUrlParameter());

                        //strCprsPdfUrlPage = "http://pdfobject.com/examples/simplest-styled.html";
                        //epo pdf :http://worldwide.espacenet.com/maximizedOriginalDocument?flavour=maximizedPlainPage&locale=en_EP&FT=D&date=20070712&CC=US&NR=2007161985A1&KC=A1
                        LiteralPdf.Text = string.Format("<iframe id='irmPdf' src='{0}' style='z-index:0;' frameborder='0' width='100%' height='600'></iframe>", strCprsPdfUrlPage);
                    }
                }
                LinkButtonDownload.Visible = false;
                break;
            case "权利要求":
                if ( LiteralRights.Text == "Loading......" )
                {
                    if ( Request.QueryString["Id"] != null && Request.QueryString["Id"] != "" )
                    {
                        LiteralRights.Text = "暂无数据";
                    }
                }
                LinkButtonDownload.Visible = true;
                break;
            case "说明书":
                if ( LiteralBook.Text == "Loading......" )
                {
                    if ( Request.QueryString["Id"] != null && Request.QueryString["Id"] != "" )
                    {
                        LiteralBook.Text = "暂无数据";
                    }
                }
                LinkButtonDownload.Visible = true;
                break;
            case "法律状态":
                if (LiteralLeagl.Text == "Loading......")
                {
                    if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                    {
                        if (btnActiveTab.Attributes["apno"].StartsWith("CN"))
                        {
                            ////frmLawInfo.aspx?Idx=9FCA3CBA8BGA9AHB9DFA6DDA9EHD9DHE9IFG9GGF4DAA9HCE
                            LiteralLeagl.Text = string.Format("<iframe id='irmLagel' src='frmLawInfo.aspx?Idx={0}' style='z-index:0;' frameborder='0' width='100%' height='600'></iframe>",
                              Cpic.Cprs2010.Cfg.UrlParameterCode_DE.encrypt(Cpic.Cprs2010.Cfg.Data.cnDataService.FormatEpoToCNApNo(btnActiveTab.Attributes["apno"])));

                        }
                        else
                        {
                            LiteralLeagl.Text = string.Format("<iframe id='irmLagel' src='{0}comm/epo_legal.aspx?pubno={1}' style='z-index:0;' frameborder='0' width='100%' height='600'></iframe>",
                               XmPatentComm.strUrlDome, Cpic.Cprs2010.Cfg.UrlParameterCode_DE.DecryptionAll(Request.QueryString["Id"]));
                            LiteralLeagl.Text = XmPatentComm.getLegalWebUrl(Cpic.Cprs2010.Cfg.UrlParameterCode_DE.DecryptionAll(Request.QueryString["Id"])) + LiteralLeagl.Text;
                        }


                    }
                }
                LinkButtonDownload.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "FLZTXZ");
                break;
            case "引文信息":
                if (LiteralQuote_1.Text == "Loading......")
                {
                    if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                    {
                        //LiteralQuote.Text = "暂无数据";
                        LiteralQuote_1.Text = search.GetEnCitedWithSrepPhase(Request.QueryString["Id"].Trim(), "APP", "is").Replace (";", "<br />");
                    }
                }
                if (LiteralQuote_2.Text == "Loading......")
                {
                    if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                    {
                        //LiteralQuote.Text = "暂无数据";
                        LiteralQuote_2.Text = search.GetEnCitedWithSrepPhase(Request.QueryString["Id"].Trim(), "APP", "not").Replace(";", "<br />");
                    }
                }
                LinkButtonDownload.Visible = true;
                break;
        }
    }

    /// <summary>
    /// 自定义标注内容绑定
    /// </summary>
    /// <param name="_strPid"></param>
    private void BindUserCollect(string _strPid)
    {
        try
        {
            string strSql = "select a.CollectId,a.AlbumId, b.Title as floder,a.Note,a.NoteDate from TLC_Collects a, TLC_Albums b where a.Pid={0} and a.AlbumId=b.AlbumId  and a.UserId={1} and a.NoteDate<>'' and a.[Type]='EN'";

            //string strPid = Request.QueryString["PID"].Trim(); //8779247
            GridView1.DataSource = DBA.SqlDbAccess.GetDataTable(CommandType.Text, string.Format(strSql, _strPid, Convert.ToInt32(Session["UserID"])));
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// 同族显示格式化
    /// </summary>
    /// <param name="tongZu"></param>
    /// <returns></returns>
    public string ShowSimilar(string tongZu)
    {
        string strReturn = string.Empty;
        string[] arraySimilar = tongZu.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        strReturn += "[" + arraySimilar.Length.ToString() + "]";
        foreach (string pubNo in arraySimilar)
        {
            strReturn += string.Format("<a href='EnPatentDetails.aspx?Id={0}' target='_blank'>{1}</a>  ",
                Cpic.Cprs2010.Cfg.UrlParameterCode_DE.encrypt(pubNo), pubNo);
        }
        return strReturn;
    }

    /// <summary>
    /// espacenet PDF URL 参数
    /// </summary>
    /// <returns></returns>
    private string getEpoPdfUrlParameter()
    {
        string strRs = "";
        try
        {
            //    src="http://worldwide.espacenet.com/espacenetImage.jpg?flavour=firstPageClipping&locale=en_EP&FT=D&date=20070712&CC=US&NR=2007161985A1&KC=A1"
            //epo pdf :http://worldwide.espacenet.com/maximizedOriginalDocument?flavour=maximizedPlainPage&locale=en_EP&FT=D&date=20070712&CC=US&NR=2007161985A1&KC=A1
            string strFuUrl = LiteralImageFt.Text.Trim();

            Match rsMat = Regex.Match(strFuUrl, "flavour\\=firstPageClipping&locale=en_EP&FT=D(.*)\"");
            if (rsMat.Success)
            {
                strRs = "&" + rsMat.Groups[1].Value.Trim(); ;
            }

        }
        catch (Exception ex)
        {
        }
        return strRs;
    }

    public string FormateDisPlayTz(string strPubNo, string CPIC)
    {
        string strRs = "";
        int nTopN = 50;
        int nCount = 0;
        try
        {
            ResultDataManagerDataContext db = new ResultDataManagerDataContext();
            Table<DocdbDocInfo> tbDocdbInfo = db.DocdbDocInfo;
            var result = from item in tbDocdbInfo
                         where CPIC != "0" && item.CPIC.Equals(CPIC) && item.PubID != strPubNo
                         select item.PubID;


            nCount = result.Count();

            strRs += "[" + nCount.ToString() + "]&nbsp;";

            if (nTopN < nCount)
            {
                result = result.Take(nTopN);
            }
            foreach (var pub in result)
            {
                strRs += string.Format("&nbsp;<a class='patItem' href='EnPatentDetails.aspx?Id={0}' target='_blank'>{1}</a>",
                    Cpic.Cprs2010.Cfg.UrlParameterCode_DE.encrypt(pub), pub);
            }

            if (nTopN < nCount)
            {
                strRs += string.Format("&nbsp;&nbsp;<a class='int' href='#' onclick=\"showFamily('{1}','{2}')\">更多[{0}]</a>", nCount - nTopN, strPubNo, CPIC);
            }
        }
        catch (Exception ex)
        {
        }
        return strRs;
    }
}
