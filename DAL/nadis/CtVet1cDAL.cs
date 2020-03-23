using System;
using System.Collections.Generic;
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
                    _conn.Query<CtVet1c>("SELECT TOP 10 * FROM ctVet1c WHERE (KIDro=@KIDroP and repMo between @bDt and @eDt)"
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


        public static bool IsUniqueRecord(CtVet1c tmp)
        {
            if (tmp == null) return false;
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                int count = _conn.QueryFirst<int>("SELECT COUNT(*) FROM CtVet1c WHERE ("
                    + "repMO=@repMOP and KIDro=@KIDroP and KIDdiv=@KIDdivP and KIDspc=@KIDspcP "+
                    "and KIDdis=@KIDdisP and KIDtrt=@KIDtrtP)",
                    new { repMOP = tmp.RepMO, KIDroP = tmp.KIDro,
                            KIDdivP = tmp.KIDdiv, KIDspcP = tmp.KIDspc,
                            KIDdisP = tmp.KIDdis, KIDtrtP = tmp.KIDtrt
                            });
                _conn.Close();
                if (count == 0) { return true; } else { return false; }
            }

        }


        public static void Delete_CtVet1c(Guid Id)
        {
            if (Id == null) return;
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                _conn.Execute("DELETE FROM CtVet1c WHERE ID=@IDR", new { IDR = Id });
            }
        }


        public static void Add_CtVet1c(CtVet1c tmp)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            SqlConnection _conn = new SqlConnection(connectionString);
            _conn.Execute("INSERT INTO CtVet1c (repMO,KIDro,dtObs,KIDdiv,"
                + "KIDspc,KIDdis,KIDtrt,"+
                "tfemage1,tfemage2,tmalage1,tmalage2) VALUES (" +
                "@repMOP,@KIDroP,@dtObsP,@KIDdivP,@KIDspcP,@KIDdisP,@KIDtrtP,"
                + "@tfemage1P,@tfemage2P,@tmalage1P,@tmalage2P)",
                new
                {
                    KIDroP = tmp.KIDro,
                    repMOP = tmp.RepMO,
                    dtObsP = tmp.dtObs,
                    KIDdivP = tmp.KIDdiv,
                    KIDspcP = tmp.KIDspc,
                    KIDdisP = tmp.KIDdis,
                    KIDtrtP = tmp.KIDtrt,
                    tfemage1P = tmp.tfemage1,
                    tfemage2P = tmp.tfemage2,
                    tmalage1P = tmp.tfemage1,
                    tmalage2P = tmp.tfemage2
                }
                );
            _conn.Close();
        }


        public static void Update_CtVet1c(CtVet1c tmp)
        {
            if (tmp == null) return;
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            SqlConnection _conn = new SqlConnection(connectionString);
            _conn.Execute("UPDATE CtVet1c SET KIDro=@KIDroP," +
                "repMo=@repMoP, dtObs=@dtObsP, KIDdiv=@KIDdivP, KIDspc=@KIDspcP,"+
                "KIDdis=@KIDdisP, KIDtrt=@KIDtrtP, "+
                "tfemage1 = @tfemage1P, tfemage2 = @tfemage2P, "+
                "tmalage1 = @tmalage1P, tmalage2 = @tmalage2P WHERE ID=@IdP"  ,
                new
                    {
                        IdP = tmp.ID,
                        KIDroP = tmp.KIDro,
                        repMoP = tmp.RepMO,
                        dtObsP = tmp.dtObs,
                        KIDdivP = tmp.KIDdiv,
                        KIDspcP = tmp.KIDspc,
                        KIDdisP = tmp.KIDdis,
                        KIDtrtP = tmp.KIDtrt,
                        tfemage1P = tmp.tfemage1,
                        tfemage2P = tmp.tfemage2,
                        tmalage1P = tmp.tfemage1,
                        tmalage2P = tmp.tfemage2}
            );
            _conn.Close();
        }
    }
}