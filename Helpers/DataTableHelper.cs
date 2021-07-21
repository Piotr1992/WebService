using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Helpers
{
    public static class DataTableHelper
    {
        public static string GetSchema(DataTable dt)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXmlSchema(sw);
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return "";
            }
        }
        public static string GetXml(DataTable dt)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return "";
            }
        }
        public static string GetJson(this DataTable dt)
        {
            
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName.Trim(), dr[col]);
                }
                rows.Add(row);
            }
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Serialize(rows);
        }
    }
}