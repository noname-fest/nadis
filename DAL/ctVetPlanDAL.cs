using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using nadis.DAL;

namespace nadis.DAL.nadis
{
    public static class ctVetPlanDAL
    {
        public static bool IsUniqRecord(Guid id)
        {
            return false;
        }

        public static  IEnumerable<CtVetPlan> GetAll(string IDro, int PlY)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                string q = "SELECT * FROM ctVetPlan WHERE KIDro=@KIDroP and PlanYr=@PlanYearP";
                var param = new 
                    {
                        KIDroP = IDro,
                        PlanYearP = PlY
                    };
                
                IEnumerable<CtVetPlan> tmp = _conn.Query<CtVetPlan>(q,param);
                _conn.Close();
                
                foreach (CtVetPlan item in tmp)
                {
                    item.KIDtrtDisplay = spDAL.KIDtrtName(item.KIDtrt);
                    item.KIDdivDisplay = spDAL.KIDdivName(item.KIDdiv);
                    item.KIDspcDisplay = spDAL.KIDspcName(item.KIDspc);
                    item.KIDdisDisplay = spDAL.KIDdisName(item.KIDdis);
                }
                return tmp;
            }
        }
    }
}