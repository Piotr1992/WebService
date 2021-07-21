using Helpers;
using Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using BmpWebServiceBackend.Entities;

namespace BmpWebServiceBackend
{
    public class WSBReklamacje
    {
        public static string B2B_SprzedazKontrahentaSN(int kntnumer, string searchString, int searchType)
        {
            try
            {
                List<QueryParam> lstPar = new List<QueryParam>
                {
                    new QueryParam("@kntnumer", kntnumer),
                    new QueryParam("@searchString", searchString),
                    new QueryParam("@searchType",searchType)
                };
                DataTable dt = DBHelper.RunSqlProcParam("dbo.b2b_spSprzedazKontrahentaSN", "b2b", lstPar);
                if (dt != null)
                {
                    return DataTableHelper.GetJson(dt);
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
  
        public static string B2B_ZadaniaReklamacji()
        {
            try
            {
                return DBHelper.RunSqlQuery("select * from dbo.b2b_vReklamacjeSlownikZadan", "ZadaniaReklamacji")?.GetJson();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public static string B2B_ReklamacjeAtrybutySlowniki()
        {
            try
            {
                return DBHelper.RunSqlQuery("select * from dbo.b2b_vReklamacjeAtrybutySlowniki", "ZadaniaReklamacji")?.GetJson();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        
        public static string B2B_NowaReklamacja(string xml, byte[] zal=null, string zalNazwa="", string zalRozszerzenie="")
        {
            try
            {
                byte[] zalC = ErpCompress.Compress(zal);
                StringReader theReader = new StringReader(xml);
                DataSet ds = new DataSet();
                ds.ReadXml(theReader);

                if (!ds.Tables[1].Columns.Contains("AtrybutRodzajZgloszenia")) { ds.Tables[1].Columns.Add("AtrybutRodzajZgloszenia"); };
                List<SqlParameter> lstPar = new List<SqlParameter>()
                {
                    new SqlParameter("@nag",SqlDbType.Structured){TypeName = "dbo.b2b_udttReklamacja",Value=ds.Tables[0]},
                    new SqlParameter("@elem",SqlDbType.Structured){TypeName = "dbo.b2b_udttReklamacjaElementy",Value=ds.Tables[1]},
                    new SqlParameter("@zal",SqlDbType.Image){Value=zalC},
                    new SqlParameter("@zalNazwa",SqlDbType.VarChar){Value=zalNazwa},
                    new SqlParameter("@zalRozszerzenie",SqlDbType.VarChar){Value=zalRozszerzenie},
                    new SqlParameter("@zalRozmiar",SqlDbType.Int){Value=zal.Length}
                };
                var dt = DBHelper.RunSqlProcParam("dbo.b2b_spNowaReklamacja", "resp", lstPar);
                return dt.GetJson();
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.Message);
            }
            finally
            {
                  
            }
            return null;       
        }
      
    }
}
