using Serilog;
using SproutSocial.API.Extensions.ApplicationExtensions;
using SproutSocial.API.Extensions.ServiceExtensions;
using SproutSocial.Application;
using SproutSocial.Application.Helpers.Settings;
using SproutSocial.Infrastructure;
using SproutSocial.Infrastructure.Services.Storage.Local;
using SproutSocial.Persistence;
using SproutSocial.Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrasturctureServices();
builder.Services.AddQuartzServices();
builder.Services.AddStorage<LocalStorage>();

builder.Services.AddApiVersionService(builder.Configuration["Version"]);

builder.Services.AddApiService();

builder.Services.AddCorsService(builder.Configuration.GetSection("Client:Urls").Get<string[]>());

builder.Host.AddSerilogService(builder.Configuration["ConnectionStrings:Default"]);
builder.Services.AddHttpLogingService();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

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

app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
