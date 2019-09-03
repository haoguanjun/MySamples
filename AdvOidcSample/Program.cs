using IdentityModel.Client;
using IdentityModel.OidcClient;
using System;

namespace AdvOidcSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.Example_AuthorizationCode();
        }

        static void Example_AuthorizationCode()
        {
            // insert into dbo.ClientRedirectUris(ClientId, RedirectUri) values((select Id from Clients where ClientId = 'authcode.apxui'),'http://localhost:5002/')
            var client = new AdvIedntityClient
            {
                Issuer = "https://vmapxba8.advent.com:5001",
                ClientId = "authcode.apxui",
                ClientSecret = "advs",
                Scope = "openid apxapi offline_access",
                RedirectUri = "http://localhost:5002/"
            };

            var result = client.Signin();
            Program.Print(result);
        }

        static void Example_Password()
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

        static void Example_WindowsAuth()
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
