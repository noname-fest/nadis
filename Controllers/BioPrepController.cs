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
    }
}