using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using cdn_api;
using System.Data.SqlClient;
using System.IO;
using Helpers;
using Helpers.Logger;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data;
using System.Xml.Linq;
using System.Globalization;
using System.Reflection;

namespace CDNOperations
{
    public class CDNOperations
    {
        [DllImport("ClaRUN.dll")]
        public static extern void AttachThreadToClarion(int flag);
        int wersjaAPI;
        int tryb = 1;
        int winieta = -1;
        string plikLog;
        string programID;
        string serwerKlucza;
        string opeIdent;
        string opeHaslo;
        string baza;
        string connString;
        int gidGrupaKnt;
        int gidFirma;
        int? trybZamknieciaZam;
        int sesja = -1;
        int sesjaTmp = -1;
        int trybWsadowy = 1;
        public static Object sem = new Object();
        #region Creator
        public CDNOperations()
        {
            AttachThreadToClarion(1);
            wersjaAPI = Convert.ToInt32(ConfigurationManager.AppSettings["wersjaAPI"]);
            tryb = Convert.ToInt32(ConfigurationManager.AppSettings["Tryb"]);
            winieta = Convert.ToInt32(ConfigurationManager.AppSettings["Winieta"]);
            plikLog = Convert.ToString(ConfigurationManager.AppSettings["PlikLog"]);
            programID = Convert.ToString(ConfigurationManager.AppSettings["ProgramID"]);
            serwerKlucza = Convert.ToString(ConfigurationManager.AppSettings["SerwerKlucza"]);
            opeIdent = Convert.ToString(ConfigurationManager.AppSettings["OpeIdent"]);
            opeHaslo = Convert.ToString(ConfigurationManager.AppSettings["OpeHaslo"]);
            baza = Convert.ToString(ConfigurationManager.AppSettings["BazaCDNXL"]);
            connString = Convert.ToString(ConfigurationManager.AppSettings["ConnString"]);
            trybWsadowy = Convert.ToInt32(ConfigurationManager.AppSettings["TrybWsadowy"]);
            if (ConfigurationManager.AppSettings["TrybZamknieciaZam"]!=null){
                trybZamknieciaZam = Convert.ToInt32(ConfigurationManager.AppSettings["TrybZamknieciaZam"]);
            } else {trybZamknieciaZam = null;}
            if (ConfigurationManager.AppSettings["gidGrupaKnt"]!=null){
                gidGrupaKnt = Convert.ToInt32(ConfigurationManager.AppSettings["gidGrupaKnt"]);
            } else {gidGrupaKnt = 0; }
            if (ConfigurationManager.AppSettings["gidFirma"]!=null){
                gidFirma = Convert.ToInt32(ConfigurationManager.AppSettings["gidFirma"]);
            } else {gidFirma = 0;}
        }
        public CDNOperations(int sesja)
        {
            AttachThreadToClarion(1);
            wersjaAPI = Convert.ToInt32(ConfigurationManager.AppSettings["wersjaAPI"]);
            tryb = Convert.ToInt32(ConfigurationManager.AppSettings["Tryb"]);
            winieta = Convert.ToInt32(ConfigurationManager.AppSettings["Winieta"]);
            plikLog = Convert.ToString(ConfigurationManager.AppSettings["PlikLog"]);
            programID = Convert.ToString(ConfigurationManager.AppSettings["ProgramID"]);
            serwerKlucza = Convert.ToString(ConfigurationManager.AppSettings["SerwerKlucza"]);
            opeIdent = Convert.ToString(ConfigurationManager.AppSettings["OpeIdent"]);
            opeHaslo = Convert.ToString(ConfigurationManager.AppSettings["OpeHaslo"]);
            baza = Convert.ToString(ConfigurationManager.AppSettings["BazaCDNXL"]);
            connString = Convert.ToString(ConfigurationManager.AppSettings["ConnString"]);
            trybWsadowy = Convert.ToInt32(ConfigurationManager.AppSettings["TrybWsadowy"]);
            if (ConfigurationManager.AppSettings["TrybZamknieciaZam"]!=null){
                trybZamknieciaZam = Convert.ToInt32(ConfigurationManager.AppSettings["TrybZamknieciaZam"]);
            } else {trybZamknieciaZam = null;}
            if (ConfigurationManager.AppSettings["gidGrupaKnt"]!=null){
                gidGrupaKnt = Convert.ToInt32(ConfigurationManager.AppSettings["gidGrupaKnt"]);
            } else {gidGrupaKnt = 0; }
            if (ConfigurationManager.AppSettings["gidFirma"]!=null){
                gidFirma = Convert.ToInt32(ConfigurationManager.AppSettings["gidFirma"]);
            } else {gidFirma = 0;}
            SprawdzSesje(sesja);
        }
        #endregion

