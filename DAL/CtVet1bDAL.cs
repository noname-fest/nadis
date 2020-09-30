using Dapper;
using nadis.Models;
using nadis.tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace nadis.DAL
{
    public static class CtVet1bDAL
    {
        public static IEnumerable<CtVet1b> GetAll_CtVet1bF(string KIDro, int Y, int M,
                            string fdt, string fdiv,string fspc, string fdis)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                //string q = "SELECT * FROM ctVet1b WHERE "+
                //            "(KIDro=@KIDroP and repMO between @bDt and @eDt) ORDER BY repMO DESC";
                 var q = new StringBuilder();
                q.Append("SELECT * FROM ctVet1b WHERE (KIDro=@KIDroP ");
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

                var param = new 
                {
                    KIDroP = KIDro, 
                    bDt = new DateTime(Y,M,1),
                    eDt = new DateTime(DateTime.Today.Year,
                                        DateTime.Today.Month+1,
                                        1) 
                };
                IEnumerable<CtVet1b> tmpList = _conn.Query<CtVet1b>(q.ToString(),param);
                foreach(var tmp in tmpList)
                {
                    tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                    tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                    tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                    tmp.testDisplay   = spDAL.testName(tmp.test);
                }
                return tmpList;
            }
        }

        public static IEnumerable<CtVet1b> GetAll_CtVet1b(string KIDro, int Y, int M)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "SELECT * FROM ctVet1b WHERE "+
                            "(KIDro=@KIDroP and repMO between @bDt and @eDt) ORDER BY repMO DESC";
                var param = new 
                {
                    KIDroP = KIDro, 
                    bDt = new DateTime(Y,M,1),
                    eDt = new DateTime(DateTime.Today.Year,
                                        DateTime.Today.Month+1,
                                        1) 
                };
                IEnumerable<CtVet1b> tmpList = _conn.Query<CtVet1b>(q,param);
                foreach(var tmp in tmpList)
                {
                    tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                    tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                    tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                    tmp.testDisplay   = spDAL.testName(tmp.test);
                }
                return tmpList;
            }
        }


        public static void Add_CtVet1b(CtVet1b tmp)
        {
            if(tmp==null)return;
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "INSERT INTO ctVET1b "+ 
	                "(KIDro,repMO,KIDdiv,KIDspc,KIDdis,test,femage_1,femage_2,"+
                            "fage1_pos,fage2_pos,dtObs,test_tot,pos_tot) "+
	                    "VALUES "+
	                "(@KIDro,@repMO,@KIDdiv,@KIDspc,@KIDdis,@test,@femage_1,@femage_2,"+
                            "@fage1_pos,@fage2_pos,@dtObs,@test_tot,@pos_tot)";
                var param = new 
                {
                    KIDro = tmp.KIDro,
                    repMO = tmp.repMO,
                    KIDdiv = tmp.KIDdiv,
                    KIDspc = tmp.KIDspc,
                    KIDdis = tmp.KIDdis,
                    test = tmp.test,
                    femage_1 = tmp.femage_1,
                    femage_2 = tmp.femage_2,
                    fage1_pos = tmp.fage1_pos,
                    fage2_pos = tmp.fage2_pos,
                    dtObs = tmp.dtObs,
                    test_tot = tmp.femage_1+tmp.femage_2,
                    pos_tot = tmp.fage1_pos+tmp.fage2_pos
                };
                _conn.Execute(q,param);
                _conn.Close();
            }
        }


        public static CtVet1b GetCtVet1bById(Guid id)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                CtVet1b tmp = _conn.QueryFirstOrDefault<CtVet1b>("SELECT * FROM ctVet1b WHERE ID=@idd",
                                new {idd = id});
                tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                tmp.testDisplay   = spDAL.testName(tmp.test);
                return tmp;
            }
        }


        public static void UpdateCtVet1b(CtVet1b tmp)
        {
            if (tmp is null) { return; }
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "UPDATE ctVET1b SET "+ 
		                    "KIDro=@KIDro,repMO=@repMO,KIDdiv=@KIDdiv,"+
		                    "KIDspc=@KIDspc,KIDdis=@KIDdis,test = @test,"+
                            "dtObs = @dtObs,femage_1=@femage_1,femage_2=@femage_2,"+
                            "fage1_pos = @fage1_pos,fage2_pos = @fage2_pos,"+
                            "test_tot=@test_tot, pos_tot=@pos_tot "+
                            "WHERE ID=@ID";
                var param = new 
                        {
                            ID = tmp.ID,
                    		KIDro = tmp.KIDro,
                            repMO = tmp.repMO,
                            KIDdiv= tmp.KIDdiv,
                            KIDspc=tmp.KIDspc,
                            KIDdis=tmp.KIDdis,
                            test = tmp.test,
                            dtObs = tmp.dtObs,
                            femage_1=tmp.femage_1,
                            femage_2=tmp.femage_2,
                            fage1_pos = tmp.fage1_pos,
                            fage2_pos = tmp.fage2_pos,
                            test_tot = tmp.femage_1+tmp.femage_2,
                            pos_tot = tmp.fage1_pos+tmp.fage2_pos
                        };
                _conn.Execute(q,param);
            }
        }

        public static void DeleteCtVet1b(Guid id)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                _conn.Execute("DELETE FROM ctVet1b WHERE ID=@idd",new{idd = id});
            }
        }

        public static bool IsUniqueRecord(CtVet1b tmp)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q="SELECT COUNT(*) FROM ctVet1b WHERE "+
                "(repMO=@repMOP and KIDro=@KIDroP and KIDspc=@KIDspcP and "+
                "KIDdis=@KIDdisP and test=@testP and "+
                "KIDdiv=@KIDdivP)";
                var param = new 
                {  
                    repMOP=tmp.repMO,
                    KIDroP=tmp.KIDro,
                    KIDspcP=tmp.KIDspc,
                    KIDdisP=tmp.KIDdis,
                    testP=tmp.test,
                    KIDdivP=tmp.KIDdiv
                };
                int count = _conn.QueryFirstOrDefault<int>(q,param);
                if (count == 0) { return true; } else { return false; }
            }
        }
    }
}
