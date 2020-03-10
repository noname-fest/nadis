using System;
using System.ComponentModel.DataAnnotations;

namespace nadis.Models
{
    public class CtVet1a
    {
        public Guid ID { get; set; }

        [StringLength(21)]
        public string GID { get; set; }

        [StringLength(13)]
        public string KIDvil { get; set; }

        [StringLength(10)]
        public string KIDdiv { get; set; }

        [StringLength(4)]
        public string KIDspc { get; set; }

        [StringLength(4)]
        public string KIDdis { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dtObs { get; set; }

        [Column(TypeName = "date")]
        public DateTime? repMO { get; set; }

        public int? pos_units { get; set; }

        public int? positives { get; set; }

        public int? dead { get; set; }

        public int? end_pos_units { get; set; }

        public int? end_pos_animals { get; set; }

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

        public int? culled { get; set; }

    }
}
