using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpic.Cprs2010.User;

namespace ProXZQDLL
{
    public class ClsLog
    {
        public static int getFangWenLToDay()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result =( from item in db.TbLog
                         where item.ShiJian >=Convert.ToDateTime( DateTime.Now.ToShortDateString()) && item.ShiJian<=DateTime.Now
                         select item.IP +item.YongHuLeiXing).Distinct();

            return result.Count();

        }

        public static int getFangWenLZong()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = (from item in db.TbLog                          
                          select item.IP + item.YongHuLeiXing).Distinct();

            return result.Count();
        }

        public static List<TbLog> QueryLog(string dateStart, string dateEnd ,string YongHuLX,string UserName)
        {

            DateTime dtStart = new DateTime();
            DateTime dtEnd = new DateTime();
            try
            {
                dtStart = Convert.ToDateTime(dateStart);
            }
            catch (Exception ex)
            {
            }

            try
            {
                dtEnd = Convert.ToDateTime(dateEnd);
                dtEnd = dtEnd.AddDays(1);
            }
            catch (Exception ex)
            {
            }

            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.TbLog
                         where item.YongHuLeiXing.Contains(YongHuLX) && item.UserName.Contains(UserName ) 
                         select item;

            if (dateStart != "")
            {
                result = result.Where(a => a.ShiJian >= dtStart);
            }
            if (dateEnd != "")
            {
                result = result.Where(a => a.ShiJian < dtEnd);
            }

            result = result.OrderByDescending(a => a.ID);
            return result.ToList();
        }

        /// <summary>
        /// 栏目访问日志
        /// </summary>
        /// <param name="LanMu"></param>
        public static void LogInsertLanMu(string LanMu)
        {
            TbLog tb = new TbLog();
            tb.ShiJian = DateTime.Now;
            tb.LanMu = LanMu;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                db.TbLog.InsertOnSubmit(tb);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 栏目访问日志
        /// </summary>
        /// <param name="LanMu"></param>
        public static void LogInsertLanMu(System.Web.UI.Page page, string LanMu)
        {
            try
            {
                string IP = page.Request.UserHostAddress.ToString(); ;//HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
                ProXZQDLL.TbUser tbSesionUser = (ProXZQDLL.TbUser)page.Session["UserInfo"];
                LogInsert(IP, tbSesionUser.RealName, tbSesionUser.YongHuLeiXing, LanMu);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 记录栏目的访问日志
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="UserName"></param>
        /// <param name="YongHuLeiXing"></param>
        /// <param name="LanMu"></param>
        public static void LogInsert(string IP, string UserName, string YongHuLeiXing, string LanMu)
        {
            try
            {                
                TbLog tb = new TbLog();
                tb.ShiJian = DateTime.Now;
                tb.IP = IP;
                tb.UserName = UserName;
                tb.YongHuLeiXing = YongHuLeiXing;
                tb.LanMu = LanMu;

                string[] subIP = IP.Split('.');
                if (subIP.Length == 4)
                {
                    tb.DiQu = Stat.GetLocal(IP);
                }

                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    db.Log = Console.Out;
                    db.TbLog.InsertOnSubmit(tb);
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="UserName"></param>
        /// <param name="YongHuLeiXing"></param>
        public static void LogInsert(string IP, string UserName, string YongHuLeiXing)
        {
            TbLog tb = new TbLog();
            tb.ShiJian = DateTime.Now;
            tb.IP = IP;
            tb.UserName = UserName;
            tb.YongHuLeiXing = YongHuLeiXing;
            tb.LanMu = "登录";

            string[] subIP = IP.Split('.');
            if (subIP.Length == 4)
            {
                tb.DiQu = Stat.GetLocal(IP);
            }

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                db.TbLog.InsertOnSubmit(tb);
                db.SubmitChanges();
            }
        }

        public static List<TbSendMailLog> QuerySendMailLog(string ShouJianRen, string dateStart, string dateEnd)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from item in db.TbSendMailLog
                         where item.ShouJianRen.Contains(ShouJianRen)
                         select item;
            DateTime dStart = new DateTime();
            DateTime dEnd = new DateTime();

            try
            {
                dStart = Convert.ToDateTime(dateStart);
            }
            catch (Exception ex)
            { }

            try
            {
                dEnd = Convert.ToDateTime(dateEnd);
                dEnd = dEnd.AddDays(1);
            }
            catch (Exception ex)
            { }

            if (dateStart != "")
            {
                result = result.Where(a => a.FaSongShiJian >= dStart);
            }
            if (dateEnd != "")
            {
                result = result.Where(a => a.FaSongShiJian < dEnd);
            }


            return result.ToList();
        }

        public static void SendMailLogInsert(string ShouJianRen, string YouJianMingCheng, string ZhuanLiQuYu, string FaSongZhuangTai)
        {
            TbSendMailLog tb = new TbSendMailLog();
            tb.ShouJianRen = ShouJianRen;
            tb.YouJianMingCheng = YouJianMingCheng;
            tb.ZhuanLiQuYu = ZhuanLiQuYu;
            tb.FaSongShiJian = DateTime.Now;
            tb.FaSongZhuangTai = FaSongZhuangTai;

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                db.TbSendMailLog.InsertOnSubmit(tb);
                db.SubmitChanges();
            }

        }

        public static string getIPCountry(string strIP)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            long ip = (long)Dot2LongIP(strIP);

            var query = from item in db.IPTABLE
                        where item.StartIPNum <= ip && item.EndIPNum >= ip
                        select item;
            if (query.Count() <= 0)
            {
                return "";
            }

            return query.ToList()[0].Country;
        }

        public static double Dot2LongIP(string dotIP)
        {
            string[] subIP = dotIP.Split('.');
            if (subIP.Length != 4)
            {
                return 0;
            }

            long ip = 16777216 * Convert.ToInt64(subIP[0]) + 65536 * Convert.ToInt64(subIP[1]) + 256 * Convert.ToInt64(subIP[2]) + Convert.ToInt64(subIP[3]);

            ip = (Convert.ToInt64(subIP[0]) << 24) | (Convert.ToInt64(subIP[1]) << 16) | (Convert.ToInt64(subIP[2]) << 8) | Convert.ToInt64(subIP[3]);

            double s = Convert.ToDouble(subIP[0]) * 255d * 255d * 255d + Convert.ToDouble(subIP[1]) * 255d * 255d + Convert.ToDouble(subIP[2]) * 255d + Convert.ToDouble(subIP[3]);
            return (double)ip;
        }


        public static List<TbLegalUrl_Cfg> getTbLegal()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            
            var result = from item in db.TbLegalUrl_Cfg              
                         select item;

            return result.ToList();
        }

        public static bool  TbLegalOperate(string GuoBie, string Des, string Url)
        {
            bool flag = false;
            DataClasses1DataContext db = new DataClasses1DataContext();

            var result = from item in db.TbLegalUrl_Cfg
                         where item.CO==GuoBie
                         select item;

            if (result.Count() <= 0)
            {
              flag=  TbLegalInsert(GuoBie, Des, Url);
            }
            else
            {
              flag=  TbLegalUpdate(GuoBie, Des, Url);
            }
            return flag;
        }

        public static bool TbLegalDel(string GuoBie)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var tb = db.TbLegalUrl_Cfg.SingleOrDefault(o => o.CO == GuoBie);
            if (tb == null)
            {
                return false;
            }

            try
            {
                db.TbLegalUrl_Cfg.DeleteOnSubmit(tb);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private  static bool TbLegalInsert(string GuoBie, string Des, string Url)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            TbLegalUrl_Cfg tb = new TbLegalUrl_Cfg();
            tb.CO = GuoBie;
            tb.Des = Des;
            tb.LegUrl = Url;
                     
            try
            {
                db.TbLegalUrl_Cfg.InsertOnSubmit(tb);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private  static bool TbLegalUpdate(string GuoBie, string Des, string Url)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var tb = db.TbLegalUrl_Cfg.SingleOrDefault(o => o.CO == GuoBie);
            if (tb == null)
            {
                return false;
            }
            tb.Des = Des;
            tb.LegUrl = Url;

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

         
    }
}
