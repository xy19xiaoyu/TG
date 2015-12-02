using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Patentquery.My
{
    public partial class frmDesignImgs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divNoImg.Visible = false;
            if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
            {
                SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                string strLiTmp = " <li><a href='{0}' target='_blank'><img src='{0}' height='80' alt='外观图片' onerror='imgOnError(this)' /></a></li>";

                string strImageUrls = search.getInfoByPatentID(Request.QueryString["Id"], "CN", "4");
                string[] arrayImageUrl = strImageUrls.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrayImageUrl.Length > 0)
                {
                    //ImagePicture.ImageUrl = arrayImageUrl[0];
                    string strTmpUrl = "";
                    foreach (string strUrlItem in arrayImageUrl)
                    {
                        ////http://search.patentstar.com.cn/CPRS2010/CNDegImg/2929/2013300887967/000003.JPG
                        //strTmpUrl = strUrlItem.Substring(strUrlItem.ToUpper().IndexOf("CNDEGIMG/") + 9);
                        //strTmpUrl = Cpic.Cprs2010.Cfg.UrlParameterCode_DE.encrypt(strTmpUrl);
                        //strTmpUrl = string.Format("http://202.106.92.181/cprs2010/comm/getimg.aspx?idx={0}&Ty=CWGF", strTmpUrl);

                        //litDesigImgsLi.Text += "<img src=\"" + arrayImageUrl[i] + "\" height=\"80\" onclick=\"showImg('" + arrayImageUrl[i] + "')\" alt=\"\" style=\"border:solid 1px #BDBDBD; margin:20px; cursor:pointer;\"/> ";
                        strTmpUrl = strUrlItem;
                        litDesigImgsLi.Text += string.Format(strLiTmp, strTmpUrl);
                    }
                }
                else
                {
                    divZoomBox.Visible = false;
                    divNoImg.Visible = true;
                }
            }
            else
            {
                divZoomBox.Visible = false;
                divNoImg.Visible = true;
            }
        }
    }
}