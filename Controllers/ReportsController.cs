using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.DAL.nadis;
using System.Globalization;
using System.Linq;

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
        public IActionResult AnalizPEM()
        {
            ViewBag.Page = "AnalizPEM";
            ViewBag.RepList  = spDAL.ReportToToday();
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