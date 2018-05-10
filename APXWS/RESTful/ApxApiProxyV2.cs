using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.ApxRestApiExample
{
    public class ApxApiProxyV2 : ApxApiProxyBase
    {
        /// <summary>
        /// Connect Apx REST Web Service through Windows NT user
        /// </summary>
        /// <param name="webServer">Web Server Name</param>
        public ApxApiProxyV2(string webServer)
            : base(webServer)
        {
        }

        /// <summary>
        /// Connect Apx REST Web Service through non-Windows NT user
        /// </summary>
        /// <param name="webServer">Web Server Name</param>
        /// <param name="login">Login User Name of Non-Windows NT user</param>
        /// <param name="password">Password of Login User</param>
        public ApxApiProxyV2(string webServer, string login, string password)
            : base(webServer, login, password)
        {
        }

        public string GetBlotters(string filter = null, string columns = null)
        {
            string url = "api/v2/blotters";
            this.CombineUrl(url, filter, columns);
            return this.client.HttpGet(url);
        }

        public string GetBlotter(string guid, string columns = null)
        {
            string url = string.Format("api/v2/blotters({0})", guid);
            this.CombineUrl(url, null, columns);
            return this.client.HttpGet(url);
        }

        public string CreateBlotter(string request)
        {
            string url = "api/v2/blotters";
            return this.client.HttpPost(url, request);
        }

        public string UpdateBlotter(string guid, string request)
        {
            string url = string.Format("api/v2/blotters({0})", guid);
            return this.client.HttpPatch(url, request);
        }

        public string DeleteBlotter(string guid)
        {
            string url = string.Format("api/v2/blotters({0})/delete", guid);
            return this.client.HttpPost(url);
        }

        public string GetBloterLines(string guid, string filter = null, string columns = null)
        {
            string url = string.Format("api/v2/blotters({0})/GetBlotterLines", guid);
            this.CombineUrl(url, filter, columns);
            return this.client.HttpGet(url);
        }

        public string AppendBlotterLines(string guid, string request)
        {
            string url = string.Format("api/v2/blotters({0})/AppendBlotterLines", guid);
            return this.client.HttpPost(url, request);
        }

        public string DeleteBlotterLines(string guid, string request)
        {
            string url = string.Format("api/v2/blotters({0})/DeleteBlotterLines", guid);
            return this.client.HttpPost(url, request);
        }

        public string DeleteBlotterLines(string guid)
        {
            string url = string.Format("api/v2/blotters({0})/DeleteBlotterLines", guid);
            return this.client.HttpPost(url);
        }

        public string PostBlotter(string guid)
        {
            string url = string.Format("api/v2/blotters({0})/PostBlotter", guid);
            return this.client.HttpPost(url);
        }

        public string PostBlotter(string guid, string request)
        {
            string url = string.Format("api/v2/blotters({0})/PostBlotter", guid);
            return this.client.HttpPost(url, request);
        }

        private string CombineUrl(string url, string filter, string columns)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                url = string.Format("{0}?filter={1}", url, filter);
                if (!string.IsNullOrEmpty(columns))
                {
                    url = string.Format("{0}&select={1}", url, columns);
                }
            }
            else if (!string.IsNullOrEmpty(columns))
            {
                url = string.Format("{0}?select={1}", url, columns);
            }

            return url;
        }
    }
}
