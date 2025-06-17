using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperHero.Domain.Settings;
using SuperHero.Infra;
using AutoMapper;
using MediatR;
using SuperHero.Infra.Context;

namespace SuperHero.Application;

public static class DependencyInjection
{
    public static void SetupSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationSettings>(configuration.GetSection(ApplicationSettings.SectionName));
    }
    
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.ConfigureDbContext(configuration);
        
        services.AddRepositories<SuperHeroDbContext>();
        
        services
            .AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services
            .AddMediatR(Assembly.GetExecutingAssembly());
    }
}