namespace Advent.APXRESTfulAPI
{
    using System;
    using System.Net.Http;
    public class AuthFilter
    {
        public static void Logout(string webserver, string sessioncode)
        {
            string requestUrl = string.Format("http://{0}/apxlogin/Api/authenticate?logout", webserver);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("sessioncode", sessioncode);
            HttpResponseMessage response = client.GetAsync(requestUrl).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Logout Fails.");
            }
        }
    }
}
