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
using System.Text.RegularExpressions;
using TLC.BusinessLogicLayer;

public partial class My_LawQueryResult : System.Web.UI.Page
{
    private static int pagSize;
    private static int pagIndex;
    private static int pagCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnQian.Enabled = false;
            btnHou.Enabled = false;
            btnShouYe.Enabled = false;
            btnMoYe.Enabled = false;

            pagSize = 10;
            pagIndex = 1;
            pagCount = 0;
            string type = Request.QueryString["type"];
            if (System.String.IsNullOrEmpty(type))
            {
                return;
            }
            hfType.Value = type;
            RefGrv();
        }
    }

    /// <summary>
    /// 刷新页面
    /// </summary>
    private void RefGrv()
    {
        string type = hfType.Value.ToString().Trim().ToLower();
        if (type == "zhuanyi")
        {
            RefGrvZhuanYi();
        }
        if (type == "zhiya")
        {
            RefGrvZhiYa();
        }
        if (type == "xuke")
        {
            RefGrvXuKe();
        }
        if (type == "falv")
        {
            RefGrvFL();
        }

        lblCountItem.Text = pagCount.ToString().Trim();

        if (pagCount % pagSize != 0)
        {
            pagCount = pagCount / pagSize;
            pagCount++;
        }
        else
        {
            pagCount = pagCount / pagSize;
        }

        lblCount.Text = pagCount.ToString();
       
        ReSetValue();
    }
    /// <summary>
    /// 中国专利法律状态检索
    /// </summary>
    private void RefGrvFL()
    {
        int UserID = Convert.ToInt32(Session["UserID"]);
        string AppNo, GongGaoR, FaLvZT, ZhuanLiQR, hitCount, SearchNo;
        AppNo = Server.UrlDecode(Request.QueryString["AppNo"]);
        GongGaoR = Server.UrlDecode(Request.QueryString["GongGaoR"]);
        FaLvZT = Server.UrlDecode(Request.QueryString["FaLvZT"]);
        ZhuanLiQR = Server.UrlDecode(Request.QueryString["ZhuanLiQR"]);
        hitCount = Server.UrlDecode(Request.QueryString["hitCount"]);
        SearchNo = Server.UrlDecode(Request.QueryString["SearchNo"]);


        Patentquery.WSFLZT.Service ss = new Patentquery.WSFLZT.Service();


        GridView1.DataSource = ss.QueryFaLvZTGetList(SearchNo, pagIndex, pagSize, UserID);
        GridView1.DataBind();
        pagCount = Convert.ToInt32(hitCount);

    }

    /// <summary>
    /// 转移
    /// </summary>
    private void RefGrvZhuanYi()
    {
        string AppNo, IPC = "",MC,ZY,ZQX, DengJiSXR, BianGengQQLR, BianGengHQLR, BianGengQDZ, BianGengHDZ;
        AppNo = Server.UrlDecode(Request.QueryString["AppNo"]);
        IPC = Server.UrlDecode(Request.QueryString["IPC"]);

        MC = Server.UrlDecode(Request.QueryString["MC"]);
        ZY = Server.UrlDecode(Request.QueryString["ZY"]);
        ZQX = Server.UrlDecode(Request.QueryString["ZQX"]);

        DengJiSXR = Server.UrlDecode(Request.QueryString["DengJiSXR"]);
        BianGengQQLR = Server.UrlDecode(Request.QueryString["BianGengQQLR"]);
        BianGengHQLR = Server.UrlDecode(Request.QueryString["BianGengHQLR"]);
        BianGengQDZ = Server.UrlDecode(Request.QueryString["BianGengQDZ"]);
        BianGengHDZ = Server.UrlDecode(Request.QueryString["BianGengHDZ"]);

        Patentquery.WSFLZT.Service ss = new Patentquery.WSFLZT.Service();

        List<Patentquery.WSFLZT.CnLegal012> lst = new List<Patentquery.WSFLZT.CnLegal012>();
        lst = ss.QueryZhuanYi(AppNo, IPC,MC,ZY,ZQX, DengJiSXR, BianGengQQLR, BianGengHQLR, BianGengQDZ, BianGengHDZ, pagIndex, pagSize, out pagCount).ToList();

        GridView1.DataSource = lst;
        GridView1.DataBind();

    }

    /// <summary>
    /// 质押
    /// </summary>
    private void RefGrvZhiYa()
    {
        string AppNo, IPC, MC, ZY, ZQX, HeTongZT, DengJiSXR, BianGengR, JieChuR, DengJiH, CHuZhiR, ZhiQuanR;
        AppNo = Server.UrlDecode(Request.QueryString["AppNo"]);
        IPC = Server.UrlDecode(Request.QueryString["IPC"]);

        MC= Server.UrlDecode(Request.QueryString["MC"]);
        ZY = Server.UrlDecode(Request.QueryString["ZY"]);
        ZQX = Server.UrlDecode(Request.QueryString["ZQX"]);

        HeTongZT = Server.UrlDecode(Request.QueryString["HeTongZT"]);
        DengJiSXR = Server.UrlDecode(Request.QueryString["DengJiSXR"]);
        BianGengR = Server.UrlDecode(Request.QueryString["BianGengR"]);

        JieChuR = Server.UrlDecode(Request.QueryString["JieChuR"]);
        DengJiH = Server.UrlDecode(Request.QueryString["DengJiH"]);
        CHuZhiR = Server.UrlDecode(Request.QueryString["CHuZhiR"]);
        ZhiQuanR = Server.UrlDecode(Request.QueryString["ZhiQuanR"]);

        Patentquery.WSFLZT.Service ss = new Patentquery.WSFLZT.Service();

        List<Patentquery.WSFLZT.CnLegal015> lst = new List<Patentquery.WSFLZT.CnLegal015>();
        lst = ss.QueryZhiYa(AppNo, IPC, MC, ZY, ZQX, HeTongZT, DengJiSXR, BianGengR, JieChuR, DengJiH, CHuZhiR, ZhiQuanR, pagIndex, pagSize, out pagCount).ToList();

        GridView1.DataSource = lst;
        GridView1.DataBind();
    }
    /// <summary>
    /// 实施许可
    /// </summary>
    private void RefGrvXuKe()
    {
        string AppNo, IPC, MC, ZY, ZQX, XuKeZL, HeTongZT, BeiAnRQ, BianGengR, JIeChuR, HeTongBAH, RangYuR, ShouRangR;
        AppNo = Server.UrlDecode(Request.QueryString["AppNo"]);
        IPC = Server.UrlDecode(Request.QueryString["IPC"]);

        MC = Server.UrlDecode(Request.QueryString["MC"]);
        ZY = Server.UrlDecode(Request.QueryString["ZY"]);
        ZQX = Server.UrlDecode(Request.QueryString["ZQX"]);

        XuKeZL = Server.UrlDecode(Request.QueryString["XuKeZL"]);
        HeTongZT = Server.UrlDecode(Request.QueryString["HeTongZT"]);
        BeiAnRQ = Server.UrlDecode(Request.QueryString["BeiAnRQ"]);
        BianGengR = Server.UrlDecode(Request.QueryString["BianGengR"]);
        JIeChuR = Server.UrlDecode(Request.QueryString["JIeChuR"]);
        HeTongBAH = Server.UrlDecode(Request.QueryString["HeTongBAH"]);
        RangYuR = Server.UrlDecode(Request.QueryString["RangYuR"]);
        ShouRangR = Server.UrlDecode(Request.QueryString["ShouRangR"]);

        Patentquery.WSFLZT.Service ss = new Patentquery.WSFLZT.Service();

        List<Patentquery.WSFLZT.CnLegal014> lst = new List<Patentquery.WSFLZT.CnLegal014>();
        lst = ss.QueryShiShiXuKe(AppNo, IPC, MC, ZY, ZQX, XuKeZL, HeTongZT, BeiAnRQ, BianGengR, JIeChuR, HeTongBAH, RangYuR, ShouRangR, pagIndex, pagSize, out pagCount).ToList();

        GridView1.DataSource = lst;
        GridView1.DataBind();
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridView1.Rows[i].Cells[3].Text = GridView1.Rows[i].Cells[3].Text.Replace("|", "<BR>");
        }
    }

    protected void btnQian_Click(object sender, EventArgs e)
    {
        pagIndex--;
        RefGrv();
    }

    protected void btnHou_Click(object sender, EventArgs e)
    {
        pagIndex++;
        RefGrv();
    }

    private void ReSetValue()
    {
        btnHou.Enabled = true;
        btnQian.Enabled = true;
        btnShouYe.Enabled = true;
        btnMoYe.Enabled = true;
        if (pagIndex <= 1)
        {
            btnQian.Enabled = false;
            btnShouYe.Enabled = false;
        }
        if (pagIndex >= pagCount)
        {
            btnHou.Enabled = false;
            btnMoYe.Enabled = false;
        }

        lblPagIndex.Text = pagIndex.ToString();
        txtPagIndex.Text = pagIndex.ToString();
    }
    protected void btnShouYe_Click(object sender, EventArgs e)
    {
        pagIndex = 1;
        RefGrv();
    }
    protected void btnMoYe_Click(object sender, EventArgs e)
    {
        pagIndex = pagCount;
        RefGrv();
    }
    /// <summary>
    /// 每页显示数量
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlPagCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        pagIndex = 1;
        pagSize = Convert.ToInt32(ddlPagCount.SelectedValue.ToString().Trim());
        RefGrv();
    }

    protected void txtPagIndex_TextChanged(object sender, EventArgs e)
    {
        try
        {
            pagIndex = Convert.ToInt32(txtPagIndex.Text.ToString().Trim());
        }
        catch (Exception ex)
        {
            MSG.AlertMsg(Page, "请输入正确页码！");
            return;
        }
                
        RefGrv();
    }
}
