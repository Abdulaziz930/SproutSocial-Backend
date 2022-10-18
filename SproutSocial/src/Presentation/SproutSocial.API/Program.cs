using SproutSocial.API.Extensions.ApplicationExtensions;
using SproutSocial.API.Extensions.ServiceExtensions;
using SproutSocial.Application;
using SproutSocial.Infrastructure;
using SproutSocial.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrasturctureServices();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddApiService();

builder.Services.AddJwtAuthenticationService(builder.Configuration["Jwt:Audience"], builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:SigningKey"]);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.AddInitializeApplicationService();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
