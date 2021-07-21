using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using CrystalDecisions.Shared;
using System.Configuration;

namespace ReportModule
{
    public enum ReportType
    {
        FS
    }

    public class ReportModule
    {     
        private CrystalDecisions.Shared.ConnectionInfo conn;
        public ReportModule()
        {
            conn = new CrystalDecisions.Shared.ConnectionInfo();
            conn.DatabaseName = ConfigurationManager.AppSettings["BazaCDNXL"];
            conn.ServerName =  ConfigurationManager.AppSettings["DSN"];
            conn.Password = ConfigurationManager.AppSettings["DBPass"];
            conn.UserID = ConfigurationManager.AppSettings["DBLogin"];
        }
        public void GenerateReport(string reportPath, string fileName, HttpResponse response, int TrN_GIDNumer)
        {
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(HttpContext.Current.Server.MapPath(reportPath));            
            crystalReport.RecordSelectionFormula = "{TraNag.TrN_GIDNumer}="+TrN_GIDNumer;
            crystalReport.Refresh();
            FixDatabase(crystalReport, conn);
            crystalReport.VerifyDatabase();
            foreach (ParameterField par in crystalReport.ParameterFields)
            {
                if (par.Name == "CDN_DrukDaty")
                {
                    crystalReport.SetParameterValue(par.Name, 0);
                }
            }
            crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, response, true, fileName);
        }
    
        private void FixDatabase(ReportDocument report, ConnectionInfo someConnectionInfo)
        {
            ConnectionInfo crystalConnectionInfo = someConnectionInfo;

            foreach (Table table in report.Database.Tables)
            {
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                logOnInfo = table.LogOnInfo;               

                if (logOnInfo != null)
                {
                    logOnInfo.ConnectionInfo = crystalConnectionInfo;
                    table.LogOnInfo.TableName = table.Name;
                    table.LogOnInfo.ConnectionInfo.UserID = someConnectionInfo.UserID;
                    table.LogOnInfo.ConnectionInfo.Password = someConnectionInfo.Password;
                    table.LogOnInfo.ConnectionInfo.DatabaseName = someConnectionInfo.DatabaseName;
                    table.LogOnInfo.ConnectionInfo.ServerName = someConnectionInfo.ServerName;
                    table.ApplyLogOnInfo(table.LogOnInfo);
                    table.Location = table.Name;
                }
            }

            foreach (ReportObject reportObject in report.ReportDefinition.ReportObjects)
            {
                if (reportObject.Kind == ReportObjectKind.SubreportObject)
                {
                    FixDatabase(report.OpenSubreport(((SubreportObject)reportObject).SubreportName), someConnectionInfo);
                }
            }
        }
    }
}
