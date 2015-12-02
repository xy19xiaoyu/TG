using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Linq;
using System.Collections;
using Cpic.Cprs2010.Search;

namespace Cpic.Cprs2010.Search
{
    /// <summary>
    /// 检索结果返回
    /// </summary>
    public class ResultServices
    {
        //结果文件绝对目录
        private readonly string resultfile = "{0}\\{1}\\{2}.cnp";
        private readonly string resultfiledir = "{0}\\{1}\\Set{2}";

        /// <summary>
        /// 自定义结果文根目录
        /// </summary>
        private static string strDY_CnpFileBasePath = "";

        /// <summary>
        /// 静态构造
        /// </summary>
        static ResultServices()
        {
            try
            {
                strDY_CnpFileBasePath = ConfigurationManager.AppSettings["DY_CnpFileBasePath"] == null ? "" : ConfigurationManager.AppSettings["DY_CnpFileBasePath"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 得到某一结果文件的全部数据
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="SortExpression">排序</param>
        /// <returns>list int</returns>
        public List<int> GetResultList(SearchPattern sp, string SortExpression)
        {
            List<int> lstfml = new List<int>();
            int readlength;
            string filepath = getResultFilePath(sp);
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException();
            }
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                byte[] allnos = new byte[fs.Length];
                readlength = fs.Read(allnos, 0, (int)fs.Length);
                for (int i = 0; i < readlength; i += 4)
                {
                    lstfml.Add(BitConverter.ToInt32(allnos, i));
                }
            }
            //todo:StorBy(SortExpression);
            return lstfml;
        }

        /// <summary>
        /// 得到某一结果文件的全部数据
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="SortExpression">排序</param>
        /// <returns>1,2,3,4,5,6</returns>
        public string GetResultString(SearchPattern sp, string SortExpression)
        {
            List<int> lstfml = GetResultList(sp, SortExpression);
            //todo:StorBy(SortExpression);
            //排序
            StringBuilder sbResult = new StringBuilder();
            foreach (int fml in lstfml)
            {
                sbResult.Append(",");
                sbResult.Append(fml);
                //,1,2,3,4,5,6
            }
            ///如果长度很短 只有一个字符“，” 或者没有的时候返回string.empty
            if (sbResult.Length <= 1)
            {
                return string.Empty;
            }

            return sbResult.ToString(1, sbResult.Length - 1);
            //1,2,3,4,5,6
        }


        /// <summary>
        /// 得到某一结果文件的某一页数据 并按某一字段排序
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>list(int)</int></returns>
        public List<int> GetResultList(SearchPattern sp, int PageSize, int PageIndex, string SortExpression)
        {
            List<int> lstfml = new List<int>();
            byte[] fmlNo = new byte[4];
            int readlength;
            long Start;
            string filepath;
            if (PageIndex <= 0 || PageSize <= 0)
            {
                throw new ArgumentException("参数错误:分页大少或页码不能小于等于0");
            }

            Start = PageSize * (PageIndex - 1) * 4;   //PageSize * (PageIndex - 1);
            filepath = getResultFilePath(sp);
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException(filepath);
            }
            //
            //todo:StorBy(SortExpression);
            //先排序 后分页
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                //设置文件指针到预定位置
                fs.Seek(Start, SeekOrigin.Begin);

                #region colsed 一条一条的读
                ////读多少条
                //for (int i = 0; i < PageSize; i++)
                //{
                //    readlength = fs.Read(fmlNo, 0, 4);
                //    if (readlength <= 0)
                //    {
                //        break;
                //    }
                //    lstfml.Add(BitConverter.ToInt32(fmlNo, 0));
                //}
                #endregion

                byte[] allnos = new byte[PageSize * 4];
                readlength = fs.Read(allnos, 0, allnos.Length);
                for (int i = 0; i < readlength; i += 4)
                {
                    lstfml.Add(BitConverter.ToInt32(allnos, i));
                }

            }
            return lstfml;
        }

