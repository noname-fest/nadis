using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nadis.Models
{
    public class CtVet1d
    {
        [Display(Name = "guid Записи")]
        public Guid ID { get; set; }

        [Display(Name = "Периуд")]
        [DisplayFormat(DataFormatString = "{0:MMMyyyy}")]
        [Column(TypeName = "date")]
        public DateTime repMO { get; set; }

        [StringLength(7)]
        public string KIDro { get; set; }

        [Display(Name = "Айыл Аймак")]
        [StringLength(10)]
        public string KIDdiv { get; set; }
        public string KIDdivDisplay {get;set;} = "";


        [Display(Name = "Дата наблюдения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Column(TypeName = "date")]
        public DateTime dtObs { get; set; }

        [Display(Name = "Болезнь")]
        [StringLength(4)]
        public string KIDdis { get; set; } = ""; // bolezn
        public string KIDdisDisplay {get;set;} = "";

        //d3SANITARY
        [Display(Name = "Вид работы")]
        [StringLength(5)]
        public string KIDtyp { get; set; }
        public string KIDtypDisplay {get;set;}="";

        [Display(Name = "Обработано животноводческих помещений, территорий ферм")]
        [StringLength(300)]
        public string area { get; set; }


        [Display(Name = "Другие комментарии, информация")]
        [StringLength(300)]
        public string measure { get; set; }

        [Display(Name = "Кв. м.")]
        public int? m2 { get; set; }

    }
}
