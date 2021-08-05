using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
using System.Linq;

namespace ApiRobustas.Api
{
    public class Program
    {
        private const string LogServerUrl = @"http://localhost:5341";
        private static readonly string ENVIRONMENT =
           Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Production;

        private static IConfiguration _configurationForLogging;

        public static int Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            // .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            // .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            // .Enrich.FromLogContext()
            // .WriteTo.Console()
            // .CreateLogger();

            _configurationForLogging = CreateConfigurationForLogging(args);

            Log.Logger = CreateLogger();

            try
            {
                Log.Information("Inicializando a aplicação.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "A aplicação terminou inesperadamente.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

        private static IConfiguration CreateConfigurationForLogging(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{ENVIRONMENT}.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        private static Logger CreateLogger()
        {
            return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("healthcheck")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("healthcheck-ui")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("HealthChecksDb")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("HealthChecksUI")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("healthchecks-data-ui")))
            .WriteTo.Console()
            .WriteTo.Seq(Environment.GetEnvironmentVariable("SEQ_URL") ?? LogServerUrl)
            .CreateLogger();

            //var logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(_configurationForLogging)
            //    .MinimumLevel.Debug()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            //    .Enrich.FromLogContext()
            //    .Enrich.WithMachineName()
            //    .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("healthcheck")))
            //    .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("healthcheck-ui")))
            //    .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("HealthChecksDb")))
            //    .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("HealthChecksUI")))
            //    .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("healthchecks-data-ui")))
            //    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
            //    .Enrich.WithProperty("Environment", ENVIRONMENT)
            //    .WriteTo.Seq(Environment.GetEnvironmentVariable("SEQ_URL") ?? LogServerUrl);
        }
    }
}
