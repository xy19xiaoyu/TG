#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2011 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: cnDataFromTransPrj.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2011-9-8 9:51:08
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
using Cpic.Cprs2010.Cfg.Data;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Cfg.Port
{
    /// <summary>
    ///cnDataFromTransPrj 对外检索中文说明书及权利要求通过翻译项目获取数据
    /// </summary>
    public class cnDataFromTransPrj
    {
        private cnDataFromTransPrj()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static cnDataFromTransPrj ins = new cnDataFromTransPrj();


        /// <summary>
        /// 取得类实例
        /// </summary>
        /// <returns>单例实例</returns>
        public static cnDataFromTransPrj getInstance()
        {
            return ins;
        }

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// 得到指定申请号的权利要求文件的全路径
        /// </summary>
        /// <param name="_strApNo">申请号为12位,8位内部转换</param>
        public string getClamXmlFile(string _strApNo)
        {
            string strFilePath = "";
            try
            {
                //TBD: 8位转12位
                //_strApNo = cnDataService.ApNo8To12(_strApNo);
                cnDataFromTransService.ServiceSoapClient client = new cnDataFromTransService.ServiceSoapClient();
                strFilePath = client.GetXmlByApp(_strApNo, "C");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                strFilePath = "";
            }

            return strFilePath;
        }

        /// <summary>
        /// 得到指定申请号的说明书文件的全路径
        /// </summary>
        /// <param name="_strApNo">申请号为12位,8位内部转换</param>
        public string getDesXmlFile(string _strApNo)
        {
            string strFilePath = "";
            try
            {
                //TBD: 8位转12位
                //_strApNo = cnDataService.ApNo8To12(_strApNo);
                cnDataFromTransService.ServiceSoapClient client = new cnDataFromTransService.ServiceSoapClient();
                strFilePath = client.GetXmlByApp(_strApNo, "D");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                strFilePath = "";
            }

            return strFilePath;
        }


    }
}
