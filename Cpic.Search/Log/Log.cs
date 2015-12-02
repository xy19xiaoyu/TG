using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using log4net;
[assembly: log4net.Config.DOMConfigurator()]  

namespace Cpic.Cprs2010.Log
{
   
    /// <summary>
    /// 日志
    /// </summary>
    public static class Log
    {
        public static ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}
