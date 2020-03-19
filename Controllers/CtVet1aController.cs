using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.Models;

namespace nadis.Controllers
{
    public class CtVet1aController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            _ = new List<CtVet1a>();
            List<CtVet1a> vet1aList = CtVet1aDAL.GetAll_CtVet1a(
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
            CtVet1a tmp = new CtVet1a
            {
                KIDro =  User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                RepMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            };
            return View(tmp);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CtVet1a tmpVet)
        {
            if (ModelState.IsValid)
            {
                CtVet1aDAL.AddCtVet1a(tmpVet);
                return RedirectToAction("Index");
            }
            return View(tmpVet);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            CtVet1a tmpVet1a = CtVet1aDAL.GetCtVet1aById(id);
            if(tmpVet1a == null) return NotFound();

            ViewBag.RepMoList  = spDAL.RepMO1YearList(tmpVet1a.RepMO.Year);
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList(); ViewBag.KIDdisList = spDAL.KIDdisList();

            return View(tmpVet1a);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,[Bind] CtVet1a objCtVet1a)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                CtVet1aDAL.UpdateCtVet1a(objCtVet1a);
                return RedirectToAction("Index");
            }
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
            CtVet1a tmpVet1a = CtVet1aDAL.GetCtVet1aById(id);
            if (tmpVet1a == null)
            {
                return NotFound();
            }
            return View(tmpVet1a);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet1a tmpVet1a = CtVet1aDAL.GetCtVet1aById(id);
            if (tmpVet1a == null)
            {
                return NotFound();
            }
            return View(tmpVet1a);
        }

        [Authorize]
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVet1a(Guid id)
        {
            CtVet1aDAL.DeleteCtVet1a(id);
            return RedirectToAction("Index");
        }
    }
}