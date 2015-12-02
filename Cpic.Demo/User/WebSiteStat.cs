using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace Cpic.Cprs2010.User
{
    /// <summary>
    /// 流量统计类
    /// 作者：陈晓雨
    /// 创建时间：2010-03-17
    /// </summary>
    public class WebSiteStat
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("WebSiteStat");
        private static string strVirtualPath = "";

        public WebSiteStat()
        {
            //TODO
            //1.初始配置信息 得到所有要统计的页面

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool AddRequest()
        {

            HttpContext.Current.Session["UserInfo"] = "xy1";
            HttpContext.Current.Application["AppInfo"] = "xx";
            return true;
        }
        /// <summary>
        /// 一次新的会话请求  在线人数加一
        /// </summary>
        /// <returns></returns>
        public static void AddSession()
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["在线人数"] = Convert.ToInt32(HttpContext.Current.Application["在线人数"].ToString()) + 1;
            HttpContext.Current.Application["总访问人数"] = Convert.ToInt32(HttpContext.Current.Application["总访问人数"].ToString()) + 1;
            if ((int)HttpContext.Current.Application["在线人数"] > (int)HttpContext.Current.Application["最多在线人数"])
            {
                HttpContext.Current.Application["最多在线人数"] = HttpContext.Current.Application["在线人数"];
            }
            HttpContext.Current.Application.UnLock();
            AuToSaveStat(Convert.ToInt32(HttpContext.Current.Application["总访问人数"].ToString()), Convert.ToInt32(HttpContext.Current.Application["最多在线人数"].ToString()));

        }

        public static void RemoveSession()
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["在线人数"] = Convert.ToInt32(HttpContext.Current.Application["在线人数"].ToString()) - 1;
            HttpContext.Current.Application.UnLock();
            AuToSaveStat(Convert.ToInt32(HttpContext.Current.Application["总访问人数"].ToString()), Convert.ToInt32(HttpContext.Current.Application["最多在线人数"].ToString()));
        }

        /// <summary>
        /// 构建目录
        /// </summary>
        /// <param name="_strPath"></param>
        /// <returns></returns>
        private static bool BuildPath(string _strPath)
        {
            string strPathTemp = _strPath; // getFilePath(AppFileSerialNo);

            if (string.IsNullOrEmpty(strPathTemp)) //strPathTemp.Equals("") || strPathTemp.Equals(strVirtualPath))
            {
                return false;
            }

            try
            {
                if (!Directory.Exists(strPathTemp))
                {
                    Directory.CreateDirectory(strPathTemp);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 网站开始 如果网站不是第一次发布  则读取以前的统计信息
        /// 如果是第一次发布，则所有统计信息为空
        /// </summary>
        public static void WebSiteStart()
        {
            strVirtualPath = System.Configuration.ConfigurationSettings.AppSettings["WebInfordPath"].ToString();

            if (strVirtualPath.StartsWith("~/"))
            {
                strVirtualPath = System.Web.HttpContext.Current.Server.MapPath(strVirtualPath);
            }
            BuildPath(strVirtualPath);

            if (!File.Exists(strVirtualPath+@"\WebSiteStat.Stat"))
            {
                HttpContext.Current.Application["总访问人数"] = 0;
                HttpContext.Current.Application["最多在线人数"] = 0;
            }
            else
            {
                List<byte[]> listdata = new List<byte[]>();
                using (FileStream sw = new FileStream(strVirtualPath + @"\WebSiteStat.Stat", FileMode.Open))
                {

                    int readCount;
                    
                    for (int i = 0; i <= 1; i++)
                    {
                        if (sw.CanRead)
                        {
                            byte[] by = new byte[4];
                            readCount = sw.Read(by, 0, 4);
                            if (readCount < 4)
                            {
                                log.Error("配置信息错误！");
                                throw new Exception("配置信息错误");
                            }
                            listdata.Add(by);
                        }
                    }
                }
                HttpContext.Current.Application["总访问人数"] = System.BitConverter.ToInt32(listdata[0], 0);     //总访问量                
                HttpContext.Current.Application["最多在线人数"] = System.BitConverter.ToInt32(listdata[1], 0);     //最多在线人数
            }
            HttpContext.Current.Application["在线人数"] = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zrs"></param>
        /// <param name="maxzx"></param>
        public static void AuToSaveStat(int zrs, int maxzx)
        {
            try
            {
                using (FileStream sw = new FileStream(strVirtualPath + @"\WebSiteStat.Stat", FileMode.OpenOrCreate))
                {

                    byte[] by;
                    sw.Seek(0, SeekOrigin.Begin);

                    by = System.BitConverter.GetBytes(zrs);
                    sw.Write(by, 0, by.Length);

                    by = System.BitConverter.GetBytes(maxzx);
                    sw.Write(by, 0, by.Length);

                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }

        /// <summary>
        /// 网站停止时
        /// </summary>
        public static void WebSiteEnd()
        {
            AuToSaveStat(Convert.ToInt32(HttpContext.Current.Application["总访问人数"].ToString()), Convert.ToInt32(HttpContext.Current.Application["最多在线人数"].ToString()));
        }

    }
}
