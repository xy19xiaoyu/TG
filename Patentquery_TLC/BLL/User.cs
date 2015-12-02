using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer 
{
    /// <summary>
    /// �û�
    /// </summary>
    public class Users
    {
        /*** ��̬�ֶ� ***/
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
        
        /*** ���캯�� ***/

        //����Users��
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
        
        /*** ���� ***/

        //�û�ID
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //�û���
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
        //����
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
        //����
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
        //�ֻ�
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
        //�绰
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
        //��ַ
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
        //��ҵID
        public int CorpId
        {
            get { return _CorpId; }
            set { _CorpId = value; }
        }
        //��Ч��
        public DateTime ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        //Ȩ��
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
        //��ע
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
        //״̬
        public byte State
        {
            get { return _State; }
            set { _State = value; }
        }

        /***���� ***/

        /// <summary>
        /// �����򱣴�ָ�����û���Ϣ
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
        /// ɾ���û�
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteUser(this.UserId);
        }

        /*** ��̬���� ***/
        /// <summary>
        /// �����û�
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
        /// ɾ��ָ���û�ID���û�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool DeleteUser(int userId)
        {
          DataAccess DALLayer = DataAccessHelper.GetDataAccess();
          return (DALLayer.DeleteUser(userId));
        }

        /// <summary>
        /// ����ָ���û�ID���û�
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
        /// ��ȡ���е��û��б�
        /// </summary>
        /// <returns></returns>
        public static List<Users> GetAllUsers() 
        {
          DataAccess DALLayer = DataAccessHelper.GetDataAccess();
          return (DALLayer.GetAllUsers());
        }
        /// <summary>
        /// ��ȡָ����ɫ����ҵ���ؼ��ʵ��û��б�
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="corpId">-1 ����</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<Users> GetUsersByRoleNameAndCorpIdAndKeyword(string roleName, int corpId, string keyword)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetUsersByRoleNameAndCorpIdAndKeyword(roleName, corpId, keyword));
        }
        /// <summary>
        /// ��ȡָ��ID���û�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Users GetUserByUserId(int userId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetUserByUserId(userId));
        }
        /// <summary>
        /// ��ȡָ���û������û�
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
