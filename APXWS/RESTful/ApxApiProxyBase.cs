

namespace Advent.ApxRestApiExample
{
    using System;

    public class ApxApiProxyBase : IDisposable
    {
        protected HttpClientProxy client;

        /// <summary>
        /// Connect Apx REST Web Service through Windows NT user
        /// </summary>
        /// <param name="webServer">Web Server Name</param>
        public ApxApiProxyBase(string webServer)
        {
            this.client = new HttpClientProxy(webServer);
        }

        public ApxApiProxyBase(string webServer, string login, string password)
        {
            this.client = new HttpClientProxy(webServer, login, password);
        }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
