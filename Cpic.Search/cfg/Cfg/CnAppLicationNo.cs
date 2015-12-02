#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2012 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: CnAppLicationNo.cs
*	创 建 人: xiwenlei(xiwl) $ wangshuxin(wsx)
*	创建日期: 2012-1-5 13:43:17
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
namespace Cpic.Cprs2010.Cfg
{
    /// <summary>
    ///CnAppLicationNo 的摘要说明
    /// </summary>
    public class CnAppLicationNo
    {
        /// <summary>
        ///公开方法
        ///判断申请号的合法性
        ///输入：申请号
        ///输出：布尔型，表示申请号是否合法
        ///判断方法：依据申请时间，04年以后的，采用13位的校验算法，否则，采用11位的校验算法
        /// </summary>
        /// <param name="AppNo">[13-14]位申请号|[9-10]位申请号</param>
        /// <returns></returns>        
        public static bool ValidCheck(string AppNo)
        {
            //移出校验位与号码间的.符号
            AppNo = AppNo.Replace(".", "");
            int Year = GetYear(AppNo);
            switch (AppNo.Length)
            {
                case 13://13位申请号
                    if (Year > 2003)
                        return CheckFCode13(AppNo);
                    else if (Year == -1)
                        return false;
                    else
                    {
                        AppNo = AppNo13to11(AppNo);
                        if (AppNo == string.Empty)
                            return false;
                        else
                            return CheckFCode11(AppNo);
                    }
                case 11://11位申请号
                    if (Year > 2003)
                    { //2003年10月1日启用的编号体系：结构:申请年号(+专利申请种类 + 申请顺序号 + 计算机校验位),共13位   ？？？？  LY April,17  2006  year为Integer型？
                        AppNo = AppNo11to13(AppNo);
                        if (AppNo == string.Empty)
                            return false;
                        else
                            return CheckFCode13(AppNo);
                    }
                    else if (Year == -1)
                        return false;
                    else
                        return CheckFCode11(AppNo);
                case 9://9位申请号
                    if (Year > 2003)
                    {// 20060620 modified by Liuyan  //AppNo = ToAppNo13(AppNo)  
                        AppNo = AppNo9to13(AppNo);
                        if (AppNo == string.Empty)
                            return false;
                        else
                            return CheckFCode13(AppNo);
                    }
                    else if (Year == -1)
                    {
                        return false;
                    }
                    else
                    {
                        AppNo = AppNo9to11(AppNo);
                        if (AppNo == String.Empty)
                            return false;
                        else
                            return CheckFCode11(AppNo);
                    }


            }
            return false;
        }

