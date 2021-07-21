using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

             var idks = new OptimaOperations.OptimaOperations().NoweZlecenieSerwisowe(1,3,"dkd");
             var id = new OptimaOperations.OptimaOperations().NowaCzynnoscZlecenia(8500,3,346,"mojek3",0);


            var idk = new OptimaOperations.OptimaOperations().ZakonczCzynnoscZlecenia(8500, id, 3, "zamykajek");
//            HttpRequest req=new HttpRequest("sss","sadas","fds");
//            HttpResponse res=new HttpResponse()
//            HttpContext ct = new HttpContext(;
//            DataTable dt = DBHelper.RunSqlQuery(@"SELECT ""KntAdresy"".""KnA_NipE"", ""TraNag"".""TrN_Data2"", ""TraNag"".""TrN_Data3"", ""KntAdresy"".""KnA_Nazwa1"", ""KntAdresy"".""KnA_Nazwa2"", ""KntAdresy"".""KnA_Nazwa3"", ""KntAdresy"".""KnA_KodP"", ""KntAdresy"".""KnA_Miasto"", ""KntAdresy"".""KnA_Ulica"", ""KntAdresy"".""KnA_Adres"", ""KntAdresy"".""KnA_Regon"", ""KnADocelowy"".""KnA_Nazwa1"", ""KnADocelowy"".""KnA_Nazwa2"", ""KnADocelowy"".""KnA_Nazwa3"", ""KnADocelowy"".""KnA_KodP"", ""KnADocelowy"".""KnA_Miasto"", ""KnADocelowy"".""KnA_Ulica"", ""KnADocelowy"".""KnA_Adres"", ""TrNOpisy"".""TnO_TrnTyp"", ""TrNOpisy"".""TnO_Opis"", ""TraNag"".""TrN_SposobDostawy"", ""TraElem"".""TrE_Pozycja"", ""TraElem"".""TrE_TwrNazwa"", ""TwrKarty"".""Twr_Sww"", ""TraElem"".""TrE_Ilosc"", ""TraElem"".""TrE_PrzeliczM"", ""TraElem"".""TrE_PrzeliczL"", ""TraElem"".""TrE_JmZ"", ""TraNag"".""TrN_GIDNumer"", ""TraNag"".""TrN_GIDTyp"", ""OpeKarty"".""Ope_Nazwisko"", ""TraElem"".""TrE_Cena"", ""KntOsoby"".""KnS_Nazwa"", ""KntKarty"".""Knt_FaVATOsw"", ""KntKarty"".""Knt_FAVATData"", ""TraNag"".""TrN_FlagaNB"", ""TraElem"".""TrE_KsiegowaNetto"", ""TraElem"".""TrE_KsiegowaBrutto"", ""TraElem"".""TrE_StawkaPod"", ""TraElem"".""TrE_FlagaVat"", ""TwrJm"".""TwJ_Calkowita"", ""TraNag"".""TrN_ExpoNorm"", ""TraElem"".""TrE_GIDNumer"", ""KnAPlatnik"".""KnA_GIDNumer"", ""PodmiotyView"".""PODV_Nazwa1"", ""PodmiotyView"".""PODV_Nazwa2"", ""PodmiotyView"".""PODV_Nazwa3"", ""PodmiotyView"".""PODV_Kodp"", ""PodmiotyView"".""PODV_Miasto"", ""PodmiotyView"".""PODV_Ulica"", ""PodmiotyView"".""PODV_Adres"", ""PodmiotyView"".""PODV_NipE"", ""KnAPlatnik"".""KnA_Nazwa1"", ""KnAPlatnik"".""KnA_Nazwa2"", ""KnAPlatnik"".""KnA_Nazwa3"", ""KnAPlatnik"".""KnA_KodP"", ""KnAPlatnik"".""KnA_Miasto"", ""KnAPlatnik"".""KnA_Ulica"", ""KnAPlatnik"".""KnA_Adres"", ""KnAPlatnik"".""KnA_NipE"", ""KnAPlatnik"".""KnA_Regon"", ""TraNag"".""TrN_KnANumer"", ""TraNag"".""TrN_AdPNumer"", ""KntAdresy"".""KnA_Nip"", ""KntAdresy"".""KnA_NipPrefiks"", ""PodmiotyView"".""PODV_NipPrefiks"", ""KnAPlatnik"".""KnA_Nip"", ""KnAPlatnik"".""KnA_NipPrefiks"", ""TwrKarty"".""Twr_Kod"", ""TraNag"".""TrN_FrsID"", ""TraElem"".""TrE_KGOJednostkowe"", ""TraElem"".""TrE_PrecyzjaCeny"", ""TraElem"".""TrE_KursM"", ""TraElem"".""TrE_KursL"", ""SpiNag"".""TrN_GIDTyp"", ""SpiNag"".""TrN_GIDNumer"", ""TraNag"".""TrN_FrmNumer"", ""TraElem"".""TrE_Zlom"", CDN.NumerDokumentu(""TraNag"".""TrN_GIDTyp"",""TraNag"".""TrN_SpiTyp"",""TraNag"".""TrN_TrNTyp"",""TraNag"".""TrN_TrNNumer"",""TraNag"".""TrN_TrNRok"",""TraNag"".""TrN_TrNSeria"",""TraNag"".""TrN_TrNMiesiac""), CDN.DokGenerujKodKreskowy(""TraNag"".""TrN_GIDTyp"", ""TraNag"".""TrN_GIDNumer"") FROM     (((((((((((""CDN"".""TraNag"" ""TraNag"" INNER JOIN ""CDN"".""TraNag"" ""SpiNag"" ON ""TraNag"".""TrN_GIDNumer""=""SpiNag"".""TrN_SpiNumer"") INNER JOIN ""CDN"".""KntAdresy"" ""KntAdresy"" ON ""TraNag"".""TrN_KnANumer""=""KntAdresy"".""KnA_GIDNumer"") INNER JOIN ""CDN"".""OpeKarty"" ""OpeKarty"" ON ""TraNag"".""TrN_OpeNumerW""=""OpeKarty"".""Ope_GIDNumer"") INNER JOIN ""CDN"".""KntKarty"" ""KntKarty"" ON (""TraNag"".""TrN_KntTyp""=""KntKarty"".""Knt_GIDTyp"") AND (""TraNag"".""TrN_KntNumer""=""KntKarty"".""Knt_GIDNumer"")) INNER JOIN ""CDN"".""PodmiotyView"" ""PodmiotyView"" ON (""TraNag"".""TrN_KnPTyp""=""PodmiotyView"".""PODV_GIDTyp"") AND (""TraNag"".""TrN_KnPNumer""=""PodmiotyView"".""PODV_GIDNumer"")) LEFT OUTER JOIN ""CDN"".""KntAdresy"" ""KnADocelowy"" ON ""TraNag"".""TrN_AdWNumer""=""KnADocelowy"".""KnA_GIDNumer"") LEFT OUTER JOIN ""CDN"".""TrNOpisy"" ""TrNOpisy"" ON ""TraNag"".""TrN_GIDNumer""=""TrNOpisy"".""TnO_TrnNumer"") LEFT OUTER JOIN ""CDN"".""KntOsoby"" ""KntOsoby"" ON (""TraNag"".""TrN_OdoNumer""=""KntOsoby"".""KnS_KntNumer"") AND (""TraNag"".""TrN_OdoLp""=""KntOsoby"".""KnS_KntLp"")) LEFT OUTER JOIN ""CDN"".""KntAdresy"" ""KnAPlatnik"" ON ""TraNag"".""TrN_AdPNumer""=""KnAPlatnik"".""KnA_GIDNumer"") LEFT OUTER JOIN ""CDN"".""TraElem"" ""TraElem"" ON ""SpiNag"".""TrN_GIDNumer""=""TraElem"".""TrE_GIDNumer"") LEFT OUTER JOIN ""CDN"".""TwrKarty"" ""TwrKarty"" ON ""TraElem"".""TrE_TwrNumer""=""TwrKarty"".""Twr_GIDNumer"") LEFT OUTER JOIN ""CDN"".""TwrJm"" ""TwrJm"" ON (""TraElem"".""TrE_TwrNumer""=""TwrJm"".""TwJ_TwrNumer"") AND (""TraElem"".""TrE_JmZ""=""TwrJm"".""TwJ_JmZ"")
//
//WHERE
//   ((TraNag.TrN_GIDTyp=2033 AND TraNag.TrN_GIDNumer=93512))  ORDER BY ""TraElem"".""TrE_Pozycja""", "FS");
//            //context.Response.ContentType = "text/plain";
//            ReportModule.ReportModule rpt = new ReportModule.ReportModule();
//            rpt.GenerateReport("~/Reports/FS.RPT", dt, "FS", context.Response);
        }
    }
}
