using System;
using System.Collections;
using System.Xml.Serialization;

namespace Cpic.Cprs2010.User
{
    [Serializable]
    public class TbRoleInfo
    {
        #region Field Members

        private int m_iD = 0;         
        private string m_roleCode = "";         
        private string m_roleName = "";         

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

        [XmlElement(ElementName = "RoleName")]
        public virtual string RoleName
        {
            get
            {
                return this.m_roleName;
            }
            set
            {
                this.m_roleName = value;
            }
        }


        #endregion

      }
}