using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProXZQDLL;
using System.Text;
using System.IO;
using DBA;

namespace Patentquery.SysAdmin
{
    public partial class frmOpinionMng : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefGrv("", "");
            }
        }

        private void RefGrv(string strSDate, string strEDate)
        {
            List<VoOpinion> lstOpinion = ClsOpinion.getOpinion(strSDate, strEDate);

            if (ViewState["PgData"] != null)
            {
                ViewState["PgData"] = lstOpinion;
            }
            else
            {
                ViewState.Add("PgData", lstOpinion);
            }

            if (lstOpinion.Count > 0)
            {
                Button1.Visible = true;
                //labExlFile.Visible = true;
            }
            else
            {
                Button1.Visible = false;
                //labExlFile.Visible = false;
            }

            GridView1.DataSource = lstOpinion;
            GridView1.DataBind();
        }

        protected void btnChaXun_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DateTime dtStart = new DateTime();
            DateTime dtEnd = new DateTime();
            if (txtDateStart.Text.ToString().Trim() != "")
            {
                try
                {
                    dtStart = Convert.ToDateTime(txtDateStart.Text.ToString().Trim());
                }
                catch (Exception ex)
                {
                    MSG.AlertMsg(Page, "请输入正确的起始日期！");
                    return;
                }
            }

            if (txtDateStart.Text.ToString().Trim() != "")
            {
                try
                {
                    dtEnd = Convert.ToDateTime(txtDateEnd.Text.ToString().Trim());
                    dtEnd = dtEnd.AddDays(1);
                }
                catch (Exception ex)
                {
                    MSG.AlertMsg(Page, "请输入正确的结束日期！");
                    return;
                }
            }
            RefGrv(txtDateStart.Text.ToString().Trim(), txtDateEnd.Text.ToString().Trim());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            List<VoOpinion> lstOpinion = (List<VoOpinion>)ViewState["PgData"];

            GridView1.DataSource = lstOpinion;
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
                DBA.DbAccess.ExecNoQuery(CommandType.Text, string.Format("delete from tbOpinion where id={0}",ID));
                //MSG.AlertMsg(Page, "请输入正确的结束日期！");
                btnChaXun_Click(null, null);
            }
            catch (Exception ex)
            {
                MSG.AlertMsg(Page, "删除操作失败，请重试！");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Export("application/vnd.excel", "意见报表.xls");

            try
            {

                ExcelDisplay exl = new ExcelDisplay();
                DataTable dtTmp = new DataTable(); 
                dtTmp.Columns.Add("意见标题");
                dtTmp.Columns.Add("提交人");
                dtTmp.Columns.Add("提交时间");
                dtTmp.Columns.Add("意见内容");
                dtTmp.TableName = "意见报表"; // labHeadInfor.Text;

                List<VoOpinion> lstOpinion = (List<VoOpinion>)ViewState["PgData"];
                foreach (VoOpinion item in lstOpinion)
                {
                    DataRow dr= dtTmp.NewRow();
                    dr["意见标题"] = item.Title;
                    dr["提交人"] = item.UName;
                    dr["提交时间"] = item.TJDate;
                    dr["意见内容"] = item.LYTxt;
                    dtTmp.Rows.Add(dr);
                }

                exl.PushXls2Web(this, dtTmp);
                if (exl.strXlsFile != "")
                {
                    labExlFile.Visible = true;
                    labExlFile.Text = string.Format("如果未出现下载窗口,请<a href={0}>[点击]</a>下载!", exl.strXlsFile);
                }
                else
                {
                    labExlFile.Visible = false;
                }
            }
            catch (Exception ex)
            {
                labExlFile.Visible = false;
            }
        }

        private void Export(string FileType, string FileName)
        {            
            Response.Charset = "GB2312";
            Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
            Response.ContentType = FileType;  //Response.ContentType = "application/vnd.xls";
            this.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            GridView1.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
           
        }
    }
}