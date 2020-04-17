using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        [HttpPost]
        public bool IsUniqRecord(Guid _id,string IDro, DateTime repMo, string VetPrep, string e) 
            => BioPrepDAL.IsUniqueRecord(_id,IDro, repMo, VetPrep, e);

        [Authorize]
        [HttpPost]
        public long? GetByloById(string IDro, DateTime repMo, string VetPrep, string e) 
            => BioPrepDAL.GetByloById(IDro, repMo, VetPrep, e);

        [Authorize]
        public IActionResult Index()
        {
            _ = new List<BioPrep>();
            int reportDtYear = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int reportDtMonth = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            List<BioPrep> BioPrepList = BioPrepDAL.GetAll_BioPrep(
                                                User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                                                reportDtYear,
                                                reportDtMonth
                                                ).ToList();
            ViewBag.Page = "BioPrep";
            return View(BioPrepList);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            int reportDtYear = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int reportDtMonth = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            ViewBag.RepMoList = spDAL.RepMO1YearList(reportDtYear,reportDtMonth);
            ViewBag.VetPrepList = spDAL.VetPrepList();
            ViewBag.EdIzmList = spDAL.EdIzmList();

            BioPrep tmp = new BioPrep
            {
                KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                RepMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            };
            ViewBag.Page = "BioPrep";
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
                    TempData["EM"] = "Такая запись уже существует";
                    //return LocalRedirect("~/Home/Error");
                    //return RedirectToAction("Error");
                };
                return RedirectToAction("Index");
            }
            ViewBag.Page = "BioPrep";
            return View(tmp);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            BioPrep tmp = BioPrepDAL.GetById_BioPrep(id);
            if (tmp == null) return NotFound();

            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            ViewBag.RepMoList = spDAL.RepMO1YearList(Y,M);
            ViewBag.VetPrepList = spDAL.VetPrepList();
            ViewBag.EdIzmList = spDAL.EdIzmList();

            ViewBag.Page = "BioPrep";
            //if(tmp.ByloPredMonth!=tmp.Bylo)
            //    TempData["EM"] = "Остаток на нач. месяца не совпадает с введенными ранее данными";
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
            ViewBag.Page = "BioPrep";
            return View(objBioPrep);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BioPrep tmp = BioPrepDAL.GetById_BioPrep(id);
            if (tmp == null)
            {
                return NotFound();
            }
            ViewBag.Page = "BioPrep";
            return View(tmp);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBioPrep(Guid id)
        {
            BioPrepDAL.Delete_BioPrep(id);
            ViewBag.Page = "BioPrep";
            return RedirectToAction("Index");
        }

    }

}