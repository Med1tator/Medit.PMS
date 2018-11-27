using Medit.PMS.Application.DepartmentApp.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medit.PMS.Application.DepartmentApp
{
    public interface IDepartmentAppService
    {
        List<DepartmentDto> GetAllList();

        List<DepartmentDto> GetChindrenByParent(string parentId, int page, int size, out int rowCount);

        bool InsertOrUpdate(DepartmentDto dto);

        void BatchDelete(List<string> ids);

        void Delete(string id);

        DepartmentDto Get(string id);
    }
}
