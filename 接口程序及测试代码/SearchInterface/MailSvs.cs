using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace SearchInterface
{
    class MailSvs
    {
        private static string strSendUserName = "";

        public static string StrSendUserName
        {
            get { return strSendUserName; }
            set { strSendUserName = value; }
        }
        private static string strSendUserMail = "";

        public static string StrSendUserMail
        {
            get { return strSendUserMail; }
            set { strSendUserMail = value; }
        }

        private static string strSendMailPwd = "";

        public static string StrSendMailPwd
        {
            get { return strSendMailPwd; }
            set { strSendMailPwd = value; }
        }
        private static string strSednMailSmtp = "";

        public static string StrSednMailSmtp
        {
            get { return strSednMailSmtp; }
            set { strSednMailSmtp = value; }
        }

        private static string strSendUserDis = "";

        public static string StrSendUserDis
        {
            get { return MailSvs.strSendUserDis; }
            set { MailSvs.strSendUserDis = value; }
        }

        public static string strTxtCfgFile = "";
        static MailSvs()
        {
            strTxtCfgFile = System.Configuration.ConfigurationManager.AppSettings["MailCftTxt"];
            InitValues(strTxtCfgFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTxtCfg">mail|userName|PWD|ShowName|smtpSvs</param>
        public static void InitValues(string strTxtCfg)
        {
            try
            {
                string strTxt = File.ReadAllText(strTxtCfg);
                string[] strTmp = strTxt.Split('|');

                StrSendUserMail = strTmp[0].Trim();
                StrSendUserName = strTmp[1].Trim();
                StrSendMailPwd = strTmp[2].Trim();
                StrSendUserDis = strTmp[3].Trim();
                StrSednMailSmtp = strTmp[4].Trim();
            }
            catch (Exception ex)
            {
                StrSendUserMail = "xuxitao@cnpat.com.cn";
                StrSendUserName = "xuxitao";
                StrSendMailPwd = "111111";
                StrSendUserDis = "厦漳泉科技基础资源服务平台";
                StrSednMailSmtp = "mail.cnpat.com.cn";
            }
        }

        public static void SaveCft()
        {
            string strTmp = string.Format("{0}|{1}|{2}|{3}|{4}", StrSendUserMail, StrSendUserName,
               StrSendMailPwd, StrSendUserDis, StrSednMailSmtp);

            File.WriteAllText(strTxtCfgFile, strTmp, Encoding.UTF8);
        }


        public bool SendMail(string ShoujianRenEmail, string strTi, string body)
        {
            //System.IO.StreamReader sr = new System.IO.StreamReader(@"D:\aaaaaaaaaaa\huiyuan.htm",Encoding.Default );
            //body = sr.ReadToEnd();
            //sr.Close();

            try
            {
                //编码暂硬性规定为GB2312 
                Encoding encoding = Encoding.GetEncoding("utf-8");
                MailMessage Message = new MailMessage(
                new MailAddress(StrSendUserMail, StrSendUserDis, encoding),//第一个是发信人的地址，第二个参数是发信人
                new MailAddress(ShoujianRenEmail));//收信人邮箱
                Message.SubjectEncoding = encoding;
                Message.Subject = strTi;//标题
                Message.BodyEncoding = encoding;
                Message.Body = body; //主体

                SmtpClient smtpClient = new SmtpClient(StrSednMailSmtp);//信箱服务器
                smtpClient.Credentials = new NetworkCredential(StrSendUserName, StrSendMailPwd);//信箱的用户名和密码
                smtpClient.Timeout = 999999;
                smtpClient.Send(Message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
