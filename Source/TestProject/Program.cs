using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace TestProject {
    public class Program {
        public static void Main(string[] args) {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("serilog.json")
                 .AddJsonFile($"serilog.{environmentName}.json")
                 .Build();

            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();

            try {
                Log.Information($"Starting web host at Production environment");
                CreateHostBuilder(args).Build().Run();
            } catch(Exception ex) {
                Log.Fatal(ex, "Host terminated unexpectedly");
                throw ex;
            }
            finally {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
