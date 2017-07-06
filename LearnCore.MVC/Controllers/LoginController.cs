using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearnCore.Application.UserApp;
using LearnCore.MVC.Models;
using LearnCore.Utility;

namespace LearnCore.Controllers
{
    public class LoginController : Controller
    {
        private IUserAppService userService;
        public LoginController(IUserAppService userAppService)
        {
            userService = userAppService;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //检查用户
                var user = userService.CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    //记录用户
                    HttpContext.Session.Set("CurrentUser", ByteConvertHelper.Object2Bytes(user));
                    //跳转系统首页
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "用户名密码错误！");
                    return View();
                }
            }

            return View();
        }
    }
}