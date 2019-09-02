
namespace AdvIdentityModelSample
{
    using IdentityModel.Client;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    class AdvIdentityClient
    {
        public string Issuer
        {
            get
            {
                if (this.discovery == null)
                {
                    return null;
                }

                return this.discovery.Issuer;
            }
            set
            {
                this.discovery = this.GetDiscoveryDocument(value);
            }
        }

        public string ClientId
        { get; set; }
        public string ClientSecret
        { get; set; }
        public string Scope
        { get; set; }

        private DiscoveryDocumentResponse discovery;

        private DiscoveryDocumentResponse GetDiscoveryDocument(string Issuer)
        {
            if (string.IsNullOrEmpty(Issuer))
            {
                throw new ArgumentNullException("Issuer can't be null or empty.");
            }

            // bypass self-signed Certificate error
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var client = new HttpClient();
            var response = client.GetDiscoveryDocumentAsync(Issuer);
            response.Wait();
            if (response.IsFaulted)
            {
                throw response.Exception;
            }

            return response.Result;
        }

        public TokenResponse Login(string username, string password)
        {
            // bypass self-signed Certificate error
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var client = new HttpClient();
            var response = client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = this.discovery.TokenEndpoint,
                ClientId = this.ClientId,
                ClientSecret = this.ClientSecret,
                Scope = this.Scope,
                UserName = username,
                Password = password
            });

            response.Wait();
            if (response.IsFaulted)
            {
                throw response.Exception;
            }

            return response.Result;
        }

        public TokenResponse Login()
        {
            // bypass self-signed Certificate error
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            var client = new HttpClient(handler);
            var response = client.RequestTokenAsync(new TokenRequest
            {
                Address = this.discovery.TokenEndpoint,
                ClientId = this.ClientId,
                ClientSecret = this.ClientSecret,
                Parameters = {
                    { "scope", this.Scope}
                },
                GrantType = "WindowsAuth",
            });

            response.Wait();
            if (response.IsFaulted)
            {
                throw response.Exception;
            }

            return response.Result;
        }
    }
}
