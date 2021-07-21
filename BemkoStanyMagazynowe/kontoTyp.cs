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
    public partial class kontoTyp
    {

        private string loginField;

        private string hasloField;

        /// <uwagi/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string login
        {
            get
            {
                return this.loginField;
            }
            set
            {
                this.loginField = value;
            }
        }

        /// <uwagi/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public string haslo
        {
            get
            {
                return this.hasloField;
            }
            set
            {
                this.hasloField = value;
            }
        }
    }
}