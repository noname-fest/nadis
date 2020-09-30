using System;
using System.Collections.Generic;
using nadis.tools;
using System.Data.SqlClient;
using nadis.Models;
using Dapper;
using System.Text;
namespace nadis.DAL
{
    public static class CtVet1cDAL
    {

        public static IEnumerable<CtVet1c> GetAll_CtVet1cF(string KIDro, int Y, int M,
                            string fdt, string fdiv,string fspc, string fdis)
            {
                using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
                {
                    var q = new StringBuilder();
                    q.Append("SELECT * FROM ctVet1c WHERE (KIDro=@KIDroP ");
                    if(fdt.Length!=0)
                        { 
                            q.Append(" and month(repMO)="+ fdt.Substring(0,2) + 
                                    " and year(repMO)=" + fdt.Substring(3,4)+" ");
                        } else
                        {
                            q.Append(" and repMo between @bDt and @eDt ");
                        }
                    if(fdiv.Length!=0)
                            q.Append(" and KIDdiv='"+fdiv+"' ");
                    //else    q.Append(" ");
                    if(fspc.Length!=0)
                            q.Append(" and KIDspc='"+fspc+"' ");
                    if(fdis.Length!=0)
                            q.Append(" and KIDspc='"+fdis+"' ");
                    q.Append(" ) ORDER BY repMO DESC");

                    IEnumerable<CtVet1c> tmpList = 
                    _conn.Query<CtVet1c>(q.ToString(), 
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

        public static IEnumerable<CtVet1c> GetAll_CtVet1c(string KIDro, int Y, int M)
            {
                using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
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
            using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                CtVet1c tmp = 
                _conn.QueryFirstOrDefault<CtVet1c>("SELECT * FROM ctVet1c WHERE ID=@KID",
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
            using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                int count = _conn.QueryFirstOrDefault<int>("SELECT COUNT(*) FROM CtVet1c WHERE ("
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
            using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                _conn.Execute("DELETE FROM CtVet1c WHERE ID=@IDR", new { IDR = Id });
            }
        }


        public static void Add_CtVet1c(CtVet1c tmp)
        {
            SqlConnection _conn = new SqlConnection(spDAL.connStr);
            _conn.Execute("INSERT INTO CtVet1c (repMO,KIDro,dtObs,KIDdiv,"
                + "KIDspc,KIDdis,KIDtrt,"+
                "tfemage1,tfemage2,tmalage1,tmalage2,nVT) VALUES (" +
                "@repMOP,@KIDroP,@dtObsP,@KIDdivP,@KIDspcP,@KIDdisP,@KIDtrtP,"
                + "@tfemage1P,@tfemage2P,@tmalage1P,@tmalage2P,@nVT)",
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
                    tmalage1P = tmp.tmalage1,
                    tmalage2P = tmp.tmalage2,
                    nVT = tmp.tfemage1+tmp.tfemage2+tmp.tmalage1+tmp.tmalage2
                });
            _conn.Close();
        }


        public static void Update_CtVet1c(CtVet1c tmp)
        {
            if (tmp == null) return;
            SqlConnection _conn = new SqlConnection(spDAL.connStr);
            _conn.Execute("UPDATE CtVet1c SET KIDro=@KIDroP," +
                "repMo=@repMoP, dtObs=@dtObsP, KIDdiv=@KIDdivP, KIDspc=@KIDspcP,"+
                "KIDdis=@KIDdisP, KIDtrt=@KIDtrtP, "+
                "tfemage1 = @tfemage1P, tfemage2 = @tfemage2P, "+
                "tmalage1 = @tmalage1P, tmalage2 = @tmalage2P, nVT=@nVT "+
                "WHERE ID=@IdP"  ,
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
                        tmalage1P = tmp.tmalage1,
                        tmalage2P = tmp.tmalage2,
                        nVT = tmp.tfemage1+tmp.tfemage2+tmp.tmalage1+tmp.tmalage2
                    }
            );
            _conn.Close();
        }
    }
}