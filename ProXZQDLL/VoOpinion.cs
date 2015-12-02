using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProXZQDLL
{
    [Serializable]
    public class VoOpinion
    {
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string tJDate;

        public string TJDate
        {
            get { return tJDate; }
            set { tJDate = value; }
        }
        private string lYTxt;

        public string LYTxt
        {
            get { return lYTxt; }
            set { lYTxt = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string uID;

        public string UID
        {
            get { return uID; }
            set { uID = value; }
        }
        private string uName;

        public string UName
        {
            get { return uName; }
            set { uName = value; }
        }
    }
}
