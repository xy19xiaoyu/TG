using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using System.Web;
using System.Threading;
namespace ExcelLib
{
    public class NPOIHelper
    {

        public static DataTable Excel2DataTable(string filename, bool hasHeadRow, int SheetIndex = 1)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                IWorkbook wb = WorkbookFactory.Create(fs);

                ISheet sheet = wb.GetSheetAt(0);

                DataTable dt = new DataTable(sheet.SheetName);
                int firstrow = sheet.FirstRowNum;
                IRow head = sheet.GetRow(firstrow);

                if (hasHeadRow)
                {
                    for (int i = 0; i < head.LastCellNum; i++)
                    {
                        dt.Columns.Add(new DataColumn(head.Cells[i].ToString()));
                    }
                    firstrow++;
                }
                else
                {
                    for (int tmpi = 0; tmpi < head.LastCellNum; tmpi++)
                    {
                        dt.Columns.Add(new DataColumn("列-" + (tmpi + 1).ToString()));
                    }
                }

                for (int j = firstrow; j <= sheet.LastRowNum; j++)
                {
                    IRow row = sheet.GetRow(j);
                    if (row == null) continue;
                    DataRow addrow = dt.NewRow();
                    for (int tmpj = head.FirstCellNum; tmpj < head.LastCellNum; tmpj++)
                    {
                        addrow[tmpj - head.FirstCellNum] = row.GetCell(tmpj);
                    }
                    dt.Rows.Add(addrow);
                }
                return dt;
            }

        }

        public static DataSet Excel2DatatSet(string filename, bool hasHeadRow)
        {
            DataSet ds = new DataSet();

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                IWorkbook wb = WorkbookFactory.Create(fs);
                for (int i = 0; i < wb.NumberOfSheets; i++)
                {
                    ISheet sheet = wb.GetSheetAt(i);
                    DataTable dt = new DataTable(sheet.SheetName);
                    int firstrow = sheet.FirstRowNum;
                    IRow head = sheet.GetRow(firstrow);
                    if (hasHeadRow)
                    {

                        foreach (ICell cell in head.Cells)
                        {
                            dt.Columns.Add(new DataColumn(cell.ToString()));
                        }
                        firstrow++;
                    }
                    else
                    {
                        for (int tmpi = 0; tmpi < head.LastCellNum; tmpi++)
                        {
                            dt.Columns.Add(new DataColumn("列-" + (tmpi + 1).ToString()));
                        }
                    }

                    for (int j = firstrow; j <= sheet.LastRowNum; j++)
                    {
                        IRow row = sheet.GetRow(j);
                        if (row == null) continue;
                        DataRow addrow = dt.NewRow();
                        for (int tmpj = head.FirstCellNum; tmpj < head.LastCellNum; tmpj++)
                        {
                            addrow[tmpj - head.FirstCellNum] = row.GetCell(tmpj);
                        }
                        dt.Rows.Add(addrow);
                    }
                    ds.Tables.Add(dt);
                }
                return ds;
            }
        }
        public string DataTable2File(DataTable dt, string FileName, string FileType)
        {
            switch (FileType.ToUpper())
            {
                case "TXT":
                    DataTable2TEXT(dt, FileName);
                    break;
                case "XLS":
                    Export(dt, FileName, "专利数据", "");
                    break;
                case "CSV":
                    DataTable2CSV(dt, FileName);
                    break;
            }
            return FileName;
        }
        public string DataTable2TEXT(DataTable dt, string FileName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Columns.Count - 1; i++)
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
            using (StreamWriter sw = new StreamWriter(FileName, false, Encoding.GetEncoding("gb2312")))
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
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource, string strFileName, string strHeaderText = "", string strImageFileName = "")
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText, strImageFileName))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream Export(DataTable dtSource, string strHeaderText = "统计数据", string strImageFileName = "")
        {
            IWorkbook workbook = new HSSFWorkbook();
            #region 填充图片
            if (strImageFileName != "")
            {
                ISheet sheet1 = workbook.CreateSheet("图标");
                byte[] bytes = System.IO.File.ReadAllBytes(strImageFileName);
                int pictureIdx = workbook.AddPicture(bytes, PictureType.JPEG);

                // Create the drawing patriarch.  This is the top level container for all shapes.
                HSSFPatriarch patriarch = (HSSFPatriarch)sheet1.CreateDrawingPatriarch();

                //add a picture
                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 20, 20);
                HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                pict.Resize();
                //rowIndex += 20;
            }
            #endregion
            if (strHeaderText == "")
            {
                strHeaderText = "统计数据";
            }
            ISheet sheet = workbook.CreateSheet(strHeaderText);


            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                arrColWidth[item.Ordinal] = arrColWidth[item.Ordinal] > 50 ? 50 : arrColWidth[item.Ordinal];
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j] && intTemp < 50)
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }
                    if (strHeaderText != "")
                    {
                        #region 表头及样式
                        {
                            IRow headerRow = sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strHeaderText);

                            ICellStyle headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Center;
                            IFont font = workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                            //headerRow.Dispose();
                        }
                        #endregion
                        rowIndex = 1;
                    }
                    else
                    {
                        rowIndex = 0;
                    }


                    #region 列头及样式
                    {
                        IRow headerRow = sheet.CreateRow(rowIndex);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        //headerRow.Dispose();
                    }
                    #endregion

                    rowIndex++;



                }
                #endregion



                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }


            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        ///// <summary>
        ///// 用于Web导出
        ///// </summary>
        ///// <param name="dtSource">源DataTable</param>
        ///// <param name="strHeaderText">表头文本</param>
        ///// <param name="strFileName">文件名</param>
        //public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        //{
        //    HttpContext curContext = HttpContext.Current;

        //    // 设置编码和附件格式
        //    curContext.Response.ContentType = "application/vnd.ms-excel";
        //    curContext.Response.ContentEncoding = Encoding.UTF8;
        //    curContext.Response.Charset = "";
        //    curContext.Response.AppendHeader("Content-Disposition",
        //        "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

        //    curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
        //    curContext.Response.End();
        //}

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
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