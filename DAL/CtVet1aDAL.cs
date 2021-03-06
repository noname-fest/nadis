﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using nadis.tools;
using nadis.Models;
using Dapper;
using System.Text;

namespace nadis.DAL
{
    public static class CtVet1aDAL
    {
        public static IEnumerable<CtVet1a> GetAll_CtVet1aF(string KIDro, int Y, int M,
                                        string fdt, string fdiv,string fspc, string fdis)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                //string q = "SELECT * FROM ctVet1a WHERE "+
                //            "(KIDro=@KIDroP and repMO between @bDt and @eDt) ORDER BY repMO DESC";
                var q = new StringBuilder();
                q.Append("SELECT * FROM ctVet1a WHERE (KIDro=@KIDroP ");
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
                    //eDt = new DateTime(DateTime.Today.Year,
                    //                    DateTime.Today.Month+1,
                    //                    1)
                    eDt = new DateTime(DateTime.Today.AddMonths(1).Year,
                                        DateTime.Today.AddMonths(1).Month,1)
                };
                IEnumerable<CtVet1a> tmpList = _conn.Query<CtVet1a>(q.ToString(),param);
                foreach(var tmp in tmpList)
                {
                    tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                    tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                    tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                }
                return tmpList;
            }
        }


        public static IEnumerable<CtVet1a> GetAll_CtVet1a(string KIDro, int Y, int M)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "SELECT * FROM ctVet1a WHERE "+
                            "(KIDro=@KIDroP and repMO between @bDt and @eDt) ORDER BY repMO DESC";
                var param = new
                {
                    KIDroP = KIDro, 
                    bDt = new DateTime(Y,M,1),
                    eDt = new DateTime(DateTime.Today.Year,
                                        DateTime.Today.Month+1,
                                        1)
                };
                IEnumerable<CtVet1a> tmpList = _conn.Query<CtVet1a>(q,param);
                foreach(var tmp in tmpList)
                {
                    tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                    tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                    tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                }
                return tmpList;
            }
        }

        public static void Add_CtVet1a(CtVet1a tmp)
        {
            if (tmp == null) return;
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "INSERT INTO ctVet1a (KIDro,repMO,KIDdiv,KIDspc,KIDdis,pos_units,"+
                            "positives,dead,end_pos_units,end_pos_animals,culled,comments)"+
                            " VALUES ("+
                            "@KIDro,@repMO,@KIDdiv,@KIDspc,@KIDdis,@pos_units,"+
                            "@positives,@dead,@end_pos_units,@end_pos_animals,@culled,@c)";
                var param = new
                {
                    KIDro = tmp.KIDro,
                    repMO = tmp.repMO,
                    KIDdiv= tmp.KIDdiv,
                    KIDspc= tmp.KIDspc,
                    KIDdis= tmp.KIDdis,
                    pos_units = tmp.pos_units,
                    positives = tmp.positives,
                    dead = tmp.dead,
                    end_pos_units = tmp.end_pos_units,
                    end_pos_animals = tmp.end_pos_animals,
                    culled = tmp.culled,
                    c = tmp.comments
                };
                _conn.Execute(q,param);
                _conn.Close();
            }
        }

        public static void UpdateCtVet1a(CtVet1a tmp)
        {
            if (tmp is null) { return; }
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "UPDATE ctVET1a SET "+ 
                            "KIDro=@KIDro,"+
                            "repMO=@repMO,"+
                            "KIDdiv=@KIDdiv,"+
                            "KIDspc=@KIDspc,"+
                            "KIDdis=@KIDdis,"+
                            "pos_units=@pos_units,"+
                            "positives=@positives,"+
                            "dead=@dead,"+
                            "end_pos_units=@end_pos_units,"+
                            "end_pos_animals=@end_pos_animals,"+
                            "culled=@culled,"+
                            "comments=@c "+
                            "WHERE ID=@ID";
                var param = new
                {
                    KIDro=tmp.KIDro,
                    repMO=tmp.repMO,
                    KIDdiv=tmp.KIDdiv,
                    KIDspc=tmp.KIDspc,
                    KIDdis=tmp.KIDdis,
                    pos_units=tmp.pos_units,
                    positives=tmp.positives,
                    dead=tmp.dead,
                    end_pos_units=tmp.end_pos_units,
                    end_pos_animals=tmp.end_pos_animals,
                    culled=tmp.culled,
                    ID=tmp.ID,
                    c = tmp.comments
                };
                _conn.Execute(q,param);
                _conn.Close();
            }
        }

        public static void Delete_CtVet1a(Guid id)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                _conn.Execute("DELETE FROM ctVet1a WHERE ID=@idd",new{idd=id});
            }
        }

        public static CtVet1a Get_CtVet1a_ById(Guid id)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q="SELECT * FROM ctVet1a WHERE ID=@idd";
                var param = new {idd = id};
                CtVet1a tmp = _conn.QueryFirstOrDefault<CtVet1a>(q,param);
                tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                return tmp;
            }
        }

        public static bool IsUniqueRecord(CtVet1a tmp)
        {
            if (tmp == null) return false;
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "SELECT COUNT(ID) as kolvo FROM ctVET1a "+
	                       "WHERE (repMO = @repMO and KIDdiv=@KIDDiv "+
                           "and KIDspc=@KIDspc and KIDdis=@KIDdis and KIDro=@KIDro)";
                var param = new 
                {
                    repMO = tmp.repMO,
                    KIDdiv = tmp.KIDdiv,
                    KIDspc = tmp.KIDspc,
                    KIDdis = tmp.KIDdis,
                    KIDro = tmp.KIDro
                };
                int count = _conn.QueryFirstOrDefault<int>(q,param);
                if (count == 0) { return true; } else { return false; }
            }
        }
    }
}
