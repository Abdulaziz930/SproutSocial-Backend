using SproutSocial.API.Extensions.ApplicationExtensions;
using SproutSocial.API.Extensions.ServiceExtensions;
using SproutSocial.Application;
using SproutSocial.Infrastructure;
using SproutSocial.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrasturctureServices();

builder.Services.AddApiVersionService(builder.Configuration["Version"]);

builder.Services.AddApiService();

builder.Services.AddJwtAuthenticationService(builder.Configuration["Jwt:Audience"], builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:SigningKey"]);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"SproutSocial v1");
        //options.SwaggerEndpoint($"/swagger/v2/swagger.json", $"SproutSocial v2");
    });

    app.AddInitializeApplicationService();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
