using IdentityModel.Client;
using IdentityModel.OidcClient;
using System;

namespace AdvOidcSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Resource Owner Passord flow");
            Program.PasswordFlow();

            Console.WriteLine("Windows Authentiation flow");
            Program.WindowsAuthFlow();

            Console.WriteLine("Authorization Code flow");
            Program.AuthorizationCodeFlow();
        }

        static void AuthorizationCodeFlow()
        {
            var client = new AdvIedntityClient
            {
                Issuer = "https://vmapxba8.advent.com:5001",
                ClientId = "authcode.apxui",
                ClientSecret = "advs",
                Scope = "openid apxapi offline_access"
            };

            var result = client.Signin();
            Program.Print(result);
        }

        static void PasswordFlow()
        {
            var client = new AdvIedntityClient
            {
                Issuer = "https://vmapxba8.advent.com:5001",
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                Scope = "apxapi offline_access"
            };

            var response = client.Login("api", "advs");
            Program.Print(response);
        }

        static void WindowsAuthFlow()
        {
            var client = new AdvIedntityClient
            {
                Issuer = "https://vmapxba8.advent.com:5001",
                ClientId = "ro.APXAPIClient",
                ClientSecret = "advs",
                Scope = "apxapi offline_access"
            };

            var response = client.Login();
            Program.Print(response);
        }

        static void Print(TokenResponse reponse)
        {
            Console.WriteLine("========================================================");
            if (reponse == null)
            {
                Console.WriteLine("Object \"reponse\" is null.");
            }
            else if (reponse.IsError)
            {
                Console.WriteLine(reponse.Error);
            }
            else
            {
                Console.WriteLine("IdentityToken");
                Console.WriteLine(reponse.IdentityToken);
                Console.WriteLine("========================================================");
                Console.WriteLine("AccessToken");
                Console.WriteLine(reponse.AccessToken);
                Console.WriteLine("========================================================");
                Console.WriteLine("RefreshToken");
                Console.WriteLine(reponse.RefreshToken);
            }

            Console.WriteLine("========================================================");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        static void Print(LoginResult result)
        {
            Console.WriteLine("================================================");
            if (result == null)
            {
                Console.WriteLine("Object \"result\" is null.");
            }
            else if (result.IsError)
            {
                Console.WriteLine(result.Error);
            }
            else
            {
                Console.WriteLine("Id_Token:", result.IdentityToken);
                Console.WriteLine(result.IdentityToken);
                Console.WriteLine("================================================");
                Console.WriteLine("Refresh_Token:", result.RefreshToken);
                Console.WriteLine(result.RefreshToken);
                Console.WriteLine("================================================");
                Console.WriteLine("Access_Token:", result.AccessToken);
                Console.WriteLine(result.AccessToken);
            }

            Console.WriteLine("================================================");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }
}
