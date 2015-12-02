#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2012 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: Code_DE.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2012-2-14 15:58:08
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
using log4net;
using System.Reflection;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Cfg
{
    /// <summary>
    ///UrlParameterCode_DE 的摘要说明
    ///加密解密URL参数字符串,
    /// </summary>
    public class UrlParameterCode_DE
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private UrlParameterCode_DE()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private static Random rm = new Random();
        private static int c_nMak = 65;   //混淆数字

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string encrypt(string str)
        {
            System.Text.StringBuilder retString = new System.Text.StringBuilder();
            char[] ary = str.ToCharArray();

            for (int i = 0; i <= ary.Length - 1; i++)
            {
                retString.Append(EncryptChar(ary[i]));
            }
            return retString.ToString();
        }
        /// <summary>
        /// 加密一个字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string EncryptChar(char c)
        {
            int rmd1 = 0;
            int rmd2 = 0;
            int rmd3 = 0;
            int sum = 0;

            rmd1 = rm.Next(0, 9);
            rmd2 = rm.Next(0, 9);
            //rmd3 = rm.Next(0, 8);

            sum = c + rmd1 + rmd2;
            int nTmp = 0;

            //设置 sum的取值为数字/字母
            if (sum > 57 && sum < 65)
            {
                nTmp = sum - 57;
                sum = 57;
            }
            else if (sum > 90 && sum < 97)
            {
                nTmp = sum - 90;
                sum = 90;
            }
            else if (sum > 122)
            {
                nTmp = sum - 122;
                sum = 122;
            }

            string s1 = null;
            string s2 = null;
            string s3 = null;
            string s4 = null;

            s1 = ((char)sum).ToString();
            s2 = ((char)(rmd1 + c_nMak)).ToString();
            s3 = ((char)(rmd2 + c_nMak)).ToString();
            s4 = ((char)(nTmp + c_nMak)).ToString();

            return s1 + s2 + s3 + s4;
        }
        /// <summary>
        /// 解密一个字符串 4个解密成一个
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static char Decryption(string str)
        {
            if (str.Length != 4)
            {
                return '\0';
            }
            char[] chrs = str.ToCharArray();
            int c1 = chrs[1] - c_nMak;
            int c2 = chrs[2] - c_nMak;
            int c3 = chrs[3] - c_nMak;
            int sum = chrs[0] + c3;
            return (char)(sum - (c1 + c2));
        }
        /// <summary>
        /// 解密整个字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string DecryptionAll(string str)
        {
            if (str.Length % 4 != 0)
            {
                return string.Empty;
            }
            string strtmp = null;
            char[] chrS = str.ToCharArray();
            System.Text.StringBuilder strContent = new System.Text.StringBuilder();
            for (int i = 0; i <= str.Length - 1; i += 4)
            {
                strtmp = chrS[i].ToString() + chrS[i + 1].ToString() + chrS[i + 2].ToString() + chrS[i + 3].ToString();
                strContent.Append(Decryption(strtmp));
            }
            return strContent.ToString();
        }

    }
}
