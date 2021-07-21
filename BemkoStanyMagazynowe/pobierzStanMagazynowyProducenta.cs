using BemkoStanyMagazynowe.Properties;
using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BemkoStanyMagazynowe
{
    public class pobierzStanMagazynowy
    {
       
        public static odpowiedz Pobierz(pytanie pytanie)
        {
            

            List<QueryParam> lstPar = new List<QueryParam>();
            StringBuilder sbQuery = new StringBuilder();
            //sbQuery.Append(Settings.Default.stanyMagazynoweQuery);
            for (int i = 0; i < pytanie.towary.Length; i++)
            {
                string paramName = string.Format("@twr_kod{0}", i);
                lstPar.Add(new QueryParam(paramName, pytanie.towary[i].symbol));
                if (sbQuery.Length > 0)
                {
                    sbQuery.Append(",");
                }
                sbQuery.Append(paramName);
            }
            string query = string.Format("{0} and twr_kod in ({1})", Settings.Default.stanyMagazynoweQuery, sbQuery.ToString());
            DataTable results = DBHelper.RunSqlQueryParam(query, "stanymagazynowe", lstPar);

            odpowiedz odpowiedz = new odpowiedz();
            odpowiedz.wersja = Settings.Default.wersja;
            odpowiedz.znacznik = DateTime.Now;
            //odpowiedz.znacznikSpecified = true;
            List<towarOdpowiedzTyp> lst = new List<towarOdpowiedzTyp>();
            if (results != null)
            {
                for (int i = 0; i < pytanie.towary.Length; i++)
                {
                    towarPytanieTyp twrPyt = pytanie.towary[i];
                    DataRow[] foundRows = results.Select(string.Format("Symbol='{0}'", twrPyt.symbol));

                    if (foundRows == null || foundRows.Length == 0)
                    {
                        //4)	Odpowiedź numer cztery - towar nie istnieje (to samo znaczenie będzie miało, gdy w odpowiedzi w ogóle nie pojawi się pytany symbol).
                        lst.Add(new towarOdpowiedzTyp()
                            {
                                symbol = twrPyt.symbol,
                                ilosc = -1,
                                iloscSpecified=true
                            });
                    }
                    else
                    {
                        DataRow row = foundRows[0];
                        towarOdpowiedzTyp twrOdp = new towarOdpowiedzTyp()
                        {
                            symbol = twrPyt.symbol
                        };
                        if (Convert.ToDouble(row["Stan"]) > twrPyt.ilosc)
                        {
                            //1)	Odpowiedź numer jeden - towar istnieje, na magazynie jest przynajmniej 5 sztuk.
                            twrOdp.ilosc = twrPyt.ilosc;
                            twrOdp.iloscSpecified = true;
                            //twr.iloscSpecified = true;
                        }
                        else
                        {
                            //2)	Odpowiedź numer dwa - towar istnieje, ale ilość sztuk o jaką było pytanie nie jest dostępna.
                            //twr.iloscSpecified = false;
                        }
                        lst.Add(twrOdp);
                    }

                }
            }
            odpowiedz.towary = lst.ToArray();
            return odpowiedz;
        }
    }
}