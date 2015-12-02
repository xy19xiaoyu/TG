using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PatentWarnning
{
    public class TaskWarnning
    {
        /// <summary>
        /// 任务 ：检索 + 插入数据 
        /// </summary>
        /// <param name="o"></param>
        public static void task(object o)
        {
            ParamObject po = o as ParamObject;
            Searches se = po._se;
            int userid = po.UserId;
            ScanSearch scan = new ScanSearch();
            try
            {
                PatentWarnning.ServiceReference.SearchDbType YjDbType = PatentWarnning.ServiceReference.SearchDbType.Cn;
                switch (se.DBSource.ToUpper().Trim())
                {
                    case "CN":
                        //送中文引擎                       
                        break;
                    case "DOCDB":
                    case "EN": //陈晓雨修改
                        //送英文引擎
                        YjDbType = PatentWarnning.ServiceReference.SearchDbType.DocDB;                        
                        break;
                }
                scan.SendSearch(se, 998, userid, YjDbType);

            }
            catch (Exception ex)
            {
                //log.Info(System.DateTime.Now + "--" + ex.Message);
                return;
            }
            finally
            {
                if (po.mre != null)
                {
                    //po.mre.Set();  //方法执行结束前,设置事件对象的信号发出（终止状态）
                }
            }

        }
        public class ParamObject
        {
            public Searches _se;
            public ManualResetEvent mre;
            public int UserId;
            public ParamObject(ManualResetEvent mre, Searches se, int userid)
            {
                this._se = se;
                this.mre = mre;
                this.UserId = userid;
            }
        }      
    }
}
