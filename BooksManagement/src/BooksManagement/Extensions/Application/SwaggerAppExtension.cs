namespace BooksManagement.Extensions.Application;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;
using BooksManagement.Middleware;

public static class SwaggerAppExtension
{
    public static void UseSwaggerExtension(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            config.SwaggerEndpoint("/swagger/v1/swagger.json", "");
            config.DocExpansion(DocExpansion.None);
        });
    }
}