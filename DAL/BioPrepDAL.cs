using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using nadis.tools;
using System;
using System.Data.SqlTypes;

namespace nadis.DAL.nadis
{
    public static class BioPrepDAL
    {
        public static long? GetByloById(string _KIDro, DateTime _repMo, string _prep,string _edizm)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            long? _bylo = 0;
            int y = _repMo.Year;
            int m = _repMo.Month;
            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                string q = "SELECT TOP 1 (Bylo+PostupiloVsego+PostupiloRazn-IzrashodVsego-IzrashodDrugoe) "
                +"as ByloPredMonth FROM BioPrep WHERE (" +
                "KIDro=@KIDro and VetPrep=@VetPrep and EdIzm=@EdIzm and "+
                "repMO < @dt) ORDER BY repMO DESC";
                var param = new 
                    {
                        KIDro   = _KIDro,
                        VetPrep = _prep,
                        EdIzm   = _edizm,
                        dt      = _repMo
                    };
                _bylo = _conn.QueryFirstOrDefault<long?>(q,param);
                if(_bylo == null) _bylo=0;
            }
            return _bylo;
        }

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
            /*
            string q = "SELECT TOP 1 (Bylo+PostupiloVsego+PostupiloRazn-IzrashodVsego-IzrashodDrugoe) "
                        +"as ByloPredMonth FROM BioPrep WHERE (" +
                        "KIDro=@KIDro and VetPrep=@VetPrep and EdIzm=@EdIzm and "+
                        "repMO < @dt) ORDER BY repMO DESC";
                        //"MONTH(repMO)=@M and YEAR(repMO)=@Y )";
                        */
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
                "SELECT * FROM BioPrep WHERE ID=@IDv",new { IDv = Id });
            
            string q = "SELECT TOP 1 (Bylo+PostupiloVsego+PostupiloRazn-IzrashodVsego-IzrashodDrugoe) "
                        +"as ByloPredMonth FROM BioPrep WHERE (" +
                        "KIDro=@KIDro and VetPrep=@VetPrep and EdIzm=@EdIzm and "+
                        "repMO < @dt) ORDER BY repMO DESC";
            var param = new 
                {
                    KIDro = tmp.KIDro,
                    VetPrep = tmp.VetPrep,
                    EdIzm = tmp.EdIzm,
                    dt = tmp.RepMO,
                };
            tmp.Bylo = _conn.QueryFirstOrDefault<long?>(q,param);
            if(tmp.Bylo == null) tmp.Bylo=0;
            _conn.Close();

                tmp.VetPrepDisplay = spDAL.VetPrepName(tmp.VetPrep);
                tmp.EdIzmDisplay = spDAL.EdIzmName(tmp.EdIzm);
                tmp.Ostatok_za_mesyac = tmp.Bylo + tmp.PostupiloVsego +
                                        tmp.PostupiloRazn - 
                                        tmp.IzrashodVsego-
                                        tmp.IzrashodDrugoe;

            return tmp;
        }

        public static void Add_BioPrep(BioPrep tmp)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            SqlConnection _conn = new SqlConnection(connectionString);
            string q = "SELECT TOP 1 (Bylo+PostupiloVsego+PostupiloRazn-IzrashodVsego-IzrashodDrugoe) "
                        +"as ByloPredMonth FROM BioPrep WHERE (" +
                        "KIDro=@KIDro and VetPrep=@VetPrep and EdIzm=@EdIzm and "+
                        "repMO < @dt) ORDER BY repMO DESC";
            var param = new 
                {
                    KIDro = tmp.KIDro,
                    VetPrep = tmp.VetPrep,
                    EdIzm = tmp.EdIzm,
                    dt = tmp.RepMO,
                };
            tmp.Bylo = _conn.QueryFirstOrDefault<long?>(q,param);
            if(tmp.Bylo == null) tmp.Bylo=0;

            _conn.Execute("INSERT INTO BioPrep (KIDro, repMO,VetPrep,EdIzm,Bylo,"
                + "PostupiloVsego,PostupiloRazn,IzrashodVsego,IzrashodDrugoe,Ostatok_za_mesyac) VALUES (" +
                "@KIDroP,@repMOP,@VetPrepP,@EdIzmP,@ByloP,"
                + "@PostupiloVsegoP,@PostupiloRaznP,@IzrashodVsegoP,@IzrashodDrugoeP,@Ostatok_za_mesyac)",
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
                    IzrashodVsegoP = tmp.IzrashodVsego,
                    Ostatok_za_mesyac = tmp.Bylo + tmp.PostupiloVsego +
                                        tmp.PostupiloRazn - 
                                        tmp.IzrashodVsego-
                                        tmp.IzrashodDrugoe
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
            /*
            string qb = "SELECT TOP 1 (Bylo+PostupiloVsego+PostupiloRazn-IzrashodVsego-IzrashodDrugoe) "
                        +"as ByloPredMonth FROM BioPrep WHERE (" +
                        "KIDro=@KIDro and VetPrep=@VetPrep and EdIzm=@EdIzm and "+
                        "repMO < @dt) ORDER BY repMO DESC";
            var paramb = new 
                {
                    KIDro = tmp.KIDro,
                    VetPrep = tmp.VetPrep,
                    EdIzm = tmp.EdIzm,
                    dt = tmp.RepMO,
                };
            long? bb = _conn.QueryFirstOrDefault(qb,paramb);
            if(bb==null)bb=0;
            */

            string q = 
                "UPDATE BioPrep SET KIDro=@KIDroP," +
                "repMO=@repMOP," +
                "VetPrep=@VetPrepP," +
                "EdIzm=@EdIzmP," +
                "Bylo=@Bylo,"+
                "PostupiloVsego=@PostupiloVsegoP," +
                "PostupiloRazn=@PostupiloRaznP," +
                "IzrashodVsego=@IzrashodVsegoP," +
                "IzrashodDrugoe=@IzrashodDrugoeP,"+
                "Ostatok_za_mesyac=@Ostatok_za_mesyac"+
                " WHERE ID=@IdP";
            var param = new {
                    IdP = tmp.ID,
                    KIDroP = tmp.KIDro,
                    repMOP = tmp.RepMO,
                    VetPrepP = tmp.VetPrep,
                    EdIzmP = tmp.EdIzm,
                    Bylo = tmp.Bylo,
                    PostupiloVsegoP = tmp.PostupiloVsego,
                    PostupiloRaznP = tmp.PostupiloRazn,
                    IzrashodDrugoeP = tmp.IzrashodDrugoe,
                    IzrashodVsegoP = tmp.IzrashodVsego,
                    Ostatok_za_mesyac = 
                                tmp.Bylo + tmp.PostupiloVsego +
                                tmp.PostupiloRazn -
                                tmp.IzrashodDrugoe -
                                tmp.IzrashodVsego
            };
            _conn.Execute(q,param);
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