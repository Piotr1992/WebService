using CDNOperations;
using Helpers;
using Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace BemkoWebService
{
    public class Global : System.Web.HttpApplication
    {
       

        protected void Application_Start(object sender, EventArgs e)
        {
            
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Logger.LogInfo("Application_Start");
                int SesjaId = 0;
                if (Application["SesjaAPI"] != null)
                {
                    SesjaId = Convert.ToInt32(Application["SesjaAPI"]);
                }
                CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations();
                if (SesjaId != 0)
                {
                    cdn.Logout(SesjaId);
                }

                string error = "";
                int result = cdn.Login(ref error);

                int sesja = cdn.GetSesja();
                
                if (sesja > 0)
                {
                    Application.Add("SesjaAPI", sesja.ToString());
                }
                watch.Stop();
                Logger.LogDebug($"Application_Start sesja={sesja.ToString()} cdnInit took:{watch.ElapsedMilliseconds}ms");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            try
            {
                // do logowania użycia metod
                var watch = System.Diagnostics.Stopwatch.StartNew();
                LogUsage.Usage=false;
                LogUsage.Params=false;
                var ret = DBHelper.RunScalarSqlQueryParam("SELECT count(*) val FROM sys.objects WHERE (object_id = OBJECT_ID(N'dbo.bmp_spLogUsage') AND type IN ( N'P', N'PC' )) or  (object_id = OBJECT_ID(N'dbo.bmp_LogUsage') and type ='U')",null);
                var r = DBHelper.RunScalarSqlQueryParam("if exists (SELECT 1 FROM sys.objects WHERE (object_id = OBJECT_ID(N'dbo.bmp_spLogUsageClearParameters') AND type IN ( N'P', N'PC' ))) exec dbo.bmp_spLogUsageClearParameters",null);

                var logObjectsExist=Convert.ToInt16(ret)==2;
                //0 nie loguje statsow, 1 loguje statsy, default 1, inne wartosci - wylaczaja logowanie
                var logUsageStats=(ConfigurationManager.AppSettings["logUsageStats"]!=null ? Convert.ToInt32(ConfigurationManager.AppSettings["logUsageStats"]) : 1) == 1;
                //0 nie loguje parametrów, 1 loguje parametry w trybie debug/all,2 loguje parametry zawsze, default 1, inne wartosci - wylaczaja logowanie
                int logParamsCfg=ConfigurationManager.AppSettings["logParams"]!=null ? Convert.ToInt32(ConfigurationManager.AppSettings["logParams"]) : 0;
                LogUsage.Params = logParamsCfg==2 ? true : (logParamsCfg==1 && Logger.IsDebugEnabled ? true : false);

                watch.Stop();
                if (logObjectsExist==false && logUsageStats==true ) 
                {
                    Logger.LogInfo($"Application_Start Method usage logging not possible, missing table or procedure, logging check took:{watch.ElapsedMilliseconds}ms");
                }
                if (logObjectsExist==true && logUsageStats==true) 
                {
                    LogUsage.Usage=logUsageStats;
                    Logger.LogInfo($"Application_Start Method usage logging is set to {LogUsage.Usage}, logging check took:{watch.ElapsedMilliseconds}ms");
                }
                Logger.LogInfo($"Application_Start Parameter logging is set to {LogUsage.Params}, logging check took:{watch.ElapsedMilliseconds}ms");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Logger.LogInfo("Application_End");
            if (Application["SesjaAPI"] != null)
            {

                int result = 0;
                try
                {
                    int sesja = Convert.ToInt32(Application["SesjaAPI"].ToString());
                    CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations();
                    result = cdn.Logout(sesja);
                    sesja = 0;
                    Application["SesjaAPI"] = sesja.ToString();
                    if (result != 0)
                    {
                        Logger.LogInfo("Nie udało się wylogować z CDNXL, wynik: " + result);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogInfo("Nie udało się wylogować z CDNXL, wynik: " + result);
                    Logger.LogException(ex);
                }
            }
        }
    }
}