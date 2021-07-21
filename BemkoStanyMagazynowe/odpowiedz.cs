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
    public partial class odpowiedz
    {

        private towarOdpowiedzTyp[] towaryField;

        private string wersjaField;

        private System.DateTime znacznikField;

        //private bool znacznikFieldSpecified;

        /// <uwagi/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("towar", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public towarOdpowiedzTyp[] towary
        {
            get
            {
                return this.towaryField;
            }
            set
            {
                this.towaryField = value;
            }
        }

        /// <uwagi/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        ///// <uwagi/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool znacznikSpecified
        //{
        //    get
        //    {
        //        return this.znacznikFieldSpecified;
        //    }
        //    set
        //    {
        //        this.znacznikFieldSpecified = value;
        //    }
        //}
    }

}