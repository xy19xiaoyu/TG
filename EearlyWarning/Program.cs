using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PatentWarnning;
using Cpic.Cprs2010.Search;
namespace EearlyWarning
{
    class Program
    {
        private static int poolFlag = 0;//标记
        private const int amountThread = 10;//线程总量
        private const int maxThread = 3;//可执行线程最大数量
        private static Mutex muxConsole = new Mutex();

        private static string strYJUserID = (string.IsNullOrEmpty(System.Configuration.ConfigurationSettings.AppSettings["YJ_USERID"])) ?
                                            "9000000" : System.Configuration.ConfigurationSettings.AppSettings["YJ_USERID"].ToString().Trim();

        static void Main(string[] args)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("fileLog");
            Console.WriteLine(System.DateTime.Now.ToString() + "----开始更新");
            //log.Info(System.DateTime.Now.ToString() + "----开始更新");
            ScanSearch scan = new ScanSearch();
            List<Searches> lst = new List<Searches>();


            Console.WriteLine(System.DateTime.Now.ToString() + "----读取达到更新周期的检索式");
            //log.Info(System.DateTime.Now.ToString() + "----读取达到更新周期的检索式");
            //提取检索式
            lst = scan.ReadSearch();
            Console.WriteLine(System.DateTime.Now.ToString() + "----检索式：[" + lst.Count + "]条");
            //log.Info(System.DateTime.Now.ToString() + "----检索式：[" + lst.Count + "]条");
            for (int i = 0; i < lst.Count; i++)
            {
                //ManualResetEvent[] MREs = new ManualResetEvent[amountThread];

                //for (int k = 0; k < amountThread && i + k < lst.Count; k++)
                //{
                //    MREs[k] = new ManualResetEvent(false);//初始化事件对象为无信号状态(非终止)
                //    Searches se = lst[i + k] as Searches;

                //    PatentWarnning.TaskWarnning.ParamObject po = new PatentWarnning.TaskWarnning.ParamObject(MREs[k], se,k+10);
                //    Console.WriteLine(se.SearchName);
                //    //Task(se);
                //    Thread.Sleep(500);
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(TaskWarnning.task), po);
                //}


                //ManualResetEvent[] MREs = new ManualResetEvent[amountThread];

                //for (int k = 0; k < 1; k++)
                //{
                //MREs[k] = new ManualResetEvent(false);//初始化事件对象为无信号状态(非终止)
                //Searches se = lst[i] as Searches;

                //PatentWarnning.TaskWarnning.ParamObject po = new PatentWarnning.TaskWarnning.ParamObject(MREs[k], se, k + 10);

                //TaskWarnning.task(po);                  
                //}

                Searches se = lst[i] as Searches;
                Console.WriteLine(string.Format("{0}/{1}:{2}", i + 1, lst.Count, se.SearchName));
                PatentWarnning.TaskWarnning.ParamObject po = new PatentWarnning.TaskWarnning.ParamObject(null, se, int.Parse(strYJUserID));
                TaskWarnning.task(po);
            }
            Console.WriteLine("OK!");
        }

    }
}
