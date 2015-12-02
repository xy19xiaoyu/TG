using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProXZQDLL
{
    public class ClsYongHuLX
    {
        private string _Key;
        public string Key
        {
            set { _Key = value; }
            get { return _Key; }

        }
        private int _count;
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }
    }
    public class ClsTJ
    {
        /// <summary>
        /// 用户类型统计
        /// </summary>
        /// <returns></returns>
        public static List<ClsYongHuLX> getYongHuLX()
        {

            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = from item in db.TbUser
                        group item by item.YongHuLeiXing into g
                        select new ClsYongHuLX
                        {
                            Key = g.Key,
                            Count = g.Count()
                        };

            return query.ToList<ClsYongHuLX>();
        }

        /// <summary>
        /// 访问模块统计
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static DataSet getLanMu(string dateStart, string dateEnd)
        {
            DataSet ds = new DataSet();
            string sql = "select LanMu,COUNT(*) as ShuLiang from TbLog Where LanMu is not null ";
         

            if (dateStart != "")
            {
                sql += "And ShiJian>='" + dateStart + "' ";
            }
            if (dateEnd != "")
            {
                sql += "And ShiJian<'" + dateEnd + "' ";
            }
              sql += "group by LanMu ";
            ds = DBA.SqlDbAccess.GetDataSet(CommandType.Text, sql);

            return ds;

        }
    }
}
