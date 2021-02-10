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

            //string PlanYFilter = Request.Query.FirstOrDefault(p => p.Key == "PlanY").Value.ToString();
            string divFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDdiv").Value.ToString();
            string trtFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDtrt").Value.ToString();
            string spcFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDspc").Value.ToString();
            string disFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDdis").Value.ToString();
            
            List<CtVetPlan> l = ctVetPlanDAL.GetAll_CtVetPlan(KIDro,DateTime.Today.Year.ToString(),
                                            divFilter,trtFilter,spcFilter,disFilter).ToList();
            
            ViewBag.KIDdivList = FilterTools.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value, divFilter);
            //ViewBag.PlanYearList = FilterTools.PlanYearList(PlanYFilter);
            ViewBag.PlanYearList = spDAL.PlanYearList();
            ViewBag.KIDtrtList = FilterTools.TrtList(trtFilter);
            ViewBag.KIDspcList = FilterTools.KIDspcList(spcFilter);
            ViewBag.KIDdisList = FilterTools.KIDdisList(disFilter);
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
                //tmp.nPlan = tmp.nPlan_I+tmp.nPlan_II+tmp.nPlan_III+tmp.nPlan_IV;
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
                PlanYr = DateTime.Today.Year,
                tfemage1_I = 0,
                tfemage2_I = 0,
                tfemage1_II = 0,
                tfemage2_II = 0,
                tfemage1_III = 0,
                tfemage2_III = 0,
                tfemage1_IV = 0,
                tfemage2_IV = 0,
                nPlan_I = 0,
                nPlan_II = 0,
                nPlan_III = 0,
                nPlan_IV = 0,
                nPlan = 0
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
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVetPlan tmp = ctVetPlanDAL.GetById_CtVetPlan(id);
            if (tmp == null)
            {
                return NotFound();
            }
            ViewBag.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
            ViewBag.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
            ViewBag.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
            ViewBag.KIDtrtDisplay = spDAL.KIDtrtName(tmp.KIDtrt);
            ViewBag.Page = "BioPrep";
            return View(tmp);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVetPlan(Guid id)
        {
            ctVetPlanDAL.Delete_CtVetPlan(id);
            ViewBag.Page = "CtVetPlan";
            return RedirectToAction("Index");
        }
    }
}