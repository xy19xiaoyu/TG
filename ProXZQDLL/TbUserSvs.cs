#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2013 @ CPIC Corp
*	CLR 版本: 2.0.50727.3643
*	文 件 名: WgImgExport.cs
*	创 建 人: xiwenlei(xiwl)
*	创建日期: 2013-9-7 14:15:53
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
using System.Data;

namespace ProXZQDLL
{
    /// <summary>
    /// 用户服务类
    /// </summary>
    public class TbUserSvs
    {
        public enum EntrancesType
        {
            Cn = 1,
            En = 2,
        }

        /// <summary>
        /// 根据用户ID获取其配置的检索入口
        /// </summary>
        /// <param name="entType"></param>
        /// <param name="strUid"></param>
        /// <returns></returns>
        public static string getEntrances(EntrancesType entType, string strUid)
        {
            string strRsValues = "";
            try
            {
                strRsValues = DBA.SqlDbAccess.ExecuteScalar(CommandType.Text,
                    string.Format("select {1}_Entrances from TbUser where ID={0}", Convert.ToInt32(strUid), entType.ToString())).ToString();
            }
            catch (Exception ex)
            {
            }
            return strRsValues;
        }



        public static bool updateEntrancesCfg(string strEntrances, EntrancesType entType, string strUid)
        {
            bool bRs = false;
            try
            {
                string strUpSql = "Update TbUser set {0}_Entrances='{1}' where ID={2}";
                strEntrances = strEntrances.Equals("") ? "," : strEntrances;

                int nLoginUid = Convert.ToInt32(strUid);
                DBA.SqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strUpSql, entType.ToString(), strEntrances, nLoginUid));
                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }
            return bRs;
        }
    }
}
