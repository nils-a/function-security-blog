using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace SecurityExample
{
    public static class UserInfo
    {
        [FunctionName("UserInfo")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var principal = ClaimsPrincipal.Current;
            if(principal == null || !principal.Identity.IsAuthenticated)
            {
                return req.CreateResponse(HttpStatusCode.OK, "Unauthenticated", "text/plain");
            }

            var identity = principal.Identity;

            var sb = new StringBuilder();
            sb.AppendLine($"Authenticated: {identity.Name} ({identity.AuthenticationType})");

            if (principal.Claims.Any())
            {
                sb.AppendLine($"Claims:");
                foreach (var c in principal.Claims)
                {
                    sb.AppendLine($" - {c.Type}: {c.Value}");
                }
            }

            return req.CreateResponse(HttpStatusCode.OK, sb.ToString(), "text/plain");
        }
    }
}
