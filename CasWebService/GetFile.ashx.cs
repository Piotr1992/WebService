using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

using Helpers;
using Helpers.Logger;

namespace CasWebService
{
    /// <summary>
    /// Summary description for GetFile
    /// </summary>
    public class GetFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string type = context.Request.QueryString["type"];
            Logger.LogInfo("GetFile.ProcessRequest" + type);
            switch (type)
            {
                case "bin":
                    {
                        Logger.LogDebug("GetFile.ProcessRequest.bin");
                        GetBinFile(context);
                    }
                    break;
            }

        }

        private void GetBinFile(HttpContext context)
        {
            try
            {
                string message = "";
                if (LogUsage.Params) message = $"Parameters - none";
                using (var w = new UsageLogger("WebSerwis", MethodBase.GetCurrentMethod(), message ?? ""))
                {
                    int id = 0;
                    if (!string.IsNullOrEmpty(context.Request.QueryString["Id"]) && Int32.TryParse(context.Request.QueryString["Id"], out id))
                    {
                        List<QueryParam> lstPar = new List<QueryParam>();
                        lstPar.Add(new QueryParam("@Id", id));
                        //Logger.LogInfo("Id:"+id.ToString());
                        Logger.LogDebug("get bin file id:" + id.ToString());
                        DataTable dt = DBHelper.RunSqlQueryParam("select * from dbo.b2b_danebinarne(@Id)", "b2b_danebinarne", lstPar);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            byte[] data = (byte[])dt.Rows[0]["DAB_Dane"];
                            //int rozmiar = Convert.ToInt32(dt.Rows[0]["DAB_Rozmiar"]) > 0 ? Convert.ToInt32(dt.Rows[0]["DAB_Rozmiar"]) : data.Length;
                            Logger.LogDebug("Nazwa:" + Convert.ToString(dt.Rows[0]["DAB_Nazwa"]) + " Ext: " + Convert.ToString(dt.Rows[0]["DAB_Rozszerzenie"]) + " Len:" + data.Length.ToString());
                            context.Response.Clear();
                            if (Convert.ToInt32(dt.Rows[0]["DAB_Rozmiar"]) > 0)
                            {
                                data = null;
                                DataCompression.DecompressData((byte[])dt.Rows[0]["DAB_Dane"], out data);
                            }
                            //switch(Convert.ToString(dt.Rows[0]["DAB_Rozszerzenie"]))
                            //{
                            //    case "jpg":
                            //        Logger.LogInfo("image/jpeg");
                            //        context.Response.ContentType = "image/jpeg";
                            //        break;
                            ////    default:
                            //       Logger.LogInfo("application/octet-stream");
                            context.Response.ContentType = "application/octet-stream";
                            //        break;
                            //}

                            context.Response.AddHeader("Content-Disposition", Convert.ToString(dt.Rows[0]["DAB_Nazwa"]) + "." + Convert.ToString(dt.Rows[0]["DAB_Rozszerzenie"]));

                            context.Response.OutputStream.Write(data, 0, data.Length);
                            context.Response.Flush();
                            try
                            {
                                context.Response.End();
                            }
                            catch (ThreadAbortException e)
                            {

                            }
                            catch (Exception ee)
                            {
                                Logger.LogException(ee);
                            }


                            Logger.LogDebug("get bin file End");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(ex.Message);
            }
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