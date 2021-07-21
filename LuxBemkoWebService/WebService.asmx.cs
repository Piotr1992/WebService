using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace LuxBemkoWebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        #region b2b_towary
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_towary()
        {
            DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_lux_towary()", "b2b_towary");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }
        #endregion
        #region b2b_zamnag
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_zamnag(int gidnumer)
        {
            DataTable dt = DBHelper.RunSqlProcParam("dbo.b2b_lux_zamnag", "b2b_zamnag", "@gidnumer", gidnumer);
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }
        #endregion
        #region b2b_zamelem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_zamelem(int gidnumer, int kndnumer)
        {
            List<QueryParam> lstPar = new List<QueryParam>();
            lstPar.Add(new QueryParam("@gidnumer", gidnumer));
            lstPar.Add(new QueryParam("@kndnumer", kndnumer));
            DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_lux_zamelem](@gidnumer,@kndnumer)", "b2b_zamelem", lstPar);
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_nowyDokumentZam(string xml)
        {
            #region test
            //            xml=@"<?xml version=""1.0""?>
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
            CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations(SesjaId);
            int sesja = cdn.GetSesja();
            Logger.Logger.LogInfo("sesja=" + sesja.ToString());
            int docId = 0;
            string error = "";
            string numer = "";
            if (sesja != 0)
            {
                if (sesja != SesjaId)
                {
                    SesjaId = sesja;
                }

                docId = cdn.NowyDokumentZam(6, xml, ref error);
                if (string.IsNullOrEmpty(error))
                {
                    numer = DBHelper.GetNumerDokumentuTRN(docId);
                }
            }
            else
            {
                error = "Nie udalo się zainicjować sesji";
            }
            CDNResponse resp = new CDNResponse();
            resp.Id = docId;
            resp.Error = error;
            resp.Numer = numer;
            return ResponseSerializer.Serialize(resp);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_nowyDokumentZamOsoba(string xml)
        {
            CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations(SesjaId);
            int sesja = cdn.GetSesja();
            Logger.Logger.LogInfo("sesja=" + sesja.ToString());
            int docId = 0;
            string error = "";
            string numer = "";
            if (sesja != 0)
            {
                if (sesja != SesjaId)
                {
                    SesjaId = sesja;
                }

                docId = cdn.NowyDokumentZamOsoba(6, xml, ref error);
                if (string.IsNullOrEmpty(error))
                {
                    numer = DBHelper.GetNumerDokumentuTRN(docId);
                }
            }
            else
            {
                error = "Nie udalo się zainicjować sesji";
            }
            CDNResponse resp = new CDNResponse();
            resp.Id = docId;
            resp.Error = error;
            resp.Numer = numer;
            return ResponseSerializer.Serialize(resp);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_nowyAdres(string xml)
        {
            CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations(SesjaId);
            int sesja = cdn.GetSesja();
            Logger.Logger.LogInfo("sesja=" + sesja.ToString());
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
            CDNResponse resp = new CDNResponse();
            resp.Id = docId;
            resp.Error = error;
            resp.Numer = numer;
            return ResponseSerializer.Serialize(resp);
        }
        #endregion
        #region b2b_stany
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string b2b_stany(int Id)
        {
            DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_lux_stany](@Id)", "b2b_stany", "@Id", Id);
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
        public string b2b_CenySpecjalne()
        {
            DataTable dt = DBHelper.RunSqlProc("[dbo].[dbo_b2b_lux_CenySpecjalne]", "b2b_CenySpecjalne");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }

        #endregion
        //#region b2b_operatorzy
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_operatorzy()
        //{
        //    DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_lux_operatorzy()", "b2b_operatorzy");
        //    if (dt != null)
        //    {
        //        return DataTableHelper.GetJson(dt);
        //    }
        //    return null;
        //}
        //#endregion
        #region hiddenMetods
        //#region b2b_kontrahenci
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_kontrahenci()
        //{
        //    DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_kontrahenci()", "b2b_kontrahenci");
        //    if (dt != null)
        //    {
        //        return DataTableHelper.GetJson(dt);
        //    }
        //    return null;
        //}

        //#endregion
        //#region b2b_danebinarne
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_danebinarne()
        //{
        //    DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_lista_danebinarne()", "b2b_danebinarne");
        //    if (dt != null)
        //    {
        //        return DataTableHelper.GetJson(dt);
        //    }
        //    return null;
        //}

        //#endregion

        //#region b2b_dystrybutorzy
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_dystrybutorzy()
        //{
        //    DataTable dt = DBHelper.RunSqlQuery("select * from dbo.b2b_dystrybutorzy ()", "b2b_dystrybutorzy");
        //    if (dt != null)
        //    {
        //        return DataTableHelper.GetJson(dt);
        //    }
        //    return null;
        //}


        //#endregion
        //#region b2b_cechy
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_cechy(int twrId)
        //{
        //    DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_cechy](@twrid)", "b2b_stany", "@twrid", twrId);
        //    if (dt != null)
        //    {
        //        return DataTableHelper.GetJson(dt);
        //    }
        //    return null;
        //}

        //#endregion


        //#region b2b_traelem
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_traelem(int trnnumer, int kndnumer)
        //{
        //    List<QueryParam> lstPar = new List<QueryParam>();
        //    lstPar.Add(new QueryParam("@trnnumer", trnnumer));
        //    lstPar.Add(new QueryParam("@kndnumer", kndnumer));
        //    DataTable dt = DBHelper.RunSqlProcParam("[dbo].[b2b_traelem]", "b2b_traelem", lstPar);
        //    if (dt != null)
        //    {
        //        return DataTableHelper.GetJson(dt);
        //    }
        //    return null;
        //}
        //#endregion
        //#region b2b_tranag
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_tranag(int gidnumer)
        //{
        //    List<QueryParam> lstPar = new List<QueryParam>();
        //    lstPar.Add(new QueryParam("@gidnumer", gidnumer));
        //    DataTable dt = DBHelper.RunSqlProcParam("[dbo].[b2b_tranag]", "b2b_tranag", lstPar);
        //    if (dt != null)
        //    {
        //        return DataTableHelper.GetJson(dt);
        //    }
        //    return null;
        //}
        //#endregion
        //#region b2b_ceny
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_ceny(int Id)
        //{
        //    DataTable dt = DBHelper.RunSqlQueryParam("select * from [dbo].[b2b_ceny](@Id)", "b2b_stany", "@Id", Id);
        //    if (dt != null)
        //    {
        //        return DataTableHelper.GetJson(dt);
        //    }
        //    return null;
        //}

        //#endregion
        //#region b2b_twrzamienniki
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_twrzamienniki()
        //{
        //    List<QueryParam> lstPar = new List<QueryParam>();
        //    DataTable dt = DBHelper.RunSqlProcParam("[dbo].[b2b_twrzamienniki]", "b2b_twrzamienniki", lstPar);
        //    if (dt != null)
        //    {
        //        return DataTableHelper.GetJson(dt);
        //    }
        //    return null;
        //}
        //#endregion
        #endregion

        //#region VerifyUser
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_verifyUser(string name, string password)
        //{

        //    //string str=Coder.Encrypt("CaLKC?Gz");
        //    return DBHelper.VerifyUser(name, password);

        //}
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string b2b_setNewUserPassword(string name)
        //{
        //    return DBHelper.SetNewUserPassword(name);
        //}
        //#endregion

    }
}