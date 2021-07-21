using BmpWebServiceBackend;
using CDNOperations;
using Helpers;
using Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;

namespace CasWebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService()]
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_AdresyKontrahenta(int kntnumer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - kntnumer:{kntnumer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_AdresyKontrahenta](@kntnumer)", "b2b_AdresyKontrahenta", "@kntnumer", kntnumer);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ceny(int Id)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - Id:{Id}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_ceny](@Id)", "b2b_stany", "@Id", Id);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_kontrahenci()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_kontrahenci()", "b2b_kontrahenci");
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_nowyAdres(string xml)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - xml:{xml}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    Cdnsys th = new Cdnsys();
                    th.AttachThread();

                    CDNOperations.CDNOperations cdn = StartCDNOperation("b2b_nowyAdres");
                    int sesja = cdn.GetSesja();
                    Logger.LogDebug("Nowy Adres sesja=" + sesja.ToString());
                    Logger.LogInfo("sesja=" + sesja.ToString());

                    int docId = 0;
                    string error = "";
                    string numer = "";
                    if (sesja != 0)
                    {
                        if (sesja != SesjaId)
                        {
                            SesjaId = sesja;
                        }

                        docId = cdn.NowyAdres(xml, ref error);
                    }
                    else
                    {
                        error = "Nie udalo się zainicjować sesji";
                    }
                    EndCDNOperation("b2b_nowyAdres", cdn);

                    CDNResponse resp = new CDNResponse
                    {
                        Id = docId,
                        Error = error,
                        Numer = numer
                    };
                    return ResponseSerializer.Serialize(resp);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_nowyDokumentZam(string xml)
        {
            bool Potwierdzony = false;
            string KodRabatowy = "";
            string message = "";
            if (LogUsage.Params) message = $"Parameters - xml:{xml},potwierdzony:{Potwierdzony},kodrabatowy:{KodRabatowy}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    Logger.LogDebug("lockSem Start b2b_nowyDokumentZam");
                    Cdnsys th = new Cdnsys();
                    th.AttachThread();

                    CDNOperations.CDNOperations cdn = StartCDNOperation("b2b_nowyDokumentZam");
                    int sesja = cdn.GetSesja();
                    int docId = 0;

                    string error = "";
                    string numer = "";
                    if (sesja != 0)
                    {
                        if (sesja != SesjaId)
                        {
                            SesjaId = sesja;
                        }

                        docId = cdn.CasNowyDokumentZam(6, xml, ref error, Potwierdzony ? 2 : 0, KodRabatowy);
                        if (string.IsNullOrEmpty(error))
                        {
                            numer = DBHelper.GetNumerDokumentuTRN(docId);
                        }
                    }
                    else
                    {
                        error = "Nie udalo się zainicjować sesji";
                    }
                    EndCDNOperation("b2b_nowyDokumentZam", cdn);

                    CDNResponse resp = new CDNResponse();
                    resp.Id = docId;
                    resp.Error = error;
                    resp.Numer = numer;
                    Logger.LogDebug("lockSem End b2b_nowyDokumentZam");
                    return ResponseSerializer.Serialize(resp);

                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_NumerWeb(int kntid, string numer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - kntid:{kntid},numer:{numer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                return DBHelper.WebNumer(kntid, numer);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_stany(int Id)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - Id:{Id}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_stany](@Id)", "b2b_stany", "@Id", Id);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_towary()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_towary()", "b2b_towary");
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_traelem(int trnnumer, int kndnumer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - trnnumer:{trnnumer},kndnumer:{kndnumer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@trnnumer", trnnumer));
                lstPar.Add(new QueryParam("@kndnumer", kndnumer));
                DataTable dt = DBHelper.RunSqlProcParam("[dbo].[b2b_traelem]", "b2b_traelem", lstPar);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_tranag(int gidnumer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - gidnumer:{gidnumer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@gidnumer", gidnumer));
                DataTable dt = DBHelper.RunSqlProcParam("[dbo].[b2b_tranag]", "b2b_tranag", lstPar);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_zamelem(int gidnumer, int kndnumer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - gidnumer:{gidnumer},kndnumer:{kndnumer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@gidnumer", gidnumer));
                lstPar.Add(new QueryParam("@kndnumer", kndnumer));
                DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_zamelem](@gidnumer,@kndnumer)", "b2b_zamelem", lstPar);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_zamnag(int gidnumer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - gidnumer:{gidnumer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlProcParam("dbo.b2b_zamnag", "b2b_zamnag", "@gidnumer", gidnumer);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_cdloginlogout()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                Logger.LogDebug("Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                List<CDNResponse> respList = new List<CDNResponse>();
                respList.Add(new CDNResponse());
                respList.Add(new CDNResponse());
                lock (Locker.lockSem)
                {
                    CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations();
                    //int sesjaId = cdn.GetSesja();
                    string error = "";
                    //if (!cdn.SprawdzSesje(sesjaId))
                    //{
                    //    sesjaId = cdn.Login(ref error);
                    //}

                    int resu = cdn.Login(ref error);
                    int sesjaId = cdn.GetSesja();
                    CDNResponse resp = respList[0];
                    resp.Id = resu;
                    resp.Error = error;
                    resp.Numer = "Login";

                    resu = cdn.Logout(sesjaId);
                    CDNResponse resp1 = respList[1];
                    resp1.Id = resu;
                    resp1.Error = "";
                    resp1.Numer = "Logout";
                    return ResponseSerializer.Serialize(respList);
                }
            }
        }
    
        private CDNOperations.CDNOperations StartCDNOperation(string functionName)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - functionName:{functionName}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations(SesjaId);
                SesjaId = cdn.GetSesja();
                Logger.LogDebug(string.Format("{1} sesja={0}", SesjaId.ToString(), functionName));
                int resu = 0;
                string error = "";
                resu = cdn.Login(ref error);
                Logger.LogDebug(string.Format("{2} login: {0} sesja: {1}", resu.ToString(), SesjaId, functionName));
                return cdn;
            }
        }

        private void EndCDNOperation(string functionName, CDNOperations.CDNOperations cdn)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - functionName:{functionName},cdnoperations";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                int resu = 12; // cdn.Logout(SesjaId);
                Logger.LogDebug(string.Format("functionName logout: {0} sesja: {1}", resu.ToString(), SesjaId, functionName));
            }
        }

        private int SesjaId
        {
            get
            {
                if (Application["SesjaAPI"] != null)
                {
                    return Convert.ToInt32(Application["SesjaAPI"].ToString());
                }
                return 0;
            }
            set
            {
                if (Application["SesjaAPI"] != null)
                {
                    Application["SesjaAPI"] = value;
                }
                else
                {
                    Application.Add("SesjaAPI", value);
                }
            }
        }
    }
}