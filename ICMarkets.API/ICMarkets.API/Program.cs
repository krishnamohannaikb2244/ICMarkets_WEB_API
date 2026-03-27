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

app.UseAuthorization();

app.MapControllers();

app.Run();
