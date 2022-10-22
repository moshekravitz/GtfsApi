using Catalog.repositories;
using GtfsApi.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Configuration;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;
using Catalog.Attributes;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            return new MongoClient(Environment.GetEnvironmentVariable("MONGO_URL"));
        });
        builder.Services.AddSingleton<ApiKeyAttribute>();
        builder.Services.AddSingleton<ApiKeyAdminAttribute>();
        builder.Services.AddSingleton<IRoutesRepo, MongoDbRoutesDepository>();
        builder.Services.AddSingleton<IExtendedRoutesRepo, MongoDbExtendedRoutesDepository>();
        builder.Services.AddSingleton<IStopTimesRepo, MongoDbStopTimesListDepository>();
        builder.Services.AddSingleton<IStopInfoRepo, MongoDbStopInfoDepository>();
        builder.Services.AddSingleton<IRouteToDateRepo, MongoDbRouteToDateDepository>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHealthChecks()
            .AddMongoDb(Environment.GetEnvironmentVariable("MONGO_URL"),
                name: "mongodb",
                timeout: TimeSpan.FromSeconds(3),
                tags: new[] { "ready" });



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
        }


        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = (check) => check.Tags.Contains("ready"),
            ResponseWriter = async (context, report) =>
            {
                var result = JsonSerializer.Serialize(
                    new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(entry => new
                        {
                            name = entry.Key,
                            status = entry.Value.Status.ToString(),
                            exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                            duration = entry.Value.Duration.ToString()
                        })
                    }
                );
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(result);
            }
        });

        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = (_) => false
        });

        app.Run();
    }
}
