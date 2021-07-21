
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
//using ReportModule;
namespace CasWebService
{
    /// <summary>
    /// Summary description for GetPDF
    /// </summary>
    public class GetPDF : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}