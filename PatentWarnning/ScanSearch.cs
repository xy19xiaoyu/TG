using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using Cpic.Cprs2010.Search;
using System.Net.Mail;
using System.Net;
using SearchInterface;
using Cpic.Cprs2010.Search.ResultData;

namespace PatentWarnning
{
    public class Searches
    {
        private int _wid;//检索ID
        private int _cid;//订制ID
        private DateTime _changedate;
        private int _currentnum;
        private int _changenum;
        private int _type;
        private string _sname;
        private byte[] _fs;
        private string _db;
        private string _nid;
        private string c_type;

        public int W_ID
        {
            set { _wid = value; }
            get { return _wid; }
        }
        public int C_ID
        {
            set { _cid = value; }
            get { return _cid; }
        }
        public DateTime ChangeDate
        {
            set { _changedate = value; }
            get { return _changedate; }
        }
        public int CurrentNum
        {
            set { _currentnum = value; }
            get { return _currentnum; }
        }
        public int ChangeNum
        {
            set { _changenum = value; }
            get { return _changenum; }
        }
        public int TYPE
        {
            set { _type = value; }
            get { return _type; }
        }
        public string SearchName
        {
            set { _sname = value; }
            get { return _sname; }
        }
        public byte[] SearchFile
        {
            set { _fs = value; }
            get { return _fs; }
        }
        public string DBSource
        {
            set { _db = value; }
            get { return _db; }
        }
        public string NID
        {
            set { _nid = value; }
            get { return _nid; }
        }
        public string C_TYPE
        {
            set { c_type = value; }
            get { return c_type; }
        }

        private string _s_name;
        public string S_Name
        {
            set { _s_name = value; }
            get { return _s_name; }
        }
    }

    public class ScanSearch
    {
        public static string basepath = System.Configuration.ConfigurationManager.AppSettings["CPRS2010UserPath"].ToString();

        /// <summary>
        /// 提取符合更新周期的检索式,第一次执行，或手动更新时使用
        /// </summary>
        /// <param name="C_ID"></param>
        /// <param name="flag">0:初次提取 1：手动更新</param>
        /// <returns></returns>
        public List<Searches> ReadSearch(int C_ID, int flag)
        {
            List<string> sendMail = new List<string>();
            DataSet ds = new DataSet();

            List<Searches> LstSearch = new List<Searches>();
            string sql = "select b.W_ID, a.C_ID,b.S_Name, b.Pattern, b.type, b.SearchFile, a.dbsource, a.USER_ID,b.NID,a.C_TYPE  from C_EARLY_WARNING a left join C_W_SECARCH b "
                    + " on a.C_ID=b.C_ID "
                    + " where  a.C_ID=" + C_ID;

            DataTable dt = new DataTable();


            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Searches sc = new Searches();
                sc.W_ID = Convert.ToInt32(dt.Rows[i]["W_ID"]);
                sc.C_ID = Convert.ToInt32(dt.Rows[i]["C_ID"]);
                sc.ChangeDate = DateTime.Now;
                sc.ChangeNum = 0;
                sc.NID = dt.Rows[i]["NID"].ToString();
                sc.CurrentNum = 0;
                sc.SearchName = dt.Rows[i]["Pattern"].ToString();
                sc.S_Name = dt.Rows[i]["S_Name"].ToString();
                sc.TYPE = Convert.ToInt32(dt.Rows[i]["type"]);
                sc.C_TYPE = dt.Rows[i]["C_TYPE"].ToString();

                if (flag != 0)
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream((byte[])dt.Rows[i]["SearchFile"]);
                        byte[] bt = ms.ToArray();

                        sc.SearchFile = bt;
                    }
                    catch (Exception ex)
                    {
                        //不处理
                    }
                }
                sc.DBSource = dt.Rows[i]["dbsource"].ToString();
                LstSearch.Add(sc);


