using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDNOperations
{
    static class XLError
    {
        public static string XLDodajAtrybut(int er)
        {
            var d = new Dictionary<int, string>()
            {
                { -1,"brak sesji" },
                { 2,"błąd przy zakładaniu logout" },
                { 3,"nie znaleziono obiektu" },
                { 4,"nie znalezniono klasy atrybutu" },
                { 5,"klasa nieprzypisana do definicji obiektu" },
                { 6 ,"atrybut juz istnieje w kolejce" },
                { 7 ,"błąd ADO Connection" },
                { 8 ,"błąd ADO" },
                { 9 ,"brak zdefiniowanego obiektu" }
            };
            if (d.ContainsKey(er)) return d[er];
            return "";
        }
    }
}
