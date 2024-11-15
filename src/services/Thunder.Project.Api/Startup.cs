using Microsoft.EntityFrameworkCore;
using Thunder.Project.Api.Configurations;
using Thunder.Project.Infrastructure.Contexts;

namespace Thunder.Project.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddDataContextConfigurations(services);

            services.AddAutoMapperConfiguration();

            services.AddWebApiConfiguration();

            services.AddSwaggerConfiguration();

            services.AddDependencyInjectionConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebApiConfiguration(true);

            app.UseSwaggerConfiguration(env);
        }

        private void AddDataContextConfigurations(IServiceCollection services)
        {
            services.AddDbContext<TodoDataContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")
                    , sqlServerOptionsBuilder => sqlServerOptionsBuilder.UseCompatibilityLevel(120)
                );
                opt.EnableSensitiveDataLogging();

            }, ServiceLifetime.Scoped);

        }
    }
}
