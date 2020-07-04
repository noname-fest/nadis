using nadis.Models.sp;
using nadis.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace nadis.Models
{
    public class CtVetPlan
    {
        [Display(Name = "guid Записи")]
        public Guid ID { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MMMyyyy}")]

        //Aiil Aimak code and Display
        [Display(Name = "KIDdiv")]
        [StringLength(10)]
        public string KIDdiv { get; set; } = "";  
        public string KIDdivDisplay { get; set; } = "";

        [Display(Name = "PlanYr")]
        public int? PlanYr {get; set;} = 1900;

        //Vzroslie po Q
        public int? tmalage1_I {get; set;} = 0;
        public int? tmalage1_II {get; set;} = 0;
        public int? tmalage1_III {get; set;} = 0;
        public int? tmalage1_IV {get; set;} = 0;
        //Molodnak po Q
        public int? tfemage2_I {get; set;} = 0;
        public int? tfemage2_II {get; set;} = 0;
        public int? tfemage2_III {get; set;} = 0;
        public int? tfemage2_IV {get; set;} = 0;
        //pLan po Q
        public int? nPlan_I   {get{return tmalage1_I+tfemage2_I;} }
        public int? nPlan_II  {get{return tmalage1_II+tfemage2_II;}}
        public int? nPlan_III {get{return tmalage1_III+tfemage2_III;}}
        public int? nPlan_IV  {get{return tmalage1_IV+tfemage2_IV;}}
        public int? nPlan     {get{return nPlan_I+nPlan_II+nPlan_III+nPlan_IV;}}

        //Zaplanirovanie deistvia
        [Display(Name = "KIDtrt")]
        [StringLength(5)]
        public string KIDtrt {get; set;} = "";
        public string KIDtrtDisplay {get; set;} = "";

        //Vid jivotnogo
        [Display(Name = "KIDspc")]
        [StringLength(4)]
        public string KIDspc { get; set; } = ""; // vid jivotnogo
        public string KIDspcDisplay { get; set; } = "";

        //Bolezn
        [Display(Name = "KIDdis")]
        [StringLength(4)]
        public string KIDdis { get; set; } = ""; // bolezn
        public string KIDdisDisplay { get; set; } = "";

        //Tip analizov
        [Display(Name = "KIDtest")]
        [StringLength(5)]
        public string KIDtest {get; set;} = "";
        public string KIDtestDisplay {get; set;} = "";

    }
}