using nadis.Models;
using nadis.tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace nadis.DAL.nadis
{
    public static class CtVet1bDAL
    {
        public static IEnumerable<CtVet1b> GetAll_CtVet1b(string KIDro)//, string repMO)
        {

            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            List<CtVet1b> tmpList = new List<CtVet1b>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("nadis_GetAll_CtVet1b", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@KIDro", KIDro);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CtVet1b tmp = new CtVet1b()
                    {
                        ID = Guid.Parse(dr["ID"].ToString().Trim()),
                        KIDro = dr["KIDro"].ToString().Trim(),
                        RepMO = (DateTime)dr["repMO"],

                        KIDdiv = dr["KIDdiv"].ToString().Trim(),
                        KIDdivDisplay = dr["KIDdivDisplay"].ToString().Trim(),

                        KIDspc = dr["KIDspc"].ToString().Trim(),
                        KIDspcDisplay = dr["KIDspcDisplay"].ToString().Trim(),

                        KIDdis = dr["KIDdis"].ToString().Trim(),
                        KIDdisDisplay = dr["KIDdisDisplay"].ToString().Trim(),

                        test = dr["test"].ToString().Trim(),
                        testDisplay = dr["testDisplay"].ToString().Trim(),

                        femage_1 = (int?)dr["femage_1"],
                        femage_2 = (int?)dr["femage_2"],
                        fage1_pos = (int?)dr["fage1_pos"],
                        fage2_pos = (int?)dr["fage2_pos"],
                        //if (tmp.dtObs is null) { tmp.dtObs = DateTime.Today; } else tmp.dtObs = DateTime.Today;
                        dtObs = dr["dtObs"] is null ? DateTime.Today : (DateTime)(dr["dtObs"])
                        //dtObs = (DateTime)(dr["dtObs"])
                };
                    
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return tmpList;
        }


        public static void Add_CtVet1b(CtVet1b tmp)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using SqlConnection _conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_Add_CtVet1b", _conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@KIDro", tmp.KIDro);
            cmd.Parameters.AddWithValue("@repMO", tmp.RepMO);
            cmd.Parameters.AddWithValue("@KIDdiv", tmp.KIDdiv);
            cmd.Parameters.AddWithValue("@KIDspc", tmp.KIDspc);
            cmd.Parameters.AddWithValue("@KIDdis", tmp.KIDdis);
            cmd.Parameters.AddWithValue("@test", tmp.test);
            cmd.Parameters.AddWithValue("@femage_1", tmp.femage_1);
            cmd.Parameters.AddWithValue("@femage_2", tmp.femage_2);
            cmd.Parameters.AddWithValue("@fage1_pos", tmp.fage1_pos);
            cmd.Parameters.AddWithValue("@fage2_pos", tmp.fage2_pos);
            cmd.Parameters.AddWithValue("@dtObs", tmp.dtObs);

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();

        }


        public static CtVet1b GetCtVet1bById(Guid id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            CtVet1b tmp = new CtVet1b();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetById_CtVet1b", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", id);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tmp.ID = Guid.Parse(dr["ID"].ToString());
                    tmp.KIDro = dr["KIDro"].ToString();
                    tmp.RepMO = (DateTime)dr["repMO"];

                    tmp.KIDdiv = dr["KIDdiv"].ToString();
                    tmp.KIDdivDisplay = dr["KIDdivDisplay"].ToString();

                    tmp.KIDspc = dr["KIDspc"].ToString();
                    tmp.KIDspcDisplay = dr["KIDspcDisplay"].ToString();

                    tmp.KIDdis = dr["KIDdis"].ToString();
                    tmp.KIDdisDisplay = dr["KIDdis"].ToString();

                    tmp.test = dr["test"].ToString();
                    tmp.testDisplay = dr["testDisplay"].ToString();

                    tmp.femage_1 = (int?)dr["femage_1"];
                    tmp.femage_2 = (int?)dr["femage_2"];
                    tmp.fage1_pos = (int?)dr["fage1_pos"];
                    tmp.fage2_pos = (int?)dr["fage2_pos"];

                    tmp.dtObs = (DateTime)dr["dtObs"];
                }
                _conn.Close();
            }
            return tmp;
        }


        public static void UpdateCtVet1b(CtVet1b tmp)
        {
            if (tmp is null) { return; }
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using SqlConnection _conn = new SqlConnection(connectionString);
            using (SqlCommand cmd = new SqlCommand("sp_Update_CtVet1b", _conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                cmd.Parameters.AddWithValue("@ID", tmp.ID);
                cmd.Parameters.AddWithValue("@KIDro", tmp.KIDro);
                cmd.Parameters.AddWithValue("@repMO", tmp.RepMO);
                cmd.Parameters.AddWithValue("@KIDdiv", tmp.KIDdiv);
                cmd.Parameters.AddWithValue("@KIDspc", tmp.KIDspc);
                cmd.Parameters.AddWithValue("@KIDdis", tmp.KIDdis);
                cmd.Parameters.AddWithValue("@femage_1", tmp.femage_1);
                cmd.Parameters.AddWithValue("@femage_2", tmp.femage_2);
                cmd.Parameters.AddWithValue("@fage1_pos", tmp.fage1_pos);
                cmd.Parameters.AddWithValue("@fage2_pos", tmp.fage2_pos);
                cmd.Parameters.AddWithValue("@dtObs", tmp.dtObs);
                cmd.Parameters.AddWithValue("@test", tmp.test);

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            _conn.Close();
        }

        public static void DeleteCtVet1b(Guid id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using SqlConnection _conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_Delete_CtVet1b", _conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ID", id);

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }
    }
}
