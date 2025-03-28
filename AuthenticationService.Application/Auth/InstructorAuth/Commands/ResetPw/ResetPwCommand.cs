﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Auth.InstructorAuth.Commands.ResetPw
{
    public class ResetPwCommand : IRequest<bool>
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
