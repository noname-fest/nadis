using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using nadis.Models.sp;
using nadis.tools;

namespace nadis.DAL.nadis
{
  
    public static class spDAL
    {
        public static SelectList RepMO1YearList(int y)
        {
            List<sp_values> tmpList = new List<sp_values>();
            for (int i = 1; i < 13; i++)
            {
                sp_values tmp_sp = new sp_values
                {
                    ID   = new DateTime(y, i, 1).ToString(),
                    Text = new DateTime(y, i, 1).ToString("MMM yyyy")
                };
                tmpList.Add(tmp_sp);
            }
            return new SelectList(tmpList, "ID", "Text");
        }

        public static SelectList KIDdivList(string KIDro)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT gAID as KID,Socunit FROM[73GEO3] " +
                    "INNER JOIN[1VETUNITS] ON[1VETUNITS].KIDray =[73GEO3].gRID " +
                        "WHERE[1VETUNITS].KID = '"+KIDro+"'", _conn);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["KID"].ToString();
                        tmp.Text = dr["Socunit"].ToString().Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }

            return new SelectList(tmpList, "ID", "Text",tmpList[0].ID);
        }
        

        public static SelectList KIDspcList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT KID, Species FROM [d2SPECIES]", _conn);
                //{
                //    CommandType = CommandType.StoredProcedure
                //};
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["KID"].ToString();
                        tmp.Text = dr["Species"].ToString().Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return new SelectList(tmpList, "ID", "Text");
        }

        public static SelectList KIDdisList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT KID, Disease FROM [d2DISEASES]", _conn);
                //{
                //    CommandType = CommandType.StoredProcedure
                //};
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["KID"].ToString();
                        tmp.Text = dr["Disease"].ToString().Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return new SelectList(tmpList, "ID", "Text");
        }



        public static SelectList testList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT KID,Testname FROM [d2TESTS] ", _conn);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["KID"].ToString();
                        tmp.Text = dr["Testname"].ToString().Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }

            return new SelectList(tmpList, "ID", "Text", tmpList[0].ID);
        }

    }
}
