using AuthenticationService.Application.DTO.UserAuthDto;
using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Application.ServiceInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Auth.Commands
{
    public class LoginCommandHandler:IRequestHandler<LoginCommand, string>
    {
        private readonly IUserAuthRepo _userAuthRepo;
        private readonly IJwtUserService _jwtService;

        public LoginCommandHandler(IUserAuthRepo userAuthRepo, IJwtUserService jwtService)
        {
            _userAuthRepo = userAuthRepo;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userAuthRepo.GetUserByEmail(request.Email);
            if(user == null)
            {
                throw new Exception("User not found.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if(!isPasswordValid)
            {
                throw new Exception("Invalid password.");
            }

            var token =  _jwtService.GenerateJwtToken(user);
            return token;
        }
    }
}
