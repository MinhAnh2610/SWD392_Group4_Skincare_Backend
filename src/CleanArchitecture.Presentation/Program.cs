using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Data.Extensions;
using CleanArchitecture.Presentation;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
  .AddApplicationServices(builder.Configuration)
  .AddInfrastructureServices(builder.Configuration)
  .AddApiServices(builder.Configuration);
var config = new TypeAdapterConfig();
config.Apply(new CosmeticMappingConfig());
builder.Services.AddSingleton(config);
builder.Services.AddMapster();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  await app.InitializeDatabaseAsync();
}

app.UseApiServices();

app.Run();