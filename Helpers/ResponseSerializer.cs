using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Helpers
{
    public class ResponseSerializer
    {
        public static string Serialize(object ob)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(ob);
        }
    }
}