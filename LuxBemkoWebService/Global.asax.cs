using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace LuxBemkoWebService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                Logger.Logger.LogInfo("Application_Start");
                CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations();
                string error = string.Empty;
                int result = cdn.Login(ref error);

                int sesja = cdn.GetSesja();
                if (sesja > 0)
                {
                    Application.Add("SesjaAPI", sesja.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
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
            Logger.Logger.LogInfo("Application_End");
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
                        Logger.Logger.LogInfo("Nie udało się wylogować z CDNXL, wynik: " + result);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Logger.LogInfo("Nie udało się wylogować z CDNXL, wynik: " + result);
                    Logger.Logger.LogException(ex);
                }
            }
        }

    }
}