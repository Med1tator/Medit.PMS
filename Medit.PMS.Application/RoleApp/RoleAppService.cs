using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Medit.PMS.Application.RoleApp.Dtos;
using Medit.PMS.Domain.Entities;
using Medit.PMS.Domain.IRepositories;

namespace Medit.PMS.Application.RoleApp
{
    public class RoleAppService : IRoleAppService
    {
        private readonly IRoleRepository _repository;

        public RoleAppService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public void BatchDelete(List<string> ids)
        {
            _repository.Delete(o => ids.Contains(o.Id));
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public RoleDto Get(string id)
        {
            return Mapper.Map<RoleDto>(_repository.Get(id));
        }

        public List<RoleDto> GetAllList()
        {
            return Mapper.Map<List<RoleDto>>(_repository.GetAll(o => o.Id != string.Empty).OrderBy(o => o.Code));
        }

        public List<string> GetAllMenuListByRole(string roleId)
        {
            return _repository.GetAllMenu(roleId);
        }

        public List<RoleDto> GetAllPageList(int page, int size, out int total)
        {
            return Mapper.Map<List<RoleDto>>(_repository.LoadPageList(page, size, out total, null, o => o.Code));
        }

        public bool InsertOrUpdate(RoleDto dto)
        {
            var role = _repository.InsertOrUpdate(Mapper.Map<Role>(dto));
            return role != null;
        }

        public bool UpdateRoleMenu(string roleId, List<RoleMenuDto> roleMenus)
        {
            return _repository.UpdateRoleMenu(roleId, Mapper.Map<List<RoleMenu>>(roleMenus));
        }
    }
}
