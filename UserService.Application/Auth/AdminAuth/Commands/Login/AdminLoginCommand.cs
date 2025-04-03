using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Auth.AdminAuth.Commands.Login
{
    public class AdminLoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AdminLoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
