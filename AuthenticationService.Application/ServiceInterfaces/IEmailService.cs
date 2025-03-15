using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.ServiceInterfaces
{
    public interface IEmailService
    {
        Task<bool> SendOtp(string email, int otp);
    }
}
