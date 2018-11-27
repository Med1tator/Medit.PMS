using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Medit.PMS.Web.Models;

namespace Medit.PMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(string errorMsg=null)
        {
            ErrorModel errorModel = new ErrorModel()
            {
                Msg = errorMsg ?? "",
                PageTitle = "错误"
            };
            return View(errorModel);
        }
    }
}
