
namespace Advent.ApxSoap
{
    using System;
    using System.Net;
    using System.Web.Services.Protocols;

    class ApxSoapApiProxy : IDisposable
    {
        private AuthenticateWS.AuthenticateWS authenticateWS;

        /// <summary>
        /// Login as Windows NT user
        /// </summary>
        /// <param name="webServer">APX Web Server Name</param>
        public ApxSoapApiProxy()
        {
            this.authenticateWS = this.CreateAuthenticateWSInstance();
            this.ApxWS = this.CreateApxWSInstance(authenticateWS);
        }

        /// <summary>
        /// Login as non Windows NT user
        /// </summary>
        /// <param name="webServer">APX Web Server Name</param>
        /// <param name="login">Login name</param>
        /// <param name="password">password of login user</param>
        public ApxSoapApiProxy(string login, string password)
        {
            this.authenticateWS = this.CreateAuthenticateWSInstance(login, password);
            this.ApxWS = this.CreateApxWSInstance(authenticateWS);
        }

        public ApxWS.ApxWS ApxWS
        {
            get;
            private set;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.authenticateWS != null)
            {
                this.authenticateWS.Logout();
            }
        }

        /// <summary>
        /// Create a login session with Windows NT user
        /// </summary>
        /// <returns>Instance of AuthenticateWS</returns>
        private AuthenticateWS.AuthenticateWS CreateAuthenticateWSInstance()
        {
            AuthenticateWS.AuthenticateWS authenticateWS = new AuthenticateWS.AuthenticateWS();
            // Set UseDefaultCredentials to true for Windows integrated users.
            authenticateWS.UseDefaultCredentials = true;
            authenticateWS.CookieContainer = new CookieContainer();

            this.ResolveServiceUrl(authenticateWS);
            return authenticateWS;
        }

        /// <summary>
        /// Create a login session with APX user (non-Windows NT)
        /// </summary>
        /// <param name="login">Login name of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns>Instance of AuthenticateWS</returns>
        private AuthenticateWS.AuthenticateWS CreateAuthenticateWSInstance(string login, string password)
        {
            AuthenticateWS.AuthenticateWS authenticateWS = new AuthenticateWS.AuthenticateWS();
            // Set UseDefaultCredentials to false for APX users.
            authenticateWS.UseDefaultCredentials = false; 
            authenticateWS.CookieContainer = new CookieContainer();
            Uri cookieUri = new Uri((new Uri(authenticateWS.Url)).GetLeftPart(UriPartial.Authority));
            authenticateWS.CookieContainer.Add(cookieUri, new Cookie("LoginName", login));
            authenticateWS.CookieContainer.Add(cookieUri, new Cookie("Password", password));

            this.ResolveServiceUrl(authenticateWS);
            return authenticateWS;
        }

        /// <summary>
        /// Create an instance of ApxWS based on existing login session
        /// </summary>
        /// <param name="authenticate">AuthenticateWS instance with login session</param>
        /// <returns>ApxWS instance</returns>
        private ApxWS.ApxWS CreateApxWSInstance(AuthenticateWS.AuthenticateWS authenticateWS)
        {
            ApxWS.ApxWS apxws = null;
            if (authenticateWS.Login())
            {
                apxws = new ApxWS.ApxWS();
                apxws.UseDefaultCredentials = authenticateWS.UseDefaultCredentials;
                apxws.CookieContainer = authenticateWS.CookieContainer;
                this.ResolveServiceUrl(apxws);
            }
            else
            {
                throw new Exception("Autentication Falis.");
            }

            return apxws;
        }

        /// <summary>
        /// Resolve Service Url for Windows NT or non-Windows NT logins
        /// </summary>
        /// <param name="service"></param>
        private void ResolveServiceUrl(SoapHttpClientProtocol service)
        {
            // Url for Windows NT user is like "../apx/..", while url for non-Windows NT user is like "../apxlogin/.."
            if (service.UseDefaultCredentials)
            {
                service.Url = service.Url.ToLower().Replace("/apxlogin/", "/apx/");
            }
            else
            {
                service.Url = service.Url.ToLower().Replace("/apx/", "/apxlogin/");
            }
        }
    }
}
