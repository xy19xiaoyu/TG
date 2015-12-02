#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: DmManager.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-7-29 10:11:51
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
using System.Data.Linq;
using log4net;
using System.Reflection;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Cfg
{
    /// <summary>
    ///DmManager 的摘要说明
    /// </summary>
    public class DmManager
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public static Dictionary<string, string> DicGsDm = new Dictionary<string, string>();
        static DmManager()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
            CfgDataManagerDataContext db = new CfgDataManagerDataContext();
            Table<CountryConfig> tbCountryConfig = db.CountryConfig;

            var result = from item in tbCountryConfig
                         select new
                         {
                             Key = item.DaiMa.Trim(),
                             Value = item.MingCheng.Trim(),
                         };
            DicGsDm = result.ToDictionary(c => c.Key, c => c.Value);
        }


        /// <summary>
        /// 根据国省代码返回名称
        /// </summary>
        /// <param name="_strCNM"></param>
        /// <returns></returns>
        public static string getCCName(string _strCNM)
        {
            try
            {
                return DicGsDm[_strCNM];
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return _strCNM;
            }
        }

        /// <summary>
        /// 根据国省代码返回显示内容
        /// </summary>
        /// <param name="_strCNM"></param>
        /// <returns></returns>
        public static string getCC_Display(string _strCNM)
        {
            try
            {
                return string.Format("{0}({1})", DicGsDm[_strCNM], _strCNM);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return _strCNM;
            }
        }

        /// <summary>
        /// 根据代理机构代码返回MC
        /// </summary>
        /// <param name="_strAGNM"></param>
        /// <returns></returns>        
        public static string getAGame(string _strAGNM)
        {
            try
            {

                CfgDataManagerDataContext db = new CfgDataManagerDataContext();
                Table<ZPT_SJWH_DLJGPZB> tbZpt = db.ZPT_SJWH_DLJGPZB;


                var result = from item in tbZpt
                             where item.DAILIJGDM.Equals(_strAGNM.Trim())
                             select item;


                if (result.Count() > 0)
                {
                    return result.First().DAILIJGMC.Trim();
                }
                else
                {
                    return _strAGNM;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return _strAGNM;
            }
        }

        /// <summary>
        /// 根据代理机构代码返回显示内容
        /// </summary>
        /// <param name="_strAGNM"></param>
        /// <returns></returns>
        public static string getAG_Display(string _strAGNM)
        {
            try
            {

                CfgDataManagerDataContext db = new CfgDataManagerDataContext();
                Table<ZPT_SJWH_DLJGPZB> tbZpt = db.ZPT_SJWH_DLJGPZB;


                var result = from item in tbZpt
                             where item.DAILIJGDM.Equals(_strAGNM.Trim())
                             select item;


                if (result.Count() > 0)
                {
                    return string.Format("{0}({1})", result.First().DAILIJGMC.Trim(), _strAGNM);
                }
                else
                {
                    return _strAGNM;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return _strAGNM;
            }
        }


        /// <summary>
        /// 根据代理机构代码返回getAG_Addres
        /// </summary>
        /// <param name="_strAGNM"></param>
        /// <returns></returns>
        public static string getAG_Addres(string _strAGNM)
        {
            try
            {

                CfgDataManagerDataContext db = new CfgDataManagerDataContext();
                Table<ZPT_SJWH_DLJGPZB> tbZpt = db.ZPT_SJWH_DLJGPZB;


                var result = from item in tbZpt
                             where item.DAILIJGDM.Equals(_strAGNM.Trim())
                             select item;


                if (result.Count() > 0)
                {
                    return string.Format("{0}({1}))", result.First().DAILIJGDZ.Trim(), result.First().DAILIJGYB.Trim());
                }
                else
                {
                    return _strAGNM;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return _strAGNM;
            }
        }

        /// <summary>
        /// 获取用户自定义信息
        /// </summary>
        /// <param name="_strApNo"></param>
        /// <param name="_strUid"></param>
        /// <returns></returns>
        public static string getUsetInfo(string _strApNo, int _nUid)
        {
            string strInfo = "";
            try
            {
                CfgDataManagerDataContext db = new CfgDataManagerDataContext();
                Table<Tb_UsetPatentInfo> tbUsetInfo = db.Tb_UsetPatentInfo;

                strInfo = db.Tb_UsetPatentInfo.First(p => p.UId.Equals(_nUid) && p.ApNo.Equals(_strApNo)).Info;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return strInfo;
        }
    }
}
