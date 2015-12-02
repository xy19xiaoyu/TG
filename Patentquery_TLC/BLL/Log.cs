using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer 
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Log
    {
        /*** 静态字段 ***/

        private int _LogId;
        private int _UserId;
        private byte _Types;
        private DateTime _LogDate;
        private string _Note;

        /*** 构造函数 ***/

        //构造Log类
        public Log(int logId, int userId, byte types, DateTime logDate, string note)
        {
            _LogId = logId;
            _UserId = userId;
            _Types = types;
            _LogDate = logDate;
            _Note = note;
        }
        
        /*** 属性 ***/

        //日志ID
        public int LogId
        {
            get { return _LogId; }
            set { _LogId = value; }
        }
        //用户ID
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //类型
        public byte Types
        {
            get { return _Types; }
            set { _Types = value; }
        }
        //日志时间
        public DateTime LogDate
        {
            get { return _LogDate; }
            set { _LogDate = value; }
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

        /***方法 ***/

        /// <summary>
        /// 创建或保存指定的日志信息
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (_LogId <= DefaultValues.GetLogIdMinValue())
            {
                int TempId = DALLayer.CreateNewLog(this);
                if (TempId > 0)
                {
                    _LogId = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DALLayer.UpdateLog(this));
        }
        /// <summary>
        /// 删除日志
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteLog(this.LogId);
        }

        /*** 静态方法 ***/
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <returns></returns>
        public static int InsertLog(int userId, byte types, DateTime logDate, string note)
        {
            Log insertLog = new Log(0, userId, types, logDate, note);
            if (insertLog.Save())
            {
                return insertLog.LogId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除指定日志ID的日志
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public static bool DeleteLog(int logId)
        {
          DataAccess DALLayer = DataAccessHelper.GetDataAccess();
          return (DALLayer.DeleteLog(logId));
        }

        /// <summary>
        /// 更新指定日志ID的日志
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public static bool UpdateLog(int logId, int userId, byte types, DateTime logDate, string note)
        {
            Log updateLog = Log.GetLogByLogId(logId);
            if (updateLog != null)
            {
                updateLog.UserId = userId;
                updateLog.Types = types;
                updateLog.LogDate = logDate;
                updateLog.Note = note;

                return (updateLog.Save());
            }
            else
                return false;
        }

        /// <summary>
        /// 获取所有的日志列表
        /// </summary>
        /// <returns></returns>
        public static List<Log> GetAllLogs() {
          DataAccess DALLayer = DataAccessHelper.GetDataAccess();
          return (DALLayer.GetAllLogs());
        }

        /// <summary>
        /// 获取指定用户的日志列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Log> GetLogsByUserId(int userId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetLogsByUserId(userId));
        }

        /// <summary>
        /// 获取指定用户名、类型的日志列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="types">255:全部</param>
        /// <returns></returns>
        public static List<Log> GetLogsByUserNameAndType(string userName, byte types)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetLogsByUserNameAndType(userName, types));
        }

        /// <summary>
        /// 获取日志ID的日志
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public static Log GetLogByLogId(int logId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetLogByLogId(logId));
        }
        
    }
}
