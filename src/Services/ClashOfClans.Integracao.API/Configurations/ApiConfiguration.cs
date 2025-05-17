using Microsoft.AspNetCore.ResponseCompression;

namespace ClashOfClans.Integracao.API.Configurations;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddResponseCompression(options => {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.EnableForHttps = true;
        });
        services.AddMemoryCache();
        services.AddHealthChecks();
        //services.ConfigureProblemDetailsModelState();

        return services;
    }

    //public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    //{
    //    app.UseResponseCompression();

    //    if (env.IsDevelopment())
    //    {
    //        app.UseDeveloperExceptionPage();
    //    }

    //    //app.UseHttpsRedirection();

    //    app.UseRouting();

    //    app.UseAuthConfiguration();

    //    ILogger logger = app.ApplicationServices.GetService<ILogger<Startup>>();
    //    app.UseProblemDetailsExceptionHandler(logger);

    //    app.UseWhen(
    //        context => context.Request.Path.StartsWithSegments("/api"),
    //        appBuilder => appBuilder.UseMiddleware<TenantCacheMiddleware>()
    //        );
    //    app.UseEndpoints(endpoints =>
    //    {
    //        endpoints.MapControllers();
    //    });

    //    app.UseHealthChecks(
    //        "/healthcheck",
    //        new HealthCheckOptions
    //        {
    //            ResponseWriter = async (context, report) => {
    //                var result = JsonSerializer.Serialize(
    //                    new
    //                    {
    //                        status = report.Status.ToString(),
    //                        checks = report.Entries.Select(c => new { check = c.Key, result = c.Value.Status.ToString() })
    //                    }
    //                );

    //                context.Response.ContentType = MediaTypeNames.Application.Json;
    //                await context.Response.WriteAsync(result);
    //            }
    //        });

    //    return app;
    //}
}
