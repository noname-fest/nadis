using System;
using nadis.Models;
using nadis.DAL.nadis;
using FastReport.Web;
using FastReport.Data;
using FastReport.Utils;
using System.IO;
using FastReport.Export.PdfSimple;

namespace nadis
{
    public static class ReportGenerator
    {
        public static Reports GetReportCA(string dt, string reportName,string Raion, string Oblast)
        {
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            Reports webR = new Reports();
            webR.report = new WebReport();

            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.report.Report.Dictionary.Connections.Add(_con);
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path +  "\\FastReports\\CA\\"+ reportName +".frx";
            
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            webR.report.Report.Load(_path);
            webR.report.Report.SetParameterValue("yyyy",y);
            webR.report.Report.SetParameterValue("mm",m);
            if(Raion is null) Raion = "";
            if(Oblast is null) Oblast = "";
            webR.report.Report.SetParameterValue("raion", Raion);
            webR.report.Report.SetParameterValue("Oblast", Oblast);
            //webR.report.Report.Prepare();
            webR.dt = dt;
            webR.ReportName = reportName;
            //TableDataSource table = webR.report.Report.GetDataSource("v1") as TableDataSource;
            //table.SelectCommand = "";
            return webR;
        }

        public static Reports GetReport(string dt, string reportName,string KIDro)
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
            //TableDataSource table = webR.report.Report.GetDataSource("v1") as TableDataSource;
            //table.SelectCommand = "";

            return webR;
        }

        public static byte[] ExportToPDF(string dt, string reportName,string KIDro)
        {
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            WebReport webR = new WebReport();

            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.Report.Dictionary.Connections.Add(_con);
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path +  "\\FastReports\\"+ reportName +".frx";
 
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            
            webR.Report.Load(_path);
            webR.Report.SetParameterValue("yyyy",y.ToString());
            webR.Report.SetParameterValue("mm",m.ToString());
            webR.Report.SetParameterValue("idro",KIDro);
            //webR.dt
            //webR.ReportName = reportName;
            webR.Report.Prepare();
            using (MemoryStream ms = new MemoryStream())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(webR.Report, ms);
                ms.Flush();
                pdfExport.Dispose();
                return ms.ToArray();
            }              

        }
    }
}