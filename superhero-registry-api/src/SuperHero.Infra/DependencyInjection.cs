using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperHero.Infra.Context;
using SuperHero.Infra.Database;

namespace SuperHero.Infra;

public static class DependencyInjection
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        

        var connectionString = configuration.GetConnectionString("POSTGRES")!;
        services
            .AddDbContext<SuperHeroDbContext>(
                x => x.UseNpgsql(connectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.CommandTimeout(60);
                        npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(45), null);
                    })
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    #if DEBUG
                    .EnableSensitiveDataLogging()
                    #endif
            );
        
    }
    
     public static void AddRepositories<T>(this IServiceCollection services) where T : DbContext
     {
        services.AddScoped<IRepository<T>, Repository<T>>();
    }
    
    
    public static void UseMigrations(this IApplicationBuilder app, IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SuperHeroDbContext>();
        db.Database.Migrate();
    }
}