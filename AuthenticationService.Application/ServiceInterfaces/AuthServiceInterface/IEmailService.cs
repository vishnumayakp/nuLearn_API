using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.ServiceInterfaces.AuthServiceInterface
{
    public interface IEmailService
    {
        Task<bool> SendOtp(string email, int otp);
    }
}
