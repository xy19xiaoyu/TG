#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.1433
*	文 件 名: com.cs
*	创 建 人: xiwenlei(ChenXiaoYu) $ chenxiaoyu(xy)
*	创建日期: 2010-10-18 11:02:38
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
using System.Reflection;
/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Index
{
   
    /// <summary>
    ///com 的摘要说明
    /// </summary>
   public class com
    {
        public com()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 把时间差转换成D天hh小时MM分钟ss秒
        /// </summary>
        /// <param name="tmspan"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FormatTimeSpan(TimeSpan tmspan)
        {
            String TimeString = String.Empty;
            if (tmspan.Days > 0)
            {
                TimeString += tmspan.Days + "天";
            }
            if (tmspan.Hours > 0)
            {
                TimeString += tmspan.Hours + "小时";
            }
            if (tmspan.Minutes > 0)
            {
                TimeString += tmspan.Minutes + "分钟";
            }
            if (tmspan.Seconds > 0)
            {
                TimeString += tmspan.Seconds + "秒";
            }
            TimeString += tmspan.Milliseconds + "毫秒";
            return TimeString;
        }
        public static string FormatFileSize(long size)
        {
            if (size > Convert.ToDouble(1024.00 * 1024 * 1024 * 1024 ))
            {
                return BTOTB(size).ToString() + "TB";
            }
            if (size > Convert.ToDouble(1024.00 * 1024 * 1024 ))
            {
                return BTOGB(size).ToString() + "GB";
            }
            if (size > Convert.ToDouble(1024.00 * 1024 ))
            {
                return BTOMB(size).ToString() + "MB";
            }
            if (size > Convert.ToDouble(1024.00))
            {
                return BTOKB(size).ToString() + "KB";
            }
            if (size <= Convert.ToDouble(1024.00))
            {
                return size.ToString() + "B";
            }
            return string.Empty;
        }
        public static double BTOKB(long Size)
        {
            return Convert.ToDouble((Size / 1024).ToString("0.00"));
        }

        public static double BTOMB(long Size)
        {
            return Convert.ToDouble((BTOKB(Size) / 1024).ToString("0.00"));
        }

        public static double BTOGB(long Size)
        {
            return Convert.ToDouble((BTOMB(Size) / 1024).ToString("0.00"));
        }

        public static double BTOTB(long Size)
        {
            return Convert.ToDouble((BTOGB(Size) / 1024).ToString("0.00"));
        }

    }
}
