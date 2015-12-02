using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.SysAdmin
{
    public partial class frmLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // RefGrv();
                txtDateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ddlUserName.Items.Clear();
                ddlUserName.Items.Add("请选择");

                lblToDay.Text = ProXZQDLL.ClsLog.getFangWenLToDay().ToString();
                lblZong.Text = ProXZQDLL.ClsLog.getFangWenLZong().ToString();
            }
        }

        protected void btnChaXun_Click(object sender, EventArgs e)
        {
            RefGrv();
        }

        private void RefGrv()
        {



            grvInfo.DataSource = ProXZQDLL.ClsLog.QueryLog(txtDateStart.Text.ToString().Trim(), txtDateEnd.Text.ToString().Trim(), ddlYongHuLX.SelectedValue.ToString().Trim().Replace("请选择", ""), ddlUserName.SelectedValue.ToString().Trim().Replace("请选择", ""));
            grvInfo.DataBind();
        }

        protected void grvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvInfo.PageIndex = e.NewPageIndex;
            RefGrv();
        }

        /// <summary>
        /// 用户类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYongHuLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYongHuLX.SelectedValue == "请选择")
            {
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add("请选择");
                return;
            }
            refUserDDL(ddlYongHuLX.SelectedValue.ToString().Trim());
        }

        private void refUserDDL(string YongHuLX)
        {
            ddlUserName.Items.Clear();
            ProXZQDLL.DataClasses1DataContext db = new ProXZQDLL.DataClasses1DataContext();
            var result = from item in db.TbUser
                         where item.YongHuLeiXing == YongHuLX
                         select item;

            ddlUserName.Items.Add("请选择");
            foreach (var item in result)
            {
                ddlUserName.Items.Add(item.RealName);
            }           
        }
    }
}