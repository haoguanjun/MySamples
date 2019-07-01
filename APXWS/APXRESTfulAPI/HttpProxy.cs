
namespace Advent.APXRESTfulAPI
{
    using IdentityModel.Client;
    using System;
    using System.Net.Http;
    using System.Text;

    public class HttpProxy : IDisposable
    {
        private HttpClient client;
        private string webserver;

        public HttpProxy(string webServer, TokenResponse token)
        {
            this.webserver = webServer;
            this.client = new HttpClient();
            //this.client.SetBearerToken(token.AccessToken);
            this.client.DefaultRequestHeaders.Add("Authorization", string.Format("{0} {1}", token.TokenType, token.AccessToken));
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
