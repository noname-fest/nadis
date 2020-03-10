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
            List<CtVet1a> vet1aList = vet1aDAL.GetAllCtVet1a("RD04235", "01/12/2019").ToList();
            return View(vet1aList);
        }
    }
}