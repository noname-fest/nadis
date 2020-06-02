using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.DAL.nadis;
using nadis.Models;

namespace nadis.Controllers
{
    public class CtVet1bController : Controller
    {

        [Authorize]
        public IActionResult Index()
        {
            _ = new List<CtVet1b>();
            int reportDtYear = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int reportDtMonth = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);

            List<CtVet1b> vet1aList = CtVet1bDAL.GetAll_CtVet1b(
                                                User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                                                reportDtYear,
                                                reportDtMonth
                                                ).ToList();
            ViewBag.Page = "CtVet1b";
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
            ViewBag.testList   = spDAL.testList();
            CtVet1b tmp = new CtVet1b
            {
                KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                repMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                dtObs = DateTime.Now
            };
            ViewBag.Page = "CtVet1b";
            return View(tmp);
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CtVet1b tmpVet)
        {
            if (ModelState.IsValid)
            {
                //проверка на существования аналогичной записи
                if (CtVet1bDAL.IsUniqueRecord(tmpVet)) CtVet1bDAL.Add_CtVet1b(tmpVet);
                else
                {
                    TempData["EM"] = "Такая запись уже существует";
                };
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet1b";
            return View(tmpVet);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            CtVet1b tmpVet1b = CtVet1bDAL.GetCtVet1bById(id);
            if (tmpVet1b == null) return NotFound();

            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);

            ViewBag.RepMoList  = spDAL.RepMO1YearList(Y,M);
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList();
            ViewBag.KIDdisList = spDAL.KIDdisList();
            ViewBag.testList   = spDAL.testList();

            ViewBag.Page = "CtVet1b";
            return View(tmpVet1b);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind] CtVet1b objCtVet1b)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                CtVet1bDAL.UpdateCtVet1b(objCtVet1b);
                ViewBag.Page = "CtVet1b";
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet1b";
            return View(objCtVet1b);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet1b tmpVet1b = CtVet1bDAL.GetCtVet1bById(id);
            if (tmpVet1b == null)
            {
                return NotFound();
            }
            ViewBag.Page = "CtVet1b";
            return View(tmpVet1b);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet1b tmpVet1a = CtVet1bDAL.GetCtVet1bById(id);
            if (tmpVet1a == null)
            {
                return NotFound();
            }
            ViewBag.Page = "CtVet1b";
            return View(tmpVet1a);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVet1a(Guid id)
        {
            CtVet1bDAL.DeleteCtVet1b(id);
            ViewBag.Page = "CtVet1b";
            return RedirectToAction("Index");
        }

    }
}