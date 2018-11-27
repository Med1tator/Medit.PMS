using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medit.PMS.Application.MenuApp;
using Medit.PMS.Application.MenuApp.Dtos;
using Medit.PMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medit.PMS.Web.Controllers
{
    /// <summary>
    /// 功能管理控制器
    /// </summary>
    public class MenuController : PMSControllerBase
    {
        private readonly IMenuAppService _menuAppService;
        public MenuController(IMenuAppService menuAppService, Application.UserApp.IUserAppService userAppService)
        {
            _menuAppService = menuAppService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取功能树JSON数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuTreeData()
        {
            var menus = _menuAppService.GetAllList();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var menu in menus)
            {
                treeModels.Add(new TreeModel() { Id = menu.Id.ToString(), Text = menu.Name, Parent = string.IsNullOrEmpty(menu.ParentId) ? "#" : menu.ParentId.ToString() });
            }
            return Json(treeModels);
        }
        /// <summary>
        /// 获取子级功能列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMneusByParent(string parentId, int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = _menuAppService.GetMenuByParent(parentId, startPage, pageSize, out rowCount);
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result,
            });
        }
        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IActionResult Edit(MenuDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Failed",
                    Message = GetModelStateError()
                });
            }
            if (_menuAppService.InsertOrUpdate(dto))
            {
                return Json(new { Result = "Success" });
            }
            return Json(new { Result = "Failed" });
        }

        public IActionResult DeleteMuti(string ids)
        {
            try
            {
                List<string> delIds = ids.Split(',').ToList();
                
                _menuAppService.BatchDelete(delIds);
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Failed",
                    Message = ex.Message
                });
            }
        }
        public IActionResult Delete(string id)
        {
            try
            {
                _menuAppService.Delete(id);
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Failed",
                    Message = ex.Message
                });
            }
        }
        public ActionResult Get(string id)
        {
            var dto = _menuAppService.Get(id);
            return Json(dto);
        }


    }
}