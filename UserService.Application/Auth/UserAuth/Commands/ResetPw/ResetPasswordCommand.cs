using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Auth.UserAuth.Commands.ResetPw
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public ResetPasswordCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
