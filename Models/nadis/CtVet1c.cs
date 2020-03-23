    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


namespace nadis.Models
{
    public class CtVet1c
    {
        [Display(Name = "guid Записи")]
        //[ScaffoldColumn(false)]
        public Guid ID { get; set; }

        [Display(Name = " Периуд ")]
        [DisplayFormat(DataFormatString = "{0:MMM yyyy}")]
        public DateTime RepMO { get; set; } = DateTime.Today;//periud dannix

        //[ScaffoldColumn(false)]
        [StringLength(7)]
        public string KIDro { get; set; }     //code пользователя RD02525

        [Display(Name = "     Айыл Аймак     ")]
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

        [Display(Name="Дата наблюдения")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime dtObs { get; set; } = DateTime.Today;
        
        [Display(Name = "Мероприятие")]
        public string KIDtrtDisplay {get;set;}
        [StringLength(5)]
        public string KIDtrt { get; set; } = "";

        /*
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? nVT { get; set; }

        [Required(ErrorMessage = "Только целое число > 0")]
        public int treated { get; set; }
        */
        [Required(ErrorMessage = "Только целое число > 0")]
        public int tfemage1 { get; set; } = 0;

        [Required(ErrorMessage = "Только целое число > 0")]
        public int tfemage2 { get; set; } = 0;

        [Required(ErrorMessage = "Только целое число > 0")]
        public int tmalage1 { get; set; } = 0;

        [Required(ErrorMessage = "Только целое число > 0")]
        public int tmalage2 { get; set; } = 0;

        public const string test = "NOTS";

    }
}
