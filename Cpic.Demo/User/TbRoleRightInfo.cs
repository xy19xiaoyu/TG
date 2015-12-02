using System;
using System.Collections;
using System.Xml.Serialization;

namespace Cpic.Cprs2010.User
{
    [Serializable]
    public class TbRoleRightInfo
    {
        #region Field Members

        private int m_iD = 0;         
        private string m_roleCode = "";         
        private string m_rightCode = "";         

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

        [XmlElement(ElementName = "RoleCode")]
        public virtual string RoleCode
        {
            get
            {
                return this.m_roleCode;
            }
            set
            {
                this.m_roleCode = value;
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


        #endregion

       
    }
}