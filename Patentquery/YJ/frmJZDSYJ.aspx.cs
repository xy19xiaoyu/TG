using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProYJDLL;

namespace Patentquery.YJ
{
    public partial class frmJZDSYJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["country"] == null)
                {
                    Session["country"] = "CN";
                }

                if (Session["C_TYPE"] == null)
                {
                    Session["C_TYPE"] = "1";
                }
                RefddlKeyWord();

            }
        }
        /// <summary>
        /// 查询的类别下拉框邦定
        /// </summary>
        private void RefddlKeyWord()
        {
            int LeiXing = int.Parse(Session["C_TYPE"].ToString().Trim());
            string item = "";
            switch (LeiXing)
            {
                case 1:
                    item = "竞争对手";
                    break;
                case 2:
                    item = "技术领域";
                    break;
                case 3:
                    item = "发明人";
                    break;
                case 4:
                    item = "区域";
                    break;
                case 5:
                    item = "来华公司";
                    break;
                case 6:
                    item = "检索式";
                    break;
            }

            ddlKeyWord.Items.Clear();
            ddlKeyWord.Items.Add(item);
            ddlKeyWord.Items.Add("预警设置日期");
            ddlKeyWord.Items.Add("预警名称");

            ddlKeyWord.Items[0].Value = "0";
            ddlKeyWord.Items[1].Value = "1";
            ddlKeyWord.Items[2].Value = "2";

            RefGrv();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChaXun_Click(object sender, EventArgs e)
        {
            RefGrv();
        }

        /// <summary>
        /// 信息邦定
        /// </summary>
        private void RefGrv()
        {
            DateTime dt;
            if (ddlKeyWord.SelectedValue.ToString().TrimEnd() == "1")
            {
                try
                {
                    dt = Convert.ToDateTime(txtKeyWord.Text.ToString().Trim());
                }
                catch (Exception ex)
                {
                    MSG.AlertMsg(Page, "请输入合法的日期查询。如：2013-01-01");
                    return;
                }
            }
            int C_TYPE = 0;
            if (Session["C_TYPE"] != null)
            {
                C_TYPE = Convert.ToInt32(Session["C_TYPE"]);
            }
            int pagCount = 0;
            int userid = int.Parse(System.Web.HttpContext.Current.Session["UserID"].ToString());
            grvInfo.DataSource = YJDB.getYJ(ddlKeyWord.SelectedValue.ToString().Trim(), txtKeyWord.Text.ToString().Trim(), C_TYPE, 1, Session["country"].ToString().Trim(), 1, 1, out pagCount, userid);
            grvInfo.DataBind();
        }
        /// <summary>
        /// 竞争对手动向预警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnJZDS_Click(object sender, EventArgs e)
        {
            Session["C_TYPE"] = 1;
            RefddlKeyWord();
        }

        /// <summary>
        /// 特定技术动向预警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTDJS_Click(object sender, EventArgs e)
        {
            Session["C_TYPE"] = 2;
            RefddlKeyWord();
        }

        /// <summary>
        /// 发明人动向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFaMingRenDX_Click(object sender, EventArgs e)
        {
            Session["C_TYPE"] = 3;
            RefddlKeyWord();
        }
        /// <summary>
        /// 区域分布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnZhuanLiQuYuFB_Click(object sender, EventArgs e)
        {
            Session["C_TYPE"] = 4;
            RefddlKeyWord();
        }
        /// <summary>
        /// 来华专利
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLaiHuaZhuanLi_Click(object sender, EventArgs e)
        {
            Session["C_TYPE"] = 5;
            RefddlKeyWord();
        }

        /// <summary>
        /// 高级定制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGaoJi_Click(object sender, EventArgs e)
        {
            Session["C_TYPE"] = 6;
            RefddlKeyWord();
        }
        /// <summary>
        /// 中国专利预警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCN_Click(object sender, EventArgs e)
        {
            Session["country"] = "CN";
            btnLaiHuaZhuanLi.Visible = true;
            RefddlKeyWord();
        }

        /// <summary>
        /// 世界专利预警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEN_Click(object sender, EventArgs e)
        {
            Session["country"] = "DOCDB";
            btnLaiHuaZhuanLi.Visible = false;
            RefddlKeyWord();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdd.aspx");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDel_Click(object sender, EventArgs e)
        {
            CheckBox chk = new CheckBox();
            int CID = 0;
            bool flag = false;

            foreach (GridViewRow row in grvInfo.Rows)
            {
                chk = (CheckBox)row.Cells[0].FindControl("chkXuanZe");
                if (chk.Checked)
                {
                    flag = true;
                    CID = int.Parse(chk.ToolTip.ToString().Trim());
                    ProYJDLL.YJDB.YjDelete(CID);
                    ProYJDLL.YJDB.YjDeleteItemAll(CID);
                }
            }

            if (!flag)
            {
                MSG.AlertMsg(Page, "请选择要删除的记录！");
                return;
            }
            RefGrv();
        }
    }
}