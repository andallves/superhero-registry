using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.SwaggerGen;
using SuperHero.API.Configuration.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SuperHero.API.Configuration;

public static class SwaggerConfiguration
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.UseDateOnlyTimeOnlyStringConverters();
            
            options.OperationFilter<SwaggerDefaultValues>();
            options.DocumentFilter<LowercaseDocumentFilter>();
            
            options.OrderActionsBy(a => a.GroupName);
        });
    }

    public static void UseSwaggerConfig(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();
        
        app.UseReDoc(options =>
        {
            options.RoutePrefix = "redoc";
        });
        
        app.UseSwaggerUI(options =>
        {
            foreach (var groupName in provider.ApiVersionDescriptions.Select(c => c.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json",
                    groupName.ToUpperInvariant());
            }
            
            options.DefaultModelExpandDepth(2);
            options.DefaultModelRendering(ModelRendering.Model);
            options.DefaultModelsExpandDepth(-1);
            options.DisplayOperationId();
            options.DisplayRequestDuration();
            options.DocExpansion(DocExpansion.None);
            options.EnableDeepLinking();
            options.EnableFilter();
            options.ShowExtensions();
            options.ShowCommonExtensions();
            options.EnableValidator();
            options.UseRequestInterceptor("(request) => { return request; }");
            options.UseResponseInterceptor("(response) => { return response; }");
        });
    }
}