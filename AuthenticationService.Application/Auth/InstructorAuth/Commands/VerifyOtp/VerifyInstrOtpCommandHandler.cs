using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.AuthRepo;

namespace UserService.Application.Auth.InstructorAuth.Commands.VerifyOtp
{
    public class VerifyInstrOtpCommandHandler : IRequestHandler<VerifyInstrOtpCommand, bool>
    {
        private readonly IInstructorAuthRepo _instructorAuthRepo;

        public VerifyInstrOtpCommandHandler(IInstructorAuthRepo instructorAuthRepo)
        {
            _instructorAuthRepo = instructorAuthRepo;
        }

        public async Task<bool> Handle(VerifyInstrOtpCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var instructor = await _instructorAuthRepo.GetVerifyUserByEmail(command.Email);
                if (instructor == null)
                {
                    throw new Exception("Instructo Not Found");
                }

                if (instructor.Otp != command.Otp)
                {
                    throw new Exception("Invalid Otp");
                }

                if (instructor.Expire_time < TimeOnly.FromDateTime(DateTime.Now))
                {
                    throw new Exception("OTP has expired.");
                }



                var newInstructor = new UserService.Domain.Entities.Instructor
                {
                    Name = instructor.Name,
                    Tag = null,
                    Email = instructor.Email,
                    Password = instructor.Password,
                    Profile = null,
                    Phone = null,
                    Certificate_Url = instructor.Certificate_Url,
                    Created_by = "Initial Create",
                    Created_on = DateTime.UtcNow,
                    Updated_by = "Initial Create",
                    Updated_on = DateTime.UtcNow,
                    Deleted_on = DateTime.UtcNow,
                    Deleted_by = "Initial Create"
                };

                await _instructorAuthRepo.AddInstructor(newInstructor);
                await _instructorAuthRepo.RemoveVerifyUser(instructor);
                await _instructorAuthRepo.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
