using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer 
{
    /// <summary>
    /// 用户
    /// </summary>
    public class Users
    {
        /*** 静态字段 ***/
        private int _UserId;
        private string _UserName;
        private string _Password;
        private string _TrueName;
        private string _Mobile;
        private string _Tel;
        private string _Adds;
        private int _CorpId;
        private DateTime _ExpiryDate;
        private string _Authoritys;
        private string _Note;
        private byte _State;
        
        /*** 构造函数 ***/

        //构造Users类
        public Users(int userId, string userName, string password, string trueName, string mobile, string tel, string adds, int corpId, DateTime expiryDate, string authoritys, string note, byte state)
        {
            _UserId = userId;
            _UserName = userName;
            _Password = password;
            _TrueName = trueName;
            _Mobile = mobile;
            _Tel = tel;
            _Adds = adds;
            _CorpId = corpId;
            _ExpiryDate = expiryDate;
            _Authoritys = authoritys;
            _Note = note;
            _State = state;
        }
        
        /*** 属性 ***/

        //用户ID
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //用户名
        public string UserName
        {
            get
            {
                if (String.IsNullOrEmpty(_UserName))
                    return string.Empty;
                else
                    return _UserName;
            }
            set { _UserName = value; }
        }
        //密码
        public string Password
        {
            get
            {
                if (String.IsNullOrEmpty(_Password))
                    return string.Empty;
                else
                    return _Password;
            }
            set { _Password = value; }
        }
        //姓名
        public string TrueName
        {
            get
            {
                if (String.IsNullOrEmpty(_TrueName))
                    return string.Empty;
                else
                    return _TrueName;
            }
            set { _TrueName = value; }
        }
        //手机
        public string Mobile
        {
            get
            {
                if (String.IsNullOrEmpty(_Mobile))
                    return string.Empty;
                else
                    return _Mobile;
            }
            set { _Mobile = value; }
        }
        //电话
        public string Tel
        {
            get
            {
                if (String.IsNullOrEmpty(_Tel))
                    return string.Empty;
                else
                    return _Tel;
            }
            set { _Tel = value; }
        }
        //地址
        public string Adds
        {
            get
            {
                if (String.IsNullOrEmpty(_Adds))
                    return string.Empty;
                else
                    return _Adds;
            }
            set { _Adds = value; }
        }
        //企业ID
        public int CorpId
        {
            get { return _CorpId; }
            set { _CorpId = value; }
        }
        //有效期
        public DateTime ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        //权限
        public string Authoritys
        {
            get
            {
                if (String.IsNullOrEmpty(_Authoritys))
                    return string.Empty;
                else
                    return _Authoritys;
            }
            set { _Authoritys = value; }
        }
        //备注
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
        //状态
        public byte State
        {
            get { return _State; }
            set { _State = value; }
        }

        /***方法 ***/

        /// <summary>
        /// 创建或保存指定的用户信息
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (_UserId <= DefaultValues.GetUserIdMinValue())
            {
                int TempId = DALLayer.CreateNewUser(this);
                if (TempId > 0)
                {
                    _UserId = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DALLayer.UpdateUser(this));
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteUser(this.UserId);
        }

        /*** 静态方法 ***/
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public static int InsertUser(string userName, string password, string trueName, string mobile, string tel, string adds, int corpId, DateTime expiryDate, string authoritys, string note, byte state)
        {
            Users insertUser = new Users(0, userName, password, trueName, mobile, tel, adds, corpId, expiryDate, authoritys, note, state);
            if (insertUser.Save())
            {
                return insertUser.UserId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除指定用户ID的用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool DeleteUser(int userId)
        {
          DataAccess DALLayer = DataAccessHelper.GetDataAccess();
          return (DALLayer.DeleteUser(userId));
        }

        /// <summary>
        /// 更新指定用户ID的用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool UpdateUser(int userId, string userName, string password, string trueName, string mobile, string tel, string adds, int corpId, DateTime expiryDate, string authoritys, string note, byte state)
        {
            Users updateUser = Users.GetUserByUserId(userId);
            if (updateUser != null)
            {
                updateUser.UserName = userName;
                updateUser.Password = password;
                updateUser.TrueName = trueName;
                updateUser.Mobile = mobile;
                updateUser.Tel = tel;
                updateUser.Adds = adds;
                updateUser.CorpId = corpId;
                updateUser.ExpiryDate = expiryDate;
                updateUser.Authoritys = authoritys;
                updateUser.Note = note;
                updateUser.State = state;

                return (updateUser.Save());
            }
            else
                return false;
        }
        /// <summary>
        /// 获取所有的用户列表
        /// </summary>
        /// <returns></returns>
        public static List<Users> GetAllUsers() 
        {
          DataAccess DALLayer = DataAccessHelper.GetDataAccess();
          return (DALLayer.GetAllUsers());
        }
        /// <summary>
        /// 获取指定角色、企业、关键词的用户列表
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="corpId">-1 不限</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<Users> GetUsersByRoleNameAndCorpIdAndKeyword(string roleName, int corpId, string keyword)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetUsersByRoleNameAndCorpIdAndKeyword(roleName, corpId, keyword));
        }
        /// <summary>
        /// 获取指定ID的用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Users GetUserByUserId(int userId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetUserByUserId(userId));
        }
        /// <summary>
        /// 获取指定用户名的用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Users GetUserByUserName(string userName)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetUserByUserName(userName));
        }
    }
}
