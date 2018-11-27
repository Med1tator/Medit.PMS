using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medit.PMS.Application.RoleApp;
using Medit.PMS.Application.UserApp;
using Medit.PMS.Application.UserApp.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Medit.PMS.Web.Controllers
{
    public class UserController : PMSControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IRoleAppService _roleAppService;

        public UserController(IUserAppService userAppService, IRoleAppService roleAppService)
        {
            _userAppService = userAppService;
            _roleAppService = roleAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUserByDepartment(string departmentId, int startPage, int pagesize)
        {
            int rowCount = 0;
            var result = _userAppService.GetUserByDepartment(departmentId, startPage, pagesize, out rowCount);
            var roles = _roleAppService.GetAllList();
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pagesize),
                rows = result,
                roles = roles
            });
        }

        public IActionResult Edit(UserDto dto, string roles)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Id))
                    dto.Id = Guid.NewGuid().ToString();

                var userRoles = new List<UserRoleDto>();
                foreach (var role in roles.Split(','))
                {
                    userRoles.Add(new UserRoleDto()
                    {
                        UserId = dto.Id,
                        RoleId = role
                    });
                }
                dto.UserRoles = userRoles;
                var user = _userAppService.InsertOrUpdate(dto);
                return Json(new { Result = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Faild", Message = ex.Message });
            }
        }

        public IActionResult DeleteMulti(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<string> delIds = idArray.ToList();

                _userAppService.BatchDelete(delIds);
                return Json(new { Result = "Success" });

            }
            catch (Exception ex)
            {
                return Json(new { Result = "Faild", Message = ex.Message });
            }
        }

        public IActionResult Delete(string id)
        {
            try
            {
                _userAppService.Delete(id);
                return Json(new { Result = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Faild", Message = ex.Message });
            }
        }

        public IActionResult Get(string id)
        {
            var dto = _userAppService.Get(id);
            return Json(dto);
        }
    }
}