
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.Models;
using nadis.DAL;
using nadis.DAL.nadis;
using System.Security.Claims;

namespace nadis.Controllers
{
    public class BioPrepController
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