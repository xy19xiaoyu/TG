using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// ��ҵ
    /// </summary>
    public class Corp
    {
        /*** ��̬�ֶ� ***/

        private int _CorpId;
        private string _Admin;
        private string _Title;
        private string _Note;

        /*** ���캯�� ***/

        //����Corp��
        public Corp(int corpId, string admin, string title, string note)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));

            _CorpId = corpId;
            _Admin = admin;
            _Title = title;
            _Note = note;
        }

        /*** ���� ***/

        //��ҵID
        public int CorpId
        {
            get { return _CorpId; }
            set { _CorpId = value; }
        }
        //����Ա
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
        /// �����򱣴�ָ������ҵ��Ϣ
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
        /// ɾ����ҵ
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteCorp(this.CorpId);
        }

        /*** ��̬���� ***/
        /// <summary>
        /// ������ҵ
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
        /// ɾ��ָ����ҵID����ҵ
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public static bool DeleteCorp(int corpId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeleteCorp(corpId));
        }

        /// <summary>
        /// ����ָ����ҵID����ҵ
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
        /// ��ȡ���е���ҵ�б�
        /// </summary>
        /// <returns></returns>
        public static List<Corp> GetAllCorps()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllCorps());
        }

        /// <summary>
        /// ��ȡָ���û��������ҵ�б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Corp> GetCorpsByUserId(int userId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCorpsByUserId(userId));
        }

        /// <summary>
        /// ��ȡ��ҵID����ҵ
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
