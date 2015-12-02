using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Collections;
using System.Linq.Expressions;
using System.IO;
using PatentWarnning;
using System.Data;
using TLC;

namespace ProYJDLL
{
    public class yjitem
    {
        private int? _W_ID;

        public int? W_ID
        {
            get { return _W_ID; }
            set { _W_ID = value; }
        }
        private int? _C_ID;

        public int? C_ID
        {
            get { return _C_ID; }
            set { _C_ID = value; }
        }
        private string _S_NAME;

        public string S_NAME
        {
            get { return _S_NAME; }
            set { _S_NAME = value; }
        }
        private string _ALIAS;

        public string ALIAS
        {
            get { return _ALIAS; }
            set { _ALIAS = value; }
        }
        //private DateTime? _C_DATE;

        //public DateTime? C_DATE
        //{
        //    get { return _C_DATE; }
        //    set { _C_DATE = value; }
        //}

        private DateTime _C_DATE;

        public DateTime C_DATE
        {
            get { return _C_DATE; }
            set { _C_DATE = value; }
        }


        private int? _CURRENTNUM;

        public int? CURRENTNUM
        {
            get { return _CURRENTNUM; }
            set { _CURRENTNUM = value; }
        }
        private int? _CHANGENUM;

        public int? CHANGENUM
        {
            get { return _CHANGENUM; }
            set { _CHANGENUM = value; }
        }
        private string _BEIZHU;

        public string BEIZHU
        {
            get { return _BEIZHU; }
            set { _BEIZHU = value; }
        }
        private int? _PERIOD;

        public int? PERIOD
        {
            get { return _PERIOD; }
            set { _PERIOD = value; }
        }
        private int? _STATUS;

        public int? STATUS
        {
            get { return _STATUS ; }
            set { _STATUS = value; }
        }
    }

    public class ShengShi
    {

        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Sheng;

        public string Sheng
        {
            get { return _Sheng; }
            set { _Sheng = value; }
        }

        private string _DaiMa;

        public string DaiMa
        {
            get { return _DaiMa; }
            set { _DaiMa = value; }
        }

        private string _DaiMaID;
        public string DaiMaID
        {
            get { return _DaiMaID; }
            set { _DaiMaID = value; }
        }
    }
    public class YJDB
    {
        /// <summary>
        /// 预警主表信息查询
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <param name="KeyValue"></param>
        /// <param name="C_TYPE"></param>
        /// <param name="type"></param>
        /// <param name="country"></param>
        /// <param name="pagIndex">当前页码，从1开始</param>
        /// <param name="pagSize">每页显示的记录数</param>
        /// <param name="pagCount">总的页数</param>
        /// <returns></returns>
        public static List<yjitem> getYJ(string KeyWord, string KeyValue, int C_TYPE, int type, string country, int pagIndex, int pagSize, out int pagCount, int userid)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var query = from a in db.C_W_SECARCH
                        join b in db.C_EARLY_WARNING on a.C_ID equals b.C_ID
                        where b.C_TYPE == C_TYPE && a.TYPE == type && b.dbsource == country && b.USER_ID == userid
                        orderby b.C_ID descending
                        select new yjitem
                        {
                            W_ID = a.W_ID,
                            C_ID = a.C_ID.Value,
                            S_NAME = a.S_NAME,
                            ALIAS = b.ALIAS,
                            C_DATE = b.C_DATE.Value,
                            CURRENTNUM = a.CURRENTNUM.Value,
                            CHANGENUM = a.CHANGENUM.Value,
                            BEIZHU = b.BEIZHU,
                            PERIOD = b.PERIOD,
                            STATUS=b.Status
                        };

