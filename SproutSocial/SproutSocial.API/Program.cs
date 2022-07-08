using Microsoft.AspNetCore.Identity;
using SproutSocial.API.Extensions.ApplicationExtensions;
using SproutSocial.API.Extensions.ServiceExtensions;
using SproutSocial.Core;
using SproutSocial.Core.Entities;
using SproutSocial.Data;
using SproutSocial.Service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddApiService();

//dotnet ef --startup-project ..\SproutSocial.API migrations add InitialMigration
builder.Services.AddDatabaseConnectionService(configuration, "DefaultConnection");
builder.Services.AddIdentityService<AppUser, IdentityRole>();

builder.Services.AddAuthenticationService(configuration);

builder.Services.AddMapperService();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddService(ServiceLifetime.Scoped, typeof(ITopicService));
builder.Services.AddHelperService();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConstants(configuration);

builder.Services.AddConfigureService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseExceptionHandlerMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseSeedData();

app.Run();
