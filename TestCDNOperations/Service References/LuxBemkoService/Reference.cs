﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestCDNOperations.LuxBemkoService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LuxBemkoService.WebServiceSoap")]
    public interface WebServiceSoap {
        
        // CODEGEN: Generating message contract since element name b2b_towaryResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/b2b_towary", ReplyAction="*")]
        TestCDNOperations.LuxBemkoService.b2b_towaryResponse b2b_towary(TestCDNOperations.LuxBemkoService.b2b_towaryRequest request);
        
        // CODEGEN: Generating message contract since element name b2b_zamnagResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/b2b_zamnag", ReplyAction="*")]
        TestCDNOperations.LuxBemkoService.b2b_zamnagResponse b2b_zamnag(TestCDNOperations.LuxBemkoService.b2b_zamnagRequest request);
        
        // CODEGEN: Generating message contract since element name b2b_zamelemResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/b2b_zamelem", ReplyAction="*")]
        TestCDNOperations.LuxBemkoService.b2b_zamelemResponse b2b_zamelem(TestCDNOperations.LuxBemkoService.b2b_zamelemRequest request);
        
        // CODEGEN: Generating message contract since element name xml from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/b2b_nowyDokumentZam", ReplyAction="*")]
        TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamResponse b2b_nowyDokumentZam(TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamRequest request);
        
        // CODEGEN: Generating message contract since element name xml from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/b2b_nowyAdres", ReplyAction="*")]
        TestCDNOperations.LuxBemkoService.b2b_nowyAdresResponse b2b_nowyAdres(TestCDNOperations.LuxBemkoService.b2b_nowyAdresRequest request);
        
        // CODEGEN: Generating message contract since element name b2b_stanyResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/b2b_stany", ReplyAction="*")]
        TestCDNOperations.LuxBemkoService.b2b_stanyResponse b2b_stany(TestCDNOperations.LuxBemkoService.b2b_stanyRequest request);
        
        // CODEGEN: Generating message contract since element name b2b_CenySpecjalneResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/b2b_CenySpecjalne", ReplyAction="*")]
        TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneResponse b2b_CenySpecjalne(TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_towaryRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_towary", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_towaryRequestBody Body;
        
        public b2b_towaryRequest() {
        }
        
        public b2b_towaryRequest(TestCDNOperations.LuxBemkoService.b2b_towaryRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class b2b_towaryRequestBody {
        
        public b2b_towaryRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_towaryResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_towaryResponse", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_towaryResponseBody Body;
        
        public b2b_towaryResponse() {
        }
        
        public b2b_towaryResponse(TestCDNOperations.LuxBemkoService.b2b_towaryResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_towaryResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string b2b_towaryResult;
        
        public b2b_towaryResponseBody() {
        }
        
        public b2b_towaryResponseBody(string b2b_towaryResult) {
            this.b2b_towaryResult = b2b_towaryResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_zamnagRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_zamnag", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_zamnagRequestBody Body;
        
        public b2b_zamnagRequest() {
        }
        
        public b2b_zamnagRequest(TestCDNOperations.LuxBemkoService.b2b_zamnagRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_zamnagRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int gidnumer;
        
        public b2b_zamnagRequestBody() {
        }
        
        public b2b_zamnagRequestBody(int gidnumer) {
            this.gidnumer = gidnumer;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_zamnagResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_zamnagResponse", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_zamnagResponseBody Body;
        
        public b2b_zamnagResponse() {
        }
        
        public b2b_zamnagResponse(TestCDNOperations.LuxBemkoService.b2b_zamnagResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_zamnagResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string b2b_zamnagResult;
        
        public b2b_zamnagResponseBody() {
        }
        
        public b2b_zamnagResponseBody(string b2b_zamnagResult) {
            this.b2b_zamnagResult = b2b_zamnagResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_zamelemRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_zamelem", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_zamelemRequestBody Body;
        
        public b2b_zamelemRequest() {
        }
        
        public b2b_zamelemRequest(TestCDNOperations.LuxBemkoService.b2b_zamelemRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_zamelemRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int gidnumer;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int kndnumer;
        
        public b2b_zamelemRequestBody() {
        }
        
        public b2b_zamelemRequestBody(int gidnumer, int kndnumer) {
            this.gidnumer = gidnumer;
            this.kndnumer = kndnumer;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_zamelemResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_zamelemResponse", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_zamelemResponseBody Body;
        
        public b2b_zamelemResponse() {
        }
        
        public b2b_zamelemResponse(TestCDNOperations.LuxBemkoService.b2b_zamelemResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_zamelemResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string b2b_zamelemResult;
        
        public b2b_zamelemResponseBody() {
        }
        
        public b2b_zamelemResponseBody(string b2b_zamelemResult) {
            this.b2b_zamelemResult = b2b_zamelemResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_nowyDokumentZamRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_nowyDokumentZam", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamRequestBody Body;
        
        public b2b_nowyDokumentZamRequest() {
        }
        
        public b2b_nowyDokumentZamRequest(TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_nowyDokumentZamRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xml;
        
        public b2b_nowyDokumentZamRequestBody() {
        }
        
        public b2b_nowyDokumentZamRequestBody(string xml) {
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_nowyDokumentZamResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_nowyDokumentZamResponse", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamResponseBody Body;
        
        public b2b_nowyDokumentZamResponse() {
        }
        
        public b2b_nowyDokumentZamResponse(TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_nowyDokumentZamResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string b2b_nowyDokumentZamResult;
        
        public b2b_nowyDokumentZamResponseBody() {
        }
        
        public b2b_nowyDokumentZamResponseBody(string b2b_nowyDokumentZamResult) {
            this.b2b_nowyDokumentZamResult = b2b_nowyDokumentZamResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_nowyAdresRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_nowyAdres", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_nowyAdresRequestBody Body;
        
        public b2b_nowyAdresRequest() {
        }
        
        public b2b_nowyAdresRequest(TestCDNOperations.LuxBemkoService.b2b_nowyAdresRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_nowyAdresRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xml;
        
        public b2b_nowyAdresRequestBody() {
        }
        
        public b2b_nowyAdresRequestBody(string xml) {
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_nowyAdresResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_nowyAdresResponse", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_nowyAdresResponseBody Body;
        
        public b2b_nowyAdresResponse() {
        }
        
        public b2b_nowyAdresResponse(TestCDNOperations.LuxBemkoService.b2b_nowyAdresResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_nowyAdresResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string b2b_nowyAdresResult;
        
        public b2b_nowyAdresResponseBody() {
        }
        
        public b2b_nowyAdresResponseBody(string b2b_nowyAdresResult) {
            this.b2b_nowyAdresResult = b2b_nowyAdresResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_stanyRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_stany", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_stanyRequestBody Body;
        
        public b2b_stanyRequest() {
        }
        
        public b2b_stanyRequest(TestCDNOperations.LuxBemkoService.b2b_stanyRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_stanyRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int Id;
        
        public b2b_stanyRequestBody() {
        }
        
        public b2b_stanyRequestBody(int Id) {
            this.Id = Id;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_stanyResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_stanyResponse", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_stanyResponseBody Body;
        
        public b2b_stanyResponse() {
        }
        
        public b2b_stanyResponse(TestCDNOperations.LuxBemkoService.b2b_stanyResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_stanyResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string b2b_stanyResult;
        
        public b2b_stanyResponseBody() {
        }
        
        public b2b_stanyResponseBody(string b2b_stanyResult) {
            this.b2b_stanyResult = b2b_stanyResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_CenySpecjalneRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_CenySpecjalne", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneRequestBody Body;
        
        public b2b_CenySpecjalneRequest() {
        }
        
        public b2b_CenySpecjalneRequest(TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class b2b_CenySpecjalneRequestBody {
        
        public b2b_CenySpecjalneRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class b2b_CenySpecjalneResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="b2b_CenySpecjalneResponse", Namespace="http://tempuri.org/", Order=0)]
        public TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneResponseBody Body;
        
        public b2b_CenySpecjalneResponse() {
        }
        
        public b2b_CenySpecjalneResponse(TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class b2b_CenySpecjalneResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string b2b_CenySpecjalneResult;
        
        public b2b_CenySpecjalneResponseBody() {
        }
        
        public b2b_CenySpecjalneResponseBody(string b2b_CenySpecjalneResult) {
            this.b2b_CenySpecjalneResult = b2b_CenySpecjalneResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebServiceSoapChannel : TestCDNOperations.LuxBemkoService.WebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebServiceSoapClient : System.ServiceModel.ClientBase<TestCDNOperations.LuxBemkoService.WebServiceSoap>, TestCDNOperations.LuxBemkoService.WebServiceSoap {
        
        public WebServiceSoapClient() {
        }
        
        public WebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TestCDNOperations.LuxBemkoService.b2b_towaryResponse TestCDNOperations.LuxBemkoService.WebServiceSoap.b2b_towary(TestCDNOperations.LuxBemkoService.b2b_towaryRequest request) {
            return base.Channel.b2b_towary(request);
        }
        
        public string b2b_towary() {
            TestCDNOperations.LuxBemkoService.b2b_towaryRequest inValue = new TestCDNOperations.LuxBemkoService.b2b_towaryRequest();
            inValue.Body = new TestCDNOperations.LuxBemkoService.b2b_towaryRequestBody();
            TestCDNOperations.LuxBemkoService.b2b_towaryResponse retVal = ((TestCDNOperations.LuxBemkoService.WebServiceSoap)(this)).b2b_towary(inValue);
            return retVal.Body.b2b_towaryResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TestCDNOperations.LuxBemkoService.b2b_zamnagResponse TestCDNOperations.LuxBemkoService.WebServiceSoap.b2b_zamnag(TestCDNOperations.LuxBemkoService.b2b_zamnagRequest request) {
            return base.Channel.b2b_zamnag(request);
        }
        
        public string b2b_zamnag(int gidnumer) {
            TestCDNOperations.LuxBemkoService.b2b_zamnagRequest inValue = new TestCDNOperations.LuxBemkoService.b2b_zamnagRequest();
            inValue.Body = new TestCDNOperations.LuxBemkoService.b2b_zamnagRequestBody();
            inValue.Body.gidnumer = gidnumer;
            TestCDNOperations.LuxBemkoService.b2b_zamnagResponse retVal = ((TestCDNOperations.LuxBemkoService.WebServiceSoap)(this)).b2b_zamnag(inValue);
            return retVal.Body.b2b_zamnagResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TestCDNOperations.LuxBemkoService.b2b_zamelemResponse TestCDNOperations.LuxBemkoService.WebServiceSoap.b2b_zamelem(TestCDNOperations.LuxBemkoService.b2b_zamelemRequest request) {
            return base.Channel.b2b_zamelem(request);
        }
        
        public string b2b_zamelem(int gidnumer, int kndnumer) {
            TestCDNOperations.LuxBemkoService.b2b_zamelemRequest inValue = new TestCDNOperations.LuxBemkoService.b2b_zamelemRequest();
            inValue.Body = new TestCDNOperations.LuxBemkoService.b2b_zamelemRequestBody();
            inValue.Body.gidnumer = gidnumer;
            inValue.Body.kndnumer = kndnumer;
            TestCDNOperations.LuxBemkoService.b2b_zamelemResponse retVal = ((TestCDNOperations.LuxBemkoService.WebServiceSoap)(this)).b2b_zamelem(inValue);
            return retVal.Body.b2b_zamelemResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamResponse TestCDNOperations.LuxBemkoService.WebServiceSoap.b2b_nowyDokumentZam(TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamRequest request) {
            return base.Channel.b2b_nowyDokumentZam(request);
        }
        
        public string b2b_nowyDokumentZam(string xml) {
            TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamRequest inValue = new TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamRequest();
            inValue.Body = new TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamRequestBody();
            inValue.Body.xml = xml;
            TestCDNOperations.LuxBemkoService.b2b_nowyDokumentZamResponse retVal = ((TestCDNOperations.LuxBemkoService.WebServiceSoap)(this)).b2b_nowyDokumentZam(inValue);
            return retVal.Body.b2b_nowyDokumentZamResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TestCDNOperations.LuxBemkoService.b2b_nowyAdresResponse TestCDNOperations.LuxBemkoService.WebServiceSoap.b2b_nowyAdres(TestCDNOperations.LuxBemkoService.b2b_nowyAdresRequest request) {
            return base.Channel.b2b_nowyAdres(request);
        }
        
        public string b2b_nowyAdres(string xml) {
            TestCDNOperations.LuxBemkoService.b2b_nowyAdresRequest inValue = new TestCDNOperations.LuxBemkoService.b2b_nowyAdresRequest();
            inValue.Body = new TestCDNOperations.LuxBemkoService.b2b_nowyAdresRequestBody();
            inValue.Body.xml = xml;
            TestCDNOperations.LuxBemkoService.b2b_nowyAdresResponse retVal = ((TestCDNOperations.LuxBemkoService.WebServiceSoap)(this)).b2b_nowyAdres(inValue);
            return retVal.Body.b2b_nowyAdresResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TestCDNOperations.LuxBemkoService.b2b_stanyResponse TestCDNOperations.LuxBemkoService.WebServiceSoap.b2b_stany(TestCDNOperations.LuxBemkoService.b2b_stanyRequest request) {
            return base.Channel.b2b_stany(request);
        }
        
        public string b2b_stany(int Id) {
            TestCDNOperations.LuxBemkoService.b2b_stanyRequest inValue = new TestCDNOperations.LuxBemkoService.b2b_stanyRequest();
            inValue.Body = new TestCDNOperations.LuxBemkoService.b2b_stanyRequestBody();
            inValue.Body.Id = Id;
            TestCDNOperations.LuxBemkoService.b2b_stanyResponse retVal = ((TestCDNOperations.LuxBemkoService.WebServiceSoap)(this)).b2b_stany(inValue);
            return retVal.Body.b2b_stanyResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneResponse TestCDNOperations.LuxBemkoService.WebServiceSoap.b2b_CenySpecjalne(TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneRequest request) {
            return base.Channel.b2b_CenySpecjalne(request);
        }
        
        public string b2b_CenySpecjalne() {
            TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneRequest inValue = new TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneRequest();
            inValue.Body = new TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneRequestBody();
            TestCDNOperations.LuxBemkoService.b2b_CenySpecjalneResponse retVal = ((TestCDNOperations.LuxBemkoService.WebServiceSoap)(this)).b2b_CenySpecjalne(inValue);
            return retVal.Body.b2b_CenySpecjalneResult;
        }
    }
}
