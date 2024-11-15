using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Thunder.Project.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Thunder Backend Api",
                    Description = "Api backend of project Thunder",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Thunder",
                        Email = "thunder@thunder.com",
                        Url = new Uri("https://thunder.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licença de uso: Thunder | Api backend",
                        Url = new Uri("https://thunder.com")
                    }
                });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
            else
            {
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = string.Empty;
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }


            return app;
        }

    }
}
