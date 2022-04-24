namespace BooksManagement.Extensions.Services;

using BooksManagement.Databases;
using BooksManagement.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        // DbContext -- Do Not Delete
        if (env.IsEnvironment(LocalConfig.FunctionalTestingEnvName))
        {
            services.AddDbContext<BooksDbContext>(options =>
                options.UseInMemoryDatabase($"BooksManagement"));
        }
        else
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if(string.IsNullOrEmpty(connectionString))
            {
                // this makes local migrations easier to manage. feel free to refactor if desired.
                connectionString = env.IsDevelopment() 
                    ? "Host=localhost;Port=5432;Database=dev_booksmanagement;Username=postgres;Password=postgres"
                    : throw new Exception("DB_CONNECTION_STRING environment variable is not set.");
            }

            services.AddDbContext<BooksDbContext>(options =>
                options.UseNpgsql(connectionString,
                    builder => builder.MigrationsAssembly(typeof(BooksDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());
        }

        // Auth -- Do Not Delete
    }
}
