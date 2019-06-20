
namespace Advent.APXRESTfulAPI
{
    using System;
    using System.Net.Http;
    using System.Text;

    public class HttpProxy : IDisposable
    {
        private HttpClient client;
        private string webserver;

        public HttpProxy(string webServer, string accesstoken)
        {
            this.webserver = webServer;
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", accesstoken));
            this.client.BaseAddress = new Uri(string.Format("http://{0}/apxlogin/api/odata/v1", webserver));
        }

        public string Get(string relativeUri)
        {
            string requestUrl = string.Format("{0}/{1}", this.client.BaseAddress.AbsoluteUri, relativeUri);
            var response = this.client.GetAsync(requestUrl);
            if (response.IsFaulted)
            {
                throw response.Exception;
            }

            return response.Result.Content.ReadAsStringAsync().Result;
        }

        public string Patch(string relativeUri, string payload)
        {
            string requestUrl = string.Format("{0}/{1}", this.client.BaseAddress.AbsoluteUri, relativeUri);
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("Patch"), requestUrl);
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = this.client.SendAsync(request);
            if (response.IsFaulted)
            {
                throw response.Exception;
            }

            return response.Result.Content.ReadAsStringAsync().Result;
        }

        public void Dispose()
        {
        }
    }
}
