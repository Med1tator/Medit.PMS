using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Medit.PMS.Application.UserApp.Dtos;
using Medit.PMS.Domain.Entities;
using Medit.PMS.Domain.IRepositories;

namespace Medit.PMS.Application.UserApp
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _repository;

        public UserAppService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void BatchDelete(List<string> ids)
        {
            _repository.Delete(o => ids.Contains(o.Id));
        }

        public User CheckUser(string userName, string password)
        {
            return _repository.Check(userName, password);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public UserDto Get(string id)
        {
            return Mapper.Map<UserDto>(_repository.Get(id));
        }

        public List<UserDto> GetUserByDepartment(string departmentId, int page,  int size,out int total)
        {
            return Mapper.Map<List<UserDto>>(_repository.LoadPageList(page, size, out total, o => o.DepartmentId == departmentId,o=>o.CreateTime).ToList());
        }

        public UserDto InsertOrUpdate(UserDto dto)
        {
            return Mapper.Map<UserDto>(_repository.InsertOrUpdate(Mapper.Map<User>(dto)));
        }
    }
}
