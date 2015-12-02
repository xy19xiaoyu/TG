using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using System.Text;
using Cpic.Cprs2010.Search.ResultData;
using System.Data;
using System.Xml;
using ProXZQDLL;
using SearchInterface;

namespace Patentquery.My
{
    public partial class frmPatDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                    {
                        string strFormatUrl = "<a href='frmDoSq.aspx?db=CN&Query=F XX ({0}/{1})' target='_blank'>{2}</a>";
                        //string strFormatUrl = "<span re='frmDoSq.aspx?db=CN&Query=F XX ({0}/{1})' >{2}</span>";
                        string strFoumatTrans = "<span>{0}</span> &nbsp;&nbsp;<a href='javascript:void(0);' onclick=\"transABS(this,'','CN')\"><img title='翻译' src='../images/Trans_20.jpg' /></a>";

                        //frmDoSq.aspx?db=CN&Query=F XX (20030623/AD)
                        SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                        xmlDataInfo currentXmlDataInfo = search.GetCnxmlDataInfo(Request.QueryString["Id"].Trim());

                        LiteralTitle.Text = string.Format(strFoumatTrans, currentXmlDataInfo.StrTitle);
                        LiteralApDate.Text = string.Format(strFormatUrl, Server.UrlEncode(EncodeDate(currentXmlDataInfo.StrApDate)), "AD", currentXmlDataInfo.StrApDate);
                        LiteralApNo.Text = currentXmlDataInfo.StrApNo;
                        LiteralInventor.Text = XmPatentComm.getSplitString(currentXmlDataInfo.StrInventor, "IN", "CN");
                        LiteralApply.Text = XmPatentComm.getSplitString(currentXmlDataInfo.StrApply, "PA", "CN");
                        LiteralCountryCode.Text = currentXmlDataInfo.StrCountryCode;
                        LiteralAdds.Text = currentXmlDataInfo.StrShenQingRenDiZhi;
                        LiteralPubNo.Text = currentXmlDataInfo.StrPubNo;
                        if (currentXmlDataInfo.StrPubDate.StartsWith("000") || currentXmlDataInfo.StrPubDate == "")
                        {
                            LiteralPubDate.Text = currentXmlDataInfo.StrPubDate;
                        }
                        else
                        {
                            LiteralPubDate.Text = string.Format(strFormatUrl, Server.UrlEncode(EncodeDate(currentXmlDataInfo.StrPubDate)), "PD", currentXmlDataInfo.StrPubDate);
                        }
                        LiteralAnnNo.Text = currentXmlDataInfo.StrAnnNo;

                        if (currentXmlDataInfo.StrAnnDate.StartsWith("000") || currentXmlDataInfo.StrAnnDate == "")
                        {
                            LiteralAnnDate.Text = currentXmlDataInfo.StrAnnDate;
                        }
                        else
                        {
                            LiteralAnnDate.Text = string.Format(strFormatUrl, Server.UrlEncode(EncodeDate(currentXmlDataInfo.StrAnnDate)), "GD", currentXmlDataInfo.StrAnnDate);
                        }

                        LiteralAgency.Text = currentXmlDataInfo.StrAgency;
                        LiteralAgent.Text = currentXmlDataInfo.StrDaiLiRen;

                        try
                        {
                            LiteralMainIpc.Text = string.Format(strFormatUrl, Server.UrlEncode(Util.FormatUtil.FormatIPC(currentXmlDataInfo.StrMainIPC)), "MC", currentXmlDataInfo.StrMainIPC);
                        }
                        catch (Exception exx)
                        {
                            LiteralMainIpc.Text = currentXmlDataInfo.StrMainIPC;
                        }


                        LiteralIpc.Text = XmPatentComm.getSplitString(currentXmlDataInfo.StrIpc, "IC", "CN");
                        LiteralBrief.Text = string.Format(strFoumatTrans, currentXmlDataInfo.StrAbstr);
                        ltraPro.Text = currentXmlDataInfo.StrPri;

