using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medit.PMS.Web.Controllers
{
    public class PMSControllerBase : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            byte[] result;
            context.HttpContext.Session.TryGetValue("CurrentUser", out result);
            if (result == null)
            {
                context.Result = new RedirectResult("/Login/Index");
                return;
            }
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 获取服务端验证的第一条错误消息
        /// </summary>
        /// <returns></returns>
        public string GetModelStateError()
        {
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                    return item.Errors[0].ErrorMessage;
            }
            return string.Empty;
        }
    }
}
