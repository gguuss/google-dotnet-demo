using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util;

using Newtonsoft.Json;

namespace ServiceAccountDemo
{
    class Program
    {
        // OAuth 2.0 Service account credentials settings.
        private const string SERVICE_ACCOUNT_EMAIL = "<some-id>@developer.gserviceaccount.com";
        private const string SERVICE_ACCOUNT_PKCS12_FILE_PATH = @"\path\to\<public_key_fingerprint>-privatekey.p12";

        static private Oauth2Service cachedService = null;
        static private string cachedAccessToken = "";


        /// <summary>
        /// Builds an OAuth2 service for demonstrating how to construct the
        /// object for an API call.
        /// </summary>
        /// @param userEmail The email of the user.
        /// <returns>Google OAuth 2 service object.</returns>
        static async void BuildService()
        {
            X509Certificate2 certificate = new X509Certificate2(SERVICE_ACCOUNT_PKCS12_FILE_PATH, "notasecret",
                X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(SERVICE_ACCOUNT_EMAIL)
                {
                    Scopes = new[] {"profile"}
                }.FromCertificate(certificate));

            await credential.RequestAccessTokenAsync(CancellationToken.None);
            string accessToken = credential.Token.AccessToken;

            cachedService = new Oauth2Service(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Merchant POS Demo"
            });

            Oauth2Service.TokeninfoRequest request = cachedService.Tokeninfo();
            request.AccessToken = accessToken;
            Console.WriteLine("Response: " + JsonConvert.SerializeObject(request.Execute()));
            Console.WriteLine("Input to exit.");
        }


        static void Main(string[] args)
        {
            BuildService();
            Console.ReadLine();
        }
    }
}
