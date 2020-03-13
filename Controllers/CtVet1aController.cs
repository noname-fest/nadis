using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using nadis.Models;

namespace nadis.Controllers
{
    public class CtVet1aController : Controller
    {
        readonly CtVet1aDAL vet1aDAL = new CtVet1aDAL();
        public IActionResult Index()
        {
            _ = new List<CtVet1a>();
            List<CtVet1a> vet1aList = vet1aDAL.GetAllCtVet1a("RD02205", "01/12/2019").ToList();
            return View(vet1aList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CtVet1a tmpVet)
        {
            if (ModelState.IsValid)
            {
                vet1aDAL.AddCtVet1a(tmpVet);
                return RedirectToAction("Index");
            }
            return View(tmpVet);
        }

       
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet1a tmpVet1a = vet1aDAL.GetCtVet1aById(id);
            if(tmpVet1a == null)
            {
                return NotFound();
            }

            return View(tmpVet1a);
        }

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
                vet1aDAL.UpdateCtVet1a(objCtVet1a);
                return RedirectToAction("Index");
            }
            ViewBag
            return View(vet1aDAL);
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            } 
            CtVet1a tmpVet1a = vet1aDAL.GetCtVet1aById(id);
            if (tmpVet1a == null)
            {
                return NotFound();
            }
            return View(tmpVet1a);
        }

        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet1a tmpVet1a = vet1aDAL.GetCtVet1aById(id);
            if (tmpVet1a == null)
            {
                return NotFound();
            }
            return View(tmpVet1a);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVet1a(Guid id)
        {
            vet1aDAL.DeleteCtVet1a(id);
            return RedirectToAction("Index");
        }
    }
}