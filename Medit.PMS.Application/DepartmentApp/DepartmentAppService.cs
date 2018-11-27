using AutoMapper;
using Medit.PMS.Application.DepartmentApp.Dtos;
using Medit.PMS.Domain.Entities;
using Medit.PMS.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medit.PMS.Application.DepartmentApp
{
    public class DepartmentAppService : IDepartmentAppService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentAppService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public void BatchDelete(List<string> ids)
        {

            _repository.Delete(it => ids.Contains(it.Id));
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public DepartmentDto Get(string id)
        {
            return Mapper.Map<DepartmentDto>(_repository.Get(id));
        }

        public List<DepartmentDto> GetAllList()
        {
            return Mapper.Map<List<DepartmentDto>>(_repository.GetAll(it => it.Id != string.Empty).OrderBy(it => it.Code));
        }

        public List<DepartmentDto> GetChindrenByParent(string parentId, int page, int size, out int rowCount)
        {
            return Mapper.Map<List<DepartmentDto>>(_repository.LoadPageList(page, size, out rowCount, it => it.ParentId == parentId, it => it.Code));
        }

        public bool InsertOrUpdate(DepartmentDto dto)
        {
            var department= _repository.InsertOrUpdate(Mapper.Map<Department>(dto));
                return department != null;
        }
    }
}