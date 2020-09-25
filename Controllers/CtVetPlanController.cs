using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.DAL;
using nadis.Models;

namespace nadis.Controllers
{
    public class CtVetPlanController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            int reportDtYear = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int reportDtMonth = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;

            List<CtVetPlan> l = ctVetPlanDAL.GetAll_CtVetPlan(KIDro,reportDtYear).ToList();
            ViewBag.Page = "CtVetPlan";
            //ViewBag.RepList  = spDAL.ReportToToday();
            return View(l);
        }

        
        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            CtVetPlan tmp = ctVetPlanDAL.GetById_CtVetPlan(id);
            if(tmp==null) return NotFound();

            ViewBag.PlanYearList = spDAL.PlanYearList();
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList();
            ViewBag.KIDdisList = spDAL.KIDdisList();
            ViewBag.KIDtrtList = spDAL.KIDtrtList();
            ViewBag.KIDtestList = spDAL.testList();
            ViewBag.Page = "CtVetPlan";
            return View(tmp);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind] CtVetPlan tmp)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //if(BioPrepDAL.IsUniqueRecord(objBioPrep))
                ctVetPlanDAL.Update_CtVetPlan(tmp);
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVetPlan";
            return View(tmp);
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
        
            CtVetPlan tmp = new CtVetPlan()
            {
                KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                PlanYr = DateTime.Today.Year
            };

            ViewBag.PlanYearList = spDAL.PlanYearList();
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList();
            ViewBag.KIDdisList = spDAL.KIDdisList();
            ViewBag.KIDtrtList = spDAL.KIDtrtList();
            ViewBag.KIDtestList = spDAL.testList();

            ViewBag.Page = "CtVetPlan";
            return View(tmp);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CtVetPlan tmp)
        {
            if (ModelState.IsValid)
            {
                //проверка на существования аналогичной записи
                //if (CtVetPlan.IsUniqueRecord(tmp))
                //    CtVet1cDAL.Add_CtVet1c(tmp);
                //else
                //{
                //    TempData["EM"] = "Такая запись уже существует";
                    //return LocalRedirect("~/Home/Error");
                    //return RedirectToAction("Error");
                //};
                //------------------
                ctVetPlanDAL.Add_CtVetPlan(tmp);
                ViewBag.Page = "CtVetPlan";
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVetPlan";
            return View(tmp);
        }
    }
}