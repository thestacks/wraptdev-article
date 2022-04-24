
namespace BooksManagement.FunctionalTests;

using BooksManagement.Databases;
using BooksManagement.Resources;
using BooksManagement;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class TestingWebApplicationFactory : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(LocalConfig.FunctionalTestingEnvName);

        builder.ConfigureServices(services =>
        {
            // Create a new service provider.
            var provider = services.BuildServiceProvider();

            // Add a database context (BooksDbContext) using an in-memory database for testing.
            services.AddDbContext<BooksDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(provider);
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (BooksDbContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<BooksDbContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();
            }
        });
    }
}