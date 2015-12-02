using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// 收藏
    /// </summary>
    public class Collect
    {
        /*** 静态字段 ***/

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

        /*** 构造函数 ***/

        //构造Collect类
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

        /*** 属性 ***/

        //收藏ID
        public int CollectId
        {
            get { return _CollectId; }
            set { _CollectId = value; }
        }
        //用户Id
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //分类Id
        public int AlbumId
        {
            get { return _AlbumId; }
            set { _AlbumId = value; }
        }
        //类型
        //0 中国专利
        //1 世界专利
        public byte Types
        {
            get { return _Types; }
            set { _Types = value; }
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
        //编号
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
        //法律状态
        public byte LawState
        {
            get { return _LawState; }
            set { _LawState = value; }
        }
        //收藏日期
        public DateTime CollectDate
        {
            get { return _CollectDate; }
            set { _CollectDate = value; }
        }
        //标注
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
        //标注日期
        public DateTime NoteDate
        {
            get { return _NoteDate; }
            set { _NoteDate = value; }
        }

        /***方法 ***/

        /// <summary>
        /// 创建或保存指定的收藏信息
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
        /// 删除收藏
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteCollect(this.CollectId);
        }

        /*** 静态方法 ***/
        /// <summary>
        /// 创建收藏
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
        /// 删除指定收藏ID的收藏
        /// </summary>
        /// <param name="collectId"></param>
        /// <returns></returns>
        public static bool DeleteCollect(int collectId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeleteCollect(collectId));
        }

        /// <summary>
        /// 更新指定收藏ID的收藏
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
        /// 获取所有的收藏列表
        /// </summary>
        /// <returns></returns>
        public static List<Collect> GetAllCollects()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllCollects());
        }

        /// <summary>
        /// 获取指定用户、分类、类型、是否标注的收藏列表
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
        /// 获取指定用户、搜索类型、关键词的收藏列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchType">关键词 名称 申请号 标注</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<Collect> GetCollectsByUserIdAndSearchTypeAndKeyword(int userId, byte searchType, string keyword)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCollectsByUserIdAndSearchTypeAndKeyword(userId, searchType, keyword));
        }

        /// <summary>
        /// 获取指定条数的热门收藏列表
        /// </summary>
        /// <param name="counts"></param>
        /// <returns></returns>
        public static List<Collect> GetCollectsHotByCounts(int counts)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCollectsHotByCounts(counts));
        }

        /// <summary>
        /// 获取收藏ID的收藏
        /// </summary>
        /// <param name="collectId"></param>
        /// <returns></returns>
        public static Collect GetCollectByCollectId(int collectId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetCollectByCollectId(collectId));
        }

        /// <summary>
        /// 获取指定用户、收藏夹、专利号的收藏
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