        #region LoginLogout
        public int GetSesja()
        {
            return sesja;
        }
        public bool SprawdzSesje(int sesjaTmp)
        {
            string error = string.Empty;
            try
            {
                
                Logger.LogInfo("SprawdzSesje");
                int aktywna = 0;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    Logger.LogInfo("SprawdzSesje sesjaTmp:" + sesjaTmp);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "select ses_aktywna,ses_stop from cdn.sesje with (nolock) where SES_SesjaID = " + sesjaTmp.ToString();
                    cmd.Connection = conn;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    int ses_aktywna = 1;
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        aktywna = rdr.GetInt32(1);
                        ses_aktywna = rdr.GetInt32(0);
                        Logger.LogDebug(string.Format("SprawdzSesje rdr aktywna = {0}, ses_aktywna: {1}", aktywna.ToString(), ses_aktywna));
                    }
                    else
                    {
                        aktywna = 1;
                    }
                    rdr.Close();
                    Logger.LogDebug(string.Format("SprawdzSesje aktywna = {0}, sesja: {1}", aktywna.ToString(), sesja));
                    if (aktywna != 0)
                    {
                        if (aktywna > 1 && ses_aktywna == 0)
                        {
                            Logger.LogDebug(string.Format("SprawdzSesje Logout aktywna = {0}, sesja: {1}", aktywna.ToString(), sesja));
                            Logout(sesjaTmp);
                        }
                        int resu;
                        Logger.LogDebug("SprawdzSesje sesja przed Login() = " + sesja);

                        if (ses_aktywna == 2)
                        {
                            Logger.LogDebug("update cdn.sesje set ses_aktywna=1,ses_stop = 0 where SES_SesjaID = " + sesjaTmp.ToString());
                            cmd = new SqlCommand();

                            cmd.CommandText = "update cdn.sesje set ses_aktywna=1,ses_stop = 0 where SES_SesjaID = " + sesjaTmp.ToString();
                            cmd.Connection = conn;

                            cmd.ExecuteNonQuery();
                        }
                        resu = Login(ref error);
                        Logger.LogDebug(string.Format("SprawdzSesje sesja po Login() = {0}, status: {1}", sesja, resu.ToString()));
                        if (resu > 0)
                        {
                            if (sesja == sesjaTmp)
                            {
                                Logger.LogInfo("sesja == sesjaTmp");
                                cmd = new SqlCommand();

                                cmd.CommandText = "update cdn.sesje set ses_aktywna=1 where SES_SesjaID = " + sesja.ToString();
                                cmd.Connection = conn;
                                Logger.LogInfo("Aktualizacja sesji");
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        sesja = sesjaTmp;
                        Logger.LogInfo("sesja = sesjaTmp");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return false;
            }

            Logger.LogDebug(string.Format("SprawdzSesje return sesja: {0}, sesjaTmp: {1}", sesja, sesjaTmp));
            return true;
        }
        public int Login(ref string error)
        {
            int result = 0;
            try
            {
                XLLoginInfo_20193 login = new XLLoginInfo_20193();
                login.Wersja = wersjaAPI;
                login.TrybWsadowy = trybWsadowy;
                login.Winieta = winieta;
                // login.PlikLog = plikLog; //@"C:\cdnxl.log";
                login.ProgramID = programID;
                login.SerwerKlucza = serwerKlucza;
                login.OpeIdent = opeIdent;
                login.OpeHaslo = opeHaslo;
                login.Baza = baza;
                login.UtworzWlasnaSesje = 0;
                Logger.LogDebug(string.Format(@"Wersja: {0}, TrybWsadowy: {1}, Winieta: {2}, ProgramID: {3}, SerwerKlucza: {4}, OpeIdent: {5},  Baza: {6}", login.Wersja, login.TrybWsadowy, login.Winieta, login.ProgramID, login.SerwerKlucza, login.OpeIdent,  login.Baza));


                lock (sem)
                {
                    result = cdn_api.cdn_api.XLLogin(login, ref sesja);
                }
                Logger.LogDebug(string.Format(@"Login result: {0}", result));

                if (result != 0)
                {
                    error = "";
                    CDNKomunikaty.Komunikaty_XLLogin(result, ref error);
                    Logger.LogInfo(error);

                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error += ex.ToString();
            }
            return result;
        }

        public int Logout(int sesja)
        {
            int result = 0;
            if (sesja > 0)
            {
                try
                {
                    lock (sem)
                    {
                        result = cdn_api.cdn_api.XLLogout(sesja);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    return -999;
                }
                sesja = 0;
            }
            return result;
        }

        #endregion

        #region Zamowienia
        public int NowyDokumentZam(int typ, string documentXml, ref string error, int TrybZamkniecia = 0,string KodRabatowy = null)
        {
            documentXml = documentXml.Replace("_20163", "_20193");
            Logger.LogDebug(string.Format("tryb zamkniecia: {0}, config: {1}",TrybZamkniecia.ToString(),trybZamknieciaZam.ToString()));
            if (trybZamknieciaZam!=null) {TrybZamkniecia=(int)trybZamknieciaZam;}
            int IDDokumentuZam = 0;
            error = "";
            try
            {
                int result = 0;
                int ZamId = 0;
                Logger.LogDebug("Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                XLDokumentZamNagInfo_20193 dokumentZamNagInfo = new XLDokumentZamNagInfo_20193();
                dokumentZamNagInfo = (XLDokumentZamNagInfo_20193)CDNSerializerDeserializer.Deserialize(documentXml, dokumentZamNagInfo);
                dokumentZamNagInfo.Wersja = wersjaAPI;
                dokumentZamNagInfo.Tryb = tryb;
                dokumentZamNagInfo.Typ = typ;
                dokumentZamNagInfo.KntTyp = 32;
                dokumentZamNagInfo.ZamSeria = DBHelper.GetSeriaMagazynu(dokumentZamNagInfo.MagNumer);
                dokumentZamNagInfo.Cecha = "B2B";
                dokumentZamNagInfo.RezerwujZasoby = -1; //zgodnie z definicja dokumentu
                dokumentZamNagInfo.RezerwacjeNaNiepotwierdzonym = 0; //zgodnie z definicja dokumentu

                Logger.LogDebug(string.Format("formapl:{0}, platnik:{1}, platniktyp:{2}",dokumentZamNagInfo.FormaPl,dokumentZamNagInfo.KnPNumer,dokumentZamNagInfo.KnPTyp));
                if (DBHelper.GetPayerNumberFromPaymentForm(dokumentZamNagInfo.FormaPl) != 0)
                {
                    dokumentZamNagInfo.KnPNumer = DBHelper.GetPayerNumberFromPaymentForm(dokumentZamNagInfo.FormaPl);
                    dokumentZamNagInfo.KnPTyp = 32;
                }
                Logger.LogDebug(string.Format("formapl:{0}, platnik:{1}, platniktyp:{2}",dokumentZamNagInfo.FormaPl,dokumentZamNagInfo.KnPNumer,dokumentZamNagInfo.KnPTyp));

                //dokumentZamNagInfo.SposobDst

                lock (sem)
                {
                    result = cdn_api.cdn_api.XLNowyDokumentZam(sesja, ref ZamId, dokumentZamNagInfo);
                    Logger.LogDebug(string.Format("Nowy dokument zam kontrahent: {0} typ:{3} status: {1} sesja: {2}", dokumentZamNagInfo.KntNumer, result.ToString(), sesja.ToString(),dokumentZamNagInfo.Typ));
                    if (result == 0)
                    {
                        DodajKodRabatowyDoZamowienia(KodRabatowy, dokumentZamNagInfo);
                    }

                }
                if (result != 0 || ZamId <= 0)
                {
                    CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                    Logger.LogInfo(string.Format("Dodaj dokument zam: {0}, xml: {1}" ,error ,documentXml));
                }
                else
                {
                    IDDokumentuZam = dokumentZamNagInfo.GIDNumer;
                    DodajPozycjeZam(documentXml, ref error, ZamId);
                    if (string.IsNullOrEmpty(error))
                    {
                        result = ZamknijDokumentZam(ref error, ZamId, TrybZamkniecia);
                        if (result != 0 || IDDokumentuZam <= 0)
                        {
                            CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                            Logger.LogInfo("Zamknij dokument zam: " + error + documentXml);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error = ex.Message;
            }
            return IDDokumentuZam;

        }
        public int CasNowyDokumentZam(int typ, string documentXml, ref string error, int TrybZamkniecia = 0, string KodRabatowy = null)
        {
            documentXml = documentXml.Replace("_20163", "_20193");
            Logger.LogDebug(string.Format("tryb zamkniecia: {0}, config: {1}", TrybZamkniecia.ToString(), trybZamknieciaZam.ToString()));
            if (trybZamknieciaZam != null) { TrybZamkniecia = (int)trybZamknieciaZam; }
            int IDDokumentuZam = 0;
            error = "";
            try
            {
                int result = 0;
                int ZamId = 0;
                Logger.LogDebug("Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                XLDokumentZamNagInfo_20193 dokumentZamNagInfo = new XLDokumentZamNagInfo_20193();
                dokumentZamNagInfo = (XLDokumentZamNagInfo_20193)CDNSerializerDeserializer.Deserialize(documentXml, dokumentZamNagInfo);
                dokumentZamNagInfo.Wersja = wersjaAPI;
                dokumentZamNagInfo.Tryb = tryb;
                dokumentZamNagInfo.Typ = typ;
                dokumentZamNagInfo.KntTyp = 32;
                dokumentZamNagInfo.Cecha = "B2B";
                dokumentZamNagInfo.RezerwujZasoby = -1; //zgodnie z definicja dokumentu
                dokumentZamNagInfo.RezerwacjeNaNiepotwierdzonym = 0; //zgodnie z definicja dokumentu

                Logger.LogDebug(string.Format("formapl:{0}, platnik:{1}, platniktyp:{2}", dokumentZamNagInfo.FormaPl, dokumentZamNagInfo.KnPNumer, dokumentZamNagInfo.KnPTyp));
                Logger.LogDebug(string.Format("formapl:{0}, platnik:{1}, platniktyp:{2}", dokumentZamNagInfo.FormaPl, dokumentZamNagInfo.KnPNumer, dokumentZamNagInfo.KnPTyp));

                lock (sem)
                {
                    result = cdn_api.cdn_api.XLNowyDokumentZam(sesja, ref ZamId, dokumentZamNagInfo);
                    Logger.LogDebug(string.Format("Nowy dokument zam kontrahent: {0} typ:{3} status: {1} sesja: {2}", dokumentZamNagInfo.KntNumer, result.ToString(), sesja.ToString(), dokumentZamNagInfo.Typ));
                }
                if (result != 0 || ZamId <= 0)
                {
                    CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                    Logger.LogInfo(string.Format("Dodaj dokument zam: {0}, xml: {1}", error, documentXml));
                }
                else
                {
                    IDDokumentuZam = dokumentZamNagInfo.GIDNumer;
                    DodajPozycjeZam(documentXml, ref error, ZamId);
                    if (string.IsNullOrEmpty(error))
                    {
                        result = ZamknijDokumentZam(ref error, ZamId, TrybZamkniecia);
                        if (result != 0 || IDDokumentuZam <= 0)
                        {
                            CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                            Logger.LogInfo("Zamknij dokument zam: " + error + documentXml);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error = ex.Message;
            }
            return IDDokumentuZam;

        }
        public int NowyDokumentZamOsoba(int typ, string documentXml, ref string error, int TrybZamkniecia = 0,string KodRabatowy = null)
        {
            documentXml=documentXml.Replace("_20163", "_20193");
            Logger.LogDebug(string.Format("tryb zamkniecia: {0}, config: {1}",TrybZamkniecia.ToString(),trybZamknieciaZam.ToString()));
            if (trybZamknieciaZam!=null) {TrybZamkniecia=(int)trybZamknieciaZam;}
            int IDDokumentuZam = 0;
            error = "";
            try
            {
                int result = 0;
                int ZamId = 0;
                Logger.LogDebug("Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                XLDokumentZamNagInfo_20193 dokumentZamNagInfo = new XLDokumentZamNagInfo_20193();
                XDocument d = XDocument.Parse(documentXml);
                var KnsTyp = d.Root.Element(dokumentZamNagInfo.GetType().Name).Element("KnsTyp").Value;
                var KnsFirma = d.Root.Element(dokumentZamNagInfo.GetType().Name).Element("KnsFirma").Value;
                var KnsNumer = d.Root.Element(dokumentZamNagInfo.GetType().Name).Element("KnsNumer").Value;
                var KnsLp = d.Root.Element(dokumentZamNagInfo.GetType().Name).Element("KnsLp").Value;
                d.Root.Element(dokumentZamNagInfo.GetType().Name).Element("KnsTyp").Remove();
                d.Root.Element(dokumentZamNagInfo.GetType().Name).Element("KnsFirma").Remove();
                d.Root.Element(dokumentZamNagInfo.GetType().Name).Element("KnsNumer").Remove();
                d.Root.Element(dokumentZamNagInfo.GetType().Name).Element("KnsLp").Remove();
                
                dokumentZamNagInfo = (XLDokumentZamNagInfo_20193)CDNSerializerDeserializer.Deserialize(documentXml, dokumentZamNagInfo);
                dokumentZamNagInfo.Wersja = wersjaAPI;
                dokumentZamNagInfo.Tryb = tryb;
                dokumentZamNagInfo.Typ = typ;
                dokumentZamNagInfo.KntTyp = 32;
                dokumentZamNagInfo.ZamSeria = DBHelper.GetSeriaMagazynu(dokumentZamNagInfo.MagNumer);
                dokumentZamNagInfo.Cecha = "B2B";
                dokumentZamNagInfo.RezerwujZasoby = -1; //zgodnie z definicja dokumentu
                dokumentZamNagInfo.RezerwacjeNaNiepotwierdzonym = 0; //zgodnie z definicja dokumentu
                
                Logger.LogDebug(string.Format("formapl:{0}, platnik:{1}, platniktyp:{2}",dokumentZamNagInfo.FormaPl,dokumentZamNagInfo.KnPNumer,dokumentZamNagInfo.KnPTyp));
                if (DBHelper.GetPayerNumberFromPaymentForm(dokumentZamNagInfo.FormaPl) != 0)
                {
                    dokumentZamNagInfo.KnPNumer = DBHelper.GetPayerNumberFromPaymentForm(dokumentZamNagInfo.FormaPl);
                    dokumentZamNagInfo.KnPTyp = 32;
                }
                Logger.LogDebug(string.Format("formapl:{0}, platnik:{1}, platniktyp:{2}",dokumentZamNagInfo.FormaPl,dokumentZamNagInfo.KnPNumer,dokumentZamNagInfo.KnPTyp));

                lock (sem)
                {
                    result = cdn_api.cdn_api.XLNowyDokumentZam(sesja, ref ZamId, dokumentZamNagInfo);
                    if (result == 0)
                    {
                        using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                        {
                            con.Open();
                            using (var com = con.CreateCommand())
                            {
                                com.CommandType = CommandType.Text;
                                com.CommandText = $"update [CDN].[ZamNag] set [ZaN_KnSTyp] = {KnsTyp},[ZaN_KnSFirma] = {KnsFirma},[ZaN_KnSNumer] = {KnsNumer},[ZaN_KnSLp] = {KnsLp} where ZaN_GIDNumer = {dokumentZamNagInfo.GIDNumer}";
                                com.ExecuteNonQuery();
                            }
                        }
                    }

                    Logger.LogDebug(string.Format("Nowy dokument zam kontrahent: {0} typ:{3} status: {1} sesja: {2}", dokumentZamNagInfo.KntNumer, result.ToString(), sesja.ToString(), dokumentZamNagInfo.Typ));
                }
                if (result != 0 || ZamId <= 0)
                {
                    CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                    Logger.LogInfo(string.Format("Dodaj dokument zam: {0}, xml: {1}", error, documentXml));
                }
                else
                {
                    IDDokumentuZam = dokumentZamNagInfo.GIDNumer;
                    DodajPozycjeZam(documentXml, ref error, ZamId);
                    if (string.IsNullOrEmpty(error))
                    {
                        result = ZamknijDokumentZam(ref error, ZamId, TrybZamkniecia);
                        if (result != 0 || IDDokumentuZam <= 0)
                        {
                            CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                            Logger.LogInfo("Zamknij dokument zam: " + error + documentXml);
                        }
                        DodajKodRabatowyDoZamowienia(KodRabatowy, dokumentZamNagInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error = ex.Message;
            }
            return IDDokumentuZam;

        }

        private void DodajKodRabatowyDoZamowienia(string KodRabatowy, XLDokumentZamNagInfo_20193 dokumentZamNagInfo)
        {
            if (!string.IsNullOrEmpty(KodRabatowy))
            {
                var atrR = cdn_api.cdn_api.XLDodajAtrybut(sesja, new XLAtrybutInfo_20193()
                {
                    Wersja = wersjaAPI,                    
                    Klasa= "B2B_KodRabatowy",
                    GIDFirma = dokumentZamNagInfo.GIDFirma,
                    GIDLp = dokumentZamNagInfo.GIDLp,
                    GIDNumer = dokumentZamNagInfo.GIDNumer,
                    GIDTyp = dokumentZamNagInfo.GIDTyp,
                    Wartosc = KodRabatowy,
                    ZamTyp = 1280
                });

                if (atrR != 0)
                {
                    Logger.LogInfo(string.Format("Dodaj dokument zam atrybut: {0}, xml: {1}", XLError.XLDodajAtrybut(atrR), KodRabatowy));
                }
            }
        }

        private void DodajPozycjeZam(string documentXml, ref string error, int IDDokumentuZam)
        {
            documentXml=documentXml.Replace("_20163", "_20193");
            try
            {
                XLDokumentZamElemInfo_20193 elem = new XLDokumentZamElemInfo_20193();
                List<XLDokumentZamElemInfo_20193> lst = CDNSerializerDeserializer.DeserializeList(documentXml, elem).Cast<XLDokumentZamElemInfo_20193>().ToList();
                foreach (XLDokumentZamElemInfo_20193 dokumentZamElemInfo in lst)
                {
                    int result = 0;
                    dokumentZamElemInfo.Wersja = wersjaAPI;
                    dokumentZamElemInfo.TwrTyp = 16;
                    lock (sem)
                    {
                        result = cdn_api.cdn_api.XLDodajPozycjeZam(IDDokumentuZam, dokumentZamElemInfo);
                        Logger.LogDebug("Dodaj pozycje: " + dokumentZamElemInfo.TwrNumer.ToString());
                    }
                    if (result != 0)
                    {
                        string komunikat = "";
                        CDNKomunikaty.Komunikaty_GlowneFunkcje(42, result, ref komunikat, wersjaAPI);
                        Logger.LogDebug("Dodaj pozycje: " + komunikat + documentXml);
                        error += komunikat;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException("Dodaj pozycje ex: " + ex);
                error += ex.Message;
            }
        }
        private int ZamknijDokumentZam(ref string error, int IDDokumentuZam, int TrybZamkniecia = 0)
        {
            int result = 0;
            try
            {

                XLZamkniecieDokumentuZamInfo_20193 zamkniecieDokumentuZamInfo = new XLZamkniecieDokumentuZamInfo_20193();
                zamkniecieDokumentuZamInfo.Wersja = wersjaAPI;
                if (TrybZamkniecia != 0)
                {
                    zamkniecieDokumentuZamInfo.TrybZamkniecia = TrybZamkniecia;
                }
                lock (sem)
                {
                    result = cdn_api.cdn_api.XLZamknijDokumentZam(IDDokumentuZam, zamkniecieDokumentuZamInfo);
                    Logger.LogDebug(string.Format("Zamkniecie dokumentu nr: {0} status: {1}", IDDokumentuZam.ToString(), result));
                }

            }
            catch (Exception ex)
            {
                Logger.LogException("Zamknij dokument ex: " + ex);
                error += ex.Message;
            }
            return result;
        }
        #endregion

        #region Handlowe
        public int NowyDokument(string documentXml, ref int gidtyp, ref string error)
        {
            documentXml=documentXml.Replace("_20163", "_20193");
            int IDDokumentu = 0;
            error = "";
            try
            {
                int result = 0;
                int DokId = 0;

                XLDokumentNagInfo_20193 dokumentNagInfo = new XLDokumentNagInfo_20193();
                dokumentNagInfo = (XLDokumentNagInfo_20193)CDNSerializerDeserializer.Deserialize(documentXml, dokumentNagInfo);
                dokumentNagInfo.Wersja = wersjaAPI;
                dokumentNagInfo.Tryb = tryb;
                dokumentNagInfo.KntTyp = 32;
                dokumentNagInfo.Cecha = "B2B";
                lock (sem)
                {
                    result = cdn_api.cdn_api.XLNowyDokument(sesja, ref DokId, dokumentNagInfo);
                    gidtyp = dokumentNagInfo.GIDTyp;
                    Logger.LogDebug(string.Format("Nowy dokument kontrahent: {0} status: {1} sesja: {2}", dokumentNagInfo.KntNumer, result.ToString(), sesja.ToString()));
                }
                if (result != 0 || DokId <= 0)
                {
                    CDNKomunikaty.Komunikaty_GlowneFunkcje(1, result, ref error, wersjaAPI);
                    Logger.LogInfo("Dodaj dokument: " + error + documentXml);
                }
                else
                {
                    IDDokumentu = dokumentNagInfo.GIDNumer;
                    DodajPozycje(documentXml, ref error, DokId);
                    if (string.IsNullOrEmpty(error))
                    {
                        result = ZamknijDokument(ref error, DokId);
                        if (result != 0 || IDDokumentu <= 0)
                        {
                            CDNKomunikaty.Komunikaty_GlowneFunkcje(7, result, ref error, wersjaAPI);
                            Logger.LogInfo("Zamknij dokument: " + error + documentXml);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error = ex.Message;
            }
            return IDDokumentu;

        }
        private void DodajPozycje(string documentXml, ref string error, int IDDokumentu)
        {
            documentXml=documentXml.Replace("_20163", "_20193");
            try
            {
                int lp = 1;
                XLDokumentElemInfo_20193 elem = new XLDokumentElemInfo_20193();
                List<XLDokumentElemInfo_20193> lst = CDNSerializerDeserializer.DeserializeList(documentXml, elem).Cast<XLDokumentElemInfo_20193>().ToList();
                foreach (XLDokumentElemInfo_20193 dokumentElemInfo in lst)
                {
                    int result = 0;
                    dokumentElemInfo.Wersja = wersjaAPI;
                    dokumentElemInfo.TwrTyp = 16;
                    dokumentElemInfo.ZamLp = lp;
                    lock (sem)
                    {
                        result = cdn_api.cdn_api.XLDodajPozycje(IDDokumentu, dokumentElemInfo);
                        Logger.LogDebug("Dodaj pozycje: " + dokumentElemInfo.TwrNumer.ToString());
                        lp = lp + 1;
                    }
                    if (result != 0)
                    {
                        string komunikat = "";
                        CDNKomunikaty.Komunikaty_GlowneFunkcje(2, result, ref komunikat, wersjaAPI);
                        Logger.LogDebug("Dodaj pozycje: " + komunikat + documentXml);
                        error += komunikat;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException("Dodaj pozycje ex: " + ex);
                error += ex.Message;
            }
        }
        private int ZamknijDokument(ref string error, int IDDokumentu)
        {
            int result = 0;
            try
            {

                XLZamkniecieDokumentuInfo_20193 zamkniecieDokumentuZamInfo = new XLZamkniecieDokumentuInfo_20193();
                zamkniecieDokumentuZamInfo.Wersja = wersjaAPI;
                lock (sem)
                {
                    result = cdn_api.cdn_api.XLZamknijDokument(IDDokumentu, zamkniecieDokumentuZamInfo);
                    Logger.LogDebug(string.Format("Zamkniecie dokumentu nr: {0} status: {1}", IDDokumentu.ToString(), result));
                }

            }
            catch (Exception ex)
            {
                Logger.LogException("Zamknij dokument ex: " + ex);
                error += ex.Message;
            }
            return result;
        }
        #endregion
        #region Wydruki
        public int PobierzWydruk(ref string error, List<string> lstParams, List<string> lstValues, string printName, ref byte[] fileBytes)
        {
            int result = 0;
            try
            {

                if (PrintsConfig.Conf.prints.Where(p => p.nazwa == printName).Any())
                {
                    Logger.LogDebug("Znaleziono wydruk:" + printName);
                    string guid = Guid.NewGuid().ToString();
                    string fileName = guid + ".pdf";
                    string filePath = Path.Combine(ConfigurationManager.AppSettings["TempDir"], fileName);
                    Print prt = PrintsConfig.Conf.prints.Where(p => p.nazwa == printName).First();
                    XLWydrukInfo_20193 wydrukInfo = new XLWydrukInfo_20193();
                    wydrukInfo.Wersja = wersjaAPI;
                    wydrukInfo.Zrodlo = Convert.ToInt32(prt.zrodlo);
                    wydrukInfo.Wydruk = Convert.ToInt32(prt.wydruk);
                    wydrukInfo.Format = Convert.ToInt32(prt.format);
                    wydrukInfo.FiltrSQL = BuildFiltrSql(lstParams, lstValues);
                    wydrukInfo.DrukujDoPliku = 1;
                    wydrukInfo.Urzadzenie = 3;
                    wydrukInfo.PlikDocelowy = filePath;
                    Logger.LogDebug($"Przygotowano XLWydruk {wydrukInfo.FiltrSQL}");
                    lock (sem)
                    {
                        result = cdn_api.cdn_api.XLWykonajPodanyWydruk(wydrukInfo);
                    }
                    Logger.LogDebug("Wynik XLWyjkonajPodanyWydruk:" + result);
                    if (result == 0)
                    {
                        if (File.Exists(filePath))
                        {
                            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["TempDir"]);

                            fileBytes = File.ReadAllBytes(filePath);
                            foreach (string file in files)
                            {
                                if (file.Contains(guid))
                                {
                                    File.Delete(file);
                                }
                            }
                        }
                        else
                        {
                            Logger.LogDebug("Nie znaleziono pliku:" + filePath);
                        }
                    }
                    else
                    {
                        Logger.LogException("Wystąpił błąd:" + result);
                    }


                }

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error += ex.Message;
            }
            return result;
        }

        private string BuildFiltrSql(List<string> lstParams, List<string> lstValues)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lstValues.Count; i++)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" and ");
                }
                sb.Append(lstParams[i] + "=" + lstValues[i]);
            }
            return "(" + sb.ToString() + ")";
        }
        #endregion
        #region Kontraheci
        public int NowyAdres(string documentXml, ref string error)
        {
            documentXml=documentXml.Replace("_20163", "_20193");
            documentXml = documentXml.Replace("<Nazwa1>", "<Nazwa1><![CDATA[");
            documentXml = documentXml.Replace("<Nazwa2>", "<Nazwa2><![CDATA[");
            documentXml = documentXml.Replace("<Nazwa3>", "<Nazwa3><![CDATA[");
            documentXml = documentXml.Replace("</Nazwa1>", "]]></Nazwa1>");
            documentXml = documentXml.Replace("</Nazwa2>", "]]></Nazwa2>");
            documentXml = documentXml.Replace("</Nazwa3>", "]]></Nazwa3>");
            documentXml = documentXml.Replace("<![CDATA[<![CDATA[", "<![CDATA[");
            documentXml = documentXml.Replace("]]>]]>", "]]>");
            Logger.LogDebug($"Cdn_NowyAdres: documentXml {documentXml}");
            int IDAdresu = 0;
            error = "";
            try
            {
                int result = 0;
                int AdrId = 0;
                XLAdresInfo_20193 adresInfo = new XLAdresInfo_20193();
                adresInfo = (XLAdresInfo_20193)CDNSerializerDeserializer.Deserialize(documentXml, adresInfo);
                adresInfo.Wersja = wersjaAPI;
                adresInfo.Tryb = tryb;
                adresInfo.KntTyp = 32;
                var rep = false;
                if (adresInfo.Adres.Trim()=="" || adresInfo.Adres==null) {adresInfo.Adres=".pusta."; rep=true; }
                if (adresInfo.Nazwa2.Trim()=="" || adresInfo.Nazwa2==null) {adresInfo.Nazwa2=".pusta."; rep=true; }
                if (adresInfo.Nazwa3.Trim()=="" || adresInfo.Nazwa3==null) {adresInfo.Nazwa3=".pusta."; rep=true; }
                lock (sem)
                {
                    result = cdn_api.cdn_api.XLNowyAdres(sesja, ref AdrId, adresInfo);
                }
                if (result != 0 || AdrId <= 0)
                {
                    CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                    Logger.LogInfo(error + documentXml);
                }
                else
                {
                    IDAdresu = adresInfo.GIDNumer;
                    Logger.LogDebug($"Cdn_NowyAdres: rep {rep}");
                    if (rep) DBHelper.RunScalarSqlQueryParam($"update a set kna_adres=replace(kna_adres,'.pusta.',''),kna_nazwa2=replace(kna_nazwa2,'.pusta.',''),kna_nazwa3=replace(kna_nazwa3,'.pusta.','') from cdn.kntadresy a where kna_gidnumer={IDAdresu} and kna_gidtyp=896", null);

                    result = ZamknijAdres(ref error, AdrId);
                    if (result != 0 || IDAdresu <= 0)
                    {
                        CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                        Logger.LogInfo(error + documentXml);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error = ex.Message;
            }
            return IDAdresu;
        }
        private int ZamknijAdres(ref string error, int IDAdresu)
        {
            int result = 0;
            try
            {

                XLZamkniecieAdresuInfo_20193 zamkniecieAdresuInfo = new XLZamkniecieAdresuInfo_20193();
                zamkniecieAdresuInfo.Wersja = wersjaAPI;
                lock (sem)
                {
                    result = cdn_api.cdn_api.XLZamknijAdres(IDAdresu, zamkniecieAdresuInfo);
                }

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error += ex.Message;
            }
            return result;
        }

        public DataTable NowyKontrahent(string _nip,string kraj, string nazwa, string ulica, string miasto, string kodPocztowy,string prefix="B2B",string grupa=@"GRUPY\|B2B")
        {
            var nip = _nip.Trim().Replace("-", "").Replace(" ", "");
            DataTable ret = new DataTable();
            ret.Columns.Add("Dodano");
            ret.Columns.Add("gid");
            var tab = Helpers.DBHelper.RunSqlQueryParam("select Knt_GIDNumer from [CDN].[KntKarty] nolock where Knt_Nip = @p1", "T", new List<QueryParam>(new[] { new QueryParam("@p1", nip) }));
            if (tab.Rows.Count > 0 && nip!="")
            {
                ret.Rows.Add(false, tab.Rows[0].ItemArray[0]);
            }
            else
            {
                int IDKontrahenta = 0;
                string Akronik = "";
                string error = "";
                try
                {
                    int result = 0;
                    int KonId = 0;
                    XLKontrahentInfo_20193 kontrahentInfo = new XLKontrahentInfo_20193();
                    kontrahentInfo.Wersja = wersjaAPI;
                    kontrahentInfo.Akronim = prefix+"x";
                    kontrahentInfo.NipE = nip; 
                    kontrahentInfo.Nazwa1 = nazwa;
                    kontrahentInfo.Ulica = ulica;
                    kontrahentInfo.Miasto = miasto;
                    kontrahentInfo.KodP = kodPocztowy;
                    kontrahentInfo.Kraj = kraj;
                    kontrahentInfo.Tryb = 2;
                    kontrahentInfo.GRPFirma=gidFirma;
                    kontrahentInfo.GRPNumer=gidGrupaKnt;
                    kontrahentInfo.GRPLp=0;
                    kontrahentInfo.GRPTyp=-32;
                    kontrahentInfo.GrupaSciezka = grupa;
                    Logger.LogDebug(string.Format("nowy kontrahent grupa: {0}",kontrahentInfo.GrupaSciezka));

                    lock (sem)
                    {
                        result = cdn_api.cdn_api.XLNowyKontrahent(sesja, ref IDKontrahenta, kontrahentInfo);
                        Logger.LogDebug(string.Format("nowy kontrahent grupa: {0}, result {1}",kontrahentInfo.GrupaSciezka,result.ToString()));

                        if (result != 0)
                        {
                            CDNKomunikaty.Komunikaty_GlowneFunkcje(12, result, ref error, wersjaAPI);
                            Logger.LogInfo(string.Format("XLNowyKontrahent result: {0} e:{1}",result,error));
                        }
                        var opp = new XLKomunikatInfo_20193() { Funkcja = 12, Wersja = wersjaAPI, Blad = -1 };
                        var op = cdn_api.cdn_api.XLOpisBledu(opp);
                        if (result == 0)
                        {
                            result = cdn_api.cdn_api.XLZamknijKontrahenta(IDKontrahenta, new XLZamkniecieKontrahentaInfo_20193()
                            {
                                Wersja = wersjaAPI,
                                Tryb = 1,
                            });
                        }

                        if (result != 0)
                        {
                            CDNKomunikaty.Komunikaty_GlowneFunkcje(13, result, ref error, wersjaAPI);
                            Logger.LogInfo(string.Format("XLZamknijKontrahenta result: {0} e:{1}",result,error));
                        }

                        if (result == 0)
                        {
                            var otwmod = new XLModyfikacjaKntSQLInfo_20193()
                            {
                                GIDNumer = kontrahentInfo.GIDNumer,
                                GIDTyp = kontrahentInfo.GIDTyp,
                                GIDFirma = kontrahentInfo.GIDFirma,
                                Wersja = wersjaAPI,
                            };
                            result = cdn_api.cdn_api.XLOtworzKontrahentaSQL(sesja, otwmod);
                            if (result != 0)
                            {
                                Logger.LogInfo(string.Format("XLOtworzKontrahentaSQL result: {0} e: {1}",result,otwmod.OpisBledu));
                            }
                        }

                        if (result == 0)
                        {
                            var mod = new XLModyfikacjaKntSQLInfo_20193()
                            {
                                GIDNumer = kontrahentInfo.GIDNumer,
                                GIDTyp = kontrahentInfo.GIDTyp,
                                GIDFirma = kontrahentInfo.GIDFirma,
                                Wartosc = prefix + "-" + kontrahentInfo.GIDNumer.ToString("000000"),
                                Wersja = wersjaAPI,
                                NazwaPola = "Akronim"
                            };
                            result = cdn_api.cdn_api.XLZmienPoleKntSQL(sesja, mod);
                            if (result != 0)
                            {
                                Logger.LogInfo(string.Format("XLZmienPoleKntSQL result: {0} e: {1}",result,mod.OpisBledu));

                            }
                            Akronik = prefix + "-" + kontrahentInfo.GIDNumer.ToString("000000");
                            IDKontrahenta = kontrahentInfo.GIDNumer;
                            Logger.LogInfo(string.Format("kontrahhent ingo: {0}",kontrahentInfo.GrupaSciezka));
                        }

                        if (result == 0)
                        {
                            var xx2 = cdn_api.cdn_api.XLZamknijKontrahentaSQL(sesja, new XLModyfikacjaKntSQLInfo_20193()
                            {
                                Wersja = wersjaAPI,
                            });
                        }
                    }

                    if (result != 0)
                    {
                        ret.Rows.Add(false, IDKontrahenta);
                        CDNKomunikaty.Komunikaty_GlowneFunkcje(13, result, ref error, wersjaAPI);
                        Logger.LogInfo(string.Format("XLZamknijKontrahentaSQLpr result: {0} e: {1}",result,error));
                    }
                    if (result==0) ret.Rows.Add(true, IDKontrahenta);
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    error = ex.Message;
                }
                
            }
            return ret;
        }

        #endregion
        #region Dodatkowe
        public string PobierzNumerDokumentu(int gidnumer, int gidtyp, ref string error)
        {
            int result = 0;
            try
            {
                XLNumerDokumentuInfo_20193 numerDokumentuInfo = new XLNumerDokumentuInfo_20193();
                numerDokumentuInfo.GIDNumer = gidnumer;
                numerDokumentuInfo.GIDTyp = gidtyp;
                numerDokumentuInfo.Wersja = wersjaAPI;
                lock (sem)
                {
                    result = cdn_api.cdn_api.XLPobierzNumerDokumentu(numerDokumentuInfo);
                    if (result != 0)
                    {
                        error = result.ToString();
                    }
                    return numerDokumentuInfo.NumerDokumentu;

                }

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error += ex.Message;
            }
            return "";
        }
        #endregion

        #region Osoby Kontaktowe

        public string DodajOsobe(string gid, string nazwa, string stanowisko, string telefon, string telefonK, string email, string fax, string dzial, string tytul,int Upowazniona,int UpowaznionaZam)
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("Dodano");
            ret.Columns.Add("gid");
            ret.Columns.Add("lp");

            string KntAkronim = "";
            int lp = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    using (var com = conn.CreateCommand())
                    {
                        com.CommandText = $"select top 1 KnS_KntLp, KnS_KntNumer from cdn.KntOsoby where KnS_EMail = '{email}'";
                        com.CommandType = CommandType.Text;
                        DataTable t = new DataTable();
                        t.Load(com.ExecuteReader());
                        var ia = t.Rows[0].ItemArray;
                        lp = Convert.ToInt32(ia[0]);
                        gid = Convert.ToInt32(ia[1]).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lp = 0;
            }

            if (lp != 0)
            {
                ret.Rows.Add(false, gid, lp);
                return ret.GetJson();
            }

            var parametry = new List<QueryParam>();
            parametry.Add(new QueryParam("@KntAkronim", KntAkronim));
            parametry.Add(new QueryParam("@Nazwa", nazwa));
            parametry.Add(new QueryParam("@Telefon", telefon));
            parametry.Add(new QueryParam("@EMail", email));
            parametry.Add(new QueryParam("@Dzial", dzial));
            parametry.Add(new QueryParam("@TelefonK", telefonK));
            parametry.Add(new QueryParam("@Fax", fax));
            parametry.Add(new QueryParam("@Tytul", tytul));
            parametry.Add(new QueryParam("@KntNumer", gid));
            parametry.Add(new QueryParam("@Upowazniona", Upowazniona));
            parametry.Add(new QueryParam("@UpowaznionaZam", UpowaznionaZam));
            parametry.Add(new QueryParam("@Stanowisko", stanowisko));
            var pret = Helpers.DBHelper.RunScalarSqlProcParam("[dbo].[bmp_XLDodajOsobeDoKNT]", parametry);
            if (pret != null)
            {
                lp = Convert.ToInt32(pret);
            }

            if (lp > 0)
            {
                var atr = new XLAtrybutInfo_20193()
                {
                    Wersja = wersjaAPI,
                    GIDNumer = Convert.ToInt32(gid),
                    GIDLp = lp,
                    GIDSubLp = 8211,
                    GIDTyp = 32,
                    GIDFirma = gidFirma,
                    Wartosc="TAK",
                    Klasa = "B2B_Dostęp"
                };
                var p = cdn_api.cdn_api.XLDodajAtrybut(sesja, atr);
                Logger.LogDebug(string.Format("CurrentCulture is {0}.", System.Globalization.CultureInfo.CurrentCulture.Name));
                Logger.LogDebug(string.Format("dodaj osobe, atrubtu {0}",p.ToString()));
            }


            ret.Rows.Add((pret!=null),gid,lp);
            return ret.GetJson();
        }

        public string DodajOsobe_v2(string gid, string nazwa, string stanowisko, string telefon, string telefonK, string email, string fax, string dzial, string tytul, int Upowazniona, int UpowaznionaZam, bool Dostep)
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("Dodano");
            ret.Columns.Add("gid");
            ret.Columns.Add("lp");
            ret.Columns.Add("dostep");

            string KntAkronim = "";
            int lp = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    using (var com = conn.CreateCommand())
                    {
                        com.CommandText = $"select top 1 KnS_KntLp, KnS_KntNumer from cdn.KntOsoby where KnS_EMail = '{email}'";
                        com.CommandType = CommandType.Text;
                        DataTable t = new DataTable();
                        t.Load(com.ExecuteReader());
                        var ia = t.Rows[0].ItemArray;
                        lp = Convert.ToInt32(ia[0]);
                        gid = Convert.ToInt32(ia[1]).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lp = 0;
            }

            if (lp != 0)
            {
                var atrvar = DBHelper.RunScalarSqlQueryParam($"SELECT [Atr_Wartosc] FROM [CDN].[Atrybuty] join [CDN].[AtrybutyKlasy] on AtK_ID = Atr_AtkId where AtK_Nazwa = 'B2B_Dostęp' and Atr_ObiSubLp = 8211 and Atr_ObiNumer = {gid} and Atr_ObiLp = {lp}", null);

                ret.Rows.Add(false, gid, lp, atrvar);
                return ret.GetJson();
            }

            var parametry = new List<QueryParam>();
            parametry.Add(new QueryParam("@KntAkronim", KntAkronim));
            parametry.Add(new QueryParam("@Nazwa", nazwa));
            parametry.Add(new QueryParam("@Telefon", telefon));
            parametry.Add(new QueryParam("@EMail", email));
            parametry.Add(new QueryParam("@Dzial", dzial));
            parametry.Add(new QueryParam("@TelefonK", telefonK));
            parametry.Add(new QueryParam("@Fax", fax));
            parametry.Add(new QueryParam("@Tytul", tytul));
            parametry.Add(new QueryParam("@KntNumer", gid));
            parametry.Add(new QueryParam("@Upowazniona", Upowazniona));
            parametry.Add(new QueryParam("@UpowaznionaZam", UpowaznionaZam));
            parametry.Add(new QueryParam("@Stanowisko", stanowisko));
            var pret = Helpers.DBHelper.RunScalarSqlProcParam("[dbo].[bmp_XLDodajOsobeDoKNT]", parametry);
            if (pret != null)
            {
                lp = Convert.ToInt32(pret);
            }

            var atr = new XLAtrybutInfo_20193()
            {
                Wersja = wersjaAPI,
                GIDNumer = Convert.ToInt32(gid),
                GIDLp = lp,
                GIDSubLp = 8211,
                GIDTyp = 32,
                GIDFirma = gidFirma,
                Wartosc = Dostep?"TAK":"NIE",
                Klasa = "B2B_Dostęp"
            };

            if (lp > 0)
            {                
                var p = cdn_api.cdn_api.XLDodajAtrybut(sesja, atr);
                Logger.LogDebug(string.Format("CurrentCulture is {0}.", System.Globalization.CultureInfo.CurrentCulture.Name));
                Logger.LogDebug(string.Format("dodaj osobe v2, atrybutwynik: {0}, klasa {1}, lp {2} ",p.ToString(),atr.Klasa.ToString(),atr.GIDLp.ToString()));
            }

            ret.Rows.Add((pret != null), gid, lp, atr.Wartosc);
            return ret.GetJson();
        }

        #endregion
        #region platnosci
        public string PrzyjeciePlatnosci(int typ_dokumentu, int id_dokumentu, decimal kwota,string id_transakcji)
        {
            var dt = new DataTable();
            dt.Columns.Add("Blad", typeof(int));
            dt.Columns.Add("Komunikat", typeof(string));

            string nrDok = "Płatność";

            GidData KntGid = null;
            GidData KPGid = null;
            GidData DokGid = null;

            if (typ_dokumentu == 960)
            {
                var pom = DBHelper.RunSqlQuery($"select ZaN_Stan,ZaN_GIDFirma,ZaN_GIDLp,ZaN_GIDNumer,ZaN_GIDTyp from cdn.zamnag where ZaN_GIDTyp = {typ_dokumentu} and ZaN_GIDNumer = {id_dokumentu}", "T");
                if (pom.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "Nie znaleziono dokumentu");
                    Logger.LogDebug($"CDNOperations {MethodBase.GetCurrentMethod()} line {(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber()} return={dt.GetJson()}");
                    return dt.GetJson();
                }

                if (Convert.ToInt32(pom.Rows[0][0]) != 3)
                {
                    dt.Rows.Add(-2, "Zamówienie w złym statusie");
                    Logger.LogDebug($"CDNOperations {MethodBase.GetCurrentMethod()} line {(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber()} return={dt.GetJson()}");
                    return dt.GetJson();
                }

                DokGid = new GidData()
                {
                    GidFirma = Convert.ToInt32(pom.Rows[0][1]),
                    GidLp = Convert.ToInt32(pom.Rows[0][2]),
                    GidNumer = Convert.ToInt32(pom.Rows[0][3]),
                    GidTyp = Convert.ToInt32(pom.Rows[0][4]),
                };

                var kon = DBHelper.RunSqlQuery($"select zan_KntFirma,zan_KntLp,zan_KntNumer,zan_KntTyp, CDN.NumerDokumentuTRN ( CDN.DokMapTypDokumentu (ZaN_GIDTyp,ZaN_ZamTyp, ZaN_Rodzaj),0,0,ZaN_ZamNumer,ZaN_ZamRok,ZaN_ZamSeria) from cdn.zamnag where zan_GIDTyp = {typ_dokumentu} and zan_GIDNumer = {id_dokumentu}", "t").Rows[0].ItemArray;

                KntGid = new GidData()
                {
                    GidFirma = Convert.ToInt32(kon[0]),
                    GidLp = Convert.ToInt32(kon[1]),
                    GidNumer = Convert.ToInt32(kon[2]),
                    GidTyp = Convert.ToInt32(kon[3]),
                };

                nrDok = kon[4].ToString();
            }

            if (typ_dokumentu == 2033)
            {

                var pom = DBHelper.RunSqlQuery($"select TrN_KntFirma,TrN_KntLp,TrN_KntNumer,TrN_KntTyp,TrN_GIDFirma,TrN_GIDLp,TrN_GIDNumer,TrN_GIDTyp from cdn.tranag where TrN_GIDTyp = {typ_dokumentu} and TrN_GIDNumer = {id_dokumentu}", "t");

                if (pom.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "Nie znaleziono dokumentu");
                    Logger.LogDebug($"CDNOperations {MethodBase.GetCurrentMethod()} line {(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber()} return={dt.GetJson()}");
                    return dt.GetJson();
                }

                var kon = pom.Rows[0].ItemArray;


                var f = Convert.ToInt32(DBHelper.RunSqlQuery($"select sum(TrP_Pozostaje)from cdn.TraPlat where TrP_GIDTyp = {typ_dokumentu} and TrP_GIDNumer = {id_dokumentu}", "t").Rows[0].ItemArray[0]??0);
                if (f == 0)
                {
                    dt.Rows.Add(-3, "Faktura jest rozliczona");
                    Logger.LogDebug($"CDNOperations {MethodBase.GetCurrentMethod()} line {(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber()} return={dt.GetJson()}");
                    return dt.GetJson();
                }

                
                KntGid = new GidData()
                {
                    GidFirma = Convert.ToInt32(kon[0]),
                    GidLp = Convert.ToInt32(kon[1]),
                    GidNumer = Convert.ToInt32(kon[2]),
                    GidTyp = Convert.ToInt32(kon[3]),
                };

                DokGid = new GidData()
                {
                    GidFirma = Convert.ToInt32(kon[4]),
                    GidLp = Convert.ToInt32(kon[5]),
                    GidNumer = Convert.ToInt32(kon[6]),
                    GidTyp = Convert.ToInt32(kon[7]),
                };
            }

            var zapis = new XLZapisKasowyInfo_20193()
            {
                Bufor = 0,
                Wersja = wersjaAPI,
                Tryb = 2,
                Kwota = kwota.ToString(),
                Kasa = ConfigurationManager.AppSettings["Kasa"],
                Operacja = ConfigurationManager.AppSettings["OperacjaKP"],
                KNTFirma = KntGid.GidFirma,
                KNTLp = KntGid.GidLp,
                KNTNumer = KntGid.GidNumer,
                KNTTyp = KntGid.GidTyp,
                Tresc = id_transakcji,
                Numer = nrDok,
            };

            Logger.LogDebug($"CDNOperations {MethodBase.GetCurrentMethod()} line {(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber()} sesja={sesja}");
            int bl = cdn_api.cdn_api.XLDodajZapis(sesja, 0, zapis);
            Logger.LogDebug($"CDNOperations {MethodBase.GetCurrentMethod()} line {(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber()} bl={bl}");
            KPGid = new GidData();
            KPGid.GidFirma = zapis.GIDFirma;
            KPGid.GidLp = zapis.GIDLp;
            KPGid.GidNumer = zapis.GIDNumer;
            KPGid.GidTyp = zapis.GIDTyp;

            if (bl != 0)
            {
                var obl = new Dictionary<int,string>()
                {
                    { -1,"błędny parametr SesjaID" },
                    { 925,"błąd autonumeracji" },
                    { 935,"błąd zakładania transakcji" },
                    { 1002,"brak parametru Tryb" },
                    { 8007,"nie znaleziono bieżącego okresu obrachunkowego" },
                    { 8034,"raport kasowy zamknięty" },
                    { 8035,"zła data zapisu kasowego" },
                    { 8131,"kasa poniżej zera" },
                    { 8156,"brak operacji kasowej" },
                    { 8157,"brak rejestru kasowego" },
                    { 8158,"nie znaleziono raportu o podanym RaportID" },
                    { 8174,"Nie znaleziono w bazie podmiotu (kontrahenta lub pracownika) o podanym identyfikatorze. Dodanie zapisu niemożliwe" },
                    { 8182,"operacja nie jest przypisana do rejestru" },
                    {  8309,"Kwota równa zero, nie można wprowadzić takiego zapisu" },
                    { 8310,"Brak treści" },
                };

                var opis = obl.ContainsKey(bl) ? obl[bl] : "";
                dt.Rows.Add(-3, $"nieudało się stworzyć zapisu kasowego \n {opis}");
                Logger.LogDebug($"CDNOperations {MethodBase.GetCurrentMethod()} line {(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber()} return={dt.GetJson()}");
                return dt.GetJson();
            }

            string nrKp = DBHelper.RunSqlQuery($@"select SUBSTRING(cast(KRP_Rok as varchar(4)),3,2)+'/'+KAZ_Seria+'/' +cast(KRP_Numer as varchar(10)) +'/' +  cast(KAZ_KRPLp as varchar(10))
from CDN.Zapisy inner join cdn.Raporty on KRP_GIDNumer = KAZ_KRPNumer where {KPGid.GidNumer} = KAZ_GIDNumer", "T").Rows[0][0].ToString();

            if (typ_dokumentu == 960)
            {
                #region atrybut numer KP
                var kl = ConfigurationManager.AppSettings["AtrybutPlatnosciNaZamowieniu"];
                
                var podt = DBHelper.RunSqlQuery($"select Atr_Id from cdn.atrybuty join cdn.AtrybutyKlasy on Atr_AtkId = AtK_ID where Atr_ObiTyp = 960 and AtK_Nazwa = '{kl}' and Atr_ObiNumer = {DokGid.GidNumer}", "");

                if (podt.Rows.Count == 0)
                {
                    var atrbl = DodajAtrybutZS(kl, nrKp, DokGid);
                    if (atrbl != null) return atrbl;
                }
                else
                {
                    DopiszAtrybutZS(nrKp, Convert.ToInt32(podt.Rows[0][0]));
                }
                #endregion 

                #region atrybut id KP
                var klid = ConfigurationManager.AppSettings["AtrybutIdPlatnosciNaZamowieniu"];

                var podtid = DBHelper.RunSqlQuery($"select Atr_Id from cdn.atrybuty join cdn.AtrybutyKlasy on Atr_AtkId = AtK_ID where Atr_ObiTyp = 960 and AtK_Nazwa = '{klid}' and Atr_ObiNumer = {DokGid.GidNumer}", "");

                if (podtid.Rows.Count == 0)
                {
                    var atrbl = DodajAtrybutZS(klid, KPGid.GidNumer.ToString(), DokGid);
                    if (atrbl != null) return atrbl;
                }
                else
                {
                    DopiszAtrybutZS(KPGid.GidNumer.ToString(), Convert.ToInt32(podtid.Rows[0][0]));
                }
                #endregion
            }

            if (typ_dokumentu == 2033)
            {
                var rozliczenie = new XLRozliczenieInfo_20193() { Wersja = wersjaAPI};
                bl = cdn_api.cdn_api.XLRozliczaj(sesja, new XLGIDParaInfo_20193()
                {
                    Wersja = wersjaAPI,
                    GID1Firma = DokGid.GidFirma,
                    GID1Lp = DokGid.GidLp,
                    GID1Numer = DokGid.GidNumer,
                    GID1Typ = DokGid.GidTyp,
                    GID2Firma = KPGid.GidFirma,
                    GID2Lp = KPGid.GidLp,
                    GID2Numer = KPGid.GidNumer,
                    GID2Typ = KPGid.GidTyp,
                }, rozliczenie);

                if (bl != 0)
                {
                    dt.Rows.Add(-5, "błąd podczas dodawania rozliczenia");
                    Logger.LogDebug($"CDNOperations {MethodBase.GetCurrentMethod()} line {(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber()} return={dt.GetJson()}");
                    return dt.GetJson();
                }
            }

            dt.Rows.Add(0, "OK");
            Logger.LogDebug($"CDNOperations {MethodBase.GetCurrentMethod()} line {(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber()} return={dt.GetJson()}");
            return dt.GetJson();
        }
        #endregion

        #region nowezamjednorazowy
        private class kontrahentAdres
        {
            public int knt {get;set;}
            public int adresGid {get;set;}
            public int adresGidTyp {get;set;}
            public int adresGidFirma {get;set;}
            public int adresGidLp {get;set;}
            public int adresWGid {get;set;}
            public int adresWGidTyp {get;set;}
            public int adresWGidFirma {get;set;}
            public int adresWGidLp {get;set;}
            public string error {get;set;}
        }   
        public int nowyDokumentZamJednorazowy(int typ, string xmlAdres, string xmlAdresW, string xmlOrder, ref string error, int TrybZamkniecia = 0,string KodRabatowy = null)
        {
            xmlAdres=xmlAdres.Replace("_20163", "_20193");
            xmlAdresW=xmlAdresW.Replace("_20163", "_20193");
            xmlOrder=xmlOrder.Replace("_20163", "_20193");

            Logger.LogDebug("tryb zamkniecia" + TrybZamkniecia.ToString());
            if (trybZamknieciaZam!=null) {TrybZamkniecia=(int)trybZamknieciaZam;}
            error = "";
            kontrahentAdres kntAdres=new kontrahentAdres();
            kntAdres=pobierzKontrahentaIAdres(xmlAdres,xmlAdresW);
            
            error = kntAdres.error;
            int IDDokumentuZam = 0;
            if(kntAdres.knt!=-1) {
                try
            {
                int result = 0;
                int ZamId = 0;
                Logger.LogDebug("Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                XLDokumentZamNagInfo_20193 dokumentZamNagInfo = new XLDokumentZamNagInfo_20193();
                dokumentZamNagInfo = (XLDokumentZamNagInfo_20193)CDNSerializerDeserializer.Deserialize(xmlOrder, dokumentZamNagInfo);
                dokumentZamNagInfo.Wersja = wersjaAPI;
                dokumentZamNagInfo.Tryb = tryb;
                dokumentZamNagInfo.Typ = typ;
                dokumentZamNagInfo.KntTyp = 32;
                dokumentZamNagInfo.ZamSeria = DBHelper.GetSeriaMagazynu(dokumentZamNagInfo.MagNumer);
                dokumentZamNagInfo.Cecha = "B2B";
                dokumentZamNagInfo.RezerwujZasoby = -1;
                dokumentZamNagInfo.RezerwacjeNaNiepotwierdzonym = 0;
                dokumentZamNagInfo.AdrNumer=kntAdres.adresGid;
                dokumentZamNagInfo.AdrTyp=kntAdres.adresGidTyp;
                dokumentZamNagInfo.AdrFirma=kntAdres.adresGidFirma;
                dokumentZamNagInfo.AdrLp=kntAdres.adresGidLp;
                dokumentZamNagInfo.AdPNumer=kntAdres.adresGid;
                dokumentZamNagInfo.AdwNumer=kntAdres.adresWGid;
                dokumentZamNagInfo.AdwTyp=kntAdres.adresWGidTyp;
                dokumentZamNagInfo.AdwFirma=kntAdres.adresWGidFirma;
                dokumentZamNagInfo.AdwLp=kntAdres.adresWGidLp;
                dokumentZamNagInfo.KntNumer=kntAdres.knt;

                lock (sem)
                {
                    result = cdn_api.cdn_api.XLNowyDokumentZam(sesja, ref ZamId, dokumentZamNagInfo);
                    Logger.LogDebug(string.Format("Nowy dokument zam kontrahent: {0} typ:{3} status: {1} sesja: {2}", dokumentZamNagInfo.KntNumer, result.ToString(), sesja.ToString(),dokumentZamNagInfo.Typ));
                }
                if (result != 0 || ZamId <= 0)
                {
                    CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                    Logger.LogInfo(string.Format("Dodaj dokument zam: {0}, xml: {1}" ,error ,xmlOrder));
                }
                else
                {
                    IDDokumentuZam = dokumentZamNagInfo.GIDNumer;
                    DodajPozycjeZam(xmlOrder, ref error, ZamId);
                    if (string.IsNullOrEmpty(error))
                    {
                        result = ZamknijDokumentZam(ref error, ZamId, TrybZamkniecia);
                        if (result != 0 || IDDokumentuZam <= 0)
                        {
                            CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                            Logger.LogInfo("Zamknij dokument zam: " + error + xmlOrder);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                kntAdres.error += ex.Message;
            }
            }
            kntAdres.error += error;
            return IDDokumentuZam;
        }
        private kontrahentAdres pobierzKontrahentaIAdres(string xmlAdres,string xmlAdresW)
        {
            kontrahentAdres kntAdres = new kontrahentAdres();
            int kntid=-1;
            int knttyp=-1;
            int adrid=-1;
            int adrWid=-1;
            string error="";

            XLAdresInfo_20193 adresInfo = new XLAdresInfo_20193();
            adresInfo = (XLAdresInfo_20193)CDNSerializerDeserializer.Deserialize(xmlAdres, adresInfo);
            adresInfo.Wersja = wersjaAPI;
            adresInfo.Tryb = tryb;
            adresInfo.KntTyp = 32;
            adresInfo.Wysylkowy=1;
            adresInfo.NipE=adresInfo.NipE.Trim().Replace("-", "").Replace(" ", "");
            
            XLAdresInfo_20193 adresInfoW = new XLAdresInfo_20193();
            adresInfoW = (XLAdresInfo_20193)CDNSerializerDeserializer.Deserialize(xmlAdresW, adresInfo);
            adresInfoW.Wersja = wersjaAPI;
            adresInfoW.Tryb = tryb;
            adresInfoW.KntTyp = 32;
            adresInfo.Wysylkowy=1;
            adresInfoW.NipE=adresInfo.NipE.Trim().Replace("-", "").Replace(" ", "");

            List<QueryParam> lstPar = new List<QueryParam>();
            lstPar.Add(new QueryParam("@email", adresInfo.EMail));
            lstPar.Add(new QueryParam("@nip", adresInfo.NipE));
            DataTable dt = DBHelper.RunSqlQueryParam("select knt_gidnumer,knt_gidtyp from cdn.kntkarty (nolock) where knt_email=@email and knt_nipe=case when @nip='' then knt_nipe else @nip end","kntkarty",lstPar);
            if (dt.Rows.Count > 0)
            {
                Logger.LogDebug(string.Format("nowydokument"));
                Int32.TryParse(dt.Rows[0].ItemArray[0].ToString(),out kntid);
                Int32.TryParse(dt.Rows[0].ItemArray[1].ToString(),out knttyp);
                kntAdres.knt=kntid;
                if (kntid != -1){
                adresInfo.KntNumer=kntid;
                adresInfo.KntTyp=knttyp;
                adresInfoW.KntNumer=kntid;
                adresInfoW.KntTyp=knttyp;
                adrid=nowyAdresJednorazowyZmien(adresInfo,ref error);
                kntAdres.error+=error;
                error="";
                adrWid=nowyAdresJednorazowy(adresInfoW,ref error);
                kntAdres.error+=error;
                error="";
                kntAdres.adresGid=adresInfo.GIDNumer;
                kntAdres.adresGidTyp=adresInfo.GIDTyp;
                kntAdres.adresGidFirma=adresInfo.GIDFirma;
                kntAdres.adresGidLp=adresInfo.GIDLp;
                kntAdres.adresWGid=adresInfoW.GIDNumer;
                kntAdres.adresWGidTyp=adresInfoW.GIDTyp;
                kntAdres.adresWGidFirma=adresInfoW.GIDFirma;
                kntAdres.adresWGidLp=adresInfoW.GIDLp;
                }
            }
            else
            {
                kntid = nowyKontrahentJednorazowy(adresInfo,ref error);
                kntAdres.error+=error;
                error="";
                kntAdres.knt=kntid;
                if (kntid != -1){
                adresInfo.KntNumer=kntid;
                adresInfoW.KntNumer=kntid;
                adrid=nowyAdresJednorazowy(adresInfo,ref error);
                kntAdres.error+=error;
                error="";
                adrWid=nowyAdresJednorazowy(adresInfoW,ref error);
                kntAdres.error+=error;
                error="";
                kntAdres.adresGid=adresInfo.GIDNumer;
                kntAdres.adresGidTyp=adresInfo.GIDTyp;
                kntAdres.adresGidFirma=adresInfo.GIDFirma;
                kntAdres.adresGidLp=adresInfo.GIDLp;
                kntAdres.adresWGid=adresInfoW.GIDNumer;
                kntAdres.adresWGidTyp=adresInfoW.GIDTyp;
                kntAdres.adresWGidFirma=adresInfoW.GIDFirma;
                kntAdres.adresWGidLp=adresInfoW.GIDLp;
                }
            }
            kntAdres.error+=error;
            error="";
            return kntAdres;
        }
        private int nowyKontrahentJednorazowy(XLAdresInfo_20193 adresInfo,ref string error)
        {
                string err="";
                int IDKontrahenta = -1;
                string Akronik = "";
                try
                {
                    int result = 0;
                    XLKontrahentInfo_20193 kontrahentInfo = new XLKontrahentInfo_20193();
                    kontrahentInfo.Wersja = wersjaAPI;
                    kontrahentInfo.Akronim = "B2Bx";
                    kontrahentInfo.NipE = adresInfo.NipE; 
                    kontrahentInfo.Nazwa1 = adresInfo.Nazwa1;
                    kontrahentInfo.Ulica = adresInfo.Ulica;
                    kontrahentInfo.Miasto = adresInfo.Miasto;
                    kontrahentInfo.KodP = adresInfo.KodP;
                    kontrahentInfo.Kraj = adresInfo.Kraj;
                    kontrahentInfo.EMail=adresInfo.EMail;
                    kontrahentInfo.Telefon1=adresInfo.Telefon1;
                    kontrahentInfo.Tryb = 2;
                    kontrahentInfo.Status = adresInfo.NipE.Length>1 ? 1:2;
                    lock (sem)
                    {
                        result = cdn_api.cdn_api.XLNowyKontrahent(sesja, ref IDKontrahenta, kontrahentInfo);
                        if (result != 0)
                        {
                            CDNKomunikaty.Komunikaty_GlowneFunkcje(12, result, ref err, wersjaAPI);
                            Logger.LogInfo(err);
                            error +=err;
                        }
                        var opp = new XLKomunikatInfo_20193() { Funkcja = 12, Wersja = wersjaAPI, Blad = -1 };
                        var op = cdn_api.cdn_api.XLOpisBledu(opp);
                        if (result == 0)
                        {
                            result = cdn_api.cdn_api.XLZamknijKontrahenta(IDKontrahenta, new XLZamkniecieKontrahentaInfo_20193()
                            {
                                Wersja = wersjaAPI,
                                Tryb = 1,
                            });
                        }

                        if (result != 0)
                        {
                            CDNKomunikaty.Komunikaty_GlowneFunkcje(13, result, ref err, wersjaAPI);
                            Logger.LogInfo(err);
                            error +=err;
                        }

                        if (result == 0)
                        {
                            var otwmod = new XLModyfikacjaKntSQLInfo_20193()
                            {
                                GIDNumer = kontrahentInfo.GIDNumer,
                                GIDTyp = kontrahentInfo.GIDTyp,
                                GIDFirma = kontrahentInfo.GIDFirma,
                                Wersja = wersjaAPI,
                            };
                            result = cdn_api.cdn_api.XLOtworzKontrahentaSQL(sesja, otwmod);
                            if (result != 0)
                            {
                                Logger.LogInfo(otwmod.OpisBledu);
                            error +=otwmod.OpisBledu;
                            }
                        }

                        if (result == 0)
                        {
                            var mod = new XLModyfikacjaKntSQLInfo_20193()
                            {
                                GIDNumer = kontrahentInfo.GIDNumer,
                                GIDTyp = kontrahentInfo.GIDTyp,
                                GIDFirma = kontrahentInfo.GIDFirma,
                                Wartosc = "B2B-J-" + kontrahentInfo.GIDNumer.ToString("0000000"),
                                Wersja = wersjaAPI,
                                NazwaPola = "Akronim"
                            };
                            result = cdn_api.cdn_api.XLZmienPoleKntSQL(sesja, mod);
                            if (result != 0)
                            {
                                Logger.LogInfo(mod.OpisBledu);
                            }
                            Akronik = "B2B-J-" + kontrahentInfo.GIDNumer.ToString("0000000");
                            IDKontrahenta = kontrahentInfo.GIDNumer;
                        }

                        if (result == 0)
                        {
                            var xx2 = cdn_api.cdn_api.XLZamknijKontrahentaSQL(sesja, new XLModyfikacjaKntSQLInfo_20193()
                            {
                                Wersja = wersjaAPI,
                            });
                        }
                    }

                    if (result != 0 || IDKontrahenta <= 0)
                    {
                        CDNKomunikaty.Komunikaty_GlowneFunkcje(13, result, ref err, wersjaAPI);
                        Logger.LogInfo(err);
                        error += err;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    error += ex.Message;
                }
                return IDKontrahenta;
            }
        private int nowyAdresJednorazowy(XLAdresInfo_20193 adresInfo, ref string error)
        {
            int IDAdresu = 0;
            try
            {
                int result = 0;
                int AdrId = 0;
                lock (sem)
                {
                    result = cdn_api.cdn_api.XLNowyAdres(sesja, ref AdrId, adresInfo);
                }
                if (result != 0 || AdrId <= 0)
                {
                    CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                    Logger.LogInfo(error + CDNSerializerDeserializer.Serialize(adresInfo));
                }
                else
                {
                    IDAdresu = adresInfo.GIDNumer;
                    result = ZamknijAdres(ref error, AdrId);
                    if (result != 0 || IDAdresu <= 0)
                    {
                        CDNKomunikaty.Komunikaty_GlowneFunkcje(41, result, ref error, wersjaAPI);
                        Logger.LogInfo(error + CDNSerializerDeserializer.Serialize(adresInfo));
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error = ex.Message;
            }
            return IDAdresu;

        }
        private int nowyAdresJednorazowyZmien(XLAdresInfo_20193 adresInfo, ref string error)
        {
            int IDAdresu = 0;
            try
            {
                int result = 0;
                int AdrId = 0;
                lock (sem)
                {
                    result = cdn_api.cdn_api.XLZmienAdres(adresInfo);
                    IDAdresu=adresInfo.GIDNumer;
                }
                
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                error = ex.Message;
            }
            return IDAdresu;

        }
        #endregion

        int DopiszAtrybutZS(string wartosc,int idAtr)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
            {
                conn.Open();
                using (var com = conn.CreateCommand())
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = $"update cdn.atrybuty set Atr_Wartosc = Atr_Wartosc+';{wartosc}' where Atr_Id = {idAtr}";
                    return com.ExecuteNonQuery();
                }
            }
        }

        string DodajAtrybutZS(string klasa, string wartosc,GidData gidObjektu)
        {
            int bl = cdn_api.cdn_api.XLDodajAtrybut(sesja, new XLAtrybutInfo_20193()
            {
                GIDFirma = gidObjektu.GidFirma,
                GIDLp = gidObjektu.GidLp,
                GIDNumer = gidObjektu.GidNumer,
                GIDTyp = gidObjektu.GidTyp,
                Wersja = wersjaAPI,
                Klasa = klasa,
                Wartosc = wartosc,
                ZamTyp = 1280,
                ZamRodzaj = 4,
            });

            if (bl != 0)
            {
                var dt = new DataTable();
                dt.Columns.Add("Blad", typeof(int));
                dt.Columns.Add("Komunikat", typeof(string));
                dt.Rows.Add(-4, "błąd podczas dodawania atrybutu");
                return dt.GetJson();
            }
            return null;
        }

        int DopiszAtrybut(string wartosc, int idAtr)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
            {
                conn.Open();
                using (var com = conn.CreateCommand())
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = $"update cdn.atrybuty set Atr_Wartosc = Atr_Wartosc+';{wartosc}' where Atr_Id = {idAtr}";
                    return com.ExecuteNonQuery();
                }
            }
        }

        string DodajAtrybut(string klasa, string wartosc, GidData gidObjektu)
        {
            int bl = cdn_api.cdn_api.XLDodajAtrybut(sesja, new XLAtrybutInfo_20193()
            {
                GIDFirma = gidObjektu.GidFirma,
                GIDLp = gidObjektu.GidLp,
                GIDNumer = gidObjektu.GidNumer,
                GIDTyp = gidObjektu.GidTyp,
                Wersja = wersjaAPI,
                Klasa = klasa,
                Wartosc = wartosc
            });

            if (bl != 0)
            {
                var dt = new DataTable();
                dt.Columns.Add("Blad", typeof(int));
                dt.Columns.Add("Komunikat", typeof(string));
                dt.Rows.Add(-4, "błąd podczas dodawania atrybutu");
                return dt.GetJson();
            }
            return null;
        }

    }
}
