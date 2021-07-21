using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logger
{
    public class Logger
    {
        static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        //private ConfigurationSection config;
        private static object locker = new object();

        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        //public Logger()
        //{
        //    config = new AppConfig().GetConfiguration();
        //}

        public static void LogException(Exception ex)
        {
            log.ErrorFormat("Exception: " + ex.ToString() + (ex.StackTrace ?? String.Empty));
        }
        public static void LogException(string ex)
        {
            log.ErrorFormat("Exception: " + ex);
        }
        public static void LogInfo(string message)
        {
            log.Info(message);
        }


        public static void LogDebug(string message)
        {
            log.Debug(message);
        }
    }
}
