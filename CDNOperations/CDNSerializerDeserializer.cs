using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CDNOperations
{
    public static class CDNSerializerDeserializer
    {
        public static string Serialize(Object serializeObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   
            XmlSerializer xmlSerializer = new XmlSerializer(serializeObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, serializeObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }

        }
        public static Object Deserialize(string xml,Object deserializeObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(deserializeObject.GetType());
            XDocument doc = XDocument.Parse(xml);
            string tagName = deserializeObject.GetType().Name;
            deserializeObject = oXmlSerializer.Deserialize(new StringReader(doc.Root.Elements(tagName).First().ToString()));
            return deserializeObject;
        }
        
        public static List<Object> DeserializeList(string xml,Object deserializeObject)
        {
            List<Object> lst=new List<object>();
            string tagName = deserializeObject.GetType().Name;
            XDocument doc = XDocument.Parse(xml);

            XmlSerializer oXmlSerializer = new XmlSerializer(deserializeObject.GetType());
            foreach (var tag in doc.Root.Elements(tagName).ToList())
            {
                 lst.Add(oXmlSerializer.Deserialize(new StringReader(tag.ToString())));
            }
            return lst;
        }
     
    }
}
