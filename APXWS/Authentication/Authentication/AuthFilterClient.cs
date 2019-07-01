using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

namespace Authentication
{
    public class AuthFilterClient
    {
        private string BaseUrl;
        private bool UseDefaultCredentials;
        public string SessionCode
        {
            get;
            private set;
        }

        public AuthFilterClient(string webServerUrl)
        {
            this.BaseUrl = string.Format("{0}/apx/api/authenticate", webServerUrl);
        }

        /// <summary>
        /// Login as Windows NT user
        /// </summary>
        public void Login()
        {
            this.BaseUrl = this.BaseUrl.Replace("/apxlogin/", "/apx/");
            this.SessionCode = this.GetSessionCode(this.BaseUrl, true);
        }

        /// <summary>
        /// Login as non-Windows NT user
        /// </summary>
        public void Login(string login, string password)
        {
            this.BaseUrl = this.BaseUrl.Replace("/apx/", "/apxlogin/");
            string requestUrl = string.Format("{0}?loginname={1}&password={2}", this.BaseUrl, login, password);
            this.SessionCode = this.GetSessionCode(requestUrl, false);
        }

        /// <summary>
        /// Logout currnt user
        /// </summary>
        public void Logout()
        {
            string requestUrl = string.Format("{0}?logout", this.BaseUrl);
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = this.UseDefaultCredentials;
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("sessioncode", this.SessionCode);
            HttpResponseMessage response = client.GetAsync(requestUrl).Result;
            response.EnsureSuccessStatusCode();
        }

        private string GetSessionCode(string requestUrl, bool useDefaultCredentials)
        {
            string sessionCode = null;

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = useDefaultCredentials;
            this.UseDefaultCredentials = useDefaultCredentials;
            var client = new HttpClient(handler);
            HttpResponseMessage response = client.GetAsync(requestUrl).Result;
            response.EnsureSuccessStatusCode();

            IEnumerable<string> headers;
            if (response.Headers.TryGetValues("Set-Cookie", out headers))
            {
                // AOAuth1=SessionCod=2F7BAAC4-B608-4A61-BEF5-D680D4FC28F9; expires=Mon, 24 Jun 2019 04:38:57 GMT; path =/; version = 1; HttpOnly
                foreach (string header in headers)
                {
                    if (header.StartsWith("AOAuth1"))
                    {
                        sessionCode = Regex.Match(header, @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}").Value;
                    }
                }
            }

            return sessionCode;
        }
    }
}
