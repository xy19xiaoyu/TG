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
using TLC.BusinessLogicLayer;
using ProXZQDLL;


public partial class My_LawQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //FLZT		法律状态检索
            //ZLQLZYJS		专利权利转移检索
            //ZLZYBQJS		专利质押保全检索
            //ZLSSXKJS		专利实施许可检索

            //<li><a href="#TabDegImgs">专利权利转移检索</a></li>
            //<li><a href="#DivtabPdf">专利质押保全检索</a></li>
            //<li><a href="#divTabDes">专利实施许可检索</a></li>


            TabZlqlzy.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "ZLQLZYJS");
            TabZlzybq.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "ZLZYBQJS");
            TabZlssxk.Visible = UserRight.getVisibleRight(Session["UserID"].ToString(), "ZLSSXKJS");

            LitTabsLi.Text += TabZlqlzy.Visible ? "</li><li><a href='#TabZlqlzy'>专利权利转移检索</a>" : "";
            LitTabsLi.Text += TabZlzybq.Visible ? "</li><li><a href='#TabZlzybq'>专利质押保全检索</a>" : "";
            LitTabsLi.Text += TabZlssxk.Visible ? "</li><li><a href='#TabZlssxk'>专利实施许可检索</a>" : "";
        }
    }

    /// <summary>
    /// 中国专利法律状态检索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnChaXunFL_Click(object sender, EventArgs e)
    {
        Patentquery.WSFLZT.Service ss = new Patentquery.WSFLZT.Service();
        Patentquery.WSFLZT.ResultInfo result = new Patentquery.WSFLZT.ResultInfo();
        int UserID = Convert.ToInt32(Session["UserID"]);

        string AppNo = txtAppNoFL.Text.ToString().Trim().ToUpper().Replace("CN", "").Replace(".", "");
        if (AppNo.Length > 8)
        {
            if (AppNo.Substring(0, 1) == "0")
            {
                if (AppNo.Length == 9)
                {
                    AppNo = AppNo.Substring(0, 8);
                }
            }

            if (AppNo.Substring(0, 1) == "2")
            {
                if (AppNo.Length == 13)
                {
                    AppNo = AppNo.Substring(0, 12);
                }
            }
            if (AppNo.Substring(0, 1) == "9" || AppNo.Substring(0, 1) == "8")
            {
                if (AppNo.Length == 9)
                {
                    AppNo = AppNo.Substring(0, 8);
                }
            }
        }


        txtAppNoFL.Text = AppNo;

        result = ss.QueryFaLvZT(txtAppNoFL.Text.ToString().Trim(), txtGongGaoR.Text.ToString().Trim(), txtFaLvZT.Text.ToString().Trim(), txtZhuanLiQR.Text.ToString().Trim(), UserID);

        int hitCount = result.HitCount;

        Patentquery.WSFLZT.SearchPattern strPatern = result.SearchPattern;
        string SearchNo = strPatern.SearchNo;

        string strURL = "LawQueryResult.aspx?type=falv&hitCount=" + hitCount + "&SearchNo=" + SearchNo + "&";

        strURL += getURL("AppNo", txtAppNoFL);
        strURL += getURL("GongGaoR", txtGongGaoR);
        strURL += getURL("FaLvZT", txtFaLvZT);
        strURL += getURL("ZhuanLiQR", txtZhuanLiQR);


        strURL = strURL.TrimEnd('&');

        if (!strURL.Contains("&"))
        {
            return;
        }

        Response.Redirect(strURL);
    }

    /// <summary>
    /// 重至
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnChongZhiFL_Click(object sender, EventArgs e)
    {
        txtClear(txtAppNoFL);
        txtClear(txtGongGaoR);
        txtClear(txtFaLvZT);
        txtClear(txtZhuanLiQR);
    }


    /// <summary>
    /// 专利权利转移检索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnZhuanYi_Click(object sender, EventArgs e)
    {
        //--zhuanYi
        //select top 10 ipc,DengJiSXR,BianGengQQLR,BianGengHQLR,BianGengQDZ,BianGengHDZ from CnLegal012  

        getFormatRQ(txtShengXiaoR);

        string strURL = "LawQueryResult.aspx?type=zhuanyi&";
        strURL += getURL("AppNo", txtAppNo);
        strURL += getURL("IPC", txtIPC);

        strURL += getURL("MC", txtMingChengZY);
        strURL += getURL("ZY", txtZhaiYaoZY);
        strURL += getURL("ZQX", txtZhuQuanXiangZY);

        strURL += getURL("DengJiSXR", txtShengXiaoR);
        strURL += getURL("BianGengQQLR", txtBianGengQQLR);
        strURL += getURL("BianGengHQLR", txtBianGengHQLR);
        strURL += getURL("BianGengQDZ", txtBianGengQDZ);
        strURL += getURL("BianGengHDZ", txtBianGengHDZ);

        strURL = strURL.TrimEnd('&');

        if (!strURL.Contains("&"))
        {
            return;
        }
        Response.Redirect(strURL);


    }
    protected void btnZhuanYiCZ_Click(object sender, EventArgs e)
    {
        txtClear(txtAppNo);
        txtClear(txtIPC);

        txtClear(txtMingChengZY);
        txtClear(txtZhaiYaoZY);
        txtClear(txtZhuQuanXiangZY);

        txtClear(txtShengXiaoR);
        txtClear(txtBianGengQQLR);
        txtClear(txtBianGengHQLR);
        txtClear(txtBianGengQDZ);
        txtClear(txtBianGengHDZ);
    }
    /// <summary>
    /// 专利质押保全检索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBaoQuan_Click(object sender, EventArgs e)
    {
        //zhiYa
        //select top 10 ipc,HeTongZT,DengJiSXR,BianGengR,JieChuR,DengJiH,CHuZhiR,ZhiQuanR  from CnLegal015 where shenqingh like '%2%' and legaldate like '%%'
        getFormatRQ(txtShengXiaoRBaoQuan);
        getFormatRQ(txtBianGengR);
        getFormatRQ(txtJieChuR);
        string strURL = "LawQueryResult.aspx?type=zhiya&";

        strURL += getURL("AppNo", txtAppNoZhiYa);
        strURL += getURL("IPC", txtIPCZhiYa);

        strURL += getURL("MC", txtMingChengBQ);
        strURL += getURL("ZY", txtZhaiYaoBQ);
        strURL += getURL("ZQX", txtZhuQuanXiangBQ);

        strURL += getURLCHK("HeTongZT", chkHeTongZT);
        strURL += getURL("DengJiSXR", txtShengXiaoRBaoQuan);
        strURL += getURL("BianGengR", txtBianGengR);
        strURL += getURL("JieChuR", txtJieChuR);
        strURL += getURL("DengJiH", txtDengJiH);
        strURL += getURL("CHuZhiR", txtChuZhiR);
        strURL += getURL("ZhiQuanR", txtZhiQuanR);

        strURL = strURL.TrimEnd('&');

        if (!strURL.Contains("&"))
        {
            return;
        }
        Response.Redirect(strURL);
    }


    /// <summary>
    /// 保全重置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBaoQuanCZ_Click(object sender, EventArgs e)
    {
        txtClear(txtAppNoZhiYa);
        txtClear(txtIPCZhiYa);

        txtClear(txtMingChengBQ);
        txtClear(txtZhaiYaoBQ);
        txtClear(txtZhuQuanXiangBQ);

        txtClear(txtShengXiaoRBaoQuan);
        txtClear(txtBianGengR);
        txtClear(txtJieChuR);
        txtClear(txtDengJiH);
        txtClear(txtChuZhiR);
        txtClear(txtZhiQuanR);
    }

    protected void btnXuKe_Click(object sender, EventArgs e)
    {
        getFormatRQ(txtBeiAnR);
        getFormatRQ(txtBianGengR);
        getFormatRQ(txtJieChuRiXuKe);
        //ShiSHiXuKe
        //select top 10 *,ipc,XuKeZL,HeTongZT,BeiAnRQ,BianGengR,JIeChuR,HeTongBAH,RangYuR,SHouRangR  from CnLegal014

        string strURL = "LawQueryResult.aspx?type=xuke&";
        strURL += getURL("AppNo", txtAppNoXuKe);
        strURL += getURL("IPC", txtIPCXuKe);

        strURL += getURL("MC", txtMingChengXK);
        strURL += getURL("ZY", txtZhaiYaoXK);
        strURL += getURL("ZQX", txtZhuQuanXiangXK);

        strURL += getURLDDL("XuKeZL", ddlXuKeZL);
        strURL += getURLCHK("HeTongZT", chkHeTongBAJD);
        strURL += getURL("BeiAnRQ", txtBeiAnR);
        strURL += getURL("BianGengR", txtBianGengRiXuKe);
        strURL += getURL("JIeChuR", txtJieChuRiXuKe);
        strURL += getURL("HeTongBAH", txtHeTongBAH);
        strURL += getURL("RangYuR", txtRangYuR);
        strURL += getURL("ShouRangR", txtShouRangR);


        strURL = strURL.TrimEnd('&');

        if (!strURL.Contains("&"))
        {
            return;
        }
        Response.Redirect(strURL);
    }
    protected void btnXuKeCZ_Click(object sender, EventArgs e)
    {
        txtClear(txtAppNoXuKe);
        txtClear(txtIPCXuKe);

        txtClear(txtMingChengXK);
        txtClear(txtZhaiYaoXK);
        txtClear(txtZhuQuanXiangXK);

        ddlXuKeZL.SelectedIndex = 0;
        txtClear(txtBeiAnR);
        txtClear(txtBianGengR);
        txtClear(txtJieChuRiXuKe);
        txtClear(txtHeTongBAH);
        txtClear(txtRangYuR);
        txtClear(txtShouRangR);
    }
    private string getURL(string strTitle, TextBox txt)
    {
        string strURL = "";
        if (txt.Text.ToString().Trim() != "")
        {
            strURL = strTitle + "=" + Server.UrlEncode(txt.Text.ToString().Trim()) + "&";
        }
        return strURL;
    }

    private string getURLCHK(string strTitle, CheckBoxList chk)
    {
        string strURL = "";
        string str = "";
        for (int i = 0; i < chk.Items.Count; i++)
        {
            if (chk.Items[i].Selected)
            {
                str += chk.Items[i].Text.ToString().Trim() + "|";
            }
            else
            {
                str += "|";
            }
        }
        str = str.Substring(0, str.Length - 1);
        strURL = strTitle + "=" + str + "&";

        return strURL;
    }

    private string getURLDDL(string strTitle, DropDownList ddl)
    {
        string strURL = "";

        strURL = strTitle + "=" + ddlXuKeZL.SelectedValue.ToString().Trim() + "&";

        return strURL;
    }


    private void txtClear(TextBox txt)
    {
        txt.Text = "";
    }
    /// <summary>
    /// 8位数字日期格式化为带点格式
    /// </summary>
    /// <param name="strRQ"></param>
    private void getFormatRQ(TextBox txt)
    {
        string strRQ = txt.Text.ToString().Trim();
        if (strRQ.Length < 8)
        {
            return;
        }

        //strRQ = strRQ.Substring(0, 4) + "." + strRQ.Substring(4, 2) + "." + strRQ.Substring(6, 2);

        strRQ = strRQ.Replace(".", "").Replace(" ", "").Replace("-", "");
        txt.Text = strRQ;
    }
}
