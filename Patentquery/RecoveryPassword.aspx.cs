using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Net.Configuration;
using TLC.BusinessLogicLayer;

public partial class _RecoveryPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Configuration currentConfig = WebConfigurationManager.OpenWebConfiguration("~/");
            //MailSettingsSectionGroup currentMailSettings = (MailSettingsSectionGroup)currentConfig.GetSectionGroup("system.net/mailSettings");
            //PasswordRecovery1.MailDefinition.From = currentMailSettings.Smtp.From;
        }
    }

    //protected void PasswordRecovery1_VerifyingUser(object sender, LoginCancelEventArgs e)
    //{
    //    PasswordRecovery1.UserName = Membership.GetUserNameByEmail(PasswordRecovery1.UserName); 
    //}

    //protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
    //{
    //    string strBody = e.Message.Body;
    //    Users currentUser = Users.GetUserByUserId(Convert.ToInt32(PasswordRecovery1.UserName));
    //    if (currentUser != null)
    //    {
    //        strBody = strBody.Replace("<%LastName%>", currentUser.FirstName + " " + currentUser.LastName );
    //    }
    //    strBody = strBody.Replace("<%SendDate%>", DateTime.Now.ToString("yyyy年M月d日"));
    //    e.Message.Body = strBody;
    //}
}
