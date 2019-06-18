
namespace Advent.APXRESTfulAPI
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using Microsoft.IdentityModel.JsonWebTokens;

    public class HttpClientProxy : IDisposable
    {
        private HttpClient client;
        private Token token;
        private string webserver;

        public HttpClientProxy(string webServer, string username, string password)
        {
            this.webserver = webServer;
            this.token = this.GetToken(webServer, username, password);
            this.client = this.CreateHttpClientInstance(webServer, this.token.access_token);
        }

        public HttpClientProxy(string webServer)
        {
            this.webserver = webServer;
            this.token = this.GetToken(this.webserver);
            this.client = this.CreateHttpClientInstance(webServer, this.token.access_token);
        }

        private Token GetToken(string webServer, string username, string password)
        {
            // this is to bypass certificate validation error. 
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
           
            string requestUri = string.Format("https://{0}:5001/connect/token", webServer);
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

        private Token GetToken(string webServer)
        {
            // this is to bypass certificate validation error. 
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string requestUri = string.Format("https://{0}:5001/connect/token", webServer);
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
                throw new Exception(string.Format("Code={0}; Error={1}",response.StatusCode, result));
            }

            Token token = JsonConvert.DeserializeObject<Token>(result);
            return token;
        }

        private HttpClient CreateHttpClientInstance(string webserver, string accessToken)
        {
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", accessToken));
            client.BaseAddress = new Uri(string.Format("http://{0}", webserver));

            return client;
        }

        public string HttpGet(string relativeUri)
        {
            string requestUrl = string.Format("{0}/{1}", this.client.BaseAddress.AbsoluteUri, relativeUri);
            HttpResponseMessage response = this.client.GetAsync(requestUrl).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                //throw new Exception(string.Format("Code={0}; Error={1}", response.StatusCode, result));
            }

            return result;
        }

        public string HttpPatch(string relativeUri, string jsonString)
        {
            string requestUrl = string.Format("{0}/{1}", this.client.BaseAddress.AbsoluteUri, relativeUri);
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("Patch"), requestUrl);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = this.client.SendAsync(request).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("Code={0}; Error={1}", response.StatusCode, result));
            }

            return result;
        }

        private void Logout(string sessionGuid)
        {
            string requestUrl = string.Format("http://{0}/apxlogin/Api/authenticate?logout", this.webserver);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("sessioncode", sessionGuid);
            
            HttpResponseMessage response = client.GetAsync(requestUrl).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Logout Fails.");
            }
        }

        public void Dispose()
        {
            JsonWebToken jwt = new JsonWebToken(this.token.access_token);
            this.Logout(jwt.Subject);
        }
    }
}