        public string getResultFilePath(SearchPattern sp, bool _bExistsAndCreat)
        {
            string ResultFilePath;
            string ResultFileDir;
            //如果结果文件已经存在 直接返回路径
            if (!string.IsNullOrEmpty(sp.StrCnpFile) && sp.StrCnpFile.EndsWith(".CNP"))
            {
                return strDY_CnpFileBasePath + "\\" + sp.StrCnpFile;
            }
            else
            {
                //如果没有 去Set×××目录下合成 新的***.cnp文件
                ResultFilePath = string.Format(resultfile, CprsConfig.GetUserPath(sp.UserId, sp.GroupName), Enum.GetName(typeof(SearchDbType), sp.DbType), sp.SearchNo);
                if (_bExistsAndCreat && !File.Exists(ResultFilePath))
                {
                    ResultFileDir = string.Format(resultfiledir, CprsConfig.GetUserPath(sp.UserId, sp.GroupName), Enum.GetName(typeof(SearchDbType), sp.DbType), sp.SearchNo);
                    string[] files = Directory.GetFiles(ResultFileDir, "*.CNP",SearchOption.AllDirectories);
                    byte[] by;
                    int readLength;
                    using (FileStream fsw = new FileStream(ResultFilePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        foreach (string f in files)
                        {
                            if (File.Exists(f))
                            {
                                using (FileStream fsr = new FileStream(f, FileMode.Open, FileAccess.Read))
                                {
                                    if (fsr.Length > 0)
                                    {
                                        by = new byte[fsr.Length];
                                        readLength = fsr.Read(by, 0, (int)fsr.Length);
                                        fsw.Write(by, 0, readLength);
                                    }
                                }
                            }
                        }
                    }

                }
                return ResultFilePath;
            }
        }
        public string getResultFilePathOnly(SearchPattern sp)
        {
            string ResultFilePath;
            ResultFilePath = string.Format(resultfile, CprsConfig.GetUserPath(sp.UserId, sp.GroupName), Enum.GetName(typeof(SearchDbType), sp.DbType), sp.SearchNo);
            return ResultFilePath;
        }

        /// <summary>
        /// 获取结果文件路径
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string getResultFilePath(SearchPattern sp)
        {
            string ResultFilePath = getResultFilePath(sp, true);
            return ResultFilePath;
        }

        /// <summary>
        /// 得到某一结果文件的某一页数据 并按某一字段排序，文件从后往前读
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>list(int)</int></returns>
        public List<int> GetResultListByEnd(SearchPattern sp, int PageSize, int PageIndex, string SortExpression)
        {
            List<int> lstfml = new List<int>();

            //排序
            if (!string.IsNullOrEmpty(SortExpression))
            {
                switch (SortExpression.ToUpper().Replace(" ", ""))
                {
                    case "PD|AESC":
                        return GetResultList(sp, PageSize, PageIndex, SortExpression);
                        break;
                    case "AD|DESC":
                        break;
                    case "AD|AESC":
                        break;
                }
            }

            byte[] fmlNo = new byte[4];
            int readlength;
            long Start;
            string filepath;
            if (PageIndex <= 0 || PageSize <= 0)
            {
                throw new ArgumentException("参数错误:分页大少或页码不能小于等于0");
            }

            Start = PageSize * (PageIndex - 1) * 4;
            filepath = getResultFilePath(sp);
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException(filepath);
            }
            //
            //todo:StorBy(SortExpression);
            //先排序 后分页
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                Start = fs.Length - 4 * PageSize * PageIndex;
                //开始位置小于0时，置为0
                if (Start < 0)
                {
                    PageSize = PageSize + Convert.ToInt16(Start / 4);
                    Start = 0;
                }
                //设置文件指针到预定位置
                fs.Seek(Start, SeekOrigin.Begin);
                //读多少条
                //for (int i = PageSize; i > 0; i--)
                //{

                //    readlength = fs.Read(fmlNo, 0, 4);
                //    if (readlength <= 0)
                //    {
                //        break;
                //    }
                //    lstfml.Add(BitConverter.ToInt32(fmlNo, 0));
                //}

                byte[] allnos = new byte[PageSize * 4];
                readlength = fs.Read(allnos, 0, allnos.Length);
                for (int i = 0; i < readlength; i += 4)
                {
                    lstfml.Add(BitConverter.ToInt32(allnos, i));
                }
            }
            lstfml.Reverse(0, lstfml.Count);
            return lstfml;
        }
        /// <summary>
        /// 得到某一结果文件的某一页数据 并按某一字段排序
        /// </summary>
        /// <param name="sp">检索式</param>
        /// <param name="PageIndex">要取的页数</param>
        /// <param name="PageSize">页数大小</param>
        /// <param name="SortExpression">排序字段</param>
        /// <returns>string:1,2,3,4,5</returns>
        public string GetResultString(SearchPattern sp, int PageSize, int PageIndex, string SortExpression)
        {
            List<int> lstfml = GetResultList(sp, PageSize, PageIndex, SortExpression);

            StringBuilder sbResult = new StringBuilder();
            foreach (int fml in lstfml)
            {
                sbResult.Append(",");
                sbResult.Append(fml);
                //,1,2,3,4,5,6
            }

            ///如果长度很短 只有一个字符“，” 或者没有的时候返回string.empty
            if (sbResult.Length <= 1)
            {
                return string.Empty;
            }
            return sbResult.ToString(1, sbResult.Length - 1);
            //1,2,3,4,5,6
        }

    }
}
