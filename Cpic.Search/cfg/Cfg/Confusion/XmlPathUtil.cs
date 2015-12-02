#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: XmlPathUtil.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-7-25 16:03:00
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
using System.Linq;
using System.Text;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Cfg.Confusion
{
    /// <summary>
    ///XmlPathUtil 的摘要说明
    /// </summary>
    public class XmlPathUtil
    {
        //Docdb根据公开号获得xml文件的路径
        public static String getXmlPathByPublicNo_Docdb(String publicno)
        {
            String xmlpath = Common.DocDB_File_Root;
            String xmlpath_16 = publicno.PadLeft(16, '0');
            xmlpath = xmlpath + publicno.Substring(0, 2) + "//" + xmlpath_16.Substring(0, 4) + "//" + xmlpath_16.Substring(4, 4) + "//"
                + xmlpath_16.Substring(8, 4) + "//" + xmlpath_16.Substring(12, 4) + "//" + publicno + ".xml";
            return xmlpath;
        }

        //Dwpi根据入藏号获得文献路径
        public static String getPathByAccession_DWPI(String Accession)
        {
            Accession = Accession.Trim();
            String path = Common.DWPI_File_Root;
            if (!(Accession.Trim().Length == 10 || Accession.Trim().Length == 9))
            {
                throw new Exception("入藏号" + Accession + "不是10位");
            }
            else
            {
                path = path + Accession.Substring(0, 4) + "//" + Accession.Substring(4, 3) + "//";
            }
            String[] fileList = System.IO.Directory.GetFileSystemEntries(path, Accession + "*");
            if (fileList == null || fileList.Length == 0)
            {
                throw new Exception("目录" + path + "中没有入藏号" + Accession + "对应的DWPI文件");
            }
            else
            {
                return fileList[0];
            }
        }
        //根据申请号获得中文摘要文献路径
        public static String getAbstractFilePath_CN(String rootDir, String applyno)
        {
            if (applyno.Length == 12)
            {
                return rootDir + "//" + applyno.Substring(0, 4) + "//" + applyno.Substring(4, 5) + "//" + applyno + ".xml";
            }
            else
            {
                throw new Exception("申请号：" + applyno + "不是12位");
            }
        }

        //根据申请号获得含有错我编码的中文摘要文献路径
        public static String getErrorAbstractFilePath_CN(String applyno)
        {
            if (applyno.Length == 12)
            {
                return Common.CN_ErrorFile_Dir + applyno + ".xml";
            }
            else
            {
                throw new Exception("申请号：" + applyno + "不是12位");
            }
        }

        /// <summary>
        /// 根据申请号和卷期号获得中文全文文献路径
        /// </summary>
        /// <param name="applyno">申请号</param>
        /// <param name="weekno">卷期号</param>
        /// <param name="type">D表示说明书，C表示权利要求</param>
        /// <returns>文献路径</returns>
        public static String getFullFilePath_CN(String applyno, String weekno, String type)
        {
            if (applyno.Length == 14)
            {
                String path = Common.CN_Full_Root;
                if (applyno.Substring(4, 1) == "1" || applyno.Substring(4, 1) == "8")
                {
                    path = path + "FM//";
                }
                if (applyno.Substring(4, 1) == "2" || applyno.Substring(4, 1) == "9")
                {
                    path = path + "XX//";
                }
                if (applyno.Substring(4, 1) == "3")
                {
                    path = path + "WG//";
                }
                path = path + applyno.Substring(0, 4) + "//" + weekno + "//" + applyno.Substring(11, 1) + "//" + applyno + "//" + applyno + type + ".xml";
                return path;
            }
            else
            {
                throw new Exception("中文全文文献对应的申请号格式不正确");
            }
        }
    }
}
