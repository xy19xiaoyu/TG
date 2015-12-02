#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: FileChoose.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-7-25 15:55:54
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
using System.IO;
using Cpic.Cprs2010.Cfg.Confusion;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Cfg
{
    /// <summary>
    ///FileChoose 的摘要说明
    /// </summary>
    public class FileChoose
    {
        public static String key = "this is cnpat c# key";
        private static String encode;


        public FileChoose(String encode)
        {
            encode = encode;
        }

        //加密字符串
        public static  string EncryptString(string str, string key)
        {
            byte[] bStr = (new UnicodeEncoding()).GetBytes(str);
            byte[] bKey = (new UnicodeEncoding()).GetBytes(key);

            for (int i = 0; i < bStr.Length; i += 2)
            {
                for (int j = 0; j < bKey.Length; j += 2)
                {
                    bStr[i] = Convert.ToByte(bStr[i] ^ bKey[j]);
                }
            }

            return (new UnicodeEncoding()).GetString(bStr).TrimEnd('\0');
        }

        //解密字符串
        public static  string DecryptString(string str, string key)
        {
            return EncryptString(str, key);
        }


        //把文件srcFile加密后写到文件desFile中
        private static void CopyAFileWithEncrypt(String srcFile, String desFile)
        {
            String content = "";
            using (FileStream fsread = new FileStream(srcFile, FileMode.Open))
            {
                using (StreamReader srread = new StreamReader(fsread, Encoding.GetEncoding(encode)))
                {
                    content = srread.ReadToEnd();
                    content = EncryptString(content, key);
                }
            }

            FileInfo file = new FileInfo(desFile);
            if (!Directory.Exists(file.DirectoryName))
            {
                Directory.CreateDirectory(file.DirectoryName);
            }
            using (FileStream fsWrite = new FileStream(desFile, FileMode.Create))
            {
                using (StreamWriter srWrite = new StreamWriter(fsWrite, Encoding.GetEncoding(encode)))
                {
                    srWrite.Write(content);
                }
            }
        }

        //把文件srcFile解密后写到文件desFile中
        private static void CopyAFileWithDecrypt(String srcFile, String desFile)
        {
            String content = "";
            using (FileStream fsread = new FileStream(srcFile, FileMode.Open))
            {
                using (StreamReader srread = new StreamReader(fsread, Encoding.GetEncoding(encode)))
                {
                    content = srread.ReadToEnd();
                    content = DecryptString(content, key);
                }
            }

            FileInfo file = new FileInfo(desFile);
            if (!Directory.Exists(file.DirectoryName))
            {
                Directory.CreateDirectory(file.DirectoryName);
            }
            using (FileStream fsWrite = new FileStream(desFile, FileMode.Create))
            {
                using (StreamWriter srWrite = new StreamWriter(fsWrite, Encoding.GetEncoding(encode)))
                {
                    srWrite.Write(content);
                }
            }
        }

        /// <summary>
        /// 把xml文件加密后从srcDir目录中保存到desDir目录中
        /// </summary>
        /// <param name="AppNoFilePath">申请号列表文件</param>
        /// <param name="srcDir">xml文件的原始目录</param>
        /// <param name="desDir">拷贝xml文件的目的目录</param>
        public static  void CopyFilesWithEncrypt(String AppNoFilePath, String srcDir, String desDir)
        {
            using (FileStream fsread = new FileStream(AppNoFilePath, FileMode.Open))
            {
                using (StreamReader srread = new StreamReader(fsread, Encoding.GetEncoding("utf-8")))
                {
                    String ApNo = srread.ReadLine();
                    while (ApNo != null && ApNo.Trim() != "")
                    {
                        String srcFile = XmlPathUtil.getAbstractFilePath_CN(srcDir, ApNo);
                        String desFile = srcFile.Replace(srcDir, desDir);
                        CopyAFileWithEncrypt(srcFile, desFile);
                        ApNo = srread.ReadLine();
                    }
                }
            }
        }

        /// <summary>
        /// 把xml文件解解后从srcDir目录中保存到desDir目录中
        /// </summary>
        /// <param name="AppNoFilePath">申请号列表文件</param>
        /// <param name="srcDir">xml文件的原始目录</param>
        /// <param name="desDir">拷贝xml文件的目的目录</param>
        public static  void CopyFilesWithDecrypt(String AppNoFilePath, String srcDir, String desDir)
        {
            using (FileStream fsread = new FileStream(AppNoFilePath, FileMode.Open))
            {
                using (StreamReader srread = new StreamReader(fsread, Encoding.GetEncoding("utf-8")))
                {
                    String ApNo = srread.ReadLine();
                    while (ApNo != null && ApNo.Trim() != "")
                    {
                        String srcFile = XmlPathUtil.getAbstractFilePath_CN(srcDir, ApNo);
                        String desFile = srcFile.Replace(srcDir, desDir);
                        CopyAFileWithDecrypt(srcFile, desFile);
                        ApNo = srread.ReadLine();
                    }
                }
            }
        }


    }
}
