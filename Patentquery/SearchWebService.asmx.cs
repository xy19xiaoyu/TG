using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Cpic.Cprs2010.Search;
using System.Data;

namespace Patentquery
{
    /// <summary>
    /// SearchWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SearchWebService : System.Web.Services.WebService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPattern"></param>
        /// <param name="UserID"></param>
        /// <param name="_SDbType"></param>
        /// <returns></returns>
        [WebMethod]
        public ResultInfoWebService Search(string strPattern, int UserID, int nSNo, SearchDbType _SDbType)
        {
            SearchPattern schPatItem = new Cpic.Cprs2010.Search.SearchPattern();

            schPatItem.SearchNo = nSNo.ToString().PadLeft(3, '0');  //检索编号
            schPatItem.Pattern = strPattern;      //检索式：F XX 2010/AD
            schPatItem.UserId = UserID;   //用户ID
            schPatItem.DbType = _SDbType;


            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            ResultInfo res = search.DoSearch(SearchInterface.XmPatentComm.strWebSearchGroupName, strPattern, Convert.ToInt32(UserID), schPatItem.SearchNo, _SDbType);


            // Cpic.Cprs2010.Search.ResultInfo res = Cpic.Cprs2010.Search.SearchManager.SearchFactory.CreatDoSearch(schPatItem);

            ResultInfoWebService resultInfoWebService = new ResultInfoWebService();
            resultInfoWebService.ResultInfo = res;

            ResultServices result = new ResultServices();
            string resultFilePath = result.getResultFilePath(schPatItem);
            resultInfoWebService.ResultSearchFilePath = resultFilePath;

            return resultInfoWebService;

        }
        [WebMethod]
        public user_menu_rights getRightList(int UserID, string LeiXing)
        {
            string rights = "";
            DataSet ds = new DataSet();
            ds = ProXZQDLL.UserRight.getUserRightDs(UserID.ToString(), LeiXing);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                rights = rights + "~" + ds.Tables[0].Rows[i]["PageName"].ToString().Trim();
            }
            rights = rights.TrimStart('~');
            user_menu_rights rigth = new user_menu_rights();
            rigth.user_id = UserID.ToString();
            rigth.menu_rights = rights;
            return rigth;
        }

    }

    public class user_menu_rights
    {
        public string user_id;
        public string menu_rights;
    }

}
