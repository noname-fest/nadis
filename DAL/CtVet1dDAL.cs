using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using nadis.tools;
using nadis.Models.sp;
using System;

namespace nadis.DAL.nadis
{
    public static class CtVet1dDAL
    {
        public static IEnumerable<CtVet1d> GetAll_CtVet1d(string KIDro,int Y,int M)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "SELECT * FROM CtVet1d WHERE (KIDro=@KIDroP and repMO between @bDt and @eDt) ORDER BY repMO DESC";
                var param = new {
                    KIDroP = KIDro,
                    bDt = new DateTime(Y,M,1),
                    eDt = new DateTime(DateTime.Today.Year, DateTime.Today.Month,1)
                };
                IEnumerable<CtVet1d> tmpList = _conn.Query<CtVet1d>(q,param);
                foreach(var tmp in tmpList)
                {
                    tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                    tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                    tmp.KIDtypDisplay = spDAL.KIDtypName(tmp.KIDtyp);
                }
                return tmpList;
            }
        }

        public static CtVet1d GetByID_CtVet1d(Guid id)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "SELECT * FROM CtVet1d WHERE ID=@Idd";
                var param = new {
                    Idd = id
                };
                var tmp = _conn.QueryFirst<CtVet1d>(q,param);
                tmp.KIDdisDisplay = spDAL.KIDdisName(tmp.KIDdis);
                tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                tmp.KIDtypDisplay = spDAL.KIDtypName(tmp.KIDtyp);
                return tmp;
            }
        }

        public static void Update(CtVet1d tmp)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "UPDATE ctVet1d SET "+
                " KIDro=@KIDro,repMO=@repMO,KIDdiv=@KIDdiv,"+
                " dtObs=@dtObs,KIDdis=@KIDdis,KIDtyp=@KIDtyp,"+
                " area=@area,measure=@measure,m2=@m2 "+
                " WHERE Id=@idd";
                var param = new 
                {
                    KIDro = tmp.KIDro,
                    repMO = tmp.repMO,
                    KIDdiv = tmp.KIDdiv,
                    dtObs = tmp.dtObs,
                    KIDdis = tmp.KIDdis,
                    KIDtyp = tmp.KIDtyp,
                    area = tmp.area,
                    measure = tmp.measure,
                    m2 = tmp.m2,
                    idd = tmp.ID
                };
                _conn.Execute(q,param);
                _conn.Close();
            }
        }

        public static bool IsUniqueRecord(CtVet1d tmp)
        {
            //проверка на существования аналогичной записи
            int count = 0;
            using(SqlConnection _conn_check = new SqlConnection(spDAL.connStr))
            {
                count = _conn_check.QueryFirst<int>("SELECT COUNT(*) FROM CtVet1d WHERE ("
                + "repMO=@repMOP and KIDro=@KIDroP and KIDdiv=@KIDdivP and KIDdis=@KIDdisP"
                + " and dtObs=@dtObsP and KIDtyp=@KIDtypP)",
                new {   repMOP = tmp.repMO, KIDroP = tmp.KIDro, KIDdivP = tmp.KIDdiv,
                        KIDdisP = tmp.KIDdis, dtObsP= tmp.dtObs, KIDtypP=tmp.KIDtyp });
                _conn_check.Close();
                if (count == 0) return true;else return false;
            }
        }

        public static void Add(CtVet1d tmp)
        {
            if(tmp is null) return;
            string q = "INSERT INTO ctVet1d (repMO,KIDro,KIDdiv,dtObs,KIDdis,"
            + "KIDtyp,area,measure,m2) VALUES (@repMO,@KIDro,@KIDdiv,@dtObs,"
            + "@KIDdis,@KIDtyp,@area,@measure,@m2)";
            var p = new
            {
               repMO = tmp.repMO,
               KIDro = tmp.KIDro,
               KIDdiv = tmp.KIDdiv,
               dtObs = tmp.dtObs,
               KIDdis = tmp.KIDdis,
               KIDtyp = tmp.KIDtyp,
               area = tmp.area,
               measure = tmp.measure,
               m2 = tmp.m2
            };
            using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
               _conn.Execute(q, p);
               _conn.Close();
            } 
        }

        public static void Delete(Guid id)
        {
            if (id == null) return;
            using (SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                _conn.Execute("DELETE FROM ctVet1d WHERE ID=@IDR", new { IDR = id });
            }
        }
    }
}
