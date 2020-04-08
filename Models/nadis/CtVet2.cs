using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nadis.Models
{
    [Table ("ctVet2")]
    public class CtVet2
    {
        [Key]
        [Display(Name = "guid Записи")]
        //[ScaffoldColumn(false)]
        public Guid ID { get; set; }

        [Display(Name = "Периуд")]
        public string repPer { get; set; } = "";//periud dannix

        //[ScaffoldColumn(false)]
        [StringLength(7)]
        public string KIDro { get; set; }     //code пользователя RD02525

        [Display(Name = "Айыл Аймак")]
        [StringLength(10)]
        public string KIDdiv { get; set; } = ""; // AA code
        public string KIDdivDisplay {get;set;} = "";
        
        [Display(Name = "Дата наблюд.")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Column(TypeName = "date")]
        public DateTime dtObs { get; set; } = DateTime.Today;  //Дата наблюдения

        //d3DISTYPES
        [Display(Name = "Группа болезней")]
        [StringLength(4)]
        public string KIDdtp {get;set;} = "";
        public string KIDdtpDisplay {get;set;} = "";


        [Display(Name = "Вид")]
        [StringLength(4)]
        public string KIDspc { get; set; } = ""; // vid jivotnogo
        public string KIDspcDisplay { get; set; } = "";

        [Display(Name = "Зарегистрировано больных животных первично, голов")]
        public int? nA {get;set;} = 0;

        [Display(Name = "Из числа зарегистрированных животных, пало")]
        public int? nD {get;set;} = 0;

        [Display(Name = "Примечание")]
        public string Notes {get;set;} = "";
    }
}