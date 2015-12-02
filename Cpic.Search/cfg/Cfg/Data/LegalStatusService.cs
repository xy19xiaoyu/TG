#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2012 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: LegalStatusService.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2012-4-9 16:33:38
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
using System.Data.Linq;
using System.Collections;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Cfg.Data
{
    /// <summary>
    ///LegalStatusService 的摘要说明
    /// </summary>
    public class LegalStatusService
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public LegalStatusService()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static List<CnLegalStatus> GetLegalStateus(string _strApNo)
        {
            try
            {
                CfgLegalStatusDataContext db = new CfgLegalStatusDataContext();
                Table<CnLegalStatus> tbCnleg = db.CnLegalStatus;

                var result = from item in tbCnleg
                             where item.AppNo.Equals(_strApNo)
                             orderby item.LegalDate descending
                             select item;

                //paging... (LINQ)
                //IEnumerable ien = result.Skip((PageNumber - 1) * PageSize).Take(PageSize);
                //IEnumerable ien = result.DefaultIfEmpty();
                return result.ToList<CnLegalStatus>();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public static string GetLegalInfo(string _strInidCode, string _strOldInfo, string _strNewInfo)
        {
            string strRInfo = "";
            try
            {
                if (string.IsNullOrEmpty(_strInidCode))
                {
                    return strRInfo;
                }

                string strTmpF = "[{0}]变更<br/>变更前：{1}<br/>变更后：{2}";
                switch (_strInidCode.Trim().ToUpper())
                {
                    //0=其他，1=发明人,2：专利权人/申请人， 3：专利权人/申请人地址， 4：共同专利权人/共同申请人，5：代理机构，6：代理人，7：发明设计名称，8：优先权项，9：申请日
                    case "0":
                        strRInfo = "其他";
                        break;
                    case "1":
                        strRInfo = "发明人";
                        break;
                    case "2":
                        strRInfo = "专利权人/申请人";
                        break;
                    case "3":
                        strRInfo = "专利权人/申请人地址";
                        break;
                    case "4":
                        strRInfo = "共同专利权人/共同申请人";
                        break;
                    case "5":
                        strRInfo = "代理机构";
                        break;
                    case "6":
                        strRInfo = "代理人";
                        break;
                    case "7":
                        strRInfo = "发明设计名称";
                        break;
                    case "8":
                        strRInfo = "优先权项 ";
                        break;
                    case "9":
                        strRInfo = "申请日";
                        break;
                    default:
                        strRInfo = "其他";
                        break;
                }
                strRInfo = string.Format(strTmpF, strRInfo, _strOldInfo, _strNewInfo);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return strRInfo;
        }

        public static string GetLegalCodeInfo(string _strCode)
        {
            string strRInfo = "无";

            try
            {
                switch (_strCode.Trim().ToUpper())
                {
                    case "A":
                        strRInfo = "公开";
                        break;
                    case "C":
                        strRInfo = "审定";
                        break;
                    case "D":
                        strRInfo = "授权";
                        break;
                    case "B":
                        strRInfo = "实质审查请求的生效 ";
                        break;
                    case "E":
                        strRInfo = "专利申请的驳回";
                        break;
                    case "F":
                        strRInfo = "专利申请的撤回";
                        break;
                    case "G":
                        strRInfo = "专利申请的视为撤回";
                        break;
                    case "H":
                        strRInfo = "专利权的撤销（全部撤销）";
                        break;
                    case "I":
                        strRInfo = "专利权的撤销（部分撤销）";
                        break;
                    case "J":
                        strRInfo = "专利权的无效宣告(专利权全部无效)";
                        break;
                    case "K":
                        strRInfo = "专利权的无效宣告(专利权部分无效)";
                        break;
                    case "L":
                        strRInfo = "专利权的视为放弃";
                        break;
                    case "M":
                        strRInfo = "专利权的终止(专利权的主动放弃)";
                        break;
                    case "N":
                        strRInfo = "专利权的终止(未缴年费)";
                        break;
                    case "O":
                        strRInfo = "专利权的终止(专利权有效期届满)";
                        break;
                    case "Q":
                        strRInfo = "专利权有效期的续展";
                        break;
                    case "P":
                        strRInfo = "专利权的恢复";
                        break;
                    case "R":
                        strRInfo = "著录项目变更";
                        break;
                    case "S":
                        strRInfo = "中止程序";
                        break;
                    case "V":
                        strRInfo = "专利申请的恢复";
                        break;
                    case "W":
                        strRInfo = "避免重复授权放弃专利权";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return strRInfo;
        }
    }
}
