using Medit.PMS.Application.UserApp.Dtos;
using Medit.PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medit.PMS.Application.UserApp
{
    public interface IUserAppService
    {
        User CheckUser(string userName, string password);

        List<UserDto> GetUserByDepartment(string departmentId, int page, int size,out int total);

        UserDto InsertOrUpdate(UserDto dto);

        void BatchDelete(List<string> ids);

        void Delete(string id);

        UserDto Get(string id);
    }
}
