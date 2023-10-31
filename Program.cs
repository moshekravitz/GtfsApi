using Catalog.repositories;
using GtfsApi.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Driver;
using System.Net.Mime;
using System.Text.Json;
using Catalog.Attributes;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Http.Features;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string ConnectionString = Environment.GetEnvironmentVariable("MONGO_URL");
        //string ConnectionString = "mongodb://localhost:27017/";
        builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            return new MongoClient(ConnectionString);
        });
        builder.Services.AddSingleton<ApiKeyAttribute>();
        builder.Services.AddSingleton<ApiKeyAdminAttribute>();
        builder.Services.AddSingleton<IRoutesRepo, MongoDbRoutesDepository>();
        builder.Services.AddSingleton<IExtendedRoutesRepo, MongoDbExtendedRoutesDepository>();
        builder.Services.AddSingleton<IStopInfoRepo, MongoDbStopInfoDepository>();
        builder.Services.AddSingleton<IShapesRepo, MongoDbShapesDepository>();
        
        /* builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 1000000000; // 1 GB
            options.ValueLengthLimit = int.MaxValue;
            options.ValueCountLimit = int.MaxValue;
            options.KeyLengthLimit = int.MaxValue;
            options.MultipartHeadersLengthLimit = int.MaxValue;
        }); */
        

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        /*.AddMongoDb(Environment.GetEnvironmentVariable("MONGO_URL"), */
        builder.Services.AddHealthChecks()
            .AddMongoDb(ConnectionString,
                name: "mongodb",
                timeout: TimeSpan.FromSeconds(3),
                tags: new[] { "ready" });

        builder.Services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.RequestProperties;
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseHttpsRedirection();
        }


        app.UseHttpLogging();

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