                        LiteralImageFt.Text = string.Format("<img id='ImageFt' src='../Images/loding_imgFt.gif' onload=\"resizeFt(this,'{0}')\" alt='摘要附图'/>", currentXmlDataInfo.StrFtUrl);

                        LabelClaim.Text = string.IsNullOrEmpty(currentXmlDataInfo.StrClaim) ? "无" : string.Format(strFoumatTrans, currentXmlDataInfo.StrClaim);

                        if (currentXmlDataInfo.ZhuanLiLeiXing.Equals("3"))
                        {
                            //labAbsJYSM.Text = "简要说明：";
                        }

                        btnActiveTab.ToolTip = currentXmlDataInfo.CPIC;
                        //标引
                        //LabelAuto.Text = search.GetBiaoYin(Request.QueryString["Id"]);

                        //BindUserCollect(currentXmlDataInfo.CPIC);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        //private void BindUserCollect(string _strPid)
        //{
        //    try
        //    {
        //        string strSql = "select a.CollectId,a.AlbumId, b.Title as floder,a.Note,a.NoteDate from TLC_Collects a, TLC_Albums b where a.Pid={0} and a.AlbumId=b.AlbumId  and a.UserId={1} and a.NoteDate<>'' and a.[Type]='CN'";

        //        //string strPid = Request.QueryString["PID"].Trim(); //8779247
        //        GridView1.DataSource = DBA.SqlDbAccess.GetDataTable(CommandType.Text, string.Format(strSql, _strPid, Convert.ToInt32(Session["UserID"])));
        //        GridView1.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

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
                case "外观图形":
                    if (LiteralPictureList.Text == "Loading......")
                    {
                        if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                        {
                            LiteralPictureList.Text = string.Format("<iframe id='irmWgIms' src='frmDesignImgs.aspx?Id={0}' frameborder='0' width='100%' height='660'></iframe>", Request.QueryString["Id"]);
                        }
                    }
                    LinkButtonDownload.Visible = false;
                    break;
                case "全文PDF":
                    if (LiteralPdf.Text == "Loading......")
                    {
                        if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                        {
                            #region closed.....
                            //string strPdfUrls = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "2");
                            //string[] strArryPdfUrls = strPdfUrls.Split('|');
                            //StringBuilder strBud = new StringBuilder();
                            //for (int i = 0; i < strArryPdfUrls.Length; i++)
                            //{
                            //    if (strArryPdfUrls[i].Contains("0ACN"))
                            //    {
                            //        string pdfInfo = "公开文本";
                            //        //strBud.Append(string.Format("<a id='hrf{2}' href='{1}' target='_blank' onclick='return LoadPdf(this)'>公开文本[{0}]</a>&nbsp;&nbsp;",
                            //        //    bnsFiles[i].Substring(bnsFiles[i].LastIndexOf('/') + 1, 34), bnsFiles[i], i + 1));
                            //        strBud.Append(string.Format("<a id='hrf{0}' href='{1}' target='_blank' onclick='return LoadPdf(this)'>" + pdfInfo + "</a>&nbsp;&nbsp;", i + 1, strArryPdfUrls[i]));
                            //    }
                            //    else
                            //    {
                            //        string pdfInfo = "公告文本";
                            //        //strBud.Append(string.Format("<a id='hrf{2}' href='{1}' target='_blank' onclick='return LoadPdf(this)'>公告文本[{0}]</a>&nbsp;&nbsp;",
                            //        //    bnsFiles[i].Substring(bnsFiles[i].LastIndexOf('/') + 1, 34), bnsFiles[i], i + 1));
                            //        strBud.Append(string.Format("<a id='hrf{0}' href='{1}' target='_blank' onclick='return LoadPdf(this)'>" + pdfInfo + "</a>&nbsp;&nbsp;", i + 1, strArryPdfUrls[i]));
                            //    }
                            //}
                            //LiteralPdf.Text = string.Format("", "");
                            //LiteralPdf.Text = "<object classid=\"clsid:CA8A9780-280D-11CF-A24D-444553540000\" width=\"900\" height=\"600\" border=\"0\"><param name=\"_Version\" value=\"65539\"><param name=\"_ExtentX\" value=\"20108\"><param name=\"_ExtentY\" value=\"10866\"><param name=\"_StockProps\" value=\"0\"><param name=\"SRC\" value=\"" + search.getInfoByPatentID(Request.QueryString["Id"], "CN", "2") + "\"><object align=\"center\" data=\"" + search.getInfoByPatentID(Request.QueryString["Id"], "CN", "2") + "\" type=\"application/pdf\" width=\"900\" height=\"600\"></object></object>";

                            //<form name="form1" method="post" action="GetBns.aspx?PNo=APP6CCA6DDA9HBA9GFF9EFB9GEB9ICB9EDB9GHH9IGG3BAA5CBA&amp;type=CN" id="form1">
                            //string strCprsPdfUrls = "<form id='frmPdf_1' method='post' action='http://202.106.92.181/cprs2010/docdb/GetBns.aspx?PNo=APP{0}&type=CN'></form><script type='text/javascript'>alter(document.getElementById('frmPdf_1'))</script>";
                            //LiteralPdf.Text = string.Format(strCprsPdfUrls, Request.QueryString["Id"].Trim());
                            //LiteralPdf.Text = "<div><form id='aspnetForm' name='aspnetForm' method='post' action='http://202.106.92.181/cprs2010/docdb/GetBns.aspx?PNo=APP&type=CN'></form></div>";

                            //LiteralPdf.Mode = LiteralMode.Encode;
                            #endregion

                            LiteralPdf.Text = "<div id='divPfpage'>Loading......</div>";
                            string strCprsPdfUrlPage = string.Format("http://211.160.117.105/bns/comm/GetBns.aspx?PNo=APP{0}&type=CN", Request.QueryString["Id"].Trim());
                            //string strCprsPdfUrlPage = string.Format("http://202.106.92.181/cprs2010/docdb/GetBns.aspx?PNo=APP{0}&type=CN", Request.QueryString["Id"].Trim());


                            //System.Net.WebClient MyWebClient = new System.Net.WebClient();
                            //MyWebClient.Encoding = System.Text.Encoding.UTF8;
                            //string strRs = MyWebClient.DownloadString(strCprsPdfUrlPage);
                            ////LiteralPdf.Text = strRs;
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "LiteralPdf", "LoadPdfFile('divPfpage','" + strCprsPdfUrlPage + "')", true);
                            //HiddenField1Pdf.Value = strRs;
                            //strCprsPdfUrlPage = "http://pdfobject.com/examples/simplest-styled.html";
                            LiteralPdf.Text = string.Format("<iframe id='irmPdf' src='{0}' style='z-index:0;' frameborder='0' width='100%' height='600'></iframe>", strCprsPdfUrlPage);
                        }
                    }
                    LinkButtonDownload.Visible = false;
                    break;
                case "权利要求":
                    if (LiteralRights.Text == "Loading......")
                    {
                        if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                        {
                            //LiteralRights.Text = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "0");
                            string xmltext = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "0");

                            if (xmltext.StartsWith("ERROR："))
                            {
                                LiteralRights.Text = xmltext;
                            }
                            else
                            {
                                MSXML2.DOMDocument30Class xml = new MSXML2.DOMDocument30Class();
                                MSXML2.DOMDocument30Class xslt = new MSXML2.DOMDocument30Class();
                                //xmltext=xmltext.Replace("<![CDATA[<math>", "<math>").Replace("</math>]]>", "</math>");
                                xml.loadXML(xmltext);

                                XmlDocument doc = new XmlDocument();
                                doc.Load(Server.MapPath("~") + "\\newcss\\claims.xsl");
                                string xsltext = doc.InnerXml;

                                xslt.loadXML(xsltext);
                                LiteralRights.Text = xml.transformNode(xslt).Replace("charset=UTF-16", "charset=GB2312");
                            }
                        }
                    }
                    LinkButtonDownload.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "QWXZ");
                    break;
                case "说明书":
                    if (LiteralBook.Text == "Loading......")
                    {
                        if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
                        {
                            LiteralBook.Text = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "1");
                        }
                    }
                    LinkButtonDownload.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "QWXZ");
                    break;
                case "法律状态":
                    if (LiteralLeagl.Text == "Loading......")
                    {

                        SearchInterface.WSFLZT.CnLegalStatus[] currentDataSet = search.getFalvZhuangTai(Request.QueryString["Id"]);
                        if (currentDataSet != null)
                        {
                            GridViewLegal.DataSource = currentDataSet;
                            GridViewLegal.DataBind();
                        }
                    }
                    LiteralLeagl.Text = "";
                    GridViewLegal.Visible = true;
                    LinkButtonDownload.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "FLZTXZ");
                    break;
                case "引文信息":
                    if (LiteralQuote.Text == "Loading......")
                    {
                        if (LiteralAnnNo.Text != null && LiteralAnnNo.Text != "")
                        {
                            string yzInf = search.getYZInf(LiteralAnnNo.Text);
                            if (!yzInf.Equals(""))
                            {
                                if (yzInf.IndexOf("@@@") > 0)
                                {
                                    LiteralQuote.Text = yzInf.Replace("@@@", "<br />");
                                }
                                else
                                {
                                    LiteralQuote.Text = yzInf;
                                }
                            }
                            else
                            {
                                LiteralQuote.Text = "暂无数据";
                            }
                            //LiteralQuote.Text = yzInf == "" ? "暂无数据" : yzInf;
                        }
                        else
                        {
                            LiteralQuote.Text = "暂无数据";
                        }
                    }
                    LinkButtonDownload.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// 日期格式化 yyyymmdd
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string EncodeDate(string date)
        {
            string strReturn = string.Empty;
            try
            {
                if (date == "" || date.StartsWith("000"))
                {
                    strReturn = "";
                }
                else
                {
                    if (date.IndexOf("]") > 0)
                    {
                        date = date.Substring(date.IndexOf("]") + 1);
                    }
                    date = date.Replace("年", "-");
                    date = date.Replace("月", "-");
                    date = date.Replace("日", "");
                    strReturn = Convert.ToDateTime(date).ToString("yyyyMMdd");
                }
            }
            catch (Exception ex)
            {
                strReturn = date;
            }
            return strReturn;
        }


        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    TextBox txNote = (TextBox)GridView1.Rows[e.RowIndex].Controls[0].Controls[1];

        //    string strUpSql = "update TLC_Collects set Note='{1}' where CollectId={0}";

        //    DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strUpSql, e.Keys[0], txNote.Text));

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "GridView1", "alert('修改成功!')", true);
        //}

        protected void GridViewLegal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewLegal.PageIndex = e.NewPageIndex;
            GridViewLegal.DataBind();
        }

        protected void GridViewLegal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //各行加上鼠标经过效果
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");
                //自定义按钮加上CommandArgument以支持按钮事件
            }
        }


        protected void LinkButtonDownload_Click(object sender, EventArgs e)
        {
            String strFileName = LiteralApNo.Text;  // "PatentDetails_" + DateTime.Today.ToString("yyyyMMdd");

            string strComma = ",";
            string strNewLine = Environment.NewLine;

            StringBuilder sbContent = new StringBuilder();
            bool success = false;

            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            switch (hidActiveTabTi.Value)
            {
                case "著录项目信息"://著录信息
                    Response.Redirect(string.Format("ptdownload.aspx?Id={0}&tp=cn", Request.QueryString["Id"]));
                    break;
                case "PDF全文"://PDF
                    //Response.Redirect(search.getInfoByPatentID(Request.QueryString["Id"], "CN", "2"));
                    break;
                case "权利要求"://代码化全文

                    sbContent.Append(LiteralRights.Text);

                    Page.Response.Clear();
                    success = ResponseFile(Page.Request, Page.Response, strFileName + "_C.html", sbContent.ToString(), 1024000);
                    if (success)
                    {
                        //LiteralHint.Text = "成功";
                    }
                    break;
                case "说明书"://代码化全文
                    sbContent.Append(LiteralBook.Text);
                    Page.Response.Clear();
                    success = ResponseFile(Page.Request, Page.Response, strFileName + "_D.html", sbContent.ToString(), 1024000);
                    if (success)
                    {
                        //LiteralHint.Text = "成功";
                    }
                    break;
                case "法律状态"://法律状态
                    sbContent.Append("申请号");
                    sbContent.Append(strComma);
                    sbContent.Append("法律状态公告日");
                    sbContent.Append(strComma);
                    sbContent.Append("法律状态");
                    sbContent.Append(strComma);
                    sbContent.Append("详细信息");
                    SearchInterface.WSFLZT.CnLegalStatus[] currentDataSet = search.getFalvZhuangTai(Request.QueryString["Id"]);
                    if (currentDataSet != null)
                    {
                        foreach (var item in currentDataSet)
                        {
                            sbContent.Append(strNewLine);
                            sbContent.Append("'" + item.SHENQINGH);
                            sbContent.Append(strComma);
                            sbContent.Append(item.LegalDate);
                            sbContent.Append(strComma);
                            sbContent.Append(item.LegalStatusInfo);
                            sbContent.Append(strComma);
                            sbContent.Append(item.LegalStatusInfo + ";" + item.DETAIL);
                        }
                    }

                    Page.Response.Clear();
                    success = ResponseFile(Page.Request, Page.Response, strFileName + "_L.csv", sbContent.ToString(), 1024000);
                    if (success)
                    {
                        //LiteralHint.Text = "成功";
                    }
                    break;
            }
        }



        // 输出string，提供下载
        // 输入参数 _Request: Page.Request对象，  _Response: Page.Response对象， _fileName: 下载文件名， strContent: 文件内容， _speed 每秒允许下载的字节数
        // 返回是否成功
        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string strContent, long _speed)
        {
            MemoryStream currentStream = new MemoryStream(Encoding.Default.GetBytes(strContent));
            BinaryReader br = new BinaryReader(currentStream);
            _Response.AddHeader("Accept-Ranges", "bytes");
            _Response.Buffer = false;
            long fileLength = currentStream.Length;
            long startBytes = 0;

            int pack = 10240; //10K bytes
            int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
            if (_Request.Headers["Range"] != null)
            {
                _Response.StatusCode = 206;
                string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                startBytes = Convert.ToInt64(range[1]);
            }
            _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
            if (startBytes != 0)
            {
                _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
            }
            _Response.AddHeader("Connection", "Keep-Alive");
            _Response.ContentType = "application/octet-stream";
            _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

            br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
            int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;
            for (int i = 0; i < maxCount; i++)
            {
                if (_Response.IsClientConnected)
                {
                    _Response.BinaryWrite(br.ReadBytes(pack));
                }
                else
                {
                    i = maxCount;
                }
            }
            br.Close();
            currentStream.Close();
            return true;
        }

        [WebMethod]
        public static string getAutoBiaoYin(string strANX)
        {
            string strRs = "";
            try
            {
                SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                strRs = search.GetBiaoYin(strANX);
            }
            catch (Exception ex)
            {
                strRs = "未加载标引内容";
            }
            return strRs;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>window.open('SplitScreenDisplay.aspx?Id=" + Request.QueryString["Id"].Trim() + "')</script>");
        }
    }
}