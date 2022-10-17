using SproutSocial.API.Extensions;
using SproutSocial.Application;
using SproutSocial.Infrastructure;
using SproutSocial.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrasturctureServices();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddApiService();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.AddMigrationService();

app.MapControllers();

app.Run();
