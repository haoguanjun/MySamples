
namespace Advent.APXRESTfulAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HttpClientProxy proxy = new HttpClientProxy("vmw12aoscidb4.gencos.com", "u1", "advs"))
            {
                //string result = proxy.HttpGet("apxlogin/api/odata/v1/portfolios");
            }
        }
    }
}