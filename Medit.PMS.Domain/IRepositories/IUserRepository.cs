using Medit.PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medit.PMS.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Check(string userName, string password);

        User GetWithRoles(string id);
    }
}
