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
using System.Linq;
using nadis.tools;
using System.Data.SqlClient;
using Dapper;
using nadis.DAL.nadis;

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
        public IActionResult Login() => View();

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
        //[ValidateAntiForgeryToken]
        public  IActionResult UsersEdit()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                var tmpList = _conn.Query<User>("SELECT * FROM Users");
                _conn.Close();
                return View(tmpList);
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id == null) return NotFound();
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                var usr = _conn.QueryFirst<User>("SELECT * FROM Users WHERE Id=@idd", new {idd = id});
                if(usr == null) return NotFound();
                ViewBag.KIDroList = spDAL.KIDroList();
                return View(usr);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]  

        public IActionResult Edit(int id, [Bind] User objUsr)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //if(BioPrepDAL.IsUniqueRecord(objBioPrep))
                    //BioPrepDAL.Update_BioPrep(objBioPrep);
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];

                using(SqlConnection _conn = new SqlConnection(connectionString))
                    {
                        _conn.Execute("UPDATE Users SET KIDro=@_kidro,"+
                        "username=@_username, UserFullname=@_UserFullname,"+
                        "userpassword=@_userpassword, Role=@_Role, reportDt=@_reportDt "+
                        "WHERE Id=@_Id",
                        new {
                            _Id = objUsr.Id,
                            _kidro = objUsr.KIDro,
                            _username = objUsr.username,
                            _UserFullname = objUsr.UserFullname,
                            _userpassword = objUsr.userpassword,
                            _Role = objUsr.Role,
                            _reportDt = objUsr.reportDt
                        } );   
                        return RedirectToAction("UsersEdit");
                    }
            }
            return View(objUsr);
        }
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
            string usrFullName;
            string roleP;
            string KIDro;
            DateTime rDt; 

            if(U.Role!=null) roleP = U.Role; else roleP ="";
            if(U.KIDro!=null) KIDro =U.KIDro; else KIDro = "";
            if(U.reportDt!=null) rDt = U.reportDt; else rDt = new DateTime(DateTime.Today.Year,DateTime.Today.Month-1,1);
            if(U.UserFullname!=null) usrFullName = U.UserFullname; else usrFullName = "";

            int Y = rDt.Year;
            int M = rDt.Month;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roleP),
                new Claim("KIDro", KIDro),
                new Claim("Role", roleP),
                new Claim("reportDtYear", Y.ToString()),
                new Claim("reportDtMonth", M.ToString()),
                new Claim("UserFullName", usrFullName)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)).ConfigureAwait(false);
        }
     }
}
