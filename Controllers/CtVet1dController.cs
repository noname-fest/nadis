using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.DAL;
using nadis.Models;
using Dapper;
using nadis.tools;

namespace nadis.Controllers
{
    public class CtVet1dController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;
            
            var list = CtVet1dDAL.GetAll_CtVet1d(KIDro,Y,M);
            ViewBag.Page = "CtVet1d";
            ViewBag.RepList  = spDAL.ReportToToday();
            return View(list);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;

                CtVet1d tmp = CtVet1dDAL.GetByID_CtVet1d(id);
                if (tmp == null) return NotFound();

                tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                tmp.KIDtypDisplay = spDAL.KIDtypName(tmp.KIDtyp);

                ViewBag.RepMoList = spDAL.RepMO1YearList(Y,M);
                ViewBag.KIDdivList = spDAL.KIDdivList(KIDro);
                ViewBag.KIDdisList = spDAL.KIDdisList();
                ViewBag.KIDtypList = spDAL.KIDtypList();

                ViewBag.Page = "ctVet1d";
                return View(tmp);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind] CtVet1d tmp)
        {
            if (id == null)  return NotFound();
            if (ModelState.IsValid)
            {
                CtVet1dDAL.Update(tmp);
                return RedirectToAction("Index");
            }
            ViewBag.Page = "ctVet1d";
            return View(tmp);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;

                ViewBag.RepMoList = spDAL.RepMO1YearList(Y,M);
                ViewBag.KIDdivList = spDAL.KIDdivList(KIDro);
                ViewBag.KIDdisList = spDAL.KIDdisList();
                ViewBag.KIDtypList = spDAL.KIDtypList();
           
            CtVet1d tmp = new CtVet1d
            {
                KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                repMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                dtObs = DateTime.Today
            };

            ViewBag.Page = "CtVet1d";
            return View(tmp);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CtVet1d tmp)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Page = "CtVet1d";
                return View(tmp);
            }
            if (CtVet1dDAL.IsUniqueRecord(tmp)) CtVet1dDAL.Add(tmp);
            else
            {
                TempData["EM"] = "Такая запись уже существует";
            };
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet1d tmp = CtVet1dDAL.GetByID_CtVet1d(id);
            if (tmp == null)
            {
                return NotFound();
            }
            ViewBag.Page = "CtVet1d";
            return View(tmp);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVet1d(Guid id)
        {
            CtVet1dDAL.Delete(id);
            ViewBag.Page = "CtVet1d";
            return RedirectToAction("Index");
        }
    }
    
}