
namespace Advent.APXRESTfulAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string appserver = "vmapxba9.advent.com";
            string webserver = "vmapxba9.advent.com";

            using (IdentityClient identity = new IdentityClient(appserver, "api", "advs"))
            {
                using (HttpProxy http = new HttpProxy(webserver, identity.AccessToken))
                {
                    string result1 = http.Get("fxtypes");
                    identity.Dispose();
                    string result2 = http.Get("fxtypes");
                }
            }
        }
    }
}