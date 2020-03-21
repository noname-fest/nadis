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
    public class BioPrepController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            _ = new List<BioPrep>();
            List<BioPrep> BioPrepList = BioPrepDAL.GetAll_BioPrep(
                                                User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value).
                                                ToList();
            return View(BioPrepList);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            //User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value
            ViewBag.RepMoList = spDAL.RepMO1YearList(DateTime.Today.Year);
            ViewBag.VetPrepList = spDAL.VetPrepList();
            ViewBag.EdIzmList = spDAL.EdIzmList();

            BioPrep tmp = new BioPrep
            {
                KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                RepMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            };

            return View(tmp);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] BioPrep tmp)
        {
            if (ModelState.IsValid)
            {
                //проверка на существования аналогичной записи
                if (BioPrepDAL.IsUniqueRecord(tmp))
                    BioPrepDAL.Add_BioPrep(tmp);
                else
                {
                    return NotFound();
                };
                return RedirectToAction("Index");
            }
            return View(tmp);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            BioPrep tmp = BioPrepDAL.GetById_BioPrep(id);
            if (tmp == null) return NotFound();

            ViewBag.RepMoList = spDAL.RepMO1YearList(DateTime.Today.Year);
            ViewBag.VetPrepList = spDAL.VetPrepList();
            ViewBag.EdIzmList = spDAL.EdIzmList();

            return View(tmp);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind] BioPrep objBioPrep)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //if(BioPrepDAL.IsUniqueRecord(objBioPrep))
                    BioPrepDAL.Update_BioPrep(objBioPrep);
                return RedirectToAction("Index");
            }
            return View(objBioPrep);
        }

    }

}