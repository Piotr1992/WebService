using Helpers;
using Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CasWebService
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
                string message = "";
                if (LogUsage.Params) message = $"Parameters - none";
                using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
                {
                    lock (Locker.lockSem)
                    {
                        Logger.LogDebug("lockSem Start GetReport");
                        if (!string.IsNullOrEmpty(context.Request.QueryString["name"]))
                        {
                            Logger.LogInfo("GetReport" + context.Request.QueryString["name"]);
                            string name = context.Request.QueryString["name"];
                            string typ = context.Request.QueryString["TrN_GIDTyp"];
                            if (typ == "2041")
                            {
                                name = "FSK";
                            }
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
                            Cdnsys th = new Cdnsys();
                            th.AttachThread();

                            CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations(SesjaId);
                            int sesja = cdn.GetSesja();
                            string error = "";
                            Logger.LogDebug(string.Format("GetReport sesja={0} sesjaid={1}", sesja.ToString(), SesjaId.ToString()));
                            int resu = 0;
                            resu = cdn.Login(ref error);
                            Logger.LogDebug(string.Format("GetReport login: {0} sesja: {1}", resu.ToString(), SesjaId));

                            Logger.LogDebug("Pobierz dokument sesja=" + sesja.ToString());
                            Logger.LogInfo("sesja=" + sesja.ToString());


                            if (sesja != 0)
                            {
                                if (sesja != SesjaId)
                                {
                                    SesjaId = sesja;
                                }
                                byte[] fileBytes = null;

                                cdn.PobierzWydruk(ref error, lstParams, lstValues, name, ref fileBytes);
                                Logger.LogInfo(string.Format("error {0}", error));
                                resu = cdn.Logout(sesja);
                                Logger.LogInfo(string.Format("logout {0}", resu));
                                context.Response.ContentType = "application/octet-stream";
                                //        break;
                                //}

                                context.Response.AddHeader("Content-Disposition", name + ".pdf");

                                context.Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
                                context.Response.Flush();
                                context.Response.SuppressContent = true;
                                context.ApplicationInstance.CompleteRequest();
                                //context.Response.End();
                            }
                            else
                            {
                                error = "Nie udalo się zainicjować sesji";
                            }

                            resu = cdn.Logout(sesja);

                            Logger.LogInfo(error);
                            //if (!string.IsNullOrEmpty(context.Request.QueryString["gidnumer"]))
                            //{
                            //    int numer = Convert.ToInt32(context.Request.QueryString["gidnumer"]);

                            //    ReportModule.ReportModule rpt = new ReportModule.ReportModule();
                            //    rpt.GenerateReport("~/Reports/FS.RPT", "FS", context.Response, numer);
                            //}
                        }
                        Logger.LogDebug("lockSem End GetReport");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);

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