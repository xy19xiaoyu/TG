using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Cpic.Cprs2010.Search;
using Cpic.Cprs2010.User;
using System.Web.Services;
using Cpic.Cprs2010.Search.SearchManager;
using System.Text.RegularExpressions;
using System.Collections;
using log4net;
using System.Data;
using Cpic.Cprs2010.Cfg;
using TLC.BusinessLogicLayer;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Patentquery.My
{
    public partial class frmcnExpertSearch : System.Web.UI.Page
    {
        private static readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    return;
                }
                if (HttpContext.Current.Session["UserName"] == null)
                {
                    // UserLogin.GetYouKe();
                }
                updateSearchHisTable(-1);
            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        /// 更新检索历史列表
        /// </summary>
        /// <param name="usr">用于获取检索历史的用户id</param>
        /// <param name="pageIndex">检索历史列表显示的页码，如果值为-1，则表示显示最后一页</param>
        private void updateSearchHisTable(int pageIndex)
        {
            //if (usr != null)
            //    strHis = usr.getSearchHis(Cpic.Cprs2010.Search.SearchDbType.Cn);
            //else
            //    return;

            string strLogName = Session["UserID"].ToString().Trim();
            List<Pattern> lstPtQ = Pattern.GetPatternsByUserIdAndSourceAndTypes(Convert.ToInt32(strLogName), Convert.ToByte("2"), Convert.ToByte("0"));

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
                string strUrl = string.Format("frmPatentList.aspx?db=CN&No={0}&kw=&Nm={1}&etp=&Qsrc=2&Query={2}", item.Number.ToString(), item.Hits.ToString(), this.Server.UrlEncode(item.Expression.Trim()).Replace("+", "%20"));
                dr["HyperLink"] = strUrl;

                // 增加一列
                dt.Rows.Add(dr);
            }

            if (pageIndex == -1)
                this.cnSearchHistoryGrid.PageIndex = dt.Rows.Count / this.cnSearchHistoryGrid.PageSize + 1;
            else
                this.cnSearchHistoryGrid.PageIndex = pageIndex;
            cnSearchHistoryGrid.DataSource = dt;
            cnSearchHistoryGrid.ShowHeader = false;
            cnSearchHistoryGrid.DataBind();
            // 利用客户端js，设置table div的滚动条至最底下
            ScriptManager.RegisterStartupScript(this, this.GetType(), "format", "formatTableScrollBar();", true);
        }

        /// <summary>
        /// 检索历史页码发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cnSearchHistoryGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //User userInfor = (User)this.Session["Userinfo"];
            updateSearchHisTable(e.NewPageIndex);
        }


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

            foreach (GridViewRow currentRow in cnSearchHistoryGrid.Rows)
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

        // 删除检索式，如果maxNum>0，则表示要更新检索编号至maxNum，当maxNum为-1时，表示不予更新
        [WebMethod]
        public static bool DeleteBatchHis(string numArr, int maxNum)
        {
            string[] _numArr = numArr.Split(new Char[] { ',', '|' });

            foreach (string stritem in _numArr)
            {
                Pattern.DeletePattern(int.Parse(stritem));
            }
            return true;
        }

        protected void BtnSearchHisUpdate_Click(object sender, EventArgs e)
        {
            //User userInfor = (User)this.Session["Userinfo"];
            //if (userInfor != null)
            updateSearchHisTable(-1);
        }
    }
}