using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLC;

namespace Patentquery.Comm
{
    public partial class sysTreeNodes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int PId;
            string clstype;
            string SType;
            string value;

            try
            {

                PId = Convert.ToInt32(Request["Id"].ToString());
            }
            catch (Exception ex)
            {
                PId = 0;
            }

            if (string.IsNullOrEmpty(Request["clstype"]))
            {
                clstype = "IPC";
            }
            else
            {
                clstype = Request["clstype"].ToString().Trim().ToUpper();
            }

            if (string.IsNullOrEmpty(Request["SType"]))
            {
                SType = "";
            }
            else
            {
                SType = Request["SType"].ToString().Trim().ToUpper();
            }


            if (string.IsNullOrEmpty(Request["value"]))
            {
                value = "";
            }
            else
            {
                value = Request["value"].ToString().Trim().ToUpper();
            }
            if (string.IsNullOrEmpty(SType) || string.IsNullOrEmpty(value))
            {
                Response.Write(SysTreeHelper.GetNodes(PId, clstype));
            }
            else
            {
                Response.Write( new SysTreeHelper().GetNodes(clstype,SType,value));
            }

        }

    }
}