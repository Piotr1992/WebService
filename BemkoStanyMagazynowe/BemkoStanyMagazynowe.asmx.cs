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
using System.Web.Services;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BemkoStanyMagazynowe
{
    /// <summary>
    /// Summary description for BemkoStanyMagazynowe
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class BemkoStanyMagazynowe : System.Web.Services.WebService
    {
        [WebMethod]
        public string pobierzStanMagazynowyProducenta(string pytanie)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
                pytanie pytanieOb = new pytanie();
                XDocument doc = XDocument.Parse(pytanie);
                string tagName = pytanieOb.GetType().Name;
                //The StringReader will be the stream holder for the existing XML file 
                XmlSerializer oXmlSerializer = new XmlSerializer(pytanieOb.GetType());
                if (doc.Root.Name == tagName)
                {
                    pytanieOb = (pytanie)oXmlSerializer.Deserialize(new StringReader(doc.Root.ToString()));
                }
                else
                {
                    pytanieOb = (pytanie)oXmlSerializer.Deserialize(new StringReader(doc.Root.Elements(tagName).First().ToString()));
                }


                
                
               
                bool valid = false;

                using (PrincipalContext context = new PrincipalContext(EnumHelper.ParseEnum<ContextType>(Settings.Default.loginContext)))
                {

                    valid = context.ValidateCredentials(pytanieOb.konto.login, pytanieOb.konto.haslo);
                }
               // valid = true;
                if (valid)
                {
                    odpowiedz odpowiedz = pobierzStanMagazynowy.Pobierz(pytanieOb);
               
                    XmlSerializer xs = new XmlSerializer(typeof(odpowiedz));
                    StringWriter sw = new StringWriter();
                    xs.Serialize(sw, odpowiedz);
                    return sw.ToString();
                }
                else
                {
                    Logger.Logger.LogInfo(string.Format("Błąd logowania dla użytkownika:{0},hasło:{1},loginContext:{2}", pytanieOb.konto.login, pytanieOb.konto.haslo, Settings.Default.loginContext));
                    Context.Response.Status = "403 Forbidden";
                    Context.Response.StatusCode = 403;
                    Context.Response.End();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }
    }
}
