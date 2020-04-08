using nadis.Models.sp;
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
        [Display(Name = "guid Записи")]
        //[ScaffoldColumn(false)]
        public Guid ID { get; set; }

        [Display(Name = "repMO")]
        [DisplayFormat(DataFormatString = "{0:MMMyyyy}")]
        public DateTime repMO { get; set; } = DateTime.Today;//periud dannix

        //[ScaffoldColumn(false)]
        [StringLength(7)]
        public string KIDro { get; set; }     //code пользователя RD02525

        [Display(Name = "Айыл Аймак")]
        //[StringLength(30)]
        public string KIDdivDisplay { get; set; } = "";

        [Display(Name = "KIDdiv")]
        [StringLength(10)]
        public string KIDdiv { get; set; } = "";

        [Display(Name = "Вид")]
        public string KIDspcDisplay { get; set; } = "";

        [Display(Name = "KIDspc")]
        [StringLength(4)]
        public string KIDspc { get; set; } = ""; // vid jivotnogo

        [Display(Name = "Болезнь")]
        public string KIDdisDisplay { get; set; } = "";
        [Display(Name = "KIDdis")]
        [StringLength(4)]
        public string KIDdis { get; set; } = ""; // bolezn

        //[Display(Name = "Небл.пункт")]
        [Display(Name = "pos_units")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? pos_units { get; set; } = 0;

        //[Display(Name = "Забол.")]
        [Display(Name = "positives")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? positives { get; set; } = 0;

        //[Display(Name = "Пало")]
        [Display(Name = "dead")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? dead { get; set; } = 0;

        //[Display(Name = "Небл.пункт")]
        [Display(Name = "end_pos_units")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? end_pos_units { get; set; } = 0;

        //[Display(Name = "Больных")]
        [Display(Name = "end_pos_animals")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? end_pos_animals { get; set; } = 0;

        //[Display(Name = "Забито")]
        [Display(Name = "culled")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? culled { get; set; } = 0;
    }
}
