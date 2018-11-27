using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Medit.PMS.Application.MenuApp.Dtos;
using Medit.PMS.Domain.Entities;
using Medit.PMS.Domain.IRepositories;

namespace Medit.PMS.Application.MenuApp
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public MenuAppService(IMenuRepository menuRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _menuRepository = menuRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public void BatchDelete(List<string> ids)
        {
            _menuRepository.Delete(o => ids.Contains(o.Id));
        }

        public void Delete(string id)
        {
            _menuRepository.Delete(id);
        }

        public MenuDto Get(string id)
        {
            return Mapper.Map<MenuDto>(_menuRepository.Get(id));
        }

        public List<MenuDto> GetAllList()
        {
            return Mapper.Map<List<MenuDto>>(_menuRepository.GetAll().OrderBy(o => o.SerialNumber));
        }

        public List<MenuDto> GetMenuByParent(string parentId, int page, int size, out int total)
        {
            var menus = _menuRepository.LoadPageList(page, size, out total, o => o.ParentId == parentId, o => o.SerialNumber);
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public List<MenuDto> GetMenuByUser(string userId)
        {
           var menus= _menuRepository.GetAll(o => o.Type == 0).OrderBy(o => o.SerialNumber);
            if (string.IsNullOrEmpty(userId)||userId.ToLower()=="admin")// 超级管理员
                return Mapper.Map<List<MenuDto>>(menus);

            var user = _userRepository.GetWithRoles(userId);
            if (user == null)// 为找到人员
                return new List<MenuDto>();

            var userRoles = user.UserRoles;

            List<string> menuIds = new List<string>();
            foreach (var ur in userRoles)
            {
                menuIds = menuIds.Union(_roleRepository.GetAllMenu(ur.RoleId)).ToList();
            }

            return Mapper.Map<List<MenuDto>>(menus.Where(o => menuIds.Contains(o.Id)));
        }

        public bool InsertOrUpdate(MenuDto dto)
        {
            var menu= _menuRepository.InsertOrUpdate(Mapper.Map<Menu>(dto));
            return menu != null;
        }
    }
}
