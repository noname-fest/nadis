﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using nadis.tools;

namespace nadis.Models
{
    public class CtVet1aDAL
    {
        //readonly string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=nadis;Integrated Security=True";
        /*
        public List<sp.sp_values> GetSPValues(string sp_name, string sp_table_name)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            List<sp.sp_values> tmpList = new List<sp.sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT KID,name FROM " + sp_table_name, _conn);
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
        */
        public IEnumerable<CtVet1a> GetAllCtVet1a(string KIDro)//, string repMO)
        {

            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            List<CtVet1a> tmpList = new List<CtVet1a>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Get_CtVet1a", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@KIDro", KIDro);
                //cmd.Parameters.AddWithValue("@repMO", repMO);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CtVet1a tmp = new CtVet1a
                    {
                        ID              = Guid.Parse(dr["ID"].ToString().Trim()),
                        KIDro           = dr["KIDro"].ToString().Trim(),
                        RepMO           = (DateTime)dr["repMO"],

                        KIDdiv          = dr["KIDdiv"].ToString().Trim(),
                        KIDdivDisplay   = dr["KIDdivDisplay"].ToString().Trim(),

                        KIDspc          = dr["KIDspc"].ToString().Trim(),
                        KIDspcDisplay   = dr["KIDspcDisplay"].ToString().Trim(),

                        KIDdis          = dr["KIDdis"].ToString().Trim(),
                        KIDdisDisplay   = dr["KIDdisDisplay"].ToString().Trim(),

                        pos_units       = (int?)dr["pos_units"],
                        positives       = (int?)dr["positives"],
                        dead            = (int?)dr["dead"],
                        end_pos_units   = (int?)dr["end_pos_units"],
                        end_pos_animals = (int?)dr["end_pos_animals"],
                        culled          = (int?)dr["culled"]
                    };

                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return tmpList;
        }

        public void AddCtVet1a(CtVet1a tmp)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using SqlConnection _conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_Add_CtVet1a", _conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@KIDro", /*tmp.KIDro*/"RD02205");
            cmd.Parameters.AddWithValue("@repMO", tmp.RepMO);
            cmd.Parameters.AddWithValue("@KIDdiv", tmp.KIDdiv);
            cmd.Parameters.AddWithValue("@KIDspc", tmp.KIDspc);
            cmd.Parameters.AddWithValue("@KIDdis", tmp.KIDdis);
            cmd.Parameters.AddWithValue("@pos_units", tmp.pos_units);
            cmd.Parameters.AddWithValue("@positives", tmp.positives);
            cmd.Parameters.AddWithValue("@dead", tmp.dead);
            cmd.Parameters.AddWithValue("@end_pos_units", tmp.end_pos_units);
            cmd.Parameters.AddWithValue("@end_pos_animals", tmp.end_pos_animals);
            cmd.Parameters.AddWithValue("@culled", tmp.culled);

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();

        }

        public void UpdateCtVet1a(CtVet1a tmp)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using SqlConnection _conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_Update_CtVet1a", _conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ID", tmp.ID);
            cmd.Parameters.AddWithValue("@KIDro",tmp.KIDro);
            cmd.Parameters.AddWithValue("@repMO", tmp.RepMO);
            cmd.Parameters.AddWithValue("@KIDdiv", tmp.KIDdiv);
            cmd.Parameters.AddWithValue("@KIDspc", tmp.KIDspc);
            cmd.Parameters.AddWithValue("@KIDdis", tmp.KIDdis);
            cmd.Parameters.AddWithValue("@pos_units", tmp.pos_units);
            cmd.Parameters.AddWithValue("@positives", tmp.positives);
            cmd.Parameters.AddWithValue("@dead", tmp.dead);
            cmd.Parameters.AddWithValue("@end_pos_units", tmp.end_pos_units);
            cmd.Parameters.AddWithValue("@end_pos_animals", tmp.end_pos_animals);
            cmd.Parameters.AddWithValue("@culled", tmp.culled);

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public void DeleteCtVet1a(Guid id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            using SqlConnection _conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_Delete_CtVet1a", _conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ID", id);
            
            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public CtVet1a GetCtVet1aById(Guid id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            CtVet1a tmp = new CtVet1a();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetById_CtVet1a", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", id);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tmp.ID              = Guid.Parse(dr["ID"].ToString());
                    tmp.KIDro           = dr["GID"].ToString();
                    tmp.RepMO           = (DateTime)dr["repMO"];

                    tmp.KIDdiv          = dr["KIDdiv"].ToString();
                    tmp.KIDdivDisplay   = dr["KIDdivDisplay"].ToString();

                    tmp.KIDspc          = dr["KIDspc"].ToString();
                    tmp.KIDspcDisplay   = dr["KIDspcDisplay"].ToString();

                    tmp.KIDdis          = dr["KIDdis"].ToString();
                    tmp.KIDdisDisplay   = dr["KIDdis"].ToString();

                    tmp.pos_units   = (int?)dr["pos_units"];
                    tmp.positives   = (int?)dr["positives"];
                    tmp.dead        = (int?)dr["dead"];
                    tmp.end_pos_units = (int?)dr["end_pos_units"];
                    tmp.end_pos_animals = (int?)dr["end_pos_animals"];
                    tmp.culled = (int?)dr["culled"];
                }
                _conn.Close();
            }
            return tmp;
        }
    }
}
