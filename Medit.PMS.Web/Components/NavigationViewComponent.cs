﻿using Medit.PMS.Application.MenuApp;
using Medit.PMS.Application.UserApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medit.PMS.Web.Components
{
    [ViewComponent(Name ="Navigation")]
    public class NavigationViewComponent:ViewComponent
    {
        private readonly IMenuAppService _menuAppService;
        private readonly IUserAppService _userAppService;
        public NavigationViewComponent(IMenuAppService menuAppService, IUserAppService userAppService)
        {
            _menuAppService = menuAppService;
            _userAppService = userAppService;
        }

        public IViewComponentResult Invoke() {
            var userId = HttpContext.Session.GetString("CurrentUserId");
            var menus = _menuAppService.GetMenuByUser(userId);
            return View(menus);
        }
    }
}
