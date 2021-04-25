namespace Cornplex.Infrastructure.Extension
{
    using System.Reflection;
    using Cornplex.Persistence;
    using Cornplex.Service.Features.User.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        public static void AddServiceLayer(this IServiceCollection services)
        {
            // Get the name of the assembly
            services.AddMediatR(typeof(GetAllUserQuery).GetTypeInfo().Assembly);
        }

        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }

        //public static void AddController(this IServiceCollection serviceCollection)
        //{
        //    serviceCollection.AddControllers().AddNewtonsoftJson();
        //}

        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddSwagger(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cornplex.Api", Version = "v1" });
            });
        }


    }
}
