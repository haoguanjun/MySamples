
namespace Advent.ApxRestApiExample
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;

    public class HttpClientProxy: IDisposable
    {
        private HttpClient httpClient;

         /// <summary>
        /// Connect Apx REST Web Service through Windows NT user
        /// </summary>
        /// <param name="webServer">Web Server Name</param>
        public HttpClientProxy(string webServer)
        {
            this.httpClient = this.CreateHttlpClientInstance(webServer);
            this.Login();
        }

        /// <summary>
        /// Connect Apx REST Web Service through non-Windows NT user
        /// </summary>
        /// <param name="webServer">Web Server Name</param>
        /// <param name="login">Login User Name of Non-Windows NT user</param>
        /// <param name="password">Password of Login User</param>
        public HttpClientProxy(string webServer, string login, string password)
        {
            this.httpClient = this.CreateHttlpClientInstance(webServer);
            this.Login(login, password);
        }

        /// <summary>
        /// Http Get method
        /// </summary>
        /// <param name="relativeUri">Relative Uri</param>
        /// <returns>Result of Http Get method</returns>
        public string HttpGet(string relativeUri)
        {
            string requestUrl = string.Format("{0}/{1}", this.httpClient.BaseAddress.AbsoluteUri, relativeUri);
            HttpResponseMessage response = this.httpClient.GetAsync(requestUrl).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public string HttpPatch(string relativeUri, string jsonString)
        {
            string requestUrl = string.Format("{0}/{1}", this.httpClient.BaseAddress.AbsoluteUri, relativeUri);
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("Patch"), requestUrl);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");            
            HttpResponseMessage response = this.httpClient.SendAsync(request).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Http Post method
        /// </summary>
        /// <param name="relativeUri">Relative Uri</param>
        /// <param name="jsonString">json string to post.</param>
        /// <returns>Result of Http Post method</returns>
        public string HttpPost(string relativeUri, string jsonString)
        {
            string requestUrl = string.Format("{0}/{1}", this.httpClient.BaseAddress.AbsoluteUri, relativeUri);
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = this.httpClient.PostAsync(requestUrl, content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Send Post request without input arguments
        /// </summary>
        /// <param name="relativeUri">Relative Uri</param>
        /// <returns>result</returns>
        public string HttpPost(string relativeUri)
        {
            string requestUrl = string.Format("{0}/{1}", this.httpClient.BaseAddress.AbsoluteUri, relativeUri);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            HttpResponseMessage response = this.httpClient.SendAsync(request).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Http Delete method
        /// </summary>
        /// <param name="relativeUri">Relative Uri</param>
        /// <returns>Result of Http Delete method.</returns>
        public string HttpDelete(string relativeUri)
        {
            string requestUrl = string.Format("{0}/{1}", this.httpClient.BaseAddress.AbsoluteUri, relativeUri);
            HttpResponseMessage response = this.httpClient.DeleteAsync(requestUrl).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Create HttpClient Instance
        /// </summary>
        /// <param name="webServer">APX Web Server Name</param>
        /// <returns>HttpClient Instance</returns>
        private HttpClient CreateHttlpClientInstance(string webServer)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;
            clientHandler.CookieContainer = new CookieContainer();

            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.BaseAddress = new Uri(string.Format("{0}/apx", webServer));

            return httpClient;
        }

        /// <summary>
        /// Login as Windows NT user
        /// </summary>
        private void Login()
        {
            string requestUrl = string.Format("{0}/Api/authenticate", this.httpClient.BaseAddress.AbsoluteUri);
            HttpResponseMessage response = this.httpClient.GetAsync(requestUrl).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Windows NT User Login Fails.");
            }
        }

        /// <summary>
        /// Login as non-Windows NT user
        /// </summary>
        /// <param name="login">Login name of APX User</param>
        /// <param name="password">Password of Login User</param>
        private void Login(string login, string password)
        {
            string requestUrl = string.Format("{0}/Api/authenticate?loginname={1}&password={2}", this.httpClient.BaseAddress.AbsoluteUri, login, password);
            HttpResponseMessage response = this.httpClient.GetAsync(requestUrl).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("APX User Login Fails.");
            }
        }

        /// <summary>
        /// Logout currnt user
        /// </summary>
        private void Logout()
        {
            string requestUrl = string.Format("{0}/Api/authenticate?logout", this.httpClient.BaseAddress.AbsoluteUri);
            HttpResponseMessage response = this.httpClient.GetAsync(requestUrl).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Logout Fails.");
            }
        }

        public void Dispose()
        {
            this.Logout();
        }
    }
}
