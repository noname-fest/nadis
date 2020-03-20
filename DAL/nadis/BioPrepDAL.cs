using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using nadis.tools;
using nadis.Models.sp;

namespace nadis.DAL.nadis
{
    public static class BioPrepDAL
    {
        public static IEnumerable<BioPrep> GetAll_BioPrep(string KIDro)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            //List<BioPrep> tmpList = new List<BioPrep>();
            /*
            using (SqlConnection _conn = new SqlConnection(connectionString))
                {
                    var bio = _conn.Query<BioPrep>("SELECT * FROM BioPrep WHERE KIDro=@KIDroP", new{ KIDroP = KIDro});
                    return bio;
                }
            */
            SqlConnection _conn = new SqlConnection(connectionString);
            IEnumerable<BioPrep> tmpList = 
                _conn.Query<BioPrep>("SELECT * FROM BioPrep WHERE KIDro=@KIDroP ORDER BY repMO DESC", new { KIDroP = KIDro });
            _conn.Close();
            foreach(var tmp in tmpList)
            {
                tmp.KIDdivDisplay = spDAL.DivDisplay(tmp.KIDro, tmp.KIDdiv);
            };
            return tmpList;
        }
    }
}