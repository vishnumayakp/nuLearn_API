using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Application.ServiceInterfaces;
using AuthenticationService.Domain.Entities;
using MediatR;
using PlotLink.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.InstructorAuth.Commands
{
    public class RegisterInstructorCommandHandler:IRequestHandler<RegisterInstructorCommand, bool>
    {
        private readonly IInstructorAuthRepo _authRepo;
        private readonly IEmailService _emailService;
        private readonly ICloudinaryService _cloudinaryService;

        public RegisterInstructorCommandHandler(IInstructorAuthRepo authRepo, IEmailService emailService, ICloudinaryService cloudinaryService)
        {
            _authRepo = authRepo;
            _emailService = emailService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<bool> Handle(RegisterInstructorCommand command , CancellationToken cancellationToken)
        {
            try
            {
                var existingInstructors = await _authRepo.GetInstructorByEmail(command.Email);
                if (existingInstructors != null)
                {
                    throw new Exception("Email already exists.");
                }

                var instructor = await _authRepo.GetVerifyUserByEmail(command.Email);
                if (instructor != null)
                {
                    await _authRepo.RemoveVerifyUser(instructor);
                    _authRepo.SaveChangesAsync();
                }

                Random random = new Random();
                int otp = random.Next(100000, 999999);
                await _emailService.SendOtp(command.Email, otp);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);
                var expiryTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(5));

                var verifyInstructor = new VerifyInstructor
                {

                    Name = command.Name,
                    Email = command.Email,
                    Password = hashedPassword,
                    Otp = otp,
                    Certificate_Url=command.UploadFile.ToString(),
                    Expire_time = expiryTime,
                };

                await _authRepo.AddVerifyUser(verifyInstructor);
                await _authRepo.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
