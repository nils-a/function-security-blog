using System;
using System.Net;

namespace Sample1.SimpleCall
{
    class Program
    {
        static void Main(string[] args)
        {
            const string function = "https://securityexample.azurewebsites.net/api/HelloWorld";
            const string code = "Srs8ca27tCSwuyFTv0kBOwMt/WTzLXW43WNcPUYeG7L273TmQTSf0A==";

            using (var client = new WebClient())
            {
                Console.WriteLine("Sending code via get-parameter...");
                var fullUrl = $"{function}?code={code}";
                var data = client.DownloadString(fullUrl);
                Console.WriteLine(data);
            }

            using (var client = new WebClient())
            {
                Console.WriteLine("Sending code via HTTP-Header...");
                client.Headers.Add("x-functions-key", code);
                var data = client.DownloadString(function);
                Console.WriteLine(data);
            }


#if DEBUG
            Console.WriteLine("Done");
            Console.ReadKey();
#endif
        }
    }
}
