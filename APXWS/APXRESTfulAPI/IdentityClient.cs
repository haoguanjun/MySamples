

namespace Advent.APXRESTfulAPI
{
    using IdentityModel.Client;
    using System;
    using System.Net;
    using System.Net.Http;

    public class IdentityClient : IDisposable
    {
        private string AppServer
        {
            get;
            set;
        }

        public string AccessToken
        {
            get;
            private set;
        }

        public IdentityClient(string appserver)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            this.AppServer = appserver;
            var client = new HttpClient();
            var response = client.RequestTokenAsync(new TokenRequest
            {
                Address = string.Format("https://{0}:5001/connect/token", appserver),
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                GrantType = "WindowsAuth",
                Parameters = {
                    { "scope", "apxapi" }
                }
            });

            if (response.IsFaulted)
            {
                throw response.Exception;
            }

            this.AccessToken = response.Result.AccessToken;
        }

        public IdentityClient(string appserver, string username, string password)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            this.AppServer = appserver;
            var client = new HttpClient();
            var response = client.RequestTokenAsync(new TokenRequest
            {
                Address = string.Format("https://{0}:5001/connect/token", appserver),
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                GrantType = "password",
                Parameters = {
                    { "scope", "apxapi" },
                    { "username", username },
                    { "password", password }
                }
            });

            if (response.IsFaulted)
            {
                throw response.Exception;
            }

            this.AccessToken = response.Result.AccessToken;
        }

        private void RevokeToken(string appserver, string accesstoken)
        {
            var client = new HttpClient();
            var response = client.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = string.Format("https://{0}:5001/connect/revocation", appserver),
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                Token = accesstoken
            });

            if (response.IsFaulted)
            {
                throw response.Exception;
            }
        }

        private void EndSession(string appserver, string accesstoken)
        {            
            RequestUrl requestUrl = new RequestUrl(string.Format("https://{0}:5001/connect/endsession", appserver));
            string endsessionUrl = requestUrl.CreateEndSessionUrl(accesstoken);

            var client = new HttpClient();
            var response = client.GetAsync(endsessionUrl);
            if (response.IsFaulted)
            {
                throw response.Exception;
            }
        }

        public void Dispose()
        {
            this.EndSession(this.AppServer, this.AccessToken);
            this.RevokeToken(this.AppServer, this.AccessToken);
        }
    }
}
