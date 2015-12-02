using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cpic.Cprs2010.User
{
    public class TbPackageDetailInfo
    {
        private string _packagecode = "";
        private string _fuctioncode = "";
        private int _eachlimit = 0;
        private int _monthlimit = 0;
        private int _id = 0;


        [XmlElement(ElementName = "ID")]
        public virtual int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        [XmlElement(ElementName = "PackageCode")]
        public virtual string PackageCode
        {
            get
            {
                return this._packagecode;
            }
            set
            {
                _packagecode = value;
            }
        }
        [XmlElement(ElementName = "FuctionCode")]
        public virtual string FuctionCode
        {
            get
            {
                return _fuctioncode;
            }
            set
            {
                _fuctioncode = value;
            }
        }

        [XmlElement(ElementName = "EachLimit")]
        public virtual int EachLimit
        {
            get
            {
                return _eachlimit;
            }
            set
            {
                _eachlimit = value;
            }
        }

        [XmlElement(ElementName = "MonthLimit")]
        public virtual int MonthLimit
        {
            get
            {
                return _monthlimit;
            }
            set
            {
                _monthlimit = value;
            }
        }
        
    }
}
