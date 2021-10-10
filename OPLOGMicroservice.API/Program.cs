using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OPLOGMicroservice.API.Configuration;

namespace OPLOGMicroservice.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost server = BuildWebHost(args);
            server.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
             .UseStartup<Startup>()
             .ConfigureAppConfiguration((hostingContext, configBuilder) =>
             {
                 ConfigurationOptions config =
                     configBuilder.Build().GetSection("AppSettings").Get<ConfigurationOptions>();
             }).ConfigureServices(e =>
             {
             }).ConfigureKestrel((context, options) =>
             {
                 options.AllowSynchronousIO = true;
             }).Build();
    }
}
