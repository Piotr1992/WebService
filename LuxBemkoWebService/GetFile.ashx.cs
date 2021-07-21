using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LuxBemkoWebService
{
    /// <summary>
    /// Summary description for GetFile
    /// </summary>
    public class GetFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string type = context.Request.QueryString["type"];
            Logger.Logger.LogInfo("GetFile.ProcessRequest" + type);
            switch (type)
            {
                case "bin":
                    {
                        Logger.Logger.LogDebug("GetFile.ProcessRequest.bin");
                        GetBinFile(context);
                    }
                    break;
            }

        }

        private void GetBinFile(HttpContext context)
        {
            try
            {
                int id = 0;
                if (!string.IsNullOrEmpty(context.Request.QueryString["Id"]) && Int32.TryParse(context.Request.QueryString["Id"], out id))
                {
                    List<QueryParam> lstPar = new List<QueryParam>();
                    lstPar.Add(new QueryParam("@Id", id));
                    //Logger.Logger.LogInfo("Id:"+id.ToString());
                    Logger.Logger.LogDebug("id:" + id.ToString());
                    DataTable dt = DBHelper.RunSqlQueryParam("select * from dbo.b2b_danebinarne(@Id)", "b2b_danebinarne", lstPar);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        byte[] data = (byte[])dt.Rows[0]["DAB_Dane"];
                        //int rozmiar = Convert.ToInt32(dt.Rows[0]["DAB_Rozmiar"]) > 0 ? Convert.ToInt32(dt.Rows[0]["DAB_Rozmiar"]) : data.Length;
                        Logger.Logger.LogDebug("Nazwa:" + Convert.ToString(dt.Rows[0]["DAB_Nazwa"]) + " Ext: " + Convert.ToString(dt.Rows[0]["DAB_Rozszerzenie"]) + " Len:" + data.Length.ToString());
                        context.Response.Clear();
                        if (Convert.ToInt32(dt.Rows[0]["DAB_Rozmiar"]) > 0)
                        {
                            data = null;
                            DataCompression.DecompressData((byte[])dt.Rows[0]["DAB_Dane"], out data);
                        }
                        //switch(Convert.ToString(dt.Rows[0]["DAB_Rozszerzenie"]))
                        //{
                        //    case "jpg":
                        //        Logger.Logger.LogInfo("image/jpeg");
                        //        context.Response.ContentType = "image/jpeg";
                        //        break;
                        ////    default:
                        //       Logger.Logger.LogInfo("application/octet-stream");
                        context.Response.ContentType = "application/octet-stream";
                        //        break;
                        //}

                        context.Response.AddHeader("Content-Disposition", Convert.ToString(dt.Rows[0]["DAB_Nazwa"]) + "." + Convert.ToString(dt.Rows[0]["DAB_Rozszerzenie"]));

                        context.Response.OutputStream.Write(data, 0, data.Length);
                        context.Response.Flush();
                        context.Response.End();
                        Logger.Logger.LogDebug("dt End");
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(ex.Message);
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}