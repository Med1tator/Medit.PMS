using Medit.PMS.Domain.Entities;
using Medit.PMS.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medit.PMS.EFCore.Repositories
{
    public class UserRepository : PMSRepositoryBase<User>, IUserRepository
    {
        public UserRepository(PMSDbContext dbContext)
            : base(dbContext) { }

        public User Check(string userName, string password)
        {
            return _dbContext.Set<User>().FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }

        public User GetWithRoles(string id)
        {
            User user = _dbContext.Set<User>().FirstOrDefault(u => u.Id == id);
            if (user != null)
                user.UserRoles = _dbContext.Set<UserRole>().Where(it => it.UserId == id).ToList();

            return user;
        }
    }
}
