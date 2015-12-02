using System;
using System.Collections.Generic;
using System.Text;

namespace Cpic.Cprs2010.Index
{
    public class SubSearchEnter
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private int _Length;

        public int Length
        {
            get { return _Length; }
            set { _Length = value; }
        }
    }
}
