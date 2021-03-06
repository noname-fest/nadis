using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.DAL;
using nadis.Models;

namespace nadis.Controllers
{
    public class CtVet1cController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            int reportDtYear = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int reportDtMonth = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);

            string idRo = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;

            string repMoFilter = Request.Query.FirstOrDefault(p => p.Key == "repMO").Value.ToString();
            string divFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDdiv").Value.ToString();
            string spcFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDspc").Value.ToString();
            string disFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDdis").Value.ToString();

            List<CtVet1c> CtVet1cList = CtVet1cDAL.GetAll_CtVet1cF(
                                                idRo,
                                                reportDtYear,
                                                reportDtMonth,
                                                repMoFilter,
                                                divFilter,
                                                spcFilter,
                                                disFilter
                                                ).ToList();
            ViewBag.Page = "CtVet1c";
            ViewBag.RepList  = spDAL.ReportToToday();

            ViewBag.repMOList = FilterTools.repMOList(  reportDtYear,
                                                        reportDtMonth,
                                                        repMoFilter);
            ViewBag.KIDdivList = FilterTools.KIDdivList(idRo,divFilter);
            ViewBag.KIDspcList = FilterTools.KIDspcList(spcFilter);
            ViewBag.KIDdisList = FilterTools.KIDdisList(disFilter);

            return View(CtVet1cList);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            CtVet1c tmp = CtVet1cDAL.GetById_CtVet1c(id);
            if (tmp == null) return NotFound();

            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);

            ViewBag.RepMoList  = spDAL.RepMO1YearList(Y,M);
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList();
            ViewBag.KIDdisList = spDAL.KIDdisList();
            ViewBag.KIDtrtList = spDAL.KIDtrtList();
            ViewBag.Page = "CtVet1c";
            return View(tmp);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind] CtVet1c objCtVet1c)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //if(BioPrepDAL.IsUniqueRecord(objBioPrep))
                    CtVet1cDAL.Update_CtVet1c(objCtVet1c);
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet1c";
            return View(objCtVet1c);
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
            ViewBag.KIDtrtList = spDAL.KIDtrtList();

            CtVet1c tmp = new CtVet1c
            {
                KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                RepMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                dtObs = DateTime.Today
            };

            ViewBag.Page = "CtVet1c";
            return View(tmp);

        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CtVet1c tmp)
        {
            if (ModelState.IsValid)
            {
                //проверка на существования аналогичной записи
                if (CtVet1cDAL.IsUniqueRecord(tmp))
                    CtVet1cDAL.Add_CtVet1c(tmp);
                else
                {
                    TempData["EM"] = "Такая запись уже существует";
                    //return LocalRedirect("~/Home/Error");
                    //return RedirectToAction("Error");
                };
                ViewBag.Page = "CtVet1c";
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet1c";
            return View(tmp);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet1c tmp = CtVet1cDAL.GetById_CtVet1c(id);
            if (tmp == null)
            {
                return NotFound();
            }
            ViewBag.Page = "CtVet1c";
            return View(tmp);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVet1c(Guid id)
        {
            CtVet1cDAL.Delete_CtVet1c(id);
            ViewBag.Page = "CtVet1c";
            return RedirectToAction("Index");
        }
    }
}