﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.Models;
using nadis.DAL;

namespace nadis.Controllers
{
    public class CtVet1aController : Controller
    {

        [Authorize]
        public IActionResult Index()
        {
            _ = new List<CtVet1a>();
            int reportDtYear = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int reportDtMonth = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            string idRo = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;

            string repMoFilter = Request.Query.FirstOrDefault(p => p.Key == "repMO").Value.ToString();
            string divFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDdiv").Value.ToString();
            string spcFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDspc").Value.ToString();
            string disFilter = Request.Query.FirstOrDefault(p => p.Key == "KIDdis").Value.ToString();

            List<CtVet1a> vet1aList = CtVet1aDAL.GetAll_CtVet1aF(
                                                idRo,
                                                reportDtYear,
                                                reportDtMonth,
                                                repMoFilter,
                                                divFilter,
                                                spcFilter,
                                                disFilter
                                                ).ToList();
            ViewBag.Page = "CtVet1a";
            ViewBag.RepList  = spDAL.ReportToToday();

            ViewBag.repMOList = FilterTools.repMOList(  reportDtYear,
                                                        reportDtMonth,
                                                        repMoFilter);
            ViewBag.KIDdivList = FilterTools.KIDdivList(idRo,divFilter);
            ViewBag.KIDspcList = FilterTools.KIDspcList(spcFilter);
            ViewBag.KIDdisList = FilterTools.KIDdisList(disFilter);

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
            
            CtVet1a tmp = new CtVet1a
            {
                KIDro =  User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                repMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            };
            ViewBag.Page = "CtVet1a";
            return View(tmp);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CtVet1a tmpVet)
        {
            if (ModelState.IsValid)
            {
                //проверка на существования аналогичной записи
                if(CtVet1aDAL.IsUniqueRecord(tmpVet)) 
                        CtVet1aDAL.Add_CtVet1a(tmpVet);
                    else
                    {
                        TempData["EM"] = "Такая запись уже существует";
                        ModelState.AddModelError("","Такая запись уже существует");
                        //return NotFound();
                    };
                ViewBag.Page = "CtVet1a";
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet1a";
            return View(tmpVet);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == null) return NotFound();
            CtVet1a tmpVet1a = CtVet1aDAL.Get_CtVet1a_ById(id);
            if(tmpVet1a == null) return NotFound();
 
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);

            ViewBag.RepMoList  = spDAL.RepMO1YearList(Y,M);
            ViewBag.KIDdivList = spDAL.KIDdivList(User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value);
            ViewBag.KIDspcList = spDAL.KIDspcList(); ViewBag.KIDdisList = spDAL.KIDdisList();
            ViewBag.Page = "CtVet1a";
            return View(tmpVet1a);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,[Bind] CtVet1a objCtVet1a)
        {
            //if ((id == null)||(!CtVet1aDAL.IsUniqueRecord(objCtVet1a)))
            if(id==null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                CtVet1aDAL.UpdateCtVet1a(objCtVet1a);
                ViewBag.Page = "CtVet1a";
                return RedirectToAction("Index");
            }
            ViewBag.Page = "CtVet1a";
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
            CtVet1a tmpVet1a = CtVet1aDAL.Get_CtVet1a_ById(id);
            if (tmpVet1a == null)
            {
                return NotFound();
            }
            ViewBag.Page = "CtVet1a";
            return View(tmpVet1a);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CtVet1a tmpVet1a = CtVet1aDAL.Get_CtVet1a_ById(id);
            if (tmpVet1a == null)
            {
                return NotFound();
            }
            ViewBag.Page = "CtVet1a";
            return View(tmpVet1a);
        }

        [Authorize]
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCtVet1a(Guid id)
        {
            CtVet1aDAL.Delete_CtVet1a(id);
            ViewBag.Page = "CtVet1a";
            return RedirectToAction("Index");
        }
    }
}