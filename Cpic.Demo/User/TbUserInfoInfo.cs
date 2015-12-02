using System;
using System.Collections;
using System.Xml.Serialization;

namespace Cpic.Cprs2010.User
{
    [Serializable]
    public class TbUserInfoInfo
    {

        #region Field Members

        private int m_iD = 0;         
        private string m_user_logname = "";         
        private string m_pSD = "";         
        private string m_use_Name = "";         
        private string m_user_Number = "";         
        private string m_user_Email = "";
        private int m_user_Checkemail = 0;
        private string m_user_Tel = "";
        private string m_user_Province = "";
        private string m_user_City = "";
        private string m_user_County = "";
        private string m_user_QQ = "";
        private string m_user_Company = "";
        private string m_user_Sex = "";
        private string m_user_Post = "";
        private string m_user_Bussiness = "";
        private int m_user_Money = 30;

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
        /// <summary>
        /// 
        /// </summary>
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

        [XmlElement(ElementName = "PSD")]
        public virtual string PSD
        {
            get
            {
                return this.m_pSD;
            }
            set
            {
                this.m_pSD = value;
            }
        }

        [XmlElement(ElementName = "Use_Name")]
        public virtual string Use_Name
        {
            get
            {
                return this.m_use_Name;
            }
            set
            {
                this.m_use_Name = value;
            }
        }

        [XmlElement(ElementName = "User_Number")]
        public virtual string User_Number
        {
            get
            {
                return this.m_user_Number;
            }
            set
            {
                this.m_user_Number = value;
            }
        }

        [XmlElement(ElementName = "User_Email")]
        public virtual string User_Email
        {
            get
            {
                return this.m_user_Email;
            }
            set
            {
                this.m_user_Email = value;
            }
        }
        [XmlElement(ElementName = "CheckEmail")]
        public virtual int CheckEmail
        {
            get
            {
                return this.m_user_Checkemail;
            }
            set
            {
                this.m_user_Checkemail = value;
            }
        }
        [XmlElement(ElementName = "User_Tel")]
        public virtual string User_Tel
        {
            get
            {
                return this.m_user_Tel;
            }
            set
            {
                this.m_user_Tel = value;
            }
        }
        
        [XmlElement(ElementName = "Province")]
        public virtual string Province
        {
            get
            {
                return this.m_user_Province;
            }
            set
            {
                this.m_user_Province = value;
            }
        }

        [XmlElement(ElementName = "City")]
        public virtual string City
        {
            get
            {
                return this.m_user_City;
            }
            set
            {
                this.m_user_City = value;
            }
        }

        [XmlElement(ElementName = "County")]
        public virtual string County
        {
            get
            {
                return this.m_user_County;
            }
            set
            {
                this.m_user_County = value;
            }
        }

        [XmlElement(ElementName = "QQ")]
        public virtual string QQ
        {
            get
            {
                return this.m_user_QQ;
            }
            set
            {
                this.m_user_QQ = value;
            }
        }
        [XmlElement(ElementName = "Company")]
        public virtual string Company
        {
            get
            {
                return this.m_user_Company;
            }
            set
            {
                this.m_user_Company = value;
            }
        }
        [XmlElement(ElementName = "Sex")]
        public virtual string Sex
        {
            get
            {
                return this.m_user_Sex;
            }
            set
            {
                this.m_user_Sex = value;
            }
        }
        [XmlElement(ElementName = "Post")]
        public virtual string Post
        {
            get
            {
                return this.m_user_Post;
            }
            set
            {
                this.m_user_Post = value;
            }
        }
        [XmlElement(ElementName = "Bussiness")]
        public virtual string Bussiness
        {
            get
            {
                return this.m_user_Bussiness;
            }
            set
            {
                this.m_user_Bussiness = value;
            }
        }
        [XmlElement(ElementName = "Money")]
        public virtual int Money
        {
            get
            {
                return this.m_user_Money;
            }
            set
            {
                this.m_user_Money = value;
            }
        }
       
        #endregion

    }
}