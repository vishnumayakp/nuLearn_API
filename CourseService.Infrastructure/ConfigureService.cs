using Contracts.Events.Request;
using CourseService.Application.Common;
using CourseService.Application.Consumers;
using CourseService.Application.RepoInterface.CourseRepoInterface;
using CourseService.Application.RepoInterface.ICategoryRepoInterface;
using CourseService.Application.RepoInterface.SubCateRepoInterface;
using CourseService.Application.ServiceInterface;
using CourseService.Infrastructure.Repositories;
using CourseService.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseService.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, AppSettings appsettings)
        {
            // Configure PostgreSQL Database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(appsettings.DbConnectionString);
            });

            // Register Repositories
            services.AddTransient<ICategoryRepo, CategoryRepo>();
            services.AddTransient<ISubCategoryRepo, SubCategoryRepo>();
            services.AddTransient<ICourseRepo, CourseRepo>();
            services.AddTransient<IVideoRepo, VideoRepo>();
            services.AddTransient<IDocumentRepo, DocumentRepo>();

            // Register Services
            services.AddTransient<ICloudinaryService, CloudinaryService>();

            // Configure MassTransit with RabbitMQ
            services.AddMassTransit(config =>
            {
                config.AddConsumer<AdminVerifiedConsumer>(); // Register Consumer
                config.AddConsumer<CourseApprovalConsumer>();

                config.AddRequestClient<InstructorVerificationRequested>();



                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(appsettings.Host, h =>
                    {
                        h.Username(appsettings.UserName);
                        h.Password(appsettings.Password);
                    });

                    cfg.ReceiveEndpoint("admin-verification-queue", e =>
                    {
                        e.ConfigureConsumer<AdminVerifiedConsumer>(ctx);
                    });

                    cfg.ReceiveEndpoint("course-approval-queue", e =>
                    {
                        e.ConfigureConsumer<CourseApprovalConsumer>(ctx);
                    });
                });
            });


            return services;
        }
    }
}
