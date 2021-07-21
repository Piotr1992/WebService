using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JanexWebService
{
    public class ConfigHelper
    {
        public static string GetProperty(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}