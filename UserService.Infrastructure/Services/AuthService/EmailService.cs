using UserService.Application.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.ServiceInterfaces.AuthServiceInterface;

namespace UserService.Infrastructure.Services.AuthService
{
    public class EmailService : IEmailService
    {
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
    }
}
