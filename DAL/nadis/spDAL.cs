using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using nadis.Models.sp;
using nadis.tools;
using Dapper;

namespace nadis.DAL.nadis
{

    public static class spDAL
    {

        public static SelectList KIDroList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using SqlConnection _conn = new SqlConnection(connectionString);
            var tmp = _conn.Query<sp_values>("SELECT KID as ID, [VetUnit] as Text FROM [1VETUNITS]");
            List<sp_values> tL = new List<sp_values>();
            foreach (var tt in tmp)
            {
                tL.Add(tt);
            }
            return new SelectList(tL, "ID", "Text");
        }
        
        //d3PREVMEASURES
        public static SelectList KIDtrtList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using SqlConnection _conn = new SqlConnection(connectionString);
            var tmp = _conn.Query<sp_values>("SELECT KID as ID, [Measure] as Text FROM [d3PREVMEASURES]");
            List<sp_values> tL = new List<sp_values>();
            foreach (var tt in tmp)
            {
                tL.Add(tt);
            }
            return new SelectList(tL, "ID", "Text");
        }
        
        public static string KIDtrtName(string KIDtrt)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                string rez = _conn.QueryFirst<string>(
                        "SELECT TOP 1 [Measure] FROM [d3PREVMEASURES] WHERE [KID]=@val", new { val = KIDtrt});
                return rez;
            }
        }

        public static string KIDdisName(string KIDdis)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                string rez = _conn.QueryFirst<string>(
                        "SELECT TOP 1 [Disease] FROM [d2DISEASES] WHERE [KID]=@val", new { val = KIDdis});
                return rez;
            }
        }


        public static string KIDdivName(string KIDdiv)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                string rez = _conn.QueryFirst<string>(
                        "SELECT TOP 1 [Socunit] FROM [73GEO3] WHERE [gAID]=@val", new { val = KIDdiv});
                return rez;
            }
        }
        
        public static string KIDspcName(string KIDspc)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                string rez = _conn.QueryFirst<string>(
                        "SELECT TOP 1 [Species] FROM [d2SPECIES] WHERE KID=@val", new { val = KIDspc});
                return rez;
            }
        }

        public static SelectList VetPrepList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using SqlConnection _conn = new SqlConnection(connectionString);
            var tmp = _conn.Query<sp_values>("SELECT KID as ID, PrepType as Text FROM VetPrep");
            List<sp_values> tL = new List<sp_values>();
            foreach (var tt in tmp)
            {
                tL.Add(tt);
            }
            return new SelectList(tL, "ID", "Text");
        }

        public static SelectList EdIzmList()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using SqlConnection _conn = new SqlConnection(connectionString);
            var tmp = _conn.Query<sp_values>("SELECT KID as ID, EdIzm as Text FROM EdIzm");
            List<sp_values> tL = new List<sp_values>();
            foreach (var tt in tmp) tL.Add(tt);
            return new SelectList(tL, "ID", "Text");
        }

        public static string VetPrepName(string KIDVetPrep)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                string VetUnit = _conn.QueryFirst<string>(
                        "SELECT TOP 1 PrepType FROM [VetPrep] WHERE KID=@VetPP", new { VetPP = KIDVetPrep});
                return VetUnit;
            }
        }

        public static string EdIzmName(string KIDEdIzm)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                return _conn.QueryFirst<string>(
                        "SELECT TOP 1 EdIzm FROM [EdIzm] WHERE KID=@VetPP", new { VetPP = KIDEdIzm });
            }
        }

        public static List<sp_values> __RepMO1YearList(int y)
        {
            List<sp_values> tmpList = new List<sp_values>();
            for (int i = 1; i < 13; i++)
            {
                sp_values tmp_sp = new sp_values
                {
                    ID = new DateTime(y, i, 1).ToString(),
                    Text = new DateTime(y, i, 1).ToString("MMM yyyy")
                };
                tmpList.Add(tmp_sp);
            }
            return tmpList;
        }
        public static SelectList RepMO1YearList(int y)
        {
            /*
            List<sp_values> tmpList = new List<sp_values>();
            for (int i = 1; i < 13; i++)
            {
                sp_values tmp_sp = new sp_values
                {
                    ID = new DateTime(y, i, 1).ToString(),
                    Text = new DateTime(y, i, 1).ToString("MMM yyyy")
                };
                tmpList.Add(tmp_sp);
            }
            */
            return new SelectList(__RepMO1YearList(y), "ID", "Text");
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
                SqlCommand cmd = new SqlCommand(@"SELECT KID,Testname FROM [d2TESTS]", _conn);
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

        public static string testName(string tst)
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            var connectionString = appSettingsJson["DefaultConnection"];
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                string t = _conn.QueryFirstOrDefault<string>(
                        "SELECT TOP 1 Testname FROM [d2TESTS] WHERE KID=@tt", new { tt = tst});
                return t;
            }
        }        

    }
}
