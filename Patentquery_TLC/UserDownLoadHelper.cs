using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBA;
namespace TLC
{
    public class UserDownLoadHelper
    {

        public static bool RecordDownload( List<int> ids,string type)
        {
            DataTable dt = new DataTable();
            DataColumn colid = new DataColumn("pid",typeof(int));
            DataColumn coltype = new DataColumn("type",typeof(string));

            dt.Columns.Add(colid);
            dt.Columns.Add(coltype);

            foreach (int i in ids)
            {
                DataRow row = dt.NewRow();
                row["pid"] = i;
                row["type"] = type;
                dt.Rows.Add(row);
            }

            using (SqlConnection con = SqlDbAccess.GetSqlConnection())
            {
                con.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(con, SqlBulkCopyOptions.CheckConstraints, null))
                {
                    copy.BatchSize = 5000;
                    copy.BulkCopyTimeout = 3000;
                    copy.DestinationTableName = "dbo.RecordDownload";
                    copy.ColumnMappings.Add("pid", "pid");
                    copy.ColumnMappings.Add("type", "type");
                    copy.WriteToServer(dt);
                }
                con.Close();
            }
            return true;
        }
        public static bool RecordDownload(DataTable dt)
        {
            using (SqlConnection con = SqlDbAccess.GetSqlConnection())
            {
                con.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(con, SqlBulkCopyOptions.CheckConstraints, null))
                {
                    copy.BatchSize = 5000;
                    copy.BulkCopyTimeout = 3000;
                    copy.DestinationTableName = "dbo.RecordDownload";
                    copy.ColumnMappings.Add("pid", "pid");
                    copy.ColumnMappings.Add("type", "type");
                    copy.ColumnMappings.Add("ipc1", "ipc1");
                    copy.ColumnMappings.Add("ipc3", "ipc3");
                    copy.ColumnMappings.Add("ipc4", "ipc4");
                    copy.ColumnMappings.Add("ipc7", "ipc7");
                    copy.ColumnMappings.Add("ipc", "ipc");
                    copy.ColumnMappings.Add("UserName", "UserName");
                    copy.WriteToServer(dt);
                }
                con.Close();
            }
            return true;
        }
    }
}
