using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace nadis.Models
{
    public class CtVet1b
    {
        [Display(Name = "Guid записи")]
        public Guid ID { get; set; }


        [Display(Name = "  Периуд  ")]
        [DisplayFormat(DataFormatString = "{0:MMM yyyy}")]
        public DateTime RepMO { get; set; } = DateTime.Now;//periud dannix


        [Display(Name = "      Айыл Аймак      ")]
        public string KIDdivDisplay { get; set; } = "";
        [StringLength(10)]
        public string KIDdiv { get; set; } // AA code


        [Display(Name = "Вид")]
        public string KIDspcDisplay { get; set; } = "";
        [StringLength(4)]
        public string KIDspc { get; set; } = "";// vid jivotnih

        [Display(Name = "Болезнь")]
        public string KIDdisDisplay { get; set; } = "";
        [StringLength(4)]
        public string KIDdis { get; set; } = ""; // bolezn

        [Display(Name = "Дата наблюдения")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime? dtObs { get; set; } = DateTime.Today;  //Дата наблюдения

        [Display(Name = " Периуд ")]
        [DisplayFormat(DataFormatString = "{0:MMM yyyy}")]
        [Column(TypeName = "date")]
        public DateTime? repMO { get; set; } = DateTime.Today; // периуд записи

        [StringLength(7)]
        public string KIDro { get; set; }  // code raion

        [Display(Name = "Метод исследования")]
        public string testDisplay { get; set; }
        [StringLength(5)]
        public string test { get; set; } = ""; //Метод исследования

        //public int? pos_tot { get; set; } fage1_pos + fage2_pos
        //public int? test_tot { get; set; } femage_1 + femage_2

        [Display(Name = "Молодняк")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? femage_1 { get; set; } = 0;

        [Display(Name = "Взрослые особи")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? femage_2 { get; set; } = 0;

        [Display(Name = "Молодняк")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? fage1_pos { get; set; } = 0;

        [Display(Name = "Взрослые особи")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? fage2_pos { get; set; } = 0;

        public static string KIDtrt = "DIAG"; ///???????

    }
}
