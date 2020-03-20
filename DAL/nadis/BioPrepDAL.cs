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
        public static IEnumerable<BioPrep> GetAll_BioPrep(string KIDro)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            SqlConnection _conn = new SqlConnection(connectionString);
            IEnumerable<BioPrep> tmpList = 
                _conn.Query<BioPrep>("SELECT * FROM BioPrep WHERE (KIDro=@KIDroP and DATEDIFF(mm,repMO,GETDATE())<2)"
                                      + " ORDER BY repMO DESC", new { KIDroP = KIDro });
            _conn.Close();
            foreach(var tmp in tmpList)
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
                "SELECT TOP 1 FROM BioPrep WHERE ID=@IDv",
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
                + "@PostupiloVsegoP,@PostupiloRaznP,@IzrashodVsegovP,@IzrashodDrugoeP)",
                new {
                        KIDroP=KIDro,repMOP=repMO,VetPrepP=VetPrep,EdIzmP=EdIzm,ByloP=Bylo,
                        PostupiloVsegoP=PostupiloVsego,PostupiloRaznP=PostupiloRazn,
                        IzrashodDrugoeP=IzrashodDrugoeP,IzrashodVsegoP=IzrashodVsego
                    }
                );
            _conn.Close();
        }
    }
}