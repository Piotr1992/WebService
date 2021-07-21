using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cdn_api;
using CDNOperations;

using Helpers;
using System.Reflection;
using System.Net;
using System.IO;

namespace TestCDNOperations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int sesjaId = 0;
        private void cbObject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {

            //XLDokumentZamElemInfo_20193 zam = new XLDokumentZamElemInfo_20193();
            //zam.Ilosc = "1";
            //string str1 = CDNSerializerDeserializer.Serialize(zam);
            //tbSerialized.Text = str1;

            //XLDokumentZamNagInfo_20193 doc = new XLDokumentZamNagInfo_20193();
            //doc.Wersja = 15;
            //doc.Typ = 6;
            //doc.KntNumer = 134;
            //string str=CDNSerializerDeserializer.Serialize(doc);
            //tbSerialized.Text = str;
            XLAdresInfo_20193 adres = new XLAdresInfo_20193();
            adres.Adres = "string";
            adres.Akronim = "string";
            adres.Bank = "string";
            adres.EMail = "string";
            adres.Fax = "string";
            adres.Gmina = "string";
            adres.KodP = "string";
            adres.KontoDostawcy = "string";
            adres.KontoOdbiorcy = "string";
            adres.Kraj = "string";
            adres.Miasto = "string";
            adres.Modem = "string";
            adres.Nazwa1 = "string";
            adres.Nazwa2 = "string";
            adres.Nazwa3 = "string";
            adres.NipE = "string";
            adres.NipPrefiks = "string";
            adres.NrRachunku = "string";
            adres.Odleglosc = "string";
            adres.Pesel = "string";
            adres.Powiat = "string";
            adres.Regon = "string";
            adres.Telefon1 = "string";
            adres.Telefon2 = "string";
            adres.Telex = "string";
            adres.Ulica = "string";
            adres.Wojewodztwo = "string";
            
            string str1 = CDNSerializerDeserializer.Serialize(adres);
            tbSerialized.Text = str1;
        }

        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            XLDokumentZamNagInfo_20193 doc = new XLDokumentZamNagInfo_20193();
            doc = (XLDokumentZamNagInfo_20193)CDNSerializerDeserializer.Deserialize(tbSerialized.Text, doc);
            XLDokumentZamElemInfo_20193 elem = new XLDokumentZamElemInfo_20193();
            List<XLDokumentZamElemInfo_20193> lst = CDNSerializerDeserializer.DeserializeList(tbSerialized.Text, elem).Cast<XLDokumentZamElemInfo_20193>().ToList();


        }

        private void bntBuildZam_Click(object sender, EventArgs e)
        {
            string error = "";
            CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations(sesjaId);
            sesjaId = cdn.GetSesja();
            int docId = cdn.NowyDokumentZam(6, tbSerialized.Text, ref error);
            string numer = DBHelper.GetNumerDokumentuTRN(docId);
            tbError.Text = error;
        }

        private void btnOpenZam_Click(object sender, EventArgs e)
        {
            CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations(sesjaId);
            sesjaId = cdn.GetSesja();
            //cdn.OtworzDokumentZam(1, 53077, ref error);

        }

        private void Login_Click(object sender, EventArgs e)
        {
            CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations();
            string error = "";
            tbError.Text = cdn.Login(ref error).ToString();
            tbError.Text = tbError.Text+ " sesja "+cdn.GetSesja().ToString();
        }

        private void tbSerialized_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbError_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbDeserialized_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddAdres_Click(object sender, EventArgs e)
        {
            string error = "";
            CDNOperations.CDNOperations cdn = new CDNOperations.CDNOperations(sesjaId);
            sesjaId = cdn.GetSesja();
            int adrId = cdn.NowyAdres(tbDeserialized.Text, ref error);

            
        }

        private void btnTestService_Click(object sender, EventArgs e)
        {
            try
            {
                WSProxy.WSProxy proxy = new WSProxy.WSProxy();
                Dictionary<string, string> dictPar = new Dictionary<string, string>();
                dictPar.Add("xml", tbDeserialized.Text);
                string results = proxy.CallWebMethod(tbDeserialized.Text, tbSerialized.Text, dictPar);
                tbError.Text = results;
            }
            catch(Exception ex)
            {
                tbError.Text = ex.Message + ex.StackTrace;
            }
            //HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create("http://localhost:3453/WebService.asmx/b2b_danebinarne");
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XLDokumentNagInfo_20193 doc = new XLDokumentNagInfo_20193();
            doc.Akronim = "aaa";
            doc.Akwizytor="aaa";
            doc.Cecha = "aaa";
            doc.Docelowy = "aaa";
            doc.DokumentObcy = "aaa";
            doc.IncotermsMiejsce = "aaa";
            doc.IncotermsSymbol = "aaa";
            doc.KodRodzajuTransakcji = "aaa";
            doc.KodRodzajuTransportu = "aaa";
            doc.Kraj = "aaa";
            doc.MagazynD = "aaa";
            doc.MagazynZ = "aaa";
            doc.NB = "aaa";
            doc.Opis = "aaa";
            doc.Osoba = "aaa";
            doc.Platnik = "aaa";
            doc.PrzyczynaKorekty = "aaa";
            doc.PrzyczynaZW = "aaa";
            doc.Rejestr = "aaa";
            doc.Seria = "aaa";
            doc.SposobDst = "aaa";
            doc.URL = "aaa";
            doc.Waluta = "aaa";
            
            string str1 = CDNSerializerDeserializer.Serialize(doc);
            tbSerialized.Text = str1;
            //XLDokumentElemInfo_20193 elem = new XLDokumentElemInfo_20193();
            //List<XLDokumentZamElemInfo_20193> lst = CDNSerializerDeserializer.DeserializeList(tbSerialized.Text, elem).Cast<XLDokumentElemInfo_20193>().ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XLDokumentElemInfo_20193 elem = new XLDokumentElemInfo_20193();
            elem.BudzetPrmID = "aaa";
            elem.BudzetWartosc = "aaa";
            elem.Cecha = "aaa";
            elem.Cecha2 = "aaa";
            elem.Cena = "aaa";
            elem.CenaP = "aaa";
            elem.CenaPrzed = "aaa";
            elem.Ilosc = "aaa";
            elem.IloscPrzed = "aaa";
            elem.JmZ = "aaa";
            elem.KGO = "aaa";
            elem.Kraj = "aaa";
            elem.Magazyn = "aaa";
            elem.Opis = "aaa";
            elem.PCN = "aaa";
            elem.PrzyczynaKorekty = "aaa";
            elem.TowarEAN = "aaa";
            elem.TowarKod = "aaa";
            elem.TowarNazwa = "aaa";
            elem.Vat = "aaa";
            elem.VatPrzed = "aaa";
            elem.Waluta = "aaa";
            elem.Wartosc = "aaa";
            elem.WartoscPrzed = "aaa";
            elem.WartoscR = "aaa";

            string str1 = CDNSerializerDeserializer.Serialize(elem);
            tbSerialized.Text = str1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
