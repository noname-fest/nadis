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
            return View(list);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            int Y = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int M = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            string KIDro = User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value;

            //ViewBag.repPerList = CtVet2DAL.RepPerListList(Y,M);
            //ViewBag.KIDdivList = spDAL.KIDdivList(KIDro);
            //ViewBag.KIDspcList = spDAL.KIDspcList();
            //ViewBag.KIDdisList = spDAL.KIDdisList();

            CtVet1a tmp = new CtVet1a
            {
                KIDro =  User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                RepMO = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            };
            ViewBag.Page = "CtVet2";
            return View(tmp);
        }







    }
}