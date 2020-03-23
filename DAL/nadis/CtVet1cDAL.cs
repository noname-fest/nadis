using System;
using System.Collections.Generic;
using nadis.Models.nadis;
using nadis.tools;
using System.Data.SqlClient;
using nadis.Models;
using Dapper;

namespace nadis.DAL.nadis
{
    public static class CtVet1cDAL
    {
        public static IEnumerable<CtVet1c> GetAll_CtVet1c(string KIDro, int Y, int M)
            {
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];

                using (SqlConnection _conn = new SqlConnection(connectionString))
                {
                    IEnumerable<CtVet1c> tmpList = 
                    _conn.Query<CtVet1c>("SELECT * FROM ctVet1c WHERE (KIDro=@KIDroP and repMo between @bDt and @eDt)"
                                      + " ORDER BY repMO DESC", 
                                            new { KIDroP = KIDro, 
                                                  bDt = new DateTime(Y,M,1),
                                                  eDt = new DateTime(DateTime.Today.Year, DateTime.Today.Month,1) 
                                                });
                    _conn.Close();
                    foreach (var tmp in tmpList)
                    {
                        tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                        tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                        tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                        tmp.KIDtrtDisplay = spDAL.KIDtrtName(tmp.KIDtrt);
                    };
                    return tmpList;
                }
            }
        public static CtVet1c GetById_CtVet1c(Guid Id)
        {
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];

                using (SqlConnection _conn = new SqlConnection(connectionString))
                {
                    CtVet1c tmp = 
                    _conn.QueryFirst<CtVet1c>("SELECT * FROM ctVet1c WHERE ID=@KID",
                                              new { KID = Id });
                    _conn.Close();
                        tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                        tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                        tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                        tmp.KIDtrtDisplay = spDAL.KIDtrtName(tmp.KIDtrt);
                    return tmp;
                }
        }
    }
}