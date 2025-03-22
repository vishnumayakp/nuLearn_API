using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using UserService.Infrastructure.Services;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.Repositories.AuthRepositories;
using UserService.Infrastructure.Services.AuthService;
using UserService.Infrastructure;
using UserService.Infrastructure;
using UserService.Application.ServiceInterfaces.AuthServiceInterface;
using UserService.Application.RepoInterfaces.AuthRepo;
using UserService.Infrastructure.Repositories;
using UserService.Application.ServiceInterfaces;
using UserService.Application.Common;
using UserService.Application.Common;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Infrastructure.Repositories.ViewRepositories;
using UserService.Application.ServiceInterfaces.UserViewServiceInterface;
using UserService.Infrastructure.Services.UserViewService;

namespace UserService.Infrastructure
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
            services.AddTransient<IUserViewService, UserViewService>();

            services.AddTransient<IUserAuthRepo, UserAuthRepository>();
            services.AddTransient<IInstructorAuthRepo, InstructorAuthRepo>();
            services.AddTransient<IAdminAuthRpo, AdminAuthrepo>();
            services.AddTransient<IUserViewRepo, UserViewRepo>();
            services.AddTransient<IInstructorViewRepo, InstructorViewRepo>();
            return services;
        }
    }
}
