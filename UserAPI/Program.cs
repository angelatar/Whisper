using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace UserAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost
                        .CreateDefaultBuilder(args)
                        .UseUrls("http://192.168.88.136:61366") // <--add urls
                        .UseStartup<Startup>()
                        .Build();
        }
    }
}
