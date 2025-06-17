using System.Globalization;
using IFCE.SysRA.API.Configuration;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Logging;
using SuperHero.API.Configuration;
using SuperHero.Application;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .Configure<RequestLocalizationOptions>(o => 
    {
        var supportedCultures = new[] { new CultureInfo("pt-BR") };
        o.DefaultRequestCulture = new RequestCulture("pt-BR", "pt-BR");
        o.SupportedCultures = supportedCultures;
        o.SupportedUICultures = supportedCultures;
    });

builder
    .Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder
    .Services
    .SetupSettings(builder.Configuration);

builder
    .Services
    .AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
    });

builder
    .Services
    .AddApiConfiguration();

builder.Services.ConfigureApplication(builder.Configuration, builder.Environment);

builder
    .Services.
    AddVersioning();

builder
    .Services
    .AddSwagger();


var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("pt-BR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.UseApiConfiguration(app.Services, app.Environment);

if (!app.Environment.IsProduction())
{
    IdentityModelEventSource.ShowPII = true;
    app.UseSwaggerConfig();
}

app.UseResponseCompression();

app.UseHttpsRedirection();


// app.UseMiddleware<ExceptionMiddleware>();
//
// app.UseMiddleware<LogResponseTracingMiddleware>();


app.MapControllers();

await app.RunAsync();