using LearnCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCore.Application.UserApp
{
    public interface IUserAppService
    {
        User CheckUser(string userName, string password);
    }
}
