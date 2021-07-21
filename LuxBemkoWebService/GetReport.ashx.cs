using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxBemkoWebService
{
    /// <summary>
    /// Summary description for GetReport
    /// </summary>
    public class GetReport : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context.Request.QueryString["name"]))
                {
                    string name = context.Request.QueryString["name"];
                    List<string> lstParams = new List<string>();
                    List<string> lstValues = new List<string>();

                    for (int i = 1; i < context.Request.QueryString.AllKeys.Length; i++)
                    {

                        lstParams.Add(context.Request.QueryString.AllKeys[i]);
                        lstValues.Add(context.Request.QueryString[context.Request.QueryString.AllKeys[i]]);

                    }
                    int SesjaId = 0;
                    if (context.Application["SesjaAPI"] != null)
                    {
                        SesjaId = Convert.ToInt32(context.Application["SesjaAPI"]);
                    }
                    CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations(SesjaId);
                    int sesja = cdn.GetSesja();
                    Logger.Logger.LogInfo("sesja=" + sesja.ToString());
                    string error = "";
                    if (sesja != 0)
                    {
                        if (sesja != SesjaId)
                        {
                            SesjaId = sesja;
                        }
                        byte[] fileBytes = null;
                        cdn.PobierzWydruk(ref error, lstParams, lstValues, name, ref fileBytes);
                        context.Response.ContentType = "application/octet-stream";
                        //        break;
                        //}

                        context.Response.AddHeader("Content-Disposition", name + ".pdf");

                        context.Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
                        context.Response.Flush();
                        context.Response.End();
                    }
                    else
                    {
                        error = "Nie udalo się zainicjować sesji";
                    }
                    Logger.Logger.LogInfo(error);
                    //if (!string.IsNullOrEmpty(context.Request.QueryString["gidnumer"]))
                    //{
                    //    int numer = Convert.ToInt32(context.Request.QueryString["gidnumer"]);

                    //    ReportModule.ReportModule rpt = new ReportModule.ReportModule();
                    //    rpt.GenerateReport("~/Reports/FS.RPT", "FS", context.Response, numer);
                    //}
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                context.Response.Flush();
                context.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}