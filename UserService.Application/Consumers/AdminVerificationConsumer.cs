using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts.Events.Request;
using Contracts.Events.Response;
using UserService.Application.RepoInterfaces.ViewRepo;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace UserService.Application.Consumers
{
    public class AdminVerificationConsumer : IConsumer<AdminVerificationRequested>
    {
        private readonly IAdminViewRepo _viewRepo;
        private readonly IPublishEndpoint _publishEndpoint; // 🔹 Store IPublishEndpoint
        private readonly ILogger<AdminVerificationConsumer> _logger;

        public AdminVerificationConsumer(IAdminViewRepo viewRepo, IPublishEndpoint publishEndpoint, ILogger<AdminVerificationConsumer> logger)
        {
            _viewRepo = viewRepo;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<AdminVerificationRequested> context)
        {
            try
            {
                var exists = await _viewRepo.AdminExists(context.Message.AdminId);
                if (!exists)
                {
                    throw new Exception("Admin Not Found");
                    
                }

                var isAdmin = await _viewRepo.GetAdminRole(context.Message.AdminId);
                if (isAdmin==null)
                {
                    throw new Exception("Role Validation Failed");
                }

                await context.RespondAsync(new AdminVerified(context.Message.AdminId, true,
                    context.Message.CategoryName, context.Message.Image));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin verification failed");
                await context.RespondAsync(new AdminVerified(context.Message.AdminId, false, "", ""));
            }

        }
    }
}
