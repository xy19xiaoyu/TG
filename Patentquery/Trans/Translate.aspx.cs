using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Trans_Translate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string inputContent =string.Empty;
        string type = string.Empty;

        if (!string.IsNullOrEmpty(Request["inputContent"]))
        {
            type = Request["inputContent"].ToString();
        }

        if (!string.IsNullOrEmpty(Request["type"]))
        {
            type = Request["type"].ToString();
        }

    }

    [WebMethod]
    public static string Translate(string inputContent, string type)
    {

        string transContent = "";

        try
        {
            Cpic.Cprs2010.Cfg.Port.Translation trans = Cpic.Cprs2010.Cfg.Port.Translation.getInstance();
            if (type == "1")//汉英
            {                
               transContent = trans.CnToEnSplit(inputContent.Trim());
                //transContent = "汉英好用了!";
            }
            else//英汉
            {               
              transContent = trans.EnToCn(System.Web.HttpUtility.UrlDecode(inputContent).Trim());
                //transContent = "英汉好用了!";
            }      
        }
        catch
        {
            transContent = "failed for CPRS";
        }     
        

        return transContent;
    }
    [WebMethod]
    public static string Translate2Google(string inputContent, string type)
    {

        string transContent = "";
        try
        {
            Cpic.Cprs2010.Cfg.Port.Translation trans = Cpic.Cprs2010.Cfg.Port.Translation.getInstance();
            if (type == "1")//汉英
            {                
                transContent =trans.Translate(inputContent,"en");
            }
            else//英汉
            {
                transContent =trans.Translate(inputContent,"zh-CN");
            }

        }
        catch
        {
            transContent = "failed for Google";
        }

        return transContent;
    }

}