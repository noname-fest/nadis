using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using nadis.DAL;
using Dapper.Contrib.Extensions;
using System.Text;

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
        
        public static  IEnumerable<CtVetPlan> GetAll_CtVetPlan(string IDro, string PlY,
                                                                string fdiv,
                                                                string trt,
                                                                string spc,
                                                                string dis
                                                                )
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                //string q = "SELECT * FROM ctVetPlan WHERE KIDro=@KIDroP and PlanYr=@PlanYearP";
                
                var q = new StringBuilder();
                q.Append("SELECT * FROM ctVetPlan WHERE (KIDro=@KIDroP and PlanYr=@PlanYearP ");
                if(fdiv.Length!=0)
                        q.Append(" and KIDdiv='"+fdiv+"' ");
                if(trt.Length!=0)
                        q.Append(" and KIDtrt='"+trt+"' " );
                if(spc.Length!=0)
                        q.Append(" and KIDspc='"+spc+"'");
                if(dis.Length!=0)
                        q.Append(" and KIDdis='"+dis+"'");
                        q.Append(" )");
                
                var param = new 
                    {
                        KIDroP = IDro,
                        PlanYearP = PlY
                    };
                
                IEnumerable<CtVetPlan> tmp = _conn.Query<CtVetPlan>(q.ToString(),param);
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
                tmp.nPlan_I = tmp.tfemage1_I + tmp.tfemage2_I;
                tmp.nPlan_II = tmp.tfemage1_II + tmp.tfemage2_II;
                tmp.nPlan_III = tmp.tfemage1_III + tmp.tfemage2_III;
                tmp.nPlan_IV = tmp.tfemage1_IV + tmp.tfemage2_IV;

                tmp.nPlan = tmp.nPlan_I+tmp.nPlan_II+tmp.nPlan_III+tmp.nPlan_IV;
                _conn.Insert<CtVetPlan>(tmp);
            }
        }

        public static void Update_CtVetPlan(CtVetPlan tmp)
        {
            using(SqlConnection _conn = new SqlConnection(spDAL.connStr))
            {
                tmp.nPlan_I = tmp.tfemage1_I + tmp.tfemage2_I;
                tmp.nPlan_II = tmp.tfemage1_II + tmp.tfemage2_II;
                tmp.nPlan_III = tmp.tfemage1_III + tmp.tfemage2_III;
                tmp.nPlan_IV = tmp.tfemage1_IV + tmp.tfemage2_IV;

                tmp.nPlan = tmp.nPlan_I+tmp.nPlan_II+tmp.nPlan_III+tmp.nPlan_IV;                
                _conn.Update<CtVetPlan>(tmp);
            }
        }
    }
}