using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace Cpic.Cprs2010.Search
{
    /// <summary>
    /// CPRS 系统全局配置信息
    /// </summary>
    public static class CprsConfig
    {
        private static string _CPRS2010UserPath;
        /// <summary>
        /// 用户目录
        /// </summary>
        public static string CPRS2010UserPath
        {
            get
            {
                return _CPRS2010UserPath;
            }
        }

        private static string _cnIP;

        public static string CnIP
        {
            get { return _cnIP; }
        }

        private static string _cnPort;

        public static string CnPort
        {
            get { return _cnPort; }
        }
        private static string _DwpiIP;

        public static string DwpiIP
        {
            get { return _DwpiIP; }
        }
        private static string _DwpiPort;

        public static string DwpiPort
        {
            get { return _DwpiPort; }
        }
        private static string _DocdbIP;

        public static string DocdbIP
        {
            get { return _DocdbIP; }
        }
        private static string _DocdbPort;

        public static string DocdbPort
        {
            get { return _DocdbPort; }
        }
        public static int TimeOut
        {
            get { return _TimeOut; }
        }
        private static int _TimeOut;


        static CprsConfig()
        {
            try
            {
                Init();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 初始化配置信息
        /// </summary>
        public static void Init()
        {
            _CPRS2010UserPath = ConfigurationManager.AppSettings["CPRS2010UserPath"].ToString();
            _cnIP = System.Configuration.ConfigurationManager.AppSettings["cnIP"].ToString();
            _cnPort = System.Configuration.ConfigurationManager.AppSettings["cnPort"].ToString();
            _DwpiIP = System.Configuration.ConfigurationManager.AppSettings["DwpiIP"].ToString();
            _DwpiPort = System.Configuration.ConfigurationManager.AppSettings["DwpiPort"].ToString();
            _DocdbIP = System.Configuration.ConfigurationManager.AppSettings["DocdbIP"].ToString();
            _DocdbPort = System.Configuration.ConfigurationManager.AppSettings["DocdbPort"].ToString();
            _TimeOut = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SocketTimeOut"].ToString());
        }
        /// <summary>
        /// 用户检索历史,检索结果存放目录
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///   临时用户:id=000000  目录： 存放目录\0\00\000
        ///   注册用户:id=100000  目录： 存放目录\1\00\000
        /// </remarks>
        public static string GetUserPath(int UserId, string strGroup)
        {

            string Id = UserId.ToString().PadLeft(7, '0');
            return _CPRS2010UserPath + (string.IsNullOrEmpty(strGroup) ? "" : strGroup + "\\")
                + Id.Substring(0, 1) + "\\" + Id.Substring(1, 3) + "\\" + Id.Substring(4);


        }
    }
}
