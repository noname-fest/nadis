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
                //sp_values tmp_sp = new sp_values
                //{
                //    ID = dtB.ToString().Trim(),
                //    Text = mmm[dtB.Month-1] + dtB.Year.ToString()
                //};
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
                        "(KIDro=@KIDroP and repMO between @bDt and @eDt) ORDER BY repMO DESC";
                var p = new 
                    {
                        KIDroP = KIDro, 
                        bDt = new DateTime(Y,M,1),
                        eDt = new DateTime(DateTime.Today.Year,
                                            DateTime.Today.Month+1,
                                            1) 
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



    }
}