using System;
using System.Collections.Generic;
using System.Text;
using LearnCore.Domain.Entities;
using LearnCore.Domain.IRepositories;

namespace LearnCore.Application.UserApp
{
    /// <summary>
    /// 用户管理服务
    /// </summary>
    public class UserAppService : IUserAppService
    {
        /// <summary>
        /// 用户管理仓储接口
        /// </summary>
        private readonly IUserRepository dbUser;

        /// <summary>
        /// 构造函数 实现依赖注入
        /// </summary>
        /// <param name="userRepository">仓储对象</param>
        public UserAppService(IUserRepository userRepository)
        {
            dbUser = userRepository;
        }

        /// <summary>
        /// 用户检查
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User CheckUser(string userName, string password)
        {
            return dbUser.CheckUser(userName, password);
        }
    }
}
