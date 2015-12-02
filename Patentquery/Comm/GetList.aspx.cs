using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Reflection;
using SearchInterface;
using Cpic.Cprs2010.Search.ResultData;
using System.Web.Services;


public partial class GetList : System.Web.UI.Page
{
    //private int UserId;
    //private int NodeId;
    //private int PageSize;
    //private int PageCount;
    //private int PageIndex;
    //private int ItemCount;
    //private string Type;
    //private string SourceType;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

    }


    [WebMethod]
    public static string GetPageList1(string Type, string NodeId, string SourceType, int ItemCount, int pageindex, int rows, string Sort, string isupdata)
    {
        ClsSearch cls = new ClsSearch();
        List<int> hitlist = new List<int>();
        List<xmlDataInfo> ie1 = null;

        if (SourceType.ToUpper().Substring(0, 2) == "YJ")
        {
            hitlist = ProYJDLL.YJDB.getYJItemByWID(Convert.ToInt32(NodeId), Convert.ToInt16(SourceType.Substring(2).ToString()), pageindex, rows);
            ItemCount = getItemCount(SourceType, Convert.ToInt32(NodeId), Type, ItemCount);
            ie1 = cls.GetResult(hitlist, Type.ToUpper());
            switch (Sort)
            {
                case "PD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "PD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "AD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                case "AD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                default:
                    ie1 = ie1.OrderByDescending(x => x.StrPubDate.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
            }
        }
        else if (SourceType.ToUpper() == "DB")
        {
            DateTime time = DateTime.Now;
            List<ztresult> res = ztHelper.GetResultList(NodeId, Type, rows, pageindex, isupdata, out ItemCount);
            TimeSpan sp = DateTime.Now - time;
            Console.WriteLine(sp);
            time = DateTime.Now;
            foreach (var x in res)
            {
                hitlist.Add(x.pid);
            }
            ie1 = cls.GetResult(hitlist, Type.ToUpper());
            sp = DateTime.Now - time;
            Console.WriteLine(sp);
            foreach (xmlDataInfo rowData in ie1)
            {
                ztresult x = (from y in res
                              where y.pid.ToString() == rowData.StrSerialNo
                              select y).First();

                rowData.Iscore = x.star.ToString();
                rowData.Form = x.strForm;

            }
            switch (Sort)
            {
                case "PD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "PD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "AD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                case "AD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                case "STAR":
                    ie1 = (from e in ie1.AsEnumerable()
                           orderby e.Iscore descending
                           select e).ToList<xmlDataInfo>();
                    break;
                default:
                    ie1 = ie1.OrderByDescending(x => x.StrPubDate.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
            }



        }
        else
        {
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            ie1 = search.getSearchData(XmPatentComm.strWebSearchGroupName, NodeId.ToString().PadLeft(3, '0'),
                Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]), Type.ToUpper(), rows, pageindex, Sort);
            //foreach (xmlDataInfo rowData in ie1)
            //{
            //    rowData.StrPubDate = FormatDate(rowData.StrPubDate);
            //}
            switch (Sort)
            {
                case "PD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "PD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "AD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                case "AD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                default:
                    ie1 = ie1.OrderByDescending(x => x.StrPubDate.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
            }

        }
        return JsonHelper.ListToJson<xmlDataInfo>(ie1, "rows", ItemCount.ToString());


    }


    [WebMethod]
    public static string GetPageList(string Type, int NodeId, string SourceType, int ItemCount, int pageindex, int rows, string Sort)
    {
        ClsSearch cls = new ClsSearch();
        List<int> hitlist = new List<int>();
        List<xmlDataInfo> ie1 = null;

        if (SourceType.ToUpper().Substring(0, 2) == "YJ")
        {
            hitlist = ProYJDLL.YJDB.getYJItemByWID(Convert.ToInt32(NodeId), Convert.ToInt16(SourceType.Substring(2).ToString()), pageindex, rows);
            ItemCount = getItemCount(SourceType, NodeId, Type, ItemCount);
            ie1 = cls.GetResult(hitlist, Type.ToUpper());
            switch (Sort)
            {
                case "PD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "PD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "AD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                case "AD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                default:
                    ie1 = ie1.OrderByDescending(x => x.StrPubDate.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
            }
        }
        else
        {
            SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
            ie1 = search.getSearchData(XmPatentComm.strWebSearchGroupName, NodeId.ToString().PadLeft(3, '0'),
                Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]), Type.ToUpper(), rows, pageindex, Sort);
            //foreach (xmlDataInfo rowData in ie1)
            //{
            //    rowData.StrPubDate = FormatDate(rowData.StrPubDate);
            //}
            switch (Sort)
            {
                case "PD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "PD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrPdOrGd.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
                case "AD|DESC":
                    ie1 = ie1.OrderByDescending(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                case "AD|AESC":
                    ie1 = ie1.OrderBy(x => x.StrApDate).ToList<xmlDataInfo>();
                    break;
                default:
                    ie1 = ie1.OrderByDescending(x => x.StrPubDate.Replace("公开", "").Replace("公告", "")).ToList<xmlDataInfo>();
                    break;
            }

        }
        return JsonHelper.ListToJson<xmlDataInfo>(ie1, "rows", ItemCount.ToString());

    }

    private static int getItemCount(string SourceType, int NodeId, string Type, int ItemCount)
    {
        if (SourceType.ToUpper() == "DB")
        {
            return ztHelper.GetItemCountByNodeId(NodeId, Type);
        }
        else
        {
            return ItemCount;
        }
    }
    private static int getItemCount(string SourceType, int NodeId, string Type, int ItemCount, string isupdata)
    {
        if (SourceType.ToUpper() == "DB")
        {
            return ztHelper.GetItemCountByNodeId(NodeId, Type, isupdata);
        }
        else
        {
            return ItemCount;
        }
    }
    private static string GetStars(int pid, int value)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(string.Format("<input class=\"auto-submit-star required\" type=\"radio\" name=\"star{0}\" value=\"1\" {1}/>", pid, (1 == value ? "checked=\"checked\"" : "")));

        for (int i = 2; i <= 5; i++)
        {
            sb.Append(string.Format("<input class=\"auto-submit-star\" type=\"radio\" name=\"star{0}\" value=\"{1}\" {2} />", pid, i, (i == value ? "checked=\"checked\"" : "")));
        }

        return sb.ToString();
    }

    private static string FormatDate(string date)
    {
        if (date == null) return string.Empty;
        int ix = date.IndexOf("]");
        string left;
        string right;
        string returstr = date;
        if (ix > 0)
        {
            left = date.Substring(0, ix + 1);
            right = date.Substring(ix + 1);
            DateTime time;
            if (DateTime.TryParse(right, out time))
            {
                returstr = left + time.ToString("yyyy年MM月dd日");
            }
        }

        return returstr;
    }
}
