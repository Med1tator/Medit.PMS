using Medit.PMS.Application.MenuApp.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Medit.PMS.Application.MenuApp
{
    public interface IMenuAppService
    {
        List<MenuDto> GetAllList();

        List<MenuDto> GetMenuByParent(string parentId, int page, int size, out int total);

        bool InsertOrUpdate(MenuDto dto);

        void BatchDelete(List<string> ids);

        void Delete(string id);

        MenuDto Get(string id);

        List<MenuDto> GetMenuByUser(string userId);
    }
}