using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearnCore.Application.UserApp;

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
            var user = userService.CheckUser("admin", "123456");
            return View();
        }
    }
}