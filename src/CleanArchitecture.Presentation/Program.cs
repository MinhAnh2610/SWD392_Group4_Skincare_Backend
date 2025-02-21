using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Data.Extensions;
using CleanArchitecture.Presentation;
using Mapster;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
  .AddApplicationServices(builder.Configuration)
  .AddInfrastructureServices(builder.Configuration)
  .AddApiServices(builder.Configuration);
// Register Mapster and the mapping configuration
var config = new TypeAdapterConfig();
config.Apply(new CosmeticMappingConfig());
builder.Services.AddSingleton(config);
builder.Services.AddMapster();
builder.Services.AddSwaggerGen(opt =>
            {
              opt.SwaggerDoc("v1",
              new OpenApiInfo
              {
                Title = "De Fleur API - " + version,
                Version = version
              }
               );
            });
   var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  await app.InitializeDatabaseAsync();
}

app.UseApiServices();

app.Run();
