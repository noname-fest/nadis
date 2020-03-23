using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.DAL.nadis;
using nadis.Models;
using nadis.Models.nadis;

namespace nadis.Controllers
{
    public class CtVet1cController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            _ = new List<CtVet1c>();
            int reportDtYear = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
            int reportDtMonth = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);
            List<CtVet1c> CtVet1cList = CtVet1cDAL.GetAll_CtVet1c(
                                                User.Claims.ToList().FirstOrDefault(x => x.Type == "KIDro").Value,
                                                reportDtYear,
                                                reportDtMonth
                                                ).ToList();
            return View(CtVet1cList);
        }
    }
}