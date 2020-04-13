using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nadis.Models;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

namespace nadis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewData["UserName"]  = User.Identity.Name;
            ViewData["UserFullName"] = User.Claims.ToList().
                                        FirstOrDefault(x => x.Type == "UserFullName").Value;
            ViewData["KIDro"] = User.Claims.ToList().
                                        FirstOrDefault(x => x.Type == "KIDro").Value;
            ViewData["Role"] = User.Claims.ToList().
                                        FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            ViewData["reportDt"] =(new DateTime(Convert.ToInt32(User.Claims.ToList().
                                        FirstOrDefault(x => x.Type == "reportDtYear").Value),
                                   Convert.ToInt32(User.Claims.ToList().
                                        FirstOrDefault(x => x.Type == "reportDtMonth").Value), 1)).ToString("MMMM yyyy") ;
            ViewBag.CurrentL = CultureInfo.CurrentCulture.ToString();
            ViewBag.CurrentUIL = CultureInfo.CurrentUICulture.ToString();
            ViewBag.Page = "Home";
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            ViewBag.Page = "Privacy";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
