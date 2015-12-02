using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using ProXZQDLL;
using ProNewsDll;

namespace mobileAppWebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class CprsMobileExtSvc : System.Web.Services.WebService
    {
        
        [WebMethod(Description = "获取知识产权动态信息列表")]
        public List<NewsInfo> GetNewsList(int _pageNo, int _pageSize)
        {
            int newscount;

            List<NewsInfo> lstnews = NewsDB.GetNewsList(_pageNo, _pageSize,out newscount);



            return lstnews; 
        }

        [WebMethod(Description = "获取知识产权动态信息详情")]
        public List<NewsInfo> GetNewsInf(int _strSID)
        {
            List<NewsInfo> niList = new List<NewsInfo>();

            niList = NewsDB.GetNewsInfo(_strSID);

            return niList;
        }

        [WebMethod(Description = "获取我的咨询列表")]
        public List<QuestionsInfo> GetMyQuestionList(string _strUID, int _pageNo, int _pageSize)
        {
            int pagecount;
            List<QuestionsInfo> qi = new List<QuestionsInfo>();

            qi=QuestionDB.GetQuestionList(_pageNo,_pageSize,int.Parse(_strUID),out pagecount);


            return qi;
        }

        [WebMethod(Description = "获取我的咨询详情")]
        public List<QuestionsInfo> GetMyQuestionInf(int _strSID)
        {
            List<QuestionsInfo> qi = new List<QuestionsInfo>();

            qi = QuestionDB.GetQuestionInfo(_strSID);


            return qi;
        }

        [WebMethod(Description = "提交咨询")]
        public Boolean submitQuestion(string _strUID, string title, string content)
        {
            if (_strUID != null && !_strUID.Equals("")
                && title != null && !title.Equals("")
                && content != null && !content.Equals(""))
                if (QuestionDB.InsertQuestion(title, content, DateTime.Now, int.Parse(_strUID)) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            else
                return false;
        }
    }    
}