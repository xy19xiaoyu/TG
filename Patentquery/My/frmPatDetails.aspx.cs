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

            //switch (hidActiveTabTi.Value)
            //{               
            //    case "外观图形":
            //        if (LiteralPictureList.Text == "Loading......")
            //        {
            //            if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
            //            {
            //                LiteralPictureList.Text = string.Format("<iframe id='irmWgIms' src='frmDesignImgs.aspx?Id={0}' frameborder='0' width='100%' height='660'></iframe>", Request.QueryString["Id"]);
            //            }
            //        }
            //        LinkButtonDownload.Visible = false;
            //        break;
            //    case "全文PDF":
            //        if (LiteralPdf.Text == "Loading......")
            //        {
            //            if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
            //            {
            //                LiteralPdf.Text = "<div id='divPfpage'>Loading......</div>";
            //                string strCprsPdfUrlPage = string.Format("http://211.160.117.105/bns/comm/GetBns.aspx?PNo=APP{0}&type=CN", Request.QueryString["Id"].Trim());                           
            //                LiteralPdf.Text = string.Format("<iframe id='irmPdf' src='{0}' style='z-index:0;' frameborder='0' width='100%' height='600'></iframe>", strCprsPdfUrlPage);
            //            }
            //        }
            //        LinkButtonDownload.Visible = false;
            //        break;
            //    case "权利要求":
            //        if (LiteralRights.Text == "Loading......")
            //        {
            //            if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
            //            {
            //                string xmltext = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "0");

            //                if (xmltext.StartsWith("ERROR："))
            //                {
            //                    LiteralRights.Text = xmltext;
            //                }
            //                else
            //                {
            //                    MSXML2.DOMDocument30Class xml = new MSXML2.DOMDocument30Class();
            //                    MSXML2.DOMDocument30Class xslt = new MSXML2.DOMDocument30Class();
            //                    //xmltext=xmltext.Replace("<![CDATA[<math>", "<math>").Replace("</math>]]>", "</math>");
            //                    xml.loadXML(xmltext);

            //                    XmlDocument doc = new XmlDocument();
            //                    doc.Load(Server.MapPath("~") + "\\newcss\\claims.xsl");
            //                    string xsltext = doc.InnerXml;

            //                    xslt.loadXML(xsltext);
            //                    LiteralRights.Text = xml.transformNode(xslt).Replace("charset=UTF-16", "charset=GB2312");
            //                }
            //            }
            //        }
            //        LinkButtonDownload.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "QWXZ");
            //        break;
            //    case "说明书":
            //        if (LiteralBook.Text == "Loading......")
            //        {
            //            if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
            //            {
            //                LiteralBook.Text = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "1");
            //            }
            //        }
            //        LinkButtonDownload.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "QWXZ");
            //        break;
            //    case "法律状态":
            //        if (LiteralLeagl.Text == "Loading......")
            //        {

            //            SearchInterface.WSFLZT.CnLegalStatus[] currentDataSet = search.getFalvZhuangTai(Request.QueryString["Id"]);
            //            if (currentDataSet != null)
            //            {
            //                GridViewLegal.DataSource = currentDataSet;
            //                GridViewLegal.DataBind();
            //            }
            //        }
            //        LiteralLeagl.Text = "";
            //        GridViewLegal.Visible = true;
            //        LinkButtonDownload.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "FLZTXZ");
            //        break;
            //    case "引文信息":
            //        if (LiteralQuote.Text == "Loading......")
            //        {
            //            if (LiteralAnnNo.Text != null && LiteralAnnNo.Text != "")
            //            {
            //                string yzInf = search.getYZInf(LiteralAnnNo.Text);
            //                if (!yzInf.Equals(""))
            //                {
            //                    if (yzInf.IndexOf("@@@") > 0)
            //                    {
            //                        LiteralQuote.Text = yzInf.Replace("@@@", "<br />");
            //                    }
            //                    else
            //                    {
            //                        LiteralQuote.Text = yzInf;
            //                    }
            //                }
            //                else
            //                {
            //                    LiteralQuote.Text = "暂无数据";
            //                }
            //                //LiteralQuote.Text = yzInf == "" ? "暂无数据" : yzInf;
            //            }
            //            else
            //            {
            //                LiteralQuote.Text = "暂无数据";
            //            }
            //        }
            //        LinkButtonDownload.Visible = true;
            //        break;
            //}
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

        protected void LinkButtonDownload_Click(object sender, EventArgs e)
        {
            //String strFileName = LiteralApNo.Text;  // "PatentDetails_" + DateTime.Today.ToString("yyyyMMdd");

            //string strComma = ",";
            //string strNewLine = Environment.NewLine;

            //StringBuilder sbContent = new StringBuilder();
            //bool success = false;

            //SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            //switch (hidActiveTabTi.Value)
            //{
            //    case "著录项目信息"://著录信息
            //        Response.Redirect(string.Format("ptdownload.aspx?Id={0}&tp=cn", Request.QueryString["Id"]));
            //        break;
            //    case "PDF全文"://PDF
            //        //Response.Redirect(search.getInfoByPatentID(Request.QueryString["Id"], "CN", "2"));
            //        break;
            //    case "权利要求"://代码化全文

            //        sbContent.Append(LiteralRights.Text);

            //        Page.Response.Clear();
            //        success = ResponseFile(Page.Request, Page.Response, strFileName + "_C.html", sbContent.ToString(), 1024000);
            //        if (success)
            //        {
            //            //LiteralHint.Text = "成功";
            //        }
            //        break;
            //    case "说明书"://代码化全文
            //        sbContent.Append(LiteralBook.Text);
            //        Page.Response.Clear();
            //        success = ResponseFile(Page.Request, Page.Response, strFileName + "_D.html", sbContent.ToString(), 1024000);
            //        if (success)
            //        {
            //            //LiteralHint.Text = "成功";
            //        }
            //        break;
            //    case "法律状态"://法律状态
            //        sbContent.Append("申请号");
            //        sbContent.Append(strComma);
            //        sbContent.Append("法律状态公告日");
            //        sbContent.Append(strComma);
            //        sbContent.Append("法律状态");
            //        sbContent.Append(strComma);
            //        sbContent.Append("详细信息");
            //        SearchInterface.WSFLZT.CnLegalStatus[] currentDataSet = search.getFalvZhuangTai(Request.QueryString["Id"]);
            //        if (currentDataSet != null)
            //        {
            //            foreach (var item in currentDataSet)
            //            {
            //                sbContent.Append(strNewLine);
            //                sbContent.Append("'" + item.SHENQINGH);
            //                sbContent.Append(strComma);
            //                sbContent.Append(item.LegalDate);
            //                sbContent.Append(strComma);
            //                sbContent.Append(item.LegalStatusInfo);
            //                sbContent.Append(strComma);
            //                sbContent.Append(item.LegalStatusInfo + ";" + item.DETAIL);
            //            }
            //        }

            //        Page.Response.Clear();
            //        success = ResponseFile(Page.Request, Page.Response, strFileName + "_L.csv", sbContent.ToString(), 1024000);
            //        if (success)
            //        {
            //            //LiteralHint.Text = "成功";
            //        }
            //        break;
            //}
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