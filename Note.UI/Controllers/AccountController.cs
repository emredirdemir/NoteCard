using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Note.UI.Identity;
using Note.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.UI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            var result =_signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true,false);

            return View();
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var user = new User()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.UserName,
                Email = registerModel.EMail
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            return RedirectToAction("Index", "Note");
        }

    }
}
