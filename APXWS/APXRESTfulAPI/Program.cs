
namespace Advent.APXRESTfulAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string appserver = "vmapxba9.advent.com";
            string webserver = "vmapxba9.advent.com";

            IdentityClient identityClient = new IdentityClient(appserver);

            identityClient.Authorize("api", "advs");
        }
    }
}