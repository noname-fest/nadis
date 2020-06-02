using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.Models;
using nadis.DAL.nadis;
using nadis.DAL;

namespace nadis.Controllers
{
    public class CtVet2Controller : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;
            IEnumerable<CtVet2> list = CtVet2DAL.GetAll(KIDro,Y,M);
            ViewBag.Page = "CtVet2";
            ViewBag.RepList  = spDAL.ReportToToday();
            return View(list);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;

            ViewBag.repPerList = CtVet2DAL.RepPerListList(Y,M);
            ViewBag.KIDdivList = spDAL.KIDdivList(KIDro);
            ViewBag.KIDspcList = spDAL.KIDspcList();
            ViewBag.KIDdtpList = spDAL.KIDdtpList();

            string pg;
            if(DateTime.Today.Month>6) pg="2"; else pg ="1";
            CtVet2 tmp = new CtVet2
            {
                KIDro =  User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                repPer =  DateTime.Today.Year+"/"+pg
                //new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            };
            ViewBag.Page = "CtVet2";
            return View(tmp);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CtVet2 tmp)
        {
            if (ModelState.IsValid)
            {
                if (CtVet2DAL.IsUniqueRecord(tmp))
                    CtVet2DAL.Add(tmp);
                else
                {
                    TempData["EM"] = "Такая запись уже существует";
                };
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet2";
            return View(tmp);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            CtVet2 tmp = CtVet2DAL.GetById(id);
            if (tmp == null) return NotFound();

            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;

            ViewBag.repPerList = CtVet2DAL.RepPerListList(Y,M);
            ViewBag.KIDdivList = spDAL.KIDdivList(KIDro);
            ViewBag.KIDspcList = spDAL.KIDspcList();
            ViewBag.KIDdtpList = spDAL.KIDdtpList();

            ViewBag.Page = "CtVet2";
            return View(tmp);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind] CtVet2 tmp)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //if(BioPrepDAL.IsUniqueRecord(objBioPrep))
                CtVet2DAL.Update(tmp);
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet2";
            return View(tmp);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet2 tmp = CtVet2DAL.GetById(id);
            if (tmp == null)
            {
                return NotFound();
            }
            ViewBag.Page = "CtVet2";
            return View(tmp);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVet2(Guid id)
        {
            CtVet2DAL.Delete(id);
            ViewBag.Page = "CtVet2";
            return RedirectToAction("Index");
        }




    }
}