                //if (sendMail.Contains(dt.Rows[i]["USER_ID"].ToString()))
                //{
                //    continue;
                //}
                //sendMail.Add(dt.Rows[i]["USER_ID"].ToString());
                //sql = "select * from TbUser Where ID=" + dt.Rows[i]["USER_ID"].ToString();
                //ds = DBA.SqlDbAccess.GetDataSet(CommandType.Text, sql);

                //if (ds.Tables[0].Rows.Count <= 0)
                //{
                //    continue;
                //}

                //if (ds.Tables[0].Rows[0]["EMail"].ToString().Trim() == "")
                //{
                //    continue;
                //}
                ////发送邮件
                //bool flagSendMail = SendMail("xuxitao", "111111", ds.Tables[0].Rows[0]["EMail"].ToString().Trim(), "你的预警信息已更新,请登陆厦漳泉科技基础资源服务平台(http://192.168.131.10:8080)查询");

                //string zt = flagSendMail ? "发送成功" : "发送失败";
                //sql = "Insert Into TbSendMailLog(ShouJianRen,YouJianMingCheng,ZhuanLiQuYu,FaSongShiJian,FaSongZhuangTai) Values('" + ds.Tables[0].Rows[0]["RealName"].ToString().Trim() + "','" + ds.Tables[0].Rows[0]["EMail"].ToString().Trim() + "','','" + DateTime.Now + "','" + zt + "')";