        //内部方法
        //获得申请号的申请年
        //输入，正确的申请号
        //输出，整型，表示该申请号的申请年
        private static int GetYear(string AppNo)
        {
            int year = -1;
            try
            {
                switch (AppNo.Length)
                {
                    case 9:
                        //If Convert.ToInt32(AppNo.Substring(0, 2)) < 50 And Convert.ToInt32(AppNo.Substring(0, 2)) > 99 Then
                        //year = Convert.ToInt32("20" + AppNo.Substring(0, 2))
                        //Else
                        //    year = Convert.ToInt32("19" + AppNo.Substring(0, 2))
                        //End If
                        // 20060620  modified by Liuyan
                        if (Convert.ToInt32(AppNo.Substring(0, 2)) > 50 && Convert.ToInt32(AppNo.Substring(0, 2)) <= 99)
                            year = Convert.ToInt32("19" + AppNo.Substring(0, 2));
                        else
                            year = Convert.ToInt32("20" + AppNo.Substring(0, 2));
                        break;
                    case 11:
                    case 13: year = Convert.ToInt32(AppNo.Substring(0, 4));
                        break;

                }
            }
            catch (Exception ex)
            {
                year = -1;
            }
            return year;
        }
        //内部方法
        //验证04年以后的13位申请号的合法性
        //输入，正确的13位申请号
        //输出，布尔类型，申请号是否合法
        private static bool CheckFCode13(string AppNo)
        {
            int iMul;
            int iSum = 0;
            //计算校验和，如果模为10，则校验位应为“X”，否则，应为模的值
            try
            {
                //从第一位到倒数第二位
                for (int i = 0; i <= AppNo.Length - 2; i++)
                {
                    iMul = i % 8 + 2;
                    iSum += Convert.ToInt32(AppNo.Substring(i, 1)) * iMul; //The Verify Function 
                }
                iSum = iSum % 11;
                if (iSum == 10)
                    if ("X".Equals(AppNo.Substring(12, 1).ToUpper()))
                        return true;
                    else
                        return false;
                else
                    if (AppNo.Substring(12, 1).Equals(iSum.ToString()))
                        return true;
                    else
                        return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //内部
        //将合理的申请号13位缩略为11位
        //输入，正确的13位申请号
        //输出，对应的11位申请号
        //缩略方法：即将第6-8位变为1位，即'A'+int(6-8位)-10
        private static string AppNo13to11(string AppNo)
        {
            string specString = AppNo.Substring(5, 3);    //specString=申请号第6-8位
            string afterFCode = AppNo.Substring(8, 5);    //afterFCode=申请号第9-13位
            string preFCode = AppNo.Substring(0, 5);      //preFCode=申请号前5位
            //IFormatProvider provider = Nothing;
            try
            {
                if (Convert.ToInt32(specString) < 10)
                    specString = specString.Substring(2, 1);   //将specString变为ASCII('A')+specString-10,例如，010变为A,011变为B
                else
                {
                    char cha = 'A';
                    //specString = Convert.ToChar(CType(cha, IConvertible).ToInt16(provider) + Convert.ToInt32(specString) - 10)
                    specString = Convert.ToString(Convert.ToInt16(cha) + Convert.ToInt32(specString) - 10);
                }
            }
            catch (Exception ex)
            {
                return string.Empty;  //出错,则返回string.empty
            }
            return preFCode + specString + afterFCode;
        }

        //内部
        //申请号11位恢复为13位
        //输入，正确的11位申请号
        //输出，对应的13位申请号
        private static string AppNo11to13(string AppNo)
        {
            string specString = AppNo.Substring(5, 1);      //specString=第6位
            string afterFCode = AppNo.Substring(6, 5);      //afterFCode=7-11位
            string preFCode = AppNo.Substring(0, 5);        //preFCode=申请号前5位
            if ("9".CompareTo(specString) >= 0)
                specString = "00" + specString;
            else if (specString.Length < 2)
                //specString = "0" + Convert.ToString(Convert.ToInt32(specString.Chars(0)) - 55);   //0'+ IntToStr(10 + integer(specChar[1])-integer('A'));
                specString = "0" + Convert.ToString(Convert.ToInt32(specString[0]) - 55);
            else
                specString = "0" + specString;
            return preFCode + specString + afterFCode;
        }

        //内部方法
        //验证04年以前的申请号的合法性，按11位申请号计算校验
        //输入，正确的11位申请号
        //输出，布尔类型，申请号是否合法
        private static bool CheckFCode11(string AppNo)
        {
            int sum = 0;
            try
            {
                //如果申请号为第6位为A（即缩略号）,则，用13位号验证算法
                if ("A".CompareTo(AppNo.Substring(5, 1).ToUpper()) <= 0)
                {
                    AppNo = AppNo11to13(AppNo);
                    return CheckFCode13(AppNo);
                }
                //计算校验和，如果模为10，则校验位应为“X”，否则，应为模的值
                for (int i = 2; i <= AppNo.Length - 2; i++)
                {
                    if ("0".CompareTo(AppNo.Substring(i, 1)) <= 0 && "9".CompareTo(AppNo.Substring(i, 1)) >= 0)
                        sum += Convert.ToInt32(AppNo.Substring(i, 1)) * i;
                    else
                        return false;
                }
                //Now Checking  the Sum 
                sum = sum % 11;
                if (sum == 10)
                { //10 用X代表了
                    if ("X".Equals(AppNo.Substring(10, 1).ToUpper()))
                        return true;
                    return false;
                }
                else
                {//others are Digital 
                    if (AppNo.Substring(10, 1).Equals(sum.ToString()))
                        return true;
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //内部方法
        //申请号从9位变11位
        //输入，正确的9位申请号
        //输出，对应的11位申请号
        private static string AppNo9to11(string AppNo)
        {
            int Year = GetYear(AppNo);
            if (Year > 1950 && Year <= 1999)//如果年在50-99之间, 申请号前加19
                return "19" + AppNo;
            else if (Year == -1)
                return string.Empty;
            else
                return "20" + AppNo;
        }
        //内部方法
        //申请号9位变为13位
        //输入，正确的9位申请号
        //输出，对应的13位申请号
        //变化方法：先9位变11位,再11位变13位
        private static string AppNo9to13(string AppNo)
        {
            //先9变11
            AppNo = AppNo9to11(AppNo);
            //再11变13
            return AppNo11to13(AppNo);
        }


        /// <summary>
        /// 根据申请号计算校验位,89年之前没有校验位
        /// </summary>
        /// <param name="AppNo">申请号[8|12]</param>
        /// <returns>字符串，申请号校验位</returns>
        public static string getValidCode(string AppNo)
        {
            string strValidCode = "";
            try
            {
                AppNo = AppNo.Trim();
                int nLen = AppNo.Length;
                if (nLen == 8 || nLen == 12)
                {
                    //合法申请号，计算校验位
                    int iMul;
                    int iSum = 0;
                    //从第一位到最后一位
                    for (int i = 0; i < nLen; i++)
                    {
                        iMul = (nLen == 12) ? (i % 8 + 2) : i + 2;
                        iSum += Convert.ToInt32(AppNo.Substring(i, 1)) * iMul; //The Verify Function 
                    }

                    iSum = iSum % 11;

                    strValidCode = iSum == 10 ? "X" : iSum.ToString();
                }
            }
            catch (Exception ex)
            {
                strValidCode = "";
            }
            return strValidCode;
        }

        /// <summary>
        /// 申请号有效验证
        /// </summary>
        /// <param name="_strApNo">[13-14]位申请号|[9-10]位申请号</param>
        /// <returns></returns>
        public static bool Check_ApNoAddVCode(string _strApNo)
        {
            bool bRe = false;

            try
            {
                _strApNo = _strApNo.Replace(".", "");

                string strApNo = "";
                string strApNocValidCode = "";

                switch (_strApNo.Length)
                {
                    case 9:
                        strApNo = _strApNo.Substring(0, 8);
                        strApNocValidCode = _strApNo.Substring(8);

                        bRe = strApNocValidCode.Equals(getValidCode(strApNo));
                        break;
                    case 13://13位申请号
                        int Year = GetYear(_strApNo);

                        //2003年12位申请号的第7位为1;8位申请号第7位为0
                        //20 039 0100001
                        if ((Year < 2003) || (Year == 2003 && _strApNo.Substring(6, 1) == "0"))
                        {
                            strApNo = _strApNo.Substring(2, 3) + _strApNo.Substring(7, 5);
                        }
                        else
                        {
                            strApNo = _strApNo.Substring(0, 12);
                        }
                        strApNocValidCode = _strApNo.Substring(12);

                        bRe = strApNocValidCode.ToUpper().Equals(getValidCode(strApNo));
                        break;
                    default:
                        bRe = false;
                        break;

                }
            }
            catch (Exception ex)
            {
            }

            return bRe;
        }
    }
}

