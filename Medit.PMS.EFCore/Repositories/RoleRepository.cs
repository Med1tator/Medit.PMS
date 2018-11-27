using Medit.PMS.Domain.Entities;
using Medit.PMS.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Medit.PMS.EFCore.Repositories
{
    public class RoleRepository : PMSRepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(PMSDbContext dbContext)
            : base(dbContext) { }

        public List<string> GetAllMenu(string roleId)
        {
            var roleMenus = _dbContext.Set<RoleMenu>().Where(r => r.RoleId == roleId);

            return roleMenus.Select(r => r.MenuId).ToList();
        }

        public bool UpdateRoleMenu(string roleId, List<RoleMenu> roleMenus)
        {
            var oldRoleMenus = _dbContext.Set<RoleMenu>().Where(rm => rm.RoleId == roleId).ToList();
            oldRoleMenus.ForEach(o => _dbContext.Set<RoleMenu>().Remove(o));
            _dbContext.SaveChanges();

            _dbContext.Set<RoleMenu>().AddRange(roleMenus);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
