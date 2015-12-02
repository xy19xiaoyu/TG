using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.SysAdmin
{
    public partial class frmIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefGrv();
                ckbIpSet.Checked = ProXZQDLL.IpFillterSvs.bIpFillter;
            }
        }

        private void RefGrv()
        {
            List<ProXZQDLL.TbIP> lst = ProXZQDLL.UserRight.getTbIP();
            grvInfo.DataSource = lst;
            grvInfo.DataBind();
        }

        protected void grvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvInfo.PageIndex = e.NewPageIndex;
            RefGrv();
        }

        protected void grvInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ProXZQDLL.UserRight.TbIpDel(int.Parse(grvInfo.Rows[e.RowIndex].Cells[0].Text.ToString().Trim()));

            RefGrv();
            MSG.AlertMsg(Page, "操作成功！");
        }

        protected void grvInfo_DataBound(object sender, EventArgs e)
        {
            if (grvInfo.Rows.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < grvInfo.Rows.Count; i++)
            {
                grvInfo.Rows[i].Cells[0].Visible = false;
            }
            grvInfo.HeaderRow.Cells[0].Visible = false;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string IP = txtIP.Text.ToString().Trim();
            ProXZQDLL.UserRight.TbIpInsert(IP, int.Parse(rbtList.SelectedValue.ToString().Trim()));
            RefGrv();

            MSG.AlertMsg(Page, "操作成功！");
        }

        protected void ckbIpSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //"glbalIpKey";  XmPatentComm
                bool bRs = ProXZQDLL.IpFillterSvs.TbIpUp("glbalIpKey", ckbIpSet.Checked ? 0 : 1);
                if (bRs)
                {
                    ProXZQDLL.IpFillterSvs.bIpFillter = ckbIpSet.Checked;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}