using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthSample.Models;
using AuthSample.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthSample.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _userContext;

        public AccountController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userContext.Users
                    .FirstOrDefaultAsync(u =>
                        u.username == loginModel.username &&
                        u.userpassword == loginModel.userpassword);

                if (user != null)
                {
                    //if()
                    await Authenticate(loginModel.username, user.KIDdiv);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин или пароль");
                }
            }

            return View(loginModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userContext.Users
                    .FirstOrDefaultAsync(u => u.username == registerModel.username);

                if (user == null)
                {
                    _userContext.Users.Add(new User
                    {
                        username = registerModel.username,
                        userpassword = registerModel.userpassword,
                        KIDdiv = registerModel.KIDdivCode
                    });
                    await _userContext.SaveChangesAsync();

                    await Authenticate(registerModel.username,registerModel.KIDdivCode);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин или пароль");
                }
            }

            return View(registerModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string email, string KIDdiv)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                new Claim("KIDdiv", KIDdiv),
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
     }
}
