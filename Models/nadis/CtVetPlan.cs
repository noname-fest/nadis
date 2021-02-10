using Dapper.Contrib.Extensions;
using nadis.Models.sp;
using nadis.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace nadis.Models
{
    [Table ("ctVetPlan")]
    public class CtVetPlan
    {
        [Display(Name = "guid Записи")]
        [Dapper.Contrib.Extensions.Key]
        public Guid ID { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MMMyyyy}")]
        [StringLength(7)]
        public string KIDro { get; set; }     //code пользователя RD02525

        //Aiil Aimak code and Display
        [Display(Name = "Айыл Аймак")]
        [StringLength(10)]
        public string KIDdiv { get; set; } = "";  
        //public string KIDdivDisplay { get; set; } = "";

        [Display(Name = "Год")]
        public int? PlanYr {get; set;} = 1900;

        //Vzroslie po Q
        public int? tfemage1_I {get; set;} = 0;
        public int? tfemage1_II {get; set;} = 0;
        public int? tfemage1_III {get; set;} = 0;
        public int? tfemage1_IV {get; set;} = 0;
        //Molodnak po Q
        public int? tfemage2_I {get; set;} = 0;
        public int? tfemage2_II {get; set;} = 0;
        public int? tfemage2_III {get; set;} = 0;
        public int? tfemage2_IV {get; set;} = 0;
        //pLan po Q
        [Display(Name = "I кв")]
        public int? nPlan_I   {get;set;} = 0;
        [Display(Name = "II кв")]
        public int? nPlan_II  {get;set;} = 0;
        [Display(Name = "III кв")]
        public int? nPlan_III {get;set;} = 0;
        [Display(Name = "IV кв")]
        public int? nPlan_IV  {get;set;} = 0;
        [Display(Name = "ИТОГО")]
        public int? nPlan     {get;set;} = 0;
        
        /*
        public int? nPlan_I   {get;set;}
        public int? nPlan_II  {get;set;}
        public int? nPlan_III {get;set;}
        public int? nPlan_IV  {get;set;}
        public int? nPlan     {get;set;}
        */


        //Zaplanirovanie deistvia
        [StringLength(5)]
        [Display(Name = "Запланированные действия")]
        public string KIDtrt {get; set;} = "";
        //public string KIDtrtDisplay {get; set;} = "";

        //Vid jivotnogo
        [StringLength(4)]
        [Display(Name = "Вид")]
        public string KIDspc { get; set; } = ""; // vid jivotnogo
//        public string KIDspcDisplay { get; set; } = "";

        //Bolezn
        [StringLength(4)]
        [Display(Name = "Болезнь")]
        public string KIDdis { get; set; } = ""; // bolezn
//        public string KIDdisDisplay { get; set; } = "";

        //Tip analizov
        [StringLength(5)]
        [Display(Name = "Тип анализа")]
        public string KIDtest {get; set;} = "";
        //public string KIDtestDisplay {get; set;} = "";

    }
}