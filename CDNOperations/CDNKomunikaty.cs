using cdn_api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDNOperations
{
    public class CDNKomunikaty
    {
        public static void Komunikaty_XLLogin(int numerBledu, ref string rezultat)
        {
            if (numerBledu == 0) rezultat = "";
            string res = string.Empty;
            switch (numerBledu)
            {
                case -8: res = "nie podano nazwy bazy"; break;
                case -7: res = "baza niezarejestrowana w systemie"; break;
                case -6: res = "nie podano hasła lub brak operatora"; break;
                case -5: res = "nieprawidłowe hasło"; break;
                case -4: res = "konto operatora zablokowane"; break;
                case -3: res = "nie podano nazwy programu (pole ProgramID)"; break;
                case -2: res = "błąd otwarcia pliku tekstowego, do którego mają być zapisywane komunikaty. Nie znaleziono ścieżki lub nazwa pliku jest nieprawidłowa."; break;
                case -1: res = "podano niepoprawną wersję API"; break;
                case 1: res = " inicjalizacja nie powiodła się"; break;
                case 2: res = " występuje w przypadku, gdy istnieje już jedna instancja programu i nastąpi ponowne logowanie (z tego samego komputera i na tego samego operatora)"; break;
                case 3: res = " występuje w przypadku, gdy istnieje już jedna instancja programu i nastąpi ponowne logowanie z innego komputera i na tego samego operatora, ale operator nie posiada prawa do wielokrotnego logowania"; break;
                case 5: res = " występuje przy pracy terminalowej w przypadku, gdy operator nie ma prawa do wielokrotnego logowania i na pytanie czy usunąć istniejące sesje terminalowe wybrano odpowiedź ‘Nie’."; break;
                case 61: res = "błąd zakładania nowej sesji"; break;
                default: res = "błąd logowanie nieokreślony " + "(" + res.ToString() + ")"; break;
            }
            rezultat = res;
        }
        public static void Komunikaty_XLDodajAtrybut(int numerBledu, ref string rezultat)
        {
            if (numerBledu == 0) rezultat = "";
            string res = string.Empty;
            switch (numerBledu)
            {
                case -1: res = " brak sesji"; break;
                case 2: res = " błąd przy zakładaniu logout"; break;
                case 3: res = " nie znaleziono obiektu"; break;
                case 4: res = " nie znalezniono klasy atrybutu"; break;
                case 5: res = " klasa nie przypisana do definicji obiektu"; break;
                case 6: res = " atrybut juz istnieje w kolejce"; break;
                case 7: res = " błąd ADO Connection"; break;
                case 9: res = " błąd ADO"; break;
                case 8: res = " brak zdefiniowanego obiektu"; break;
                default: res = "błąd logowanie nieokreślony " + "(" + res.ToString() + ")"; break;
            }
            rezultat = res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numerFunkcji"></param>
        /// <param name="numerBledu"></param>
        /// <param name="rezultat"></param>
        /// <param name="wersja"></param>
        public static void Komunikaty_GlowneFunkcje(int numerFunkcji, int numerBledu, ref string rezultat,int wersja)
        {
            int res = -1;
            string tresc = string.Empty;
            try
            {
                XLKomunikatInfo_20193 komunikat = new XLKomunikatInfo_20193();
                komunikat.Wersja = wersja;
                komunikat.Tryb = 2;
                komunikat.Funkcja = numerFunkcji;
                komunikat.Blad = numerBledu;

                res = cdn_api.cdn_api.XLOpisBledu(komunikat);
                if (res != 0)
                {
                    tresc = "Nie udało sie pobrać treści komunikatu dla błędu";
                }
                else
                {
                    tresc = komunikat.OpisBledu;
                }
            }
            catch (Exception ex)
            {
                tresc = "Nie udało sie pobrać treści komunikatu dla błędu, błąd funnkcji XLOpisBledu, " + ex.Message;
            }
            rezultat = tresc;
        }
    }
}
