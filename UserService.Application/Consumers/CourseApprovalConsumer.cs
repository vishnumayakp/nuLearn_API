using Contracts.DTO;
using Contracts.Events.Request;
using Contracts.Events.Response;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.ServiceInterfaces.AuthServiceInterface;

namespace UserService.Application.Consumers
{
    public class CourseApprovalConsumer: IConsumer<CourseApprovalRequested>
    {
        private readonly ILogger<CourseApprovalConsumer> _logger;
        private readonly IEmailService _emailService;

        public CourseApprovalConsumer(ILogger<CourseApprovalConsumer> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<CourseApprovalRequested> context)
        {
            _logger.LogInformation($"Received CourseApprovalRequested for CourseId: {context.Message.CourseId},{context.Message.InstructorId}");

            bool isApproved = true;

            var response = new CourseApproved
            {
                CourseId = context.Message.CourseId,
                IsApproved = isApproved
            };

            await context.RespondAsync(response);
            _logger.LogInformation("✅ Sent CourseApproved response.");

            var emailDto = new AdminApprovalRequestDto
            {
                InstructorId = context.Message.InstructorId,
                CourseId = context.Message.CourseId,
                CourseName = context.Message.CourseName,
                CourseImage = context.Message.ImageUrl,
                Price = context.Message.Price,
                Category = context.Message.Category,
                Videos = context.Message.VideoUrls,
                Documents = context.Message.DocumentUrls
            };
            _logger.LogInformation("📧 Preparing to send email for CourseId: {CourseId} to Admin.", context.Message.CourseId);
            await _emailService.SentEmail(emailDto);


            _logger.LogInformation("📨 Email function executed.");
        }


    }
}
