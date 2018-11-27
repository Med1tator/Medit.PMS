using Medit.PMS.Application.RoleApp.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Medit.PMS.Application.RoleApp
{
    public interface IRoleAppService
    {
        List<RoleDto> GetAllList();

        List<RoleDto> GetAllPageList(int page, int size, out int total);

        bool InsertOrUpdate(RoleDto dto);

        void BatchDelete(List<string> ids);

        void Delete(string id);

        RoleDto Get(string id);

        List<string> GetAllMenuListByRole(string roleId);

        bool UpdateRoleMenu(string roleId,List<RoleMenuDto> roleMenus);
    }
}