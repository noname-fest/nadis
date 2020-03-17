using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using nadis.Models.sp;
using nadis.tools;

namespace nadis.Models
{
  
    public class spDAL
    {
        public SelectList RepMO1YearList()
        {
            int y = DateTime.Now.Year;
            List<sp_values> tmpList = new List<sp_values>();
            //sp_values sss = new sp_values();
            for (int i = 1; i < 13; i++)
            {
                sp_values tmp_sp = new sp_values();
                tmp_sp.ID = new DateTime(y, i, 1).ToString();
                //if (DateTime.Now.Month==i) { sss = tmp_sp; }
                tmp_sp.Text = new DateTime(y, i, 1).ToString("MMM yyyy");
                tmpList.Add(tmp_sp);
            }
            //string selv = new DateTime(y, DateTime.Now.Month, 1).ToString();
            //SelectList ttt = new SelectList(tmpList, "ID", "Text", sss);
            return new SelectList(tmpList, "ID", "Text");
        }
        public SelectList RepMO1YearList(Guid id)
        {
            int y = 0;
            if (id != null)
            {
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];

                using (SqlConnection _conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetYearById_CtVet1a", _conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@id", id);
                    _conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        y = (int)dr["Y"];
                    }
                    _conn.Close();
                }

            }
            else { y = DateTime.Now.Year; }
            //return new SelectList(tmpList, "ID", "Text");

            List<sp_values> tmpList = new List<sp_values>();
            for (int i = 1; i < 13; i++)
            {
                sp_values tmp_sp = new sp_values();
                tmp_sp.ID = new DateTime(y, i, 1).ToString();
                tmp_sp.Text = new DateTime(y, i, 1).ToString("MMM yyyy");
                tmpList.Add(tmp_sp);
            }
            return new SelectList(tmpList, "ID", "Text");
        }

        public SelectList KIDdivList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT gAID as KID,Socunit FROM[73GEO3] " +
                    "INNER JOIN[1VETUNITS] ON[1VETUNITS].KIDray =[73GEO3].gRID " +
                        "WHERE[1VETUNITS].KID = 'RD02205'", _conn);
                //{
                //    CommandType = CommandType.StoredProcedure
                //};
                //cmd.Parameters.AddWithValue("@idL", id);
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
            //SelectList ttt = new SelectList(tmpList, "ID", "Text");
            //ttt.SelectedValue

            return new SelectList(tmpList, "ID", "Text",tmpList[0].ID);
        }
        public SelectList KIDdivList(Guid id)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];

            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Get_SPAa_CtVet1a", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idL", id);
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
            return new SelectList(tmpList, "ID", "Text");
        }

        public SelectList KIDspcList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Get_SPVidJiv", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
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

        public SelectList KIDdisList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Get_SPBolezni", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
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

    }
}
