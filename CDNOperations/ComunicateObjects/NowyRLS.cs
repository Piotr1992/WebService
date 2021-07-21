using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CDNOperations.ComunicateObjects
{
    [Serializable]
    public class NowyRLS
    {
        public string DokumentObcy { get; set; } // -- numer dokumentu reklamacji u kontrahenta
        //public string DokNumer { get; set; }
        public int KntNumer { get; set; }// -- kontrahent
        public int TwrNumer { get; set; }
        public decimal Ilosc { get; set; } //        -- ilość
        public string Przyczyna { get; set; } //  -- przyczyna
        public int Zadanie { get; set; }// -- identyfikator żądania reklamującego
        public string Opis { get; set; }// -- informacje dodatkowe
    }
}
