using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CDNOperations
{
    public class XmlHelper
    {
        public static string SetElementValue(string xml, string path, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            foreach(XmlNode node in  xmlDoc.SelectNodes(path))
            {
                node.InnerText = value;
            }
            return xmlDoc.OuterXml;
        }
    }
}
