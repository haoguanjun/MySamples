
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
            //using (ApxApiProxyV1 proxy = new ApxApiProxyV1("vmapxba9.advent.com", "api", "advs"))
            using (ApxApiProxyV1 proxy = new ApxApiProxyV1("vmapxba9.advent.com"))
            {
                string result = proxy.GetBlotters();
            }
        }
    }
}