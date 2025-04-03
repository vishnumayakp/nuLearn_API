using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.ServiceInterfaces;

namespace UserService.Infrastructure.Services
{
    public class EmailService:IEmailService
    {
        public async Task SentEmail(string instructorName, string courseName, Guid courseId)
        {
            try
            {
                string host = Environment.GetEnvironmentVariable("EMAIL_HOST");
                int port = int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT"));
                string senderEmail = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
                string password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
                string adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");

                using (SmtpClient smtpClient = new SmtpClient(host))
                {
                    smtpClient.Port = port;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, password);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(senderEmail);
                        mailMessage.To.Add(adminEmail);
                        mailMessage.Subject = "Course Approval Request";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = $@"
                        <h2>New Course Approval Request</h2>
                        <p><strong>Instructor:</strong> {instructorName}</p>
                        <p><strong>Course Name:</strong> {courseName}</p>
                        <p>Click the link below to approve the course:</p>
                        <a href='https://yourwebsite.com/admin/approve-course/{courseId}'>Approve Course</a>
                    ";
                        await smtpClient.SendMailAsync(mailMessage);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
