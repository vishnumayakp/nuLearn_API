using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Auth.Commands
{
    public class VerifyOtpResetPwCommand:IRequest<bool>
    {
        public string Email { get; set; }
        public int Otp { get; set; }

        public VerifyOtpResetPwCommand(string email,int otp)
        {
            Email = email;
            Otp = otp;
        }
    }
}
