
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
            using (ApxInternalApiProxy proxy = new ApxInternalApiProxy("https://vmw16apxcloud01.gencos.com", "admin", "advs"))
            {
                var result = proxy.GetUserInfo();
            }
        }
    }
}