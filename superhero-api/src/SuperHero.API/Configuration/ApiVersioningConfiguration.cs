using Microsoft.AspNetCore.Mvc;

namespace IFCE.SysRA.API.Configuration;

public static class ApiVersioningConfiguration
{
    public static void AddVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
            })
            .AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
    }
}