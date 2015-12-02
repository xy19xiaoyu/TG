using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProXZQDLL
{
    public class ClsOpinion
    {
        public static List<VoOpinion> getOpinion(string strSDate, string strEndate)
        {
            List<VoOpinion> lstRs = new List<VoOpinion>();

            try
            {
                DateTime dtStart = new DateTime();
                DateTime dtEnd = new DateTime();
                try
                {
                    dtStart = Convert.ToDateTime(strSDate);
                }
                catch (Exception ex)
                {
                }

                try
                {
                    dtEnd = Convert.ToDateTime(strEndate);
                    dtEnd = dtEnd.AddDays(1);
                }
                catch (Exception ex)
                {
                }

                DataClasses1DataContext db = new DataClasses1DataContext();
                var result = from item in db.TbOpinion
                             select item;

                if (!string.IsNullOrEmpty(strSDate))
                {
                    result = result.Where(a => a.TJDate >= dtStart);
                }
                if (!string.IsNullOrEmpty(strEndate))
                {
                    result = result.Where(a => a.TJDate < dtEnd);
                }

                result = result.OrderByDescending(a => a.ID);

                foreach (var item in result)
                {
                    VoOpinion vo = new VoOpinion();
                    vo.ID = item.ID;
                    vo.LYTxt = string.IsNullOrEmpty(item.LYTxt) ? "无" : item.LYTxt;
                    vo.Title = item.Title;
                    vo.UID = item.UID.ToString();
                    vo.UName = item.UName;
                    vo.TJDate = item.TJDate.Value.ToString("yyyy-MM-dd");

                    lstRs.Add(vo);
                }
            }
            catch (Exception ex)
            {
            }
            return lstRs;
        }

        public static List<TbHelpFiles> getSysHelpFile()
        {
            List<TbHelpFiles> lstRs = new List<TbHelpFiles>();
            try
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                var result = from item in db.TbHelpFiles
                             select item;
                lstRs = result.ToList();
            }
            catch (Exception ex)
            {
            }
            return lstRs;
        }

        public static bool AddHelpFile(string strTitle, string strFileName)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            TbHelpFiles tb = new TbHelpFiles();
            tb.fileName = strFileName;
            tb.helpTitle = strTitle;
            tb.UploadDate = DateTime.Now;

            try
            {
                db.TbHelpFiles.InsertOnSubmit(tb);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool DelHelpFile(int nId)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var tb = db.TbHelpFiles.SingleOrDefault(o => o.ID == nId);
            if (tb == null)
            {
                return false;
            }

            try
            {
                db.TbHelpFiles.DeleteOnSubmit(tb);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
