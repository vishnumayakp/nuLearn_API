using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.InstructorAuth.Commands
{
    public class ResetPwCommand:IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public ResetPwCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
