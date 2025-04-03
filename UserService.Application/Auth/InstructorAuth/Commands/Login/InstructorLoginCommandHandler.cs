using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorAuthDto;
using UserService.Application.RepoInterfaces.AuthRepo;
using UserService.Application.ServiceInterfaces.AuthServiceInterface;

namespace UserService.Application.Auth.InstructorAuth.Commands.Login
{
    public class InstructorLoginCommandHandler : IRequestHandler<InstructorLoginCommand, InstructorLoginResDto>
    {
        private readonly IInstructorAuthRepo _authRepo;
        private readonly IJwtInstrService _jwtInstrService;

        public InstructorLoginCommandHandler(IInstructorAuthRepo authRepo, IJwtInstrService jwtInstrService)
        {
            _authRepo = authRepo;
            _jwtInstrService = jwtInstrService;
        }

        public async Task<InstructorLoginResDto> Handle(InstructorLoginCommand command, CancellationToken cancellationToken)
        {
            var instructor = await _authRepo.GetInstructorByEmail(command.Email);
            if (instructor == null)
            {
                throw new Exception("Instructor not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(command.Password, instructor.Password))
            {
                throw new Exception("Invalid password.");
            }

            var token = _jwtInstrService.GenerateJwtToken(instructor);
            var loginRes = new InstructorLoginResDto
            {
                Id = instructor.Instructor_Id,
                Name = instructor.Name,
                Tag = instructor.Tag,
                Email = instructor.Email,
                Token = token,
                Profile = instructor.Profile
            };

            return loginRes;
        }

    }
}
