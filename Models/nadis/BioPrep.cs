using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nadis.Models
{

public class BioPrep
{
        public Guid ID { get; set; }

        //[StringLength(21)]
        //public string KID { get; set; }
        [StringLength(7)]
        public string KIDro { get; set; }

        [Display(Name = "     Айыл Аймак     ")]
        public string KIDdivDisplay { get; set; } = "";

        [StringLength(10)]
        //public string KIDdiv { get; set; }  //raion a/a 
        public string KIDdiv { get; set; } = "";

        [Display(Name = " Периуд ")]
        [DisplayFormat(DataFormatString = "{0:MMM yyyy}")]
        public DateTime RepMO { get; set; } = DateTime.Today;//periud dannix

        [StringLength(10)]
        public string VetPrep { get; set; } = "";
        
        [Display(Name="Вет. препарат")]
        public string VetPrepDisplay {get;set;} = "";
        //[Display(Name = " Вет. препарат ")]
        //public string VetPrepDisplay { get;set; } = "";

        [StringLength(5)]
        public string EdIzm { get; set; } = "";

        [Display(Name="Ед.изм")]
        public string EdIzmDisplay {get; set;}

        [Display(Name = "Было")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public long? Bylo { get; set; } = 0;

        [Display(Name = "Было на нач. года")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public long? Bylo_year { get; set; } = 0;

        [Display(Name = "Поступило всего")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public long? PostupiloVsego { get; set; } = 0;

        [Display(Name = "Поступило разнарядка")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public long? PostupiloRazn { get; set; } = 0;

        [Display(Name = "Израсход. всего")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public long? IzrashodVsego { get; set; } = 0;

        [Display(Name = "Израсход. другое")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public long? IzrashodDrugoe { get; set; } = 0;

        [Display(Name = "Остаток за месяц")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public long? Ostatok_za_mesyac { get; set; } = 0;

        /*
        [Display(Name = "Израсход. другое")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public long? Ostatok { get; set; }
        */
}
}