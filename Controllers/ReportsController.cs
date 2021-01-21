using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.DAL;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace nadis.Controllers
{
    public class ReportsController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetReportInPdf(string dtR, string ReportName)
        {
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;
            return File(ReportGenerator.ExportToPDF(dtR,ReportName,KIDro),"application/pdf",
                            ReportName + ".pdf");

        }

        [Authorize]
        [HttpGet]
        public IActionResult GetReportInHTML(string dtR, string ReportName)
        {
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;
            return File(ReportGenerator.ExportToHTML(dtR,ReportName,KIDro),"application/html",
                            ReportName + ".html");

        }

        [Authorize]
        [HttpGet]
        public IActionResult GetReportAA(string ReportName, string dt, string aa, string tip) //int m
        {
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;
            //string l = User.Claims.ToList().FirstOrDefault(x => x.Type == "lang").Value;
            //string lang = CultureInfo.CurrentCulture.Name;
            string lang = "ru";
            switch(tip)
            {
                case "inc" : 
                    return View(ReportGenerator.
                                GetReport(dt+"-"+aa,
                                    ReportName+"_inc-"+lang,
                                    KIDro));
                case "month" :
                    return View(ReportGenerator.
                                GetReport(dt+"-"+aa,
                                    ReportName+"-" + lang,
                                    KIDro));
                default :
                    return(null);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetReport(string ReportName, string dt, string tip) //int m
        {
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;
            //string l = User.Claims.ToList().FirstOrDefault(x => x.Type == "lang").Value;
            //string lang = CultureInfo.CurrentCulture.Name;
            string lang = "ru";
            switch(tip)
            {
                case "inc" : 
                    return View(ReportGenerator.
                                GetReport(dt,
                                    ReportName+"_inc-"+lang,
                                    KIDro));
                case "month" :
                    return View(ReportGenerator.
                                GetReport(dt,
                                    ReportName+"-" + lang,
                                    KIDro));
                default :
                    return(null);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetReportCA(string dtR, 
                                        string RaionID, 
                                        string OblID,
                                        string r, 
                                        string Tip)
        {
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;
            string Role  = User.Claims.ToList().
                                        FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            //if (Role!="Admin" || Role!="CA")  return null;
            string lang = "ru";
            switch(Tip)
            {
                case "Inc" : 
                    return View(ReportGenerator.
                                GetReportCA(dtR,
                                    r+"-Inc-"+lang,
                                    RaionID,
                                    OblID));
                case "month" :
                    return View(ReportGenerator.
                                GetReportCA(dtR,
                                    r+"-" + lang,
                                    RaionID,
                                    OblID));
                default :
                    return View();
            } 
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetReportRaznarydka(string dtR, 
                                        string RaionID, 
                                        string OblID,
                                        string Rr, 
                                        string Tip)
        {
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;
            string Role  = User.Claims.ToList().
                                        FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            //if (Role!="Admin" || Role!="CA")  return null;
            //string lang = "ru";
            switch(Tip)
            {
                case "Inc" : 
                    return View(ReportGenerator.
                                GetReportRaznaryadka(dtR,
                                    Rr,
                                    RaionID,
                                    OblID));
                case "month" :
                    return View(ReportGenerator.
                                GetReportRaznaryadka(dtR,
                                    Rr,
                                    RaionID,
                                    OblID));
                default :
                    return View();
            } 
        }

        [Authorize]
        [HttpGet]
        public IActionResult AnalizPEM()
        {
            ViewBag.Page = "AnalizPEM";
            ViewBag.RepList  = spDAL.ReportToToday();
            ViewBag.RepListAdm = spDAL.ReportToTodayAdm();
            ViewBag.RaionList = spDAL.RaionsList();
            ViewBag.OblList = spDAL.OblastList();
            ViewBag.ReportsList = spDAL.ReportsList();
            ViewBag.RaznarydkaList = spDAL.RaznaryankaReportsList();
            string Role  = User.Claims.ToList().
                                        FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            //View("Role") = Role;
            ViewData["Role"] = Role;
            return View();

            //string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;
            //string lang = CultureInfo.CurrentCulture.Name;
            //return View(ReportGenerator.
            //                    GetReport(dt,
            //                        "PEM-" + lang,
            //                        KIDro));
        }

    }
    
}