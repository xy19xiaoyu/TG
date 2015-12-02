using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpic.Cprs2010.Index
{
    public class log
    {
        System.IO.StreamWriter fs;
        public log(string logpath)
        {
            fs = new System.IO.StreamWriter(logpath,false);
            fs.AutoFlush = true;
        }
        public void error(string str)
        {
            fs.WriteLine(DateTime.Now.ToString() + "\t" + str);
        }
        ~log()
        {
            
        }
    }
}
