using System;
using System.Collections.Generic;
using TLC.DataAccessLayer;
namespace TLC.BusinessLogicLayer
{
    /// <summary>
    /// 检索式
    /// </summary>
    public class Pattern
    {
        /*** 静态字段 ***/

        private int _PatternId;
        private int _UserId;
        private byte _Source;
        private byte _Types;
        private string _Number;
        private string _Expression;
        private int _Hits;
        private DateTime _CreateDate;

        /*** 构造函数 ***/

        //构造Pattern类
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

        /*** 属性 ***/

        //检索式ID
        public int PatternId
        {
            get { return _PatternId; }
            set { _PatternId = value; }
        }
        //用户Id
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //来源
        //0 智能检索
        //1 表格检索
        //2 专家检索
        //3 分类导航检索
        //4 二次检索
        //5 过滤检索
        public byte Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        //类型
        //0 中国专利
        //1 世界专利
        public byte Types
        {
            get { return _Types; }
            set { _Types = value; }
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
        //表达式
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
        //命中数
        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }
        //检索时间
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /***方法 ***/

        /// <summary>
        /// 创建或保存指定的检索式信息
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
        /// 删除检索式
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return DALLayer.DeletePattern(this.PatternId);
        }

        /*** 静态方法 ***/
        /// <summary>
        /// 创建检索式
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
        /// 删除指定检索式ID的检索式
        /// </summary>
        /// <param name="patternId"></param>
        /// <returns></returns>
        public static bool DeletePattern(int patternId)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeletePattern(patternId));
        }

        /// <summary>
        /// 更新指定检索式ID的检索式
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
        /// 获取所有的检索式列表
        /// </summary>
        /// <returns></returns>
        public static List<Pattern> GetAllPatterns()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllPatterns());
        }

        /// <summary>
        /// 获取指定用户、来源、类型的检索式列表
        /// </summary>
        /// <param name="userId">0:所有用户</param>
        /// <param name="source">255:所有来源,  0 智能检索,1 表格检索,2 专家检索,3 分类导航检索,4 二次检索,5 过滤检索</param>
        /// <param name="types">255:所有类型,0 中国专利,1 世界专利</param>
        /// <returns></returns>
        public static List<Pattern> GetPatternsByUserIdAndSourceAndTypes(int userId, byte source, byte types)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetPatternsByUserIdAndSourceAndTypes(userId, source, types));
        }

        /// <summary>
        /// 获取指定用户、类型、关键词的检索式列表
        /// </summary>
        /// <param name="userId">0:所有用户</param>
        /// <param name="types">255:所有类型</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<Pattern> GetPatternsByUserIdAndTypesAndKeyword(int userId, byte types, string keyword)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetPatternsByUserIdAndTypesAndKeyword(userId, types, keyword));
        }

        /// <summary>
        /// 获取检索式ID的检索式
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
