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
            public DateTime dt {get;set;}
            public WebReport report {get;set;}
        }
}