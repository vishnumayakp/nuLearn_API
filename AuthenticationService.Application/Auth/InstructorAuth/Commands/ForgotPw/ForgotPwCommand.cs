using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Auth.InstructorAuth.Commands.ForgotPw
{
    public class ForgotPwCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public ForgotPwCommand(string email)
        {
            Email = email;
        }
    }
}
