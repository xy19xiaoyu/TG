using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Patentquery.SysAdmin
{
    public partial class frmTJLanMu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            ds = ProXZQDLL.ClsTJ.getLanMu(txtDateStart.Text.ToString().Trim(), txtDateEnd.Text.ToString().Trim());

            grvInfo.DataSource = ds;
            grvInfo.DataBind();
        }
    }
}