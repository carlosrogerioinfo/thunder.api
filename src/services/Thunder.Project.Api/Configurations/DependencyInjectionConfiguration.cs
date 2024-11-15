using Thunder.Project.Domain.Repositories;
using Thunder.Project.Infrastructure.Repositories;
using Thunder.Project.Infrastructure.Transactions;
using Thunder.Project.Service;

namespace Thunder.Project.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration = null)
        {

            services.AddScoped<IUow, Uow>();

            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<TodoService>();

            return services;
        }
    }

    
}
