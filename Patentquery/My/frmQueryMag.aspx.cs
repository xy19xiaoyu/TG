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
using TLC.BusinessLogicLayer;

public partial class My_PatternList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ObjectDataSource1.SelectParameters[0].DefaultValue = Page.User.Identity.Name;
            //ObjectDataSource2.SelectParameters[0].DefaultValue = Page.User.Identity.Name;
            BindData();
        }
    }
    private void BindData()
    {

        string sql = "select * from TLC_Patterns where userid=" + Session["UserID"].ToString() + " and types=" + RadioButtonListTypes.SelectedValue;
        if (TextBoxKeyword.Text.Trim() == "")
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('请输入要查询的检索式!')</script>");
        }
        else
        {
            sql += " and Expression like '%" + TextBoxKeyword.Text.Trim() + "%'";
        }
        if (ddlModel.SelectedIndex > 0)
        {
            sql += " and source=" + ddlModel.SelectedValue;
        }

        if (txtSTime.Text.Trim() == "" || txtEndTime.Text.Trim() == "")
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('请输入要查询的来源!')</script>");
        }
        else
        {
            //sql += " and Convert(varchar(10), CreateDate,120)='" + txtSTime.Text.Trim() + "'";
            sql += string.Format(" and CreateDate between '{0} 00:00:00' and '{1} 23:59:59'", txtSTime.Text.Trim(), txtEndTime.Text.Trim());
        }
        sql += " order by createdate desc ";
        DataTable dt = new DataTable();
        dt = DBA.DbAccess.GetDataTable(CommandType.Text, sql, null);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["Expression"] = dt.Rows[i]["Expression"].ToString().Replace("<", "&lt;").Replace(">", "&gt;");
        }
        GridView1.DataSource = dt;
        GridView1.PageIndex = 0;
        ViewState["dt"] = dt;
        GridView1.DataBind();
    }
    protected void RadioButtonListTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void LinkButtonSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void LinkButtonDeleteSelected_Click(object sender, EventArgs e)
    {
        bool isChecked = false;
        foreach (GridViewRow currentRow in GridView1.Rows)
        {
            CheckBox currentCheckBox = (CheckBox)currentRow.Cells[0].FindControl("CheckBoxSelect");
            if (currentCheckBox != null)
            {
                if (currentCheckBox.Checked)
                {
                    isChecked = true;
                    string strId = GridView1.DataKeys[currentRow.RowIndex].Value.ToString();
                    Pattern.DeletePattern(Convert.ToInt32(strId));
                }
            }
        }
        if (isChecked)
        {
            BindData();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('请选择要删除的检索式')</script>");
        }
    }

    protected void LinkButtonExport_Click(object sender, EventArgs e)
    {
        String strFileName = "Pattern_" + DateTime.Today.ToString("yyyyMMdd");

        string strComma = ",";
        string strNewLine = Environment.NewLine;

        StringBuilder sbContent = new StringBuilder();

        sbContent.Append("检索编号");
        sbContent.Append(strComma);
        sbContent.Append("检索式");
        sbContent.Append(strComma);
        sbContent.Append("命中数");
        sbContent.Append(strComma);
        sbContent.Append("来源");
        sbContent.Append(strComma);
        sbContent.Append("检索时间");
        bool isChecked = false;
        foreach (GridViewRow currentRow in GridView1.Rows)
        {
            CheckBox currentCheckBox = (CheckBox)currentRow.Cells[0].FindControl("CheckBoxSelect");
            if (currentCheckBox != null)
            {
                if (currentCheckBox.Checked)
                {
                    isChecked = true;
                    string strId = GridView1.DataKeys[currentRow.RowIndex].Value.ToString();
                    Pattern currentPattern = Pattern.GetPatternByPatternId(Convert.ToInt32(strId));
                    if (currentPattern != null)
                    {
                        sbContent.Append(strNewLine);
                        sbContent.Append(currentPattern.Number);
                        sbContent.Append(strComma);
                        sbContent.Append(currentPattern.Expression);
                        sbContent.Append(strComma);
                        sbContent.Append(currentPattern.Hits.ToString());
                        sbContent.Append(strComma);
                        sbContent.Append(ShowSource(currentPattern.Source));
                        sbContent.Append(strComma);
                        sbContent.Append(currentPattern.CreateDate);
                    }
                }
            }
        }
        if (isChecked)
        {
            Page.Response.Clear();
            bool success = ResponseFile(Page.Request, Page.Response, strFileName + ".csv", sbContent.ToString(), 1024000);
            if (success)
            {
                //LiteralHint.Text = "成功";
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('请选择要导出的检索式')</script>");
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["dt"];
        GridView1.DataSource = dt;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //各行加上鼠标经过效果
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");
            ////自定义按钮加上CommandArgument以支持按钮事件
            //LinkButton currentLinkButton = (LinkButton)e.Row.Cells[5].FindControl("LinkButtonQuery");
            //currentLinkButton.CommandArgument = e.Row.RowIndex.ToString();
            //Literal l = e.Row.Cells[5].FindControl("LiteralQuery") as Literal;
            //l.Text=""

        }
    }

    protected string ShowLength(string content, int length)
    {
        string strReturn = string.Empty;
        if (content.Length > length)
        {
            strReturn = content.Substring(0, length) + "..";
        }
        else
        {
            strReturn = content;
        }
        return strReturn;
    }

    protected string ShowSource(byte source)
    {
        string strReturn = string.Empty;
        switch (source)
        {
            case 0:
                strReturn = "智能检索";
                break;
            case 1:
                strReturn = "表格检索";
                break;
            case 2:
                strReturn = "专家检索";
                break;
            case 3:
                strReturn = "分类导航检索";
                break;
            case 4:
                strReturn = "二次检索";
                break;
            case 5:
                strReturn = "过滤检索";
                break;
            default:
                strReturn = "-";
                break;
        }
        return strReturn;
    }

    public string GetDispLayUrl(string ds, string No, string strHis, string strExpression)
    {
        string strURl = "#";
        try
        {
            string strDb = ds == "0" ? "CN" : "EN";
            strURl = string.Format("frmPatentList.aspx?db={0}&No={1}&kw=&Nm={2}&etp=&Query={3}", strDb, No, strHis, Server.UrlEncode(strExpression).Replace("+", "%20"));
        }
        catch (Exception ex)
        {
            strURl = "#";
        }
        return strURl;

    }
}