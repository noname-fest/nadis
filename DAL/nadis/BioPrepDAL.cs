using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using nadis.tools;
using nadis.Models.sp;
using System;

namespace nadis.DAL.nadis
{
    public static class BioPrepDAL
    {
        public static IEnumerable<BioPrep> GetAll_BioPrep(string KIDro, int rpDtYear, int rpDtMonth)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            SqlConnection _conn = new SqlConnection(connectionString);
            IEnumerable<BioPrep> tmpList =
                _conn.Query<BioPrep>("SELECT * FROM BioPrep WHERE (KIDro=@KIDroP and repMo between @bDt and @eDt)"
                                      + " ORDER BY repMO DESC", 
                                            new { KIDroP = KIDro, 
                                                  bDt = new DateTime(rpDtYear,rpDtMonth,1),
                                                  eDt = new DateTime(DateTime.Today.Year, DateTime.Today.Month,1) 
                                                });
            _conn.Close();
            foreach (var tmp in tmpList)
            {
                tmp.VetPrepDisplay = spDAL.VetPrepName(tmp.VetPrep);
                tmp.EdIzmDisplay = spDAL.EdIzmName(tmp.EdIzm);
            };
            return tmpList;
        }

        public static BioPrep GetById_BioPrep(Guid Id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            SqlConnection _conn = new SqlConnection(connectionString);

            BioPrep tmp = _conn.QueryFirst<BioPrep>(
                "SELECT * FROM BioPrep WHERE ID=@IDv",
                 new { IDv = Id });
            _conn.Close();
            tmp.VetPrepDisplay = spDAL.VetPrepName(tmp.VetPrep);
            tmp.EdIzmDisplay = spDAL.EdIzmName(tmp.EdIzm);

            return tmp;
        }

        public static void Add_BioPrep(BioPrep tmp)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            SqlConnection _conn = new SqlConnection(connectionString);
            _conn.Execute("INSERT INTO BioPrep (KIDro, repMO,VetPrep,EdIzm,Bylo,"
                + "PostupiloVsego,PostupiloRazn,IzrashodVsego,IzrashodDrugoe) VALUES (" +
                "@KIDroP,@repMOP,@VetPrepP,@EdIzmP,@ByloP,"
                + "@PostupiloVsegoP,@PostupiloRaznP,@IzrashodVsegoP,@IzrashodDrugoeP)",
                new
                {
                    KIDroP = tmp.KIDro,
                    repMOP = tmp.RepMO,
                    VetPrepP = tmp.VetPrep,
                    EdIzmP = tmp.EdIzm,
                    ByloP = tmp.Bylo,
                    PostupiloVsegoP = tmp.PostupiloVsego,
                    PostupiloRaznP = tmp.PostupiloRazn,
                    IzrashodDrugoeP = tmp.IzrashodDrugoe,
                    IzrashodVsegoP = tmp.IzrashodVsego
                }
                );
            _conn.Close();
        }

        public static void Update_BioPrep(BioPrep tmp)
        {
            if (tmp == null) return;
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            SqlConnection _conn = new SqlConnection(connectionString);
            _conn.Execute("UPDATE BioPrep SET KIDro=@KIDroP," +
                "repMO=@repMOP," +
                "VetPrep=@VetPrepP," +
                "EdIzm=@EdIzmP," +
                "Bylo=@ByloP," +
                "PostupiloVsego=@PostupiloVsegoP," +
                "PostupiloRazn=@PostupiloRaznP," +
                "IzrashodVsego=@IzrashodVsegoP," +
                "IzrashodDrugoe=@IzrashodDrugoeP WHERE ID=@IdP",
                new
                {
                    IdP = tmp.ID,
                    KIDroP = tmp.KIDro,
                    repMOP = tmp.RepMO,
                    VetPrepP = tmp.VetPrep,
                    EdIzmP = tmp.EdIzm,
                    ByloP = tmp.Bylo,
                    PostupiloVsegoP = tmp.PostupiloVsego,
                    PostupiloRaznP = tmp.PostupiloRazn,
                    IzrashodDrugoeP = tmp.IzrashodDrugoe,
                    IzrashodVsegoP = tmp.IzrashodVsego
                });
            _conn.Close();
        }

        public static bool IsUniqueRecord(BioPrep tmp)
        {
            if (tmp == null) return false;
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                int count = _conn.QueryFirst<int>("SELECT COUNT(*) FROM BioPrep WHERE ("
                    + "repMO=@repMOP and KIDro=@KIDroP and VetPrep=@VetPrepP and EdIzm=@EdIzmP)",
                    new { repMOP = tmp.RepMO, KIDroP = tmp.KIDro, VetPrepP = tmp.VetPrep, EdIzmP = tmp.EdIzm });
                _conn.Close();
                if (count == 0) { return true; } else { return false; }
            }

        }

        public static void Delete_BioPrep(Guid Id)
        {
            if (Id == null) return;
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                _conn.Execute("DELETE FROM BioPrep WHERE ID=@IDR", new { IDR = Id });
            }
        }
    }
}