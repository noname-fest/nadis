using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace nadis.Models
{
    public class CtVet1aDAL
    {
        readonly string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=nadis;Integrated Security=True";

        public IEnumerable<CtVet1a> GetAllCtVet1a(string KIDro, string repMO)
        {
            List<CtVet1a> tmpList = new List<CtVet1a>();
            using(SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Get_CtVet1a", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@KIDro", KIDro);
                cmd.Parameters.AddWithValue("@repMO", repMO);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CtVet1a tmp = new CtVet1a
                    {
                        ID = Guid.Parse(dr["ID"].ToString()),
                        GID = dr["GID"].ToString(),
                        RepMO = (DateTime)dr["repMO"],
                        KIDdiv = dr["KIDdiv"].ToString(),
                        KIDspc = dr["KIDspc"].ToString()
                    };

                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return tmpList;
        }

        public void AddCtVet1a(CtVet1a tmp)
        {
            /*
            using (SqlConnection _conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Add_CtVet1a", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@KIDdiv", tmp.KIDdiv);
                cmd.Parameters.AddWithValue("@repMO", tmp.RepMO);

                _conn.Open();
                cmd.ExecuteNonQuery();
                _conn.Close();
            }
            */

            using SqlConnection _conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_Add_CtVet1a", _conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@KIDdiv", tmp.KIDdiv);
            cmd.Parameters.AddWithValue("@repMO", tmp.RepMO);

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }
    }
}
