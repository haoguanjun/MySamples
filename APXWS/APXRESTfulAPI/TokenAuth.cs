namespace Advent.APXRESTfulAPI
{
    using Microsoft.IdentityModel.JsonWebTokens;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    public class TokenAuth:IDisposable
    {
        public string Token
        {
            get;
            private set;
        }

        public string SessionCode
        {
            get;
            private set;
        }

        private string appserver;

        public TokenAuth(string appserver)
        {
            this.Token = this.GetToken(appserver).access_token;
            this.SessionCode = this.GetSessionCode(this.Token);
            this.appserver = appserver;
        }

        public TokenAuth(string appserver, string username, string password)
        {
            this.Token = this.GetToken(appserver, username, password).access_token;
            this.SessionCode = this.GetSessionCode(this.Token);
            this.appserver = appserver;
        }

        private Token GetToken(string appServer, string username, string password)
        {
            // this is to bypass certificate validation error. 
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string requestUri = string.Format("https://{0}:5001/connect/token", appServer);
            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "client_id", "ro.APXAPIClient" },
                { "client_secret", "advs" },
                { "grant_type", "password" },
                { "scope", "apxapi" },
                { "username", username },
                { "password", password }
            });

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PostAsync(requestUri, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("Code={0}; Error={1}", response.StatusCode, result));
            }

            Token token = JsonConvert.DeserializeObject<Token>(result);
            return token;
        }

        private Token GetToken(string appServer)
        {
            // this is to bypass certificate validation error. 
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string requestUri = string.Format("https://{0}:5001/connect/token", appServer);
            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "client_id", "ro.APXAPIClient" },
                { "client_secret", "advs" },
                { "grant_type", "WindowsAuth" },
                { "scope", "apxapi" }
            });

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = client.PostAsync(requestUri, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("Code={0}; Error={1}", response.StatusCode, result));
            }

            Token token = JsonConvert.DeserializeObject<Token>(result);
            return token;
        }

        private string GetSessionCode(string token)
        {
            JsonWebToken jwt = new JsonWebToken(this.Token);
            return jwt.Subject;
        }

        private void EndSession(string appserver, string token)
        {
            string requestUri = string.Format("https://{0}:5001/connect/endsession?id_token_hint={1}", appserver, token);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(requestUri).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("Code={0}; Error={1}", response.StatusCode, result));
            }
        }

        public void Dispose()
        {
            this.EndSession(this.appserver, this.Token);
        }
    }
}
