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
                //����û�
                var user = userService.CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    //��¼�û�
                    HttpContext.Session.Set("CurrentUser", ByteConvertHelper.Object2Bytes(user));
                    //��תϵͳ��ҳ
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //ModelState.AddModelError("", "�û����������");
                    ViewBag.ErrorInfo = "�û������������";
                    return View();
                }
            }

            //foreach (var item in ModelState.Values)
            //{
            //    if (item.Errors.Count > 0)
            //    {
            //        ViewBag.ErrorInfo = item.Errors[0].ErrorMessage;
            //        break;
            //    }
            //}

            ViewBag.ErrorInfo = ModelState.Values.First().Errors[0].ErrorMessage;

            return View(model);
        }
    }
}