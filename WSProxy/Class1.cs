using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WSProxy
{
    public class WSProxy
    {
        public class MyPolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(  ServicePoint srvPoint,
                                                X509Certificate certificate, WebRequest request,
                                                int certificateProblem  )
            {                
                return true;
            }
        }
        public string CallWebMethod(string webServiceURL, string webMethod, Dictionary<string, string> dicParameters)
        {
            try
            {
                byte[] _requestData = this.CreateHttpRequestData(dicParameters);

                string uri = webServiceURL + "/" + webMethod;
                HttpWebRequest _httpRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                _httpRequest.Method = "POST";
                _httpRequest.KeepAlive = false;
                _httpRequest.ContentType = "application/x-www-form-urlencoded";
                _httpRequest.ContentLength = _requestData.Length;
                _httpRequest.Credentials = new NetworkCredential("bmp", "progres12#$"); ; 
                _httpRequest.Timeout = 30000;

                HttpWebResponse _httpResponse = null;
                string _response = string.Empty;
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                _httpRequest.GetRequestStream().Write(_requestData, 0, _requestData.Length);
                _httpResponse = (HttpWebResponse)_httpRequest.GetResponse();
                System.IO.Stream _baseStream = _httpResponse.GetResponseStream();
                System.IO.StreamReader _responseStreamReader = new System.IO.StreamReader(_baseStream);
                _response = _responseStreamReader.ReadToEnd();
                _responseStreamReader.Close();

                return _response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private byte[] CreateHttpRequestData(Dictionary<string, string> dic)
        {
            StringBuilder _sbParameters = new StringBuilder();
            foreach (string param in dic.Keys)
            {
                _sbParameters.Append(param); 
                _sbParameters.Append('=');
                _sbParameters.Append(dic[param]);
                _sbParameters.Append('&');
            }
            _sbParameters.Remove(_sbParameters.Length - 1, 1);

            UTF8Encoding encoding = new UTF8Encoding();

            return encoding.GetBytes(_sbParameters.ToString());
        }
    }
}
