using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            var host = BuildWebHost(configuration, args);
            host.Run();
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
              WebHost.CreateDefaultBuilder(args)
             .CaptureStartupErrors(false)
             .UseStartup<Startup>()
             .UseWebRoot("Logs")
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseConfiguration(configuration)
             .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            if (config.GetValue<bool>("UseVault", false))
            {

            }

            return builder.Build();
        }
    }
}
