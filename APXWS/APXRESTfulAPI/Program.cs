
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
            HttpClientProxy proxy = new HttpClientProxy("vmapxba9.advent.com", "api", "advs");
            string result = proxy.HttpGet("apxlogin/api/odata/v1/portfolios");
        }
    }
}