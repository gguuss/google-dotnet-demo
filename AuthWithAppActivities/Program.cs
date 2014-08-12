using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthWithAppActivities
{
    public class AAGoogleAuthorizationCodeRequestUrl : GoogleAuthorizationCodeRequestUrl
    {
        [Google.Apis.Util.RequestParameterAttribute("request_visible_actions", Google.Apis.Util.RequestParameterType.Query)]
        public string VisibleActions { get; set; }

        public AAGoogleAuthorizationCodeRequestUrl(Uri authorizationServerUrl)
            : base(authorizationServerUrl)
        {
        }
    }

    public class AAGoogleAuthorizationCodeFlow : AuthorizationCodeFlow
    {
        /// <summary>Constructs a new Google authorization code flow.</summary>
        public AAGoogleAuthorizationCodeFlow(AuthorizationCodeFlow.Initializer initializer)
            : base(initializer)
        {
        }

        public override AuthorizationCodeRequestUrl CreateAuthorizationCodeRequest(string redirectUri)
        {
            return new AAGoogleAuthorizationCodeRequestUrl(new Uri(AuthorizationServerUrl))
            {
                ClientId = ClientSecrets.ClientId,
                Scope = string.Join(" ", Scopes),
                RedirectUri = redirectUri,
                VisibleActions = "http://schemas.google.com/AddActivity"
            };
        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Write App Activities");
            Console.WriteLine("===");
            try
            {
                new Program().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // These come from the APIs console:
        //   https://code.google.com/apis/console
        public static ClientSecrets secrets = new ClientSecrets()
        {
            ClientId = "YOUR_CLIENT_ID",
            ClientSecret = "YOUR_CLIENT_SECRET"
        };

        private async Task Run()
        {
            UserCredential credential;

            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = secrets,
                Scopes = new[] { PlusService.Scope.PlusLogin}
            };
            var flow = new AAGoogleAuthorizationCodeFlow(initializer);
            credential = await new AuthorizationCodeInstalledApp(flow, new LocalServerCodeReceiver()).AuthorizeAsync
                ("user", CancellationToken.None).ConfigureAwait(false);

            // Create the service.
            var service = new PlusService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Gus API",
                });

            Moment body = new Moment();
            ItemScope itemScope = new ItemScope();
            itemScope.Id = "replacewithuniqueforaddtarget";
            itemScope.Image = "http://www.google.com/s2/static/images/GoogleyEyes.png";
            itemScope.Type = "";
            itemScope.Description = "The description for the action";
            itemScope.Name = "An example of add activity";
            body.Object = itemScope;
            body.Type = "http://schema.org/AddAction";
            MomentsResource.InsertRequest insert =
                new MomentsResource.InsertRequest(
                    service,
                    body,
                    "me",
                    MomentsResource.InsertRequest.CollectionEnum.Vault);

            Moment wrote = insert.Execute();

            Console.WriteLine("Wrote app activity:\n" + wrote.Result);

            ActivityFeed foo = service.Activities.List("me", ActivitiesResource.ListRequest.CollectionEnum.Public).Execute();

            foreach (var aa in foo.Items){
                Console.WriteLine(aa.Title);
            }
        }
    }
}