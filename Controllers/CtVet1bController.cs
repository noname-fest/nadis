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
        readonly CtVet1bDAL vet1bDAL = new CtVet1bDAL();
        [Authorize]
        public IActionResult Index()
        {
            _ = new List<CtVet1b>();
            List<CtVet1b> vet1aList = vet1bDAL.GetAllCtVet1b(
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
                dtObs = new DateTime?(DateTime.Now)
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
                vet1bDAL.AddCtVet1b(tmpVet);
                return RedirectToAction("Index");
            }
            return View(tmpVet);
        }

    }
}