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
    public partial class towarOdpowiedzTyp
    {

        private string symbolField;

        private double iloscField;

        private bool iloscFieldSpecified;

        private System.DateTime dataField;

        private bool dataFieldSpecified;

        /// <uwagi/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <uwagi/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double ilosc
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

        /// <uwagi/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool iloscSpecified
        {
            get
            {
                return this.iloscFieldSpecified;
            }
            set
            {
                this.iloscFieldSpecified = value;
            }
        }

        /// <uwagi/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <uwagi/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dataSpecified
        {
            get
            {
                return this.dataFieldSpecified;
            }
            set
            {
                this.dataFieldSpecified = value;
            }
        }
    }
}