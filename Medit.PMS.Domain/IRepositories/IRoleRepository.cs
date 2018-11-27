using Medit.PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medit.PMS.Domain.IRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        List<string> GetAllMenu(string roleId);

        bool UpdateRoleMenu(string roleId, List<RoleMenu> roleMenus);
    }
}
