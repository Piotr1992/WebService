using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BemkoStanyMagazynowe
{
    /// <uwagi/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://megacennik.eu/pobierzStanMagazynowyProducenta/")]
    public partial class dokumentTyp
    {

        private string wersjaField;

        private System.DateTime znacznikField;

        /// <uwagi/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string wersja
        {
            get
            {
                return this.wersjaField;
            }
            set
            {
                this.wersjaField = value;
            }
        }

        /// <uwagi/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public System.DateTime znacznik
        {
            get
            {
                return this.znacznikField;
            }
            set
            {
                this.znacznikField = value;
            }
        }
    }

}