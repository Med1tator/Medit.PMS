using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medit.PMS.Application.DepartmentApp;
using Medit.PMS.Application.DepartmentApp.Dtos;
using Medit.PMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medit.PMS.Web.Controllers
{
    public class DepartmentController : PMSControllerBase
    {
        private readonly IDepartmentAppService _departmentAppService;

        public DepartmentController(IDepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(DepartmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Result = "Faild", Message = GetModelStateError() });
            }
            dto.Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid().ToString() : dto.Id;//新增
            if (_departmentAppService.InsertOrUpdate(dto))
            {
                return Json(new { Result = "Success" });
            }
            return Json(new { Result = "Faild" });
        }

        public IActionResult GetTreeData()
        {
            var dtos = _departmentAppService.GetAllList();
            List<TreeModel> treeModels = dtos.Select(o => new TreeModel() { Id = o.Id, Text = o.Name, Parent = string.IsNullOrEmpty(o.ParentId) ? "#" : o.ParentId }).ToList();

            return Json(treeModels);
        }

        public IActionResult GetChildrenByParent(string parentId, int page, int size)
        {
            int rowCount = 0;
            var result = _departmentAppService.GetChindrenByParent(parentId, page, size, out rowCount);

            return Json(new { RowCount = rowCount, PageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / size), Rows = result });
        }

        public IActionResult DeleteMulti(string ids)
        {
            try
            {
                _departmentAppService.BatchDelete(ids.Split(',').ToList());
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
                _departmentAppService.Delete(id);
                return Json(new { Result = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Faild", Message = ex.Message });
            }
        }

        public IActionResult Get(string id)
        {
            var dto = _departmentAppService.Get(id);
            return Json(dto);
        }
    }
}