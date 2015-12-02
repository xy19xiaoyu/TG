using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cpic.Cprs2010.Search.SearchManager;
using System.Reflection;
using Cpic.Cprs2010.Search;
using Cpic.Cprs2010.User;

namespace Patentquery
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static Cpic.Cprs2010.Engine.FileFinder _fd;
        public static Cpic.Cprs2010.Engine.FileFinder fd
        {
            get
            {
                if (_fd == null)
                {
                    _fd = new Cpic.Cprs2010.Engine.FileFinder();
                }
                return _fd;
            }
        }
        private log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Ini();
            }

        }
        private void Ini()
        {
            txtshow.Text = string.Empty;
            this.txtshow.Text += string.Format("Cn&nbsp;&nbsp;&nbsp;&nbsp;IP:{0},Port:{1},Count:{2}<br/>", CprsConfig.CnIP, CprsConfig.CnPort, SearchFactory.getPoolLength(SearchDbType.Cn));
            this.txtshow.Text += string.Format("Docdb&nbsp;IP:{0},Port:{1},Count:{2}<br/>", CprsConfig.DocdbIP, CprsConfig.DocdbPort, SearchFactory.getPoolLength(SearchDbType.DocDB));
            //this.txtshow.Text += string.Format("Dwpi&nbsp;&nbsp;IP:{0},Port:{1},Count:{2}<br/>", CprsConfig.DwpiIP, CprsConfig.DwpiPort, SearchFactory.getPoolLength(SearchDbType.Dwpi));

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            //检索式
            SearchPattern sp = new SearchPattern();
            sp.Pattern = txtSearchPattern.Text.Trim();
            sp.DbType = (SearchDbType)Enum.Parse(typeof(SearchDbType), ddlDBType.SelectedValue.Trim());
            sp.SearchNo = "001";


            //用户
            Cpic.Cprs2010.User.User user;
            // 判断用户是否登录
            if (Session["UserInfo"] == null)
            {
                //user = UserManager.getGuestUser(Session.SessionID);
                //Session["UserInfo"] = user;
            }
            else
            {
                //user = (Cpic.Cprs2010.User.User)Session["UserInfo"];
            }

            sp.UserId = Convert.ToInt32(Session["UserID"]); // user.ID;
            //user.addSearchHis(sp);

            //得到检索连接
            ISearch mySearch;
            mySearch = SearchFactory.CreatSearch(sp.DbType);

            if (mySearch == null)
            {
                this.lblmessage.Text = "已超最大连接！请等待！";
            }
            else
            {
                ResultInfo rs = mySearch.Search(sp);
                this.lblmessage.Text = Server.HtmlEncode(rs.HitMsg) + "<br/>";
                SearchFactory.FreeSearch(mySearch);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SearchFactory.Ini(Request.UserHostAddress.ToString());
            Ini();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (this.TextBox1.Text == "7788")
            {

                this.Panel1.Visible = false;
                this.panxy.Visible = true;
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            log.Debug(Request.UserHostAddress.ToString() + "LogOut");
            SearchFactory.Dispose();
            Ini();
        }

        protected void btnGrow_Click(object sender, EventArgs e)
        {
            SearchFactory.Grow((SearchDbType)Enum.Parse(typeof(SearchDbType), ddlDBType.SelectedValue.Trim()));
            Ini();

        }

        protected void Button7_Click1(object sender, EventArgs e)
        {

            ////检索式
            //SearchPattern sp = new SearchPattern();
            //sp.Pattern = txtSearchPattern.Text.Trim();
            //sp.DbType = (SearchDbType)Enum.Parse(typeof(SearchDbType), ddlDBType.SelectedValue.Trim());
            //sp.SearchNo = "001";


            ////用户
            //Cpic.Cprs2010.User.User user;
            //// 判断用户是否登录
            //if (Session["UserInfo"] == null)
            //{
            //    user = UserManager.getGuestUser(Session.SessionID);
            //    Session["UserInfo"] = user;
            //}
            //else
            //{
            //    user = (Cpic.Cprs2010.User.User)Session["UserInfo"];
            //}

            //sp.UserId = user.ID;
            //user.addSearchHis(sp);

            ////得到检索连接
            //ISearch mySearch;
            //mySearch = SearchFactory.CreatSearch(sp.DbType);

            //if (mySearch == null)
            //{
            //    this.lblmessage.Text = "已超最大连接！请等待！";
            //}
            //else
            //{
            //    ResultInfo rs = mySearch.Search(sp);
            //    this.lblmessage.Text = Server.HtmlEncode(rs.HitMsg) + "<br/>";
            //    SearchFactory.FreeSearch(mySearch);
            //}

            Cpic.Cprs2010.Engine.Result result;
            Cpic.Cprs2010.Engine.SearchPattern sp = new Cpic.Cprs2010.Engine.SearchPattern(fd.Config);
            sp.SearchCommand = txtSearchPattern.Text.Trim();
            try
            {
                //初始化
                sp.Init();
                //检索
                sp.DoSearch(fd);
                //得到结果
                result = sp.GetResult();
                result.Content = result.Content.Distinct().ToList<int>();
                result.Content.Sort();
            }
            catch (Exception ex)
            {
                // MessageBox.Show("错误信息：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.lblmessage.Text = result.Content.Count().ToString();
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            _fd = new Cpic.Cprs2010.Engine.FileFinder();
        }
    }
}