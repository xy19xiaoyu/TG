using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TLC.BusinessLogicLayer;
namespace TLC.DataAccessLayer
{
    public abstract class DataAccess
    {
        /*** 属性 ***/

        /// <summary>
        /// 从配置文件中返回数据库连接信息
        /// </summary>
        protected string ConnectionString
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["ConnectionString"] == null)
                    throw (new NullReferenceException("需要在Web.Config文件中定义连接字符串！"));
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                if (String.IsNullOrEmpty(connectionString))
                    throw (new NullReferenceException("Web.Config文件中定义连接字符串为空！"));
                else
                    return (connectionString);
            }
        }

        /*** 方法  ***/

        //用户
        public abstract int CreateNewUser(Users newUser);
        public abstract bool DeleteUser(int userId);
        public abstract bool UpdateUser(Users newUser);
        public abstract List<Users> GetAllUsers();
        public abstract List<Users> GetUsersByRoleNameAndCorpIdAndKeyword(string roleName, int corpId, string keyword);
        public abstract Users GetUserByUserId(int userId);
        public abstract Users GetUserByUserName(string userName);

        //企业
        public abstract int CreateNewCorp(Corp newCorp);
        public abstract bool DeleteCorp(int corpId);
        public abstract bool UpdateCorp(Corp newCorp);
        public abstract List<Corp> GetAllCorps();
        public abstract List<Corp> GetCorpsByUserId(int userId);
        public abstract Corp GetCorpByCorpId(int corpId);

        //权限
        public abstract int CreateNewAuthority(Authority newAuthority);
        public abstract bool DeleteAuthority(int authorityId);
        public abstract bool UpdateAuthority(Authority newAuthority);
        public abstract List<Authority> GetAllAuthoritys();
        public abstract Authority GetAuthorityByAuthorityId(int authorityId);
        public abstract Authority GetAuthorityByUrl(string url);

        //检索式
        public abstract int CreateNewPattern(Pattern newPattern);
        public abstract bool DeletePattern(int patternId);
        public abstract bool UpdatePattern(Pattern newPattern);
        public abstract List<Pattern> GetAllPatterns();
        public abstract List<Pattern> GetPatternsByUserIdAndSourceAndTypes(int userId, byte source, byte types);
        public abstract List<Pattern> GetPatternsByUserIdAndTypesAndKeyword(int userId, byte types, string keyword);
        public abstract Pattern GetPatternByPatternId(int patternId);

        //分类
        public abstract int CreateNewAlbum(Album newAlbum);
        public abstract bool DeleteAlbum(int albumId);
        public abstract bool UpdateAlbum(Album newAlbum);
        public abstract List<Album> GetAllAlbums();
        public abstract List<Album> GetAlbumsByUserId(int userId);
        public abstract List<Album> GetAlbumsByParentId(int parentId);
        public abstract List<Album> GetAlbumsByAlbumId(int albumId);
        public abstract Album GetAlbumByAlbumId(int albumId);

        //收藏
        public abstract int CreateNewCollect(Collect newCollect);
        public abstract bool DeleteCollect(int collectId);
        public abstract bool UpdateCollect(Collect newCollect);
        public abstract List<Collect> GetAllCollects();
        public abstract List<Collect> GetCollectsByUserIdAndAlbumIdAndTypesAndIsNote(int userId, int albumId, byte types, byte isNote);
        public abstract List<Collect> GetCollectsByUserIdAndSearchTypeAndKeyword(int userId, byte searchType, string keywords);
        public abstract List<Collect> GetCollectsHotByCounts(int counts);
        public abstract Collect GetCollectByCollectId(int collectId);
        public abstract Collect GetCollectByUserIdAndAlbumIdAndNumber(int userId, int albumId, string number);

        //日志
        public abstract int CreateNewLog(Log newLog);
        public abstract bool DeleteLog(int logId);
        public abstract bool UpdateLog(Log newLog);
        public abstract List<Log> GetAllLogs();
        public abstract List<Log> GetLogsByUserId(int userId);
        public abstract List<Log> GetLogsByUserNameAndType(string userName, byte types);
        public abstract Log GetLogByLogId(int logId);
    }
}

