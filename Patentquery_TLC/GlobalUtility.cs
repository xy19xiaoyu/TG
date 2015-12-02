using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Web;

namespace TLC.BusinessLogicLayer {
    public static class GlobalUtility
    {
        //编码加号
        public static string UrlEncodePlus(string url)
        {
            string strReturn = string.Empty;
            strReturn = url.Replace("+", "@");
            return strReturn;
        }

        //解码加号
        public static string UrlDecodePlus(string url)
        {
            string strReturn = string.Empty;
            strReturn = url.Replace("@", "+");
            return strReturn;
        }

        //生成一个根据当前时间生成的随机字符串
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
