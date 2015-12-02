using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cpic.Cprs2010.Search;
using System.Data.SqlClient;
using System.Data;
using DBA;
using System.Configuration;


namespace SearchInterface
{
    public class ImporResult
    {

        private static int CnpCountCN = Convert.ToInt16 (ConfigurationManager.AppSettings["CnpCountCN"].ToString ());
        private static string cnpPath = "";
        private static string cnpFilePathDocDB = "";
        private static string[] AryCC;
        private static string[] AryCnpCount;

        // ConfigurationManager.AppSettings["DY_CnpFileBasePath"] 

        /// <summary>
        /// 将号单转成cnp文件
        /// </summary>
        /// <param name="strTxt">txt文件路径</param>
        /// <param name="UName">用户ID</param>
        /// <param name="strSno">检索编号</param>
        /// <param name="SearchDbType">数据源:Cn/DocDB</param>
        /// <returns></returns>
        public ResultInfo ImportLis2tResult ( string strTxt, int UName,string strSno, SearchDbType SearchDbType )
        {
            

            if ( Enum.GetName (typeof (SearchDbType), SearchDbType).ToUpper () == "CN" )
            {
                return ImportLis2tResult_CN (strTxt, UName, strSno,SearchDbType);
            }
            else
            {
                return ImportLis2tResult_DocDB (strTxt, UName, strSno, SearchDbType);
            }


        }

