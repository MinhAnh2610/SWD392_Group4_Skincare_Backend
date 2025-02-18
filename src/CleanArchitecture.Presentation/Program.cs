using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Data.Extensions;
using CleanArchitecture.Presentation;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var fullVersion = Assembly.GetExecutingAssembly()
                          .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                          .InformationalVersion ?? "1.0.0";

// Trim the commit hash suffix if it exists
var version = fullVersion.Split('+')[0];


// Add services to the container.
builder.Services
  .AddApplicationServices(builder.Configuration)
  .AddInfrastructureServices(builder.Configuration)
  .AddApiServices(builder.Configuration);

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
  app.UseSwagger(options =>
  {
    //https://localhost:5051/scalar/
    options.RouteTemplate = "/openapi/{documentName}.json";
  });
  app.MapScalarApiReference();
  app.UseSwaggerUI(c =>
  //https://localhost:5051/swagger/
  {
    c.SwaggerEndpoint("/openapi/v1.json", "De Fleur API");
    //c.RoutePrefix = string.Empty;
  });
  await app.InitializeDatabaseAsync();
}

app.UseApiServices();

app.Run();
