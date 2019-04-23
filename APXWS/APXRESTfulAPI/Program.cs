
namespace Advent.ApxRestApiExample
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            //using (HttpClientProxy proxy = new HttpClientProxy("vmapxba8.advent.com", "api", "advs"))
            using (HttpClientProxy proxy = new HttpClientProxy("vmapxba8.advent.com"))
            {
                string result = proxy.HttpGet("apxlogin/api/odata/v1/portfolios");
            }
        }
    }
}