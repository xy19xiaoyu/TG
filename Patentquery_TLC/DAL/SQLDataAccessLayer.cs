using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using TLC.BusinessLogicLayer;

namespace TLC.DataAccessLayer
{
    public class SQLDataAccess : DataAccess
    {
        /*** 泛型委托 ***/

        private delegate void TGenerateListFromReader<T>(SqlDataReader returnData, ref List<T> tempList);

        /*****************************  基本类实现 *****************************/


        /*** 用户 ***/

        //创建新用户存储过程
        private const string SP_Users_Create = "TLC_InsertNewUser";
        //删除用户存储过程
        private const string SP_Users_Delete = "TLC_DeleteUser";
        //更新用户存储过程
        private const string SP_Users_Update = "TLC_UpdateUser";
        //获取所有用户列表的存储过程
        private const string SP_Users_GetAllUsers = "TLC_SelectAllUsers";
        //根据角色、企业、关键字获取用户列表的存储过程
        private const string SP_Users_GetUsersByRoleNameAndCorpIdAndKeyword = "TLC_SelectUsersByRoleNameAndCorpIdAndKeyword";
        //根据用户ID获取用户的存储过程
        private const string SP_Users_GetUserByUserId = "TLC_SelectUserByUserId";
        //根据用户名获取用户的存储过程
        private const string SP_Users_GetUserByUserName = "TLC_SelectUserByUserName";
        /// <summary>
        /// 创建新的用户
        /// </summary>
        /// <param name="newUser">新的用户对象</param>
        /// <returns></returns>
        public override int CreateNewUser(Users newUser)
        {
            if (newUser == null)
                throw (new ArgumentNullException("newUser"));
            SqlCommand sqlCmd = new SqlCommand();
            //调用Sql帮助方法的AddParamToSQLCmd为提定的SqlCommand对象添加参数。
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 20, ParameterDirection.Input, newUser.UserName);
            AddParamToSQLCmd(sqlCmd, "@Password", SqlDbType.NVarChar, 20, ParameterDirection.Input, newUser.Password);
            AddParamToSQLCmd(sqlCmd, "@TrueName", SqlDbType.NVarChar, 20, ParameterDirection.Input, newUser.TrueName);
            AddParamToSQLCmd(sqlCmd, "@Mobile", SqlDbType.NVarChar, 11, ParameterDirection.Input, newUser.Mobile);
            AddParamToSQLCmd(sqlCmd, "@Tel", SqlDbType.NVarChar, 20, ParameterDirection.Input, newUser.Tel);
            AddParamToSQLCmd(sqlCmd, "@Adds", SqlDbType.NVarChar, 255, ParameterDirection.Input, newUser.Adds);
            AddParamToSQLCmd(sqlCmd, "@CorpId", SqlDbType.Int, 0, ParameterDirection.Input, newUser.CorpId);
            AddParamToSQLCmd(sqlCmd, "@ExpiryDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newUser.ExpiryDate);
            AddParamToSQLCmd(sqlCmd, "@Authoritys", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newUser.Authoritys);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newUser.Note);
            AddParamToSQLCmd(sqlCmd, "@State", SqlDbType.TinyInt, 0, ParameterDirection.Input, newUser.State);
            //调用Sql帮助方法的SetCommandType方法设置SqlCommand的类型和所要执行的SQL语句或存储过程。
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Users_Create);
            //调用Sql帮助方法的ExecuteScalarCmd执行Sqlcommand中的ExecuteScalar方法。
            ExecuteScalarCmd(sqlCmd);
            //获取返回的值信息。
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }
        /// <summary>
        /// 删除指定UserId的用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override bool DeleteUser(int userId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Users_Delete);
            ExecuteScalarCmd(sqlCmd);
            //获取存储过程返回值
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 更新一个用户
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public override bool UpdateUser(Users newUser)
        {
            if (newUser == null)
                throw (new ArgumentNullException("newUser"));
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, newUser.UserId);
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 20, ParameterDirection.Input, newUser.UserName);
            AddParamToSQLCmd(sqlCmd, "@Password", SqlDbType.NVarChar, 20, ParameterDirection.Input, newUser.Password);
            AddParamToSQLCmd(sqlCmd, "@TrueName", SqlDbType.NVarChar, 20, ParameterDirection.Input, newUser.TrueName);
            AddParamToSQLCmd(sqlCmd, "@Mobile", SqlDbType.NVarChar, 11, ParameterDirection.Input, newUser.Mobile);
            AddParamToSQLCmd(sqlCmd, "@Tel", SqlDbType.NVarChar, 20, ParameterDirection.Input, newUser.Tel);
            AddParamToSQLCmd(sqlCmd, "@Adds", SqlDbType.NVarChar, 255, ParameterDirection.Input, newUser.Adds);
            AddParamToSQLCmd(sqlCmd, "@CorpId", SqlDbType.Int, 0, ParameterDirection.Input, newUser.CorpId);
            AddParamToSQLCmd(sqlCmd, "@ExpiryDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newUser.ExpiryDate);
            AddParamToSQLCmd(sqlCmd, "@Authoritys", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newUser.Authoritys);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newUser.Note);
            AddParamToSQLCmd(sqlCmd, "@State", SqlDbType.TinyInt, 0, ParameterDirection.Input, newUser.State);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Users_Update);
            ExecuteScalarCmd(sqlCmd);
            //判断是否更新成功
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 获取所有的用户
        /// </summary>
        /// <returns></returns>
        public override List<Users> GetAllUsers()
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Users_GetAllUsers);
            //新建一个User类型的泛型列表
            List<Users> UserList = new List<Users>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateUserListFromReader<User>泛型方法作为委托类型参数，UserList用于返回值
            TExecuteReaderCmd<Users>(sqlCmd, TGenerateUserListFromReader<Users>, ref UserList);
            //返回泛型列表类
            return UserList;
        }
        /// <summary>
        /// 获取指定角色、企业、关键词的用户列表
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="corpId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public override List<Users> GetUsersByRoleNameAndCorpIdAndKeyword(string roleName, int corpId, string keyword)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@RoleName", SqlDbType.NVarChar, 255, ParameterDirection.Input, roleName);
            AddParamToSQLCmd(sqlCmd, "@CorpId", SqlDbType.Int, 0, ParameterDirection.Input, corpId);
            AddParamToSQLCmd(sqlCmd, "@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Users_GetUsersByRoleNameAndCorpIdAndKeyword);
            //新建一个User类型的泛型列表
            List<Users> UserList = new List<Users>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateUserListFromReader<User>泛型方法作为委托类型参数，UserList用于返回值
            TExecuteReaderCmd<Users>(sqlCmd, TGenerateUserListFromReader<Users>, ref UserList);
            //返回泛型列表类
            return UserList;
        }
        /// <summary>
        /// 根据指定的Id获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override Users GetUserByUserId(int userId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Users_GetUserByUserId);
            //新建一个User类型的泛型列表
            List<Users> UserList = new List<Users>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateUserListFromReader<Users>泛型方法作为委托类型参数，UserList用于返回值
            TExecuteReaderCmd<Users>(sqlCmd, TGenerateUserListFromReader<Users>, ref UserList);
            //如果列表项总数大于0.
            if (UserList.Count > 0)
                //返回第一个列表项。
                return UserList[0];
            else
                return null;
        }
        /// <summary>
        /// 根据指定的用户名获取用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public override Users GetUserByUserName(string userName)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 20, ParameterDirection.Input, userName);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Users_GetUserByUserName);
            //新建一个User类型的泛型列表
            List<Users> UserList = new List<Users>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateUserListFromReader<Users>泛型方法作为委托类型参数，UserList用于返回值
            TExecuteReaderCmd<Users>(sqlCmd, TGenerateUserListFromReader<Users>, ref UserList);
            //如果列表项总数大于0.
            if (UserList.Count > 0)
                //返回第一个列表项。
                return UserList[0];
            else
                return null;
        }

        /*** 权限扩展信息 ***/

        //创建新权限信息存储过程
        private const string SP_Authority_Create = "TLC_InsertNewAuthority";
        //删除权限信息存储过程
        private const string SP_Authority_Delete = "TLC_DeleteAuthority";
        //更新权限信息存储过程
        private const string SP_Authority_Update = "TLC_UpdateAuthority";
        //获取所有权限信息列表的存储过程
        private const string SP_Authority_GetAllAuthoritys = "TLC_SelectAllAuthoritys";
        //根据指定的权限ID获取权限信息的存储过程
        private const string SP_Authority_GetAuthorityByAuthorityId = "TLC_SelectAuthorityByAuthorityId";
        //根据指定的地址获取权限信息的存储过程
        private const string SP_Authority_GetAuthorityByUrl = "TLC_SelectAuthorityByUrl";
        /// <summary>
        /// 创建新的权限信息
        /// </summary>
        /// <param name="newAuthority">新的权限信息对象</param>
        /// <returns></returns>
        public override int CreateNewAuthority(Authority newAuthority)
        {
            if (newAuthority == null)
                throw (new ArgumentNullException("newAuthority"));
            SqlCommand sqlCmd = new SqlCommand();
            //调用Sql帮助方法的AddParamToSQLCmd为提定的SqlCommand对象添加参数。
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, newAuthority.Types);
            AddParamToSQLCmd(sqlCmd, "@Url", SqlDbType.NVarChar, 255, ParameterDirection.Input, newAuthority.Url);
            AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, newAuthority.Title);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newAuthority.Note);
            //调用Sql帮助方法的SetCommandType方法设置SqlCommand的类型和所要执行的SQL语句或存储过程。
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Authority_Create);
            //调用Sql帮助方法的ExecuteScalarCmd执行Sqlcommand中的ExecuteScalar方法。
            ExecuteScalarCmd(sqlCmd);
            //获取返回的值信息。
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }
        /// <summary>
        /// 删除指定AuthorityName的权限信息
        /// </summary>
        /// <param name="authorityId"></param>
        /// <returns></returns>
        public override bool DeleteAuthority(int authorityId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@AuthorityId", SqlDbType.Int, 0, ParameterDirection.Input, authorityId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Authority_Delete);
            ExecuteScalarCmd(sqlCmd);
            //获取存储过程返回值
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 更新一个权限信息
        /// </summary>
        /// <param name="newAuthority"></param>
        /// <returns></returns>
        public override bool UpdateAuthority(Authority newAuthority)
        {
            if (newAuthority == null)
                throw (new ArgumentNullException("newAuthority"));
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@AuthorityId", SqlDbType.Int, 0, ParameterDirection.Input, newAuthority.AuthorityId);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, newAuthority.Types);
            AddParamToSQLCmd(sqlCmd, "@Url", SqlDbType.NVarChar, 255, ParameterDirection.Input, newAuthority.Url);
            AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, newAuthority.Title);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newAuthority.Note);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Authority_Update);
            ExecuteScalarCmd(sqlCmd);
            //判断是否更新成功
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 获取所有的权限信息
        /// </summary>
        /// <returns></returns>
        public override List<Authority> GetAllAuthoritys()
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Authority_GetAllAuthoritys);
            //新建一个Authority类型的泛型列表
            List<Authority> AuthorityList = new List<Authority>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateAuthorityListFromReader<Authority>泛型方法作为委托类型参数，AuthorityList用于返回值
            TExecuteReaderCmd<Authority>(sqlCmd, TGenerateAuthorityListFromReader<Authority>, ref AuthorityList);
            //返回泛型列表类
            return AuthorityList;
        }
        /// <summary>
        /// 根据指定的ID获取权限信息
        /// </summary>
        /// <param name="authorityId"></param>
        /// <returns></returns>
        public override Authority GetAuthorityByAuthorityId(int authorityId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@AuthorityId", SqlDbType.Int, 0, ParameterDirection.Input, authorityId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Authority_GetAuthorityByAuthorityId);
            //新建一个Authority类型的泛型列表
            List<Authority> AuthorityList = new List<Authority>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateAuthorityListFromReader<Authority>泛型方法作为委托类型参数，AuthorityList用于返回值
            TExecuteReaderCmd<Authority>(sqlCmd, TGenerateAuthorityListFromReader<Authority>, ref AuthorityList);
            //如果列表项总数大于0.
            if (AuthorityList.Count > 0)
                //返回第一个列表项。
                return AuthorityList[0];
            else
                return null;
        }
        /// <summary>
        /// 根据指定的地址获取权限信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public override Authority GetAuthorityByUrl(string url)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@Url", SqlDbType.NVarChar, 255, ParameterDirection.Input, url);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Authority_GetAuthorityByUrl);
            //新建一个Authority类型的泛型列表
            List<Authority> AuthorityList = new List<Authority>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateAuthorityListFromReader<Authority>泛型方法作为委托类型参数，AuthorityList用于返回值
            TExecuteReaderCmd<Authority>(sqlCmd, TGenerateAuthorityListFromReader<Authority>, ref AuthorityList);
            //如果列表项总数大于0.
            if (AuthorityList.Count > 0)
                //返回第一个列表项。
                return AuthorityList[0];
            else
                return null;
        }

        /*** 企业扩展信息 ***/

        //创建新企业信息存储过程
        private const string SP_Corp_Create = "TLC_InsertNewCorp";
        //删除企业信息存储过程
        private const string SP_Corp_Delete = "TLC_DeleteCorp";
        //更新企业信息存储过程
        private const string SP_Corp_Update = "TLC_UpdateCorp";
        //获取所有企业信息列表的存储过程
        private const string SP_Corp_GetAllCorps = "TLC_SelectAllCorps";
        //根据用户获取企业信息列表的存储过程
        private const string SP_Corp_GetCorpsByUserId = "TLC_SelectCorpsByUserId";
        //根据指定的企业ID获取企业信息的存储过程
        private const string SP_Corp_GetCorpByCorpId = "TLC_SelectCorpByCorpId";
        /// <summary>
        /// 创建新的企业信息
        /// </summary>
        /// <param name="newCorp">新的企业信息对象</param>
        /// <returns></returns>
        public override int CreateNewCorp(Corp newCorp)
        {
            if (newCorp == null)
                throw (new ArgumentNullException("newCorp"));
            SqlCommand sqlCmd = new SqlCommand();
            //调用Sql帮助方法的AddParamToSQLCmd为提定的SqlCommand对象添加参数。
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@Admin", SqlDbType.NVarChar, 255, ParameterDirection.Input, newCorp.Admin);
            AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, newCorp.Title);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newCorp.Note);
            //调用Sql帮助方法的SetCommandType方法设置SqlCommand的类型和所要执行的SQL语句或存储过程。
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Corp_Create);
            //调用Sql帮助方法的ExecuteScalarCmd执行Sqlcommand中的ExecuteScalar方法。
            ExecuteScalarCmd(sqlCmd);
            //获取返回的值信息。
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }
        /// <summary>
        /// 删除指定CorpName的企业信息
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public override bool DeleteCorp(int corpId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CorpId", SqlDbType.Int, 0, ParameterDirection.Input, corpId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Corp_Delete);
            ExecuteScalarCmd(sqlCmd);
            //获取存储过程返回值
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 更新一个企业信息
        /// </summary>
        /// <param name="newCorp"></param>
        /// <returns></returns>
        public override bool UpdateCorp(Corp newCorp)
        {
            if (newCorp == null)
                throw (new ArgumentNullException("newCorp"));
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CorpId", SqlDbType.Int, 0, ParameterDirection.Input, newCorp.CorpId);
            AddParamToSQLCmd(sqlCmd, "@Admin", SqlDbType.NVarChar, 255, ParameterDirection.Input, newCorp.Admin);
            AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, newCorp.Title);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newCorp.Note);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Corp_Update);
            ExecuteScalarCmd(sqlCmd);
            //判断是否更新成功
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 获取所有的企业信息
        /// </summary>
        /// <returns></returns>
        public override List<Corp> GetAllCorps()
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Corp_GetAllCorps);
            //新建一个Corp类型的泛型列表
            List<Corp> CorpList = new List<Corp>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateCorpListFromReader<Corp>泛型方法作为委托类型参数，CorpList用于返回值
            TExecuteReaderCmd<Corp>(sqlCmd, TGenerateCorpListFromReader<Corp>, ref CorpList);
            //返回泛型列表类
            return CorpList;
        }
        /// <summary>
        /// 获取指定的用户管理的所有企业信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override List<Corp> GetCorpsByUserId(int userId)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Corp_GetCorpsByUserId);
            //新建一个Corp类型的泛型列表
            List<Corp> CorpList = new List<Corp>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateCorpListFromReader<Corp>泛型方法作为委托类型参数，CorpList用于返回值
            TExecuteReaderCmd<Corp>(sqlCmd, TGenerateCorpListFromReader<Corp>, ref CorpList);
            //返回泛型列表类
            return CorpList;
        }
        /// <summary>
        /// 根据指定的ID获取企业信息
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public override Corp GetCorpByCorpId(int corpId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@CorpId", SqlDbType.Int, 0, ParameterDirection.Input, corpId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Corp_GetCorpByCorpId);
            //新建一个Corp类型的泛型列表
            List<Corp> CorpList = new List<Corp>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateCorpListFromReader<Corp>泛型方法作为委托类型参数，CorpList用于返回值
            TExecuteReaderCmd<Corp>(sqlCmd, TGenerateCorpListFromReader<Corp>, ref CorpList);
            //如果列表项总数大于0.
            if (CorpList.Count > 0)
                //返回第一个列表项。
                return CorpList[0];
            else
                return null;
        }

        /*** 检索式 ***/

        //创建检索式存储过程
        private const string SP_Pattern_Create = "TLC_InsertNewPattern";
        //删除检索式存储过程
        private const string SP_Pattern_Delete = "TLC_DeletePattern";
        //更新检索式存储过程
        private const string SP_Pattern_Update = "TLC_UpdatePattern";
        //获取所有检索式列表的存储过程
        private const string SP_Pattern_GetAllPatterns = "TLC_SelectAllPatterns";
        //根据用户、来源、类型获取检索式列表的存储过程
        private const string SP_Pattern_GetPatternsByUserIdAndSourceAndTypes = "TLC_SelectPatternsByUserIdAndSourceAndTypes";
        //根据用户、类型、关键词获取检索式列表的存储过程
        private const string SP_Pattern_GetPatternsByUserIdAndTypesAndKeyword = "TLC_SelectPatternsByUserIdAndTypesAndKeyword";
        //根据检索式ID获取检索式的存储过程
        private const string SP_Pattern_GetPatternByPatternId = "TLC_SelectPatternByPatternId";
        /// <summary>
        /// 创建新的检索式
        /// </summary>
        /// <param name="newPattern">新的检索式对象</param>
        /// <returns></returns>
        public override int CreateNewPattern(Pattern newPattern)
        {
            if (newPattern == null)
                throw (new ArgumentNullException("newPattern"));
            SqlCommand sqlCmd = new SqlCommand();
            //调用Sql帮助方法的AddParamToSQLCmd为提定的SqlCommand对象添加参数。
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, newPattern.UserId);
            AddParamToSQLCmd(sqlCmd, "@Source", SqlDbType.TinyInt, 0, ParameterDirection.Input, newPattern.Source);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, newPattern.Types);
            AddParamToSQLCmd(sqlCmd, "@Number", SqlDbType.NVarChar, 255, ParameterDirection.Input, newPattern.Number);
            AddParamToSQLCmd(sqlCmd, "@Expression", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newPattern.Expression);
            AddParamToSQLCmd(sqlCmd, "@Hits", SqlDbType.Int, 0, ParameterDirection.Input, newPattern.Hits);
            AddParamToSQLCmd(sqlCmd, "@CreateDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newPattern.CreateDate);
            //调用Sql帮助方法的SetCommandType方法设置SqlCommand的类型和所要执行的SQL语句或存储过程。
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Pattern_Create);
            //调用Sql帮助方法的ExecuteScalarCmd执行Sqlcommand中的ExecuteScalar方法。
            ExecuteScalarCmd(sqlCmd);
            //获取返回的值信息。
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }
        /// <summary>
        /// 删除指定PatternName的检索式
        /// </summary>
        /// <param name="patternId"></param>
        /// <returns></returns>
        public override bool DeletePattern(int patternId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@PatternId", SqlDbType.Int, 0, ParameterDirection.Input, patternId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Pattern_Delete);
            ExecuteScalarCmd(sqlCmd);
            //获取存储过程返回值
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 更新一个检索式
        /// </summary>
        /// <param name="newPattern"></param>
        /// <returns></returns>
        public override bool UpdatePattern(Pattern newPattern)
        {
            if (newPattern == null)
                throw (new ArgumentNullException("newPattern"));
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@PatternId", SqlDbType.Int, 0, ParameterDirection.Input, newPattern.PatternId);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, newPattern.UserId);
            AddParamToSQLCmd(sqlCmd, "@Source", SqlDbType.TinyInt, 0, ParameterDirection.Input, newPattern.Source);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, newPattern.Types);
            AddParamToSQLCmd(sqlCmd, "@Number", SqlDbType.NVarChar, 255, ParameterDirection.Input, newPattern.Number);
            AddParamToSQLCmd(sqlCmd, "@Expression", SqlDbType.NVarChar, 4000, ParameterDirection.Input, newPattern.Expression);
            AddParamToSQLCmd(sqlCmd, "@Hits", SqlDbType.Int, 0, ParameterDirection.Input, newPattern.Hits);
            AddParamToSQLCmd(sqlCmd, "@CreateDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newPattern.CreateDate);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Pattern_Update);
            ExecuteScalarCmd(sqlCmd);
            //判断是否更新成功
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 获取所有的检索式
        /// </summary>
        /// <returns></returns>
        public override List<Pattern> GetAllPatterns()
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Pattern_GetAllPatterns);
            //新建一个Pattern类型的泛型列表
            List<Pattern> PatternList = new List<Pattern>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGeneratePatternListFromReader<Pattern>泛型方法作为委托类型参数，PatternList用于返回值
            TExecuteReaderCmd<Pattern>(sqlCmd, TGeneratePatternListFromReader<Pattern>, ref PatternList);
            //返回泛型列表类
            return PatternList;
        }
        /// <summary>
        /// 获取指定用户、类型、来源的检索式
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="source"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public override List<Pattern> GetPatternsByUserIdAndSourceAndTypes(int userId, byte source, byte types)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@Source", SqlDbType.TinyInt, 0, ParameterDirection.Input, source);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, types);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Pattern_GetPatternsByUserIdAndSourceAndTypes);
            //新建一个Pattern类型的泛型列表
            List<Pattern> PatternList = new List<Pattern>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGeneratePatternListFromReader<Pattern>泛型方法作为委托类型参数，PatternList用于返回值
            TExecuteReaderCmd<Pattern>(sqlCmd, TGeneratePatternListFromReader<Pattern>, ref PatternList);
            //返回泛型列表类
            return PatternList;
        }
        /// <summary>
        /// 获取指定用户、类型、关键词的检索式
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="types"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public override List<Pattern> GetPatternsByUserIdAndTypesAndKeyword(int userId, byte types, string keyword)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, types);
            AddParamToSQLCmd(sqlCmd, "@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Pattern_GetPatternsByUserIdAndTypesAndKeyword);
            //新建一个Pattern类型的泛型列表
            List<Pattern> PatternList = new List<Pattern>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGeneratePatternListFromReader<Pattern>泛型方法作为委托类型参数，PatternList用于返回值
            TExecuteReaderCmd<Pattern>(sqlCmd, TGeneratePatternListFromReader<Pattern>, ref PatternList);
            //返回泛型列表类
            return PatternList;
        }
        /// <summary>
        /// 根据指定的检索式ID获取检索式
        /// </summary>
        /// <param name="patternId"></param>
        /// <returns></returns>
        public override Pattern GetPatternByPatternId(int patternId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@PatternId", SqlDbType.Int, 0, ParameterDirection.Input, patternId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Pattern_GetPatternByPatternId);
            //新建一个Pattern类型的泛型列表
            List<Pattern> PatternList = new List<Pattern>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGeneratePatternListFromReader<Pattern>泛型方法作为委托类型参数，PatternList用于返回值
            TExecuteReaderCmd<Pattern>(sqlCmd, TGeneratePatternListFromReader<Pattern>, ref PatternList);
            //如果列表项总数大于0.
            if (PatternList.Count > 0)
                //返回第一个列表项。
                return PatternList[0];
            else
                return null;
        }

        /*** 分类 ***/

        //创建分类存储过程
        private const string SP_Album_Create = "TLC_InsertNewAlbum";
        //删除分类存储过程
        private const string SP_Album_Delete = "TLC_DeleteAlbum";
        //更新分类存储过程
        private const string SP_Album_Update = "TLC_UpdateAlbum";
        //获取所有分类列表的存储过程
        private const string SP_Album_GetAllAlbums = "TLC_SelectAllAlbums";
        //根据用户获取分类列表的存储过程
        private const string SP_Album_GetAlbumsByUserId = "TLC_SelectAlbumsByUserId";
        //根据父级分类获取分类列表的存储过程
        private const string SP_Album_GetAlbumsByParentId = "TLC_SelectAlbumsByParentId";
        //根据分类获取父级分类列表的存储过程
        private const string SP_Album_GetAlbumsByAlbumId = "TLC_SelectAlbumsByAlbumId";
        //根据分类ID获取分类的存储过程
        private const string SP_Album_GetAlbumByAlbumId = "TLC_SelectAlbumByAlbumId";
        /// <summary>
        /// 创建新的分类
        /// </summary>
        /// <param name="newAlbum">新的分类对象</param>
        /// <returns></returns>
        public override int CreateNewAlbum(Album newAlbum)
        {
            if (newAlbum == null)
                throw (new ArgumentNullException("newAlbum"));
            SqlCommand sqlCmd = new SqlCommand();
            //调用Sql帮助方法的AddParamToSQLCmd为提定的SqlCommand对象添加参数。
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, newAlbum.UserId);
            AddParamToSQLCmd(sqlCmd, "@ParentId", SqlDbType.Int, 0, ParameterDirection.Input, newAlbum.ParentId);
            AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, newAlbum.Title);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 255, ParameterDirection.Input, newAlbum.Note);
            AddParamToSQLCmd(sqlCmd, "@Collects", SqlDbType.Int, 0, ParameterDirection.Input, newAlbum.Collects);
            AddParamToSQLCmd(sqlCmd, "@Orders", SqlDbType.Int, 0, ParameterDirection.Input, newAlbum.Orders);
            //调用Sql帮助方法的SetCommandType方法设置SqlCommand的类型和所要执行的SQL语句或存储过程。
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Album_Create);
            //调用Sql帮助方法的ExecuteScalarCmd执行Sqlcommand中的ExecuteScalar方法。
            ExecuteScalarCmd(sqlCmd);
            //获取返回的值信息。
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }
        /// <summary>
        /// 删除指定AlbumName的分类
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public override bool DeleteAlbum(int albumId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@AlbumId", SqlDbType.Int, 0, ParameterDirection.Input, albumId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Album_Delete);
            ExecuteScalarCmd(sqlCmd);
            //获取存储过程返回值
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 更新一个分类
        /// </summary>
        /// <param name="newAlbum"></param>
        /// <returns></returns>
        public override bool UpdateAlbum(Album newAlbum)
        {
            if (newAlbum == null)
                throw (new ArgumentNullException("newAlbum"));
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@AlbumId", SqlDbType.Int, 0, ParameterDirection.Input, newAlbum.AlbumId);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, newAlbum.UserId);
            AddParamToSQLCmd(sqlCmd, "@ParentId", SqlDbType.Int, 0, ParameterDirection.Input, newAlbum.ParentId);
            AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, newAlbum.Title);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 255, ParameterDirection.Input, newAlbum.Note);
            AddParamToSQLCmd(sqlCmd, "@Collects", SqlDbType.Int, 0, ParameterDirection.Input, newAlbum.Collects);
            AddParamToSQLCmd(sqlCmd, "@Orders", SqlDbType.Int, 0, ParameterDirection.Input, newAlbum.Orders);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Album_Update);
            ExecuteScalarCmd(sqlCmd);
            //判断是否更新成功
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 获取所有的分类
        /// </summary>
        /// <returns></returns>
        public override List<Album> GetAllAlbums()
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Album_GetAllAlbums);
            //新建一个Album类型的泛型列表
            List<Album> AlbumList = new List<Album>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateAlbumListFromReader<Album>泛型方法作为委托类型参数，AlbumList用于返回值
            TExecuteReaderCmd<Album>(sqlCmd, TGenerateAlbumListFromReader<Album>, ref AlbumList);
            //返回泛型列表类
            return AlbumList;
        }
        /// <summary>
        /// 获取指定用户的分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override List<Album> GetAlbumsByUserId(int userId)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Album_GetAlbumsByUserId);
            //新建一个Album类型的泛型列表
            List<Album> AlbumList = new List<Album>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateAlbumListFromReader<Album>泛型方法作为委托类型参数，AlbumList用于返回值
            TExecuteReaderCmd<Album>(sqlCmd, TGenerateAlbumListFromReader<Album>, ref AlbumList);
            //返回泛型列表类
            return AlbumList;
        }
        /// <summary>
        /// 获取指定父级分类的分类
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public override List<Album> GetAlbumsByParentId(int parentId)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ParentId", SqlDbType.Int, 0, ParameterDirection.Input, parentId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Album_GetAlbumsByParentId);
            //新建一个Album类型的泛型列表
            List<Album> AlbumList = new List<Album>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateAlbumListFromReader<Album>泛型方法作为委托类型参数，AlbumList用于返回值
            TExecuteReaderCmd<Album>(sqlCmd, TGenerateAlbumListFromReader<Album>, ref AlbumList);
            //返回泛型列表类
            return AlbumList;
        }
        /// <summary>
        /// 获取指定分类的父级分类
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public override List<Album> GetAlbumsByAlbumId(int albumId)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@AlbumId", SqlDbType.Int, 0, ParameterDirection.Input, albumId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Album_GetAlbumsByAlbumId);
            //新建一个Album类型的泛型列表
            List<Album> AlbumList = new List<Album>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateAlbumListFromReader<Album>泛型方法作为委托类型参数，AlbumList用于返回值
            TExecuteReaderCmd<Album>(sqlCmd, TGenerateAlbumListFromReader<Album>, ref AlbumList);
            //返回泛型列表类
            return AlbumList;
        }
        /// <summary>
        /// 根据指定的分类ID获取分类
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public override Album GetAlbumByAlbumId(int albumId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@AlbumId", SqlDbType.Int, 0, ParameterDirection.Input, albumId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Album_GetAlbumByAlbumId);
            //新建一个Album类型的泛型列表
            List<Album> AlbumList = new List<Album>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateAlbumListFromReader<Album>泛型方法作为委托类型参数，AlbumList用于返回值
            TExecuteReaderCmd<Album>(sqlCmd, TGenerateAlbumListFromReader<Album>, ref AlbumList);
            //如果列表项总数大于0.
            if (AlbumList.Count > 0)
                //返回第一个列表项。
                return AlbumList[0];
            else
                return null;
        }

        /*** 收藏 ***/

        //创建收藏存储过程
        private const string SP_Collect_Create = "TLC_InsertNewCollect";
        //删除收藏存储过程
        private const string SP_Collect_Delete = "TLC_DeleteCollect";
        //更新收藏存储过程
        private const string SP_Collect_Update = "TLC_UpdateCollect";
        //获取所有收藏列表的存储过程
        private const string SP_Collect_GetAllCollects = "TLC_SelectAllCollects";
        //根据用户、分类、类型、是否标注获取收藏列表的存储过程
        private const string SP_Collect_GetCollectsByUserIdAndAlbumIdAndTypesAndIsNote = "TLC_SelectCollectsByUserIdAndAlbumIdAndTypesAndIsNote";
        //根据用户、搜索类型、关键词获取收藏列表的存储过程
        private const string SP_Collect_GetCollectsByUserIdAndSearchTypeAndKeyword = "TLC_SelectCollectsByUserIdAndSearchTypeAndKeyword";
        //根据条数获取热门收藏列表的存储过程
        private const string SP_Collect_GetCollectsHotByCounts = "TLC_SelectCollectsHotByCounts";
        //根据收藏ID获取收藏的存储过程
        private const string SP_Collect_GetCollectByCollectId = "TLC_SelectCollectByCollectId";
        //根据用户、收藏夹、专利号获取收藏的存储过程
        private const string SP_Collect_GetCollectByUserIdAndAlbumIdAndNumber = "TLC_SelectCollectByUserIdAndAlbumIdAndNumber";
        /// <summary>
        /// 创建新的收藏
        /// </summary>
        /// <param name="newCollect">新的收藏对象</param>
        /// <returns></returns>
        public override int CreateNewCollect(Collect newCollect)
        {
            if (newCollect == null)
                throw (new ArgumentNullException("newCollect"));
            SqlCommand sqlCmd = new SqlCommand();
            //调用Sql帮助方法的AddParamToSQLCmd为提定的SqlCommand对象添加参数。
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, newCollect.UserId);
            AddParamToSQLCmd(sqlCmd, "@AlbumId", SqlDbType.Int, 0, ParameterDirection.Input, newCollect.AlbumId);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, newCollect.Types);
            AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, newCollect.Title);
            AddParamToSQLCmd(sqlCmd, "@Number", SqlDbType.NVarChar, 100, ParameterDirection.Input, newCollect.Number);
            AddParamToSQLCmd(sqlCmd, "@LawState", SqlDbType.TinyInt, 0, ParameterDirection.Input, newCollect.LawState);
            AddParamToSQLCmd(sqlCmd, "@CollectDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newCollect.CollectDate);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 255, ParameterDirection.Input, newCollect.Note);
            AddParamToSQLCmd(sqlCmd, "@NoteDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newCollect.NoteDate);
            //调用Sql帮助方法的SetCommandType方法设置SqlCommand的类型和所要执行的SQL语句或存储过程。
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Collect_Create);
            //调用Sql帮助方法的ExecuteScalarCmd执行Sqlcommand中的ExecuteScalar方法。
            ExecuteScalarCmd(sqlCmd);
            //获取返回的值信息。
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }
        /// <summary>
        /// 删除指定CollectName的收藏
        /// </summary>
        /// <param name="collectId"></param>
        /// <returns></returns>
        public override bool DeleteCollect(int collectId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CollectId", SqlDbType.Int, 0, ParameterDirection.Input, collectId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Collect_Delete);
            ExecuteScalarCmd(sqlCmd);
            //获取存储过程返回值
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 更新一个收藏
        /// </summary>
        /// <param name="newCollect"></param>
        /// <returns></returns>
        public override bool UpdateCollect(Collect newCollect)
        {
            if (newCollect == null)
                throw (new ArgumentNullException("newCollect"));
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CollectId", SqlDbType.Int, 0, ParameterDirection.Input, newCollect.CollectId);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, newCollect.UserId);
            AddParamToSQLCmd(sqlCmd, "@AlbumId", SqlDbType.Int, 0, ParameterDirection.Input, newCollect.AlbumId);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, newCollect.Types);
            AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, newCollect.Title);
            AddParamToSQLCmd(sqlCmd, "@Number", SqlDbType.NVarChar, 100, ParameterDirection.Input, newCollect.Number);
            AddParamToSQLCmd(sqlCmd, "@LawState", SqlDbType.TinyInt, 0, ParameterDirection.Input, newCollect.LawState);
            AddParamToSQLCmd(sqlCmd, "@CollectDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newCollect.CollectDate);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 255, ParameterDirection.Input, newCollect.Note);
            AddParamToSQLCmd(sqlCmd, "@NoteDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newCollect.NoteDate);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Collect_Update);
            ExecuteScalarCmd(sqlCmd);
            //判断是否更新成功
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 获取所有的收藏
        /// </summary>
        /// <returns></returns>
        public override List<Collect> GetAllCollects()
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Collect_GetAllCollects);
            //新建一个Collect类型的泛型列表
            List<Collect> CollectList = new List<Collect>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateCollectListFromReader<Collect>泛型方法作为委托类型参数，CollectList用于返回值
            TExecuteReaderCmd<Collect>(sqlCmd, TGenerateCollectListFromReader<Collect>, ref CollectList);
            //返回泛型列表类
            return CollectList;
        }
        /// <summary>
        /// 获取指定用户、分类、类型、是否标注的收藏
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <param name="types"></param>
        /// <param name="isNote"></param>
        /// <returns></returns>
        public override List<Collect> GetCollectsByUserIdAndAlbumIdAndTypesAndIsNote(int userId, int albumId, byte types, byte isNote)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@AlbumId", SqlDbType.Int, 0, ParameterDirection.Input, albumId);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, types);
            AddParamToSQLCmd(sqlCmd, "@IsNote", SqlDbType.TinyInt, 0, ParameterDirection.Input, isNote);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Collect_GetCollectsByUserIdAndAlbumIdAndTypesAndIsNote);
            //新建一个Collect类型的泛型列表
            List<Collect> CollectList = new List<Collect>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateCollectListFromReader<Collect>泛型方法作为委托类型参数，CollectList用于返回值
            TExecuteReaderCmd<Collect>(sqlCmd, TGenerateCollectListFromReader<Collect>, ref CollectList);
            //返回泛型列表类
            return CollectList;
        }
        /// <summary>
        /// 获取指定用户、搜索类型、关键词的收藏
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchType">关键词 名称 申请号 法律状态 标注 标注日期</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public override List<Collect> GetCollectsByUserIdAndSearchTypeAndKeyword(int userId, byte searchType, string keywords)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@SearchType", SqlDbType.TinyInt, 0, ParameterDirection.Input, searchType);
            AddParamToSQLCmd(sqlCmd, "@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keywords);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Collect_GetCollectsByUserIdAndSearchTypeAndKeyword);
            //新建一个Collect类型的泛型列表
            List<Collect> CollectList = new List<Collect>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateCollectListFromReader<Collect>泛型方法作为委托类型参数，CollectList用于返回值
            TExecuteReaderCmd<Collect>(sqlCmd, TGenerateCollectListFromReader<Collect>, ref CollectList);
            //返回泛型列表类
            return CollectList;
        }
        /// <summary>
        /// 获取指定条数的热门收藏
        /// </summary>
        /// <param name="counts"></param>
        /// <returns></returns>
        public override List<Collect> GetCollectsHotByCounts(int counts)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@Counts", SqlDbType.Int, 0, ParameterDirection.Input, counts);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Collect_GetCollectsHotByCounts);
            //新建一个Collect类型的泛型列表
            List<Collect> CollectList = new List<Collect>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateCollectListFromReader<Collect>泛型方法作为委托类型参数，CollectList用于返回值
            TExecuteReaderCmd<Collect>(sqlCmd, TGenerateCollectListFromReader<Collect>, ref CollectList);
            //返回泛型列表类
            return CollectList;
        }
        /// <summary>
        /// 根据指定的收藏ID获取收藏
        /// </summary>
        /// <param name="collectId"></param>
        /// <returns></returns>
        public override Collect GetCollectByCollectId(int collectId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@CollectId", SqlDbType.Int, 0, ParameterDirection.Input, collectId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Collect_GetCollectByCollectId);
            //新建一个Collect类型的泛型列表
            List<Collect> CollectList = new List<Collect>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateCollectListFromReader<Collect>泛型方法作为委托类型参数，CollectList用于返回值
            TExecuteReaderCmd<Collect>(sqlCmd, TGenerateCollectListFromReader<Collect>, ref CollectList);
            //如果列表项总数大于0.
            if (CollectList.Count > 0)
                //返回第一个列表项。
                return CollectList[0];
            else
                return null;
        }
        /// <summary>
        /// 根据指定的用户、收藏夹、专利号获取收藏
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public override Collect GetCollectByUserIdAndAlbumIdAndNumber(int userId, int albumId, string number)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@AlbumId", SqlDbType.Int, 0, ParameterDirection.Input, albumId);
            AddParamToSQLCmd(sqlCmd, "@Number", SqlDbType.NVarChar, 100, ParameterDirection.Input, number);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Collect_GetCollectByUserIdAndAlbumIdAndNumber);
            //新建一个Collect类型的泛型列表
            List<Collect> CollectList = new List<Collect>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateCollectListFromReader<Collect>泛型方法作为委托类型参数，CollectList用于返回值
            TExecuteReaderCmd<Collect>(sqlCmd, TGenerateCollectListFromReader<Collect>, ref CollectList);
            //如果列表项总数大于0.
            if (CollectList.Count > 0)
                //返回第一个列表项。
                return CollectList[0];
            else
                return null;
        }

        /*** 日志 ***/

        //创建新日志存储过程
        private const string SP_Log_Create = "TLC_InsertNewLog";
        //删除日志存储过程
        private const string SP_Log_Delete = "TLC_DeleteLog";
        //更新日志存储过程
        private const string SP_Log_Update = "TLC_UpdateLog";
        //获取所有日志列表的存储过程
        private const string SP_Log_GetAllLogs = "TLC_SelectAllLogs";
        //根据用户获取日志列表的存储过程
        private const string SP_Log_GetLogsByUserId = "TLC_SelectLogsByUserId";
        //根据用户名、类型获取日志列表的存储过程
        private const string SP_Log_GetLogsByUserNameAndType = "TLC_SelectLogsByUserNameAndType";
        //根据日志ID获取日志的存储过程
        private const string SP_Log_GetLogByLogId = "TLC_SelectLogByLogId";
        /// <summary>
        /// 创建新的日志
        /// </summary>
        /// <param name="newLog">新的日志对象</param>
        /// <returns></returns>
        public override int CreateNewLog(Log newLog)
        {
            if (newLog == null)
                throw (new ArgumentNullException("newLog"));
            SqlCommand sqlCmd = new SqlCommand();
            //调用Sql帮助方法的AddParamToSQLCmd为提定的SqlCommand对象添加参数。
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, newLog.UserId);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, newLog.Types);
            AddParamToSQLCmd(sqlCmd, "@LogDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newLog.LogDate);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NText, 0, ParameterDirection.Input, newLog.Note);
            //调用Sql帮助方法的SetCommandType方法设置SqlCommand的类型和所要执行的SQL语句或存储过程。
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Log_Create);
            //调用Sql帮助方法的ExecuteScalarCmd执行Sqlcommand中的ExecuteScalar方法。
            ExecuteScalarCmd(sqlCmd);
            //获取返回的值信息。
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }
        /// <summary>
        /// 删除指定LogName的日志
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public override bool DeleteLog(int logId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@LogId", SqlDbType.Int, 0, ParameterDirection.Input, logId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Log_Delete);
            ExecuteScalarCmd(sqlCmd);
            //获取存储过程返回值
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 更新一个日志
        /// </summary>
        /// <param name="newLog"></param>
        /// <returns></returns>
        public override bool UpdateLog(Log newLog)
        {
            if (newLog == null)
                throw (new ArgumentNullException("newLog"));
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@LogId", SqlDbType.Int, 0, ParameterDirection.Input, newLog.LogId);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, newLog.UserId);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, newLog.Types);
            AddParamToSQLCmd(sqlCmd, "@LogDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newLog.LogDate);
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NText, 0, ParameterDirection.Input, newLog.Note);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Log_Update);
            ExecuteScalarCmd(sqlCmd);
            //判断是否更新成功
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        /// <summary>
        /// 获取所有的日志
        /// </summary>
        /// <returns></returns>
        public override List<Log> GetAllLogs()
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Log_GetAllLogs);
            //新建一个Log类型的泛型列表
            List<Log> LogList = new List<Log>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateLogListFromReader<Log>泛型方法作为委托类型参数，LogList用于返回值
            TExecuteReaderCmd<Log>(sqlCmd, TGenerateLogListFromReader<Log>, ref LogList);
            //返回泛型列表类
            return LogList;
        }
        /// <summary>
        /// 根据用户获取日志列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override List<Log> GetLogsByUserId(int userId)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Log_GetLogsByUserId);
            //新建一个Log类型的泛型列表
            List<Log> LogList = new List<Log>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateLogListFromReader<Log>泛型方法作为委托类型参数，LogList用于返回值
            TExecuteReaderCmd<Log>(sqlCmd, TGenerateLogListFromReader<Log>, ref LogList);
            //返回泛型列表类
            return LogList;
        }
        /// <summary>
        /// 根据用户名、类型获取日志列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public override List<Log> GetLogsByUserNameAndType(string userName, byte types)
        {
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, userName);
            AddParamToSQLCmd(sqlCmd, "@Types", SqlDbType.TinyInt, 0, ParameterDirection.Input, types);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Log_GetLogsByUserNameAndType);
            //新建一个Log类型的泛型列表
            List<Log> LogList = new List<Log>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateLogListFromReader<Log>泛型方法作为委托类型参数，LogList用于返回值
            TExecuteReaderCmd<Log>(sqlCmd, TGenerateLogListFromReader<Log>, ref LogList);
            //返回泛型列表类
            return LogList;
        }
        /// <summary>
        /// 根据指定的日志ID获取日志
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public override Log GetLogByLogId(int logId)
        {
            //初始化一个SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand();
            //为SqlCommand对象添加参数列表
            AddParamToSQLCmd(sqlCmd, "@LogId", SqlDbType.Int, 0, ParameterDirection.Input, logId);
            //为SqlCommand对象设置CommandType和CommandText
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Log_GetLogByLogId);
            //新建一个Log类型的泛型列表
            List<Log> LogList = new List<Log>();
            //调用TExecuteReaderCmd<T>泛型方法，传递TGenerateLogListFromReader<Log>泛型方法作为委托类型参数，LogList用于返回值
            TExecuteReaderCmd<Log>(sqlCmd, TGenerateLogListFromReader<Log>, ref LogList);
            //如果列表项总数大于0.
            if (LogList.Count > 0)
                //返回第一个列表项。
                return LogList[0];
            else
                return null;
        }

        /// <summary>
        /*****************************  SQL帮助方法 *****************************/
        /// <summary>
        /// 为SqlCommand对象添加参数
        /// </summary>
        /// <param name="sqlCmd">SqlCommand对象</param>
        /// <param name="paramId">参数ID</param>
        /// <param name="sqlType">Sql类型</param>
        /// <param name="paramSize">参数大小</param>
        /// <param name="paramDirection">参数方向</param>
        /// <param name="paramvalue">参数值</param>
        private void AddParamToSQLCmd(SqlCommand sqlCmd,
                                      string paramId,
                                      SqlDbType sqlType,
                                      int paramSize,
                                      ParameterDirection paramDirection,
                                      object paramvalue)
        {

            if (sqlCmd == null)
                throw (new ArgumentNullException("sqlCmd"));
            if (paramId == string.Empty)
                throw (new ArgumentOutOfRangeException("paramId"));

            SqlParameter newSqlParam = new SqlParameter();
            newSqlParam.ParameterName = paramId;
            newSqlParam.SqlDbType = sqlType;
            newSqlParam.Direction = paramDirection;

            if (paramSize > 0)
                newSqlParam.Size = paramSize;

            if (paramvalue != null)
                newSqlParam.Value = paramvalue;

            sqlCmd.Parameters.Add(newSqlParam);
        }

        /// <summary>
        /// 调用指定SQLCommand对象的ExecuteScalar方法，返回指定查询的第一行的第一列
        /// </summary>
        /// <param name="sqlCmd"></param>
        private void ExecuteScalarCmd(SqlCommand sqlCmd)
        {
            if (ConnectionString == string.Empty)
                throw (new ArgumentOutOfRangeException("ConnectionString"));

            if (sqlCmd == null)
                throw (new ArgumentNullException("sqlCmd"));

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCmd.Connection = cn;
                sqlCmd.CommandTimeout = cn.ConnectionTimeout;
                cn.Open();
                sqlCmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 设置SqlCommand对象的参数    
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        private void SetCommandType(SqlCommand sqlCmd, CommandType cmdType, string cmdText)
        {
            sqlCmd.CommandType = cmdType;
            sqlCmd.CommandText = cmdText;
        }

        /// <summary>
        /// 执行指定的SqlCommand.ExecuteReader方法，将将返回的SqlDataReader对象转换成指定类型的泛型列表
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="sqlCmd">SqlCommand对象</param>
        /// <param name="gcfr">委托</param>
        /// <param name="List">泛型列表对象</param>
        private void TExecuteReaderCmd<T>(SqlCommand sqlCmd, TGenerateListFromReader<T> gcfr, ref List<T> List)
        {
            if (ConnectionString == string.Empty)
                throw (new ArgumentOutOfRangeException("ConnectionString"));

            if (sqlCmd == null)
                throw (new ArgumentNullException("sqlCmd"));

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCmd.Connection = cn;
                sqlCmd.CommandTimeout = cn.ConnectionTimeout;
                cn.Open();
                //========================================
                gcfr(sqlCmd.ExecuteReader(), ref List);
                //=======================================
            }
        }

        /*****************************  泛型列表帮助方法 *****************************/

        /// <summary>
        /// 从数据库中获取的用户信息，转换成用户泛型列表对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="returnData">返回数据的SqlDataReader对象</param>
        /// <param name="userList">列表集合对象</param>
        private void TGenerateUserListFromReader<T>(SqlDataReader returnData, ref List<Users> userList)
        {
            while (returnData.Read())
            {
                Users newUser = new Users((int)returnData["UserId"], (string)returnData["UserName"], (string)returnData["Password"], (string)returnData["TrueName"], (string)returnData["Mobile"], (string)returnData["Tel"], (string)returnData["Adds"], (int)returnData["CorpId"], (DateTime)returnData["ExpiryDate"], (string)returnData["Authoritys"], (string)returnData["Note"], (byte)returnData["State"]);
                userList.Add(newUser);
            }
        }

        /// <summary>
        /// 从数据库中获取的企业信息，转换成企业泛型列表对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="returnData">返回数据的SqlDataReader对象</param>
        /// <param name="corpList">列表集合对象</param>
        private void TGenerateCorpListFromReader<T>(SqlDataReader returnData, ref List<Corp> corpList)
        {
            while (returnData.Read())
            {
                Corp newCorp = new Corp((int)returnData["CorpId"], (string)returnData["Admin"], (string)returnData["Title"], (string)returnData["Note"]);
                corpList.Add(newCorp);
            }
        }

        /// <summary>
        /// 从数据库中获取的权限信息，转换成权限泛型列表对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="returnData">返回数据的SqlDataReader对象</param>
        /// <param name="authorityList">列表集合对象</param>
        private void TGenerateAuthorityListFromReader<T>(SqlDataReader returnData, ref List<Authority> authorityList)
        {
            while (returnData.Read())
            {
                Authority newAuthority = new Authority((int)returnData["AuthorityId"], (byte)returnData["Types"], (string)returnData["Url"], (string)returnData["Title"], (string)returnData["Note"]);
                authorityList.Add(newAuthority);
            }
        }

        /// <summary>
        /// 从数据库中获取的检索式信息，转换成检索式泛型列表对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="returnData">返回数据的SqlDataReader对象</param>
        /// <param name="patternList">列表集合对象</param>
        private void TGeneratePatternListFromReader<T>(SqlDataReader returnData, ref List<Pattern> patternList)
        {
            while (returnData.Read())
            {
                Pattern newPattern = new Pattern((int)returnData["PatternId"], (int)returnData["UserId"], (byte)returnData["Source"], (byte)returnData["Types"], (string)returnData["Number"], (string)returnData["Expression"], (int)returnData["Hits"], (DateTime)returnData["CreateDate"]);
                patternList.Add(newPattern);
            }
        }

        /// <summary>
        /// 从数据库中获取的分类信息，转换成分类泛型列表对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="returnData">返回数据的SqlDataReader对象</param>
        /// <param name="albumList">列表集合对象</param>
        private void TGenerateAlbumListFromReader<T>(SqlDataReader returnData, ref List<Album> albumList)
        {
            while (returnData.Read())
            {
                Album newAlbum = new Album((int)returnData["AlbumId"], (int)returnData["UserId"], (int)returnData["ParentId"], (string)returnData["Title"], (string)returnData["Note"], (int)returnData["Collects"], (int)returnData["Orders"]);
                albumList.Add(newAlbum);
            }
        }

        /// <summary>
        /// 从数据库中获取的收藏信息，转换成收藏泛型列表对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="returnData">返回数据的SqlDataReader对象</param>
        /// <param name="collectList">列表集合对象</param>
        private void TGenerateCollectListFromReader<T>(SqlDataReader returnData, ref List<Collect> collectList)
        {
            while (returnData.Read())
            {
                Collect newCollect = new Collect((int)returnData["CollectId"], (int)returnData["UserId"], (int)returnData["AlbumId"], (byte)returnData["Types"], (string)returnData["Title"], (string)returnData["Number"], (byte)returnData["LawState"], (DateTime)returnData["CollectDate"], (string)returnData["Note"], (DateTime)returnData["NoteDate"]);
                collectList.Add(newCollect);
            }
        }

        /// <summary>
        /// 从数据库中获取的日志信息，转换成日志泛型列表对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="returnData">返回数据的SqlDataReader对象</param>
        /// <param name="logList">列表集合对象</param>
        private void TGenerateLogListFromReader<T>(SqlDataReader returnData, ref List<Log> logList)
        {
            while (returnData.Read())
            {
                Log newLog = new Log((int)returnData["LogId"], (int)returnData["UserId"], (byte)returnData["Types"], (DateTime)returnData["LogDate"], (string)returnData["Note"]);
                logList.Add(newLog);
            }
        }
    }
}