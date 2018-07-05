using System;
using System.Net;
using System.Threading.Tasks;

namespace Sample2.UserInfo.UI.Callers
{
    internal class UnauthenticatedCaller
    {
        internal async Task<string> Call(string functionUrl)
        {
            using (var client = new WebClient())
            {
                var data = await client.DownloadStringTaskAsync(functionUrl);
                return data;
            }
        }
    }
}
