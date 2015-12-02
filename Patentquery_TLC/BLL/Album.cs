using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// ����
    /// </summary>
    public class Album
    {
        /*** ��̬�ֶ� ***/

        private int _AlbumId;
        private int _UserId;
        private int _ParentId;
        private string _Title;
        private string _Note;
        private int _Collects;
        private int _Orders;

        /*** ���캯�� ***/

        //����Album��
        public Album(int albumId, int userId, int parentId, string title, string note, int collects, int orders)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));

            _AlbumId = albumId;
            _UserId = userId;
            _ParentId = parentId;
            _Title = title;
            _Note = note;
            _Collects = collects;
            _Orders = orders;
        }

        /*** ���� ***/

        //����ID
        public int AlbumId
        {
            get { return _AlbumId; }
            set { _AlbumId = value; }
        }
        //�û�Id
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //������Id
        public int ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
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
        //�ղ���
        public int Collects
        {
            get { return _Collects; }
            set { _Collects = value; }
        }
        //���
        public int Orders
        {
            get { return _Orders; }
            set { _Orders = value; }
        }

        /***���� ***/

        /// <summary>
        /// �����򱣴�ָ���ķ�����Ϣ
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (_AlbumId <= DefaultValues.GetAlbumIdMinValue())
            {
                int TempId = DALLayer.CreateNewAlbum(this);
                if (TempId > 0)
                {
                    _AlbumId = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DALLayer.UpdateAlbum(this));
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteAlbum(this.AlbumId);
        }

        /*** ��̬���� ***/
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public static int InsertAlbum(int userId, int parentId, string title, string note, int collects, int orders)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));
            Album insertAlbum = new Album(0, userId, parentId, title, note, collects, orders);
            if (insertAlbum.Save())
            {
                return insertAlbum.AlbumId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ɾ��ָ������ID�ķ���
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public static bool DeleteAlbum(int albumId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeleteAlbum(albumId));
        }

        /// <summary>
        /// ����ָ������ID�ķ���
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public static bool UpdateAlbum(int albumId, int userId, int parentId, string title, string note, int collects, int orders)
        {
            if (String.IsNullOrEmpty(title))
                throw (new NullReferenceException("title"));
            Album updateAlbum = Album.GetAlbumByAlbumId(albumId);
            if (updateAlbum != null)
            {
                updateAlbum.UserId = userId;
                updateAlbum.ParentId = parentId;
                updateAlbum.Title = title;
                updateAlbum.Note = note;
                updateAlbum.Collects = collects;
                updateAlbum.Orders = orders;

                return (updateAlbum.Save());
            }
            else
                return false;
        }

        /// <summary>
        /// ��ȡ���еķ����б�
        /// </summary>
        /// <returns></returns>
        public static List<Album> GetAllAlbums()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllAlbums());
        }

        /// <summary>
        /// ��ȡָ���û��ķ����б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Album> GetAlbumsByUserId(int userId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAlbumsByUserId(userId));
        }

        /// <summary>
        /// ��ȡָ����������ķ����б�
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<Album> GetAlbumsByParentId(int parentId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAlbumsByParentId(parentId));
        }

        /// <summary>
        /// ��ȡָ������ĸ��������б�
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public static List<Album> GetAlbumsByAlbumId(int albumId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAlbumsByAlbumId(albumId));
        }

        /// <summary>
        /// ��ȡ����ID�ķ���
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public static Album GetAlbumByAlbumId(int albumId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAlbumByAlbumId(albumId));
        }

    }
}
