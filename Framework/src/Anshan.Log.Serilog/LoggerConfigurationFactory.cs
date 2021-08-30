using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Anshan.Log.Serilog
{
    public static class LoggerConfigurationFactory
    {
        public static Logger Create(Assembly assembly)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;

            return isDevelopment ? CreateForDevelopment() : CreateForProduction(assembly);
        }

        private static Logger CreateForDevelopment()
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile($"appsettings.{Environments.Development}.json", false, true)
                                .Build();
            return new LoggerConfiguration()
                   .Enrich.FromLogContext()
                   .Enrich.WithExceptionDetails()
                   .Enrich.WithMachineName()
                   .WriteTo.Debug()
                   .WriteTo.Console()
                   .Enrich.WithProperty("Environment", Environments.Development)
                   .ReadFrom.Configuration(configuration)
                   .CreateLogger();
        }

        private static Logger CreateForProduction(Assembly assembly)
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", false, true)
                                .Build();


            return new LoggerConfiguration()
                   .Enrich.FromLogContext()
                   .Enrich.WithExceptionDetails()
                   .Enrich.WithMachineName()
                   .WriteTo.Debug()
                   .WriteTo.Console()
                   .WriteTo.Elasticsearch(ConfigureElasticSink(assembly, configuration, Environments.Production))
                   .Enrich.WithProperty("Environment", Environments.Production)
                   .ReadFrom.Configuration(configuration)
                   .CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(Assembly assembly,
                                                                     IConfigurationRoot configuration,
                                                                     string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat =
                    $"{assembly.GetName().Name?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}