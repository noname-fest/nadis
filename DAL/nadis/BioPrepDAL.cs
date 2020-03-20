using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using nadis.Models;
using nadis.tools;

namespace nadis.DAL.nadis
{
    public static class BioPrepDAL
    {
        public static IEnumerable<BioPrep> GetAll_BioPrep(string KIDro)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            List<BioPrep> tmpList = new List<BioPrep>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
                {
                    var bio = _conn.Query<BioPrep>("SELECT * FROM BioPrep WHERE KIDro=@KIDroP", new{ KIDroP = KIDro});
                    return bio;
                }
        }
    }
}