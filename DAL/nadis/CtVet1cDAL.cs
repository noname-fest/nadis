using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using nadis.Models.nadis;
using nadis.tools;
using Dapper;
using System;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nadis.Models;
using nadis.DAL.nadis;


namespace nadis.DAL.nadis
{
    public static class CtVet1cDAL
    {
        public static IEnumerable<CtVet1c> GetAll_CtVet1c(string KIDro, int Y, int M)
            {
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];

                List<CtVet1a> tmpList = new List<CtVet1a>();
                int reportDtYear = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtYear").Value);
                int reportDtMonth = Convert.ToInt32(User.Claims.ToList().FirstOrDefault(x => x.Type == "reportDtMonth").Value);

                using (SqlConnection _conn = new SqlConnection(connectionString))
                {
                    IEnumerable<CtVet1c> tmpList = 
                    _conn.Query<BioPrep>("SELECT * FROM ctVet1c WHERE (KIDro=@KIDroP and repMo between @bDt and @eDt)"
                                      + " ORDER BY repMO DESC", 
                                            new { KIDroP = KIDro, 
                                                  bDt = new DateTime(rpDtYear,rpDtMonth,1),
                                                  eDt = new DateTime(DateTime.Today.Year, DateTime.Today.Month,1) 
                                                });
                    _conn.Close();
                    foreach (var tmp in tmpList)
                    {
                        tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                        tmp.KIDspcDisplay = spDAL.EdIzmName(tmp.EdIzm);
                    };

                    return tmpList;
                }
            {
    }
}