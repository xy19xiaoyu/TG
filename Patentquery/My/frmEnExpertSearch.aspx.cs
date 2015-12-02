using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cpic.Cprs2010.User;
using System.Data;
using System.Text.RegularExpressions;
using TLC.BusinessLogicLayer;
using System.IO;
using Cpic.Cprs2010.Search;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Patentquery.My
{
    public partial class frmEnExpertSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    return;
                }

                updateSearchHisTable(-1);
            }
            catch (Exception ex)
            {
            }
        }

        protected void BtnSearchHisUpdate_Click(object sender, EventArgs e)
        {       
            updateSearchHisTable( -1);
        }

        /// <summary>
        /// 更新检索历史列表
        /// </summary>
        /// <param name="usr">用于获取检索历史的用户id</param>
        /// <param name="pageIndex">检索历史列表显示的页码，如果值为-1，则表示显示最后一页</param>
        private void updateSearchHisTable( int pageIndex)
        {

            //List<string> strHis = new List<string>();
            //if (usr != null)
            //    strHis = usr.getSearchHis(Cpic.Cprs2010.Search.SearchDbType.DocDB);
            //else
            //    return;

            string strLogName = Session["UserID"].ToString().Trim();
            List<Pattern> lstPtQ = Pattern.GetPatternsByUserIdAndSourceAndTypes(Convert.ToInt32(strLogName), Convert.ToByte("2"), Convert.ToByte("1"));

            DataTable dt = new DataTable();
            DataColumn colDT = new DataColumn("SearchDate");
            DataColumn colSN = new DataColumn("SearchNum");
            DataColumn colSF = new DataColumn("SearchFormula");
            DataColumn colSR = new DataColumn("SearchResult");
            DataColumn colHL = new DataColumn("HyperLink");
            DataColumn colID = new DataColumn("DbID");
            dt.Columns.Add(colDT);
            dt.Columns.Add(colSN);
            dt.Columns.Add(colSF);
            dt.Columns.Add(colSR);
            dt.Columns.Add(colHL);
            dt.Columns.Add(colID);

            lstPtQ.Reverse();
            // strHis中检索式格式： 2010-03-04 (002)F TI 计算机 <hits:1230>
            foreach (Pattern item in lstPtQ)
            {
                DataRow dr = dt.NewRow();

                dr["DbID"] = item.PatternId.ToString();
                dr["SearchDate"] = item.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                dr["SearchNum"] = item.Number.ToString();
                dr["SearchResult"] = item.Hits.ToString();
                string searchFormula = item.Expression.Trim();
                searchFormula = searchFormula.Replace("<", "&lt;").Replace(">", "&gt;");
                dr["SearchFormula"] = searchFormula + " &lt;hits:" + item.Hits.ToString() + "&gt;";
                //"?db=" + dbType + "&No=" + sNo + "&kw=" + encodeURIComponent(strkw) + "&Nm=" + num + "&etp=" + encodeURIComponent(errTip) + "&Query=" + encodeURIComponent(strQuery.substr(4));
                string strUrl = string.Format("frmPatentList.aspx?db=EN&No={0}&kw=&Nm={1}&etp=&Qsrc=2&Query={2}", item.Number.ToString(), item.Hits.ToString(), this.Server.UrlEncode(item.Expression.Trim()).Replace("+", "%20"));
                dr["HyperLink"] = strUrl;

                // 增加一列
                dt.Rows.Add(dr);
            }

            if (pageIndex == -1)
                this.docdbSearchHistoryGrid.PageIndex = dt.Rows.Count / this.docdbSearchHistoryGrid.PageSize + 1;
            else
                this.docdbSearchHistoryGrid.PageIndex = pageIndex;
            docdbSearchHistoryGrid.DataSource = dt;
            docdbSearchHistoryGrid.ShowHeader = false;
            docdbSearchHistoryGrid.DataBind();

            // 在后台设置div属性，使得检索历史在一开始呈现即是一个置底的状态。
            //docdbSearchHisTable.Style.Add(
            //docdbSearchHisTable.Style
            //if (docdbSearchHisTable.Style.scrollHeight > docdbSearchHisTable.Style.clientHeight)
            //    docdbSearchHisTable.Style.scrollTop = docdbSearchHisTable.Style.scrollHeight - docdbSearchHisTable.style.clientHeight;
            // 利用客户端js，设置table div的滚动条至最底下
            ScriptManager.RegisterStartupScript(this, this.GetType(), "format", "formatTableScrollBar();", true);
          
        }


        /// <summary>
        /// 检索历史页码发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void docdbSearchHistoryGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            updateSearchHisTable( e.NewPageIndex);
        }

        /// <summary>
        /// 导出检索历史
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonExport_Click(object sender, EventArgs e)
        {
            String strFileName = "Pattern_" + DateTime.Today.ToString("yyyyMMdd");

            string strComma = ",";

            StringBuilder sbContent = new StringBuilder();

            sbContent.Append("检索时间");
            sbContent.Append(strComma);
            sbContent.Append("检索编号");
            sbContent.Append(strComma);
            sbContent.Append("检索式");
            sbContent.Append(strComma);
            sbContent.Append("命中数");
            sbContent.Append(strComma);

            bool isChecked = false;

            foreach (GridViewRow currentRow in docdbSearchHistoryGrid.Rows)
            {
                //((CheckBox)currentRow.Cells[0].Controls[1]).CssClass
                //CheckBox currentCheckBox = (CheckBox)currentRow.Cells[0].FindControl("CheckBoxSelect");

                HtmlInputCheckBox currentCheckBox = (HtmlInputCheckBox)(currentRow.Cells[0].Controls[1]);

                if (currentCheckBox != null)
                {
                    if (currentCheckBox.Checked)
                    {
                        isChecked = true;

                        sbContent.Append(Environment.NewLine);

                        HtmlContainerControl htmlSpan = ((HtmlContainerControl)(currentRow.Cells[0].Controls[5]));
                        sbContent.Append(htmlSpan.InnerText.Replace(" ", "").Replace("\r\n", ""));

                        sbContent.Append(strComma);
                        htmlSpan = ((HtmlContainerControl)(currentRow.Cells[0].Controls[3]));
                        sbContent.Append(htmlSpan.InnerText.Replace(" ", "").Replace("\r\n", "").Replace("(", "").Replace(")", ""));

                        sbContent.Append(strComma);
                        htmlSpan = ((HtmlContainerControl)(currentRow.Cells[0].Controls[7]));
                        sbContent.Append(htmlSpan.InnerText.Replace("\r\n", "").Replace("<hits:", strComma + "<hits:").Trim());
                    }
                }
            }
            if (isChecked)
            {
                Page.Response.Clear();
                bool success = frmPatDetails.ResponseFile(Page.Request, Page.Response, strFileName + ".csv", sbContent.ToString(), 1024000);
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
    }
}