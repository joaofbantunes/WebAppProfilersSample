using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAppProfilersSample.Shared.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProfilingSampleDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(ProfilingSampleDbContext)));
            });
            return services;
        }

        public static IServiceCollection AddConfiguredMvc(this IServiceCollection services)
        {
            services
                .AddMvc()
                .AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }
    }
}
