using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using nadis.Models.sp;
using nadis.tools;

namespace nadis.Models
{
  
    public class spDAL
    {
        public List<sp_values> GetSPList(int spIndex)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            List<sp.sp_values> tmpList = new List<sp.sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT KID,name FROM ", _conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        sp.sp_values tmp = new sp.sp_values();
                        tmp.KID = rd.GetValue(0).ToString().Trim();
                        tmp.name = rd.GetValue(1).ToString().Trim();
                        tmpList.Add(tmp);
                    }
                    return tmpList;
                }
                else return null;
            }

        }

    }
}
