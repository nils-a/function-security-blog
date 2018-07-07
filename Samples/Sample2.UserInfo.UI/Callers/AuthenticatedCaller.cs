using Microsoft.OneDrive.Sdk.Authentication;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sample2.UserInfo.UI
{
    internal class AuthenticatedCaller
    {
        internal async Task<string> Call(string functionUrl)
        {
            // Step 1:
            var providerToken = await AuthenticateToProvider();

            // Step 2:
            var easyAuthToken = await AuthenticateToAzure(functionUrl, providerToken);

            //
            // Step3: Use the token we got from "EasyAuth" 
            //
            using (var client = new WebClient())
            {
                client.Headers.Add("X-ZUMO-AUTH", easyAuthToken);
                var data = await client.DownloadStringTaskAsync(functionUrl);
                return data;
            }
        }

        private async Task<string> AuthenticateToAzure(string function, string accessToken)
        {
            using (var client = new HttpClient())
            {
                var azureBaseUrl = function.Substring(0, function.IndexOf("/api", StringComparison.InvariantCultureIgnoreCase)); // everything before "/api/..."
                var AzureLoginUrl = $"{azureBaseUrl}/.auth/login/microsoftaccount"; //  ../login/micrososftaccount, not ../login/microsoft as most of the docs say...
                var request = new HttpRequestMessage(HttpMethod.Post, AzureLoginUrl);
                var reqJson = new JObject
                {
                    ["access_token"] = accessToken
                };
                request.Content = new StringContent(reqJson.ToString());
                request.Content.Headers.ContentType.MediaType = "application/json";

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error {response.StatusCode} requesting EasyAuthToken: {response.ReasonPhrase}", null);
                }
                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    var json = JObject.Parse(await reader.ReadToEndAsync());
                    return json["authenticationToken"].ToString();
                }
            }
        }

        private async Task<string> AuthenticateToProvider()
        {
            const string clientId = "1e872776-dbba-47e5-8035-3e2e4ab8a4e6"; // register this under https://apps.dev.microsoft.com/
            var authenticationProvider = new MsaAuthenticationProvider(
                    clientId,
                    "https://login.live.com/oauth20_desktop.srf",           // this is the login-url for Microsoft
                    new string[] { "wl.signin", "wl.basic" },               // https://docs.microsoft.com/en-us/previous-versions/office/developer/onedrive-live-sdk-reference/dn631845(v=office.15)
                    new CredentialVault(clientId));

            //TODO: In real app there are many more tests needed. 
            // e.g. it is possible the restored Session from cache is valid but the attached accesstoken is already expired...
            await authenticationProvider.RestoreMostRecentFromCacheOrAuthenticateUserAsync();
            return authenticationProvider.CurrentAccountSession.AccessToken;
        }

    }
}
