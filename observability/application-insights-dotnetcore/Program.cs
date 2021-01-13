using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace application_insight_dotnetcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.AddEventLog();
                    }
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        // Enable diagnostics logging for apps in Azure App Service

        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //             Host.CreateDefaultBuilder(args)
        //                 .ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics())
        //                 .ConfigureServices(serviceCollection => serviceCollection
        //                     .Configure<AzureFileLoggerOptions>(options =>
        //                     {
        //                         options.FileName = "azure-diagnostics-";
        //                         options.FileSizeLimit = 50 * 1024;
        //                         options.RetainedFileCountLimit = 5;
        //                     })
        //                     .Configure<AzureBlobLoggerOptions>(options =>
        //                     {
        //                         options.BlobName = "log.txt";
        //                     }))
        //                 .ConfigureWebHostDefaults(webBuilder =>
        //                 {
        //                     webBuilder.UseStartup<Startup>();
        //                 });
        // }
    }
}
