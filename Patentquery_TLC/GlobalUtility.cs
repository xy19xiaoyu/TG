using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Web;

namespace TLC.BusinessLogicLayer {
    public static class GlobalUtility
    {
        //����Ӻ�
        public static string UrlEncodePlus(string url)
        {
            string strReturn = string.Empty;
            strReturn = url.Replace("+", "@");
            return strReturn;
        }

        //����Ӻ�
        public static string UrlDecodePlus(string url)
        {
            string strReturn = string.Empty;
            strReturn = url.Replace("@", "+");
            return strReturn;
        }

        //����һ�����ݵ�ǰʱ�����ɵ�����ַ���
        public static string TimeRandomName()
        {
            string strReturn = string.Empty;
            Random currentRandom = new Random();
            int intRandom = currentRandom.Next(0, 1000);
            strReturn = DateTime.Now.ToString("yyyyMMddHHmmssfff") + intRandom.ToString();
            return strReturn;
        }
    }
}
