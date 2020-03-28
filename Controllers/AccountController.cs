using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using nadis.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using nadis.tools;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib;
using nadis.DAL.nadis;

namespace AuthSample.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserContext _userContext;

        //public AccountController(UserContext userContext)
        //{
        //    _userContext = userContext;
        //}

        [HttpGet]
        public IActionResult Login() 
        {
            ViewBag.Page = "Home";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if(loginModel != null)
            if (ModelState.IsValid)
            {
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];

                using(SqlConnection _conn = new SqlConnection(connectionString))
                {
                    string q = "SELECT * FROM Users WHERE username=@usr_name and userpassword=@usr_pwd";
                    var param = new
                        {
                          usr_name = loginModel.username,
                          usr_pwd  = loginModel.userpassword
                        };
                    var user = _conn.QueryFirstOrDefault<User>(q,param);
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
            }
            ViewBag.Page = "Home";
            return View(loginModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public  IActionResult UsersEdit()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                var tmpList = _conn.Query<User>("SELECT * FROM Users");
                _conn.Close();
                ViewBag.Page = "Home";
                return View(tmpList);
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                var usr = _conn.QueryFirst<User>("SELECT * FROM Users WHERE Id=@idd", new {idd = id});
                if(usr == null) return NotFound();
                ViewBag.KIDroList = spDAL.KIDroList();
                ViewBag.Page = "Home";
                return View(usr);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Edit(int id, [Bind] User objUsr)
        {
            if (ModelState.IsValid)
            {
                //if(BioPrepDAL.IsUniqueRecord(objBioPrep))
                    //BioPrepDAL.Update_BioPrep(objBioPrep);
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];

                using(var _conn = new SqlConnection(connectionString))
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
            ViewBag.Page = "Home";
            return View(objUsr);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int Id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                var usr = _conn.QueryFirst<User>("SELECT * FROM Users WHERE Id=@idd", new {idd = Id});
                if(usr == null) return NotFound();
                ViewBag.Page = "Home";
                return View(usr);
            }
        }


        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(int Id)
        {
            //BioPrepDAL.Delete_BioPrep(id);
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
                _conn.Execute("DELETE FROM Users WHERE Id=@idd", new {idd = Id});
            ViewBag.Page = "Home";
            return RedirectToAction("UsersEdit");
        }        


        [Authorize(Roles = "Admin")]
        public IActionResult Register()
            {
                ViewBag.KIDroList = spDAL.KIDroList();
                ViewBag.Page = "Home";
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
                //var user = await _userContext.Users
                //    .FirstOrDefaultAsync(u => u.username == registerModel.username).ConfigureAwait(false);
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];

                SqlConnection _conn = new SqlConnection(connectionString);
                int usr_count = _conn.QueryFirst<int>("SELECT COUNT(*) FROM Users WHERE username=@usr_name",
                                                    new{usr_name = registerModel.username});
                if (usr_count == 0)
                {
                        /*
                        _userContext.Users.Add(new User
                        {
                            username = registerModel.username,
                            userpassword = registerModel.userpassword,
                            KIDro = registerModel.KIDro,
                            Role  = registerModel.Role,
                            reportDt = new DateTime(registerModel.reportDt.Year,registerModel.reportDt.Month,1) 
                        });
                        await _userContext.SaveChangesAsync().ConfigureAwait(false);
                        await Authenticate(registerModel.username).ConfigureAwait(false);
                        */
                        var param = new
                        {
                            UsrFN = registerModel.UserFullname,
                            usrname = registerModel.username,
                            usrpwd = registerModel.userpassword,
                            @idro = registerModel.KIDro,
                            @rr = registerModel.Role,
                            @rDt = registerModel.reportDt
                        };
                        _conn.Execute("INSERT INTO Users (UserFullname,username,userpassword,KIDro,Role,reportDt) "+
                            "VALUES (@UsrFN,@usrname,@usrpwd,@idro,@rr,@rDt)" ,
                            param);
                    _conn.Close();
                    return RedirectToAction("Index", "Home");
                } else
                {
                    ModelState.AddModelError("", "Некорректные логин или пароль");
                }
                _conn.Close();
            }
            ViewBag.Page = "Home";
            return View(registerModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string username)
        {
            //var U = await _userContext.Users
            //        .FirstOrDefaultAsync(u => u.username == username).ConfigureAwait(false);

            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            SqlConnection _conn = new SqlConnection(connectionString);
            User usr = _conn.QueryFirst<User>("SELECT * FROM Users WHERE username=@usr_name",new{usr_name = username});
            string usrFullName;
            string roleP;
            string KIDro;
            DateTime rDt; 

            if(usr.Role!=null) roleP = usr.Role; else roleP ="";
            if(usr.KIDro!=null) KIDro =usr.KIDro; else KIDro = "";
            if(usr.reportDt!=null) rDt = usr.reportDt; else rDt = new DateTime(DateTime.Today.Year,DateTime.Today.Month-1,1);
            if(usr.UserFullname!=null) usrFullName = usr.UserFullname; else usrFullName = "";

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
