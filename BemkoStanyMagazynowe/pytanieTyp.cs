using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BemkoStanyMagazynowe
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://megacennik.eu/pobierzStanMagazynowyProducenta/")]
    public partial class pytanie
    {

        private dokumentTyp dokumentField;

        private kontoTyp kontoField;

        private towarPytanieTyp[] towaryField;

        /// <uwagi/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public dokumentTyp dokument
        {
            get
            {
                return this.dokumentField;
            }
            set
            {
                this.dokumentField = value;
            }
        }

        /// <uwagi/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public kontoTyp konto
        {
            get
            {
                return this.kontoField;
            }
            set
            {
                this.kontoField = value;
            }
        }

        /// <uwagi/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("towar", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public towarPytanieTyp[] towary
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
    }
}