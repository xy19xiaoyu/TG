using System;
using System.Collections.Generic;
using System.Text;

namespace Cpic.Cprs2010.User
{
    public enum UserLevel
    {
        /// <summary>
        /// 游客
        /// </summary>
        Guest = 0,
        /// <summary>
        /// 注册用户
        /// </summary>
        User = 1,
        /// <summary>
        /// 专家
        /// </summary>
        Master = 2,
        /// <summary>
        /// 管理员
        /// </summary>
        Admin = 3,
        /// <summary>
        /// 超级管理员
        /// </summary>
        SuperAdmin = 4,
    }
}
