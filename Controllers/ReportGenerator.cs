using System;
using System.Linq;
using nadis.Models;
using nadis.DAL.nadis;
using FastReport.Web;
using FastReport.Data;
using FastReport.Utils;

namespace nadis
{
    public class ReportGenerator
    {
        public Reports GetReport(string dt, string reportName,string KIDro)
        {
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            Reports webR = new Reports();
            webR.report = new WebReport();

            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.report.Report.Dictionary.Connections.Add(_con);
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path +  "\\FastReports\\"+ reportName +".frx";
            
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            webR.report.Report.Load(_path);
            webR.report.Report.SetParameterValue("yyyy",y);
            webR.report.Report.SetParameterValue("mm",m);
            webR.report.Report.SetParameterValue("idro", KIDro);
            //webR.report.Report.Prepare();
            webR.dt = dt;
            webR.ReportName = reportName;
            return webR;
        }
    }
}