

namespace Advent.APXRESTfulAPI
{
    using IdentityModel.Client;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class IdentityClient : IDisposable
    {
        private DiscoveryDocumentResponse DiscoveryDocument
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
            this.DiscoveryDocument = this.GetDiscoveryDocument(appserver).Result;
        }

        public async Task<TokenResponse> GetAccessToken()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
           
            var client = new HttpClient();
            var response = await client.RequestTokenAsync(new TokenRequest
            {
                Address = this.DiscoveryDocument.TokenEndpoint,
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                GrantType = "WindowsAuth",
                Parameters = {
                    { "scope", "apxapi" }
                }
            });

            if (response.IsError)
            {
                throw response.Exception;
            }

            return response;
        }

        public TokenResponse RequestToken(string username, string password)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var client = new HttpClient();
            var response = client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = this.DiscoveryDocument.TokenEndpoint,
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                Scope = "apxapi",
                UserName = username,
                Password = password
            });

            if (response.IsFaulted)
            {
                throw response.Exception;
            }

            return response.Result;
        }

        public async void Authorize(string username, string password)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            RequestUrl requestUrl = new RequestUrl(this.DiscoveryDocument.AuthorizeEndpoint);
            string authorizeUrl = requestUrl.CreateAuthorizeUrl(
                clientId: "ro.APXAPIClient",
                responseType: "code id_token token",
                scope: "apxapi",
                redirectUri: "http://localhost:5002/signin-oidc");
            var client = new HttpClient();
            var response = client.GetAsync(authorizeUrl).Result;
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
        }

        public async void RequestToken(string refreshToken)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var client = new HttpClient();
            var response = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = this.DiscoveryDocument.TokenEndpoint,
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                RefreshToken = refreshToken
            });
        }

        private async void RevokeAccessToken(string accesstoken)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var client = new HttpClient();
            var response = await client.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = this.DiscoveryDocument.RevocationEndpoint,
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                Token = accesstoken
            });

            if (response.IsError)
            {
                throw response.Exception;
            }
        }

        public void EndSession(string idtoken)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            RequestUrl requestUrl = new RequestUrl(this.DiscoveryDocument.EndSessionEndpoint);
            string endsessionUrl = requestUrl.CreateEndSessionUrl(idtoken);

            var client = new HttpClient();
            var response = client.GetAsync(endsessionUrl).Result;
            response.EnsureSuccessStatusCode();
        }

        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocument(string appserver)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var client = new HttpClient();
            var response = await client.GetDiscoveryDocumentAsync($"https://{appserver}:5001");
            if (response.IsError)
            {
                throw response.Exception;
            }

            return response;
        }

        public void Dispose()
        {
            this.EndSession(this.AccessToken);
        }
    }
}
