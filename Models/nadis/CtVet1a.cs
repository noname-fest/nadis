﻿using nadis.Models.sp;
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

        [Display(Name = "Отчетный периуд")]
        [DisplayFormat(DataFormatString = "{0:MMM yyyy}")]
        public DateTime RepMO { get; set; } = DateTime.Now;//periud dannix

        //[ScaffoldColumn(false)]
        [StringLength(7)]
        public string KIDro { get; set; }     //code пользователя RD02525

        [Display(Name = "Айыл Аймак")]
        public string KIDdivDisplay { get; set; } = "";

        [StringLength(10)]
        //public string KIDdiv { get; set; }  //raion a/a 
        public string KIDdiv { get; set; } = "";

        [Display(Name = "Вид животного")]
        public string KIDspcDisplay { get; set; } = "";

        [StringLength(4)]
        public string KIDspc { get; set; } = ""; // vid jivotnogo

        [Display(Name = "Болезнь")]
        public string KIDdisDisplay { get; set; } = "";
        [StringLength(4)]
        public string KIDdis { get; set; } = ""; // bolezn

        [Display(Name = "Выявл. неблагополуч. пунктов")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? pos_units { get; set; } = 0;

        [Display(Name = "Заболело голов")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? positives { get; set; } = 0;

        [Display(Name = "Пало голов")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? dead { get; set; } = 0;

        [Display(Name = "Неблагополуч. пунктов")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? end_pos_units { get; set; } = 0;

        [Display(Name = "Больных животных голов")]
        [Required(ErrorMessage = "Только целое число > 0")]
        public int? end_pos_animals { get; set; } = 0;

        [Display(Name = "Забито")]
        //[Required(ErrorMessage = "Только целое число > 0")]
        public int? culled { get; set; } = 0;

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