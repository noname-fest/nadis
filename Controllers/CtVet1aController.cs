using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using nadis.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using nadis.Models.sp;
using System.Data.SqlClient;
using nadis.tools;
using System.Data;

namespace nadis.Controllers
{
    public class CtVet1aController : Controller
    {
        readonly CtVet1aDAL vet1aDAL = new CtVet1aDAL();
        public IActionResult Index()
        {
            _ = new List<CtVet1a>();
            List<CtVet1a> vet1aList = vet1aDAL.GetAllCtVet1a("RD02205").ToList();
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

        [HttpGet]
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

            //repMO -------------------------------------------------------
            List<sp_values> tmpList = new List<sp_values>();
            int year = tmpVet1a.RepMO.Year;
            for (int i = 1; i < 13; i++)
            {
                sp_values tmp_sp = new sp_values();
                tmp_sp.ID  = new DateTime(year,i,1).ToString();
                tmp_sp.Text = new DateTime(year, i, 1).ToString("MMM yyyy");
                tmpList.Add(tmp_sp);
            }
            ViewBag.RepMoList = new SelectList(tmpList, "ID", "Text");//,tmpVet1a.RepMO.ToString());

            //Ayil Aimak ------------------------------------------------------
            List<sp_values> tmpList2 = new List<sp_values>();
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Get_SPAa", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idL", id);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["KID"].ToString();
                        tmp.Text = dr["Socunit"].ToString().Trim();
                    }
                    tmpList2.Add(tmp);
                }
                _conn.Close();
            }
            ViewBag.KIDdivList = new SelectList(tmpList2, "ID", "Text");//,tmpVet1a.idKIDdiv);


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