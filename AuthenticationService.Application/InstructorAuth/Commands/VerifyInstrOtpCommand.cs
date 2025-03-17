﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.InstructorAuth.Commands
{
    public class VerifyInstrOtpCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public int Otp { get; set; }

        public VerifyInstrOtpCommand(string email, int otp)
        {
            Email = email;
            Otp = otp;
        }
    }
}
