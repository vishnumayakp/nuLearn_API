using AuthenticationService.Application.InstructorAuth.Commands;
using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Application.ServiceInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.AdminAuth.Commands
{
    public class AdminLoginCommandHandler : IRequestHandler<AdminLoginCommand, string>
    {
        private readonly IAdminAuthRpo _authRepo;
        private readonly IJwtAdminService _jwtAdminService;

        public AdminLoginCommandHandler(IAdminAuthRpo authRepo, IJwtAdminService jwtAdminService)
        {
            _authRepo = authRepo;
            _jwtAdminService= jwtAdminService;
        }

        public async Task<string> Handle(AdminLoginCommand command, CancellationToken cancellationToken)
        {
            var instructor = await _authRepo.GetAdminorByEmail(command.Email);
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

            return _jwtAdminService.GenerateJwtToken(instructor);
        }

    }
}
