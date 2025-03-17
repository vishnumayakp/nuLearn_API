using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using AuthenticationService.Application.Common;
using AuthenticationService.Application.ServiceInterfaces;
using AuthenticationService.Infrastructure.Services;
using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Infrastructure.Repositories;

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
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IJwtUserService, JwtService>();
            services.AddTransient<IJwtInstrService, JwtInstrService>();
            services.AddTransient<IJwtAdminService, JwtAdminService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();

            services.AddTransient<IUserAuthRepo, UserAuthRepository>();
            services.AddTransient<IInstructorAuthRepo, InstructorAuthRepo>();
            services.AddTransient<IAdminAuthRpo, AdminAuthrepo>();

            return services;
        }
    }
}
