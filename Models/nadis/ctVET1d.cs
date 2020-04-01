using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nadis.Models
{

    public partial class ctVET1d
    {
        [Display(Name = "guid Записи")]
        public Guid ID { get; set; }

        [Display(Name = "Айыл Аймак")]
        [StringLength(10)]
        public string KIDdiv { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dtObs { get; set; }

        [Display(Name = "Периуд")]
        [DisplayFormat(DataFormatString = "{0:MMMyyyy}")]
        [Column(TypeName = "date")]
        public DateTime repMO { get; set; }

        //d3SANITARY
        [StringLength(5)]
        public string KIDtyp { get; set; }

        [StringLength(300)]
        public string measure { get; set; }

        [StringLength(300)]
        public string area { get; set; }

        public int? m2 { get; set; }

        [StringLength(7)]
        public string KIDro { get; set; }
    }
}
