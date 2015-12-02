using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProNewsDll
{
    /// <summary>
    /// 新闻操作类
    /// </summary>
    public class NewsDB
    {
        /// <summary>
        /// 返回新闻列表
        /// </summary>
        /// <param name="_pageNo"></param>
        /// <param name="_pageSize"></param>
        /// <param name="pagCount"></param>
        /// <returns></returns>
        public static List<NewsInfo> GetNewsList(int _pageNo, int _pageSize,out int newscount)
        {
            List<NewsInfo> lstnews = new List<NewsInfo>();
            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = from a in db.NEWSINFO
                        orderby a.CREATEDATE descending
                        select new NewsInfo
                        {
                            NID=a.id,
                            Title=a.TITLE,
                            News_Content =a.NEWS_CONTENT,
                            Summary=a.SUMMARY,
                            CreateDate=a.CREATEDATE,
                            User=a.CREATEUSER
                        };
            newscount = query.Count();
            lstnews = query.Skip(_pageSize * (_pageNo - 1)).Take<NewsInfo>(_pageSize).ToList<NewsInfo>();
            return lstnews;
        }
        /// <summary>
        /// 根据新闻ID返回新闻详细信息
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public static List<NewsInfo> GetNewsInfo(int nid)
        {            
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from a in db.NEWSINFO
                         where a.id == nid
                         select new NewsInfo
                         {
                             NID = a.id,
                             Title = a.TITLE,
                             News_Content = a.NEWS_CONTENT,
                             Summary = a.SUMMARY,
                             CreateDate = a.CREATEDATE,
                             User = a.CREATEUSER
                         }; 

            return result.ToList();          
        }

        public static int InsertNews(string title, string summary, string content, DateTime createdate, string createuser)
        {
            NEWSINFO ni = new NEWSINFO();
            ni.TITLE = title;
            ni.SUMMARY = summary;
            ni.NEWS_CONTENT = content;
            ni.CREATEDATE = createdate;
            ni.CREATEUSER = createuser;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                db.NEWSINFO.InsertOnSubmit(ni);
                db.SubmitChanges();
            }
            return ni.id;
        }
    }

    /// <summary>
    /// 咨询操作类
    /// </summary>
    public class QuestionDB
    {
        /// <summary>
        /// 当前人咨询列表
        /// </summary>
        /// <param name="_pageNo"></param>
        /// <param name="_pageSize"></param>
        /// <param name="userid"></param>
        /// <param name="pagCount"></param>
        /// <returns></returns>
        public static List<QuestionsInfo> GetQuestionList(int _pageNo, int _pageSize, int userid, out int pagCount)
        {
            List<QuestionsInfo> lstnews = new List<QuestionsInfo>();
            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = from a in db.QuestionInfo
                        where a.CreateUser == userid
                        orderby a.CreateDate descending
                        select new QuestionsInfo
                        {
                           QID=a.ID,
                           Title=a.TITLE,
                           Content=a.QuestionContent,
                           CreateDate=a.CreateDate,
                           AnserContent=a.AnserContent,
                           AnserDate=a.AnserDate,
                           Status=a.Status                           
                        };
            pagCount = query.Count();
            lstnews = query.Skip(_pageSize * (_pageNo - 1)).Take<QuestionsInfo>(_pageSize).ToList<QuestionsInfo>();
            return lstnews;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pageNo"></param>
        /// <param name="_pageSize"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<QuestionsInfo> GetQuestionList(int _pageNo, int _pageSize,int status)
        {
            List<QuestionsInfo> lstnews = new List<QuestionsInfo>();
            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = from a in db.QuestionInfo
                        where a.Status==status
                        select new QuestionsInfo
                        {
                            QID = a.ID,
                            Title = a.TITLE,
                            Content = a.QuestionContent,
                            CreateDate = a.CreateDate,
                            AnserContent = a.AnserContent,
                            AnserDate = a.AnserDate,
                            Status = a.Status
                        };
            lstnews = query.Skip(_pageSize * (_pageNo - 1)).Take<QuestionsInfo>(_pageSize).ToList<QuestionsInfo>();
            return lstnews;

        }
        /// <summary>
        /// 咨询详细信息
        /// </summary>
        /// <param name="qid"></param>
        /// <returns></returns>
        public static List<QuestionsInfo> GetQuestionInfo(int qid)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = from a in db.QuestionInfo
                        where a.ID == qid
                        select new QuestionsInfo
                        {
                            QID = a.ID,
                            Title = a.TITLE,
                            Content = a.QuestionContent,
                            CreateDate = a.CreateDate,
                            CreateUser =a.CreateUser,
                            AnserContent = a.AnserContent,
                            AnserDate = a.AnserDate,
                            AnserUser=a.AnserUser,
                            Status = a.Status
                        };
            return query.ToList();          

        }
        /// <summary>
        /// 新增咨询
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="createdate"></param>
        /// <param name="createuser"></param>
        /// <returns></returns>
        public static int InsertQuestion(string title, string content, DateTime createdate,int createuser)
        {
            QuestionInfo qi = new QuestionInfo();
            qi.TITLE = title;
            qi.QuestionContent = content;
            qi.CreateUser = createuser;
            qi.CreateDate = createdate;
            qi.Status = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                db.QuestionInfo.InsertOnSubmit(qi);                
                db.SubmitChanges();
            }
            return qi.ID;
        }
        /// <summary>
        /// 修改咨询，回答咨询
        /// </summary>
        /// <param name="qid"></param>
        /// <param name="ansercontent"></param>
        /// <param name="anserdate"></param>
        /// <param name="anseruser"></param>
        /// <returns></returns>
        public static int UpdateQuestion(int qid, string ansercontent, DateTime anserdate, string anseruser)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                var tb = db.QuestionInfo.SingleOrDefault<QuestionInfo>(s => s.ID == qid);
                if (tb == null)
                {
                    return 0;
                }
                tb.AnserContent = ansercontent;
                tb.AnserDate = anserdate;
                tb.AnserUser = anseruser;
                tb.Status = 1;
                db.SubmitChanges();
            }
            return 1;
        }
    }

    public class NewsInfo
    {
        private int? _nid;
        public int? NID
        {
            get { return _nid; }
            set { _nid = value; }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _summary;
        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }
        private string _news_content;
        public string News_Content
        {
            get { return _news_content; }
            set { _news_content = value; }
        }
        private DateTime? _createdate;
        public DateTime? CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        private string _user;
        public string User
        {
            get { return _user; }
            set { _user = value; }
        }
    }

    public class QuestionsInfo
    {
        private int? _qid;
        public int? QID
        {
            get { return _qid; }
            set { _qid = value; }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        private DateTime? _createdate;
        public DateTime? CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        private int? _createuser;
        public int? CreateUser
        {
            get { return _createuser; }
            set { _createuser = value; }
        }
        private string _ansercontent;
        public string AnserContent
        {
            get { return _ansercontent; }
            set { _ansercontent = value; }
        }
        private DateTime? _anserdate;
        public DateTime? AnserDate
        {
            get { return _anserdate; }
            set { _anserdate = value; }
        }
        private string _anseruser;
        public string AnserUser
        {
            get{return _anseruser;}
            set{_anseruser=value;}
        }
        private int? _status;
        public int? Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
