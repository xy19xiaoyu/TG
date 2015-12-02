using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PatentWarnning
{
    public class YJini
    {
        private static string strYJUserID = (string.IsNullOrEmpty(System.Configuration.ConfigurationSettings.AppSettings["YJ_USERID"])) ?
                                            "9000001" : System.Configuration.ConfigurationSettings.AppSettings["YJ_USERID"].ToString().Trim();

        public static void SearchYJini(int C_ID, int flag)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("fileLog");
            Console.WriteLine(System.DateTime.Now.ToString() + "----开始更新");
            //log.Info(System.DateTime.Now.ToString() + "----开始更新");
            ScanSearch scan = new ScanSearch();
            List<Searches> lst = new List<Searches>();


            Console.WriteLine(System.DateTime.Now.ToString() + "----读取达到更新周期的检索式");
            //log.Info(System.DateTime.Now.ToString() + "----读取达到更新周期的检索式");
            //提取检索式
            lst = scan.ReadSearch(C_ID, flag);
            Console.WriteLine(System.DateTime.Now.ToString() + "----检索式：[" + lst.Count + "]条");
            //log.Info(System.DateTime.Now.ToString() + "----检索式：[" + lst.Count + "]条");


            for (int i = 0; i < lst.Count; i++)
            {
                Searches se = lst[i] as Searches;
                PatentWarnning.TaskWarnning.ParamObject po = new PatentWarnning.TaskWarnning.ParamObject(null, se, int.Parse(strYJUserID));
                TaskWarnning.task(po);
            }
        }

    }
}
