using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.Models;
using nadis.DAL.nadis;
using FastReport.Web;
using FastReport.Data;
using FastReport.Utils;
using System.IO;
using FastReport.Export.PdfSimple;

namespace nadis.Controllers
{
    public class CtVet1aController : Controller
    {
        [Authorize]
        public IActionResult GetReportInPdf(string dt)
        {
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            WebReport webR = new WebReport();

            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.Report.Dictionary.Connections.Add(_con);
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path +  "\\FastReports\\ctVet1a-v2.frx";
 
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            
            webR.Report.Load(_path);
            webR.Report.SetParameterValue("yyyy",y.ToString());
            webR.Report.SetParameterValue("mm",m.ToString());
            webR.Report.SetParameterValue("idro",
                User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            
            webR.Report.Prepare();
            using (MemoryStream ms = new MemoryStream())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(webR.Report, ms);
                ms.Flush();
                return File(ms.ToArray(),
                            "application/pdf",
                            Path.GetFileNameWithoutExtension("ctVet1a-v2") + ".pdf");
            }              
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetReport(string dt) //int m
        {
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            Reports webR = new Reports();
            webR.report = new WebReport();

            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.report.Report.Dictionary.Connections.Add(_con);
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path +  "\\FastReports\\ctVet1a-v2.frx";
            
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            webR.report.Report.Load(_path);
            webR.report.Report.SetParameterValue("yyyy",y);//DateTime.Today.Year.ToString());//webR.dt.Year.ToString());
            webR.report.Report.SetParameterValue("mm",m);//m.ToString());//webR.dt.Month.ToString());
            webR.report.Report.SetParameterValue("idro",
                User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            //webR.report.Report.Prepare();
            //webR.m = m;
            webR.dt = dt;

            return View(webR);
        }

        [Authorize]
        public IActionResult Index()
        {
            /*
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];
                using(SqlConnection _conn = new SqlConnection(connectionString))
                {
                    _conn.Insert()
                }
            */
            //if(!ViewBag.Filter){};
            _ = new List<CtVet1a>();
            int reportDtYear = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int reportDtMonth = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);

            List<CtVet1a> vet1aList = CtVet1aDAL.GetAll_CtVet1a(
                                                User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                                                reportDtYear,
                                                reportDtMonth
                                                ).ToList();
            ViewBag.Page = "CtVet1a";
            ViewBag.RepList  = spDAL.ReportToToday();
            return View(vet1aList);
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);

            ViewBag.RepMoList  = spDAL.RepMO1YearList(Y,M);
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList();
            ViewBag.KIDdisList = spDAL.KIDdisList();
            
            CtVet1a tmp = new CtVet1a
            {
                KIDro =  User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                repMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            };
            ViewBag.Page = "CtVet1a";
            return View(tmp);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CtVet1a tmpVet)
        {
            if (ModelState.IsValid)
            {
                //проверка на существования аналогичной записи
                if(CtVet1aDAL.IsUniqueRecord(tmpVet)) 
                        CtVet1aDAL.Add_CtVet1a(tmpVet);
                    else
                    {
                        TempData["EM"] = "Такая запись уже существует";
                        ModelState.AddModelError("","Такая запись уже существует");
                        //return NotFound();
                    };
                ViewBag.Page = "CtVet1a";
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet1a";
            return View(tmpVet);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            CtVet1a tmpVet1a = CtVet1aDAL.Get_CtVet1a_ById(id);
            if(tmpVet1a == null) return NotFound();
 
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);

            ViewBag.RepMoList  = spDAL.RepMO1YearList(Y,M);
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList(); ViewBag.KIDdisList = spDAL.KIDdisList();
            ViewBag.Page = "CtVet1a";
            return View(tmpVet1a);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,[Bind] CtVet1a objCtVet1a)
        {
            if ((id == null)||(CtVet1aDAL.IsUniqueRecord(objCtVet1a)))
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                CtVet1aDAL.UpdateCtVet1a(objCtVet1a);
                ViewBag.Page = "CtVet1a";
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet1a";
            return View(objCtVet1a);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            } 
            CtVet1a tmpVet1a = CtVet1aDAL.Get_CtVet1a_ById(id);
            if (tmpVet1a == null)
            {
                return NotFound();
            }
            ViewBag.Page = "CtVet1a";
            return View(tmpVet1a);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet1a tmpVet1a = CtVet1aDAL.Get_CtVet1a_ById(id);
            if (tmpVet1a == null)
            {
                return NotFound();
            }
            ViewBag.Page = "CtVet1a";
            return View(tmpVet1a);
        }

        [Authorize]
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVet1a(Guid id)
        {
            CtVet1aDAL.Delete_CtVet1a(id);
            ViewBag.Page = "CtVet1a";
            return RedirectToAction("Index");
        }
    }
}