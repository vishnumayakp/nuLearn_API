using UserService.Application.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.ServiceInterfaces.AuthServiceInterface;
using Contracts.DTO;
using Microsoft.Extensions.Logging;

namespace UserService.Infrastructure.Services.AuthService
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public async Task<bool> SendOtp(string email, int otp)
        {
            try
            {
                string host = Environment.GetEnvironmentVariable("EMAIL_HOST");
                int port = int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT"));
                string senderEmail = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
                string password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");


                using (SmtpClient smtpClient = new SmtpClient(host))
                {
                    smtpClient.Port = port;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, password);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(senderEmail);
                        mailMessage.To.Add(email);
                        mailMessage.Subject = "OTP Verification";
                        mailMessage.Body = $"Your OTP for email verification is {otp}";

                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }


        public async Task SentEmail(AdminApprovalRequestDto emailDto)
        {
            try
            {
                string host = Environment.GetEnvironmentVariable("EMAIL_HOST");
                int port = int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT"));
                string senderEmail = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
                string password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
                string adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");

                _logger.LogInformation("📧 Attempting to send email for CourseId: {CourseId} to Admin", emailDto.CourseId, emailDto.InstructorId);

                using (SmtpClient smtpClient = new SmtpClient(host))
                {
                    smtpClient.Port = port;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, password);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(senderEmail);
                        mailMessage.To.Add(adminEmail);
                        mailMessage.Subject = "🔔 Course Approval Request";
                        mailMessage.IsBodyHtml = true;

                        StringBuilder emailBody = new StringBuilder();

                        emailBody.AppendLine("<h2 style='color: #2c3e50;'>📌 New Course Approval Request</h2>");
                        emailBody.AppendLine($"<p><strong>Instructor ID:</strong> {emailDto.InstructorId}</p>");
                        emailBody.AppendLine($"<p><strong>Course Name:</strong> {emailDto.CourseName}</p>");
                        emailBody.AppendLine($"<p><strong>Price:</strong> ${emailDto.Price}</p>");
                        emailBody.AppendLine($"<p><strong>Category:</strong> {emailDto.Category}</p>");
                        emailBody.AppendLine("<p><strong>Course Image:</strong></p>");
                        emailBody.AppendLine($"<img src='{emailDto.CourseImage}' alt='Course Image' style='width:500px; border-radius:10px; box-shadow: 2px 2px 10px #aaa;'/>");

                        if (emailDto.Videos != null && emailDto.Videos.Any())
                        {
                            emailBody.AppendLine("<h3>🎥 Videos:</h3><ul>");
                            foreach (var video in emailDto.Videos)
                            {
                                emailBody.AppendLine($"<li><a href='{video}' target='_blank' style='color:#2980b9;'>{video}</a></li>");
                            }
                            emailBody.AppendLine("</ul>");
                        }

                        if (emailDto.Documents != null && emailDto.Documents.Any())
                        {
                            emailBody.AppendLine("<h3>📄 Documents:</h3><ul>");
                            foreach (var document in emailDto.Documents)
                            {
                                emailBody.AppendLine($"<li><a href='{document}' target='_blank' style='color:#27ae60;'>{document}</a></li>");
                            }
                            emailBody.AppendLine("</ul>");
                        }

                        emailBody.AppendLine("<p>✅ <strong>Click the button below to approve the course:</strong></p>");
                        emailBody.AppendLine($"<a href='https://yourwebsite.com/admin/approve-course/{emailDto.CourseId}' style='display:inline-block; padding:10px 20px; background:#27ae60; color:#fff; text-decoration:none; border-radius:5px;'>Approve Course</a>");

                        mailMessage.Body = emailBody.ToString();

                        await smtpClient.SendMailAsync(mailMessage);

                        _logger.LogInformation("✅ Course Approval Email sent to Admin.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Email sending failed: {Message}", ex.Message);
                throw;
            }
        }


        public async Task SendCourseApprovalEmail(CourseApprovedEmailDto emailDto)
        {
            try
            {
                string host = Environment.GetEnvironmentVariable("EMAIL_HOST");
                int port = int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT"));
                string senderEmail = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
                string password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

                _logger.LogInformation("📧 Sending Course Approval Email to InstructorId={InstructorId}", emailDto.InstructorId);

                using (SmtpClient smtpClient = new SmtpClient(host))
                {
                    smtpClient.Port = port;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, password);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(senderEmail);
                        mailMessage.To.Add(emailDto.InstructorEmail);
                        mailMessage.Subject = "✅ Your Course is Approved!";
                        mailMessage.IsBodyHtml = true;

                        StringBuilder emailBody = new StringBuilder();
                        emailBody.AppendLine("<div style='font-family:Arial, sans-serif; padding:20px; border:1px solid #ddd; border-radius:10px; background:#f9f9f9;'>");
                        emailBody.AppendLine("<h2 style='color:#2c3e50;'>🎉 Congratulations, Your Course is Approved!</h2>");
                        emailBody.AppendLine($"<p>Dear Instructor,{emailDto.InstructorName}</p>");
                        emailBody.AppendLine($"<p>Your course <strong>{emailDto.CourseName}</strong> has been reviewed and successfully approved.</p>");
                        emailBody.AppendLine($"<p>It is now <span style='color:#27ae60; font-weight:bold;'>live</span> on our platform! 🎉</p>");
                        emailBody.AppendLine("<p><strong>🚀 Start Engaging with Students Today!</strong></p>");
                        emailBody.AppendLine("<p>Best Regards,</p>");
                        emailBody.AppendLine("<p>The nuLearn Team</p>");
                        emailBody.AppendLine("</div>");

                        mailMessage.Body = emailBody.ToString();

                        await smtpClient.SendMailAsync(mailMessage);

                        _logger.LogInformation("✅ Course Approval Email Sent to Instructor: {InstructorId}", emailDto.InstructorId);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Failed to send Instructor Approval Email: {Message}", ex.Message);
                throw;
            }
        }

    }
}
