using LearnCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LearnCore.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>存在返回实体,否则返回null</returns>
        User CheckUser(string userName, string password);
    }
}