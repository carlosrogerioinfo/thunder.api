﻿using System.Text.Json.Serialization;

namespace Thunder.Project.Api.Configurations
{
    public static class WebApiConfiguration
    {

        public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => { 
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            return services;
        }

        public static IApplicationBuilder UseWebApiConfiguration(this IApplicationBuilder app, bool useCors = false)
        {
            app.UseRouting();

            if (useCors)
            {
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }

    }
}
