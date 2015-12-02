using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpic.Cprs2010.Search
{
    public class ResultInfoWebService
    {
        private ResultInfo _resultInfo;
        private string _resultSearchFilePath;

        public ResultInfoWebService(){}

        /// <summary>
        /// 检索结果ResultInfo
        /// </summary>
        public ResultInfo ResultInfo
        {
            get { return _resultInfo; }
            set { _resultInfo = value; }
        }

        /// <summary>
        /// 检索结果ResultSearchFilePath
        /// </summary>
        public string ResultSearchFilePath
        {
            get { return _resultSearchFilePath; }
            set { _resultSearchFilePath = value; }
        }
    }
}
