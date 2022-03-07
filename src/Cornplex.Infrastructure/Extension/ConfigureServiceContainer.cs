namespace Cornplex.Infrastructure.Extension
{
    using System.Reflection;
    using Cornplex.Persistence;
    using Cornplex.Persistence.IRepositories;
    using Cornplex.Persistence.Repositories;
    using Cornplex.Persistence.Repositories.Cached;
    using Cornplex.Service.Contract;
    using Cornplex.Service.Features.User.Queries;
    using Cornplex.Service.Implementation;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Microsoft.FeatureManagement;
    using Cornplex.Domain.Settings;
    using System;

    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            var connectionString = configuration["PostgreSqlDbConnection"];

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            Console.WriteLine(connectionString);
        }

        public static void AddServiceLayer(this IServiceCollection services)
        {
            // Get the name of the assembly
            services.AddMediatR(typeof(GetAllUserQuery).GetTypeInfo().Assembly);
            services.AddTransient<IEmailService, MailService>();
        }

        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();
            services.Decorate<IUserRepository, CachedUserRepository>(); // cache
        }

        public static void AddMailSetting(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        }

        public static void AddOtherServices(this IServiceCollection services)
        {
            services.AddFeatureManagement();
        }

        //public static void AddController(this IServiceCollection services)
        //{
        //    services.AddControllers().AddNewtonsoftJson();
        //}

        public static void AddVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cornplex.Api", Version = "v1" });
            });
        }


    }
}
