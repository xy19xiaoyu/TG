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
using TLC.BusinessLogicLayer;
using System.Web.Services;
using System.Text.RegularExpressions;
using Cpic.Cprs2010.Cfg;
using log4net;
using System.Reflection;
using Cpic.Cprs2010.Search;

public partial class My_SmartQuery : System.Web.UI.Page
{
    private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProXZQDLL.ClsLog.LogInsertLanMu(this, "文字检索"); // 
        }
    }

    /// <summary>
    /// 执行检索
    /// </summary>
    /// <param name="strSearchQuery"></param>
    /// <param name="_strSdbType">[//CN 中国专利=0,//EN|WD 世界专利=1]</param>
    /// <param name="_sDoSrc">[0 智能检索,//1 表格检索,//2 专家检索,//3 分类导航检索,//4 二次检索,//5 过滤检索//-1连接检索]</param>
    /// <returns></returns>
    [WebMethod]
    public static IEnumerable DoPatSearch(string strSearchQuery, string _strSdbType, string _sDoSrc)
    {
        string[] strArryResutlMsg = { "", "0", "0" };  //0:msg, 1:sno, 2:hiscount
        try
        {
            //User userInfor = (User)new Page().Session["Userinfo"];
            //if (userInfor == null)  // 判断用户登录
            //{
            //    logger.Info("用户已退出，请重新登录");
            //    return "用户已退出，请重新登录";
            //}

            string strQuery = System.Web.HttpUtility.UrlDecode(strSearchQuery.Trim()).Trim();
            //string strQuery = strSearchQuery.Trim();
            //return "(001)F TI 计算机 <hit:20>"; // 测试用

            SearchDbType sdbType = SearchDbType.DocDB;

            /*----------- 申请号校验位判断 begin---------------*/
            // 用正则式抓取
            if (_strSdbType.ToUpper().Equals("CN"))
            {
                //(19|20)[123]\d{9}\.?[\d|X|x]?
                Regex reg = new Regex(@"(\d{12}\.?[\d|x|X])\s*\/AN|AN\s+(\d{12}\.?[\d|x|X])", RegexOptions.IgnoreCase);
                MatchCollection mc = reg.Matches(strQuery);
                foreach (Match m in mc)
                {
                    string eachAn = m.Groups[1].Value;
                    if (eachAn == "" || eachAn == null)
                        eachAn = m.Groups[2].Value;
                    // 送验证
                    bool ifValidate = CnAppLicationNo.Check_ApNoAddVCode(eachAn);
                    if (!ifValidate)
                    {
                        strArryResutlMsg[0] = "申请号校验位错误";
                        return strArryResutlMsg;
                    }
                    string newAn = eachAn.Length == 13 ? eachAn.Substring(0, 12) : eachAn.Substring(0, eachAn.IndexOf('.'));
                    strQuery = strQuery.Replace(eachAn, newAn);
                }

                sdbType = SearchDbType.Cn;
            }
            /*----------- 申请号校验位判断 end---------------*/
            //HttpContext.Current.User.Identity.IsAuthenticated
            // 送检索           
            if (System.Web.HttpContext.Current.Session["UserID"] == null)
            {
                strArryResutlMsg[0] = "您的会话已退出,请重新登录!";
            }
            else
            {
                string strPageUName = System.Web.HttpContext.Current.Session["UserID"].ToString().Trim();
                SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                ResultInfo res = search.DoSearch(SearchInterface.XmPatentComm.strWebSearchGroupName, strQuery, Convert.ToInt32(strPageUName), sdbType);
                string strResutlMsg = res.HitMsg;
                if (strResutlMsg.IndexOf("<hits:") != -1 && _sDoSrc != "-1") // 返回结果正确
                {
                    // 设置检索历史    ***-1连接检索不记历史
                    //userInfor.addSearchHis(schPatItem);
                    Pattern.InsertPattern(Convert.ToInt32(strPageUName), Convert.ToByte(_sDoSrc), Convert.ToByte(sdbType.GetHashCode()),
                        res.SearchPattern.SearchNo, res.SearchPattern.Pattern, res.HitCount, DateTime.Now);
                }

                strArryResutlMsg[0] = strResutlMsg.Substring(strResutlMsg.IndexOf("(" + res.SearchPattern.SearchNo + ")"));
                strArryResutlMsg[1] = res.SearchPattern.SearchNo;
                strArryResutlMsg[2] = res.HitCount.ToString();
                //logger.Info(strResutlMsg);
            }

        }
        catch (Exception ex)
        {
            strArryResutlMsg[0] = "请求错误";
            logger.Error(ex.ToString());
        }
        return strArryResutlMsg;
    }
}
