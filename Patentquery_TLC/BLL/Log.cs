using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer 
{
    /// <summary>
    /// ��־
    /// </summary>
    public class Log
    {
        /*** ��̬�ֶ� ***/

        private int _LogId;
        private int _UserId;
        private byte _Types;
        private DateTime _LogDate;
        private string _Note;

        /*** ���캯�� ***/

        //����Log��
        public Log(int logId, int userId, byte types, DateTime logDate, string note)
        {
            _LogId = logId;
            _UserId = userId;
            _Types = types;
            _LogDate = logDate;
            _Note = note;
        }
        
        /*** ���� ***/

        //��־ID
        public int LogId
        {
            get { return _LogId; }
            set { _LogId = value; }
        }
        //�û�ID
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //����
        public byte Types
        {
            get { return _Types; }
            set { _Types = value; }
        }
        //��־ʱ��
        public DateTime LogDate
        {
            get { return _LogDate; }
            set { _LogDate = value; }
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

        /***���� ***/

        /// <summary>
        /// �����򱣴�ָ������־��Ϣ
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
        /// ɾ����־
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeleteLog(this.LogId);
        }

        /*** ��̬���� ***/
        /// <summary>
        /// ������־
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
        /// ɾ��ָ����־ID����־
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public static bool DeleteLog(int logId)
        {
          DataAccess DALLayer = DataAccessHelper.GetDataAccess();
          return (DALLayer.DeleteLog(logId));
        }

        /// <summary>
        /// ����ָ����־ID����־
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
        /// ��ȡ���е���־�б�
        /// </summary>
        /// <returns></returns>
        public static List<Log> GetAllLogs() {
          DataAccess DALLayer = DataAccessHelper.GetDataAccess();
          return (DALLayer.GetAllLogs());
        }

        /// <summary>
        /// ��ȡָ���û�����־�б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Log> GetLogsByUserId(int userId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetLogsByUserId(userId));
        }

        /// <summary>
        /// ��ȡָ���û��������͵���־�б�
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="types">255:ȫ��</param>
        /// <returns></returns>
        public static List<Log> GetLogsByUserNameAndType(string userName, byte types)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetLogsByUserNameAndType(userName, types));
        }

        /// <summary>
        /// ��ȡ��־ID����־
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
