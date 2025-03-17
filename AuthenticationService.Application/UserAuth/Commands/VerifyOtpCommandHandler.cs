using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Auth.Commands
{
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, bool>
    {
        private readonly IUserAuthRepo _userAuthRepo;

        public VerifyOtpCommandHandler(IUserAuthRepo userAuthRepo)
        {
            _userAuthRepo = userAuthRepo;
        }

        public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _userAuthRepo.GetVerifyUserByEmail(request.Email);
            if (user == null)
            {
                throw new Exception("User not found or OTP expired.");
            }

            if (user.Otp != request.Otp)
            {
                throw new Exception("Invalid OTP.");
            }

            if (user.Expire_time < TimeOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("OTP has expired.");
            }

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Profile = null,
                Created_by = "Initial Create",
                Created_on = DateTime.UtcNow,
                Updated_by = "Initial Create",
                Updated_on = DateTime.UtcNow,
                Deleted_on = DateTime.UtcNow,
                Deleted_by="Initial Create"
            };

            await _userAuthRepo.AddUser(newUser);
            await _userAuthRepo.RemoveVerifyUser(user);
            await _userAuthRepo.SaveChangesAsync();

            return true;
        }
    }
}
