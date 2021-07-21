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

namespace JanexWebService
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

        #region bs
        #region b2b_towary
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
        #endregion
        #region b2b_grupy
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_grupy()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_grupy()", "b2b_grupy");
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_grupy2()
        {
            DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_grupy2()", "b2b_grupy2");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }

        #endregion
        #region b2b_CenySpecjalne
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_CenySpecjalne(int kntId)
        {
            try
            {
                string message = "";
                if (LogUsage.Params) message = $"Parameters - kntId:{kntId}";
                using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
                {
                    DataTable dt = DBHelper.RunSqlProcParam("[dbo].[dbo_b2b_CenySpecjalne]", "b2b_CenySpecjalne", "@kntgidnumer", kntId);
                    if (dt != null)
                    {
                        if (LogUsage.Json) Logger.LogDebug(DataTableHelper.GetJson(dt));
                        return DataTableHelper.GetJson(dt);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString());
            }
            return null;
        }

        #endregion
        #region b2b_kontrahenci
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
        public string b2b_NowyKontrahent(string nip, string kraj, string nazwa, string ulica, string miasto, string kodPocztowy)
        {
            string message = "";
            if (LogUsage.Params) message = $"WZ Parameters - nip:{nip},kraj:{kraj},nazwa:{nazwa},ulica:{ulica},miasto:{miasto},kodPocztowy:{kodPocztowy}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    var operacje = new CDNOperations.CDNOperations(SesjaId);
                    return DataTableHelper.GetJson(operacje.NowyKontrahent(nip, kraj, nazwa, ulica, miasto, kodPocztowy));
                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_OsobaKontaktowa(string gid, string stanowisko, string nazwa, string telefon, string telefonK, string email, string fax, string dzial, string tytul, int Upowazniona, int UpowaznionaZam)
        {
            string message = "";
            if (LogUsage.Params) message = $"WZ Parameters - gid:{gid},stanowisko:{stanowisko},nazwa:{nazwa},telefon:{telefon},telefonK:{telefonK},email:{email},fax:{fax},dzial:{dzial},tytul:{tytul},Upowazniona:{Upowazniona},UpowaznionaZam:{UpowaznionaZam}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    var operacje = new CDNOperations.CDNOperations(SesjaId);
                    return operacje.DodajOsobe(gid, nazwa, stanowisko, telefon, telefonK, email, fax, dzial, tytul, Upowazniona, UpowaznionaZam).ToString();
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_OsobaKontaktowa_v2(string gid, string stanowisko, string nazwa, string telefon, string telefonK, string email, string fax, string dzial, string tytul, int Upowazniona, int UpowaznionaZam, bool Dostep)
        {
            string message = "";
            if (LogUsage.Params) message = $"WZ Parameters - gid:{gid},stanowisko:{stanowisko},nazwa:{nazwa},telefon:{telefon},telefonK:{telefonK},email:{email},fax:{fax},dzial:{dzial},tytul:{tytul},Upowazniona:{Upowazniona},UpowaznionaZam:{UpowaznionaZam},Dostep:{Dostep}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    var operacje = new CDNOperations.CDNOperations(SesjaId);
                    return operacje.DodajOsobe_v2(gid, nazwa, stanowisko, telefon, telefonK, email, fax, dzial, tytul, Upowazniona, UpowaznionaZam, Dostep).ToString();
                }
            }
        }

        #endregion
        #region b2b_danebinarne
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_danebinarne()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_lista_danebinarne()", "b2b_danebinarne");
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        #endregion
        #region b2b_operatorzy
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_operatorzy()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_operatorzy()", "b2b_operatorzy");
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_operatorzy2(int? kntnumer = null, int? knslp = null)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - kntnumer:{kntnumer},knslp:{knslp}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                var x = DBHelper.RunSqlProcParam("[dbo].[b2b_Operators]", "T", new List<QueryParam>()
                {
                    new QueryParam("@kntnumer",kntnumer!=null?(object)kntnumer.Value:DBNull.Value),
                    new QueryParam("@knslp",knslp!=null?(object)knslp.Value:DBNull.Value),
                });
                return x.GetJson();
                //return "";
            }
        }

        #endregion
        #region b2b_dystrybutorzy
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_dystrybutorzy()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_dystrybutorzy ()", "b2b_dystrybutorzy");
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        #endregion
        #region b2b_CenyKlientow
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_cenyklientow(int kntId)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - kntId:{kntId}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQueryParam("select * from dbo.b2b_CenyKlientow(@kntnumer)", "b2b_cenyklientow", "@kntnumer", kntId);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        #endregion
        #region b2b_cechy
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_cechy(int twrId)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - twrId:{twrId}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_cechy](@twrid)", "b2b_stany", "@twrid", twrId);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        #endregion
        #region b2b_stany
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
        public string b2b_stanyKnt(int Id, int knt)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - Id:{Id},Knt:{knt}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@knt", knt));
                lstPar.Add(new QueryParam("@Id", Id));
                DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_stanyKnt](@knt,@Id)", "b2b_stanyKnt", lstPar);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }
        #endregion
        #region b2b_zamnag
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
        public string b2b_zamnagSearch(int gidnumer, string stan, string search)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - gidnumer:{gidnumer},stan:{stan},search:{search}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@gidnumer", gidnumer));
                lstPar.Add(new QueryParam("@stan", stan));
                lstPar.Add(new QueryParam("@search", search));
                DataTable dt = DBHelper.RunSqlProcParam("dbo.b2b_zamnagSearch", "b2b_zamnag", lstPar);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        #endregion
        #region b2b_zamelem
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
        #endregion
        #region b2b_adresy
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
        #endregion
        #region b2b_traelem
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
        #endregion
        #region b2b_tranag
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
        #endregion
        #region b2b_ceny
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

        #endregion
        #region b2b_twrzamienniki
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_twrzamienniki()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters -none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                DataTable dt = DBHelper.RunSqlProcParam("[dbo].[b2b_twrzamienniki]", "b2b_twrzamienniki", lstPar);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }
        #endregion
        [WebMethod]
        public string test()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQuery("select * from dbo.rcpusers", "rcpusers");
                if (dt != null)
                {

                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        #endregion bs
        #region VerifyUser
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_verifyUser(string name, string password)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - name:{name} pass:*";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                //Logger.LogInfo("verifiUser");
                //string str=Coder.Encrypt("CaLKC?Gz");
                return DBHelper.VerifyUser(name, password);
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_setNewUserPassword(string name, string new_password)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - name:{name},pass:*";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                return DBHelper.SetNewUserPassword(name, new_password);
            }
        }
        #endregion
        #region numer web w erp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_NumerWeb(int kntid, string numer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - kntid:{kntid},numer:{numer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                //Logger.LogDebug("pass webnumber to erp");
                //string str=Coder.Encrypt("CaLKC?Gz");
                return DBHelper.WebNumer(kntid, numer);
            }
        }
        #endregion
        #region CDNXL
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
        #endregion
        #region b2b_cdloginlogout
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
        #endregion

        #region b2b_nowyDokumentZam
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_nowyDokumentZam(string xml, bool Potwierdzony, string KodRabatowy)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - xml:{xml},potwierdzony:{Potwierdzony},kodrabatowy:{KodRabatowy}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    #region test
                    /*
                    <?xml version=""1.0""?>
                    <root>
                      <XLDokumentZamNagInfo_20141>
                        <Wersja>15</Wersja>
                        <GIDTyp>0</GIDTyp>
                        <GIDFirma>0</GIDFirma>
                        <GIDNumer>0</GIDNumer>
                        <GIDLp>0</GIDLp>
                        <Typ>6</Typ>
                        <Tryb>0</Tryb>
                        <Numer>0</Numer>
                        <Rok>0</Rok>
                        <Miesiac>0</Miesiac>
                        <KntTyp>32</KntTyp>
                        <KntFirma>0</KntFirma>
                        <KntNumer>5711</KntNumer>
                        <KntLp>0</KntLp>
                        <AdrTyp>0</AdrTyp>
                        <AdrFirma>0</AdrFirma>
                        <AdrNumer>0</AdrNumer>
                        <AdrLp>0</AdrLp>
                        <AdwTyp>0</AdwTyp>
                        <AdwFirma>0</AdwFirma>
                        <AdwNumer>0</AdwNumer>
                        <AdwLp>0</AdwLp>
                        <AkwTyp>0</AkwTyp>
                        <AkwFirma>0</AkwFirma>
                        <AkwNumer>0</AkwNumer>
                        <AkwLp>0</AkwLp>
                        <MagTyp>0</MagTyp>
                        <MagFirma>0</MagFirma>
                        <MagNumer>0</MagNumer>
                        <MagLp>0</MagLp>
                        <FormaPl>0</FormaPl>
                        <NrKursu>0</NrKursu>
                        <TypKursu>0</TypKursu>
                        <KursL>0</KursL>
                        <KursM>0</KursM>
                        <DataWystawienia>77930</DataWystawienia>
                        <DataRealizacji>0</DataRealizacji>
                        <DataWaznosci>0</DataWaznosci>
                        <OpeTypW>0</OpeTypW>
                        <OpeFirmaW>0</OpeFirmaW>
                        <OpeNumerW>0</OpeNumerW>
                        <OpeLpW>0</OpeLpW>
                        <OpeTypM>0</OpeTypM>
                        <OpeFirmaM>0</OpeFirmaM>
                        <OpeNumerM>0</OpeNumerM>
                        <OpeLpM>0</OpeLpM>
                        <RealWCalosci>0</RealWCalosci>
                        <AkwizytorKntPrc>0</AkwizytorKntPrc>
                        <ExpoNorm>0</ExpoNorm>
                        <Flagi>0</Flagi>
                        <Wewnetrzne>0</Wewnetrzne>
                        <KnDTyp>0</KnDTyp>
                        <KnDFirma>0</KnDFirma>
                        <KnDNumer>0</KnDNumer>
                        <KnDLp>0</KnDLp>
                        <ZrdTyp>0</ZrdTyp>
                        <ZrdFirma>0</ZrdFirma>
                        <ZrdNumer>0</ZrdNumer>
                        <ZrdLp>0</ZrdLp>
                        <MagWTyp>0</MagWTyp>
                        <MagWFirma>0</MagWFirma>
                        <MagWNumer>0</MagWNumer>
                        <MagWLp>0</MagWLp>
                        <PotwierdzenieOferty>0</PotwierdzenieOferty>
                        <DataPotwierdzeniaOferty>0</DataPotwierdzeniaOferty>
                        <DataAktywacjiRezerwacji>0</DataAktywacjiRezerwacji>
                        <DokZwiazane>0</DokZwiazane>
                        <TerminPlatnosci>0</TerminPlatnosci>
                        <RabatReq>0</RabatReq>
                        <OpiTyp>0</OpiTyp>
                        <OpiFirma>0</OpiFirma>
                        <OpiNumer>0</OpiNumer>
                        <OpiLp>0</OpiLp>
                        <KnPTyp>0</KnPTyp>
                        <KnPNumer>0</KnPNumer>
                        <AdPNumer>0</AdPNumer>
                        <RezerwujZasoby>0</RezerwujZasoby>
                        <OddDokId>0</OddDokId>
                        <FrsID>0</FrsID>
                        <RodzajCeny>0</RodzajCeny>
                        <KarNumer>0</KarNumer>
                        <WspolnaWaluta>0</WspolnaWaluta>
                        <ProjektID>0</ProjektID>
                        <RabatyOdCenyBezKGO>0</RabatyOdCenyBezKGO>
                        <IgnorujRodzajKnt>0</IgnorujRodzajKnt>
                        <RezerwacjeNaNiepotwierdzonym>0</RezerwacjeNaNiepotwierdzonym>
                        <GenerujWieleZam>0</GenerujWieleZam>
                        <WTRID>0</WTRID>
                        <Opis><![CDATA[ ma kota]]></Opis>
                        <WTRProgID>0</WTRProgID>
                      </XLDokumentZamNagInfo_20141>
                      <XLDokumentZamElemInfo_20141>
                        <Wersja>11</Wersja>
                        <GIDTyp>0</GIDTyp>
                        <GIDFirma>0</GIDFirma>
                        <GIDNumer>0</GIDNumer>
                        <GIDLp>0</GIDLp>
                        <TwrTyp>0</TwrTyp>
                        <TwrFirma>0</TwrFirma>
                        <TwrNumer>374</TwrNumer>
                        <TwrLp>0</TwrLp>
                        <CChTyp>0</CChTyp>
                        <CChFirma>0</CChFirma>
                        <CChNumer>0</CChNumer>
                        <CChLp>0</CChLp>
                        <MagTyp>0</MagTyp>
                        <MagFirma>0</MagFirma>
                        <MagNumer>0</MagNumer>
                        <MagLp>0</MagLp>
                        <ReETyp>0</ReETyp>
                        <ReEFirma>0</ReEFirma>
                        <ReENumer>0</ReENumer>
                        <ReELp>0</ReELp>
                        <KursL>0</KursL>
                        <KursM>0</KursM>
                        <DataWaznosciRezerwacji>0</DataWaznosciRezerwacji>
                        <DataPotwierdzeniaDostawy>0</DataPotwierdzeniaDostawy>
                        <DataAktywacjiRezerwacji>0</DataAktywacjiRezerwacji>
                        <RabatReq>0</RabatReq>
                        <JmFormat>0</JmFormat>
                        <TypJm>0</TypJm>
                        <PrzeliczM>0</PrzeliczM>
                        <PrzeliczL>0</PrzeliczL>
                        <CenaSpr>0</CenaSpr>
                        <PrecyzjaCeny>0</PrecyzjaCeny>
                        <Rownanie>0</Rownanie>
                        <Flagi>0</Flagi>
                        <FlagaVat>0</FlagaVat>
                        <PakietId>0</PakietId>
                        <PromocjaProgID>0</PromocjaProgID>
                        <Gratis>0</Gratis>
                        <Zlom>0</Zlom>
                        <ZrdTyp>0</ZrdTyp>
                        <ZrdFirma>0</ZrdFirma>
                        <ZrdNumer>0</ZrdNumer>
                        <ZrdLp>0</ZrdLp>
                        <IgnorujJmTwr>0</IgnorujJmTwr>
                        <RezMagPulpitKnt>0</RezMagPulpitKnt>
                        <Ilosc>1</Ilosc>
                      </XLDokumentZamElemInfo_20141>
                      <XLDokumentZamElemInfo_20141>
                        <Wersja>10</Wersja>
                        <GIDTyp>0</GIDTyp>
                        <GIDFirma>0</GIDFirma>
                        <GIDNumer>0</GIDNumer>
                        <GIDLp>0</GIDLp>
                        <TwrTyp>0</TwrTyp>
                        <TwrFirma>0</TwrFirma>
                        <TwrNumer>365</TwrNumer>
                        <TwrLp>0</TwrLp>
                        <CChTyp>0</CChTyp>
                        <CChFirma>0</CChFirma>
                        <CChNumer>0</CChNumer>
                        <CChLp>0</CChLp>
                        <MagTyp>0</MagTyp>
                        <MagFirma>0</MagFirma>
                        <MagNumer>0</MagNumer>
                        <MagLp>0</MagLp>
                        <ReETyp>0</ReETyp>
                        <ReEFirma>0</ReEFirma>
                        <ReENumer>0</ReENumer>
                        <ReELp>0</ReELp>
                        <KursL>0</KursL>
                        <KursM>0</KursM>
                        <DataWaznosciRezerwacji>0</DataWaznosciRezerwacji>
                        <DataPotwierdzeniaDostawy>0</DataPotwierdzeniaDostawy>
                        <DataAktywacjiRezerwacji>0</DataAktywacjiRezerwacji>
                        <RabatReq>0</RabatReq>
                        <JmFormat>0</JmFormat>
                        <TypJm>0</TypJm>
                        <PrzeliczM>0</PrzeliczM>
                        <PrzeliczL>0</PrzeliczL>
                        <CenaSpr>0</CenaSpr>
                        <PrecyzjaCeny>0</PrecyzjaCeny>
                        <Rownanie>0</Rownanie>
                        <Flagi>0</Flagi>
                        <FlagaVat>0</FlagaVat>
                        <PakietId>0</PakietId>
                        <PromocjaProgID>0</PromocjaProgID>
                        <Gratis>0</Gratis>
                        <Zlom>0</Zlom>
                        <ZrdTyp>0</ZrdTyp>
                        <ZrdFirma>0</ZrdFirma>
                        <ZrdNumer>0</ZrdNumer>
                        <ZrdLp>0</ZrdLp>
                        <IgnorujJmTwr>0</IgnorujJmTwr>
                        <RezMagPulpitKnt>0</RezMagPulpitKnt>
                        <Ilosc>2</Ilosc>
                      </XLDokumentZamElemInfo_20141>
                    </root>
                    */
                    #endregion
                    Logger.LogDebug("lockSem Start b2b_nowyDokumentZam");
                    Cdnsys th = new Cdnsys();
                    th.AttachThread();
                    //Logger.LogDebug("Thread ID:" + Thread.CurrentThread.ManagedThreadId);
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

                        docId = cdn.NowyDokumentZam(6, xml, ref error, Potwierdzony ? 2 : 0, KodRabatowy);
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
        public string b2b_nowyDokumentZamOsoba(string xml, bool Potwierdzony, string KodRabatowy)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - xml:{xml},potwierdzony:{Potwierdzony},kodrabatowy:{KodRabatowy}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    #region test
                    //xml = @"<?xml version=""1.0""?>
                    //<root>
                    //  <XLDokumentZamNagInfo_20141>
                    //    <Wersja>15</Wersja>
                    //    <GIDTyp>0</GIDTyp>
                    //    <GIDFirma>0</GIDFirma>
                    //    <GIDNumer>0</GIDNumer>
                    //    <GIDLp>0</GIDLp>
                    //    <Typ>6</Typ>
                    //    <Tryb>0</Tryb>
                    //    <Numer>0</Numer>
                    //    <Rok>0</Rok>
                    //    <Miesiac>0</Miesiac>
                    //    <KntTyp>32</KntTyp>
                    //    <KntFirma>0</KntFirma>
                    //    <KntNumer>5711</KntNumer>
                    //    <KntLp>0</KntLp>
                    //    <AdrTyp>0</AdrTyp>
                    //    <AdrFirma>0</AdrFirma>
                    //    <AdrNumer>0</AdrNumer>
                    //    <AdrLp>0</AdrLp>
                    //    <AdwTyp>0</AdwTyp>
                    //    <AdwFirma>0</AdwFirma>
                    //    <AdwNumer>0</AdwNumer>
                    //    <AdwLp>0</AdwLp>
                    //    <AkwTyp>0</AkwTyp>
                    //    <AkwFirma>0</AkwFirma>
                    //    <AkwNumer>0</AkwNumer>
                    //    <AkwLp>0</AkwLp>
                    //    <MagTyp>0</MagTyp>
                    //    <MagFirma>0</MagFirma>
                    //    <MagNumer>0</MagNumer>
                    //    <MagLp>0</MagLp>
                    //    <FormaPl>0</FormaPl>
                    //    <NrKursu>0</NrKursu>
                    //    <TypKursu>0</TypKursu>
                    //    <KursL>0</KursL>
                    //    <KursM>0</KursM>
                    //    <DataWystawienia>77930</DataWystawienia>
                    //    <DataRealizacji>0</DataRealizacji>
                    //    <DataWaznosci>0</DataWaznosci>
                    //    <OpeTypW>0</OpeTypW>
                    //    <OpeFirmaW>0</OpeFirmaW>
                    //    <OpeNumerW>0</OpeNumerW>
                    //    <OpeLpW>0</OpeLpW>
                    //    <OpeTypM>0</OpeTypM>
                    //    <OpeFirmaM>0</OpeFirmaM>
                    //    <OpeNumerM>0</OpeNumerM>
                    //    <OpeLpM>0</OpeLpM>
                    //    <RealWCalosci>0</RealWCalosci>
                    //    <AkwizytorKntPrc>0</AkwizytorKntPrc>
                    //    <ExpoNorm>0</ExpoNorm>
                    //    <Flagi>0</Flagi>
                    //    <Wewnetrzne>0</Wewnetrzne>
                    //    <KnDTyp>0</KnDTyp>
                    //    <KnDFirma>0</KnDFirma>
                    //    <KnDNumer>0</KnDNumer>
                    //    <KnDLp>0</KnDLp>
                    //    <ZrdTyp>0</ZrdTyp>
                    //    <ZrdFirma>0</ZrdFirma>
                    //    <ZrdNumer>0</ZrdNumer>
                    //    <ZrdLp>0</ZrdLp>
                    //    <MagWTyp>0</MagWTyp>
                    //    <MagWFirma>0</MagWFirma>
                    //    <MagWNumer>0</MagWNumer>
                    //    <MagWLp>0</MagWLp>
                    //    <PotwierdzenieOferty>0</PotwierdzenieOferty>
                    //    <DataPotwierdzeniaOferty>0</DataPotwierdzeniaOferty>
                    //    <DataAktywacjiRezerwacji>0</DataAktywacjiRezerwacji>
                    //    <DokZwiazane>0</DokZwiazane>
                    //    <TerminPlatnosci>0</TerminPlatnosci>
                    //    <RabatReq>0</RabatReq>
                    //    <OpiTyp>0</OpiTyp>
                    //    <OpiFirma>0</OpiFirma>
                    //    <OpiNumer>0</OpiNumer>
                    //    <OpiLp>0</OpiLp>
                    //    <KnPTyp>0</KnPTyp>
                    //    <KnPNumer>0</KnPNumer>
                    //    <AdPNumer>0</AdPNumer>
                    //    <RezerwujZasoby>0</RezerwujZasoby>
                    //    <OddDokId>0</OddDokId>
                    //    <FrsID>0</FrsID>
                    //    <RodzajCeny>0</RodzajCeny>
                    //    <KarNumer>0</KarNumer>
                    //    <WspolnaWaluta>0</WspolnaWaluta>
                    //    <ProjektID>0</ProjektID>
                    //    <RabatyOdCenyBezKGO>0</RabatyOdCenyBezKGO>
                    //    <IgnorujRodzajKnt>0</IgnorujRodzajKnt>
                    //    <RezerwacjeNaNiepotwierdzonym>0</RezerwacjeNaNiepotwierdzonym>
                    //    <GenerujWieleZam>0</GenerujWieleZam>
                    //    <WTRID>0</WTRID>
                    //    <Opis><![CDATA[ ma kota]]></Opis>
                    //    <WTRProgID>0</WTRProgID>
                    //  </XLDokumentZamNagInfo_20141>
                    //  <XLDokumentZamElemInfo_20141>
                    //    <Wersja>11</Wersja>
                    //    <GIDTyp>0</GIDTyp>
                    //    <GIDFirma>0</GIDFirma>
                    //    <GIDNumer>0</GIDNumer>
                    //    <GIDLp>0</GIDLp>
                    //    <TwrTyp>0</TwrTyp>
                    //    <TwrFirma>0</TwrFirma>
                    //    <TwrNumer>374</TwrNumer>
                    //    <TwrLp>0</TwrLp>
                    //    <CChTyp>0</CChTyp>
                    //    <CChFirma>0</CChFirma>
                    //    <CChNumer>0</CChNumer>
                    //    <CChLp>0</CChLp>
                    //    <MagTyp>0</MagTyp>
                    //    <MagFirma>0</MagFirma>
                    //    <MagNumer>0</MagNumer>
                    //    <MagLp>0</MagLp>
                    //    <ReETyp>0</ReETyp>
                    //    <ReEFirma>0</ReEFirma>
                    //    <ReENumer>0</ReENumer>
                    //    <ReELp>0</ReELp>
                    //    <KursL>0</KursL>
                    //    <KursM>0</KursM>
                    //    <DataWaznosciRezerwacji>0</DataWaznosciRezerwacji>
                    //    <DataPotwierdzeniaDostawy>0</DataPotwierdzeniaDostawy>
                    //    <DataAktywacjiRezerwacji>0</DataAktywacjiRezerwacji>
                    //    <RabatReq>0</RabatReq>
                    //    <JmFormat>0</JmFormat>
                    //    <TypJm>0</TypJm>
                    //    <PrzeliczM>0</PrzeliczM>
                    //    <PrzeliczL>0</PrzeliczL>
                    //    <CenaSpr>0</CenaSpr>
                    //    <PrecyzjaCeny>0</PrecyzjaCeny>
                    //    <Rownanie>0</Rownanie>
                    //    <Flagi>0</Flagi>
                    //    <FlagaVat>0</FlagaVat>
                    //    <PakietId>0</PakietId>
                    //    <PromocjaProgID>0</PromocjaProgID>
                    //    <Gratis>0</Gratis>
                    //    <Zlom>0</Zlom>
                    //    <ZrdTyp>0</ZrdTyp>
                    //    <ZrdFirma>0</ZrdFirma>
                    //    <ZrdNumer>0</ZrdNumer>
                    //    <ZrdLp>0</ZrdLp>
                    //    <IgnorujJmTwr>0</IgnorujJmTwr>
                    //    <RezMagPulpitKnt>0</RezMagPulpitKnt>
                    //    <Ilosc>1</Ilosc>
                    //  </XLDokumentZamElemInfo_20141>
                    //  <XLDokumentZamElemInfo_20141>
                    //    <Wersja>10</Wersja>
                    //    <GIDTyp>0</GIDTyp>
                    //    <GIDFirma>0</GIDFirma>
                    //    <GIDNumer>0</GIDNumer>
                    //    <GIDLp>0</GIDLp>
                    //    <TwrTyp>0</TwrTyp>
                    //    <TwrFirma>0</TwrFirma>
                    //    <TwrNumer>365</TwrNumer>
                    //    <TwrLp>0</TwrLp>
                    //    <CChTyp>0</CChTyp>
                    //    <CChFirma>0</CChFirma>
                    //    <CChNumer>0</CChNumer>
                    //    <CChLp>0</CChLp>
                    //    <MagTyp>0</MagTyp>
                    //    <MagFirma>0</MagFirma>
                    //    <MagNumer>0</MagNumer>
                    //    <MagLp>0</MagLp>
                    //    <ReETyp>0</ReETyp>
                    //    <ReEFirma>0</ReEFirma>
                    //    <ReENumer>0</ReENumer>
                    //    <ReELp>0</ReELp>
                    //    <KursL>0</KursL>
                    //    <KursM>0</KursM>
                    //    <DataWaznosciRezerwacji>0</DataWaznosciRezerwacji>
                    //    <DataPotwierdzeniaDostawy>0</DataPotwierdzeniaDostawy>
                    //    <DataAktywacjiRezerwacji>0</DataAktywacjiRezerwacji>
                    //    <RabatReq>0</RabatReq>
                    //    <JmFormat>0</JmFormat>
                    //    <TypJm>0</TypJm>
                    //    <PrzeliczM>0</PrzeliczM>
                    //    <PrzeliczL>0</PrzeliczL>
                    //    <CenaSpr>0</CenaSpr>
                    //    <PrecyzjaCeny>0</PrecyzjaCeny>
                    //    <Rownanie>0</Rownanie>
                    //    <Flagi>0</Flagi>
                    //    <FlagaVat>0</FlagaVat>
                    //    <PakietId>0</PakietId>
                    //    <PromocjaProgID>0</PromocjaProgID>
                    //    <Gratis>0</Gratis>
                    //    <Zlom>0</Zlom>
                    //    <ZrdTyp>0</ZrdTyp>
                    //    <ZrdFirma>0</ZrdFirma>
                    //    <ZrdNumer>0</ZrdNumer>
                    //    <ZrdLp>0</ZrdLp>
                    //    <IgnorujJmTwr>0</IgnorujJmTwr>
                    //    <RezMagPulpitKnt>0</RezMagPulpitKnt>
                    //    <Ilosc>2</Ilosc>
                    //  </XLDokumentZamElemInfo_20141>
                    //</root>";
                    #endregion
                    Logger.LogDebug("lockSem Start b2b_nowyDokumentZam");
                    Cdnsys th = new Cdnsys();
                    th.AttachThread();
                    //Logger.LogDebug("Thread ID:" + Thread.CurrentThread.ManagedThreadId);
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

                        docId = cdn.NowyDokumentZamOsoba(6, xml, ref error, Potwierdzony ? 2 : 0, KodRabatowy);
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

                    CDNResponse resp = new CDNResponse
                    {
                        Id = docId,
                        Error = error,
                        Numer = numer
                    };
                    Logger.LogDebug("lockSem End b2b_nowyDokumentZam");
                    return ResponseSerializer.Serialize(resp);

                }
            }
        }
        #endregion

        #region b2b_SposobyPlatnosci
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_SposobyPlatnosci()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                var t = DBHelper.RunSqlQuery("SELECT [Kon_Lp] Id ,[Kon_Wartosc] Nazwa FROM [CDN].[Konfig] where [Kon_Numer] = 736", "SposobyPlatnosci");
                foreach (DataRow i in t.Rows)
                {
                    var pom = i["Nazwa"].ToString();
                    i["Nazwa"] = pom.Trim().Substring(0, pom.Trim().IndexOf(' '));
                }
                return t.GetJson();
            }
        }
        #endregion

        #region b2b_PromocjeDodatkowe
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_PromocjeDodatkowe()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                return DBHelper.RunSqlProc("b2b_PromocjeDodatkowe", "t").GetJson();
            }
        }
        #endregion
        #region b2b_PromocjeDodatkoweKontrahenci
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_PromocjeDodatkoweKontrahenci()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                return DBHelper.RunSqlProc("b2b_PromocjeDodatkoweKontrahenci", "t").GetJson();
            }
        }
        #endregion
        #region b2b_PromocjeDodatkoweTowary
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_PromocjeDodatkoweTowary()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                return DBHelper.RunSqlProc("b2b_PromocjeDodatkoweTowary", "t").GetJson();
            }
        }
        #endregion
        //usage stats logging done powyzej do zrobienia
        #region b2b_nowyDokumentZamHan
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_nowyDokumentZamHan(string zamXml, string hanXml, string pathToSet, string KodRabatowy)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - zamXml:{zamXml},hanXml:{hanXml},pathToSet:{pathToSet},kodrabatowy:{KodRabatowy}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    Logger.LogDebug(string.Format("b2b_nowyDokumentZamHan zamXml: {0}, hanXml: {1}, pathToSet: {2}", zamXml.ToString(), hanXml, pathToSet));
                    #region pathToSet
                    //"/root/XLDokumentNagInfo_20193/ZamNumer"
                    #endregion

                    #region zamXml
                    //            <?xml version="1.0"?>
                    //<root>
                    //    <XLDokumentZamNagInfo_20193>
                    //        <Wersja></Wersja>
                    //        <GIDTyp>0</GIDTyp>
                    //        <GIDFirma>0</GIDFirma>
                    //        <GIDNumer>0</GIDNumer>
                    //        <GIDLp>0</GIDLp>
                    //        <Typ>6</Typ>
                    //        <Tryb>0</Tryb>
                    //        <Numer>0</Numer>
                    //        <Rok>0</Rok>
                    //        <Miesiac>0</Miesiac>
                    //        <KntTyp>32</KntTyp>
                    //        <KntFirma>0</KntFirma>
                    //        <KntNumer>4306</KntNumer>
                    //        <KntLp>0</KntLp>
                    //        <AdrTyp>0</AdrTyp>
                    //        <AdrFirma>0</AdrFirma>
                    //        <AdrNumer>0</AdrNumer>
                    //        <AdrLp>0</AdrLp>
                    //        <AdwTyp>0</AdwTyp>
                    //        <AdwFirma>0</AdwFirma>
                    //        <AdwNumer>0</AdwNumer>
                    //        <AdwLp>0</AdwLp>
                    //        <AkwTyp>0</AkwTyp>
                    //        <AkwFirma>0</AkwFirma>
                    //        <AkwNumer>0</AkwNumer>
                    //        <AkwLp>0</AkwLp>
                    //        <MagTyp>0</MagTyp>
                    //        <MagFirma>0</MagFirma>
                    //        <MagNumer>0</MagNumer>
                    //        <MagLp>0</MagLp>
                    //        <FormaPl>0</FormaPl>
                    //        <NrKursu>0</NrKursu>
                    //        <TypKursu>0</TypKursu>
                    //        <KursL>0</KursL>
                    //        <KursM>0</KursM>
                    //        <DataWystawienia>78968</DataWystawienia>
                    //        <DataRealizacji>0</DataRealizacji>
                    //        <DataWaznosci>0</DataWaznosci>
                    //        <OpeTypW>0</OpeTypW>
                    //        <OpeFirmaW>0</OpeFirmaW>
                    //        <OpeNumerW>0</OpeNumerW>
                    //        <OpeLpW>0</OpeLpW>
                    //        <OpeTypM>0</OpeTypM>
                    //        <OpeFirmaM>0</OpeFirmaM>
                    //        <OpeNumerM>0</OpeNumerM>
                    //        <OpeLpM>0</OpeLpM>
                    //        <RealWCalosci>0</RealWCalosci>
                    //        <AkwizytorKntPrc>0</AkwizytorKntPrc>
                    //        <ExpoNorm>0</ExpoNorm>
                    //        <Flagi>0</Flagi>
                    //        <Wewnetrzne>0</Wewnetrzne>
                    //        <KnDTyp>0</KnDTyp>
                    //        <KnDFirma>0</KnDFirma>
                    //        <KnDNumer>0</KnDNumer>
                    //        <KnDLp>0</KnDLp>
                    //        <ZrdTyp>0</ZrdTyp>
                    //        <ZrdFirma>0</ZrdFirma>
                    //        <ZrdNumer>0</ZrdNumer>
                    //        <ZrdLp>0</ZrdLp>
                    //        <MagWTyp>0</MagWTyp>
                    //        <MagWFirma>0</MagWFirma>
                    //        <MagWNumer>0</MagWNumer>
                    //        <MagWLp>0</MagWLp>
                    //        <PotwierdzenieOferty>0</PotwierdzenieOferty>
                    //        <DataPotwierdzeniaOferty>0</DataPotwierdzeniaOferty>
                    //        <DataAktywacjiRezerwacji>0</DataAktywacjiRezerwacji>
                    //        <DokZwiazane>0</DokZwiazane>
                    //        <TerminPlatnosci>0</TerminPlatnosci>
                    //        <RabatReq>0</RabatReq>
                    //        <OpiTyp>0</OpiTyp>
                    //        <OpiFirma>0</OpiFirma>
                    //        <OpiNumer>0</OpiNumer>
                    //        <OpiLp>0</OpiLp>
                    //        <KnPTyp>0</KnPTyp>
                    //        <KnPNumer>0</KnPNumer>
                    //        <AdPNumer>0</AdPNumer>
                    //        <RezerwujZasoby>0</RezerwujZasoby>
                    //        <OddDokId>0</OddDokId>
                    //        <FrsID>0</FrsID>
                    //        <RodzajCeny>0</RodzajCeny>
                    //        <KarNumer>0</KarNumer>
                    //        <WspolnaWaluta>0</WspolnaWaluta>
                    //        <ProjektID>0</ProjektID>
                    //        <RabatyOdCenyBezKGO>0</RabatyOdCenyBezKGO>
                    //        <IgnorujRodzajKnt>0</IgnorujRodzajKnt>
                    //        <RezerwacjeNaNiepotwierdzonym>0</RezerwacjeNaNiepotwierdzonym>
                    //        <GenerujWieleZam>0</GenerujWieleZam>
                    //        <WTRID>0</WTRID>
                    //        <Opis><![CDATA[ test zs zw]]></Opis>
                    //        <WTRProgID>0</WTRProgID>

                    //  </XLDokumentZamNagInfo_20193>
                    //    <XLDokumentZamElemInfo_20193>
                    //        <Wersja>11</Wersja>
                    //        <GIDTyp>0</GIDTyp>
                    //        <GIDFirma>0</GIDFirma>
                    //        <GIDNumer>0</GIDNumer>
                    //        <GIDLp>0</GIDLp>
                    //        <TwrTyp>0</TwrTyp>
                    //        <TwrFirma>0</TwrFirma>
                    //        <TwrNumer>2</TwrNumer>
                    //        <TwrLp>0</TwrLp>
                    //        <CChTyp>0</CChTyp>
                    //        <CChFirma>0</CChFirma>
                    //        <CChNumer>0</CChNumer>
                    //        <CChLp>0</CChLp>
                    //        <MagTyp>0</MagTyp>
                    //        <MagFirma>0</MagFirma>
                    //        <MagNumer>0</MagNumer>
                    //        <MagLp>0</MagLp>
                    //        <ReETyp>0</ReETyp>
                    //        <ReEFirma>0</ReEFirma>
                    //        <ReENumer>0</ReENumer>
                    //        <ReELp>0</ReELp>
                    //        <KursL>0</KursL>
                    //        <KursM>0</KursM>
                    //        <DataWaznosciRezerwacji>0</DataWaznosciRezerwacji>
                    //        <DataPotwierdzeniaDostawy>0</DataPotwierdzeniaDostawy>
                    //        <DataAktywacjiRezerwacji>0</DataAktywacjiRezerwacji>
                    //        <RabatReq>0</RabatReq>
                    //        <JmFormat>0</JmFormat>
                    //        <TypJm>0</TypJm>
                    //        <PrzeliczM>0</PrzeliczM>
                    //        <PrzeliczL>0</PrzeliczL>
                    //        <CenaSpr>0</CenaSpr>
                    //        <PrecyzjaCeny>0</PrecyzjaCeny>
                    //        <Rownanie>0</Rownanie>
                    //        <Flagi>0</Flagi>
                    //        <FlagaVat>0</FlagaVat>
                    //        <PakietId>0</PakietId>
                    //        <PromocjaProgID>0</PromocjaProgID>
                    //        <Gratis>0</Gratis>
                    //        <Zlom>0</Zlom>
                    //        <ZrdTyp>0</ZrdTyp>
                    //        <ZrdFirma>0</ZrdFirma>
                    //        <ZrdNumer>0</ZrdNumer>
                    //        <ZrdLp>0</ZrdLp>
                    //        <IgnorujJmTwr>0</IgnorujJmTwr>
                    //        <RezMagPulpitKnt>0</RezMagPulpitKnt>
                    //        <Ilosc>1</Ilosc>

                    //  </XLDokumentZamElemInfo_20193>
                    //</root>
                    #endregion
                    #region hanXml
                    //            <?xml version="1.0"?>
                    //<root>
                    //  <XLDokumentNagInfo_20193>
                    //    <Wersja>0</Wersja>
                    //    <GIDTyp>0</GIDTyp>
                    //    <GIDFirma>0</GIDFirma>
                    //    <GIDNumer>0</GIDNumer>
                    //    <GIDLp>0</GIDLp>
                    //    <Typ>960</Typ>
                    //    <Numer>0</Numer>
                    //    <Rok>0</Rok>
                    //    <Miesiac>0</Miesiac>
                    //    <Korekta>0</Korekta>
                    //    <Avista>0</Avista>
                    //    <Anulowany>0</Anulowany>
                    //    <Spinacz>0</Spinacz>
                    //    <Tryb>0</Tryb>
                    //    <Data>0</Data>
                    //    <DataSpr>0</DataSpr>
                    //    <DataVat>0</DataVat>
                    //    <DataMag>0</DataMag>
                    //    <Termin>0</Termin>
                    //    <KursNr>0</KursNr>
                    //    <KursL>0</KursL>
                    //    <KursM>0</KursM>
                    //    <RabatReq>0</RabatReq>
                    //    <Rabat>0</Rabat>
                    //    <Forma>0</Forma>
                    //    <KntTyp>32</KntTyp>
                    //    <KntFirma>0</KntFirma>
                    //    <KntNumer>4306</KntNumer>
                    //    <KntLp>0</KntLp>
                    //    <AdrTyp>0</AdrTyp>
                    //    <AdrFirma>0</AdrFirma>
                    //    <AdrNumer>0</AdrNumer>
                    //    <AdrLp>0</AdrLp>
                    //    <AdwTyp>0</AdwTyp>
                    //    <AdwFirma>0</AdwFirma>
                    //    <AdwNumer>0</AdwNumer>
                    //    <AdwLp>0</AdwLp>
                    //    <AkwTyp>0</AkwTyp>
                    //    <AkwFirma>0</AkwFirma>
                    //    <AkwNumer>0</AkwNumer>
                    //    <AkwLp>0</AkwLp>
                    //    <OpiTyp>0</OpiTyp>
                    //    <OpiFirma>0</OpiFirma>
                    //    <OpiNumer>0</OpiNumer>
                    //    <OpiLp>0</OpiLp>
                    //    <KnDTyp>0</KnDTyp>
                    //    <KnDFirma>0</KnDFirma>
                    //    <KnDNumer>0</KnDNumer>
                    //    <KnDLp>0</KnDLp>
                    //    <ZamTyp>0</ZamTyp>
                    //    <ZamFirma>0</ZamFirma>
                    //    <ZamNumer>3</ZamNumer>
                    //    <ZamLp>0</ZamLp>
                    //    <ZwrTyp>0</ZwrTyp>
                    //    <ZwrFirma>0</ZwrFirma>
                    //    <ZwrNumer>0</ZwrNumer>
                    //    <ZwrLp>0</ZwrLp>
                    //    <SaNTyp>0</SaNTyp>
                    //    <SaNFirma>0</SaNFirma>
                    //    <SaNNumer>0</SaNNumer>
                    //    <SaNLp>0</SaNLp>
                    //    <KnOTyp>0</KnOTyp>
                    //    <KnOFirma>0</KnOFirma>
                    //    <KnONumer>0</KnONumer>
                    //    <KnOLp>0</KnOLp>
                    //    <KnPTyp>0</KnPTyp>
                    //    <KnPNumer>0</KnPNumer>
                    //    <AdPNumer>0</AdPNumer>
                    //    <Flagi>0</Flagi>
                    //    <ExpoNorm>0</ExpoNorm>
                    //    <AkwizytorKntPrc>0</AkwizytorKntPrc>
                    //    <MagazynSklad>0</MagazynSklad>
                    //    <FRSID>0</FRSID>
                    //    <GenerujPlatnosci>0</GenerujPlatnosci>
                    //    <TerminRozliczeniaKaucji>0</TerminRozliczeniaKaucji>
                    //    <AktualizacjaKaucji>0</AktualizacjaKaucji>
                    //    <KosztUstalono>0</KosztUstalono>
                    //    <RSDataOd>0</RSDataOd>
                    //    <RSDataDo>0</RSDataDo>
                    //    <MiesiacVAT>0</MiesiacVAT>
                    //    <RokVAT>0</RokVAT>
                    //    <DeklaracjaVAT7>0</DeklaracjaVAT7>
                    //    <DeklaracjaVATUE>0</DeklaracjaVATUE>
                    //    <RodzajCeny>0</RodzajCeny>
                    //    <KarNumer>0</KarNumer>
                    //    <Fiskalny>0</Fiskalny>
                    //    <IDOddzial>0</IDOddzial>
                    //    <Zwiazane>0</Zwiazane>
                    //    <ProjektID>0</ProjektID>
                    //    <RabatyOdCenyBezKGO>0</RabatyOdCenyBezKGO>
                    //    <IgnorujRodzajKnt>0</IgnorujRodzajKnt>
                    //    <KonTyp>0</KonTyp>
                    //    <KonNumer>0</KonNumer>
                    //    <PrzywracajRezerwacje>0</PrzywracajRezerwacje>
                    //    <IgnorujZaliczkowe>0</IgnorujZaliczkowe>
                    //    <IgnorujMagazynowe>0</IgnorujMagazynowe>
                    //    <WtrID>0</WtrID>
                    //    <WtrProgID>0</WtrProgID>
                    //    <Zbiorcza>0</Zbiorcza>
                    //    <RodzajKor>0</RodzajKor>
                    //    <TypDatyKor>0</TypDatyKor>
                    //    <DataOdKor>0</DataOdKor>
                    //    <DataDoKor>0</DataDoKor>
                    //    <RodzajZakupu>0</RodzajZakupu>
                    //    <OdliczeniaVat>0</OdliczeniaVat>
                    //    <Rozliczac>0</Rozliczac>
                    //    <Struktura>0</Struktura>
                    //    <RozliczacP>0</RozliczacP>
                    //    <WMS>0</WMS>
                    //    <PodstawaZW>0</PodstawaZW>
                    //    <Seria></Seria>
                    //    <Akronim></Akronim>
                    //    <Akwizytor></Akwizytor>
                    //    <Docelowy></Docelowy>
                    //    <Osoba></Osoba>
                    //    <Platnik></Platnik>
                    //    <SposobDst></SposobDst>
                    //    <NB></NB>
                    //    <Waluta></Waluta>
                    //    <MagazynZ></MagazynZ>
                    //    <MagazynD></MagazynD>
                    //    <Rejestr></Rejestr>
                    //    <DokumentObcy></DokumentObcy>
                    //    <Cecha></Cecha>
                    //    <Kraj></Kraj>
                    //    <KodRodzajuTransportu></KodRodzajuTransportu>
                    //    <KodRodzajuTransakcji></KodRodzajuTransakcji>
                    //    <Opis></Opis>
                    //    <IncotermsSymbol></IncotermsSymbol>
                    //    <IncotermsMiejsce></IncotermsMiejsce>
                    //    <URL></URL>
                    //    <PrzyczynaKorekty></PrzyczynaKorekty>
                    //    <PrzyczynaZW></PrzyczynaZW>
                    //  </XLDokumentNagInfo_20193>
                    //  <XLDokumentElemInfo_20193>
                    //    <Wersja>0</Wersja>
                    //    <GIDTyp>0</GIDTyp>
                    //    <GIDFirma>0</GIDFirma>
                    //    <GIDNumer>0</GIDNumer>
                    //    <GIDLp>0</GIDLp>
                    //    <GIDLpOrg>0</GIDLpOrg>
                    //    <SubLpOrg>0</SubLpOrg>
                    //    <PozycjaOrg>0</PozycjaOrg>
                    //    <TypKorekty>0</TypKorekty>
                    //    <IloscReq>0</IloscReq>
                    //    <JmFormat>0</JmFormat>
                    //    <TypJm>0</TypJm>
                    //    <PrzeliczM>0</PrzeliczM>
                    //    <PrzeliczL>0</PrzeliczL>
                    //    <WartoscOst>0</WartoscOst>
                    //    <Rownanie>0</Rownanie>
                    //    <OdKsiegowych>0</OdKsiegowych>
                    //    <PrecyzjaCeny>0</PrecyzjaCeny>
                    //    <Rabat>0</Rabat>
                    //    <DataKursu>0</DataKursu>
                    //    <KursNr>0</KursNr>
                    //    <KursL>0</KursL>
                    //    <KursM>0</KursM>
                    //    <ZamTyp>0</ZamTyp>
                    //    <ZamFirma>0</ZamFirma>
                    //    <ZamNumer>0</ZamNumer>
                    //    <ZamLp>0</ZamLp>
                    //    <RezIgnor>0</RezIgnor>
                    //    <TwrTyp>0</TwrTyp>
                    //    <TwrFirma>0</TwrFirma>
                    //    <TwrNumer>30</TwrNumer>
                    //    <TwrLp>0</TwrLp>
                    //    <DstTyp>0</DstTyp>
                    //    <DstFirma>0</DstFirma>
                    //    <DstNumer>0</DstNumer>
                    //    <DstLp>0</DstLp>
                    //    <CChTyp>0</CChTyp>
                    //    <CChFirma>0</CChFirma>
                    //    <CChNumer>0</CChNumer>
                    //    <CChLp>0</CChLp>
                    //    <CechaReq>0</CechaReq>
                    //    <CCh2Typ>0</CCh2Typ>
                    //    <CCh2Numer>0</CCh2Numer>
                    //    <RecTyp>0</RecTyp>
                    //    <RecFirma>0</RecFirma>
                    //    <RecNumer>0</RecNumer>
                    //    <RecLp>0</RecLp>
                    //    <MagReq>0</MagReq>
                    //    <SposobPobieraniaZMag>0</SposobPobieraniaZMag>
                    //    <TwrReq>0</TwrReq>
                    //    <NiePrzeliczaj>0</NiePrzeliczaj>
                    //    <KosztUstalono>0</KosztUstalono>
                    //    <DataWaznosci>0</DataWaznosci>
                    //    <TpaID>0</TpaID>
                    //    <CenaSpr>0</CenaSpr>
                    //    <Flagi>0</Flagi>
                    //    <Agreguj>0</Agreguj>
                    //    <FlagaVAT>0</FlagaVAT>
                    //    <StawkaPod>0</StawkaPod>
                    //    <PakietId>0</PakietId>
                    //    <Gratis>0</Gratis>
                    //    <PromocjaProgID>0</PromocjaProgID>
                    //    <Zlom>0</Zlom>
                    //    <StawkaPodPrzed>0</StawkaPodPrzed>
                    //    <FlagaVATPrzed>0</FlagaVATPrzed>
                    //    <NieKontrolujMaxRabOpe>0</NieKontrolujMaxRabOpe>
                    //    <Ilosc>1</Ilosc>
                    //    <CenaP></CenaP>
                    //    <Cena></Cena>
                    //    <Wartosc></Wartosc>
                    //    <WartoscR></WartoscR>
                    //    <TowarKod></TowarKod>
                    //    <TowarEAN></TowarEAN>
                    //    <TowarNazwa></TowarNazwa>
                    //    <JmZ></JmZ>
                    //    <Vat></Vat>
                    //    <Magazyn></Magazyn>
                    //    <Waluta></Waluta>
                    //    <Cecha></Cecha>
                    //    <Cecha2></Cecha2>
                    //    <Kraj></Kraj>
                    //    <PCN></PCN>
                    //    <Opis></Opis>
                    //    <KGO></KGO>
                    //    <VatPrzed></VatPrzed>
                    //    <IloscPrzed></IloscPrzed>
                    //    <CenaPrzed></CenaPrzed>
                    //    <WartoscPrzed></WartoscPrzed>
                    //    <PrzyczynaKorekty></PrzyczynaKorekty>
                    //    <BudzetPrmID></BudzetPrmID>
                    //    <BudzetWartosc></BudzetWartosc>
                    //  </XLDokumentElemInfo_20193>
                    //</root>
                    #endregion
                    //            Cdnxl 2016.3
                    //Baza Kabis Sp.z.oo
                    Cdnsys th = new Cdnsys();
                    th.AttachThread();
                    List<CDNResponse> respList = new List<CDNResponse>();
                    respList.Add(new CDNResponse());
                    respList.Add(new CDNResponse());
                    CDNOperations.CDNOperations cdn = StartCDNOperation("b2b_nowyDokumentZamHan");
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

                        docId = cdn.NowyDokumentZam(6, zamXml, ref error, 2, KodRabatowy);
                        if (string.IsNullOrEmpty(error))
                        {
                            numer = DBHelper.GetNumerDokumentuTRN(docId);
                            CDNResponse resp0 = respList[0];
                            resp0.Id = docId;
                            resp0.Error = error;
                            resp0.Numer = numer;

                            hanXml = XmlHelper.SetElementValue(hanXml, pathToSet, docId.ToString());
                            int gidtyp = 0;
                            docId = 0;
                            docId = cdn.NowyDokument(hanXml, ref gidtyp, ref error);
                            numer = "";
                            if (string.IsNullOrEmpty(error))
                            {
                                numer = cdn.PobierzNumerDokumentu(docId, gidtyp, ref error);
                                CDNResponse resp1 = respList[1];
                                resp1.Id = docId;
                                resp1.Error = error;
                                resp1.Numer = numer;
                            }
                            else
                            {
                                CDNResponse resp1 = respList[1];
                                resp1.Id = docId;
                                resp1.Error = error;
                                resp1.Numer = numer;
                            }
                        }
                        else
                        {
                            CDNResponse resp0 = respList[0];
                            resp0.Id = docId;
                            resp0.Error = error;
                            resp0.Numer = numer;
                        }
                    }
                    else
                    {
                        error = "Nie udalo się zainicjować sesji";
                    }

                    EndCDNOperation("b2b_nowyDokumentZamHan", cdn);

                    return ResponseSerializer.Serialize(respList);
                }
            }
        }
        #endregion

        #region b2b_adresy
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_adresykontrahentow()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_adresykontrahentow()", "b2b_adresykontrahentow");
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

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
        #endregion

        #region reklamacje

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_SprzedazKontrahentaSN(int kntnumer, string searchString, int searchType)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - kntnumer:{kntnumer},searchString:{searchString},type:{searchType}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                try
                {
                    return WSBReklamacje.B2B_SprzedazKontrahentaSN(kntnumer, searchString, searchType);
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    return null;
                }
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ReklSprzedazKontrahentaSN(int kntnumer, string searchString, int searchType)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - kntnumer:{kntnumer},searchString:{searchString},type:{searchType}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                try
                {
                    return WSBReklamacje.B2B_SprzedazKontrahentaSN(kntnumer, searchString, searchType);
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    return null;
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ReklZadaniaSlownik()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                try
                {
                    return WSBReklamacje.B2B_ZadaniaReklamacji();
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    return null;
                }
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ReklAtrybutySlowniki()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                try
                {
                    return WSBReklamacje.B2B_ReklamacjeAtrybutySlowniki();
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    return null;
                }
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ReklNowaReklamacja(string xml, byte[] zal, string zalNazwa, string zalRozszerzenie)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - xml:{xml}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                try
                {
                    return WSBReklamacje.B2B_NowaReklamacja(xml, zal, zalNazwa, zalRozszerzenie);
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    return null;
                }
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ReklNag(int gidnumer, int rlnzadanie)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - gidnumer:{gidnumer},rlnzadanie:{rlnzadanie}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@gidnumer", gidnumer));
                lstPar.Add(new QueryParam("@rlnzadanie", rlnzadanie));
                try
                {
                    DataTable dt = DBHelper.RunSqlProcParam("dbo.b2b_spReklNag", "b2b_reklnag", lstPar);
                    if (dt != null)
                    {
                        return DataTableHelper.GetJson(dt);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.Message);
                    return null;
                }
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ReklNagSearch(int gidnumer, int rlnzadanie, string status, string search)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - gidnumer:{gidnumer},rlnzadanie:{rlnzadanie}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@gidnumer", gidnumer));
                lstPar.Add(new QueryParam("@rlnzadanie", rlnzadanie));
                lstPar.Add(new QueryParam("@status", status));
                lstPar.Add(new QueryParam("@search", search));
                try
                {
                    DataTable dt = DBHelper.RunSqlProcParam("dbo.b2b_spReklNag", "b2b_reklnag", lstPar);
                    if (dt != null)
                    {
                        return DataTableHelper.GetJson(dt);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.Message);
                    return null;
                }
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ReklElem(int gidnumer, int kndnumer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - gidnumer:{gidnumer},kndnumer:{kndnumer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@gidnumer", gidnumer));
                lstPar.Add(new QueryParam("@kndnumer", kndnumer));
                try
                {
                    DataTable dt = DBHelper.RunSqlProcParam("dbo.b2b_spReklElem", "b2b_reklelem", lstPar);
                    if (dt != null)
                    {
                        return DataTableHelper.GetJson(dt);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.Message);
                    return null;
                }
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ReklNowyKontrahent(string nip, string kraj, string nazwa, string ulica, string miasto, string kodPocztowy)
        {
            string message = "";
            if (LogUsage.Params) message = $"WZ Parameters - nip:{nip},kraj:{kraj},nazwa:{nazwa},ulica:{ulica},miasto:{miasto},kodPocztowy:{kodPocztowy}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    var operacje = new CDNOperations.CDNOperations(SesjaId);
                    return DataTableHelper.GetJson(operacje.NowyKontrahent(nip, kraj, nazwa, ulica, miasto, kodPocztowy, "REK", @"GRUPY\|REKLAMACJE"));
                }
            }
        }
        #endregion

        #region reklmacje
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_NowaReklamacja(int M_KontrahentId, int M_TowarId, decimal M_Ilosc, int M_IloscMiejscPoPrzecinku, int M_ZadanieId, string Przyczyna = "", string Opis = "")
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - duzo";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                Logger.LogDebug("b2b_NowaReklamacja START");
                Logger.LogDebug($"b2b_NowaReklamacja PARAM {M_KontrahentId} {M_TowarId} {M_Ilosc} {M_IloscMiejscPoPrzecinku} {M_ZadanieId} {Przyczyna} {Opis}");
                var ret = new DataTable();
                ret.Columns.Add("Powodzenie");
                ret.Columns.Add("Id");
                ret.Columns.Add("DokumentNr");
                try
                {
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                    {
                        con.Open();
                        using (var com = con.CreateCommand())
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "CDN.XLNowaReklamacja";

                            var Id = com.Parameters.AddWithValue("@Id", 0);
                            var DokumentNr = com.Parameters.AddWithValue("@DokumentNr", "");
                            DokumentNr.SqlDbType = SqlDbType.NVarChar;
                            DokumentNr.Size = 40;
                            Id.Direction = DokumentNr.Direction = ParameterDirection.Output;

                            com.Parameters.AddWithValue("@RlnTyp", 3584);
                            com.Parameters.AddWithValue("@RodId", -1);
                            com.Parameters.AddWithValue("@Ilosc", M_Ilosc);
                            com.Parameters.AddWithValue("@JmFormat", M_IloscMiejscPoPrzecinku);
                            com.Parameters.AddWithValue("@TwrTyp", 16);
                            com.Parameters.AddWithValue("@TwrNumer", M_TowarId);
                            com.Parameters.AddWithValue("@KntTyp", 32);
                            com.Parameters.AddWithValue("@KntFirma", 1);
                            com.Parameters.AddWithValue("@KntNumer", M_KontrahentId);
                            com.Parameters.AddWithValue("@KntLp", 0);
                            com.Parameters.AddWithValue("@Zadanie", M_ZadanieId);
                            com.Parameters.AddWithValue("@DokumentObcy", "");
                            com.Parameters.AddWithValue("@Przyczyna", Przyczyna ?? "");
                            com.Parameters.AddWithValue("@Opis", Opis ?? "");

                            int pret = (com.ExecuteScalar() as int?) ?? 0;

                            ret.Rows.Add(new[] { ((int)pret) == 0, Id.Value, DokumentNr.Value });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException($"b2b_NowaReklamacja {ex.Message}");
                    ret.Rows.Add(new object[] { false, 0, 0 });
                }
                Logger.LogDebug($"b2b_NowaReklamacja RET: {ret.GetJson()}");
                Logger.LogDebug("b2b_NowaReklamacja STOP");
                return ret.GetJson();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_DodajPozycjeReklamacji(int M_IdReklamacji, int M_KntNumer, int M_TwrNumer, decimal M_Ilosc, int M_IloscMiejscPoPrzecinku, int M_Zadanie, string Przyczyna = "")
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - duzo";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                Logger.LogDebug("b2b_DodajPozycjeReklamacji START");
                Logger.LogDebug($"b2b_DodajPozycjeReklamacji PAR: {M_IdReklamacji} {M_KntNumer} {M_TwrNumer} {M_Ilosc} {M_IloscMiejscPoPrzecinku} {M_Zadanie} {Przyczyna}");
                var ret = new DataTable();
                ret.Columns.Add("Powodzenie");
                ret.Columns.Add("Id");
                try
                {
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                    {
                        con.Open();
                        using (var com = con.CreateCommand())
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "CDN.XLDodajPozycjeReklamacji";

                            var Id = com.Parameters.AddWithValue("@RleId", 0);
                            Id.Direction = ParameterDirection.Output;
                            com.Parameters.AddWithValue("@RlnId", M_IdReklamacji);
                            com.Parameters.AddWithValue("@Ilosc", M_Ilosc);
                            com.Parameters.AddWithValue("@JmFormat", M_IloscMiejscPoPrzecinku);
                            com.Parameters.AddWithValue("@TwrTyp", 16);
                            com.Parameters.AddWithValue("@DokNumer", "");
                            com.Parameters.AddWithValue("@TwrNumer", M_TwrNumer);
                            com.Parameters.AddWithValue("@KntNumer", M_KntNumer);
                            com.Parameters.AddWithValue("@Zadanie", M_Zadanie);
                            com.Parameters.AddWithValue("@Przyczyna", Przyczyna ?? "");
                            com.Parameters.AddWithValue("@OpeNumer", DBNull.Value);
                            int pret = (com.ExecuteScalar() as int?) ?? 0;

                            ret.Rows.Add(new[] { pret == 0, Id.Value });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException($"b2b_DodajPozycjeReklamacji {ex.Message}");
                    ret.Rows.Add(new object[] { false, 0 });
                }
                Logger.LogDebug($"b2b_DodajPozycjeReklamacji {ret.GetJson()}");
                Logger.LogDebug("b2b_DodajPozycjeReklamacji STOP");
                return ret.GetJson();
            }
        }

        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_producenci()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_producenci()", "producenci");
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_FormyPlatnosci()
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - none";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                return DBHelper.RunSqlQuery("select Id,FormaPlatnosci from dbo.b2b_FormyPlatnosci", "T")?.GetJson();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_nowyDokumentZamJednorazowy(string xmlAdres, string xmlAdresW, string xmlOrder, bool Potwierdzony, string KodRabatowy)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - xmlAdres:{xmlAdres},xmlAdresW:{xmlAdresW},xmlOrder:{xmlOrder},potwierdzony:{Potwierdzony},kodrabatowy:{KodRabatowy}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    Logger.LogDebug("lockSem Start b2b_nowyDokumentZamJednorazowy");
                    Cdnsys th = new Cdnsys();
                    th.AttachThread();
                    //Logger.LogDebug("Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                    CDNOperations.CDNOperations cdn = StartCDNOperation("b2b_nowyDokumentZamJednorazowy");
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

                        docId = cdn.nowyDokumentZamJednorazowy(6, xmlAdres, xmlAdresW, xmlOrder, ref error, Potwierdzony ? 2 : 0, KodRabatowy);
                        if (string.IsNullOrEmpty(error))
                        {
                            numer = DBHelper.GetNumerDokumentuTRN(docId);
                        }
                    }
                    else
                    {
                        error = "Nie udalo się zainicjować sesji";
                    }
                    EndCDNOperation("b2b_nowyDokumentZamJednorazowy", cdn);

                    CDNResponse resp = new CDNResponse();
                    resp.Id = docId;
                    resp.Error = error;
                    resp.Numer = numer;
                    Logger.LogDebug("lockSem End b2b_nowyDokumentZam");
                    return ResponseSerializer.Serialize(resp);
                }
            }
        }

        #region CDNOperations
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
        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_PrzyjeciePlatnosci(int typ_dokumentu, int id_dokumentu, decimal kwota, string id_transakcji)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - typ_dokumentu:{typ_dokumentu},id_dokumentu:{id_dokumentu},kwota:{kwota},id_transakcji:{id_transakcji}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                lock (Locker.lockSem)
                {
                    var operacje = new CDNOperations.CDNOperations(SesjaId);
                    return operacje.PrzyjeciePlatnosci(typ_dokumentu, id_dokumentu, kwota, id_transakcji);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_KontoKlienta(int kntnumer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - kntnumer:{kntnumer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                return DBHelper.RunSqlProcParam("dbo.b2b_KontoKlienta", "t", "@kntnumer", kntnumer).GetJson();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_ValidateNip(string countryCode, string NIP)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - countryCode:{countryCode},NIP:{NIP}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                var resp = new Response();
                Logger.LogDebug($"web ValidateNip: {NIP} / {countryCode}");
                if (countryCode.ToUpper() == "PL" || countryCode.ToUpper() == "POL" || countryCode == "")
                {
                    if (NIP.Length <= 20 && NIP.Replace("-", "").Length == NIP.Count(c => Char.IsDigit(c)) && NIP.Count(c => Char.IsDigit(c)) == 10)
                    {
                        if (NIP.Replace("-", "").IsValidNIP()) { resp.ErrorId = 0; resp.ErrorMessage = ""; }
                        else { resp.ErrorId = 1; resp.ErrorMessage = "Błędna suma kontrolna NIP"; }
                    }
                    else
                    {
                        resp.ErrorId = 2; resp.ErrorMessage = "Nip powinien zawierać tylko cyfry i znaki '-', dodatkowo ilość cyfr powinna być równa 10";
                    }
                }
                else
                {
                    resp.ErrorId = 3; resp.ErrorMessage = "";
                }
                Logger.LogDebug($"web ValidateNip: {ResponseSerializer.Serialize(resp)}");
                return ResponseSerializer.Serialize(resp);
            }
        }
        #region b2b_Rentals
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_RentalsElem(int trnnumer, int kndnumer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - trnnumer:{trnnumer},kndnumer:{kndnumer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@trnnumer", trnnumer));
                lstPar.Add(new QueryParam("@kndnumer", kndnumer));
                DataTable dt = DBHelper.RunSqlProcParam("[dbo].[b2b_spRentalsElem]", "b2b_rentalselem", lstPar);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_Rentals(int gidnumer)
        {
            string message = "";
            if (LogUsage.Params) message = $"Parameters - gidnumer:{gidnumer}";
            using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
            {
                List<QueryParam> lstPar = new List<QueryParam>();
                lstPar.Add(new QueryParam("@gidnumer", gidnumer));
                DataTable dt = DBHelper.RunSqlProcParam("[dbo].[b2b_spRentals]", "b2b_rentals", lstPar);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
        }
        #endregion
    }
}