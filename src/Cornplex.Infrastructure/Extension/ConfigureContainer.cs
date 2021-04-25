namespace Cornplex.Infrastructure.Extension
{
    using Microsoft.AspNetCore.Builder;

    public static class ConfigureContainer
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cornplex Api v1"));
        }
    }
}
