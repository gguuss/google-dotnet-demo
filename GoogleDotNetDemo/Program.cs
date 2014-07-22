using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDotNetDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(@"Input an Access token:");
            String accessToken = Console.ReadLine();

            Oauth2Service service = new Oauth2Service(
                new Google.Apis.Services.BaseClientService.Initializer());
            Oauth2Service.TokeninfoRequest request = service.Tokeninfo();
            request.AccessToken = accessToken;

            Tokeninfo info = request.Execute();
            Console.Write(@"Scope: " + info.Scope + "\n");
            Console.WriteLine(@"Expires: " + info.ExpiresIn);
        }
    }
}