using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Application.ServiceInterfaces.AuthServiceInterface
{
    public interface IEmailService
    {
        Task<bool> SendOtp(string email, int otp);
        Task SentEmail(AdminApprovalRequestDto dto);
        Task SendCourseApprovalEmail(CourseApprovedEmailDto emailDto);
    }
}
