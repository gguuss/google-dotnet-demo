using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthFromConsole
{
    class Program
    {
        // These come from the APIs console:
        //   https://code.google.com/apis/console
        public static ClientSecrets secrets = new ClientSecrets()
        {
            ClientId = "YOUR_CLIENT_ID",
            ClientSecret = "YOUR_CLIENT_SECRET"
        };


        static void Main(string[] args)
        {
            Console.WriteLine(@"Starting authorization...");

            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets,
                new[] { PlusService.Scope.PlusLogin },
                "me",
                CancellationToken.None).Result;

            // Create the service.
            var plusService = new PlusService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Console Google+ Demo",
            });

            Person me = plusService.People.Get("me").Execute();

            Console.Write(@"Authorized user: " + me.DisplayName + "\n");
            Console.Write(@"Press enter to exit.");
            Console.Read();
        }
    }
}
