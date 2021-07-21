using BemkoStanyMagazynoweTest.ServiceReference2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BemkoStanyMagazynoweTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceReference2.BemkoStanyMagazynoweSoapClient cl = new ServiceReference2.BemkoStanyMagazynoweSoapClient();

            string xml = @"<pytanie>
<dokument>
<wersja>1.0.0</wersja>
<znacznik>2011-03-30T09:26:23+02:00</znacznik>
</dokument>
<konto>
<login>misiu_12</login>
<haslo>tasia</haslo>
</konto>
<towary>
                               <towar symbol=""D10-J118-300"" ilosc=""3.00000"" />
                               <towar symbol=""SYMBOL_2"" ilosc=""10.00000"" />
                               <towar symbol=""D15-JDG9-50C"" ilosc=""15.00000"" />
</towary>
</pytanie>
";


           
           string ret=cl.pobierzStanMagazynowyProducenta(xml);

            int i=0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string xml = @"<pytanie>
<dokument>
<wersja>1.0.0</wersja>
<znacznik>2011-03-30T09:26:23+02:00</znacznik>
</dokument>
<konto>
<login>LOGIN</login>
<haslo>PASSWORD</haslo>
</konto>
<towary>
                               <towar symbol=""SYMBOL_1"" ilosc=""5.00000"" />
                               <towar symbol=""SYMBOL_2"" ilosc=""10.00000"" />
                               <towar symbol=""SYMBOL_3"" ilosc=""15.00000"" />
<towar symbol=""SYMBOL_4"" ilość=""20.00000"" />
</towary>
</pytanie>
";
          
        }
        public Object Deserialize(string xml, Object deserializeObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(deserializeObject.GetType());
            XDocument doc = XDocument.Parse(xml);
            string tagName = deserializeObject.GetType().Name;
            //The StringReader will be the stream holder for the existing XML file 
            deserializeObject = oXmlSerializer.Deserialize(new StringReader(doc.Root.ToString()));
            //initially deserialized, the data is represented by an object without a defined type 
            return deserializeObject;
        }
        public static string Serialize(Object serializeObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(serializeObject.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, serializeObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }

        }

        public partial class towarOdpowiedzTyp
        {

            private string symbolField;

            private double? iloscField;

            //private bool iloscFieldSpecified;

            private System.DateTime dataField;

            //private bool dataFieldSpecified;

            /// <uwagi/>
         
            public string symbol
            {
                get
                {
                    return this.symbolField;
                }
                set
                {
                    this.symbolField = value;
                }
            }

            
            public double? ilosc
            {
                get
                {
                    return this.iloscField;
                }
                set
                {
                    this.iloscField = value;
                }
            }

            ///// <uwagi/>
            //[System.Xml.Serialization.XmlIgnoreAttribute()]
            //public bool iloscSpecified
            //{
            //    get
            //    {
            //        return this.iloscFieldSpecified;
            //    }
            //    set
            //    {
            //        this.iloscFieldSpecified = value;
            //    }
            //}

            /// <uwagi/>
    
            public System.DateTime data
            {
                get
                {
                    return this.dataField;
                }
                set
                {
                    this.dataField = value;
                }
            }

            ///// <uwagi/>
            //[System.Xml.Serialization.XmlIgnoreAttribute()]
            //public bool dataSpecified
            //{
            //    get
            //    {
            //        return this.dataFieldSpecified;
            //    }
            //    set
            //    {
            //        this.dataFieldSpecified = value;
            //    }
            //}
        }
    }
}
