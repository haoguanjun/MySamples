
namespace Advent.ApxRestApiExample
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;

    public class ApxToken
    {
        public string access_token
        {
            get;
            set;
        }
        public int expires_in
        {
            get;
            set;
        }
        public string token_type
        {
            get;
            set;
        }
    }

    public class HttpClientProxy : IDisposable
    {
        private HttpClient client;

        public HttpClientProxy(string webServer, string username, string password)
        {
            ApxToken token = this.GetToken(webServer, username, password);
            this.client = this.CreateHttpClientInstance(webServer, token.access_token);
        }

        public HttpClientProxy(string webServer)
        {
            ApxToken token = this.GetToken(webServer);
            this.client = this.CreateHttpClientInstance(webServer, token.access_token);
        }

        private ApxToken GetToken(string webServer, string username, string password)
        {
            // this is to bypass certificate validation error. 
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
           
            string requestUri = string.Format("https://{0}:5001/connect/token", webServer);
            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "client_id", "ro.APXPublicAPIClient" },
                { "client_secret", "advs" },
                { "grant_type", "password" },
                { "scope", "APXPublicAPI" },
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

            ApxToken token = JsonConvert.DeserializeObject<ApxToken>(result);
            return token;
        }

        private ApxToken GetToken(string webServer)
        {
            // this is to bypass certificate validation error. 
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string requestUri = string.Format("https://{0}:5001/connect/token", webServer);
            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "client_id", "ro.APXPublicAPIClient" },
                { "client_secret", "advs" },
                { "grant_type", "WindowsAuth" },
                { "scope", "APXPublicAPI" }
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

            ApxToken token = JsonConvert.DeserializeObject<ApxToken>(result);
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
                throw new Exception(string.Format("Code={0}; Error={1}", response.StatusCode, result));
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

        public void Dispose()
        {
        }
    }
}
