using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// 分类
    /// </summary>
    public class Album
    {
        /*** 静态字段 ***/

        private int _AlbumId;
        private int _UserId;
        private int _ParentId;
        private string _Title;
        private string _Note;
        private int _Collects;
        private int _Orders;

        /*** 构造函数 ***/

        //构造Album类
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

        /*** 属性 ***/

        //分类ID
        public int AlbumId
        {
            get { return _AlbumId; }
            set { _AlbumId = value; }
        }
        //用户Id
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //父分类Id
        public int ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        //标题
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
        //收藏数
        public int Collects
        {
            get { return _Collects; }
            set { _Collects = value; }
        }
        //序号
        public int Orders
        {
            get { return _Orders; }
            set { _Orders = value; }
        }

        /***方法 ***/

        /// <summary>
        /// 创建或保存指定的分类信息
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
        /// 删除分类
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteAlbum(this.AlbumId);
        }

        /*** 静态方法 ***/
        /// <summary>
        /// 创建分类
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
        /// 删除指定分类ID的分类
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public static bool DeleteAlbum(int albumId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeleteAlbum(albumId));
        }

        /// <summary>
        /// 更新指定分类ID的分类
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
        /// 获取所有的分类列表
        /// </summary>
        /// <returns></returns>
        public static List<Album> GetAllAlbums()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllAlbums());
        }

        /// <summary>
        /// 获取指定用户的分类列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Album> GetAlbumsByUserId(int userId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAlbumsByUserId(userId));
        }

        /// <summary>
        /// 获取指定父级分类的分类列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<Album> GetAlbumsByParentId(int parentId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAlbumsByParentId(parentId));
        }

        /// <summary>
        /// 获取指定分类的父级分类列表
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public static List<Album> GetAlbumsByAlbumId(int albumId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAlbumsByAlbumId(albumId));
        }

        /// <summary>
        /// 获取分类ID的分类
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
