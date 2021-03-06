﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnCore.MVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "用户名不能为空.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空.")]
        [MinLength(6, ErrorMessage = "密码长度在6位以上")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
