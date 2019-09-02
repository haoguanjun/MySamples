namespace AdvIdentityModelSample
{
    using System;
    using IdentityModel.Client;

    class Program
    {
        static void Main(string[] args)
        {
            var client = new AdvIdentityClient
            {
                Issuer = "https://vmapxba9.advent.com:5001",
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                Scope = "apxapi offline_access"
            };

            Console.WriteLine("Windows-NT User Login");
            var response1 = client.Login();
            Program.PrintTokens(response1);

            Console.WriteLine("APX User Login");
            var response2 = client.Login("api", "advs");
            Program.PrintTokens(response2);
        }

        static void PrintTokens(TokenResponse reponse)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine("IdentityToken");
            Console.WriteLine(reponse.IdentityToken);
            Console.WriteLine("========================================================");
            Console.WriteLine("AccessToken");
            Console.WriteLine(reponse.AccessToken);
            Console.WriteLine("========================================================");
            Console.WriteLine("RefreshToken");
            Console.WriteLine(reponse.RefreshToken);
            Console.WriteLine("========================================================");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }
}
