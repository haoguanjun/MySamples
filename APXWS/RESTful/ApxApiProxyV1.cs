
namespace Advent.ApxRestApiExample
{
    class ApxApiProxyV1 : ApxApiProxyBase
    {
        /// <summary>
        /// Connect Apx REST Web Service through Windows NT user
        /// </summary>
        /// <param name="webServer">Web Server Name</param>
        public ApxApiProxyV1(string webServer)
            : base(webServer)
        {
        }

        /// <summary>
        /// Connect Apx REST Web Service through non-Windows NT user
        /// </summary>
        /// <param name="webServer">Web Server Name</param>
        /// <param name="login">Login User Name of Non-Windows NT user</param>
        /// <param name="password">Password of Login User</param>
        public ApxApiProxyV1(string webServer, string login, string password)
            : base(webServer, login, password)
        {
        }

        /// <summary>
        /// Get all blotters
        /// </summary>
        /// <returns>Result of getting all blotters.</returns>
        public string GetBlotters()
        {
            return this.client.HttpGet("api/v1/Blotters");
        }

        /// <summary>
        /// Get specific blotter
        /// </summary>
        /// <param name="id">Blotter id</param>
        /// <returns>Result of getting blotter id</returns>
        public string GetBlotter(string id)
        {
            return this.client.HttpGet(string.Format("api/v1/blotters/{0}", id));
        }

        /// <summary>
        /// Post transactions from blotter to portfolios
        /// </summary>
        /// <param name="id">Blotter GUID</param>
        /// <returns>Posting Job information</returns>
        public string PostBlotter(string id)
        {
            return this.client.HttpPost(string.Format("api/v1/blotters/{0}?operation=post", id));
        }

        /// <summary>
        /// Get Posting Job
        /// </summary>
        /// <param name="jobId">Job GUid</param>
        /// <param name="tranColumns">Transaction Columns to return if there's any error.</param>
        /// <returns>Job information</returns>
        public string GetPostingJob(string jobId, params string[] tranColumns)
        {
            string columnFilter = null;
            if (tranColumns != null && tranColumns.Length != 0)
            {
                columnFilter = "?ColumnFilter=\"";
                for (int i = 0; i < tranColumns.Length; i++)
                {
                    columnFilter += tranColumns[i];
                    if (i == tranColumns.Length - 1)
                    {
                        columnFilter += "\"";
                    }
                    else
                    {
                        columnFilter += ",";
                    }
                }
            }

            return this.client.HttpGet(string.Format("api/v1/jobs/{0}{1}", jobId, columnFilter));
        }

        /// <summary>
        /// Get blotter lines for sepcific blotter
        /// </summary>
        /// <param name="id">Blotter id</param>
        /// <returns>Result of Getting blotter lines</returns>
        public string GetBlotterLines(string id)
        {
            return this.client.HttpGet(string.Format("api/v1/blotters/{0}/lines", id));
        }

        /// <summary>
        /// Append blotter lines for specific blotter.
        /// </summary>
        /// <param name="id">Blotter id</param>
        /// <param name="jsonLines">New lines in json format</param>
        /// <returns>Resoult of appending blotter lines</returns>
        public string AppendBlotterLines(string id, string jsonLines)
        {
            return this.client.HttpPost(string.Format("api/v1/blotters/{0}/lines", id), jsonLines);
        }

        /// <summary>
        /// Delete all blotter lines
        /// </summary>
        /// <param name="id">Blotter id</param>
        /// <returns>Result of deleting all blotter lines</returns>
        public string DeleteBlotterLines(string id)
        {
            return this.client.HttpDelete(string.Format("api/v1/blotters/{0}/lines", id));
        }
    }
}
