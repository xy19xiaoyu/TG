
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProXZQDLL
{
    public class IpFillterSvs
    {
        /// <summary>
        /// 是否开启IP过滤
        /// </summary>
        public static bool bIpFillter = false;

        static IpFillterSvs()
        {
            CheckAndInitIpFillter();
        }

        private static void CheckAndInitIpFillter()
        {
            try
            {
                TbIP ipFillter = getTbIP("glbalIpKey");

                if (ipFillter != null)
                {
                    bIpFillter = ipFillter.flag == 0 ? true : false;
                }
                else
                {
                    UserRight.TbIpInsert("glbalIpKey", 1);
                    bIpFillter = false;
                }
            }
            catch (Exception ex)
            {
            }
        }


        public static TbIP getTbIP(string _strIp)
        {
            try
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                var result = from item in db.TbIP
                             where item.IP.Equals(_strIp)
                             select item;
                return result.First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static bool TbIpUp(string IP, int flag)
        {
            bool bRs = false;
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var tb = db.TbIP.First(o => o.IP == IP);
                    tb.flag = flag;
                    db.SubmitChanges();
                }
                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }
            return bRs;
        }

    }
}
