using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Patentquery.zt
{
    /// <summary>
    /// ZtFileUpLoad 的摘要说明
    /// </summary>
    public class ZtFileUpLoad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string qqfile = context.Request.QueryString["qqfile"];

                if (string.IsNullOrEmpty(qqfile))
                {
                    //"[{ y: 55.11, color : colors[0] },{ y: 21.63, color: colors[1] },{ y: 11.94,color: colors[2] },{ y: 117.15, color: colors[3] }]";
                    context.Response.Write("{\"success\": false,\"tmp_avatar\": \"\", \"description\": \"未检测到客户端提交的文件信息!\"}");
                    return;
                }

                // 检测并创建目录:当月上传的文件放到以当月命名的文件夹中，例如2011年11月的文件放到网站根目录下的 /Files/201111 里面
                string strDate = DateTime.Now.Date.ToString("yyyyMM");
                string dateFolder = ZtFileUploadHelp.StrZtHeadImgPath_Local + "\\" + strDate;
                if (!Directory.Exists(dateFolder))  // 检测是否存在磁盘目录
                {
                    Directory.CreateDirectory(dateFolder);  // 不存在的情况下，创建这个文件目录 例如 C:/wwwroot/Files/201111/
                }

                // 使用Guid命名文件，确保每次文件名不会重复
                string guidFileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(qqfile.Substring(qqfile.LastIndexOf('.'))).ToLower();

                //using (var inputStream = context.Request.InputStream)
                //{
                var inputStream = context.Request.InputStream;
                if (context.Request.Files.Count > 0)
                {
                    inputStream = context.Request.Files[0].InputStream;
                }

                using (var flieStream = new FileStream(dateFolder + "\\" + guidFileName, FileMode.Create))
                {
                    inputStream.CopyTo(flieStream);
                }
                //System.Drawing.Bitmap img = new System.Drawing.Bitmap(inputStream);
                //img.Save(dateFolder + "\\" + guidFileName);
                //}
                context.Response.Write(string.Format("{0}\"success\": true,\"tmp_avatar\": \"{2}\", \"description\": \"提交ok!\"{1}", "{", "}", strDate + "/" + guidFileName));
                return;
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"success\": false,\"tmp_avatar\": \"\", \"description\": \"" + ex.ToString() + "\"}");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class ZtFileUploadHelp
    {
        // 服务器端存储文件的文件夹（磁盘路径）
        public static string StrZtHeadImgPath_Local = @"D:\ZtHeadImg\";

        static ZtFileUploadHelp()
        {
            try
            {
                StrZtHeadImgPath_Local = HttpContext.Current.Server.MapPath("~/ZtHeadImg") + "\\";
                if (!System.IO.Directory.Exists(StrZtHeadImgPath_Local))
                {
                    System.IO.Directory.CreateDirectory(StrZtHeadImgPath_Local);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}