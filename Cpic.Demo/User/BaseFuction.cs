using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Cpic.Cprs2010.User
{
    public class BaseFuction
    {
        /// <summary>
        /// 基本功能
        /// </summary>
        /// <returns></returns>
        public static DataTable GetFuctions()
        {
            DataTable dt = new DataTable();
            string sql = "select * from TbBaseFunction ";
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);
            return dt;
        }
    }
}
