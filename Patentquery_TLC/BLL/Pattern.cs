using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// ����ʽ
    /// </summary>
    public class Pattern
    {
        /*** ��̬�ֶ� ***/

        private int _PatternId;
        private int _UserId;
        private byte _Source;
        private byte _Types;
        private string _Number;
        private string _Expression;
        private int _Hits;
        private DateTime _CreateDate;

        /*** ���캯�� ***/

        //����Pattern��
        public Pattern(int patternId, int userId, byte source, byte types, string number, string expression, int hits, DateTime createDate)
        {
            if (String.IsNullOrEmpty(number))
                throw (new NullReferenceException("number"));

            _PatternId = patternId;
            _UserId = userId;
            _Source = source;
            _Types = types;
            _Number = number;
            _Expression = expression;
            _Hits = hits;
            _CreateDate = createDate;
        }

        /*** ���� ***/

        //����ʽID
        public int PatternId
        {
            get { return _PatternId; }
            set { _PatternId = value; }
        }
        //�û�Id
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //��Դ
        //0 ���ܼ���
        //1 ������
        //2 ר�Ҽ���
        //3 ���ർ������
        //4 ���μ���
        //5 ���˼���
        public byte Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        //����
        //0 �й�ר��
        //1 ����ר��
        public byte Types
        {
            get { return _Types; }
            set { _Types = value; }
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
        //���ʽ
        public string Expression
        {
            get
            {
                if (String.IsNullOrEmpty(_Expression))
                    return string.Empty;
                else
                    return _Expression;
            }
            set { _Expression = value; }
        }
        //������
        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }
        //����ʱ��
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /***���� ***/

        /// <summary>
        /// �����򱣴�ָ���ļ���ʽ��Ϣ
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (_PatternId <= DefaultValues.GetPatternIdMinValue())
            {
                int TempId = DALLayer.CreateNewPattern(this);
                if (TempId > 0)
                {
                    _PatternId = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DALLayer.UpdatePattern(this));
        }
        /// <summary>
        /// ɾ������ʽ
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeletePattern(this.PatternId);
        }

        /*** ��̬���� ***/
        /// <summary>
        /// ��������ʽ
        /// </summary>
        /// <returns></returns>
        public static int InsertPattern(int userId, byte source, byte types, string number, string expression, int hits, DateTime createDate)
        {
            if (String.IsNullOrEmpty(number))
                throw (new NullReferenceException("number"));
            Pattern insertPattern = new Pattern(0, userId, source, types, number, expression, hits, createDate);
            if (insertPattern.Save())
            {
                return insertPattern.PatternId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ɾ��ָ������ʽID�ļ���ʽ
        /// </summary>
        /// <param name="patternId"></param>
        /// <returns></returns>
        public static bool DeletePattern(int patternId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeletePattern(patternId));
        }

        /// <summary>
        /// ����ָ������ʽID�ļ���ʽ
        /// </summary>
        /// <param name="patternId"></param>
        /// <returns></returns>
        public static bool UpdatePattern(int patternId, int userId, byte source, byte types, string number, string expression, int hits, DateTime createDate)
        {
            if (String.IsNullOrEmpty(number))
                throw (new NullReferenceException("number"));
            Pattern updatePattern = Pattern.GetPatternByPatternId(patternId);
            if (updatePattern != null)
            {
                updatePattern.UserId = userId;
                updatePattern.Source = source;
                updatePattern.Types = types;
                updatePattern.Number = number;
                updatePattern.Expression = expression;
                updatePattern.Hits = hits;
                updatePattern.CreateDate = createDate;

                return (updatePattern.Save());
            }
            else
                return false;
        }

        /// <summary>
        /// ��ȡ���еļ���ʽ�б�
        /// </summary>
        /// <returns></returns>
        public static List<Pattern> GetAllPatterns()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllPatterns());
        }

        /// <summary>
        /// ��ȡָ���û�����Դ�����͵ļ���ʽ�б�
        /// </summary>
        /// <param name="userId">0:�����û�</param>
        /// <param name="source">255:������Դ,  0 ���ܼ���,1 ������,2 ר�Ҽ���,3 ���ർ������,4 ���μ���,5 ���˼���</param>
        /// <param name="types">255:��������,0 �й�ר��,1 ����ר��</param>
        /// <returns></returns>
        public static List<Pattern> GetPatternsByUserIdAndSourceAndTypes(int userId, byte source, byte types)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetPatternsByUserIdAndSourceAndTypes(userId, source, types));
        }

        /// <summary>
        /// ��ȡָ���û������͡��ؼ��ʵļ���ʽ�б�
        /// </summary>
        /// <param name="userId">0:�����û�</param>
        /// <param name="types">255:��������</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<Pattern> GetPatternsByUserIdAndTypesAndKeyword(int userId, byte types, string keyword)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetPatternsByUserIdAndTypesAndKeyword(userId, types, keyword));
        }

        /// <summary>
        /// ��ȡ����ʽID�ļ���ʽ
        /// </summary>
        /// <param name="patternId"></param>
        /// <returns></returns>
        public static Pattern GetPatternByPatternId(int patternId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetPatternByPatternId(patternId));
        }
    }
}
