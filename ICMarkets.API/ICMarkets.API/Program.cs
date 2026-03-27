using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ICMarkets.API.HealthChecks;
using ICMarkets.Services;
using ICMarkets.Services.Helpers;
using ICMarkets.Repository.Data;
using ICMarkets.Repository.UOW;
using ICMarkets.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlockchainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "SQL_Server")
    .Add(new HealthCheckRegistration("ETH_Main", sp => new BlockcypherHealthCheck(sp.GetRequiredService<IHttpClientFactory>(), "https://api.blockcypher.com/v1/eth/main", "ETH Mainnet"), null, null))
    .Add(new HealthCheckRegistration("DASH_Main", sp => new BlockcypherHealthCheck(sp.GetRequiredService<IHttpClientFactory>(), "https://api.blockcypher.com/v1/dash/main", "Dash Mainnet"), null, null))
    .Add(new HealthCheckRegistration("BTC_Main", sp => new BlockcypherHealthCheck(sp.GetRequiredService<IHttpClientFactory>(), "https://api.blockcypher.com/v1/btc/main", "BTC Mainnet"), null, null))
    .Add(new HealthCheckRegistration("BTC_Test3", sp => new BlockcypherHealthCheck(sp.GetRequiredService<IHttpClientFactory>(), "https://api.blockcypher.com/v1/btc/test3", "BTC Test3"), null, null))
    .Add(new HealthCheckRegistration("LTC_Main", sp => new BlockcypherHealthCheck(sp.GetRequiredService<IHttpClientFactory>(), "https://api.blockcypher.com/v1/ltc/main", "LTC Mainnet"), null, null));

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient<IBlockcypherHelper, BlockcypherHelper>();
builder.Services.AddScoped<IBlockcypherHelper, BlockcypherHelper>();
builder.Services.AddScoped<IBlockchainHistoryRepository, BlockchainHistoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBlockchainService, BlockchainService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DefaultCors");

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(x => new
            {
                component = x.Key,
                status = x.Value.Status.ToString(),
                description = x.Value.Description,
                exception = x.Value.Exception?.Message,
                duration = x.Value.Duration.ToString()
            }),
            totalDuration = report.TotalDuration.ToString()
        };
        await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented));
    }
});

app.Run();
