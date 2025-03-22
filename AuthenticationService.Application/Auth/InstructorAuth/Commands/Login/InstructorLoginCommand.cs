using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorAuthDto;

namespace UserService.Application.Auth.InstructorAuth.Commands.Login
{
    public class InstructorLoginCommand : IRequest<InstructorLoginResDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public InstructorLoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
