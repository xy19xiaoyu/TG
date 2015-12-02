using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Authority
    {
        /*** 静态字段 ***/

        private int _AuthorityId;
        private byte _Types;
        private string _Url;
        private string _Title;
        private string _Note;

        /*** 构造函数 ***/

        //构造Authority类
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

        /*** 属性 ***/

        //权限ID
        public int AuthorityId
        {
            get { return _AuthorityId; }
            set { _AuthorityId = value; }
        }
        //类型
        public byte Types
        {
            get { return _Types; }
            set { _Types = value; }
        }
        //地址
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
        //名称
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
        //介绍
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

        /***方法 ***/

        /// <summary>
        /// 创建或保存指定的权限信息
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
        /// 删除权限
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteAuthority(this.AuthorityId);
        }

        /*** 静态方法 ***/
        /// <summary>
        /// 创建权限
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
        /// 删除指定权限ID的权限
        /// </summary>
        /// <param name="authorityId"></param>
        /// <returns></returns>
        public static bool DeleteAuthority(int authorityId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeleteAuthority(authorityId));
        }

        /// <summary>
        /// 更新指定权限ID的权限
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
        /// 获取所有的权限列表
        /// </summary>
        /// <returns></returns>
        public static List<Authority> GetAllAuthoritys()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllAuthoritys());
        }

        /// <summary>
        /// 获取权限ID的权限
        /// </summary>
        /// <param name="authorityId"></param>
        /// <returns></returns>
        public static Authority GetAuthorityByAuthorityId(int authorityId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAuthorityByAuthorityId(authorityId));
        }

        /// <summary>
        /// 获取指定地址的权限
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
