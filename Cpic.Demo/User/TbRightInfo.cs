using System;
using System.Collections;
using System.Xml.Serialization;

namespace Cpic.Cprs2010.User
{
    [Serializable]
    public class TbRightInfo
    {
        #region Field Members

        private int m_iD = 0;         
        private string m_rightCode = "";         
        private string m_rightName = "";         
        private string m_pagefileName = "";         
        private bool m_checkLogin = false;         
        private bool m_checkRight = false;         

        #endregion

        #region Property Members

        [XmlElement(ElementName = "ID")]
        public virtual int ID
        {
            get
            {
                return this.m_iD;
            }
            set
            {
                this.m_iD = value;
            }
        }

        [XmlElement(ElementName = "RightCode")]
        public virtual string RightCode
        {
            get
            {
                return this.m_rightCode;
            }
            set
            {
                this.m_rightCode = value;
            }
        }

        [XmlElement(ElementName = "RightName")]
        public virtual string RightName
        {
            get
            {
                return this.m_rightName;
            }
            set
            {
                this.m_rightName = value;
            }
        }

        [XmlElement(ElementName = "PagefileName")]
        public virtual string PagefileName
        {
            get
            {
                return this.m_pagefileName;
            }
            set
            {
                this.m_pagefileName = value;
            }
        }

        [XmlElement(ElementName = "CheckLogin")]
        public virtual bool CheckLogin
        {
            get
            {
                return this.m_checkLogin;
            }
            set
            {
                this.m_checkLogin = value;
            }
        }

        [XmlElement(ElementName = "CheckRight")]
        public virtual bool CheckRight
        {
            get
            {
                return this.m_checkRight;
            }
            set
            {
                this.m_checkRight = value;
            }
        }


        #endregion

        
    }
}