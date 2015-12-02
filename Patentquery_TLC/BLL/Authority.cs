using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// Ȩ��
    /// </summary>
    public class Authority
    {
        /*** ��̬�ֶ� ***/

        private int _AuthorityId;
        private byte _Types;
        private string _Url;
        private string _Title;
        private string _Note;

        /*** ���캯�� ***/

        //����Authority��
        public Authority(int authorityId, byte types, string url, string title, string note)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));

            _AuthorityId = authorityId;
            _Types = types;
            _Url = url;
            _Title = title;
            _Note = note;
        }

        /*** ���� ***/

        //Ȩ��ID
        public int AuthorityId
        {
            get { return _AuthorityId; }
            set { _AuthorityId = value; }
        }
        //����
        public byte Types
        {
            get { return _Types; }
            set { _Types = value; }
        }
        //��ַ
        public string Url
        {
            get
            {
                if (String.IsNullOrEmpty(_Url))
                    return string.Empty;
                else
                    return _Url;
            }
            set { _Url = value; }
        }
        //����
        public string Title
        {
            get
            {
                if (String.IsNullOrEmpty(_Title))
                    return string.Empty;
                else
                    return _Title;
            }
            set { _Title = value; }
        }
        //����
        public string Note
        {
            get
            {
                if (String.IsNullOrEmpty(_Note))
                    return string.Empty;
                else
                    return _Note;
            }
            set { _Note = value; }
        }

        /***���� ***/

        /// <summary>
        /// �����򱣴�ָ����Ȩ����Ϣ
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (_AuthorityId <= DefaultValues.GetAuthorityIdMinValue())
            {
                int TempId = DALLayer.CreateNewAuthority(this);
                if (TempId > 0)
                {
                    _AuthorityId = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DALLayer.UpdateAuthority(this));
        }
        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteAuthority(this.AuthorityId);
        }

        /*** ��̬���� ***/
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <returns></returns>
        public static int InsertAuthority(byte types, string url, string title, string note)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));
            Authority insertAuthority = new Authority(0, types, url, title, note);
            if (insertAuthority.Save())
            {
                return insertAuthority.AuthorityId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ɾ��ָ��Ȩ��ID��Ȩ��
        /// </summary>
        /// <param name="authorityId"></param>
        /// <returns></returns>
        public static bool DeleteAuthority(int authorityId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeleteAuthority(authorityId));
        }

        /// <summary>
        /// ����ָ��Ȩ��ID��Ȩ��
        /// </summary>
        /// <param name="AuthorityId"></param>
        /// <returns></returns>
        public static bool UpdateAuthority(int authorityId, byte types, string url, string title, string note)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));
            Authority updateAuthority = Authority.GetAuthorityByAuthorityId(authorityId);
            if (updateAuthority != null)
            {
                updateAuthority.Types = types;
                updateAuthority.Url = url;
                updateAuthority.Title = title;
                updateAuthority.Note = note;

                return (updateAuthority.Save());
            }
            else
                return false;
        }

        /// <summary>
        /// ��ȡ���е�Ȩ���б�
        /// </summary>
        /// <returns></returns>
        public static List<Authority> GetAllAuthoritys()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllAuthoritys());
        }

        /// <summary>
        /// ��ȡȨ��ID��Ȩ��
        /// </summary>
        /// <param name="authorityId"></param>
        /// <returns></returns>
        public static Authority GetAuthorityByAuthorityId(int authorityId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAuthorityByAuthorityId(authorityId));
        }

        /// <summary>
        /// ��ȡָ����ַ��Ȩ��
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Authority GetAuthorityByUrl(string url)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAuthorityByUrl(url));
        }
    }
}
