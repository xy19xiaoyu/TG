using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection; // 引用这个才能使用Missing字段
using Microsoft.Office.Interop;
using System.IO;
using System.Diagnostics;
using System.Web;
using System.Threading;

namespace ExcelLib
{
    public class ExcelHelper
    {
        public ExcelHelper()
        {

        }
        public bool DataSet2ExcelFile(DataSet ds, string OpenFileName, string SaveFileName)
        {
            string FilePath = SaveFileName;
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }
            if (File.Exists(FilePath)) File.Delete(FilePath);


            //创建Application对象
            Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

            xApp.Visible = false;
            //Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(@"C:\医药制造业\Sample.xls",
            //得到WorkBook对象, 可以用两种方式之一: 下面的是打开已有的文件
            Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(OpenFileName,

            Missing.Value, Missing.Value, Missing.Value, Missing.Value
            , Missing.Value, Missing.Value, Missing.Value, Missing.Value
            , Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //xBook=xApp.Workbooks.Add(Missing.Value);//新建文件的代码
            //指定要操作的Sheet，两种方式：

            Microsoft.Office.Interop.Excel.Worksheet xSheetSample;
            List<int> savesheetid = new List<int>();
            foreach (DataTable dt in ds.Tables)
            {
                string tablename = dt.TableName;
                xSheetSample = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[tablename];

                //利用二维数组批量写入       ;
                string[,] ss = new string[dt.Rows.Count + 1, dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ss[0, i] = dt.Columns[i].ColumnName;
                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[j][k].ToString()))
                        {
                            ss[j + 1, k] = "";
                        }
                        else
                        {
                            if (dt.Rows[j][k].ToString().Length > 3000)
                            {
                                ss[j + 1, k] = dt.Rows[j][k].ToString().Substring(0, 3000);
                            }
                            else
                            {
                                ss[j + 1, k] = dt.Rows[j][k].ToString();
                            }

                        }

                    }
                }

                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)xSheetSample.Cells[1, 1];
                range = range.get_Resize(dt.Rows.Count + 1, dt.Columns.Count);
                range.Value2 = ss;
                range.Value2 = range.Value2;
            }
            int sheetcount = xBook.Sheets.Count;
            xApp.DisplayAlerts = false;
            for (int i = sheetcount; i >= 1; i--)
            {
                if (!savesheetid.Contains(i))
                {
                    ((Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[i]).Delete();
                }
            }
            xApp.DisplayAlerts = false;


            //保存方式一：保存WorkBook
            xBook.SaveAs(FilePath,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);

            ////保存方式二：保存WorkSheet
            //xSheet.SaveAs(@"C:\CData2.xls",
            //Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //保存方式三
            xBook.Save();
            xSheetSample = null;
            xBook = null;
            xApp.Quit(); //这一句是非常重要的，否则Excel对象不能从内存中退出
            xApp = null;
            GC.Collect();
            KillProcess("EXCEL");
            System.Threading.Thread.Sleep(1000 * 3);
            return true;
        }
        private void KillProcess(string processName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程
            try
            {
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();
                    }
                }
            }
            catch (Exception Exc)
            {
                throw new Exception("", Exc);
            }
        }
        public bool DataTable2ExcelFile(DataTable dt, string filename)
        {
            string FilePath = filename;
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }
            if (File.Exists(FilePath)) File.Delete(FilePath);


            //创建Application对象
            Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

            xApp.Visible = false;
            //Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(@"C:\医药制造业\Sample.xls",
            //得到WorkBook对象, 可以用两种方式之一: 下面的是打开已有的文件
            Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks.Add(Missing.Value);

            Microsoft.Office.Interop.Excel.Worksheet xSheetSample = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];



            //利用二维数组批量写入       ;
            string[,] ss = new string[dt.Rows.Count + 1, dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ss[0, i] = dt.Columns[i].ColumnName;
            }

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[j][k].ToString()))
                    {
                        ss[j + 1, k] = "";
                    }
                    else
                    {
                        if (dt.Rows[j][k].ToString().Length > 800)
                        {
                            ss[j + 1, k] = "'" +dt.Rows[j][k].ToString().Substring(0, 800);
                        }
                        else
                        {
                            ss[j + 1, k] = "'" + dt.Rows[j][k].ToString();
                        }

                    }

                }
            }

            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)xSheetSample.Cells[1, 1];
            range = range.get_Resize(dt.Rows.Count + 1, dt.Columns.Count);
            range.Value2 = ss;
            //range.Value2 = range.Value2;

            //保存方式一：保存WorkBook
            xBook.SaveAs(FilePath,
            Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);

            ////保存方式二：保存WorkSheet
            //xSheet.SaveAs(@"C:\CData2.xls",
            //Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //保存方式三
            xBook.Save();
            xSheetSample = null;
            xBook = null;
            xApp.Quit(); //这一句是非常重要的，否则Excel对象不能从内存中退出
            xApp = null;
            GC.Collect();
            //KillProcess("EXCEL");
            //System.Threading.Thread.Sleep(1000 * 3);
            return true;
        }
        public bool DataTable2ExcelFile(DataTable dt, string filename, string Samplename)
        {
            string FilePath = filename;
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }
            if (File.Exists(FilePath)) File.Delete(FilePath);


            //创建Application对象
            Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

            xApp.Visible = false;
            //Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(@"C:\医药制造业\Sample.xls",
            //得到WorkBook对象, 可以用两种方式之一: 下面的是打开已有的文件
            Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(Samplename,

            Missing.Value, Missing.Value, Missing.Value, Missing.Value
            , Missing.Value, Missing.Value, Missing.Value, Missing.Value
            , Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //xBook=xApp.Workbooks.Add(Missing.Value);//新建文件的代码
            //指定要操作的Sheet，两种方式：








            Microsoft.Office.Interop.Excel.Worksheet xSheetSample = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];


            //利用二维数组批量写入       ;
            string[,] ss = new string[dt.Rows.Count, dt.Columns.Count];

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[j][k].ToString()))
                    {
                        ss[j, k] = "";
                    }
                    else
                    {
                        if (dt.Rows[j][k].ToString().Length > 3000)
                        {
                            ss[j, k] = dt.Rows[j][k].ToString().Substring(0, 3000);
                        }
                        else
                        {
                            ss[j, k] = dt.Rows[j][k].ToString();
                        }

                    }

                }
            }

            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)xSheetSample.Cells[2, 1];
            range = range.get_Resize(dt.Rows.Count, dt.Columns.Count);
            range.Value2 = ss;
            range.Value2 = range.Value2;







            //保存方式一：保存WorkBook
            xBook.SaveAs(FilePath,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);

            ////保存方式二：保存WorkSheet
            //xSheet.SaveAs(@"C:\CData2.xls",
            //Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //保存方式三
            xBook.Save();
            xSheetSample = null;
            xBook = null;
            xApp.Quit(); //这一句是非常重要的，否则Excel对象不能从内存中退出
            xApp = null;
            GC.Collect();
            KillProcess("EXCEL");
            System.Threading.Thread.Sleep(1000 * 3);
            return true;
        }
        public DataTable Excel2DataTable(string FileName)
        {

            //创建Application对象
            Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

            xApp.Visible = false;
            //Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(@"C:\医药制造业\Sample.xls",
            //得到WorkBook对象, 可以用两种方式之一: 下面的是打开已有的文件
            Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(FileName,

            Missing.Value, Missing.Value, Missing.Value, Missing.Value
            , Missing.Value, Missing.Value, Missing.Value, Missing.Value
            , Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //xBook=xApp.Workbooks.Add(Missing.Value);//新建文件的代码
            //指定要操作的Sheet，两种方式：

            Microsoft.Office.Interop.Excel.Worksheet xSheetSample = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];

            Microsoft.Office.Interop.Excel.Range range = xSheetSample.UsedRange;
            DataTable dt = new DataTable();
            for (int j = 1; j <= range.Columns.Count; j++)
            {
                DataColumn dc = new DataColumn(((Microsoft.Office.Interop.Excel.Range)range.Cells[1, j]).Value2.ToString());
                dt.Columns.Add(dc);

            }
            for (int i = 2; i <= range.Rows.Count; i++)
            {
                DataRow tmprow = dt.NewRow();
                for (int j = 1; j <= range.Columns.Count; j++)
                {
                    tmprow[j - 1] = ((Microsoft.Office.Interop.Excel.Range)range.Cells[1, j]).Value2;
                }
                dt.Rows.Add(tmprow);
            }


            xSheetSample = null;
            xBook = null;
            xApp.Quit(); //这一句是非常重要的，否则Excel对象不能从内存中退出
            xApp = null;
            GC.Collect();
            KillProcess("EXCEL");
            System.Threading.Thread.Sleep(1000 * 3); ;
            return dt;

        }
        public string DataTable2TEXT(DataTable dt, string FileName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Columns.Count-1; i++)
            {
                sb.Append(dt.Columns[i].ColumnName + "\t");
            }
            sb.Append(dt.Columns[dt.Columns.Count - 1].ColumnName + Environment.NewLine);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count - 1; i++)
                {
                    sb.Append(dt.Rows[j][i].ToString() + "\t");
                }
                sb.Append(dt.Rows[j][dt.Columns.Count - 1].ToString() + Environment.NewLine);
            }
            using (StreamWriter sw = new StreamWriter(FileName,false,Encoding.GetEncoding("gb2312")))
            {
                sw.WriteLine(sb);
            }
            GC.Collect();
            return FileName;
        }
        public string DataTable2CSV(DataTable dt, string FileName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                sb.Append("\"" + dt.Columns[i].ColumnName + "\",");
            }
            sb.Append("\"" + dt.Columns[dt.Columns.Count - 1].ColumnName + "\"" + Environment.NewLine);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count - 1; i++)
                {
                    sb.Append("\"" + dt.Rows[j][i].ToString() + "\",");
                }
                sb.Append("\"" + dt.Rows[j][dt.Columns.Count - 1].ToString() + "\"" + Environment.NewLine);
            }
            using (StreamWriter sw = new StreamWriter(FileName, false, Encoding.GetEncoding("gb2312")))
            {
                sw.WriteLine(sb);
            }
            GC.Collect();
            return FileName;
        }
        public string DataTable2File(DataTable dt, string FileName, string FileType)
        {
            switch (FileType.ToUpper())
            {
                case "TXT":
                    DataTable2TEXT(dt, FileName);
                    break;
                case "XLS":
                    DataTable2ExcelFile(dt, FileName );
                    break;
                case "CSV":
                    DataTable2CSV(dt, FileName );
                    break;
            }
            return FileName;
        }
        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    double pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor(1000 * pack / _speed) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        //Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength-1, fileLength));
                    }
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();

                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

