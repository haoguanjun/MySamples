
namespace Advent.APXRESTfulAPI
{
    using System;
    using System.Net.Http;
    using System.Text;

    public class HttpProxy : IDisposable
    {
        private HttpClient client;
        private string webserver;

        public HttpProxy(string webServer, string token)
        {
            this.webserver = webServer;
            this.client = this.CreateHttpClientInstance(webServer, token);
        }

        private HttpClient CreateHttpClientInstance(string webserver, string accessToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", accessToken));
            client.BaseAddress = new Uri(string.Format("http://{0}/apxlogin/api/odata/v1", webserver));
            return client;
        }

        public string Get(string relativeUri)
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

        public string Patch(string relativeUri, string jsonString)
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
