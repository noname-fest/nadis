using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using nadis.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;

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
            if(loginModel != null)
            if (ModelState.IsValid)
            {
                var user = await _userContext.Users
                    .FirstOrDefaultAsync(u =>
                        u.username == loginModel.username &&
                        u.userpassword == loginModel.userpassword).ConfigureAwait(false);

                if (user != null)
                {
                    //if()
                    await Authenticate(loginModel.username).ConfigureAwait(false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин или пароль");
                }
            }
            return View(loginModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
            {
                return View();
            }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (registerModel != null)
            if (ModelState.IsValid)
            {
                var user = await _userContext.Users
                    .FirstOrDefaultAsync(u => u.username == registerModel.username).ConfigureAwait(false);
                if (user == null)
                {
                    _userContext.Users.Add(new User
                    {
                        username = registerModel.username,
                        userpassword = registerModel.userpassword,
                        KIDro = registerModel.KIDro,
                        Role  = registerModel.Role,
                        reportDt = new DateTime(registerModel.repDtYear,registerModel.repDtMont,1) 
                    });
                    await _userContext.SaveChangesAsync().ConfigureAwait(false);

                    await Authenticate(registerModel.username).ConfigureAwait(false);
                    return RedirectToAction("Index", "Home");
                } else
                {
                    ModelState.AddModelError("", "Некорректные логин или пароль");
                }
            }
            return View(registerModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string username)
        {
            var U = await _userContext.Users
                    .FirstOrDefaultAsync(u => u.username == username).ConfigureAwait(false);
            string roleP;
            string KIDro;
            DateTime rDt; 

            if(U.Role!=null) roleP = U.Role; else roleP ="";
            if(U.KIDro!=null) KIDro =U.KIDro; else KIDro = "";
            if(U.reportDt!=null) rDt = U.reportDt; else rDt = new DateTime(DateTime.Today.Year,DateTime.Today.Month-1,1);
            int Y = rDt.Year;
            int M = rDt.Month;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roleP),
                new Claim("KIDro", KIDro),
                new Claim("Role", roleP),
                new Claim("reportDtYear", Y.ToString()),
                new Claim("reportDtMonth", M.ToString())
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)).ConfigureAwait(false);
        }
     }
}
