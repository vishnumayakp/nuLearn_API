using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.InstructorAuth.Commands
{
    public class InstructorLoginCommand:IRequest<string>
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
