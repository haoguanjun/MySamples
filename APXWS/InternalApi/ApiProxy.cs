using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace InternalApi
{
    class ApiProxy : IDisposable
    {
        private string sessionCode;
        private string baseUrl;

        public ApiProxy(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public void Login(string username, string password)
        {
            string requestUri = string.Format("{0}/apxlogin/api/internal/authenticate?login", this.baseUrl);
            WebRequest request = WebRequest.CreateHttp(requestUri);
            request.Headers.Add("APXLogin", "Basic " + Helper.Base64Encode(username + ":" + password));
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            this.sessionCode = Helper.ExtractGuid(reader.ReadToEnd());
        }

        public void Get(string relevantUri)
        {
            string requestUri = string.Format("{0}/{1}", this.baseUrl, relevantUri);
            WebRequest request = WebRequest.CreateHttp(requestUri);
            request.Headers.Add("SessionCode", this.sessionCode);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string text = reader.ReadToEnd();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
