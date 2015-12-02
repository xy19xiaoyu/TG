using System;
using System.Collections;
using System.Xml.Serialization;

namespace Cpic.Cprs2010.User
{
    [Serializable]
    public class TbUserRoleInfo
    {
        #region Field Members

        private int m_iD = 0;         
        private string m_user_logname = "";         
        private string m_roleCode = "";         

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

        [XmlElement(ElementName = "User_logname")]
        public virtual string User_logname
        {
            get
            {
                return this.m_user_logname;
            }
            set
            {
                this.m_user_logname = value;
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


        #endregion

        
    }
}