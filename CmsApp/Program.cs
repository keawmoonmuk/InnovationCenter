// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CmsApp.Helpers;

namespace CmsApp
 {
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Console.WriteLine("host :" + host);

            //Seed database
            using (var scope = host.Services.CreateScope())
            {
                //get service
                var services = scope.ServiceProvider;
                Console.WriteLine("is scope : " + scope);
                Console.WriteLine("is services : "+ services);
                //Conected database sucess..
                try
                {
                    //get initializer  default
                    var databaseInitializer = services.GetRequiredService<IDatabaseInitializer>();
                    Console.WriteLine("database Initializer : " + databaseInitializer);

                    databaseInitializer.SeedAsync().Wait();
                }
                //Not Coneected database 
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogCritical(LoggingEvents.INIT_DATABASE, ex, LoggingEvents.INIT_DATABASE.Name);

                    throw new Exception(LoggingEvents.INIT_DATABASE.Name, ex);
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })  
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                    logging.AddFile(hostingContext.Configuration.GetSection("Logging"));
                });
    }
}
