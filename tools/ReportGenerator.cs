using System;
using nadis.Models;
using nadis.DAL;
using FastReport.Web;
using System.IO;
using FastReport.Utils;
using FastReport.Data;

namespace nadis
{
    public static class ReportGenerator
    {
        //[DllImport("FastReportFull.dll")]
        //static extern void Excel2007Export(FastReport.Report rp, MemoryStream ms);
        public static Reports GetReportCA(string dt, string reportName,string Raion, string Oblast)
        {
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            Reports webR = new Reports();
            webR.report = new WebReport();
            //MsSqlDataConnection _con = new MsSqlDataConnection();
            //_con.ConnectionString = spDAL.connStr;
            //_con.CreateAllTables();
           //webR.report.Report.Dictionary.Connections.Add(_con);
            string _path = System.IO.Directory.GetCurrentDirectory();
            
            _path = _path
                + System.IO.Path.DirectorySeparatorChar
                + "FastReports"
                + System.IO.Path.DirectorySeparatorChar
                + "CA"
                + System.IO.Path.DirectorySeparatorChar
                + reportName
                + ".frx";
            
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            webR.report.Report.Load(_path);
            webR.report.Report.Dictionary.Connections[0].ConnectionString = spDAL.connStr;
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

        public static Reports GetReportRaznaryadka(string dt, string reportName,string Raion, string Oblast)
        {
            
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            Reports webR = new Reports();
            webR.report = new WebReport();
            /*
            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.report.Report.Dictionary.Connections.Add(_con);
            */
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path
                + System.IO.Path.DirectorySeparatorChar
                + "FastReports"
                + System.IO.Path.DirectorySeparatorChar
                + "CA"
                + System.IO.Path.DirectorySeparatorChar
                + reportName
                + ".frx";
            
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            webR.report.Report.Load(_path);
            webR.report.Report.Dictionary.Connections[0].ConnectionString = spDAL.connStr;
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

        public static Reports GetReportPererashod(string dt, string reportName,string Raion, string Oblast)
        {
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            Reports webR = new Reports();
            webR.report = new WebReport();
            /*
            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.report.Report.Dictionary.Connections.Add(_con);
            */
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path
                + System.IO.Path.DirectorySeparatorChar
                + "FastReports"
                + System.IO.Path.DirectorySeparatorChar
                + "CA"
                + System.IO.Path.DirectorySeparatorChar
                + reportName
                + ".frx";
            
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            webR.report.Report.Load(_path);
            webR.report.Report.Dictionary.Connections[0].ConnectionString = spDAL.connStr;
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
            //webR.report.Dictionary.Connections[0].ConnectionString = spDAL.connStr;
            //webR.report.Report.Dictionary.Connections[0].ConnectionString = spDAL.connStr;
            /*
            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.report.Report.Dictionary.Connections.Add(_con);
            */
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path
                + System.IO.Path.DirectorySeparatorChar
                + "FastReports"
                + System.IO.Path.DirectorySeparatorChar
                + reportName
                + ".frx";
            //_path = _path +  "\\FastReports\\"+ reportName +".frx";
            
            string m;
            int y = 0;
            // check the CtVet2 report 
            //
            if(dt.Contains("/")) {
                //m = Int32.Parse(dt.Substring(5,1));
                m = dt.Substring(5,1);
                y = Int32.Parse(dt.Substring(0,4));                
            } else 
            if(dt.Contains("-")){
                m = dt.Substring(5,dt.Length-5);
                y = Int32.Parse(dt.Substring(0,4));
            } else
            {
                //m = Int32.Parse(dt.Substring(0,2));
                m = Int32.Parse(dt.Substring(0,2)).ToString();
                y = Int32.Parse(dt.Substring(3,4));
            }
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            webR.report.ShowExcel2007Export=true;
            webR.report.Report.Load(_path);
            webR.report.Report.Dictionary.Connections[0].ConnectionString = spDAL.connStr;
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
            //RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            WebReport webR = new WebReport();
            /*
            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.Report.Dictionary.Connections.Add(_con);
            */
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path
                + System.IO.Path.DirectorySeparatorChar
                + "FastReports"
                + System.IO.Path.DirectorySeparatorChar
                + reportName
                + ".frx";
            //_path = _path +  "\\FastReports\\"+ reportName +".frx";
 
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            
            webR.Report.Load(_path);
            webR.Report.Dictionary.Connections[0].ConnectionString = spDAL.connStr;
            webR.Report.SetParameterValue("yyyy",y.ToString());
            webR.Report.SetParameterValue("mm",m.ToString());
            webR.Report.SetParameterValue("idro",KIDro);
            //webR.dt
            //webR.ReportName = reportName;
            webR.Report.Prepare();
            using (MemoryStream ms = new MemoryStream())
            {
                /*
                var fdf = new FastReport.Export.Pdf();
                FastReport.Export.Pdf pdfExport = new FastReport.Export.PdfSimple();
                pdfExport.Export(webR.Report, ms);
                ms.Flush();
                pdfExport.Dispose();*/
                return ms.ToArray();
            }              

        }

        public static byte[] ExportToHTML(string dt, string reportName,string KIDro)
        {
            //RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            WebReport webR = new WebReport();
            /*
            MsSqlDataConnection _con = new MsSqlDataConnection();
            _con.ConnectionString = spDAL.connStr;
            _con.CreateAllTables();
            webR.Report.Dictionary.Connections.Add(_con);
            */
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path
                + System.IO.Path.DirectorySeparatorChar
                + "FastReports"
                + System.IO.Path.DirectorySeparatorChar
                + reportName
                + ".frx";
            //_path = _path +  "\\FastReports\\"+ reportName +".frx";
 
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            
            webR.Report.Load(_path);
            webR.Report.SetParameterValue("yyyy",y.ToString());
            webR.Report.SetParameterValue("mm",m.ToString());
            webR.Report.SetParameterValue("idro",KIDro);
            //webR.dt
            //webR.ReportName = reportName;
            webR.Report.Prepare();
            webR.ShowExcel2007Export = true;
            using (MemoryStream ms = new MemoryStream())
            {
                /*
                HTMLExport HTMLExport = new HTMLExport();
                //webR.Report.Export(ExportBasee)
                HTMLExport.Export(webR.Report, ms);
                ms.Flush();
                HTMLExport.Dispose();
                */
                return ms.ToArray();
            }              

        }
        
        public static byte[] ExportToExcel(string dt, string reportName,string KIDro)
        {
            //RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            //WebReport webR = new WebReport();
            //FRWF.FastReport.Web.WebReport webR = new FRWF.FastReport.Web.WebReport();
            FastReport.Report webR = new FastReport.Report();

            //MsSqlDataConnection _con = new MsSqlDataConnection();
            //_con.ConnectionString = spDAL.connStr;
            //_con.CreateAllTables();
            //webR.Report.Dictionary.Connections.Add(_con);
            //webR.Report.Dictionary.Connections[0].ConnectionString = spDAL.connStr;
            string _path = System.IO.Directory.GetCurrentDirectory();
            _path = _path
                + System.IO.Path.DirectorySeparatorChar
                + "FastReports"
                + System.IO.Path.DirectorySeparatorChar
                + reportName
                + ".frx";
            //_path = _path +  "\\FastReports\\"+ reportName +".frx";
 
            int m = Int32.Parse(dt.Substring(0,2));
            int y = Int32.Parse(dt.Substring(3,4));
            
            webR.Report.Load(_path);
            webR.Report.Dictionary.Connections[0].ConnectionString = spDAL.connStr;
            webR.Report.SetParameterValue("yyyy",y.ToString());
            webR.Report.SetParameterValue("mm",m.ToString());
            webR.Report.SetParameterValue("idro",KIDro);
            webR.Report.Prepare();

            using (MemoryStream ms = new MemoryStream())
            {
                //FastReport.Export.OoXML.Excel2007Export
                //    ex = new FastReport.Export.OoXML.Excel2007Export ();//FRF.FastReport.Export.OoXML.Excel2007Export();
                //    ex.Export()
                //ex.Export(webR.Report, ms);
                //ms.Flush();
                //ex.Dispose();
                return ms.ToArray();
            } 
        }
        
    }
}