
namespace Advent.APXRESTfulAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string appserver = "vmapxba9.advent.com";
            string webserver = "vmapxba9.advent.com";

            using (TokenAuth auth = new TokenAuth(appserver, "api", "advs"))
            {
                using (HttpProxy http = new HttpProxy(webserver, auth.Token))
                {
                    string result1 = http.Get("fxtypes");
                    auth.Dispose();
                    string result2 = http.Get("fxtypes");
                }

                AuthFilter.Logout(webserver, auth.SessionCode);
            }
        }
    }
}