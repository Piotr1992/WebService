using OP_CSRSLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Helpers.Logger;
using System.Configuration;

namespace OptimaOperations
{
    public class OptimaOperations
    {
        static object lock_obj = new object();

        public string test_datetime()
        {
            return DateTime.Now.ToString();
        }

        public int NoweZlecenieSerwisowe(int id_urzadzenia, int id_prac, string opis_zlecenia)
        {
            try
            {
                lock (lock_obj)
                {
                    int error = 0;
                    string login = ConfigurationManager.AppSettings["login"];
                    string haslo = ConfigurationManager.AppSettings["haslo"];
                    string firma = ConfigurationManager.AppSettings["firma"];
                    string sciezka = ConfigurationManager.AppSettings["optimaloc"];
                    string konString = ConfigurationManager.AppSettings["konstring"];

                    System.Environment.CurrentDirectory = sciezka;
                    CDNBase.Application rApp;
                    CDNBase.ILogin rLogin;
                    CDNBase.AdoSession Sesja;
                    SrsUrzadzenia Urzadzenia;
                    SrsUrzadzenie urzadzenie;
                    CDNHlmn.Magazyny Magazyny;
                    CDNHlmn.Magazyn nagazyn;
                    SrsZlecenia dokumenty;
                    ISrsZlecenie dokument;
                    rApp = new CDNBase.Application();
                    try
                    {                        
                        rApp.LockApp(1);

                        Logger.LogDebug(string.Format("haslo:{0},usr:{1},firma:{2}", null, login, firma));
                        rLogin = rApp.Login(login, haslo, firma, _ModulSRW: 1);
                        Logger.LogInfo(string.Format("NoweZlecenieSeriwsowe zalogowano, {0}", DateTime.Now));
                        Sesja = rLogin.CreateSession();

                        Urzadzenia = (SrsUrzadzenia)Sesja.CreateObject("CDN.SrsUrzadzenia", null);
                        urzadzenie = (SrsUrzadzenie)Urzadzenia[$"SrU_SrUId={id_urzadzenia}"];

                        Magazyny = (CDNHlmn.Magazyny)Sesja.CreateObject("CDN.Magazyny", null);
                        nagazyn = (CDNHlmn.Magazyn)Magazyny[$"Mag_Typ=3"];

                        dokumenty = (SrsZlecenia)Sesja.CreateObject("CDN.SrsZlecenia", null);
                        dokument = (ISrsZlecenie)dokumenty.AddNew(null);

                        dokument.Opis = opis_zlecenia;
                        dokument.SrsUrzadzenie = urzadzenie;
                        dokument.Magazyn = nagazyn;
                        dokument.ProwadzacyId = id_prac;
                        dokument.Stan = 1;
                        dokument.KatID = 1;
                        dokument.DataPrzyjecia = DateTime.Now;
                        dokument.DataRealizacji = DateTime.Now;
                        dokument.DataDok = DateTime.Now;
                        Sesja.Save();
                        error = dokument.ID;
                        Logger.LogInfo(string.Format("NoweZlecenieSeriwsowe utworzono zlecenie id:{0}", error));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                        error = -1;
                    }
                    finally
                    {
                        rApp.LoginOut();
                        rApp.UnlockApp();
                        rLogin = null;
                        rApp = null;
                        Sesja = null;
                    }
                    return error;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException($"{ex}");
                return -1;
            }
        }
        public int NowaCzynnoscZlecenia(int id_zlecenia, int id_pracownika, int id_kodczynnosci, string kodczynnosci_opis, int zrealizowano)
        {
            try
            { 
                lock (lock_obj)
                {
                    int error = 0;
                    string login = ConfigurationManager.AppSettings["login"];
                    string haslo = ConfigurationManager.AppSettings["haslo"];
                    string firma = ConfigurationManager.AppSettings["firma"];
                    string sciezka = ConfigurationManager.AppSettings["optimaloc"];
                    string konString = ConfigurationManager.AppSettings["konstring"];

                    System.Environment.CurrentDirectory = sciezka;
                    CDNBase.Application rApp;
                    CDNBase.ILogin rLogin;
                    CDNBase.AdoSession Sesja;
                    SrsCzynnosc srsCzynnosc;
                    SrsZlecenia srsZlecenia;
                    ISrsZlecenie srsZlecenie;

                    rApp = new CDNBase.Application();
                    try
                    {                        
                        rApp.KonfigConnectStr = konString;                        
                        rApp.LockApp(1);
                        rLogin = rApp.Login(login, haslo, firma, _ModulSRW: 1);
                        Logger.LogDebug(string.Format("NowaCzynnoscZlecenia zalogowano, {0}", DateTime.Now));
                        Sesja = rLogin.CreateSession();
                        Logger.LogDebug(string.Format("NowaCzynnoscZlecenia sesja, {0}", DateTime.Now));
                        srsZlecenia = (SrsZlecenia)Sesja.CreateObject("CDN.SrsZlecenia", null);
                        srsZlecenie = (ISrsZlecenie)srsZlecenia[$"SrZ_SrZId={id_zlecenia}"];
                        Logger.LogDebug(string.Format("NowaCzynnoscZlecenia zlecenie, {0}", DateTime.Now));
                        srsCzynnosc = srsZlecenie.Czynnosci.AddNew();
                        Logger.LogDebug(string.Format("NowaCzynnoscZlecenia zalogowano, {0}", DateTime.Now));
                        srsCzynnosc.TwrId = id_kodczynnosci;
                        srsCzynnosc.TwrNazwa = kodczynnosci_opis;
                        srsCzynnosc.SerwisantId = id_pracownika;
                        srsCzynnosc.Lp = srsZlecenie.Czynnosci.Count;
                        srsCzynnosc.Zakonczona = zrealizowano;
                        srsCzynnosc.DataWykonania = DateTime.Now;
                        srsCzynnosc.TerminOd = DateTime.Now;
                        srsCzynnosc.TerminDo = DateTime.Now;
                        Logger.LogDebug(string.Format("NowaCzynnoscZlecenia przed save, {0}", DateTime.Now));
                        Sesja.Save();
                        Logger.LogDebug(string.Format("NowaCzynnoscZlecenia po save, {0}", DateTime.Now));
                        error = srsCzynnosc.ID;
                        Logger.LogDebug(string.Format("NowaCzynnoscZlecenia utworzono nowa czynnosc id: {0} w zleceniu id:{1}", error, srsZlecenie.ID));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                        error = -1;
                    }
                    finally
                    {
                        rApp.LoginOut();
                        rApp.UnlockApp();
                        rLogin = null;
                        rApp = null;
                        Sesja = null;
                    }
                    return error;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException($"{ex}");
                return -1;
            }
        }
        public int ZakonczCzynnoscZlecenia(int id_zlecenia, int id_czynnosci, int id_pracownika, string opis)
        {
            try
            {
                lock (lock_obj)
                {
                    int error = 0;
                    string login = ConfigurationManager.AppSettings["login"];
                    string haslo = ConfigurationManager.AppSettings["haslo"];
                    string firma = ConfigurationManager.AppSettings["firma"];
                    string sciezka = ConfigurationManager.AppSettings["optimaloc"];
                    string konString = ConfigurationManager.AppSettings["konstring"];

                    System.Environment.CurrentDirectory = sciezka;
                    CDNBase.Application rApp;
                    CDNBase.ILogin rLogin;
                    CDNBase.AdoSession Sesja;
                    SrsCzynnosc srsCzynnosc;
                    SrsZlecenia srsZlecenia;
                    ISrsZlecenie srsZlecenie;

                    rApp = new CDNBase.Application();
                    try
                    {                        
                        rApp.KonfigConnectStr = konString;

                        rApp.LockApp(1);
                        rLogin = rApp.Login(login, haslo, firma, _ModulSRW: 1);
                        Logger.LogInfo(string.Format("ZakonczCzynnoscZlecenia zalogowano, {0}", DateTime.Now));
                        Sesja = rLogin.CreateSession();

                        srsZlecenia = (SrsZlecenia)Sesja.CreateObject("CDN.SrsZlecenia", null);
                        srsZlecenie = (ISrsZlecenie)srsZlecenia[$"SrZ_SrZId={id_zlecenia}"];

                        srsCzynnosc = srsZlecenie.Czynnosci[$"SrY_SrYId={id_czynnosci}"];

                        srsCzynnosc.Opis = string.Format("{0}, data: {1}", opis, DateTime.Now.ToString());
                        srsCzynnosc.SerwisantId = id_pracownika;
                        srsCzynnosc.Zakonczona = 1;
                        srsCzynnosc.DataWykonania = DateTime.Now;
                        srsCzynnosc.TerminOd = DateTime.Now;
                        srsCzynnosc.TerminDo = DateTime.Now;

                        Sesja.Save();
                        error = srsCzynnosc.ID;
                        Logger.LogInfo(string.Format("ZakonczCzynnoscZlecenia zakonczono czynnosc o id: {0} w zleceniu id:{1}", error, srsZlecenie.ID));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                        error = -1;
                    }
                    finally
                    {
                        rApp.LoginOut();
                        rApp.UnlockApp();
                        rLogin = null;
                        rApp = null;
                        Sesja = null;
                    }
                    return error;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException($"{ex}");
                return -1;
            }
        }
    }
}
