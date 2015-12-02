using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// �ղ�
    /// </summary>
    public class Collect
    {
        /*** ��̬�ֶ� ***/

        private int _CollectId;
        private int _UserId;
        private int _AlbumId;
        private byte _Types;
        private string _Title;
        private string _Number;
        private byte _LawState;
        private DateTime _CollectDate;
        private string _Note;
        private DateTime _NoteDate;

        /*** ���캯�� ***/

        //����Collect��
        public Collect(int collectId, int userId, int albumId, byte types, string title, string number, byte lawState, DateTime collectDate, string note, DateTime noteDate)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));

            _CollectId = collectId;
            _UserId = userId;
            _AlbumId = albumId;
            _Types = types;
            _Title = title;
            _Number = number;
            _LawState = lawState;
            _CollectDate = collectDate;
            _Note = note;
            _NoteDate = noteDate;
        }

        /*** ���� ***/

        //�ղ�ID
        public int CollectId
        {
            get { return _CollectId; }
            set { _CollectId = value; }
        }
        //�û�Id
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //����Id
        public int AlbumId
        {
            get { return _AlbumId; }
            set { _AlbumId = value; }
        }
        //����
        //0 �й�ר��
        //1 ����ר��
        public byte Types
        {
            get { return _Types; }
            set { _Types = value; }
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
        //���
        public string Number
        {
            get
            {
                if (String.IsNullOrEmpty(_Number))
                    return string.Empty;
                else
                    return _Number;
            }
            set { _Number = value; }
        }
        //����״̬
        public byte LawState
        {
            get { return _LawState; }
            set { _LawState = value; }
        }
        //�ղ�����
        public DateTime CollectDate
        {
            get { return _CollectDate; }
            set { _CollectDate = value; }
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
        //��ע����
        public DateTime NoteDate
        {
            get { return _NoteDate; }
            set { _NoteDate = value; }
        }

        /***���� ***/

        /// <summary>
        /// �����򱣴�ָ�����ղ���Ϣ
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (_CollectId <= DefaultValues.GetCollectIdMinValue())
            {
                int TempId = DALLayer.CreateNewCollect(this);
                if (TempId > 0)
                {
                    _CollectId = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DALLayer.UpdateCollect(this));
        }
        /// <summary>
        /// ɾ���ղ�
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteCollect(this.CollectId);
        }

        /*** ��̬���� ***/
        /// <summary>
        /// �����ղ�
        /// </summary>
        /// <returns></returns>
        public static int InsertCollect(int userId, int albumId, byte types, string title, string number, byte lawState, DateTime collectDate, string note, DateTime noteDate)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));
            Collect insertCollect = new Collect(0, userId, albumId, types, title, number, lawState, collectDate, note, noteDate);
            if (insertCollect.Save())
            {
                return insertCollect.CollectId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ɾ��ָ���ղ�ID���ղ�
        /// </summary>
        /// <param name="collectId"></param>
        /// <returns></returns>
        public static bool DeleteCollect(int collectId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeleteCollect(collectId));
        }

        /// <summary>
        /// ����ָ���ղ�ID���ղ�
        /// </summary>
        /// <param name="collectId"></param>
        /// <returns></returns>
        public static bool UpdateCollect(int collectId, int userId, int albumId, byte types, string title, string number, byte lawState, DateTime collectDate, string note, DateTime noteDate)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));
            Collect updateCollect = Collect.GetCollectByCollectId(collectId);
            if (updateCollect != null)
            {
                updateCollect.UserId = userId;
                updateCollect.AlbumId = albumId;
                updateCollect.Types = types;
                updateCollect.Title = title;
                updateCollect.Number = number;
                updateCollect.LawState = lawState;
                updateCollect.CollectDate = collectDate;
                updateCollect.Note = note;
                updateCollect.NoteDate = noteDate;

                return (updateCollect.Save());
            }
            else
                return false;
        }

        /// <summary>
        /// ��ȡ���е��ղ��б�
        /// </summary>
        /// <returns></returns>
        public static List<Collect> GetAllCollects()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllCollects());
        }

        /// <summary>
        /// ��ȡָ���û������ࡢ���͡��Ƿ��ע���ղ��б�
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <param name="types"></param>
        /// <param name="isNote"></param>
        /// <returns></returns>
        public static List<Collect> GetCollectsByUserIdAndAlbumIdAndTypesAndIsNote(int userId, int albumId, byte types, byte isNote)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCollectsByUserIdAndAlbumIdAndTypesAndIsNote(userId, albumId, types, isNote));
        }

        /// <summary>
        /// ��ȡָ���û����������͡��ؼ��ʵ��ղ��б�
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchType">�ؼ��� ���� ����� ��ע</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<Collect> GetCollectsByUserIdAndSearchTypeAndKeyword(int userId, byte searchType, string keyword)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCollectsByUserIdAndSearchTypeAndKeyword(userId, searchType, keyword));
        }

        /// <summary>
        /// ��ȡָ�������������ղ��б�
        /// </summary>
        /// <param name="counts"></param>
        /// <returns></returns>
        public static List<Collect> GetCollectsHotByCounts(int counts)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCollectsHotByCounts(counts));
        }

        /// <summary>
        /// ��ȡ�ղ�ID���ղ�
        /// </summary>
        /// <param name="collectId"></param>
        /// <returns></returns>
        public static Collect GetCollectByCollectId(int collectId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCollectByCollectId(collectId));
        }

        /// <summary>
        /// ��ȡָ���û����ղؼС�ר���ŵ��ղ�
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Collect GetCollectByUserIdAndAlbumIdAndNumber(int userId, int albumId, string number)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCollectByUserIdAndAlbumIdAndNumber(userId, albumId, number));
        }

    }
}
