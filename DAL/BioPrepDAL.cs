using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using System;
using Dapper.Contrib.Extensions;
using Dapper.Contrib;
using System.Text;

namespace nadis.DAL
{
    public static class BioPrepDAL
    {
        public static bool IsUniqueRecord(Guid id, string _KIDro, DateTime _repMo, string _prep)
        {
            using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                int count = _conn.QueryFirst<int>("SELECT COUNT(*) FROM BioPrep WHERE ("
                    + "repMO=@repMOP and KIDro=@KIDroP and VetPrep=@VetPrepP"
                    + " and ID<>@idd)",
                    new {   repMOP = _repMo, KIDroP = _KIDro,
                            VetPrepP = _prep, ///EdIzmP = _edizm,
                            idd = id });
                _conn.Close();
                if (count == 0) { return true; } else { return false; }
            }
        }
        public static long? GetByloById(string _KIDro, DateTime _repMo, string _prep)
        {
            long? _bylo = 0;
            int y = _repMo.Year;
            int m = _repMo.Month;
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                /*
                string q = "SELECT TOP 1 (Bylo+PostupiloVsego+PostupiloRazn-IzrashodVsego-IzrashodDrugoe) "
                +"as ByloPredMonth FROM BioPrep WHERE (" +
                "KIDro=@KIDro and VetPrep=@VetPrep and EdIzm=@EdIzm and "+
                "repMO < @dt) ORDER BY repMO DESC";*/
                string q = "SELECT TOP 1 (Bylo+PostupiloVsego-IzrashodVsego) "
                +"as ByloPredMonth FROM BioPrep WHERE (" +
                "KIDro=@KIDro and VetPrep=@VetPrep and "+
                "repMO < @dt) ORDER BY repMO DESC";
                var param = new 
                    {
                        KIDro   = _KIDro,
                        VetPrep = _prep,
                        //EdIzm   = _edizm,
                        dt      = _repMo
                    };
                _bylo = _conn.QueryFirstOrDefault<long?>(q,param);
                if(_bylo == null) _bylo=0;
            }
            return _bylo;
        }
        public static IEnumerable<BioPrep> GetAll_BioPrepF(string KIDro, int rpDtYear, int rpDtMonth, 
                                                            string fdt, string fvp)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                //if(){};
                //int m = Int32.Parse(fdt.Substring(0,2));
                //int y = Int32.Parse(fdt.Substring(3,4));

                var q = new StringBuilder();
                q.Append("SELECT * FROM BioPrep WHERE (KIDro=@KIDroP ");
                if(fdt.Length!=0)
                    { 
                        q.Append(" and month(repMO)="+ fdt.Substring(0,2) + 
                                 " and year(repMO)=" + fdt.Substring(3,4)+" ");
                    } else
                    {
                        q.Append(" and repMo between @bDt and @eDt ");
                    }
                if(fvp.Length!=0)
                    {
                        q.Append(" and VetPrep='" + fvp + "') ORDER BY repMO DESC");
                    } else
                    {
                        q.Append(" ) ORDER BY repMO DESC");   
                    }


                IEnumerable<BioPrep> tmpList =
                    _conn.Query<BioPrep>(q.ToString(), 
                                                new { KIDroP = KIDro, 
                                                      bDt = new DateTime(rpDtYear,rpDtMonth,1),
                                                      eDt = new DateTime(DateTime.Today.AddMonths(1).Year,
                                                            DateTime.Today.AddMonths(1).Month,1)
                                                    });
                _conn.Close();
                foreach (var tmp in tmpList)
                {
                    tmp.VetPrepDisplay = spDAL.VetPrepName(tmp.VetPrep);
                    //tmp.EdIzmDisplay = spDAL.EdIzmName(tmp.EdIzm);
                };
                return tmpList;
            }
        }

        public static IEnumerable<BioPrep> GetAll_BioPrep(string KIDro, int rpDtYear, int rpDtMonth)
        {

            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
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
                    //tmp.EdIzmDisplay = spDAL.EdIzmName(tmp.EdIzm);
                };
                return tmpList;
            }
        }

        public static BioPrep GetById_BioPrep(Guid Id)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                BioPrep tmp = _conn.QueryFirst<BioPrep>(
                    "SELECT * FROM BioPrep WHERE ID=@IDv",new { IDv = Id });
                /*
                string q = "SELECT TOP 1 (Bylo+PostupiloVsego+PostupiloRazn-IzrashodVsego-IzrashodDrugoe) "
                            +"as ByloPredMonth FROM BioPrep WHERE (" +
                            "KIDro=@KIDro and VetPrep=@VetPrep and EdIzm=@EdIzm and "+
                            "repMO < @dt) ORDER BY repMO DESC";*/
                string q = "SELECT TOP 1 (Bylo+PostupiloVsego-IzrashodVsego) "
                            +"as ByloPredMonth FROM BioPrep WHERE (" +
                            "KIDro=@KIDro and VetPrep=@VetPrep and "+
                            "repMO < @dt) ORDER BY repMO DESC";
                var param = new 
                    {
                        KIDro = tmp.KIDro,
                        VetPrep = tmp.VetPrep,
                        //EdIzm = tmp.EdIzm,
                        dt = tmp.RepMO,
                    };
                tmp.Bylo = _conn.QueryFirstOrDefault<long?>(q,param);
                if(tmp.Bylo == null) tmp.Bylo=0;
                _conn.Close();

                    tmp.VetPrepDisplay = spDAL.VetPrepName(tmp.VetPrep);
                    //tmp.EdIzmDisplay = spDAL.EdIzmName(tmp.EdIzm);
                    tmp.Ostatok_za_mesyac = tmp.Bylo + tmp.PostupiloVsego 
                                            //+ tmp.PostupiloRazn
                                            - tmp.IzrashodVsego;
                                            //-tmp.IzrashodDrugoe;
                return tmp;
            }
        }

        public static void Add_BioPrep(BioPrep tmp)
        {
            SqlConnection _conn = new SqlConnection(spDAL.connStr);
            /*
            string q = "SELECT TOP 1 (Bylo+PostupiloVsego+PostupiloRazn-IzrashodVsego-IzrashodDrugoe) "
                        +"as ByloPredMonth FROM BioPrep WHERE (" +
                        "KIDro=@KIDro and VetPrep=@VetPrep and EdIzm=@EdIzm and "+
                        "repMO < @dt) ORDER BY repMO DESC";*/
            string q = "SELECT TOP 1 (Bylo+PostupiloVsego-IzrashodVsego) "
                        +"as ByloPredMonth FROM BioPrep WHERE (" +
                        "KIDro=@KIDro and VetPrep=@VetPrep and "+
                        "repMO < @dt) ORDER BY repMO DESC";
            var param = new 
                {
                    KIDro = tmp.KIDro,
                    VetPrep = tmp.VetPrep,
                    //EdIzm = spDAL.EdIzmID(tmp.VetPrep),
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
                    EdIzmP = spDAL.EdIzmID(tmp.VetPrep),
                    ByloP = tmp.Bylo,
                    PostupiloVsegoP = tmp.PostupiloVsego,
                    PostupiloRaznP = tmp.PostupiloRazn,
                    IzrashodDrugoeP = tmp.IzrashodDrugoe,
                    IzrashodVsegoP = tmp.IzrashodVsego,
                    Ostatok_za_mesyac = tmp.Bylo + tmp.PostupiloVsego
                                        //tmp.PostupiloRazn - 
                                        - tmp.IzrashodVsego
                                        //tmp.IzrashodDrugoe
                });
            _conn.Close();
        }

        public static void Update_BioPrep(BioPrep tmp)
        {
            if (tmp == null) return;
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
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
                        EdIzmP = spDAL.EdIzmID(tmp.VetPrep),
                        Bylo = tmp.Bylo,
                        PostupiloVsegoP = tmp.PostupiloVsego,
                        PostupiloRaznP = tmp.PostupiloRazn,
                        IzrashodDrugoeP = tmp.IzrashodDrugoe,
                        IzrashodVsegoP = tmp.IzrashodVsego,
                        Ostatok_za_mesyac = 
                                    tmp.Bylo + tmp.PostupiloVsego
                                    //tmp.PostupiloRazn -
                                    //tmp.IzrashodDrugoe -
                                    - tmp.IzrashodVsego
                };
                _conn.Execute(q,param);
                _conn.Close();
            }
        }

        public static bool IsUniqueRecord(BioPrep tmp)
        {
            if (tmp == null) return false;
            using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                int count = _conn.QueryFirst<int>("SELECT COUNT(*) FROM BioPrep WHERE ("
                    + "repMO=@repMOP and KIDro=@KIDroP and VetPrep=@VetPrepP)",
                    new { repMOP = tmp.RepMO, KIDroP = tmp.KIDro, VetPrepP = tmp.VetPrep });
                _conn.Close();
                if (count == 0) { return true; } else { return false; }
            }
        }

        public static void Delete_BioPrep(Guid Id)
        {
            if (Id == null) return;
            using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                _conn.Execute("DELETE FROM BioPrep WHERE ID=@IDR", new { IDR = Id });
            }
        }
    }
}