            DateTime dt;
            switch (KeyWord)
            {
                case "0":
                    query = query.Where(a => a.S_NAME.Contains(KeyValue));
                    break;
                case "1":
                    try
                    {
                        dt = Convert.ToDateTime(KeyValue);
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                    query = query.Where(a => a.C_DATE >= dt && a.C_DATE < dt.AddDays(1));
                    break;
                case "2":
                    query = query.Where(a => a.ALIAS.Contains(KeyValue));
                    break;
            }
            pagCount = query.Count();

            return query.Skip(pagSize * (pagIndex - 1)).Take<yjitem>(pagSize).ToList<yjitem>();
        }


        /// <summary>
        /// 取得预警子项信息
        /// </summary>
        /// <param name="C_ID"></param>
        /// <returns></returns>
        public static List<C_W_SECARCH> getYJItem(int C_ID)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var query = from a in db.C_W_SECARCH
                        where a.C_ID == C_ID && a.TYPE == 0
                        select a;

            return query.ToList();
        }
        /// <summary>
        /// 取得预警子项信息
        /// </summary>
        /// <param name="C_ID"></param>
        /// <returns></returns>
        public static DataTable getYJItemxy(int C_ID)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var query = from a in db.C_W_SECARCH
                        where a.C_ID == C_ID && a.TYPE == 0
                        select a;
            DataTable dt = new DataTable();
            DataColumn col1 = new DataColumn("W_ID", typeof(string));
            DataColumn col2 = new DataColumn("S_NAME", typeof(string));
            DataColumn col3 = new DataColumn("CURRENTNUM", typeof(string));
            DataColumn col4 = new DataColumn("CHANGENUM", typeof(string));
            dt.Columns.Add(col1);
            dt.Columns.Add(col2);
            dt.Columns.Add(col3);
            dt.Columns.Add(col4);
            foreach (var x in query)
            {
                DataRow row = dt.NewRow();
                row["W_ID"] = x.W_ID;
                row["S_NAME"] = x.S_NAME;
                row["CURRENTNUM"] = x.CURRENTNUM;
                row["CHANGENUM"] = x.CHANGENUM;
                dt.Rows.Add(row);
            }
            return dt;
        }


        /// <summary>
        /// 取得预警子项信息
        /// </summary>
        /// <param name="C_ID"></param>
        /// <returns></returns>
        public static DataTable getYJItemHis(int W_ID)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var query = from a in db.C_W_SEARCHLIS
                        where a.HisOrder == W_ID && a.type == 0
                        select a;
            DataTable dt = new DataTable();
            DataColumn col1 = new DataColumn("W_ID", typeof(string));
            DataColumn col2 = new DataColumn("S_NAME", typeof(string));
            DataColumn col3 = new DataColumn("CURRENTNUM", typeof(string));
            DataColumn col4 = new DataColumn("CHANGENUM", typeof(string));
            dt.Columns.Add(col1);
            dt.Columns.Add(col2);
            dt.Columns.Add(col3);
            dt.Columns.Add(col4);
            foreach (var x in query)
            {
                DataRow row = dt.NewRow();
                row["W_ID"] = x.W_ID;
                row["S_NAME"] = x.S_NAME;
                row["CURRENTNUM"] = x.CURRENTNUM;
                row["CHANGENUM"] = x.CHANGENUM;
                dt.Rows.Add(row);
            }

            string sql = "";
            return dt;
        }


        /// <summary>
        /// 获取预警历史
        /// </summary>
        /// <param name="C_ID"></param>
        /// <returns></returns>
        public static List<yjitem> getYJHis(int C_ID, int pagIndex, int pagSize, out int pagCount)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var query = from a in db.C_EARLY_WARNING
                        join c in db.C_W_SEARCHLIS on a.C_ID equals c.C_ID
                        where c.type == 1 && a.C_ID == C_ID
                        select new yjitem
                        {
                            W_ID = c.HisOrder,
                            C_ID = a.C_ID,
                            ALIAS = a.ALIAS,
                            C_DATE = c.CHANGEDATE.Value,
                            CURRENTNUM = c.CURRENTNUM,
                            CHANGENUM = c.CHANGENUM,
                            BEIZHU = a.BEIZHU,
                            PERIOD = a.PERIOD
                        };

            pagCount = query.Count();
            return query.Skip(pagSize * (pagIndex - 1)).Take(pagSize).ToList(); ;
        }

        /// <summary>
        /// 预警信息主表新增
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ALIAS"></param>
        /// <param name="PERIOD"></param>
        /// <param name="C_TYPE"></param>
        /// <param name="dbsource"></param>
        /// <returns></returns>
        public static int YjInsert(int UserID, string ALIAS, int PERIOD, string BeiZhu, int C_TYPE, string dbsource,int status)
        {
            C_EARLY_WARNING tb = new C_EARLY_WARNING();

            tb.USER_ID = UserID;
            tb.ALIAS = ALIAS;
            tb.PERIOD = PERIOD;
            tb.BEIZHU = BeiZhu;
            tb.C_DATE = DateTime.Now;
            tb.C_TYPE = C_TYPE;
            tb.dbsource = dbsource;
            tb.Status = status;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                db.C_EARLY_WARNING.InsertOnSubmit(tb);
                db.SubmitChanges();
            }

            return tb.C_ID;
        }

        /// <summary>
        /// 预警信息主表修改
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="ALIAS"></param>
        /// <param name="PERIOD"></param>
        /// <param name="C_TYPE"></param>
        /// <param name="dbsource"></param>
        /// <returns></returns>
        public static int YjUpdate(int CID, string ALIAS, int PERIOD, string BeiZhu, int C_TYPE, string dbsource)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                var tb = db.C_EARLY_WARNING.SingleOrDefault<C_EARLY_WARNING>(s => s.C_ID == CID);
                if (tb == null)
                {
                    return 0;
                }
                tb.ALIAS = ALIAS;
                tb.PERIOD = PERIOD;
                tb.BEIZHU = BeiZhu;
                tb.C_DATE = DateTime.Now;
                tb.C_TYPE = C_TYPE;
                tb.dbsource = dbsource;

                db.SubmitChanges();
            }

            return 1;
        }
        /// <summary>
        /// 预警信息主表修改
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="ALIAS"></param>
        /// <param name="PERIOD"></param>
        /// <param name="C_TYPE"></param>
        /// <param name="dbsource"></param>
        /// <returns></returns>
        public static int YjUpdate(int CID, int status)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                var tb = db.C_EARLY_WARNING.SingleOrDefault<C_EARLY_WARNING>(s => s.C_ID == CID);
                if (tb == null)
                {
                    return 0;
                }
                tb.Status = status;

                db.SubmitChanges();
            }

            return 1;
        }
        /// <summary>
        /// 删除主表信息
        /// </summary>
        /// <param name="C_ID"></param>
        /// <returns></returns>
        public static int YjDelete(int C_ID)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var tb = db.C_EARLY_WARNING.Where(o => o.C_ID == C_ID);
                db.C_EARLY_WARNING.DeleteAllOnSubmit(tb);
                db.SubmitChanges();
            }
            return 1;
        }

        /// <summary>
        /// 预警信息从表新增
        /// </summary>
        /// <param name="C_ID"></param>
        /// <param name="S_NAME"></param>
        /// <param name="TYPE"></param>
        public static string YjInsertItem(int C_ID, string S_NAME, string S_Name2, string GuoJia, string ShiJie, int TYPE, string C_TYPE, string country,string hangyeid,string keytopvalue)
        {
            string patent = "";
            C_W_SECARCH cwSearch = new C_W_SECARCH();
            cwSearch.C_ID = C_ID;
            cwSearch.S_NAME = S_NAME;
            cwSearch.TYPE = TYPE;
            cwSearch.NID = hangyeid;
            string pattern = "";
            string pattern1 = "";
            List<int> lstZhuanTi = new List<int>();
            string zhiliang = "";
            switch (C_TYPE.Substring(0,1))
            {
                case "1"://行业
                    //专题库中取出检索式
                    //string cnpfile=ztHelper.GetZTCNP(hangyeid,country,"0");
                    //cwSearch.SEARCHFILE =
                    //lstZhuanTi=ztHelper.GetResultList(hangyeid, country, "0");
                    pattern = "";
                   //S_NAME = S_NAME.Replace("（", " ").Replace("）", " ").Replace("."," ");
                    //cwSearch.PATTERN = "F XX (" + S_NAME + "/PA)";
                    break;
                case "2"://申请人
                    if (!string.IsNullOrEmpty(hangyeid))
                    {
                        S_NAME = S_NAME.Substring(S_NAME.IndexOf("(") + 1, S_NAME.IndexOf(")") - S_NAME.IndexOf("(") - 1);
                        //cwSearch.S_NAME = S_NAME;
                    }
                    pattern = S_NAME + "/PA";
                    //cwSearch.PATTERN = "F XX (" + S_NAME + "/PA)";
                    //S_NAME = S_NAME.Replace("（", " ").Replace("）", " ").Replace(".", " ");
                    //cwSearch.PATTERN = "F XX (" + S_NAME + "/IC)";
                    break;
                case "3"://区域
                    if (country == "CN")
                    {
                        string shi = S_NAME.Substring(S_NAME.IndexOf("(") + 1, S_NAME.IndexOf(")") - S_NAME.IndexOf("(") - 1);
                        string sheng = S_NAME.Substring(0, S_NAME.IndexOf("("));

                        shi = shi.TrimEnd('市');
                        sheng = sheng.TrimEnd('省').TrimEnd('市');
                        string DanLieShi = getJiHuaDanLieShi(shi);
                        if (DanLieShi != "")//计划单列市
                        {
                            //cwSearch.PATTERN = "F XX (" + DanLieShi + "/CO)";
                            pattern = DanLieShi + "/CO";
                        }
                        else
                        {
                            //cwSearch.PATTERN = "F XX ((" + shi + "/DZ)*(" + getJiHuaDanLieShi(sheng) + "/CO))";
                            pattern = "(" + shi + "/DZ)*(" + getJiHuaDanLieShi(sheng) + "/CO)";
                        }
                    }

                    if (country == "EN")
                    {
                        string guoJiaEn = S_NAME.Substring(0, S_NAME.IndexOf("("));
                        string IPCEn = S_NAME.Substring(S_NAME.IndexOf("(") + 1, S_NAME.IndexOf(")") - S_NAME.IndexOf("(") - 1);
                        pattern = "(" + IPCEn + "/IC)@CO=" + getShiJieCode(guoJiaEn);
                        //cwSearch.PATTERN = "F XX (" + IPCEn + "/IC)@CO=" + getShiJieCode(guoJiaEn) + "";// "F XX (" + S_NAME + "/CO)";
                    }
                    //S_NAME = S_NAME.Replace("（", " ").Replace("）", " ").Replace(".", " ");
                    //cwSearch.PATTERN = "F XX (" + S_NAME + "/IN)";
                    break;
                case "4"://发明人
                    S_NAME = S_NAME.Replace("（", " ").Replace("）", " ").Replace(".", " ");
                    pattern = S_NAME + "/IN";
                    //cwSearch.PATTERN = "F XX (" + S_NAME + "/IN)";
                    break;
                case "5"://来华
                    S_NAME = S_NAME.Replace("（", " ").Replace("）", " ").Replace(".", " ");
                    cwSearch.PATTERN = C_TYPE.Substring(0,1);
                    cwSearch.PATTERN = "F XX ((" + S_NAME + "/PA)*(" + GuoJia + "/CO))";
                    break;
                case "6"://自定义
                    cwSearch.PATTERN = S_NAME;
                    break;
                default:
                    cwSearch.PATTERN = C_TYPE.Substring(0,1);
                    break;

            }
            //子项
            if (TYPE == 0)
            {
                cwSearch.S_NAME = keytopvalue;
                switch (C_TYPE.Substring(1, 1))
                {
                    case "0"://专利投入
                        break;
                    case "1"://成果                       
                        break;
                    case "2"://市场重心
                        if (country == "CN")
                        {
                            string shi = keytopvalue.Substring(keytopvalue.IndexOf("(") + 1, keytopvalue.IndexOf(")") - keytopvalue.IndexOf("(") - 1);
                            string sheng = keytopvalue.Substring(0, keytopvalue.IndexOf("("));

                            shi = shi.TrimEnd('市');
                            sheng = sheng.TrimEnd('省').TrimEnd('市');
                            string DanLieShi = getJiHuaDanLieShi(shi);
                            if (DanLieShi != "")//计划单列市
                            {
                                //cwSearch.PATTERN = "F XX (" + DanLieShi + "/CO)";
                                pattern1 = DanLieShi + "/CO";
                            }
                            else
                            {
                                //cwSearch.PATTERN = "F XX ((" + shi + "/DZ)*(" + getJiHuaDanLieShi(sheng) + "/CO))";
                                pattern1 = "(" + shi + "/DZ)*(" + getJiHuaDanLieShi(sheng) + "/CO)";
                            }
                        }

                        if (country == "EN")
                        {
                            string guoJiaEn = keytopvalue.Substring(0, keytopvalue.IndexOf("("));
                            string IPCEn = keytopvalue.Substring(keytopvalue.IndexOf("(") + 1, keytopvalue.IndexOf(")") - keytopvalue.IndexOf("(") - 1);
                            pattern1 = "(" + IPCEn + "/IC)@CO=" + getShiJieCode(guoJiaEn);
                            //cwSearch.PATTERN = "F XX (" + IPCEn + "/IC)@CO=" + getShiJieCode(guoJiaEn) + "";// "F XX (" + S_NAME + "/CO)";
                        }
                        break;
                    case "3"://技术重心
                        keytopvalue = keytopvalue.Replace("（", " ").Replace("）", " ").Replace(".", " ");
                        //cwSearch.PATTERN = "F XX (" + S_NAME + "/IC)";
                        pattern1 = keytopvalue + "/IC"; ;
                        break;
                    case "4"://申请人
                        keytopvalue = keytopvalue.Replace("（", " ").Replace("）", " ").Replace(".", " ");
                        pattern1 = keytopvalue + "/PA";

                        break;
                    case "5"://研发人才
                        keytopvalue = keytopvalue.Replace("（", " ").Replace("）", " ").Replace(".", " ");
                        pattern1 = keytopvalue + "/IN";
                        break;
                    case "6"://质量
                        zhiliang = "";
                        switch (keytopvalue)
                        {
                            case "有效发明公开":
                                zhiliang = "@LX=DI@YX";
                                break;
                            case "有效实用新型授权":
                                zhiliang = "@LX=UM@YX";
                                break;
                            case "有效外观设计授权":
                                zhiliang = "@LX=DP@YX";
                                break;
                            case "有效发明授权":
                                zhiliang = "@LX=AI@YX";
                                break;
                            case "失效发明公开":
                                zhiliang = "@LX=DI@SX";
                                break;
                            case "失效实用新型授权":
                                zhiliang = "@LX=UM@SX";
                                break;
                            case "失效外观设计授权":
                                zhiliang = "@LX=WG@SX";
                                break;
                            case "失效发明授权":
                                zhiliang = "@LX=AI@SX";
                                break;
                        }
                        break;
                    case "7"://寿命
                        break;
                    case "8"://来华
                        break;
                    case "9"://自定义
                        break;
                }
            }
            else
            {
                pattern1 = keytopvalue;
            }

            if ((pattern1 != "") && (pattern!=""))
            {
                cwSearch.PATTERN = "F XX (" + pattern + ")*(" + pattern1 + ")";                
            }
            else if (pattern1 != "")
            {
                if (C_TYPE.Substring(1, 1) != "6")
                {
                    cwSearch.PATTERN = "F XX (" + pattern1 + ")";
                }
            }
            else if (pattern != "")
            {
                if(country=="EN")//世界专利
                {
                    cwSearch.PATTERN = "F XX " + pattern;
                }else
                {
                    cwSearch.PATTERN = "F XX (" + pattern + ")";
                }
            }

            if (C_TYPE.Substring(1, 1) == "1" && !string.IsNullOrEmpty(cwSearch.PATTERN))//成果
            {
                cwSearch.PATTERN += "@LX=UM,DP,AI";
            }
            if (C_TYPE.Substring(1, 1) == "6" && !string.IsNullOrEmpty(cwSearch.PATTERN))//质量
            {
                cwSearch.PATTERN += zhiliang;
            }
            else if (C_TYPE.Substring(1, 1) == "6")
            {
                patent = keytopvalue;
                 
                cwSearch.PATTERN = patent;
                
            }
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                db.C_W_SECARCH.InsertOnSubmit(cwSearch);
                db.SubmitChanges();
            }
            if (!string.IsNullOrEmpty(cwSearch.PATTERN))
            {
                patent = cwSearch.PATTERN.Replace("F XX", "");
            }
            
            return patent;
        }

        /// <summary>
        /// 判断传入城市是否是计划单列市
        /// </summary>
        /// <param name="SNAME"></param>
        /// <returns></returns>
        private static string getJiHuaDanLieShi(string SNAME)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.CountryConfig
                         // where item.leixing == 2
                         select item;
            foreach (var tmp in result)
            {
                if (tmp.MingCheng.Contains(SNAME))
                {
                    return tmp.DaiMa;
                }
            }
            return "";
        }

        /// <summary>
        /// 删除从表信息子项信息
        /// </summary>
        /// <param name="C_ID"></param>
        /// <returns></returns>
        public static int YjDeleteItem(int C_ID)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var tb = db.C_W_SECARCH.Where(o => o.C_ID == C_ID);
                db.C_W_SECARCH.DeleteAllOnSubmit(tb);
                db.SubmitChanges();
            }
            return 1;
        }
        /// <summary>
        /// 删除从表信息所有信息
        /// </summary>
        /// <param name="C_ID"></param>
        /// <returns></returns>
        public static int YjDeleteItemAll(int C_ID)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var tb = db.C_W_SECARCH.Where(o => o.C_ID == C_ID);
                db.C_W_SECARCH.DeleteAllOnSubmit(tb);
                db.SubmitChanges();
            }
            return 1;
        }

        /// <summary>
        /// 根据CID取主表信息
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public static List<C_EARLY_WARNING> getYJByCID(int CID)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.C_EARLY_WARNING
                         where item.C_ID == CID
                         select item;

            return result.ToList();
        }

        /// <summary>
        /// 根据CID取从表子项信息
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public static List<C_W_SECARCH> getYJItemByCID(int CID)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.C_W_SECARCH
                         where item.C_ID == CID && item.TYPE == 0
                         select item;

            return result.ToList();
        }


        /// <summary>
        /// 根据CID取从表信息
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public static List<C_W_SECARCH> getYJItemByCIDAll(int CID)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.C_W_SECARCH
                         where item.C_ID == CID
                         select item;

            return result.ToList();
        }

        public static void InsertYJItem(List<C_W_SECARCH> lst)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            C_W_SECARCH tb = new C_W_SECARCH();
            foreach (var item in lst)
            {
                tb = item;
                db.C_W_SECARCH.InsertOnSubmit(tb);
                db.SubmitChanges();

            }
        }
        /// <summary>
        /// 热点预警
        /// </summary>
        /// <param name="C_TYPE">
        /// 定制类型
        /// 1、竞争对手
        /// 2、技术动向
        /// 3、发明人动向
        /// 4、区域分布
        /// 5、来华专利布局
        /// 6、高级定制</param>
        /// <param name="DbSource">
        /// CN 中国专利
        /// DOC 世界专利</param>
        /// <returns></returns>
        public static DataTable getReDianYuJing(int C_TYPE, string DbSource)
        {
            string sql = "select top 10 b.S_NAME, COUNT(*) as ct from C_EARLY_WARNING a, C_W_SECARCH b Where C_TYPE=" + C_TYPE + " And dbsource='" + DbSource + "' and a.C_ID=b.C_ID And b.type=0 Group by b.S_NAME Order by COUNT(*) DESC";
            return DBA.DbAccess.GetDataTable(CommandType.Text, sql);

        }
        /// <summary>
        /// 读取数据库中的二进制文件
        /// </summary>
        /// <param name="WID"></param>
        /// <param name="flag">0：原数量 1：差异数量</param>
        /// <returns></returns>
        public static List<int> getYJItemByWID(int WID, int flag, int pagIndex, int pagSize)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.C_W_SECARCH
                         where item.W_ID == WID
                         select item;

            if (result.Count() <= 0)
            {
                return null;
            }
            MemoryStream ms;
            byte[] bt = null;
            if (flag == 0)//原数量
            {
                ms = new MemoryStream(result.ToList()[0].SEARCHFILE.ToArray());
                bt = ms.ToArray();
            }
            if (flag == 1)//差异
            {
                ms = new MemoryStream(result.ToList()[0].COMPAREFILE.ToArray());
                bt = ms.ToArray();
            }
            List<int> lstRs = ConvertLstByte.GetCnpList(bt);

            return lstRs.Skip(pagSize * (pagIndex - 1)).Take(pagSize).ToList<int>();
        }
        /// <summary>
        /// 读取数据库中的二进制文件
        /// </summary>
        /// <param name="WID"></param>
        /// <param name="flag">0：原数量 1：差异数量</param>
        /// <returns></returns>
        public static List<int> getYJItemByWID(int WID, int flag)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.C_W_SECARCH
                         where item.W_ID == WID
                         select item;

            if (result.Count() <= 0)
            {
                return null;
            }
            MemoryStream ms;
            byte[] bt = null;
            if (flag == 0)//原数量
            {
                ms = new MemoryStream(result.ToList()[0].SEARCHFILE.ToArray());
                bt = ms.ToArray();
            }
            if (flag == 1)//差异
            {
                ms = new MemoryStream(result.ToList()[0].COMPAREFILE.ToArray());
                bt = ms.ToArray();
            }
            List<int> lstRs = ConvertLstByte.GetCnpList(bt);

            return lstRs;
        }
        /// <summary>
        /// 获取IPC 
        /// </summary>
        /// <param name="IPC"></param>
        /// <returns></returns>
        public static string getIPC(string IPC)
        {
            string sResult = "";
            DataClasses1DataContext db = new DataClasses1DataContext();

            var result = (from item in db.sysTree
                          where item.des.StartsWith(IPC) && item.type == "IPC"
                          select item).Take(20);

            foreach (var i in result)
            {
                sResult += i.des.ToString().Trim() + ",";
            }
            sResult = sResult.TrimEnd(',');

            return sResult;
        }

        /// <summary>
        /// 申请人
        /// </summary>
        /// <param name="ShenQingRen"></param>
        /// <returns></returns>
        public static string getApplicant(string ShenQingRen)
        {
            string sResult = "";
            DataClasses1DataContext db = new DataClasses1DataContext();

            var result = (from item in db.ApplicantYJ
                          where item.appl.Contains(ShenQingRen)
                          select item.appl).Take(20);

            foreach (var i in result)
            {
                sResult += i.ToString().Trim() + ",";
            }
            sResult = sResult.TrimEnd(',');

            return sResult;
        }
        /// <summary>
        /// 发明人
        /// </summary>
        /// <param name="ShenQingRen"></param>
        /// <returns></returns>
        public static string getInventor(string FaMingRen)
        {
            string sResult = "";
            DataClasses1DataContext db = new DataClasses1DataContext();

            var result = (from item in db.InventorYJ
                          where item.inventor.Contains(FaMingRen)
                          select item.inventor).Take(20);

            foreach (var i in result)
            {
                sResult += i.ToString().Trim() + ",";
            }
            sResult = sResult.TrimEnd(',');

            return sResult;
        }

        /// <summary>
        /// 区域预警
        /// </summary>
        /// <param name="quyu"></param>
        /// <returns></returns>
        public static string getShenShi(int provincialID, string quyu)
        {
            string sResult = "";
            DataClasses1DataContext db = new DataClasses1DataContext();


            var resultShi = from item in db.city
                            where item.provincialID == provincialID && item.cityName.StartsWith(quyu)
                            select item;
            foreach (var i in resultShi)
            {
                sResult += i.cityName.ToString().Trim() + ",";
            }

            sResult = sResult.TrimEnd(',');

            return sResult;
        }

        /// <summary>
        /// 省份信息
        /// </summary>
        /// <returns></returns>
        public static List<ShengShi> getSheng()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var result = from item in db.provincial
                         select new ShengShi
                         {
                             ID = item.provincialID.ToString(),
                             DaiMaID = item.DaiMa + item.provincialID.ToString(),
                             Sheng = item.provincialName
                         };


            return result.ToList();
        }

        /// <summary>
        /// 来华专利国家信息
        /// </summary>
        /// <returns></returns>
        public static List<ShengShi> getGuoJia()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var result = from item in db.CountryConfig
                         where item.leixing == 0
                         orderby item.MingCheng ascending
                         select new ShengShi
                         {
                             ID = item.id.ToString(),
                             DaiMaID = item.DaiMa + item.id.ToString(),
                             Sheng = item.MingCheng
                         };


            return result.ToList();

        }


        /// <summary>
        /// EN 区域分布
        /// </summary>
        /// <param name="ShenQingRen"></param>
        /// <returns></returns>
        public static string getCountryCN(string country)
        {
            string sResult = "";
            DataClasses1DataContext db = new DataClasses1DataContext();

            var result = (from item in db.CountryConfig
                          where (item.MingCheng.Contains(country) || item.DaiMa.StartsWith(country)) && item.leixing == 0
                          select "(" + item.DaiMa + ")" + item.MingCheng).Distinct().Take(20);

            foreach (var i in result)
            {
                sResult += i.ToString().Trim() + ",";
            }
            sResult = sResult.TrimEnd(',');

            return sResult;
        }

        
        public static List<ShengShi> getShiJie()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var result = from item in db.TbShiJie
                         select new ShengShi
                         {
                             ID = item.DaiMa.ToString(),
                             DaiMaID = item.DaiMa.ToString(),
                             Sheng = item.MingCheng
                         };


            return result.ToList();
        }

        public static string getShiJieCode(string contury)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var result = from item in db.TbShiJie
                         where item.MingCheng == contury
                         select item.DaiMa;

            if (result.Count() <= 0)
            {
                return "";
            }

            return result.ToList()[0].ToString().Trim();
        }
        /// <summary>
        /// 得到某一个预警检索式的结果列表
        /// </summary>
        /// <param name="W_ID">检索式ID 数据库W_ID</param>
        /// <param name="country">数据类别：CN,EN</param>
        /// <param name="isUpdate">0:当前数据，1:更新数据</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">每页多少</param>
        /// <returns></returns>
        public static List<int> getSearchList(string W_ID, string country, string isUpdate, int PageIndex, int PageSize)
        {
            return new List<int>();
        }

    }
}
