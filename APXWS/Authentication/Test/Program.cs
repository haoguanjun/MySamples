using Authentication;
using System;
using System.Net.Http;
using System.Net;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //AuthFilterClient client = new AuthFilterClient("http://vmapxba9.advent.com");
            //client.Login();
            //client.Login("api", "advs");

            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //httpClient.DefaultRequestHeaders.Add("sessioncode", "34D534FC-F3C2-4760-B778-CC7C4CCF2A31");
            //string requestUrl = "http://vmapxba9.advent.com/apxlogin/api/v1/blotters";
            //HttpResponseMessage response = httpClient.GetAsync(requestUrl).Result;
            //string result= response.Content.ReadAsStringAsync().Result;

            //client.Logout();

            IdentityClient client = new IdentityClient("https://vmapxba9.advent.com:5001");
            client.RequestAuthorizationCode();
        }
    }
}
