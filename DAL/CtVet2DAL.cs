using Dapper;
using nadis.Models;
using nadis.tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using nadis.DAL.nadis;
using Microsoft.AspNetCore.Mvc.Rendering;
using nadis.Models.sp;

namespace nadis.DAL
{
    public static class CtVet2DAL
    {
       public static List<sp_values> __RepPerList(int YY, int MM)
        {

            List<sp_values> tmpList = new List<sp_values>();
            DateTime dtB = new DateTime(YY,MM,1);
            DateTime dtE =  DateTime.Today;
            while(dtB <= dtE)
            {
                sp_values tmp_sp = new sp_values();
                if (dtB.Month > 6) tmp_sp.ID = dtB.Year.ToString() + "/2";
                else tmp_sp.ID = dtB.Year.ToString() + "/1";
                tmp_sp.Text = tmp_sp.ID;
                dtB = dtB.AddMonths(6);
                tmpList.Add(tmp_sp);
            }
            return tmpList;
        }
        public static SelectList RepPerListList(int y,int m)
        {
            return new SelectList(__RepPerList(y,m), "ID", "Text");
        }



        public static IEnumerable<CtVet2> GetAll(string KIDro,int Y,int M)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                string q = "SELECT * FROM ctVet2 WHERE "+
                        "(KIDro=@KIDroP and LEFT(repPer,4)=@bDt) ORDER BY repPer DESC";
                var p = new 
                    {
                        KIDroP = KIDro, 
                        bDt = Y
                    };
                IEnumerable<CtVet2> tmpList = _conn.Query<CtVet2>(q,p);
                foreach(var tmp in tmpList)
                    {
                        tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                        tmp.KIDdtpDisplay = spDAL.KIDdtpName(tmp.KIDdtp);
                        tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                    };
                return tmpList;
            }
        }

        public static CtVet2 GetById(Guid id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                CtVet2 tmp = _conn.QueryFirstOrDefault<CtVet2>("SELECT * FROM ctVet2 WHERE ID=@idd",
                                new {idd = id});
                tmp.KIDdivDisplay = spDAL.KIDdivName(tmp.KIDdiv);
                tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                tmp.KIDspcDisplay = spDAL.KIDspcName(tmp.KIDspc);
                return tmp;
            }
            
        }

        public static void Delete(Guid id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                _conn.Execute("DELETE FROM ctVet2 WHERE ID=@idd",new{idd = id});
            }
        }      

        public static void Add(CtVet2 tmp)
        {
            if(tmp==null)return;
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                string q = "INSERT INTO ctVet2 "+ 
	                "(KIDro,repPer,KIDdiv,KIDspc,KIDdtp,"+
                            "dtObs,nA,nD,Notes) "+
	                    "VALUES "+
	                "(@KIDro,@repPer,@KIDdiv,@KIDspc,@KIDdtp,@dtObs,@nA,@nD,"+
                            "@Notes)";
                var param = new 
                {
                    KIDro = tmp.KIDro,
                    repPer = tmp.repPer,
                    KIDdiv = tmp.KIDdiv,
                    KIDspc = tmp.KIDspc,
                    KIDdtp = tmp.KIDdtp,
                    dtObs = tmp.dtObs,
                    nA = tmp.nA,
                    nD = tmp.nD,
                    Notes = tmp.Notes
                };
                _conn.Execute(q,param);
                _conn.Close();
            }
        }


        public static void Update(CtVet2 tmp)
        {
            if (tmp is null) { return; }
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                string q = "UPDATE ctVET2 SET "+ 
                            "KIDro=@KIDro,"+
                            "repPer=@repPer,"+
                            "KIDdiv=@KIDdiv,"+
                            "KIDspc=@KIDspc,"+
                            "KIDdtp=@KIDdtp,"+
                            "dtObs=@dtObs,"+
                            "nA=@nA,"+
                            "nD=@nD,"+
                            "Notes=@Notes  "+
                            "WHERE ID=@ID";
                var param = new
                {
                    KIDro = tmp.KIDro,
                    repPer = tmp.repPer,
                    KIDdiv = tmp.KIDdiv,
                    KIDspc = tmp.KIDspc,
                    KIDdtp = tmp.KIDdtp,
                    dtObs = tmp.dtObs,
                    nA = tmp.nA,
                    nD = tmp.nD,
                    Notes = tmp.Notes,
                    ID=tmp.ID
                };
                _conn.Execute(q,param);
                _conn.Close();
            }
        }

        public static bool IsUniqueRecord(CtVet2 tmp)
        {
            if (tmp == null) return false;
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                string q = "SELECT COUNT(ID) as kolvo FROM ctVET2 "+
	                       "WHERE (repPer = @repPer and KIDdiv=@KIDDiv "+
                           "and KIDspc=@KIDspc and KIDdtp=@KIDdtp and "+
                           "KIDro=@KIDro and dtObs=@dtObs)";
                var param = new 
                {
                    KIDro = tmp.KIDro,
                    repPer = tmp.repPer,
                    KIDdiv = tmp.KIDdiv,
                    KIDspc = tmp.KIDspc,
                    KIDdtp = tmp.KIDdtp,
                    dtObs = tmp.dtObs,
                };
                int count = _conn.QueryFirstOrDefault<int>(q,param);
                if (count == 0) { return true; } else { return false; }
            }
        }



    }
}