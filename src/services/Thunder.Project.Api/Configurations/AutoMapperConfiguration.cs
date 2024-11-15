using Thunder.Project.Domain.Profiles;

namespace Thunder.Project.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(TodoProfile)
            );

            return services;
        }
    }
}
