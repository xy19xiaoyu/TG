using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace Cpic.Cprs2010.User
{
    /// <summary>
    /// ����ͳ����
    /// ���ߣ�������
    /// ����ʱ�䣺2010-03-17
    /// </summary>
    public class WebSiteStat
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("WebSiteStat");
        private static string strVirtualPath = "";

        public WebSiteStat()
        {
            //TODO
            //1.��ʼ������Ϣ �õ�����Ҫͳ�Ƶ�ҳ��

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
        /// һ���µĻỰ����  ����������һ
        /// </summary>
        /// <returns></returns>
        public static void AddSession()
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["��������"] = Convert.ToInt32(HttpContext.Current.Application["��������"].ToString()) + 1;
            HttpContext.Current.Application["�ܷ�������"] = Convert.ToInt32(HttpContext.Current.Application["�ܷ�������"].ToString()) + 1;
            if ((int)HttpContext.Current.Application["��������"] > (int)HttpContext.Current.Application["�����������"])
            {
                HttpContext.Current.Application["�����������"] = HttpContext.Current.Application["��������"];
            }
            HttpContext.Current.Application.UnLock();
            AuToSaveStat(Convert.ToInt32(HttpContext.Current.Application["�ܷ�������"].ToString()), Convert.ToInt32(HttpContext.Current.Application["�����������"].ToString()));

        }

        public static void RemoveSession()
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["��������"] = Convert.ToInt32(HttpContext.Current.Application["��������"].ToString()) - 1;
            HttpContext.Current.Application.UnLock();
            AuToSaveStat(Convert.ToInt32(HttpContext.Current.Application["�ܷ�������"].ToString()), Convert.ToInt32(HttpContext.Current.Application["�����������"].ToString()));
        }

        /// <summary>
        /// ����Ŀ¼
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
        /// ��վ��ʼ �����վ���ǵ�һ�η���  ���ȡ��ǰ��ͳ����Ϣ
        /// ����ǵ�һ�η�����������ͳ����ϢΪ��
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
                HttpContext.Current.Application["�ܷ�������"] = 0;
                HttpContext.Current.Application["�����������"] = 0;
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
                                log.Error("������Ϣ����");
                                throw new Exception("������Ϣ����");
                            }
                            listdata.Add(by);
                        }
                    }
                }
                HttpContext.Current.Application["�ܷ�������"] = System.BitConverter.ToInt32(listdata[0], 0);     //�ܷ�����                
                HttpContext.Current.Application["�����������"] = System.BitConverter.ToInt32(listdata[1], 0);     //�����������
            }
            HttpContext.Current.Application["��������"] = 0;
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
        /// ��վֹͣʱ
        /// </summary>
        public static void WebSiteEnd()
        {
            AuToSaveStat(Convert.ToInt32(HttpContext.Current.Application["�ܷ�������"].ToString()), Convert.ToInt32(HttpContext.Current.Application["�����������"].ToString()));
        }

    }
}
