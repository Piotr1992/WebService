//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BemkoStanyMagazynoweTest.ServiceReference2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.BemkoStanyMagazynoweSoap")]
    public interface BemkoStanyMagazynoweSoap {
        
        // CODEGEN: Generating message contract since element name pytanie from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/pobierzStanMagazynowyProducenta", ReplyAction="*")]
        BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaResponse pobierzStanMagazynowyProducenta(BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/pobierzStanMagazynowyProducenta", ReplyAction="*")]
        System.Threading.Tasks.Task<BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaResponse> pobierzStanMagazynowyProducentaAsync(BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class pobierzStanMagazynowyProducentaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="pobierzStanMagazynowyProducenta", Namespace="http://tempuri.org/", Order=0)]
        public BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequestBody Body;
        
        public pobierzStanMagazynowyProducentaRequest() {
        }
        
        public pobierzStanMagazynowyProducentaRequest(BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class pobierzStanMagazynowyProducentaRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string pytanie;
        
        public pobierzStanMagazynowyProducentaRequestBody() {
        }
        
        public pobierzStanMagazynowyProducentaRequestBody(string pytanie) {
            this.pytanie = pytanie;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class pobierzStanMagazynowyProducentaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="pobierzStanMagazynowyProducentaResponse", Namespace="http://tempuri.org/", Order=0)]
        public BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaResponseBody Body;
        
        public pobierzStanMagazynowyProducentaResponse() {
        }
        
        public pobierzStanMagazynowyProducentaResponse(BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class pobierzStanMagazynowyProducentaResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string pobierzStanMagazynowyProducentaResult;
        
        public pobierzStanMagazynowyProducentaResponseBody() {
        }
        
        public pobierzStanMagazynowyProducentaResponseBody(string pobierzStanMagazynowyProducentaResult) {
            this.pobierzStanMagazynowyProducentaResult = pobierzStanMagazynowyProducentaResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface BemkoStanyMagazynoweSoapChannel : BemkoStanyMagazynoweTest.ServiceReference2.BemkoStanyMagazynoweSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BemkoStanyMagazynoweSoapClient : System.ServiceModel.ClientBase<BemkoStanyMagazynoweTest.ServiceReference2.BemkoStanyMagazynoweSoap>, BemkoStanyMagazynoweTest.ServiceReference2.BemkoStanyMagazynoweSoap {
        
        public BemkoStanyMagazynoweSoapClient() {
        }
        
        public BemkoStanyMagazynoweSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BemkoStanyMagazynoweSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BemkoStanyMagazynoweSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BemkoStanyMagazynoweSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaResponse BemkoStanyMagazynoweTest.ServiceReference2.BemkoStanyMagazynoweSoap.pobierzStanMagazynowyProducenta(BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequest request) {
            return base.Channel.pobierzStanMagazynowyProducenta(request);
        }
        
        public string pobierzStanMagazynowyProducenta(string pytanie) {
            BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequest inValue = new BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequest();
            inValue.Body = new BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequestBody();
            inValue.Body.pytanie = pytanie;
            BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaResponse retVal = ((BemkoStanyMagazynoweTest.ServiceReference2.BemkoStanyMagazynoweSoap)(this)).pobierzStanMagazynowyProducenta(inValue);
            return retVal.Body.pobierzStanMagazynowyProducentaResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaResponse> BemkoStanyMagazynoweTest.ServiceReference2.BemkoStanyMagazynoweSoap.pobierzStanMagazynowyProducentaAsync(BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequest request) {
            return base.Channel.pobierzStanMagazynowyProducentaAsync(request);
        }
        
        public System.Threading.Tasks.Task<BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaResponse> pobierzStanMagazynowyProducentaAsync(string pytanie) {
            BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequest inValue = new BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequest();
            inValue.Body = new BemkoStanyMagazynoweTest.ServiceReference2.pobierzStanMagazynowyProducentaRequestBody();
            inValue.Body.pytanie = pytanie;
            return ((BemkoStanyMagazynoweTest.ServiceReference2.BemkoStanyMagazynoweSoap)(this)).pobierzStanMagazynowyProducentaAsync(inValue);
        }
    }
}
