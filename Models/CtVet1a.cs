using nadis.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace nadis.Models
{
    public class CtVet1a
    {
        private static List<sp_AiylAimaki> SPAa { get; set; } =  new List<sp_AiylAimaki>();
        public List<sp_AiylAimaki> getSPAa() { return SPAa; }
        public CtVet1a()
        {
            if(!(SPAa.Count>0)) 
            {
                var appSettingsJson = AppSettingJSON.GetAppSettings();
                var connectionString = appSettingsJson["DefaultConnection"];
                using (SqlConnection _conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_Get_SPAa", _conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@idL", "02-205");
                    _conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        sp_AiylAimaki tmp = new sp_AiylAimaki();
                        {
                            tmp.KID = dr["KID"].ToString();
                            tmp.name = dr["Socunit"].ToString();
                        }
                        SPAa.Add(tmp);
                    }
                    _conn.Close();
                }
            } 
        }

        public Guid ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM yyyy}")]
        public DateTime RepMO { get; set; } //periud dannix

        [StringLength(7)]
        public string KIDro { get; set; }     //code ???????
        //[StringLength(10)]
        public string KIDdiv { get; set; }  //raion a/a 

        //[StringLength(4)]
        public string KIDspc { get; set; } // vid jivotnogo
        
        //[StringLength(4)]
        public string KIDdis { get; set; } // bolezn


        public int? pos_units { get; set; }

        public int? positives { get; set; }

        public int? dead { get; set; }

        public int? end_pos_units { get; set; }

        public int? end_pos_animals { get; set; }

        public int? culled { get; set; }
        //public static List<SPAa> Aa { get; set; } = { list; }
    /*

    [Column(TypeName = "date")]
    public DateTime? dtObs { get; set; }


    [Column(TypeName = "date")]
    public DateTime? dtExp { get; set; }

    [Column(TypeName = "date")]
    public DateTime? dtCR { get; set; }

    [StringLength(10)]
    public string byCR { get; set; }

    [StringLength(10)]
    public string byCH { get; set; }

    public DateTime? dtCH { get; set; }

    [StringLength(7)]
    public string KIDro { get; set; }

    public bool? isExp { get; set; }

      */

}
}
