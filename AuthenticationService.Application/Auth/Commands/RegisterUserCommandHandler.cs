using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Application.ServiceInterfaces;
using BCrypt.Net;
using MediatR;
using PlotLink.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Auth.Commands
{
    public class RegisterUserCommandHandler:IRequestHandler<RegisterUserCommand,bool>
    {
        private readonly IUserAuthRepo _userAuthRepo;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(IUserAuthRepo userAuthRepo, IEmailService emailService)
        {
            _userAuthRepo = userAuthRepo;
            _emailService = emailService;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = request;

            var existingUser = await _userAuthRepo.GetUserByEmail(userDto.Email);
            if(existingUser != null)
            {
                throw new Exception("Email already exists.");
            }

            var user=await _userAuthRepo.GetVerifyUserByEmail(userDto.Email);
            if(user != null)
            {
                await _userAuthRepo.RemoveVerifyUser(user);
                _userAuthRepo.SaveChangesAsync();
            }

            Random random = new Random();
            int otp = random.Next(100000,999999);
            await _emailService.SendOtp(userDto.Email, otp);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var expiryTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(5));

            var verifyUser = new VerifyUser
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = hashedPassword,
                Otp = otp,
                Expire_time = expiryTime,
            };

            await _userAuthRepo.AddVerifyUser(verifyUser);
            await _userAuthRepo.SaveChangesAsync();

            return true;
        }
    }
}
