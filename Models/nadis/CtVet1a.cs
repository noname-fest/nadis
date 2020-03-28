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

        [Display(Name = " Периуд ")]
        [DisplayFormat(DataFormatString = "{0:MMMyyyy}")]
        public DateTime RepMO { get; set; } = DateTime.Today;//periud dannix

        //[ScaffoldColumn(false)]
        [StringLength(7)]
        public string KIDro { get; set; }     //code пользователя RD02525

        [Display(Name = "     Айыл Аймак     ")]
        //[StringLength(30)]
        public string KIDdivDisplay { get; set; } = "";

        [StringLength(10)]
        //public string KIDdiv { get; set; }  //raion a/a 
        public string KIDdiv { get; set; } = "";

        [Display(Name = "Вид")]
        public string KIDspcDisplay { get; set; } = "";

        [StringLength(4)]
        public string KIDspc { get; set; } = ""; // vid jivotnogo

        [Display(Name = "Болезнь")]
        public string KIDdisDisplay { get; set; } = "";
        [StringLength(4)]
        public string KIDdis { get; set; } = ""; // bolezn

        [Display(Name = "Выявл. небл. пунк.")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? pos_units { get; set; } = 0;

        [Display(Name = "Забол. гол.")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? positives { get; set; } = 0;

        [Display(Name = "Пало гол.")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? dead { get; set; } = 0;

        [Display(Name = "Небл. пункт.")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? end_pos_units { get; set; } = 0;

        [Display(Name = "Больных")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? end_pos_animals { get; set; } = 0;

        [Display(Name = "Забито")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? culled { get; set; } = 0;
    }
}
