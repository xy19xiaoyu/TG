#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: User.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-9-27 13:21:17
*	版    本: V1.0
*	备注描述: $Myparameter1$           
*
* 修改历史: 
*   ****NO_1:
*	修 改 人: 
*	修改日期: 
*	描    述: $Myparameter1$           
******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Cpic.Cprs2010.Search;
using Cpic.Cprs2010.Cfg;
using System.IO;
using System.Text.RegularExpressions;

namespace Cpic.Cprs2010.User
{
    [Serializable]
    /// <summary>
    /// 用户
    /// </summary>
    public class User : TbUserInfoInfo
    {
        /// <summary>
        /// 用户类的构造函数
        /// </summary>
        public User()
        {
        }

        private string _UserPath;
        /// <summary>
        /// 用户目录
        /// </summary>
        public string UserPath
        {
            get
            {
                if (string.IsNullOrEmpty(_UserPath))
                {
                    _UserPath = CprsConfig.GetUserPath(ID,"");
                }
                return _UserPath;
            }
        }


        /// <summary>
        /// 得到用户的检索历史List
        /// </summary>
        public System.Collections.Generic.List<string> getSearchHis(SearchDbType DBType)
        {
            string hisFilePath = UserPath + "\\" + DBType;
            string hisFile = hisFilePath + "\\SearchHistory.lst";
            List<string> SearchHis = new List<string>();
            if (File.Exists(hisFile))
            {

                //创建流
                using (StreamReader sr = new StreamReader(new FileStream(hisFile, FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        SearchHis.Add(sr.ReadLine());
                    }
                }
            }
            return SearchHis;
        }

        /// <summary>
        /// 得到用户的检索历史文件路径
        /// </summary>
        public string getSearchHisPath(SearchDbType DBType)
        {
            string hisFilePath = UserPath + "\\" + DBType;
            string hisFile = hisFilePath + "\\SearchHistory.lst";
            string SearchHis = "";
            if (File.Exists(hisFile))
            {
                SearchHis = hisFile;
            }
            return SearchHis;
        }

        /// <summary>
        /// 得到用户的检索历史存放临时目录
        /// </summary>
        public string getSearchHisTempDir()
        {
            string _tmpDir = System.Configuration.ConfigurationSettings.AppSettings["CPRS2010SearchHistoryPath"].ToString();
            //string _tmpDir = @"\\192.168.70.60\History_Temp\";
            return _tmpDir;
        }

        /// <summary>
        /// 清除用户检索历史
        /// </summary>
        public bool clearSearchHis(SearchDbType DBType)
        {
            return UserManager.clearSearchHis(ID, DBType) && UserManager.clearSearchHisData(ID, DBType);
        }

        /// <summary>
        /// 添加某一用户的检索历史
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public bool addSearchHis(SearchPattern sp)
        {
            FileMode f;
            string hisFilePath = UserPath + "\\" + sp.DbType;
            string hisFile = hisFilePath + "\\SearchHistory.lst";
            //创建目录
            if (!Directory.Exists(hisFilePath))
            {
                Directory.CreateDirectory(hisFilePath);
            }

            if (File.Exists(hisFile))
            {
                f = FileMode.Append;
            }
            else
            {
                f = FileMode.OpenOrCreate;
            }


            //创建流
            using (StreamWriter sw = new StreamWriter(new FileStream(hisFile, f)))
            {
                sw.WriteLine("(" + sp.SearchNo + ")" + sp.Pattern);
            }

            return true;
        }

        /// <summary>
        /// 添加某一用户的检索历史,添加内容为strHis
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public bool addSearchHis(SearchPattern sp, string strHis)
        {
            string _strHis = validateFormat(strHis);
            if (_strHis == "") return false;

            FileMode f;
            string hisFilePath = UserPath + "\\" + sp.DbType;
            string hisFile = hisFilePath + "\\SearchHistory.lst";
            //创建目录
            if (!Directory.Exists(hisFilePath))
            {
                Directory.CreateDirectory(hisFilePath);
            }

            if (File.Exists(hisFile))
            {
                f = FileMode.Append;
            }
            else
            {
                f = FileMode.OpenOrCreate;
            }


            //创建流
            using (StreamWriter sw = new StreamWriter(new FileStream(hisFile, f)))
            {
                sw.WriteLine(strHis);
            }

            return true;
        }

        /// <summary>
        /// 验证检索式格式，错误则返回""
        /// </summary>
        /// <param name="strHis"></param>
        /// <returns></returns>
        private string validateFormat(string strHis)
        {
            string _strHis = strHis;
            string reg = "";

            return _strHis;
        }

        /// <summary>
        /// 删除指定编号的检索历史
        /// 同时删除相关文件，例如删除002.txt以及002.Cnp文件
        /// searchNum 必须为002格式
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="searchNum"></param>
        /// <returns></returns>
        public bool deleteBatchSearchHis(SearchPattern sp, string[] searchNumArr)
        {
            FileMode f = FileMode.Create;
            string hisFilePath = UserPath + "\\" + sp.DbType;
            string hisFile = hisFilePath + "\\SearchHistory.lst";
            //创建目录
            if (!Directory.Exists(hisFilePath))
            {
                Directory.CreateDirectory(hisFilePath);
            }


            try
            {
                string[] strText;
                // 读取文件内容
                strText = File.ReadAllLines(hisFile);

                // 寻找对应的检索编号并删除
                using (StreamWriter sw = new StreamWriter(new FileStream(hisFile, f)))
                {
                    foreach (string item in strText)
                    {
                        // 如果该检索式跟所有待删除的编号都不匹配，则保留。跟一个匹配，都与删除
                        bool bDelete = false;
                        foreach (string searchNum in searchNumArr)
                        {
                            string pat = @"(" + searchNum + ")";
                            if (item.IndexOf(pat) != -1) // 删除编号对应的检索式
                                bDelete = true;

                        }
                        if (bDelete == false)
                            sw.WriteLine(item);

                    }
                }

                // 删除相关文件
                foreach (string searchNum in searchNumArr)
                {
                    string strTxt = hisFilePath + "\\" + searchNum + ".txt";
                    string strCnp = hisFilePath + "\\" + searchNum + ".Cnp";
                    File.Delete(strTxt);
                    File.Delete(strCnp);

                    //add xiwl 再册除与此编号相关的所有文件
                    foreach (string strItm in Directory.GetFiles(hisFilePath, searchNum + ".*.cnp", SearchOption.TopDirectoryOnly))
                    {
                        try
                        {
                            File.Delete(strItm);
                        }
                        catch (Exception er)
                        {
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return true;
        }

        /// <summary>
        /// 删除指定编号的检索历史
        /// 同时删除相关文件，例如删除002.txt以及002.Cnp文件
        /// searchNum 必须为002格式
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="searchNum"></param>
        /// <returns></returns>
        public bool deleteSearchHis(SearchPattern sp, string searchNum)
        {
            FileMode f = FileMode.Create;
            string hisFilePath = UserPath + "\\" + sp.DbType;
            string hisFile = hisFilePath + "\\SearchHistory.lst";
            //创建目录
            if (!Directory.Exists(hisFilePath))
            {
                Directory.CreateDirectory(hisFilePath);
            }


            try
            {
                string[] strText;
                // 读取文件内容
                strText = File.ReadAllLines(hisFile);



                // 寻找对应的检索编号并删除
                using (StreamWriter sw = new StreamWriter(new FileStream(hisFile, f)))
                {
                    foreach (string item in strText)
                    {
                        string pat = @"(" + searchNum + ")";
                        if (item.IndexOf(pat) == -1) // 找不到，则写文件
                            sw.WriteLine(item);

                    }
                }

                // 删除相关文件
                string strTxt = hisFilePath + "\\" + searchNum + ".txt";

                foreach (string strItm in Directory.GetFiles(hisFilePath, searchNum + ".*.cnp", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        File.Delete(strItm);
                    }
                    catch (Exception er)
                    {
                    }
                }

                string strCnp = hisFilePath + "\\" + searchNum + ".Cnp";
                File.Delete(strTxt);
            }
            catch (Exception e)
            {

            }
            return true;
        }
    }
}
