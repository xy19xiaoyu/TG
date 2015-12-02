using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Patentquery.My
{
    public partial class frmSearchWord : System.Web.UI.Page
    {
        public int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grvRsData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grvRsData.PageIndex = e.NewPageIndex;
            btnSearch_Click(null, null);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string sql = "select word_same from tb_sameword where word_ch=''";
            //DataTable dt = new DataTable();
            //dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, string.Format(sql, this.TextBox1.Text.Trim()), null);
            //recordCount = dt.Rows.Count;
            //grvRsData.DataSource = dt;
            //grvRsData.DataBind();

            try
            {
                lstSameWord.Items.Clear();
                string sql = "select word_same from tb_sameword where word_ch='{0}'";

                //string strSameWord = DBA.SqlDbAccess.ExecuteScalar(CommandType.Text,
                //    string.Format(sql, this.TextBox1.Text.Trim()), null).ToString();


                //if (!string.IsNullOrEmpty(strSameWord))
                //{
                //    foreach (string strItem in strSameWord.Split('／'))
                //    {
                //        lstSameWord.Items.Add(strItem);
                //    }
                //}
                //else
                //{
                //    lstSameWord.Items.Add("没有对应数据");
                //}

                sql = "select distinct  Word_Same from Tb_SameWord where Word_CH in (select Word_CH from Tb_SameWord where Word_Same='{0}')";

                DataTable dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, string.Format(sql, this.TextBox1.Text.Trim()), null);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lstSameWord.Items.Add(dr[0].ToString());
                    }
                }
                else
                {
                    lstSameWord.Items.Add("没有对应数据");
                }
            }
            catch (Exception ex)
            {
                lstSameWord.Items.Add("没有对应数据");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {

                //grvRsData.PageIndex = int.Parse(((TextBox)grvRsData.BottomPagerRow.FindControl("txtGoPagestock")).Text) - 1;
            }
            catch
            {
                return;
            }
            btnSearch_Click(null, null);
        }
    }
}