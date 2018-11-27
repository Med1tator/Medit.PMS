using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medit.PMS.Application.UserApp;
using Medit.PMS.Utility;
using Medit.PMS.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medit.PMS.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAppService _userAppService;

        public LoginController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userAppService.CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("CurrentUserId", user.UserName);
                    HttpContext.Session.Set("CurrentUser", ByteConvertUtility.Object2Bytes(user));

                    return RedirectToAction("Index", "Department");
                }
                ViewBag.ErrorMsg = "用户名或密码错误！";
            }
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count>0)
                {
                    ViewBag.ErrorMsg = item.Errors[0].ErrorMessage;
                    break;
                }
            }
            return View(model);
        }
    }
}