        /// <summary>
        /// 导入世界专利号单,生成cnp文件
        /// </summary>
        /// <param name="strTxt">号单文件路径</param>
        /// <param name="UName">用户ID</param>
        /// <param name="strSearchNo">检索编号</param>
        /// <param name="SearchDbType">数据源: Cn/DocDB</param>
        /// <returns></returns>
        private ResultInfo ImportLis2tResult_DocDB ( string strTxt, int UName,string strSearchNo, SearchDbType SearchDbType )
        {

            Cpic.Cprs2010.Search.ResultInfo res = new ResultInfo ();

            if ( !File.Exists (strTxt) )
            {
                return res;
            }

            #region 生成cnp目录

            string ConfigCC = ConfigurationManager.AppSettings["CnpCC"].ToString ();
            string ConfigCCCount = ConfigurationManager.AppSettings["CnpCCCount"].ToString ();
            AryCC = ConfigCC.Split ('|');
            AryCnpCount =  ConfigCCCount.Split ('|');
            //Dictionary<string, string> dict = new Dictionary<string, string> ();

            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch ();
            //ResultInfo res = search.DoSearch ("", Convert.ToInt32 (UName), SearchDbType);
            string SearchNo = strSearchNo;// res.SearchPattern.SearchNo.ToString ();
            string strPath = CprsConfig.GetUserPath (Convert.ToInt32 (UName),XmPatentComm.strWebSearchGroupName);

            if ( !strPath.EndsWith (@"\") )
            {
                strPath += @"\";
            }

            cnpPath = strPath + Enum.GetName (typeof (SearchDbType), SearchDbType).ToUpper () + @"\" + "Set" + SearchNo + @"\";

            
            foreach ( string s in AryCC )
            {
                cnpFilePathDocDB = cnpPath + s + @"\";

                if ( !System.IO.Directory.Exists (cnpFilePathDocDB) )
                {
                    System.IO.Directory.CreateDirectory (cnpFilePathDocDB);
                }
            }



            #endregion

            #region 生成cnp空文件


            for ( int i = 0; i < AryCC.Length; i++ )
            {
                int CnpCountCC = Convert.ToInt16 (AryCnpCount[i]);
                for ( int j = 1; j <= CnpCountCC; j++ )
                {
                    string strCnpName = "00000" + Convert.ToString (j);
                    strCnpName = strCnpName.Substring (strCnpName.Length - 5, 5) + ".CNP";

                    strCnpName = cnpPath + AryCC[i] + @"\" + strCnpName;

                    //if ( !File.Exists (strCnpName) )
                    //{
                    //    File.Create (strCnpName).Close (); ;
                    //}

                    if ( File.Exists (strCnpName) )
                    {
                        File.Delete (strCnpName);
                    }

                    File.Create (strCnpName).Close ();
                }
            }

            #endregion

            #region 从文本中读号单在数据库中查询
            List<string> txtlist = new List<string> ();//文本中号单
            //{ "CH", "CN", "DE", "EP", "FR", "GB", "JP", "KR", "OT", "RU", "US","WO" };
            List<int> hitlistCH = new List<int> ();//命中号单
            List<int> hitlistCN = new List<int> ();//命中号单
            List<int> hitlistDE = new List<int> ();//命中号单
            List<int> hitlistEP = new List<int> ();//命中号单
            List<int> hitlistFR = new List<int> ();//命中号单
            List<int> hitlistGB = new List<int> ();//命中号单
            List<int> hitlistJP = new List<int> ();//命中号单
            List<int> hitlistKR = new List<int> ();//命中号单
            List<int> hitlistOT = new List<int> ();//命中号单
            List<int> hitlistRU = new List<int> ();//命中号单
            List<int> hitlistUS = new List<int> ();//命中号单
            List<int> hitlistWO = new List<int> ();//命中号单
            txtlist = ReadText2List (strTxt);
            txtlist.Sort ();
            int hitCount = 0;
            foreach ( var x in txtlist )
            {
                string PubNo = FormatPubNo (x);
                string txtCC = x.Substring (0, 2).ToString ();
                string strSQLCC = "";
                if ( Array.IndexOf (AryCC, txtCC) >= 0 )//在国家数组里存在
                {
                    strSQLCC = "[DataInfo].[dbo].[DocdbDocInfo_" + txtCC + "]";
                }
                else
                {
                    strSQLCC = "[DataInfo].[dbo].[DocdbDocInfo_OT]";
                }

                DataTable dt = new DataTable ();
                string SQL = "select ID  sid from " + strSQLCC + "  where docid =(select id from [DataInfo].[dbo].[DocdbDocInfo] where PubID=@PubID )";

                SqlParameter[] parms =
                    new SqlParameter[1] { new System.Data.SqlClient.SqlParameter ("@PubID", SqlDbType.NVarChar, 16) };
                parms[0].Value = PubNo.Trim ();
                dt = SqlDbAccess.GetDataTable (CommandType.Text, SQL, parms);
                if ( dt == null || dt.Rows.Count == 0 )
                {
                    //记录日志
                    string cnpPath1 = strPath + Enum.GetName (typeof (SearchDbType), SearchDbType).ToUpper () + @"\";
                    WriteLog (cnpPath1 + SearchNo + "_no.txt", x);
                }
                else
                {
                    int it = Convert.ToInt32 (dt.Rows[0][0].ToString ());

                    //{ "CH", "CN", "DE", "EP", "FR", "GB", "JP", "KR", "OT", "RU", "US","WO" };
                    //保存数组
                    switch ( txtCC )
                    {
                        case "CH":
                            hitlistCH.Add (it);
                            break;
                        case "CN":
                            hitlistCN.Add (it);
                            break;
                        case "DE":
                            hitlistDE.Add (it);
                            break;
                        case "EP":
                            hitlistEP.Add (it);
                            break;
                        case "FR":
                            hitlistFR.Add (it);
                            break;
                        case "GB":
                            hitlistGB.Add (it);
                            break;
                        case "JP":
                            hitlistJP.Add (it);
                            break;
                        case "KR":
                            hitlistKR.Add (it);
                            break;
                        case "RU":
                            hitlistRU.Add (it);
                            break;
                        case "US":
                            hitlistUS.Add (it);
                            break;
                        case "WO":
                            hitlistWO.Add (it);
                            break;
                        default:
                            hitlistOT.Add (it);
                            break;
                    }
                    hitCount++;

                }

            }

            //hitlistCH.Add (500003);
            //hitlistCH.Add (300003);
            //hitlistCH.Add (700003);

            //hitlistEP.Add (4000334);
            //hitlistEP.Add (4000834);
            //hitlistEP.Add (4000134);
            //hitlistEP.Add (40134);

            //hitlistWO.Add (1000003);
            //hitlistWO.Add (200003);
            //hitlistWO.Add (100003);
            //hitlistWO.Add (1000018);
            //hitlistWO.Add (1000020);

            hitlistCH.Sort ();
            hitlistCN.Sort ();
            hitlistDE.Sort ();
            hitlistEP.Sort ();
            hitlistFR.Sort ();
            hitlistGB.Sort ();
            hitlistJP.Sort ();
            hitlistKR.Sort ();
            hitlistOT.Sort ();
            hitlistUS.Sort ();
            hitlistWO.Sort ();
            hitlistRU.Sort ();

            #endregion


            #region 保存为cnp文件

            //{ "CH", "CN", "DE", "EP", "FR", "GB", "JP", "KR", "OT", "RU", "US","WO" };
            Save2Cnp (hitlistCH,"CH");
            Save2Cnp (hitlistCN, "CN");
            Save2Cnp (hitlistDE, "DE");
            Save2Cnp (hitlistEP, "EP");
            Save2Cnp (hitlistFR, "FR");
            Save2Cnp (hitlistGB, "GB");
            Save2Cnp (hitlistJP, "JP");
            Save2Cnp (hitlistKR, "KR");
            Save2Cnp (hitlistOT, "OT");
            Save2Cnp (hitlistRU, "RU");
            Save2Cnp (hitlistUS, "US");
            Save2Cnp (hitlistWO, "WO");
           
            #endregion


            int i1 = strTxt.LastIndexOf (@"\");
            int i2 = strTxt.LastIndexOf (@"/");
            string strTxtName = "";
            if ( i1 >= 0 )
            {
                strTxtName = strTxt.Substring (i1+1);
            }
            if ( i2 >= 0 )
            {
                strTxtName = strTxt.Substring (i2 + 1);
            }
            Cpic.Cprs2010.Search.SearchPattern schPatItem = new Cpic.Cprs2010.Search.SearchPattern ();

            schPatItem.SearchNo = strSearchNo.PadLeft (3, '0');  //检索编号[001-999]
            schPatItem.Pattern = strTxtName;      //检索式：txt文件名
            schPatItem.UserId = UName;   //用户ID
            schPatItem.DbType = SearchDbType;

            res.SearchPattern = schPatItem;
            res.HitMsg = string.Format("({0})F XX {1} <hits:{2}>", schPatItem.SearchNo, strTxtName, hitCount);
            res.HitCount = hitCount;


            return res;
        }

        private void Save2Cnp ( List<int> hitlist,string cc )
        {
            int cnp1 = 0;

            List<int> cnplist = new List<int> ();
            int j = 0;
            if ( hitlist.Count > 0 )
            {
                cnp1 = hitlist[0] / 1000000 + 1;

                foreach ( var y in hitlist )
                {
                    int cnp = y / 1000000 + 1;

                    if ( j == 0 )
                    {
                        cnplist.Add (Convert.ToInt32 (y));
                    }
                    else
                    {
                        if ( cnp != cnp1 )//下一个cnp文件
                        {
                            InsertCNP_DocDB (cnp1, cnplist,cc);
                            cnplist.Clear ();
                            cnp1 = cnp;
                            cnplist.Add (y);
                            continue;

                        }

                        cnplist.Add (y);
                        cnp1 = cnp;
                    }

                    j++;
                }

                InsertCNP_DocDB (cnp1, cnplist,cc);
            }

        }

        private void InsertCNP_DocDB ( int cnp, List<int> list,string CC )
        {
            string strCnpName = "00000" + Convert.ToString (cnp);
            strCnpName = strCnpName.Substring (strCnpName.Length - 5, 5) + ".CNP";
            list.Sort ();
            string cnpPath1 = cnpPath + CC + @"\";
            strCnpName = cnpPath1 + strCnpName;

            if ( !File.Exists (strCnpName) )
            {
                File.Create (strCnpName);
            }

            using ( FileStream fsw = new FileStream (strCnpName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite) )
            {
                foreach ( var x in list )
                {
                    byte[] by = BitConverter.GetBytes (x);
                    fsw.Write (by, 0, by.Length);
                }
            }
        }

        private string FormatPubNo ( string x )
        {
            return x;
        }


        /// <summary>
        /// 导入中文号单,生成cnp文件
        /// </summary>
        /// <param name="strTxt">文件路径</param>
        /// <param name="UName">用户名</param>
        /// <param name="strSearchNo">检索编号</param>
        /// <param name="SearchDbType">数据源 CN/DocDB</param>
        /// <returns></returns>
        private ResultInfo ImportLis2tResult_CN ( string strTxt, int UName,string strSearchNo, SearchDbType SearchDbType )
        {
            Cpic.Cprs2010.Search.ResultInfo res = new ResultInfo ();

            if ( !File.Exists (strTxt) )
            {
                return res;
            }

            #region 生成cnp的目录
            
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch ();

            string SearchNo = strSearchNo;//136
            //\\192.168.131.10\VPath\User_Search_Base\1\000\066 
            //\\192.168.131.10\VPath\User_Search_Base\1\000\066\CN\Set136\

            string strPath = CprsConfig.GetUserPath (Convert.ToInt32 (UName),XmPatentComm.strWebSearchGroupName);

            if ( !strPath.EndsWith (@"\") )
            {
                strPath += @"\";
            }

            cnpPath = strPath + Enum.GetName (typeof (SearchDbType), SearchDbType).ToUpper () + @"\" + "Set" + SearchNo + @"\";


            if ( !System.IO.Directory.Exists (cnpPath) )
            {
                System.IO.Directory.CreateDirectory (cnpPath);
            }


            #endregion

            #region 从文本中读号单在数据库中查询


            string SQL = "";
            List<string> txtlist = new List<string> ();//文本中号单
            List<int> hitlist = new List<int> ();//命中号单
            txtlist = ReadText2List (strTxt);
            txtlist.Sort ();
            int hitCount=0;
            foreach ( var x in txtlist )
            {
                string apno = FormatApno (x);//去掉校验位和CN
                DataTable dt = new DataTable ();
                SQL = "select SerialNo from [DataInfo].[dbo].[CnGeneral_Info] where ApNo=@ApNo";

                SqlParameter[] parms =
                    new SqlParameter[1] { new System.Data.SqlClient.SqlParameter ("@ApNo", SqlDbType.NVarChar, 16) };
                parms[0].Value = apno.Trim ();
                dt = SqlDbAccess.GetDataTable (CommandType.Text, SQL, parms);
                if ( dt == null || dt.Rows.Count == 0 )
                {
                    //记录日志
                    string cnpPath1 = strPath + Enum.GetName (typeof (SearchDbType), SearchDbType).ToUpper () + @"\";
                    WriteLog (cnpPath1 + SearchNo + "_no.txt", x);
                }
                else
                {
                    //保存数组
                    hitlist.Add (Convert.ToInt32 (dt.Rows[0]["SerialNo"].ToString ()));
                    hitCount++;

                }
            }

            #endregion

            #region 生成空cnp文件

            if ( hitlist.Count > 0 )
            {
                for ( int i = 1; i <= CnpCountCN; i++ )
                {
                    string strCnpName = "00000" + Convert.ToString (i);
                    strCnpName = strCnpName.Substring (strCnpName.Length - 5, 5) + ".CNP";

                    strCnpName = cnpPath + strCnpName;
                    if ( File.Exists (strCnpName) )
                    {
                        File.Delete (strCnpName);
                    }

                    File.Create (strCnpName).Close ();
                   


                }
                hitlist.Sort ();
            }

            #endregion

            #region 保存为cnp文件

            int cnp1 = 0;

            List<int> cnplist = new List<int> ();
            int j = 0;
            cnp1 = hitlist[0] / 500000 + 1;

            foreach ( var y in hitlist )
            {
                int cnp = y / 500000 + 1;

                if ( j == 0 )
                {
                    cnplist.Add (Convert.ToInt32 (y));
                }
                else
                {
                    if ( cnp != cnp1 )//下一个cnp文件
                    {
                        InsertCNP (cnp1, cnplist);
                        cnplist.Clear ();
                        cnp1 = cnp;
                        cnplist.Add (y);
                        continue;

                    }

                    cnplist.Add (y);
                    cnp1 = cnp;
                }

                j++;
            }

            InsertCNP (cnp1, cnplist);

            #endregion

            #region 返回的参数
            
            
            int i1 = strTxt.LastIndexOf (@"\");
            int i2 = strTxt.LastIndexOf (@"/");
            string strTxtName = "";
            if ( i1 >= 0 )
            {
                strTxtName = strTxt.Substring (i1 + 1);
            }
            if ( i2 >= 0 )
            {
                strTxtName = strTxt.Substring (i2 + 1);
            }

            Cpic.Cprs2010.Search.SearchPattern schPatItem = new Cpic.Cprs2010.Search.SearchPattern ();

            schPatItem.SearchNo = strSearchNo.PadLeft (3, '0');  //检索编号[001-999]
            schPatItem.Pattern = strTxtName;      //检索式：txt文件名
            schPatItem.UserId = UName;   //用户ID
            schPatItem.DbType = SearchDbType;

            res.SearchPattern = schPatItem;
            res.HitMsg = string.Format("({0})F XX {1} <hits:{2}>",schPatItem.SearchNo,strTxtName,hitCount);
            res.HitCount = hitCount;

            #endregion
            return res;
        }

        private void InsertCNP ( int cnp, List<int> list )
        {
            string strCnpName = "00000" + Convert.ToString (cnp);
            strCnpName = strCnpName.Substring (strCnpName.Length - 5, 5) + ".CNP";
            list.Sort ();

            strCnpName = cnpPath + strCnpName;

            if ( !File.Exists (strCnpName) )
            {
                File.Create (strCnpName);
            }

            using ( FileStream fsw = new FileStream (strCnpName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite) )
            {
                foreach ( var x in list )
                {
                    byte[] by = BitConverter.GetBytes (x);
                    fsw.Write (by, 0, by.Length);
                }
            }
        }


        private void WriteLog ( string _filename, string _content )
        {
            System.IO.StreamWriter sw = new StreamWriter (_filename, true);
            sw.WriteLine (_content);
            sw.Close ();
        }

        private string FormatApno ( string _ApNo )
        {
            string AppNo = "";
            AppNo = _ApNo.ToUpper ();
            AppNo = AppNo.Replace ("CN", "");
            if ( AppNo.IndexOf (".") >= 0 )//01234433.8
            {
                AppNo = AppNo.Substring (0, AppNo.Length - 2);
            }
            else//012344338
            {
                if ( AppNo.Length == 13 || AppNo.Length == 9 )
                    AppNo = AppNo.Substring (0, AppNo.Length - 1);
            }

            return AppNo;
        }

        private static List<string> ReadText2List ( string sourcefile )
        {
            using ( FileStream fsw = new FileStream (sourcefile, FileMode.Open, FileAccess.Read) )
            {
                List<string> list = new List<string> ();
                StreamReader sr = new StreamReader (fsw);
                sr.BaseStream.Seek (0, SeekOrigin.Begin);
                string tmp = sr.ReadLine ();
                while ( tmp != null )
                {
                    //list.Add (Convert.ToInt32 (tmp));

                    list.Add (tmp);
                    Console.WriteLine (tmp);
                    tmp = sr.ReadLine ();

                }
                sr.Close ();
                fsw.Close ();
                return list;
            }
        }

       
    }
}
