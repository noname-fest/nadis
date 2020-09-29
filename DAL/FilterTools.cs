using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using nadis.Models.sp;
using nadis.tools;
using Dapper;
using nadis.DAL;
namespace nadis.DAL
{
    public static class FilterTools
    {
        static string[] mmm = 
        new string[]{"янв","фев","мар",
                    "апр","май","июн",
                    "июл","авг","сен",
                    "окт","ноя","дек"
            };

        public static SelectList repMOList(int y, int m)
        {
            List<sp_values> tmpList = new List<sp_values>();
            DateTime dtB = new DateTime(y,m,1);
            DateTime dtE =  DateTime.Today;
            tmpList.Add(new sp_values(){ ID = "",
                                         Text = "-все-" });
            while(dtB <= dtE)
            {
                sp_values tmp_sp = new sp_values
                {
                    ID = dtB.ToString("MM yyyy"),//dtB.Month.ToString(),
                    Text = mmm[dtB.Month-1] + " " + dtB.Year.ToString()
                };
                dtB = dtB.AddMonths(1);
                tmpList.Add(tmp_sp);
            }

            var sl =  new SelectList(tmpList,"ID","Text");
            foreach (var item in sl)
                {
                    if (item.Value == "")
                        {
                            item.Selected = true;
                            break;
                        }
                }
            return sl;//new SelectList(tmpList,"ID","Text");
        }

        public static SelectList VetPrepList()
        {
            using SqlConnection _conn = new SqlConnection(spDAL.connStr);
            string q = "SELECT VetPrep.KID as ID, (VetPrep.PrepType + ' (' + EdIzm.EdIzm + ')') as Text  FROM VetPrep "+
                       "INNER JOIN  EdIzm ON VetPrep.EdizmID = EdIzm.KID";
            //"SELECT KID as ID, PrepType as Text FROM VetPrep"
            var tmp = _conn.Query<sp_values>(q);
            List<sp_values> tL = new List<sp_values>();
            tL.Add(new sp_values(){ ID = "",
                                         Text = "-все-" });
            foreach (var tt in tmp)
            {
                tL.Add(tt);
            }

             var sl =  new SelectList(tL,"ID","Text");
            foreach (var item in sl)
                {
                    if (item.Value == "")
                        {
                            item.Selected = true;
                            break;
                        }
                }
            return sl;
        }
    }
}