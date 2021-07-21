using Helpers;
using OptimaOperations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace ChmielewskiWebService
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_testDateTime()
        {
            var datka = new OptimaOperations.OptimaOperations().test_datetime();
                return ResponseSerializer.Serialize(datka);
            }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_CzynnosciZlecen()
        {
            DataTable dt = DBHelper.RunSqlQuery("select * from dbo.bmp_srw_CzynnosciZlecen", "bmp_srw_CzynnosciZlecen");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_Pracownicy()
        {
            DataTable dt = DBHelper.RunSqlQuery("select * from dbo.bmp_srw_Pracownicy", "bmp_srw_Pracownicy");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_Towary()
        {
            DataTable dt = DBHelper.RunSqlQuery("select * from dbo.bmp_srw_Towary", " bmp_srw_Towary");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_Urzadzenia()
        {
            DataTable dt = DBHelper.RunSqlQuery("select * from dbo.bmp_srw_Urzadzenia", "bmp_srw_Urzadzenia");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_Zlecenia()
        {
            DataTable dt = DBHelper.RunSqlQuery("select * from dbo.bmp_srw_Zlecenia", "bmp_srw_Zlecenia");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_Czesci()
        {
            DataTable dt = DBHelper.RunSqlQuery("select * from dbo.bmp_srw_Czesci", "bmp_srw_Czesci");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_NoweZlecenieSerwisowe(int id_urzadzenia,int id_pracownika, string opis_zlecenia)
        {
            var id = new OptimaOperations.OptimaOperations().NoweZlecenieSerwisowe(id_urzadzenia,id_pracownika, opis_zlecenia);
            
            DataTable dt = DBHelper.RunSqlQuery($"select * from dbo.bmp_srw_Zlecenia where ZlecenieId = {id}", "bmp_srw_Zlecenia");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_NowaCzynnoscZlecenia(int id_zlecenia, int id_pracownika, int id_kodczynnosci, string kodczynnosci_opis,int zrealizowano)
        {
            var id = new OptimaOperations.OptimaOperations().NowaCzynnoscZlecenia(id_zlecenia,id_pracownika,id_kodczynnosci,kodczynnosci_opis,zrealizowano);
            
            DataTable dt = DBHelper.RunSqlQuery($"select * from dbo.bmp_srw_CzynnosciZlecen where ZlecenieId = {id_zlecenia} and CzynnoscId={id}", "bmp_srw_CzynnosciZlecen");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_ZakonczCzynnoscZlecenia(int id_zlecenia, int id_czynnosc, int id_pracownika, string kodczynnosci_opis)
        {
            var id = new OptimaOperations.OptimaOperations().ZakonczCzynnoscZlecenia(id_zlecenia,id_czynnosc,id_pracownika,kodczynnosci_opis);
            DataTable dt = DBHelper.RunSqlQuery($"select * from dbo.bmp_srw_CzynnosciZlecen where ZlecenieId = {id_zlecenia} and CzynnoscId={id_czynnosc}", "bmp_srw_CzynnosciZlecen");
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_DodajSrcCzesci(
            int SrC_SrZId,
            int SrC_TwrId,
            int SrC_SerwisantId,
            decimal SrC_Ilosc,
            int SrC_KatID,
            int SrC_MagId,
            string SrC_Opis
         )
        {
            List<QueryParam> procParams = new List<QueryParam>();
            procParams.Add(new QueryParam("@SrC_SrZId", SrC_SrZId));
            procParams.Add(new QueryParam("@SrC_TwrId", SrC_TwrId));
            procParams.Add(new QueryParam("@SrC_SerwisantId", SrC_SerwisantId));
            procParams.Add(new QueryParam("@SrC_Ilosc", SrC_Ilosc));
            procParams.Add(new QueryParam("@SrC_KatID", SrC_KatID));
            procParams.Add(new QueryParam("@SrC_MagId", SrC_MagId));
            procParams.Add(new QueryParam("@SrC_Opis", SrC_Opis));
            int result = 0;
            Int32.TryParse(Convert.ToString(DBHelper.RunScalarSqlProcParam("bmp_srw_DodajSrcCzesci", procParams)), out result);

            return ResponseSerializer.Serialize(result);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_ZamknijSrsZlecenia(
                    int SrZ_SrZId,
                      string SrZ_Opis
         )
        {
            List<QueryParam> procParams = new List<QueryParam>();
            procParams.Add(new QueryParam("@SrZ_SrZId", SrZ_SrZId));
            procParams.Add(new QueryParam("@SrZ_Opis", SrZ_Opis));

            if (DBHelper.ExecSqlProcParam("bmp_srw_ZamknijSrsZlecenia", "bmp_srw_ZamknijSrsZlecenia", procParams))
            {
                return ResponseSerializer.Serialize(SrZ_SrZId);
            }
            else
            {
                return ResponseSerializer.Serialize(0);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_ZapiszOpisZlecenia(
                    int SrZ_SrZId,
                      string SrZ_Opis
         )
        {
            List<QueryParam> procParams = new List<QueryParam>();
            procParams.Add(new QueryParam("@SrZ_SrZId", SrZ_SrZId));
            procParams.Add(new QueryParam("@SrZ_Opis", SrZ_Opis));

            if (DBHelper.ExecSqlProcParam("bmp_srw_ZapiszOpisZlecenia", "bmp_srw_ZapiszOpisZlecenia", procParams))
            {
                return ResponseSerializer.Serialize(SrZ_SrZId);
            }
            else
            {
                return ResponseSerializer.Serialize(0);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_CechyUrzadzenia(
                    int sru_id
         )
        {
            List<QueryParam> procParams = new List<QueryParam>();
            procParams.Add(new QueryParam("@sru_id",sru_id));

           
            DataTable dt = DBHelper.RunSqlProcParam("bmp_srw_CechyUrzadzenia", "bmp_srw_CechyUrzadzenia", procParams);
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bmp_srw_SrsCzesci(
                    int SrC_SrZId
         )
        {
            List<QueryParam> procParams = new List<QueryParam>();
            procParams.Add(new QueryParam("@SrC_SrZId", SrC_SrZId));
            DataTable dt = DBHelper.RunSqlProcParam("bmp_srw_SrsCzesci", "bmp_srw_SrsCzesci", procParams);
            if (dt != null)
            {
                return DataTableHelper.GetJson(dt);
            }
            return null;

        }

        
    }
}
