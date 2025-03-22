using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.UserAuthDto;
using UserService.Application.RepoInterfaces.AuthRepo;
using UserService.Application.ServiceInterfaces.AuthServiceInterface;

namespace UserService.Application.Auth.UserAuth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserLoginResDto>
    {
        private readonly IUserAuthRepo _userAuthRepo;
        private readonly IJwtUserService _jwtService;

        public LoginCommandHandler(IUserAuthRepo userAuthRepo, IJwtUserService jwtService)
        {
            _userAuthRepo = userAuthRepo;
            _jwtService = jwtService;
        }

        public async Task<UserLoginResDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userAuthRepo.GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Invalid password.");
            }

            var token = _jwtService.GenerateJwtToken(user);
            var loginRes = new UserLoginResDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token,
                Profile = user.Profile
            };

            return loginRes;
        }
    }
}
