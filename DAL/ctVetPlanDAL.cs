using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using nadis.DAL;
using Dapper.Contrib.Extensions;

namespace nadis.DAL
{
    public static class ctVetPlanDAL
    {
        public static bool IsUniqRecord(Guid id)
        {
            return false;
        }

        /*
        public static bool IsUniqRecord(CtVetPlan tmp)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                int count = _conn.QueryFirstOrDefault<int>(
                    "SELECT COUNT(*) from CtVetPlan WHERE ( "+
                    " repMO=@repMOp and KIDro=@KIDrop and )",
                new 
                    {

                    }
                );
            }
        } */
        
        public static  IEnumerable<CtVetPlan> GetAll_CtVetPlan(string IDro, int PlY)
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
                /*
                foreach (CtVetPlan item in tmp)
                {
                    item.KIDtrtDisplay = spDAL.KIDtrtName(item.KIDtrt);
                    item.KIDdivDisplay = spDAL.KIDdivName(item.KIDdiv);
                    item.KIDspcDisplay = spDAL.KIDspcName(item.KIDspc);
                    item.KIDdisDisplay = spDAL.KIDdisName(item.KIDdis);
                    item.KIDtestDisplay= spDAL.testName(item.KIDtest);
                }*/
                return tmp;
            }
        }

        public static CtVetPlan GetById_CtVetPlan(Guid id)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                return _conn.Get<CtVetPlan>(id);
            }
        }

        public static void Delete_CtVetPlan(Guid Did)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                if(Did == null) return;
                _conn.Delete<CtVetPlan>(new CtVetPlan {ID = Did});
            }
        }

        public static void Add_CtVetPlan(CtVetPlan tmp)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                _conn.Insert<CtVetPlan>(tmp);
            }
        }

        public static void Update_CtVetPlan(CtVetPlan tmp)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                _conn.Update<CtVetPlan>(tmp);
            }
        }
    }
}