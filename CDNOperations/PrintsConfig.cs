using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CDNOperations
{
    public class PrintsConfig
    {
        private static object locker = new object();
        private static Prints config=null;
        public static Prints Conf
        {
            get
            {
                lock (locker)
                {
                    if (config == null)
                    {

                        config = (Prints)ConfigurationManager.GetSection(@"prints");
                    }
                    return config;
                }
            }
        }

    }

    public class ConfigHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            XmlSerializer configSerializer = new XmlSerializer(typeof(Prints));
            object config = configSerializer.Deserialize(new XmlNodeReader(section));
            return config;
        }

        #endregion
    }
    [XmlRoot("prints")]
    [Serializable]
    public class Prints
    {
        [XmlElement(ElementName = "print")]
        public List<Print> prints { get; set; }

    }
    [XmlRoot("print")]
    public class Print
    {
        [XmlAttribute("nazwa")]
        public string nazwa;
        [XmlAttribute("zrodlo")]
        public string zrodlo;
        [XmlAttribute("wydruk")]
        public string wydruk;
        [XmlAttribute("format")]
        public string format;

    }
}
