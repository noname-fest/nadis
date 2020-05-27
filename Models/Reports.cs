using System;
using FastReport;
using FastReport.Web;

namespace nadis.Models
{
    public class Reports
        {
            // Report ID
            public int Id { get; set; }
            // Report File Name
            public string ReportName { get; set; }
            public int m {get;set;}
            public string dt {get;set;}
            public WebReport report {get;set;}
        }
}