using Challenge.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Challenge.API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection GetServiceCollection(this IServiceCollection services)
        {
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<ChallengeDBContext>();

            return services;
        }
    }
}
