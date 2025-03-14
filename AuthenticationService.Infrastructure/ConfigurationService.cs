using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using AuthenticationService.Application.Common;

namespace AuthenticationService.Infrastructure
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,Appsettings appsettings)
        {


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(appsettings.DbConnectionString);
            });

            return services;
        }
    }
}
