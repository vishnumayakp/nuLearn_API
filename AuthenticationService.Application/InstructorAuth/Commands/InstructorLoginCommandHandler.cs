using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Application.ServiceInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.InstructorAuth.Commands
{
    public class LoginCommandHandler:IRequestHandler<LoginCommand, string>
    {
        private readonly IInstructorAuthRepo _authRepo;
        private readonly IJwtInstrService _jwtInstrService;

        public LoginCommandHandler(IInstructorAuthRepo authRepo, IJwtInstrService jwtInstrService)
        {
            _authRepo = authRepo;
            _jwtInstrService = jwtInstrService;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var instructor = await _authRepo.GetInstructorByEmail(command.Email);
                if (instructor == null)
                {
                    throw new Exception("Instructor not found.");
                }
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(command.Password, instructor.Password);
                if (!isPasswordValid)
                {
                    throw new Exception("Invalid password.");
                }
                var token = _jwtInstrService.GenerateJwtToken(instructor);
                return token;
            }
        }
    }
}