                //DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql);

            }

            return LstSearch;
        }


        /// <summary>
        /// 提取符合更新周期的检索式
        /// </summary>
        /// <returns></returns>
        public List<Searches> ReadSearch()
        {
            List<string> sendMail = new List<string>();
            bool flag = true;
            List<Searches> LstSearch = new List<Searches>();

            // delete by zhangqiuyi 20151104
            //string sql = "select * from C_EARLY_WARNING a left join C_W_SECARCH b "
            //        + " on a.C_ID=b.C_ID "
            //        + " where CONVERT(varchar(10), DATEADD(DAY,PERIOD,GETDATE()),120)<=CONVERT(varchar(10),ChangeDate,120)";

            // modify by zhangqiuyi 20151104
            string sql = @"select b.W_ID, a.C_ID, b.ChangeDate, b.ChangeNum, b.CurrentNum, b.Pattern, b.type, b.SearchFile, a.dbsource, 
                    a.USER_ID from C_EARLY_WARNING a left join C_W_SECARCH b  on a.C_ID=b.C_ID  
                    where CONVERT(varchar(10), DATEADD(DAY,PERIOD*30,ChangeDate),120)<=CONVERT(varchar(10),GETDATE(),120)";

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt = DBA.SqlDbAccess.GetDataTable(CommandType.Text, sql, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Searches sc = new Searches();
                sc.W_ID = Convert.ToInt32(dt.Rows[i]["W_ID"]);
                sc.C_ID = Convert.ToInt32(dt.Rows[i]["C_ID"]);
                sc.ChangeDate = Convert.ToDateTime(dt.Rows[i]["ChangeDate"]);
                sc.ChangeNum = Convert.ToInt32(dt.Rows[i]["ChangeNum"]);
                sc.CurrentNum = Convert.ToInt32(dt.Rows[i]["CurrentNum"]);
                sc.SearchName = dt.Rows[i]["Pattern"].ToString();
                sc.TYPE = Convert.ToInt32(dt.Rows[i]["type"]);

                MemoryStream ms = new MemoryStream();
                if (!(dt.Rows[i]["SearchFile"] is DBNull))
                {
                    ms = new MemoryStream((byte[])dt.Rows[i]["SearchFile"]);
                }

                byte[] bt = ms.ToArray();

                sc.SearchFile = bt;
                sc.DBSource = dt.Rows[i]["dbsource"].ToString();
                LstSearch.Add(sc);

                //if (sendMail.Contains(dt.Rows[i]["USER_ID"].ToString()))
                //{
                //    continue;
                //}
                //sendMail.Add(dt.Rows[i]["USER_ID"].ToString());
                //sql = "select EMail,RealName from TbUser Where ID=" + dt.Rows[i]["USER_ID"].ToString();
                //ds = DBA.SqlDbAccess.GetDataSet(CommandType.Text, sql);

                //if (ds.Tables[0].Rows.Count <= 0)
                //{
                //    continue;
                //}

                //if (ds.Tables[0].Rows[0]["EMail"].ToString().Trim() == "")
                //{
                //    continue;
                //}

                ////发送邮件
                //flag = SendMail(System.Configuration.ConfigurationSettings.AppSettings["UserName"].ToString().Trim(), System.Configuration.ConfigurationSettings.AppSettings["PWD"].ToString().Trim(), ds.Tables[0].Rows[0]["EMail"].ToString().Trim(), "你的预警信息已更新,请登陆厦漳泉科技基础资源服务平台查询");

                //string zt = flag ? "发送成功" : "发送失败";
                //sql = @"Insert Into TbSendMailLog(ShouJianRen,YouJianMingCheng,ZhuanLiQuYu,FaSongShiJian,FaSongZhuangTai) Values('" 
                //    + ds.Tables[0].Rows[0]["RealName"].ToString().Trim() + "','" + ds.Tables[0].Rows[0]["EMail"].ToString().Trim() 
                //    + "','','" + DateTime.Now + "','" + zt + "')";

                //DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql);
            }

            return LstSearch;
        }
        /// <summary>
        /// 送检索引擎
        /// </summary>
        /// <param name="se"></param>
        /// <param name="sNo"></param>
        /// <param name="UserId"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public string SendSearch(Searches se, int sNo, int UserId, ServiceReference.SearchDbType _type)
        {
            List<int> lstZhuanTi = new List<int>();//专题库结果
            List<int> lstRs = new List<int>();//合并对比文件
            List<int> lstHis = new List<int>();//预警历史结果
            List<int> lstResult = new List<int>();//检索结果文件
            List<string> leixing = new List<string>();
            List<string> shixiao = new List<string>();
            string strMsg = "";
            //判断是否为专题库预警
            if (!string.IsNullOrEmpty(se.NID))
            {
                string type = "CN";
                if (_type.ToString().ToUpper() == "DOCDB")
                {
                    type = "EN";
                }
                lstZhuanTi = ztHelper.GetZTResultList(se.NID, type);
            }
            
            ServiceReference.SearchWebServiceSoapClient mySearch = new ServiceReference.SearchWebServiceSoapClient();
            /// 返回CNP文件号单 
            /// fm.cnp,授权:FS.cnp
            ///新型：um.cnp
            ///外观：wg.cnp
            ///有效：YX.cnp
            ///失效: SX.cnp
            if (se.C_TYPE == "16")//行业质量预警时
            {
                string[] lsttemp = se.SearchName.Split(';');
                foreach (string tmp in lsttemp)
                {
                    switch (tmp)
                    {
                        case "有效发明公开":
                            se.SearchName = "";
                            leixing.Add("FM");
                            shixiao.Add("YX");
                            break;
                        case "有效实用新型授权":
                            se.SearchName = "";
                            leixing.Add("UM");
                            shixiao.Add("YX");
                            break;
                        case "有效外观设计授权":
                            se.SearchName = "";
                            leixing.Add("WG");
                            shixiao.Add("YX");
                            break;
                        case "有效发明授权":
                            se.SearchName = "";
                            leixing.Add("FS");
                            shixiao.Add("YX");
                            break;
                        case "失效发明公开":
                            se.SearchName = "";
                            leixing.Add("FM");
                            shixiao.Add("SX");
                            break;
                        case "失效实用新型授权":
                            se.SearchName = "";
                            leixing.Add("UM");
                            shixiao.Add("SX");
                            break;
                        case "失效外观设计授权":
                            se.SearchName = "";
                            leixing.Add("WG");
                            shixiao.Add("SX");
                            break;
                        case "失效发明授权":
                            se.SearchName = "";
                            leixing.Add("FS");
                            shixiao.Add("SX");                            
                            break;
                    }
                }
                
            }
            if (!string.IsNullOrEmpty(se.SearchName))
            {                
                if (!se.SearchName.StartsWith("F "))
                {
                    se.SearchName = "F XX " + se.SearchName;
                }
                //送引擎
                ServiceReference.ResultInfoWebService res = mySearch.Search(se.SearchName, UserId, sNo, _type);
                if (res == null)
                {
                    return null;
                }

                strMsg = res.ResultInfo.HitMsg;
                strMsg = strMsg.Substring(strMsg.LastIndexOf(":") + 1);
                if (res.ResultInfo.HitCount > 0)// 返回结果正确
                {
                    //获取结果文件路径
                    string srcFilePath = res.ResultSearchFilePath;
                    //对比结果文件
                    FileStream fs = new FileStream(srcFilePath, FileMode.Open);//当前检索结果文件
                    byte[] bteRs = new byte[fs.Length];
                    fs.Read(bteRs, 0, bteRs.Length);
                    fs.Close();

                    lstResult = ConvertLstByte.GetCnpList(bteRs);

                }
                else
                {
                    AddSearchLis(se);
                    return strMsg;
                }
            }
            //判断是否专题库
            if (!string.IsNullOrEmpty(se.NID) && !string.IsNullOrEmpty(se.SearchName))
            {
                lstRs = lstZhuanTi.Intersect(lstResult).ToList<int>();
            }
            else if (!string.IsNullOrEmpty(se.NID))
            {
                if (se.C_TYPE == "11")//专题库授权量统计
                {
                    List<int> lstFS = GetCNPList("FS");
                    List<int> lstWG = GetCNPList("WG");
                    List<int> lstUM = GetCNPList("UM");
                    List<int> lstZhuanTi_FS = lstZhuanTi.Intersect(lstFS).ToList<int>();
                    List<int> lstZhuanTi_WG = lstZhuanTi.Intersect(lstWG).ToList<int>();
                    List<int> lstZhuanTi_UM = lstZhuanTi.Intersect(lstUM).ToList<int>();

                    lstZhuanTi = lstZhuanTi_FS.Union(lstZhuanTi_WG).ToList<int>();
                    lstZhuanTi = lstZhuanTi.Union(lstZhuanTi_UM).ToList<int>();
                }
                else if (se.C_TYPE == "16")//专题库质量统计
                {
                    List<int> lst = new List<int>();
                    for (int i = 0; i < leixing.Count; i++)
                    {                        
                        List<int> lstLX = GetCNPList(leixing[i]);//根据LEIXING读取CNP文件
                        List<int> lstSX = GetCNPList(shixiao[i]);//根据失效，有效读取CNP文件
                        List<int> lstlx_sx = lstLX.Intersect(lstSX).ToList<int>();
                        List<int> lsttmp = lstZhuanTi.Intersect(lstlx_sx).ToList<int>();
                        lst = lst.Union(lsttmp).ToList<int>();
                    }
                    lstZhuanTi = lst; 
                }
                lstRs = lstZhuanTi;
            }
            else
            {
                lstRs = lstResult;
            }
            byte[] bteResult = ConvertLstByte.GetListBytes(lstRs);//结果文件

            lstHis = ConvertLstByte.GetCnpList(se.SearchFile);

            // Union  Except  Intersect
            List<int> lstRs_His = lstRs.Except(lstHis).ToList<int>();
            List<int> lstHis_Rs = lstHis.Except(lstRs).ToList<int>();
            List<int> lstExcept = lstRs_His.Union(lstHis_Rs).ToList<int>();

            int Exceptnum = 0;
            if (lstHis.Count != 0)
            {
                Exceptnum = lstExcept.Count;//差异数
            }

            byte[] cbuffer = ConvertLstByte.GetListBytes(lstExcept);//差异文件 
            if (se.C_TYPE.Substring(1, 1) == "7")//计算专利平均寿命
            {
                string avgage = GetPatentAvgAge(lstRs);
                //检索历史
                AddSearchLis(se, bteResult, cbuffer, avgage, Exceptnum.ToString());
            }
            else
            {
                //检索历史
                AddSearchLis(se, bteResult, cbuffer, lstRs.Count.ToString(), Exceptnum.ToString());
            }
            

           
            return strMsg;

        }
        private string GetPatentAvgAge(List<int> lst)
        {
            string avgage = "0";
            List<xmlDataInfo> ie1 = null;
            ClsSearch cls = new ClsSearch();
            List<xmlDataInfo> lstRs = new List<xmlDataInfo>();
            int num = lst.Count / 500;
            if (lst.Count % 500 > 0)
            {
                num = num + 1;
            }
            for (int i = 0; i < num; i++)
            {
                List<int> ls = lst.Skip(i * 500).Take(500).ToList<int>();
                ie1 = cls.GetRestultAge(ls);
                lstRs = lstRs.Union(ie1).ToList<xmlDataInfo>();


            }
            //ie1=cls.GetResult(lst,"CN");
            int age = 0;
            foreach (xmlDataInfo data in lstRs)
            {
                if (!string.IsNullOrEmpty(data.ZLNl))
                {
                    age = age + int.Parse(data.ZLNl);
                }
            }
            if (lstRs.Count() != 0)
            {
                avgage = (age / lstRs.Count()).ToString();
            }
            return avgage;
        }
        /// <summary>
        /// 插入更新历史//检索成功
        /// </summary>
        /// <param name="se"></param>
        /// <param name="srcFilePath">当前检索结果文件</param>
        /// <param name="compareFilePath">差异文件</param>
        /// <param name="hits">结果数</param>
        ///  <param name="hits">差异数</param>
        /// <returns></returns>
        public bool AddSearchLis(Searches se, byte[] strFs, byte[] compareFile, string hits, string changenum)
        {
            int HisOrder_ID = getMaxHisOrder(se.C_ID);
            //插入检索历史表
            string sql = "insert into C_W_SEARCHLIS (W_ID,C_ID,S_NAME,CHANGEDATE,CURRENTNUM,CHANGENUM,SEARCHFILE,COMPAREFILE,TYPE,HisOrder)"
                + " VALUES (@W_ID,@C_ID,@S_NAME,@CHANGEDATE,@CURRENTNUM,@CHANGENUM,@SEARCHFILE,@COMPAREFILE,@TYPE,@HisOrder) ";
            SqlParameter[] param ={
                                     new SqlParameter("@W_ID",SqlDbType.Int,4),
                                     new SqlParameter("@C_ID",SqlDbType.Int,4),
                                       new SqlParameter("@S_NAME", SqlDbType.VarChar ,200),
                                     new SqlParameter("@CHANGEDATE",SqlDbType.DateTime,10),
                                     new SqlParameter("@CURRENTNUM",SqlDbType.Int,4),
                                     new SqlParameter("@CHANGENUM",SqlDbType.Int,4),
                                     new SqlParameter("@SEARCHFILE",SqlDbType.VarBinary,strFs.Length),
                                     new SqlParameter("@COMPAREFILE",SqlDbType.VarBinary,compareFile.Length),
                                     new SqlParameter("@TYPE",SqlDbType.Int,4) ,
                                     new SqlParameter("@HisOrder",SqlDbType.Int,4)
                                 };
            param[0].Value = se.W_ID;
            param[1].Value = se.C_ID;
            param[2].Value = getSNamebyCWID(se.W_ID);
            param[3].Value = System.DateTime.Now;
            param[4].Value = hits;
            param[5].Value = changenum;
            param[6].Value = strFs;
            param[7].Value = compareFile;
            param[8].Value = se.TYPE;
            param[9].Value = HisOrder_ID;
            using (SqlConnection con = new SqlConnection(DBA.SqlDbAccess.ConnStr))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql, param) <= 0)
                {
                    trans.Rollback();
                    return false;
                }
                //修改检索式表 变更日期为当前日期，当前数量为引擎返回数量，差异量为（A-B）+（B-A）
                sql = "UPDATE C_W_SECARCH SET CHANGEDATE=GETDATE(),CURRENTNUM=@CURRENTNUM,CHANGENUM=@CHANGENUM,SEARCHFILE=@SEARCHFILE,COMPAREFILE=@COMPAREFILE WHERE W_ID=@W_ID";
                SqlParameter[] param1 ={
                                          new SqlParameter("@W_ID",se.W_ID),
                                          new SqlParameter("CURRENTNUM",hits),
                                          new SqlParameter("CHANGENUM",changenum),
                                          new SqlParameter("SEARCHFILE",strFs),
                                          new SqlParameter("COMPAREFILE",compareFile)
                                      };
                if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql, param) <= 0)
                {
                    trans.Rollback();
                    return false;
                }

                // add by zhangqiuyi 20151103 增加更新标记
                if (int.Parse(changenum) > 0)
                {
                    sql = "UPDATE C_EARLY_WARNING SET ISUPDATE=@ISUPDATE WHERE C_ID=@C_ID";
                    SqlParameter[] param2 ={
                                              new SqlParameter("@C_ID",se.C_ID),
                                              new SqlParameter("@ISUPDATE","1")
                                          };
                    if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql, param2) <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }
                }

                trans.Commit();
                return true;
            }
        }

        /// <summary>
        /// 插入更新历史//检索失败
        /// </summary>
        /// <param name="se"></param>
        /// <param name="strFs"></param>
        /// <param name="compareFile"></param>
        /// <param name="hits"></param>
        /// <param name="changenum"></param>
        /// <returns></returns>
        public bool AddSearchLis(Searches se)
        {

            int HisOrder_ID = getMaxHisOrder(se.C_ID);

            //插入检索历史表
            string sql = "insert into C_W_SEARCHLIS (W_ID,C_ID,S_NAME,CHANGEDATE,CURRENTNUM,CHANGENUM,TYPE,HisOrder)"
                + " VALUES (@W_ID,@C_ID,@S_NAME,@CHANGEDATE,@CURRENTNUM,@CHANGENUM,@TYPE,@HisOrder) ";
            SqlParameter[] param ={
                                     new SqlParameter("@W_ID",SqlDbType.Int,4),
                                     new SqlParameter("@C_ID",SqlDbType.Int,4),
                                     new SqlParameter("@S_NAME", SqlDbType.VarChar ,200),
                                     new SqlParameter("@CHANGEDATE",SqlDbType.DateTime,10),
                                     new SqlParameter("@CURRENTNUM",SqlDbType.Int,4),
                                     new SqlParameter("@CHANGENUM",SqlDbType.Int,4),                                 
                                     new SqlParameter("@TYPE",SqlDbType.Int,4) ,
                                     new SqlParameter("@HisOrder",SqlDbType.Int,4)
                                 };
            param[0].Value = se.W_ID;
            param[1].Value = se.C_ID;
            param[2].Value = getSNamebyCWID(se.W_ID);
            param[3].Value = System.DateTime.Now;
            param[4].Value = "0";
            param[5].Value = "0";
            param[6].Value = se.TYPE;
            param[7].Value = HisOrder_ID;
            using (SqlConnection con = new SqlConnection(DBA.SqlDbAccess.ConnStr))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql, param) <= 0)
                {
                    trans.Rollback();
                    return false;
                }
                //修改检索式表 变更日期为当前日期，当前数量为引擎返回数量，差异量为（A-B）+（B-A）
                sql = "UPDATE C_W_SECARCH SET CHANGEDATE=GETDATE(),CURRENTNUM=@CURRENTNUM,CHANGENUM=@CHANGENUM WHERE W_ID=@W_ID";
                SqlParameter[] param1 ={
                                          new SqlParameter("@W_ID",se.W_ID),
                                          new SqlParameter("CURRENTNUM","0"),
                                          new SqlParameter("CHANGENUM","0"),                                       
                                      };
                if (DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, sql, param) <= 0)
                {
                    trans.Rollback();
                    return false;
                }
                trans.Commit();
                return true;
            }
        }

        /// <summary>
        /// 发送邮件的程序
        /// </summary>
        /// <param name="UserName"> 登陆邮箱的用户名 </param>
        /// <param name="UserName"> 登陆邮箱的密码  </param>
        /// <returns></returns>
        private bool SendMail(string UserName, string PassWord, string ShoujianRenEmail, string body)
        {
            //System.IO.StreamReader sr = new System.IO.StreamReader(@"D:\aaaaaaaaaaa\huiyuan.htm",Encoding.Default );
            //body = sr.ReadToEnd();
            //sr.Close();

            try
            {
                //编码暂硬性规定为GB2312 
                Encoding encoding = Encoding.GetEncoding("utf-8");
                MailMessage Message = new MailMessage(
                new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["email"].ToString().Trim(), System.Configuration.ConfigurationSettings.AppSettings["UserRealName"].ToString().Trim(), encoding),//第一个是发信人的地址，第二个参数是发信人
                new MailAddress(ShoujianRenEmail));//收信人邮箱
                Message.SubjectEncoding = encoding;
                Message.Subject = "预警信息";//标题
                Message.BodyEncoding = encoding;
                Message.Body = body; //主体

                SmtpClient smtpClient = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["SmtpClient"].ToString().Trim());//信箱服务器
                smtpClient.Credentials = new NetworkCredential(UserName, PassWord);//信箱的用户名和密码
                smtpClient.Timeout = 999999;
                smtpClient.Send(Message);

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private int getMaxHisOrder(int C_ID)
        {
            int HisOrder = 0;
            DataSet ds = new DataSet();
            string sql = "select MAX(HisOrder) from C_W_SEARCHLIS Where [TYPE] =1 and C_ID= " + C_ID + " and CHANGEDATE<DATEADD(n,-10,GETDATE())";

            ds = DBA.SqlDbAccess.GetDataSet(CommandType.Text, sql);

            if (ds.Tables[0].Rows[0][0].ToString().Trim() != "")
            {
                HisOrder = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString().Trim());
            }

            HisOrder++;

            return HisOrder;
        }

        private int getMainCWID(int C_ID)
        {
            int HisOrder = 0;
            DataSet ds = new DataSet();
            string sql = "select W_ID from C_W_SECARCH Where TYPE=1 And C_ID=" + C_ID;

            ds = DBA.SqlDbAccess.GetDataSet(CommandType.Text, sql);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                return 0;
            }

            HisOrder = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString().Trim());

            return HisOrder;
        }
        /// <summary>
        /// 根据WID取得预警名称
        /// </summary>
        /// <param name="W_ID"></param>
        /// <returns></returns>
        private string getSNamebyCWID(int W_ID)
        {
            DataSet ds = new DataSet();
            string sql = "select S_NAME from C_W_SECARCH Where W_ID=" + W_ID;

            ds = DBA.SqlDbAccess.GetDataSet(CommandType.Text, sql);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                return "";
            }

            return ds.Tables[0].Rows[0]["S_NAME"].ToString().Trim();
        }

        /// <summary>
        /// 返回CNP文件号单 
        /// fm.cnp,授权:FS.cnp
        ///新型：um.cnp
        ///外观：wg.cnp
        ///有效：YX.cnp
        ///失效: SX.cnp
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<int> GetCNPList(string type)
        {
            List<int> lstfml = new List<int>();
            int readlength;
            string filepath = basepath + "\\" + type + ".eee";
            if (!File.Exists(filepath))
            {
                return lstfml;
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
    }
}

