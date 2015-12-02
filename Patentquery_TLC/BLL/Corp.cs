using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// 企业
    /// </summary>
    public class Corp
    {
        /*** 静态字段 ***/

        private int _CorpId;
        private string _Admin;
        private string _Title;
        private string _Note;

        /*** 构造函数 ***/

        //构造Corp类
        public Corp(int corpId, string admin, string title, string note)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));

            _CorpId = corpId;
            _Admin = admin;
            _Title = title;
            _Note = note;
        }

        /*** 属性 ***/

        //企业ID
        public int CorpId
        {
            get { return _CorpId; }
            set { _CorpId = value; }
        }
        //管理员
        public string Admin
        {
            get
            {
                if (String.IsNullOrEmpty(_Admin))
                    return string.Empty;
                else
                    return _Admin;
            }
            set { _Admin = value; }
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
        /// 创建或保存指定的企业信息
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (_CorpId <= DefaultValues.GetCorpIdMinValue())
            {
                int TempId = DALLayer.CreateNewCorp(this);
                if (TempId > 0)
                {
                    _CorpId = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DALLayer.UpdateCorp(this));
        }
        /// <summary>
        /// 删除企业
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteCorp(this.CorpId);
        }

        /*** 静态方法 ***/
        /// <summary>
        /// 创建企业
        /// </summary>
        /// <returns></returns>
        public static int InsertCorp(string admin, string title, string note)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));
            Corp insertCorp = new Corp(0, admin, title, note);
            if (insertCorp.Save())
            {
                return insertCorp.CorpId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除指定企业ID的企业
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public static bool DeleteCorp(int corpId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeleteCorp(corpId));
        }

        /// <summary>
        /// 更新指定企业ID的企业
        /// </summary>
        /// <param name="CorpId"></param>
        /// <returns></returns>
        public static bool UpdateCorp(int corpId, string admin, string title, string note)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));
            Corp updateCorp = Corp.GetCorpByCorpId(corpId);
            if (updateCorp != null)
            {
                updateCorp.Admin = admin;
                updateCorp.Title = title;
                updateCorp.Note = note;

                return (updateCorp.Save());
            }
            else
                return false;
        }

        /// <summary>
        /// 获取所有的企业列表
        /// </summary>
        /// <returns></returns>
        public static List<Corp> GetAllCorps()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllCorps());
        }

        /// <summary>
        /// 获取指定用户管理的企业列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Corp> GetCorpsByUserId(int userId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCorpsByUserId(userId));
        }

        /// <summary>
        /// 获取企业ID的企业
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public static Corp GetCorpByCorpId(int corpId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCorpByCorpId(corpId));
        }
    }
}
