using LearnCore.Domain.Entities;
using LearnCore.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace LearnCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 用户管理仓储实现
    /// </summary>
    public class UserRepository : LearnCoreRepositoryBase<User>, IUserRepository
    {
        public UserRepository(LearnCoreDbContext dbContext) : base(dbContext)
        {

        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User CheckUser(string userName, string password)
        {
            return db.Set<User>().FirstOrDefault(it => it.UserName == userName && it.Password == password);
        }
    }
}