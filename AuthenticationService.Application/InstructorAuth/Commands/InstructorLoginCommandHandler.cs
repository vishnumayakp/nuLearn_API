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
    public class InstructorLoginCommandHandler:IRequestHandler<InstructorLoginCommand, string>
    {
        private readonly IInstructorAuthRepo _authRepo;
        private readonly IJwtInstrService _jwtInstrService;

        public InstructorLoginCommandHandler(IInstructorAuthRepo authRepo, IJwtInstrService jwtInstrService)
        {
            _authRepo = authRepo;
            _jwtInstrService = jwtInstrService;
        }

        public async Task<string> Handle(InstructorLoginCommand command, CancellationToken cancellationToken)
        {
            var instructor = await _authRepo.GetInstructorByEmail(command.Email);
            if (instructor == null)
            {
                throw new Exception("Instructor not found.");
            }
            Console.WriteLine($"Stored Hash: {instructor.Password}");
            Console.WriteLine($"Entered Password: {command.Password}");
            Console.WriteLine($"Verification Result: {BCrypt.Net.BCrypt.Verify(command.Password, instructor.Password)}");


            if (!BCrypt.Net.BCrypt.Verify(command.Password, instructor.Password))
            {
                throw new Exception("Invalid password.");
            }

            return _jwtInstrService.GenerateJwtToken(instructor);
        }

    }
}
