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
        //readonly CtVet1bDAL vet1bDAL = new CtVet1bDAL();
        [Authorize]
        public IActionResult Index()
        {
            _ = new List<CtVet1b>();
            List<CtVet1b> vet1aList = CtVet1bDAL.GetAll_CtVet1b(
                                                User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value).
                                                ToList();
            return View(vet1aList);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.RepMoList  = spDAL.RepMO1YearList(DateTime.Today.Year);
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList();
            ViewBag.KIDdisList = spDAL.KIDdisList();
            ViewBag.testList   = spDAL.testList();
            CtVet1b tmp = new CtVet1b
            {
                KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                RepMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                dtObs = DateTime.Now
            };
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
                    return NotFound();
                };
                return RedirectToAction("Index");
            }
            return View(tmpVet);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            CtVet1b tmpVet1b = CtVet1bDAL.GetCtVet1bById(id);
            if (tmpVet1b == null) return NotFound();

            ViewBag.RepMoList  = spDAL.RepMO1YearList(tmpVet1b.RepMO.Year);
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList();
            ViewBag.KIDdisList = spDAL.KIDdisList();
            ViewBag.testList   = spDAL.testList();

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
                return RedirectToAction("Index");
            }
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
            return View(tmpVet1a);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVet1a(Guid id)
        {
            CtVet1bDAL.DeleteCtVet1b(id);
            return RedirectToAction("Index");
        }

    }
}